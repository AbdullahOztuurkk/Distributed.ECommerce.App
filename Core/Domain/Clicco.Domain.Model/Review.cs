using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Review : BaseEntity
    {
        public string Description { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedDate { get; set; }

        //Relationship
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
