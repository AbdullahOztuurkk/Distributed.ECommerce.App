namespace Clicco.AuthAPI.Models.Response
{
    public class LoginResponseDto
    {
        public string Email { get; set; }
        public Token Token { get; set; }

        public LoginResponseDto(Token token, string email)
        {
            Token = token;
            Email = email;
        }
    }
}
