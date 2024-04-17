namespace Clicco.Domain.Model.Dtos.Review
{
    public class CreateReviewDto
    {
        public string Description { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
    }
}
