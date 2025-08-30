namespace RentACar.DataContext.Entities.AboutPage
{
    // Intro
    public class AboutIntro
    {
        public int Id { get; set; }

        // Sol tərəf üçün başlıq
        public string Title { get; set; } = string.Empty;

        // Sağ tərəf üçün description
        public string Description { get; set; } = string.Empty;

        // Statistika elementləri
        public string CompletedOrdersLabel { get; set; } = "Completed Orders";
        public int CompletedOrdersValue { get; set; }

        public string HappyCustomersLabel { get; set; } = "Happy Customers";
        public int HappyCustomersValue { get; set; }

        public string VehiclesFleetLabel { get; set; } = "Vehicles Fleet";
        public int VehiclesFleetValue { get; set; }

        public string YearsExperienceLabel { get; set; } = "Years Experience";
        public int YearsExperienceValue { get; set; }
    }
}
