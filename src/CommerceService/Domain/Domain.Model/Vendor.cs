using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Vendor : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        public string SlugUrl { get; set; }
        //Relationship
        public ICollection<Product> Products { get; set; }
    }
}
