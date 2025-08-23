
    namespace RentACar.DataContext.Entities
    {
        public class CarImage
        {
            public int Id { get; set; }
            public required string ImageUrl { get; set; }
            public int CarId { get; set; }
            public Car Car { get; set; } = null!;
        }
    }

