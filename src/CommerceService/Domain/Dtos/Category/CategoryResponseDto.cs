namespace CommerceService.Domain.Dtos.Category;

public class CategoryResponseDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? SlugUrl { get; set; }

    public CategoryResponseDto Map(Concrete.Category category)
    {
        this.Id = category.Id;
        this.Name = category.Name;
        this.SlugUrl = category.SlugUrl;
        return this;
    }
}
