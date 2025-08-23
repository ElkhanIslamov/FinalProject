namespace RentACar.Areas.Admin.Models;

    public class CarImageViewModel
    {
        public string? ImageUrl { get; set; }
    }    
        public class CarDetailsViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string? Description { get; set; }
            public decimal PricePerDay { get; set; }
            public int Seats { get; set; }
            public int Doors { get; set; }
            public int Luggage { get; set; }
            public string CategoryName { get; set; } = null!;
            public string ImageUrl { get; set; } = null!;
            public List<string> GalleryImages { get; set; } = new();
        }
    


    // Kiçik kart görünüşü üçün sadə model
    public class CarCardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal PricePerDay { get; set; }
    }

