namespace Clicco.Domain.Shared.Models.Email
{
    public class ResetPasswordEmailRequestDto : BaseEmailRequest
    {
        public string FullName { get; set; }
    }
}
