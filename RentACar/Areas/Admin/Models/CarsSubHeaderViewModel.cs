using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Areas.Admin.Models
{
    public class CarsSubHeaderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;
        public string? BackgroundImage { get; set; }   // DB-də saxlanan yol
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }      // Yüklənəcək şəkil
    }
}
