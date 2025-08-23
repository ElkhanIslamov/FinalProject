using System.ComponentModel.DataAnnotations;

namespace RentACar.Areas.Admin.Models
{
    public class TimelineStepVM
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public int Order { get; set; }

        // yüklənəcək şəkil
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }

        // DB-də saxlanmış şəkil
        public string? ImageUrl { get; set; }
    }
}
