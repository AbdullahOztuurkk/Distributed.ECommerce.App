namespace Clicco.Domain.Model.Exceptions
{
    public static class CustomErrors
    {

        public static readonly CustomError UnexceptedError = new("E_100", "Unexcepted error occurred!");

        #region NoContent Errors

        public static readonly CustomError UserNotFound = new("E_200", "User not found!");
        public static readonly CustomError MenuNotFound = new("E_201", "Menu not found!");
        public static readonly CustomError CategoryNotFound = new("E_202", "Category not found!");
        public static readonly CustomError ProductNotFound = new("E_203", "Product not found!");
        public static readonly CustomError AddressNotFound = new("E_204", "Address not found!");
        public static readonly CustomError ReviewNotFound = new("E_205", "Review not found!");
        public static readonly CustomError TransactionNotFound = new("E_206", "Transaction not found!");
        public static readonly CustomError CouponNotFound = new("E_207", "Coupon not found!");
        public static readonly CustomError ParentCategoryNotFound = new("E_208", "Main category not found!");
        public static readonly CustomError VendorNotFound = new("E_209", "Vendor not found!");
        public static readonly CustomError TransactionDetailNotFound = new("E_210", "Transaction detail not found!");

        #endregion

        #region Specific Errors

        public static readonly CustomError MenuAlreadyExist = new("E_303", "Menu already exists!");
        public static readonly CustomError CouponIsNowUsed = new("E_304", "The coupon is now used!");
        public static readonly CustomError CouponInvalid = new("E_305", "The coupon is invalid!");
        public static readonly CustomError CouponCannotUsed = new("E_306", "The coupon cannot used!");

        #endregion
    }

    public class CustomError
    {
        public CustomError(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public string ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }
    }
}