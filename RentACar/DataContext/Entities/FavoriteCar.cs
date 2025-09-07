namespace RentACar.DataContext.Entities
{
    public class FavoriteCar
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
