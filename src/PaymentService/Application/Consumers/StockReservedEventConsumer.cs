using PaymentService.API.Application.Services.Abstract;
using Shared.Domain.ValueObject;
using Shared.Events.Payment;

namespace PaymentService.API.Application.Consumers;

public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
{
    private readonly IBankServiceFactory _bankServiceFactory;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<StockReservedEventConsumer> _logger;
    public StockReservedEventConsumer(IBankServiceFactory bankServiceFactory,
                                      IPublishEndpoint publishEndpoint,
                                      ILogger<StockReservedEventConsumer> logger)
    {
        this._bankServiceFactory = bankServiceFactory;
        this._publishEndpoint = publishEndpoint;
        this._logger = logger;
    }

    public async Task Consume(ConsumeContext<StockReservedEvent> context)
    {
        var @event = context.Message;
        var bankService = _bankServiceFactory.CreateBankService(@event.Payment.BankCode);
        if (bankService is not null)
        {
            var paymentRequest = new PaymentBankRequestDto
            {
                BankCode = @event.Payment.BankCode,
                CardInformation = new CardInformation
                {
                    CardExpireMonth = @event.Payment.CardExpireMonth,
                    CardExpireYear = @event.Payment.CardExpireYear,
                    CardNumber = @event.Payment.CardNumber,
                    CardOwner = @event.Payment.CardOwner,
                    CVV = @event.Payment.CVV,
                },
                FullName = @event.FullName,
                ProductName = @event.OrderItem.Name,
                To = @event.BuyerEmail,
                TotalAmount = @event.Payment.TotalPrice,
                TransactionId = @event.TransactionId
            };

            var paymentResult = await bankService.Pay(paymentRequest);
            if (paymentResult.IsSuccess)
            {
                var paymentSuccessEvent = new PaymentSuccessEvent
                {
                    BuyerEmail = @event.BuyerEmail,
                    TransactionId = @event.TransactionId
                };

                await _publishEndpoint.Publish(paymentSuccessEvent);

                _logger.LogInformation($"{@event.Payment.TotalPrice} TL was withdrawn from credit card for transaction id = {@event.TransactionId}");
            }
            else
            {
                var paymentFailedEvent = new PaymentFailedEvent
                {
                    BuyerEmail = @event.BuyerEmail,
                    Message = paymentResult.Data.ErrorMessage,
                    OrderItem = @event.OrderItem,
                    TransactionId = @event.TransactionId,
                    FullName = @event.FullName,
                    ProductName = @event.OrderItem.Name
                };

                await _publishEndpoint.Publish(paymentFailedEvent);

                _logger.LogError($"{@event.Payment.TotalPrice} TL was not withdrawn from credit card for transaction id = {@event.TransactionId}");
            }
        }
    }
}
