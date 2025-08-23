namespace RentACar.DataContext.Entities
{
    public class HomeFeature
    {
        public int Id { get; set; }
        public string Icon { get; set; } = null!;          // FontAwesome icon
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }              // Yeni əlavə
        public string? AnimationDelay { get; set; }        // `.5s`, `.75s` və s.
    }

}
