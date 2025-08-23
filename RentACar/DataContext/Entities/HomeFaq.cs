namespace RentACar.DataContext.Entities
{
    public class HomeFaq
    {
        public int Id { get; set; }
        public required string Question { get; set; }
        public required string Answer { get; set; }
        public string? FaqText { get; set; }
    }

}
