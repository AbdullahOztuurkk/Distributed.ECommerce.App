using Clicco.Application.Helpers.Contracts;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Domain.Shared.Models.Email;
using Clicco.Domain.Shared.Models.Payment;
using MediatR;

namespace Clicco.Application.Features.Commands.Payment
{
    public class CreateTransactionCommandHandler : IRequestHandler<PaymentRequest, PaymentResult>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ITransactionDetailRepository transactionDetailRepository;
        private readonly ITransactionService transactionService;
        private readonly ICouponService couponService;
        private readonly IPaymentService paymentService;
        private readonly IClaimHelper claimHelper;
        private readonly IEmailService emailService;
        private readonly ICacheManager cacheManager;
        private readonly IInvoiceService invoiceService;
        public CreateTransactionCommandHandler(
            ITransactionRepository transactionRepository,
            ITransactionService transactionService,
            IPaymentService paymentService,
            IClaimHelper claimHelper,
            ITransactionDetailRepository transactionDetailRepository,
            IEmailService emailService,
            ICacheManager cacheManager,
            ICouponService couponService,
            IInvoiceService invoiceService)
        {
            this.transactionRepository = transactionRepository;
            this.transactionService = transactionService;
            this.paymentService = paymentService;
            this.claimHelper = claimHelper;
            this.transactionDetailRepository = transactionDetailRepository;
            this.emailService = emailService;
            this.cacheManager = cacheManager;
            this.couponService = couponService;
            this.invoiceService = invoiceService;
        }
        public async Task<PaymentResult> Handle(PaymentRequest request, CancellationToken cancellationToken)
        {
            await transactionService.CheckProductIdAsync(request.ProductId);
            await transactionService.CheckAddressIdAsync(request.AddressId);

            var product = await transactionService.GetProductByIdAsync(request.ProductId);
            var address = await transactionService.GetAddressByIdAsync(request.AddressId);
            var userEmail = claimHelper.GetUserEmail();

            var transaction = new Transaction();

            transaction.Code = GetUniqueCode();
            transaction.AddressId = request.AddressId;
            transaction.Dealer = product.Vendor.Name;
            transaction.CreatedDate = DateTime.UtcNow;
            transaction.DeliveryDate = DateTime.UtcNow.AddDays(7);
            transaction.TotalAmount = product.UnitPrice * request.Quantity;
            transaction.DiscountedAmount = transaction.TotalAmount;
            transaction.UserId = claimHelper.GetUserId();

            transaction.TransactionDetail = new TransactionDetail
            {
                ProductId = request.ProductId,
                Transaction = transaction
            };
            await transactionRepository.AddAsync(transaction);
            await transactionRepository.SaveChangesAsync();
            await transactionDetailRepository.SaveChangesAsync();
            transaction.TransactionDetailId = transaction.TransactionDetail.Id;

            if (request.CouponId.HasValue)
            {
                await couponService.CheckSelfId(request.CouponId.Value);

                var coupon = await transactionService.GetCouponByIdAsync(request.CouponId.Value);

                await couponService.IsAvailable(transaction, coupon);

                await couponService.Apply(transaction, coupon);

                await cacheManager.SetAsync(CacheKeys.GetSingleKey<Coupon>(request.CouponId.Value), request.CouponId.Value.ToString());
            }

            var bankRequest = new PaymentBankRequest
            {
                BankId = request.BankId,
                CardInformation = request.CardInformation,
                DealerName = product.Vendor.Name,
                ProductName = product.Name,
                TotalAmount = (int)(transaction.DiscountedAmount < transaction.TotalAmount
                        ? transaction.DiscountedAmount
                        : transaction.TotalAmount),
            };

            var result = await paymentService.Pay(bankRequest);

            if (result.IsSuccess)
            {
                transaction.TransactionStatus = TransactionStatus.Success;

                await emailService.SendSuccessPaymentEmailAsync(new PaymentSuccessEmailRequest
                {
                    Amount = transaction.DiscountedAmount < transaction.TotalAmount
                        ? transaction.DiscountedAmount.ToString()
                        : transaction.TotalAmount.ToString(),
                    FullName = claimHelper.GetUserName(),
                    OrderNumber = transaction.Code,
                    PaymentMethod = "Credit / Bank Card",
                    ProductName = product.Name,
                    To = userEmail,
                });

            }
            else
            {
                transaction.TransactionStatus = TransactionStatus.Failed;

                await emailService.SendFailedPaymentEmailAsync(new PaymentFailedEmailRequest
                {
                    Amount = transaction.DiscountedAmount < transaction.TotalAmount 
                        ? transaction.DiscountedAmount.ToString() 
                        : transaction.TotalAmount.ToString(),
                    FullName = claimHelper.GetUserName(),
                    OrderNumber = transaction.Code,
                    PaymentMethod = "Credit / Bank Card",
                    ProductName = product.Name,
                    To = claimHelper.GetUserEmail(),
                    Error = result.Message
                });
            }

            await transactionRepository.SaveChangesAsync();

            await invoiceService.CreateInvoice(userEmail, transaction, product, address);

            if(request.CouponId.HasValue)
                await cacheManager.RemoveAsync(CacheKeys.GetSingleKey<Coupon>(request.CouponId.Value));

            return result.IsSuccess ? new SuccessPaymentResult() : new FailedPaymentResult(result.Message);
        }

        private string GetUniqueCode()
        {
            DateTime now = DateTime.Now;
            string year = now.Year.ToString();
            string month = now.Month.ToString("00");
            string day = now.Day.ToString("00");
            string hour = now.Hour.ToString("00");
            string minute = now.Minute.ToString("00");
            string second = now.Second.ToString("00");
            string millisecond = now.Millisecond.ToString("00");
            return year + month + day + hour + minute + second + millisecond;
        }
    }
}
