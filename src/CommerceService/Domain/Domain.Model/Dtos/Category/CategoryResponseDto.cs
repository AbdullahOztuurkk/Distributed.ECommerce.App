namespace Clicco.Domain.Model.Dtos.Category
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SlugUrl { get; set; }

        public CategoryResponseDto Map(Model.Category category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
            this.SlugUrl = category.SlugUrl;
            return this;
        }
    }
}
