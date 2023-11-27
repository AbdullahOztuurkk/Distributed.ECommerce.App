using Clicco.Domain.Core.ResponseModel;

namespace Clicco.Domain.Model.Dtos.Review
{
    public class ReviewResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string ProductUrl { get; set; }

        public ReviewResponseDto Map(Model.Review review)
        {
            this.Id = review.Id;
            this.Description = review.Description;
            this.Rating = review.Rating;
            this.UpdatedDate = review.UpdatedDate.Value;
            this.ProductUrl= review.Product?.SlugUrl ?? string.Empty;
            return this;
        }
    }
}
