namespace CommerceService.Domain.Dtos.Menu;

public class MenuResponseDto
{
    public long Id { get; set; }
    public string? SlugUrl { get; set; }
    public string? Name { get; set; }

    public MenuResponseDto Map(Concrete.Menu menu)
    {
        this.Id = menu.Id;
        this.SlugUrl = menu.SlugUrl;
        this.Name = menu.Name;
        return this;
    }
}
