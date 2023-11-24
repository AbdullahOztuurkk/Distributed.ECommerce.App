namespace Clicco.AuthAPI.Models.Response
{
    public class LoginResponseDto
    {
        public string Email { get; set; }
        public string Token { get; set; }

        public LoginResponseDto(string token, string email)
        {
            Token = token;
            Email = email;
        }
    }
}
