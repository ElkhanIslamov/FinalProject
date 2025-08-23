namespace RentACar.Models
{
    public class CheckoutViewModel
    {
        public int BookingId { get; set; }
        public string CarType { get; set; } = null!;
        public decimal TotalPrice { get; set; }
    }
}
