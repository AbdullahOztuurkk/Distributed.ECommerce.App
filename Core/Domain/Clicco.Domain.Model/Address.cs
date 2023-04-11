using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Address : BaseEntity, ISoftDeletable
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public bool IsDeleted { get; set; } = false;

        //Relationship
        public ICollection<Transaction> Transactions { get; set; }
        public int UserId { get; set; }
    }
}
