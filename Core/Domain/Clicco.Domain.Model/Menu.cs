using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Menu : BaseEntity, ISoftDeletable
    {
        public string SlugUrl { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        //Relationship
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
