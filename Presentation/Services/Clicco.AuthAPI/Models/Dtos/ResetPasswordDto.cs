namespace Clicco.AuthServiceAPI.Models.Dtos
{
    public class ResetPasswordDto
    {
        public string ResetCode { get; set; }
        public string Password { get; set; }
    }
}
