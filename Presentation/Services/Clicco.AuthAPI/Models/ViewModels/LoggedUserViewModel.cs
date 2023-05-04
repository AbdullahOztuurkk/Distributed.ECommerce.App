namespace Clicco.AuthAPI.Models.Response
{
    public class LoggedUserViewModel
    {
        public string Email { get; set; }
        public string Token { get; set; }

        public LoggedUserViewModel(string token, string email)
        {
            Token = token;
            Email = email;
        }
    }
}
