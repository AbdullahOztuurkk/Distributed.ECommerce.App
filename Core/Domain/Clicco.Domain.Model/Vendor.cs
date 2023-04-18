using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Vendor : BaseEntity, ISoftDeletable
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; } = false;

        //Relationship
        public ICollection<Product> Products { get; set; }
    }
}
