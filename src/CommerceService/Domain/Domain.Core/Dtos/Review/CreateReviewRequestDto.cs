namespace CommerceService.Domain.Dtos.Review;

public class CreateReviewRequestDto
{
    public string? Description { get; set; }
    public byte Rating { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int ProductId { get; set; }
}
