using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Category : BaseEntity, ISoftDeletable
    {
        public string Name { get; set; }
        public string SlugUrl { get; set; }
        public bool IsDeleted { get; set; } = false;

        //Relationship
        public Category Parent { get; set; }
        public int? ParentId { get; set; }
        public Menu Menu { get; set; }
        public int? MenuId { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}