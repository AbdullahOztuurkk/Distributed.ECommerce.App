namespace CommerceService.Domain.Dtos.Category;

public class CreateCategoryDto
{
    public string? Name { get; set; }
    public int? ParentId { get; set; }
    public int? MenuId { get; set; }
}
