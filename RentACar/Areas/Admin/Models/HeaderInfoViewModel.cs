using Microsoft.AspNetCore.Http;

namespace RentACar.Areas.Admin.Models
{
    public class HeaderInfoViewModel
    {
        public int Id { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public DateTime WorkingTime { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? YoutubeUrl { get; set; }
        public string? PinterestUrl { get; set; }
        public string? InstagramUrl { get; set; }
    }
}
