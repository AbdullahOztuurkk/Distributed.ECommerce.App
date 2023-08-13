namespace Clicco.EmailServiceAPI.Model.Common
{
    public static class Global
    {
        public enum EmailType
        {
            NewUser = 10,
            ForgotPassword = 20,
            SuccessPayment = 30,
            FailedPayment = 40,
            Invoice = 50,
            ResetPassword = 60,
        }
    }
}
