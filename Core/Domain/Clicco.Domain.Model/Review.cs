using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Review : BaseEntity, ISoftDeletable
    {
        public string Description { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;


        //Relationship
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
