namespace Clicco.Domain.Model.Dtos.Menu
{
    public class MenuResponseDto
    {
        public int Id { get; set; }
        public string SlugUrl { get; set; }
        public string Name { get; set; }

        public MenuResponseDto Map(Model.Menu menu)
        {
            this.Id = menu.Id;
            this.SlugUrl = menu.SlugUrl;
            this.Name = menu.Name;
            return this;
        }
    }
}
