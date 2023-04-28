using Clicco.Application.ExternalModels.Email;
using Clicco.Application.ExternalModels.Payment.Request;
using Clicco.Application.Features.Commands;
using Clicco.Application.Helpers.Contracts;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clicco.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        private readonly ICouponService couponService;
        private readonly IProductService productService;
        private readonly ITransactionService transactionService;
        private readonly ITransactionDetailService transactionDetailService;
        private readonly IEmailService emailService;
        private readonly IClaimHelper claimHelper;
        private readonly IMediator mediator;
        public PaymentController(
            IPaymentService paymentService,
            ICouponService couponService,
            IMediator mediator,
            ITransactionService transactionService,
            ITransactionDetailService transactionDetailService,
            IProductService productService,
            IEmailService emailService,
            IClaimHelper claimHelper)
        {
            this.paymentService = paymentService;
            this.couponService = couponService;
            this.mediator = mediator;
            this.transactionService = transactionService;
            this.transactionDetailService = transactionDetailService;
            this.productService = productService;
            this.emailService = emailService;
            this.claimHelper = claimHelper;
        }

        //TODO: Code Smell
        [HttpPost]
        public async Task<IActionResult> PayAsync(PaymentRequest paymentRequest)
        {
            if (paymentRequest.CouponId.HasValue)
                await couponService.CheckSelfId(paymentRequest.CouponId.Value);

            await productService.CheckSelfId(paymentRequest.ProductId);
            await transactionService.CheckAddressIdAsync(paymentRequest.AddressId);

            var product = await productService.GetAsync(paymentRequest.ProductId);

            var bankRequest = new PaymentBankRequest
            {
                BankId = paymentRequest.BankId,
                CardInformation = paymentRequest.CardInformation,
                DealerName = product.Vendor.Name,
                ProductName = product.Name,
                TotalAmount = paymentRequest.TotalAmount
            };

            var result = await paymentService.Pay(bankRequest);

            var transaction = new Transaction();

            transaction.Code = GetUniqueCode();
            transaction.AddressId = paymentRequest.AddressId;
            transaction.Dealer = product.Vendor.Name;
            transaction.CreatedDate = DateTime.UtcNow;
            transaction.DeliveryDate = DateTime.UtcNow.AddDays(7);
            transaction.TotalAmount = product.UnitPrice * product.Quantity;
            transaction.DiscountedAmount = transaction.TotalAmount;
            transaction.UserId = claimHelper.GetUserId();

            try
            {
                if (result.IsSuccess)
                {
                    transaction.TransactionStatus = TransactionStatus.Success;
                    await transactionService.AddAsync(transaction);
                    var TransactionDetail = new TransactionDetail
                    {
                        TransactionId = transaction.Id,
                        ProductId = product.Id,
                    };
                    await transactionDetailService.AddAsync(TransactionDetail);
                    transaction.TransactionDetailId = TransactionDetail.Id;
                    transaction.TransactionDetail = TransactionDetail;

                    if (paymentRequest.CouponId.HasValue)
                        await mediator.Send(new ApplyCouponToTransactionCommand { CouponId = paymentRequest.CouponId.Value, Transaction = transaction });

                    await emailService.SendSuccessPaymentEmailAsync(new PaymentSuccessEmailRequest
                    {
                        Amount = transaction.DiscountedAmount == transaction.TotalAmount ? transaction.TotalAmount.ToString() : transaction.DiscountedAmount.ToString(),
                        FullName = claimHelper.GetUserName(),
                        OrderNumber = transaction.Code,
                        PaymentMethod = "Credit / Bank Card",
                        ProductName = product.Name,
                        To = claimHelper.GetUserEmail(),
                    });
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

            return result.IsSuccess ? Ok() : BadRequest();
        }

        private async Task FailedPayment(Product product, Transaction transaction, string errorMessage)
        {
            transaction.TransactionStatus = TransactionStatus.Failed;
            await transactionService.AddAsync(transaction);
            var TransactionDetail = new TransactionDetail
            {
                TransactionId = transaction.Id,
                ProductId = product.Id,
            };
            await transactionDetailService.AddAsync(TransactionDetail);
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
