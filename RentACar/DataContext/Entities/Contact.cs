namespace RentACar.DataContext.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
