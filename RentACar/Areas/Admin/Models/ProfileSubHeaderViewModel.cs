namespace RentACar.Areas.Admin.Models
{
    public class ProfileSubHeaderViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        // Fayl upload
        public IFormFile? BackgroundImageFile { get; set; }
        public string? BackgroundImage { get; set; }   // Mövcud şəkil (edit zamanı göstərmək üçün)
    }
}
