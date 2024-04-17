namespace CommerceService.Domain.Dtos.Review;

public class UpdateReviewDto
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public byte Rating { get; set; }
    public int ProductId { get; set; }
}
