namespace CommerceService.Domain.Dtos.Review;

public class ReviewResponseDto
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public byte Rating { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string? ProductUrl { get; set; }

    public ReviewResponseDto Map(Concrete.Review review)
    {
        this.Id = review.Id;
        this.Description = review.Description;
        this.Rating = review.Rating;
        this.UpdatedDate = review.UpdateDate.Value;
        this.ProductUrl = review.Product?.SlugUrl ?? string.Empty;
        return this;
    }
}
