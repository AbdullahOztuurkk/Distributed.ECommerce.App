using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string SlugUrl { get; set; }

        //Relationship
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
