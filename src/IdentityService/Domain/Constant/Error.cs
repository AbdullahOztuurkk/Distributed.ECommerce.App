using CoreLib.ResponseModel;

namespace IdentityService.API.Domain.Constant;

public record Error(string ErrorCode, string ErrorMessage)
{
    public static Error U0000 = new Error(nameof(U0000), "Record Not Found");
    public static Error U0001 = new Error(nameof(U0001), "Email or Password Is Wrong!");
    public static Error U0002 = new Error(nameof(U0002), "User Has Been Blocked!");
    public static Error U0003 = new Error(nameof(U0003), "Invalid Reset Code!");
}
public static class ErrorAdapter
{
    public static BaseResponse Fail(this BaseResponse response, Error error)
    {
        response.IsSuccess = false;
        response.ErrorCode = error.ErrorCode;
        response.Message = error.ErrorMessage;
        return response;
    }
}
