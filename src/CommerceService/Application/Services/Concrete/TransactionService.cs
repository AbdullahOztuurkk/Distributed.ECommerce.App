using CommerceService.Application.Services.Abstract;
using MassTransit;
using Shared.Domain.Constant;
using Shared.Domain.ValueObject;
using Shared.Events.Mail;
using Shared.Events.Mail.Base;
using Shared.Events.Order;

namespace CommerceService.Application.Services.Concrete;

public class TransactionService : BaseService, ITransactionService
{
    private readonly ICouponService _couponService;
    private readonly IPaymentService _paymentService;
    private readonly IUserSessionService _userSessionService;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    public TransactionService(
        IPaymentService _paymentService,
        ICouponService _couponService,
        IUserSessionService _userSessionService,
        ISendEndpointProvider sendEndpointProvider)
    {
        this._paymentService = _paymentService;
        this._couponService = _couponService;
        this._userSessionService = _userSessionService;
        this._sendEndpointProvider = sendEndpointProvider;
    }
    public async Task<BaseResponse> Create(CreateTransactionRequestDto request)
    {
        BaseResponse response = new();
        var product = await Db.GetDefaultRepo<Product>().GetByIdAsync(request.ProductId);
        if (product == null)
            return response.Fail(Error.E_0000);

        var productVendor = await Db.GetDefaultRepo<Vendor>().GetByIdAsync(product.VendorId);
        if (productVendor == null)
            return response.Fail(Error.E_0000);

        var address = await Db.GetDefaultRepo<Address>().GetByIdAsync(request.AddressId);
        if (address == null)
            return response.Fail(Error.E_0000);

        Transaction transaction = new();

        transaction.Code = GetUniqueCode();
        transaction.AddressId = request.AddressId;
        transaction.Dealer = productVendor.Name;
        transaction.DeliveryDate = DateTime.UtcNow.AddDays(7);
        transaction.TotalAmount = product.UnitPrice * request.Quantity;
        transaction.DiscountedAmount = transaction.TotalAmount;
        transaction.UserId = CurrentUser.Id;
        transaction.ProductId = request.ProductId;
        transaction.Product = product;

        await Db.GetDefaultRepo<Transaction>().InsertAsync(transaction);
        await Db.GetDefaultRepo<Transaction>().SaveChanges();

        var cacheKey = string.Format(CacheKeys.ActiveCoupon,request.CouponId.Value);
        Coupon coupon = null;
        if (request.CouponId.HasValue)
        {
            coupon = await Db.GetDefaultRepo<Coupon>().GetByIdAsync(request.CouponId.Value);
            var availability = await _couponService.CanBeAppliable(coupon,product);

            if (!availability.IsSuccess)
            {
                transaction.TransactionStatus = TransactionStatus.Failed;
                await Db.GetDefaultRepo<Transaction>().SaveChanges();
                return availability;
            }
            await _couponService.Apply(coupon, transaction);

            await CacheService.SetAsync(cacheKey, coupon);
        }

        var orderCreatedEvent = new OrderCreatedStockCheckRequestEvent
        {
            BuyerEmail = _userSessionService.GetUserEmail(),
            FullName = _userSessionService.GetUserName(),
            Payment = new PaymentMessage
            {
                CardExpireMonth = request.CardInformation.CardExpireMonth,
                CardExpireYear = request.CardInformation.CardExpireYear,
                CardNumber = request.CardInformation.CardNumber,
                CardOwner = request.CardInformation.CardOwner,
                CVV = request.CardInformation.CVV,
                TotalPrice =  transaction.DiscountedAmount,
                BankCode = request.BankCode
            },
            OrderItem = new OrderItemMessage
            {
                ProductId = request.ProductId,
                Count = request.Quantity,
                Name = product.Name,
            },
            TransactionId = transaction.Id
        };

        var orderEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueNames.OrderCreatedStockCheckQueue}"));

        await orderEndpoint.Send(orderCreatedEvent);

        if (request.CouponId.HasValue)
            await CacheService.RemoveAsync(cacheKey);

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

    public async Task<BaseResponse> Get(int id)
    {
        BaseResponse response = new();

        var transaction = await Db.GetDefaultRepo<Transaction>().GetAsync(x => x.Id == id);

        if (transaction == null)
            return response.Fail(Error.E_0000);

        response.Data = new TransactionResponseDto().Map(transaction);

        return response;
    }

    public async Task<BaseResponse> GetAll()
    {
        BaseResponse response = new();

        var transactions = await Db.GetDefaultRepo<Transaction>().GetAllAsync();
        var data = transactions.Select(x => new TransactionResponseDto().Map(x));

        response.Data = data;
        return response;
    }

    public async Task<BaseResponse> GetTransactionDetailById(int id)
    {
        BaseResponse response = new();
        var transaction = await Db.GetDefaultRepo<Transaction>().GetAsync(x => x.Id == id);
        if (transaction == null)
            return response.Fail(Error.E_0000);

        var productTask = Db.GetDefaultRepo<Product>().GetAsync(x => x.Id == transaction.ProductId);
        var couponTask = Db.GetDefaultRepo<Coupon>().GetAsync(x => x.Id == transaction.CouponId);
        var addressTask =  Db.GetDefaultRepo<Address>().GetAsync(x => x.Id ==transaction.AddressId);

        await Task.WhenAll(productTask, couponTask, addressTask);

        if(couponTask.Result == null ||  couponTask.Result == null || productTask.Result == null)
        {
            return response.Fail(Error.E_0000);
        }

        var data = new TransactionDetailResponseDto().Map(transaction);
        data.Coupon = new CouponResponseDto().Map(couponTask.Result);
        data.Address = new AddressResponseDto().Map(addressTask.Result);
        data.Product = new ProductResponseDto().Map(productTask.Result);

        response.Data = data;

        return response;
    }

    public async Task<BaseResponse> GetInvoiceEmailByTransactionId(int Id)
    {
        BaseResponse response = new();

        var transaction = await Db.GetDefaultRepo<Transaction>().GetAsync(x => x.Id == Id);

        if (transaction == null)
            return response.Fail(Error.E_0000);

        var emailEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueNames.SendInvoiceDetailEmailRequestQueue}"));

        await emailEndpoint.Send<EmailRequestEvent>(new SendInvoiceAsEmailRequestEvent { TransactionId = transaction.Id });

        return response;
    }
}
