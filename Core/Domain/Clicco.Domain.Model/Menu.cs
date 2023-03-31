using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Menu : BaseEntity
    {
        public string SlugUrl { get; set; }

        //Relationship
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
