namespace Clicco.Domain.Model.Dtos
{
    public class ReviewResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProductUrl { get; set; }
    }
}
