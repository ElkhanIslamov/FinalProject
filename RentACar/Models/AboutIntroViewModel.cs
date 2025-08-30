namespace RentACar.Models
{
    public class AboutIntroViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

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
