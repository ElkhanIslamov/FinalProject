using RentACar.DataContext.Entities;

namespace RentACar.Models
{
    public class CarsViewModel
    {
        public List<Car> Cars { get; set; } = [];
        public List<Category> Categories { get; set; } = [];
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
