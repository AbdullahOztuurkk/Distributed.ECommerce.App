namespace Clicco.Domain.Shared.Models.Email
{
    public class ForgotPasswordEmailRequestDto : BaseEmailRequest
    {
        public string FullName { get; set; }
        public string ResetCode { get; set; }
    }
}
