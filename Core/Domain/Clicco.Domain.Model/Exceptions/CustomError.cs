﻿namespace Clicco.Domain.Model.Exceptions
{
    public class CustomErrors
    {

        public static CustomError UnexceptedError = new("E_100", "Unexcepted error occurred!");

        #region NoContent Errors

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
        public static CustomError TransactionDetailNotFound = new("E_210", "Transaction detail not found!");

        #endregion

        #region Specific Errors

        public static CustomError MenuAlreadyExist = new("E_303", "Menu already exists!");
        public static CustomError CouponIsNowUsed = new("E_304", "The coupon is now used!");
        public static CustomError CouponInvalid = new("E_305", "The coupon is invalid!");
        public static CustomError CouponCannotUsed = new("E_306", "The coupon cannot used!");

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