namespace PaymentService.API.Domain.Adapters;

public static class PaymentBankResponseAdapter
{
    public static PaymentBankResponse Map(this PaymentBankResponse response, PaymentBankRequestDto request, string? ErrorMessage)
    {
        response.TransactionId = request.TransactionId;
        response.ErrorMessage = ErrorMessage;
        response.ProductName = request.ProductName;
        response.IsSuccess = false;
        response.FullName = request.FullName;
        response.To = request.To;
        response.TotalAmount = request.TotalAmount;
        return response;
    }

    public static PaymentBankResponse Map(this PaymentBankResponse response, PaymentBankRequestDto request)
    {
        return response.Map(request, null);
    }
}
