using Clicco.Application.Helpers.Contracts;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;
using Clicco.Domain.Shared.Models.Email;
using Clicco.Domain.Shared.Models.Payment;
using MediatR;

namespace Clicco.Application.Features.Commands.Payment
{
    //Todo: Code Smell
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

            var bankRequest = new PaymentBankRequest
            {
                BankId = request.BankId,
                CardInformation = request.CardInformation,
                DealerName = product.Vendor.Name,
                ProductName = product.Name,
                TotalAmount = request.TotalAmount
            };

            var result = await paymentService.Pay(bankRequest);

            var transaction = new Transaction();

            transaction.Code = GetUniqueCode();
            transaction.AddressId = request.AddressId;
            transaction.Dealer = product.Vendor.Name;
            transaction.CreatedDate = DateTime.UtcNow;
            transaction.DeliveryDate = DateTime.UtcNow.AddDays(7);
            transaction.TotalAmount = product.UnitPrice * request.Quantity;
            transaction.DiscountedAmount = transaction.TotalAmount;
            transaction.UserId = claimHelper.GetUserId();

            try
            {
                if (result.IsSuccess)
                {
                    transaction.TransactionStatus = TransactionStatus.Success;
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

                        var activeCoupons = await cacheManager.GetListAsync(CacheKeys.ActiveCoupons);

                        if (!activeCoupons.Any(x => x == coupon.Id.ToString()))
                        {
                            await cacheManager.AddToListAsync(CacheKeys.ActiveCoupons, coupon.Id.ToString());
                        }
                    }

                    await transactionRepository.SaveChangesAsync();

                    await emailService.SendSuccessPaymentEmailAsync(new PaymentSuccessEmailRequest
                    {
                        Amount = transaction.TotalAmount >= transaction.DiscountedAmount
                            ? transaction.TotalAmount.ToString()
                            : transaction.DiscountedAmount.ToString(),
                        FullName = claimHelper.GetUserName(),
                        OrderNumber = transaction.Code,
                        PaymentMethod = "Credit / Bank Card",
                        ProductName = product.Name,
                        To = userEmail,
                    });

                    await invoiceService.CreateInvoice(userEmail, transaction, product, address);

                }
                else
                {
                    await FailedPayment(product, transaction, result.Message);
                }
            }
            catch
            {
                await FailedPayment(product, transaction, CustomErrors.UnexceptedError.ErrorMessage);
            }
            //Todo: Probably catch block is dead code due of exception middleware.
            await cacheManager.RemoveAsync(CacheKeys.GetListKey<TransactionViewModel>());
            return result.IsSuccess ? new SuccessPaymentResult() : new FailedPaymentResult(result.Message);
        }

        private async Task FailedPayment(Product product, Transaction transaction, string errorMessage)
        {
            transaction.TransactionStatus = TransactionStatus.Failed;
            await transactionRepository.AddAsync(transaction);
            await transactionRepository.SaveChangesAsync();
            var TransactionDetail = new TransactionDetail
            {
                TransactionId = transaction.Id,
                ProductId = product.Id,
            };
            await transactionDetailRepository.AddAsync(TransactionDetail);
            transaction.TransactionDetailId = TransactionDetail.Id;
            transaction.TransactionDetail = TransactionDetail;

            await emailService.SendFailedPaymentEmailAsync(new PaymentFailedEmailRequest
            {
                Amount = transaction.TotalAmount.ToString(),
                FullName = claimHelper.GetUserName(),
                OrderNumber = transaction.Code,
                PaymentMethod = "Credit / Bank Card",
                ProductName = product.Name,
                To = claimHelper.GetUserEmail(),
                Error = errorMessage
            });

            await transactionRepository.SaveChangesAsync();
            await transactionDetailRepository.SaveChangesAsync();
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
