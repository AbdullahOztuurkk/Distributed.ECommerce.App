using CoreLib.ResponseModel;

namespace CommerceService.Domain.Constant;

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

public record Error(string ErrorCode, string ErrorMessage)
{
    public static Error E_0000 = new("E0000","Record not found!");
    public static Error E_0001 = new("E0001","Unexcepted error occurred!");
    public static Error E_0002 = new("E0002","Record already exists!");
    public static Error E_0003 = new("E0003","The coupon is now used!");
    public static Error E_0004 = new("E0004","The coupon is invalid!");
    public static Error E_0005 = new("E0005","The coupon cannot used!");
    public static Error E_0006 = new("E0006","You are unathorized for this operation!");
    public static Error E_0007 = new("E0007","Username or password is wrong");
    public static Error E_0008 = new("E0008",  "Reset code is invalid!");
    public static Error E_0009 = new("E0009",  "Stock Not Reserved!");

}