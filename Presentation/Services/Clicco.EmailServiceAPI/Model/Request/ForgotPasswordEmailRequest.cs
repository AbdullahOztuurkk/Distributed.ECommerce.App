using static Clicco.EmailServiceAPI.Model.Common.Global;

namespace Clicco.EmailServiceAPI.Model.Request
{
    public class ForgotPasswordEmailRequest
    {
        public string To { get; set; }
        public string FullName { get; set; }
        public string NewPassword { get; set; }
    }
}
