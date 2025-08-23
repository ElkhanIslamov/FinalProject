using Microsoft.AspNetCore.Http;

namespace RentACar.Areas.Admin.Models
{
    public class HomeFeatureViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Icon { get; set; } = "fa-trophy"; // default icon
        public IFormFile? ImageFile { get; set; }
        public string? ExistingImageUrl { get; set; }
    }
}
