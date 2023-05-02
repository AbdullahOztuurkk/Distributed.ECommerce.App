namespace Clicco.Domain.Shared.Models.Email
{
    public class ForgotPasswordEmailRequest : BaseEmailRequest
    {
        public string FullName { get; set; }
        public string NewPassword { get; set; }
    }
}
