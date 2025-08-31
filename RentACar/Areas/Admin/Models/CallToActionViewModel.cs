namespace RentACar.Areas.Admin.Models
{
    public class CallToActionViewModel
    {
        public int Id { get; set; }

        // Əsas sahələr
        public string Heading { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ButtonText { get; set; } = string.Empty;
        public string ButtonUrl { get; set; } = string.Empty;

        // Vizual sahələr
        public string? Icon { get; set; }                // Məsələn: "fa fa-phone"

        // Fayl upload üçün
        public IFormFile? BackgroundImageFile { get; set; }
        public string? BackgroundImage { get; set; }     // Mövcud şəkil yolu (edit zamanı göstərmək üçün)
    }
}
