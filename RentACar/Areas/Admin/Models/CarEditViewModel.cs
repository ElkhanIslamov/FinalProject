using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.DataContext.Entities;


namespace RentACar.Areas.Admin.Models
{
    public class CarEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Seats { get; set; }
        public int? Doors { get; set; }
        public int? Luggage { get; set; }
        public decimal PricePerDay { get; set; }
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }

        public IFormFile? CoverImageFile { get; set; }
        public List<IFormFile>? ImageFiles { get; set; }  // Yeni əlavə şəkillər
        public List<CarImage>? ExistingImages { get; set; } // Mövcud şəkilləri göstərmək üçün
        public List<SelectListItem> Categories { get; set; } = new();
        

    }

}

