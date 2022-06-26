namespace OngProject.Core.Models.DTOs.Slides
{
    public class SlideResponse
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string Text { set; get; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
        public int? OrganizationId { get; set; }
    }
}