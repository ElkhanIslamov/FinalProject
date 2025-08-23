using System.ComponentModel.DataAnnotations;

namespace RentACar.Areas.Admin.Models
{
    public class HomeHeroSectionViewModel
    {
        public int Id { get; set; }
        public string? BackgroundImageUrl { get; set; }

        [Required]
        public string SmallTitle { get; set; } = null!;

        [Required]
        public string MainTitle { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        // Yeni upload olunacaq fayl
        public IFormFile? ImageFile { get; set; }
    }
}
