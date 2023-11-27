using Clicco.Application.Interfaces.Helpers;
using Clicco.Application.Services.Abstract;
using Clicco.Application.Services.Abstract.External;
using Clicco.Domain.Model.Dtos.Transaction;
using System.Text;

namespace Clicco.Application.Services.Concrete
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSessionService _userSessionService;
        private readonly ICacheManager _cacheManager;
        private readonly ICouponManagementHelper _couponManagementHelper;
        private readonly IInvoiceService _invoiceService;
        private readonly IPaymentService _paymentService;
        public TransactionService(
            IUnitOfWork unitOfWork,
            IUserSessionService userSessionService,
            ICacheManager cacheManager,
            ICouponManagementHelper couponManagementHelper,
            IInvoiceService invoiceService,
            IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _userSessionService = userSessionService;
            _cacheManager = cacheManager;
            _couponManagementHelper = couponManagementHelper;
            _invoiceService = invoiceService;
            _paymentService = paymentService;
        }
        public async Task<ResponseDto> Create(CreateTransactionDto dto)
        {
            ResponseDto response = new();
            var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(dto.ProductId);
            if (product == null)
                return response.Fail(Errors.ProductNotFound);

            var address = await _unitOfWork.GetRepository<Address>().GetByIdAsync(dto.AddressId);
            if (address == null)
                return response.Fail(Errors.AddressNotFound);

            var userEmail = _userSessionService.GetUserEmail();
            Transaction transaction = new();

            transaction.Code = GetUniqueCode();
            transaction.AddressId = dto.AddressId;
            transaction.Dealer = product.Vendor.Name;
            transaction.CreatedDate = DateTime.UtcNow;
            transaction.DeliveryDate = DateTime.UtcNow.AddDays(7);
            transaction.TotalAmount = product.UnitPrice * dto.Quantity;
            transaction.DiscountedAmount = transaction.TotalAmount;
            transaction.UserId = _userSessionService.GetUserId();
            transaction.CreatedDate = DateTime.UtcNow.AddHours(3);

            transaction.TransactionDetail = new TransactionDetail
            {
                ProductId = dto.ProductId,
                Transaction = transaction
            };
            await _unitOfWork.GetRepository<Transaction>().AddAsync(transaction);
            await _unitOfWork.GetRepository<Transaction>().SaveChangesAsync();
            transaction.TransactionDetailId = transaction.TransactionDetail.Id;
            transaction.TransactionDetail.CreatedDate = DateTime.UtcNow.AddHours(3);

            var cacheKey = string.Format(CacheKeys.ActiveCoupon,dto.CouponId.Value);
            if (dto.CouponId.HasValue)
            {
                var coupon = await _unitOfWork.GetRepository<Coupon>().GetByIdAsync(dto.CouponId.Value);
                if (coupon == null)
                {
                    transaction.TransactionStatus = TransactionStatus.Failed;
                    _unitOfWork.GetRepository<Transaction>().Update(transaction);
                    await _unitOfWork.GetRepository<Transaction>().SaveChangesAsync();
                    return response.Fail(Errors.CouponNotFound);
                }

                var isAvailable = await _couponManagementHelper.IsAvailable(product, coupon);
                if (!isAvailable.IsSuccess)
                {
                    transaction.TransactionStatus = TransactionStatus.Failed;
                    _unitOfWork.GetRepository<Transaction>().Update(transaction);
                    await _unitOfWork.GetRepository<Transaction>().SaveChangesAsync();
                    return response.Fail(Errors.CouponIsNowUsed);
                }
                await _couponManagementHelper.Apply(transaction, coupon);
                await _cacheManager.SetAsync(cacheKey, coupon);
            }

            var bankRequest = new PaymentBankRequest
            {
                BankId = dto.BankId,
                TransactionId = transaction.Id,
                CardInformation = dto.CardInformation,
                DealerName = product.Vendor.Name,
                ProductName = product.Name,
                FullName = _userSessionService.GetUserName(),
                OrderNumber = transaction.Code,
                To = userEmail,
                PaymentMethod = "Credit / Bank Card",
                TotalAmount = (int)(transaction.DiscountedAmount < transaction.TotalAmount
                ? transaction.DiscountedAmount
                        : transaction.TotalAmount),
            };

            await _paymentService.Pay(bankRequest);
            await _invoiceService.CreateInvoice(userEmail, transaction, product, address);

            if (dto.CouponId.HasValue)
                await _cacheManager.RemoveAsync(cacheKey);

            response.Message = "Your payment dto has been received. You will be informed via email.";
            return response;
        }

        private string GetUniqueCode()
        {
            DateTime now = DateTime.Now;

            StringBuilder strBuilder = new StringBuilder(17); // Assuming a maximum of 17 characters
            strBuilder.Append(now.Year);
            strBuilder.Append(now.Month.ToString("00"));
            strBuilder.Append(now.Day.ToString("00"));
            strBuilder.Append(now.Hour.ToString("00"));
            strBuilder.Append(now.Minute.ToString("00"));
            strBuilder.Append(now.Second.ToString("00"));
            strBuilder.Append(now.Millisecond.ToString("000"));

            return strBuilder.ToString();
        }

        public async Task<ResponseDto> Delete(int id)
        {
            ResponseDto response = new();

            var transaction = await _unitOfWork.GetRepository<Transaction>().GetAsync(x => x.Id == id);

            if (transaction == null)
                return response.Fail(Errors.TransactionNotFound);

            transaction.IsDeleted = true;
            _unitOfWork.GetRepository<Transaction>().Update(transaction);
            await _unitOfWork.SaveChangesAsync();

            return response;
        }

        public async Task<ResponseDto> Get(int id)
        {
            ResponseDto response = new();

            var transaction = await _unitOfWork.GetRepository<Transaction>().GetAsync(x => x.Id == id);

            if (transaction == null)
                return response.Fail(Errors.TransactionNotFound);

            response.Data = new TransactionResponseDto().Map(transaction);

            return response;
        }

        public async Task<ResponseDto> GetAll(PaginationFilter filter)
        {
            ResponseDto response = new();
            filter.PageNumber = filter.PageNumber > 1 ? filter.PageNumber : 1;
            filter.PageSize = filter.PageNumber > 10 ? filter.PageNumber : 10;

            var transactions = await _unitOfWork.GetRepository<Transaction>().PaginateAsync(filter,null);
            var data = transactions.Select(x => new TransactionResponseDto().Map(x));
            
            response.Data = data;
            return response;
        }

        public async Task<ResponseDto> GetTransactionDetailById(int id)
        {
            ResponseDto response = new();
            var transaction = await _unitOfWork.GetRepository<Transaction>().GetByIdAsync(id, o => o.TransactionDetail,
                o => o.TransactionDetail.Product,
                o => o.Coupon != null ? o.Coupon : new Coupon(),
                o => o.Address);

            if (transaction == null)
                return response.Fail(Errors.TransactionNotFound);

            var transactionDetail = new TransactionDetailResponseDto().Map(transaction);
            response.Data = transactionDetail;

            return response;
        }

        public async Task<ResponseDto> Update(UpdateTransactionDto dto)
        {
            ResponseDto response = new();
            var transaction = await _unitOfWork.GetRepository<Transaction>().GetByIdAsync(dto.Id);
            if (transaction == null)
                return response.Fail(Errors.ProductNotFound);

            var address = await _unitOfWork.GetRepository<Address>().GetByIdAsync(dto.AddressId);
            if (address == null)
                return response.Fail(Errors.AddressNotFound);

            transaction.AddressId = dto.AddressId;
            transaction.Address = address;
            transaction.Code = dto.Code;
            transaction.UpdatedDate = DateTime.UtcNow.AddHours(3);

            _unitOfWork.GetRepository<Transaction>().Update(transaction);
            await _unitOfWork.SaveChangesAsync();

            //await _invoiceService.PushMessage(ExchangeNames.EventExchange, new InvoiceTransaction
            //{
            //    Code = transaction.Code,
            //    CreatedDate = transaction.CreatedDate,
            //    Dealer = transaction.Dealer,
            //    DeliveryDate = transaction.DeliveryDate,
            //    DiscountedAmount = transaction.DiscountedAmount,
            //    TotalAmount = transaction.TotalAmount,
            //    Id = transaction.Id,
            //    TransactionStatus = transaction.TransactionStatus.GetDescription()
            //}, EventNames.UpdatedTransaction);

            response.Data = new TransactionResponseDto().Map(transaction);

            return response;
        }

        public async Task<ResponseDto> GetInvoiceEmailByTransactionId(int Id)
        {
            ResponseDto response = new();

            var transaction = await _unitOfWork.GetRepository<Transaction>().GetAsync(x => x.Id == Id);

            if (transaction == null)
                return response.Fail(Errors.TransactionNotFound);

            await _invoiceService.SendEmailByTransactionId(transactionId: Id);
            
            return response;
        }
    }
}
