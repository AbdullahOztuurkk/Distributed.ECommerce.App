namespace Clicco.AuthAPI.Models.Response
{
    public class ResponseModel
    {
        public string Email { get; set; }
        public string Token { get; set; }

        public ResponseModel(string token, string email)
        {
            Token = token;
            Email = email;
        }
    }
}
