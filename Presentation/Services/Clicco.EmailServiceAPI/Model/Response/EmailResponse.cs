namespace Clicco.EmailServiceAPI.Model.Response
{
    public class EmailResponse
    {
        public string Email { get; private set; }
        public string? EmailType { get; private set; }

        public EmailResponse(string email, string emailType)
        {
            Email = email;
            EmailType = emailType;
        }
        public EmailResponse(string email) : this(email, string.Empty) 
        {
        
        }

        public override string ToString()
        {
            return string.Format($"Taken request for send {EmailType} email to {Email} ");
        }
    }
}
