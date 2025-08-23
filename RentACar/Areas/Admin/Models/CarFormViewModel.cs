using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.DataContext.Entities;

namespace RentACar.Areas.Admin.Models
{
    public class CarFormViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }
        public int? Seats { get; set; }
        public int? Doors { get; set; }
        public int? Luggage { get; set; }
        public decimal PricePerDay { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem>? CategoryList { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
