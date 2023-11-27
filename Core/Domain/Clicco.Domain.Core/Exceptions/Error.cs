namespace Clicco.Domain.Core.Exceptions
{
    public static class Errors
    {
        public record Error(string ErrorCode, string ErrorMessage);

        public static readonly Error UnexceptedError = new("E_100", "Unexcepted error occurred!");

        #region NoContent Errors

        public static readonly Error UserNotFound = new("E_200", "User not found!");
        public static readonly Error MenuNotFound = new("E_201", "Menu not found!");
        public static readonly Error CategoryNotFound = new("E_202", "Category not found!");
        public static readonly Error ProductNotFound = new("E_203", "Product not found!");
        public static readonly Error AddressNotFound = new("E_204", "Address not found!");
        public static readonly Error ReviewNotFound = new("E_205", "Review not found!");
        public static readonly Error TransactionNotFound = new("E_206", "Transaction not found!");
        public static readonly Error CouponNotFound = new("E_207", "Coupon not found!");
        public static readonly Error ParentCategoryNotFound = new("E_208", "Main category not found!");
        public static readonly Error VendorNotFound = new("E_209", "Vendor not found!");
        public static readonly Error TransactionDetailNotFound = new("E_210", "Transaction detail not found!");

        #endregion

        #region Specific Errors

        public static readonly Error MenuAlreadyExist = new("E_303", "Menu already exists!");
        public static readonly Error CouponIsNowUsed = new("E_304", "The coupon is now used!");
        public static readonly Error CouponInvalid = new("E_305", "The coupon is invalid!");
        public static readonly Error CouponCannotUsed = new("E_306", "The coupon cannot used!");
        public static readonly Error UserAlreadyExist = new("E_307", "User already exists!");
        public static readonly Error UnauthorizedOperation = new("E_308","You are unathorized for this operation!");
        public static readonly Error IncorrectLogin = new("E_309","Username or password is wrong!");
        public static readonly Error InvalidResetCode = new("E_310","Reset code is invalid!");
        public static readonly Error CategoryAlreadyExist = new("E_311","Category already exists!");
        public static readonly Error VendorAlreadyExist = new("E_312","Vendor already exists!");

        #endregion
    }
}