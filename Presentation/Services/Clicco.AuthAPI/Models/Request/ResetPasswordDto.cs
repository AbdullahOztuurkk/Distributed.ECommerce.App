namespace Clicco.AuthServiceAPI.Models.Request
{
    public class ResetPasswordDto
    {
        public string ResetCode { get; set; }
        public string Password { get; set; }
    }
}
