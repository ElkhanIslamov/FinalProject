using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Areas.Admin.Models
{
    public class CarCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Car Image")]
        public IFormFile? CoverImageFile { get; set; }

        public string? CoverImageUrl { get; set; }

        [Range(1, 10, ErrorMessage = "Seats must be between 1 and 10")]
        public int? Seats { get; set; }

        [Range(1, 10, ErrorMessage = "Doors must be between 1 and 10")]
        public int? Doors { get; set; }

        [Range(0, 20, ErrorMessage = "Luggage capacity must be between 0 and 20")]
        public int? Luggage { get; set; }

        [Range(0.01, 10000, ErrorMessage = "Price must be greater than zero")]
        [Display(Name = "Price Per Day ($)")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = new();

        // ✅ Yeni Əlavə edilənlər:
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Additional Images")]
        public List<IFormFile>? ImageFiles { get; set; }
    }
}
