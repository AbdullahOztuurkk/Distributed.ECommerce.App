namespace Clicco.AuthServiceAPI.Models.Dtos
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; } // Male / Female
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
