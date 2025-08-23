namespace RentACar.DataContext.Entities
{
     public class Category
    {
        public int Id { get; set; }             
        public required string Name { get; set; }
        public ICollection<Car> Cars { get; set; } = [];
    }
}
