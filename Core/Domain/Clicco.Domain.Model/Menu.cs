using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Menu : BaseEntity
    {
        public string SlugUrl { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        
        //Relationship
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
