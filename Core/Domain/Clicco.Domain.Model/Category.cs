using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string SlugUrl { get; set; }

        //Relationship
        public Category Parent { get; set; }
        public int? ParentId { get; set; }
        public Menu Menu { get; set; }
        public int MenuId { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}