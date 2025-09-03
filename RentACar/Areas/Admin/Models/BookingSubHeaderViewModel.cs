using Microsoft.AspNetCore.Http;

namespace RentACar.Areas.Admin.Models
{
    public class BookingSubHeaderViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public IFormFile? BackgroundImageFile { get; set; }   // Admin upload üçün
        public string? BackgroundImagePath { get; set; }      // Saxlanmış şəkil yolu
    }
}
