namespace Clicco.AuthAPI.Models.Email
{
    public class ForgotPasswordEmailRequest
    {
        public string To { get; set; }
        public string FullName { get; set; }
        public string NewPassword { get; set; }
    }
}
