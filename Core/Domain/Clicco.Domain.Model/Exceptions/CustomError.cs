using Clicco.Domain.Model;

namespace Clicco.Domain.Model.Exceptions
{
    public class CustomErrors
    {
        public static Dictionary<Type, CustomError> NotFoundArr = new()
        {
            { typeof(Menu) , MenuNotFound },
            { typeof(Category) , CategoryNotFound },
            { typeof(Product) , ProductNotFound },
            { typeof(Address) , AddressNotFound },
            { typeof(Transaction) , TransactionNotFound },
            { typeof(Coupon) , CouponNotFound },
            { typeof(Review) , MenuNotFound },
            { typeof(Vendor), VendorNotFound },
        };

        public static CustomError UnexceptedError = new("E_100", "Unexcepted error occurred!");

        public static CustomError UserNotFound = new("E_200", "User not found!");
        public static CustomError MenuNotFound = new("E_201", "Menu not found!");
        public static CustomError CategoryNotFound = new("E_202", "Category not found!");
        public static CustomError ProductNotFound = new("E_203", "Product not found!");
        public static CustomError AddressNotFound = new("E_204", "Address not found!");
        public static CustomError ReviewNotFound = new("E_205", "Review not found!");
        public static CustomError TransactionNotFound = new("E_206", "Transaction not found!");
        public static CustomError CouponNotFound = new("E_207", "Coupon not found!");
        public static CustomError ParentCategoryNotFound = new("E_208", "Main category not found!");
        public static CustomError VendorNotFound = new("E_209", "Vendor not found!");

        public static CustomError MenuAlreadyExist = new("E_303", "Menu already exists!");
        public static CustomError CouponIsNowUsed = new("E_304", "The coupon is now used!");
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