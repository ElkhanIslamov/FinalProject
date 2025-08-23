namespace RentACar.Models
{
    public class CarFilterRequest
    {
        public List<int> BodyTypes { get; set; } = [];
        public required string SearchKeyword { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
