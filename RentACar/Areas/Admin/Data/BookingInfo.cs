namespace RentACar.Areas.Admin.Data
{
    public class BookingInfo
    {
        public string CustomerName { get; set; }
        public string CarName { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
