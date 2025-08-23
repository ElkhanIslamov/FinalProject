namespace RentACar.DataContext.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public string CarType { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public DateTime PickupDate { get; set; }
        public string PickupTime { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ReturnTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? CarId { get; set; }
        public Car? Car { get; set; }
        public string Status { get; set; } = "Pending"; // default olaraq 'Pending'

    }


}
