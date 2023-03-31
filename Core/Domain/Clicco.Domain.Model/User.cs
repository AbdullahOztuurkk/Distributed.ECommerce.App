using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; } // Male / Female
        public string PhoneNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool IsSA { get; set; } = false;

        //Relationship
        
    }
}
