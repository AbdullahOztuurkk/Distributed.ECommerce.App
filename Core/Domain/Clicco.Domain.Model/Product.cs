using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Product : BaseEntity, ISoftDeletable
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string SlugUrl { get; set; }
        public bool IsDeleted { get; set; } = false;


        //Relationship
        public Vendor Vendor { get; set; }
        public int VendorId { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<TransactionDetail> TransactionDetails { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
