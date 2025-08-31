namespace RentACar.DataContext.Entities.AboutPage
{
    public class CallToAction
    {
        public int Id { get; set; }
        public string Heading { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ButtonText { get; set; } = string.Empty;
        public string ButtonUrl { get; set; } = string.Empty;
        public string? Icon { get; set; }    
        public string? BackgroundImage { get; set; }
    }

}
