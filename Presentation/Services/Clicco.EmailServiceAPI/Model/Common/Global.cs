namespace Clicco.EmailServiceAPI.Model.Common
{
    public static class Global
    {
        public enum EmailType
        {
            NewUser = 1,
            ForgotPassword = 2,
            SuccessPayment = 3,
            FailedPayment = 4,
            Invoice = 5,
            ResetPassword = 6,
        }
    }
}
