using Clicco.AuthAPI.Models.Common;

namespace Clicco.AuthAPI.Models
{
    public class User : BaseEntity, ISoftDeletable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; } // Male / Female
        public string PhoneNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsSA { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
