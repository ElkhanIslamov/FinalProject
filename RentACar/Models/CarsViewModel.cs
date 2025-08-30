using RentACar.DataContext.Entities;

namespace RentACar.Models
{
    public class CarsViewModel
    {
        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
