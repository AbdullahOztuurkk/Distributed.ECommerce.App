namespace Clicco.Domain.Shared.Models.Email
{
    public class ResetPasswordEmailRequest : BaseEmailRequest
    {
        public string FullName { get; set; }
    }
}
