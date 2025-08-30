namespace RentACar.DataContext.Entities.AboutPage
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;       
        public string ImageUrl { get; set; } = string.Empty;
        public string FacebookUrl { get; set; } = string.Empty;
        public string TwitterUrl { get; set; } = string.Empty;
        public string LinkedinUrl { get; set; } = string.Empty;
        public string PinterestUrl { get; set; } = string.Empty;
            
    }
}
