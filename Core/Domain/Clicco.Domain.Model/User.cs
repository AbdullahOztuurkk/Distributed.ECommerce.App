using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class User : BaseEntity, ISoftDeletable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; } // Male / Female
        public string PhoneNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool IsSA { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        //Relationship
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Review>  Reviews { get; set; }
    }
}
