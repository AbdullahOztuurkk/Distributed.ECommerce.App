using static Clicco.EmailServiceAPI.Model.Common.Global;

namespace Clicco.EmailServiceAPI.Model.Request
{
    public class RegistrationEmailRequest
    {
        public string To { get; set; }
        public string FullName { get; set; }
    }
}
