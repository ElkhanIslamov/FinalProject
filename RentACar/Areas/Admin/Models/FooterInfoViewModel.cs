using System.Collections.Generic;

namespace RentACar.Areas.Admin.Models
{
    public class FooterInfoViewModel
    {
        public int Id { get; set; }
        public required string AboutText { get; set; } = "";
        public required string Address { get; set; } = "";
        public required string Phone { get; set; } = "";
        public required string Email { get; set; } = "";
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? PinterestUrl { get; set; }
        public string? RssUrl { get; set; }
        public List<FooterLinkViewModel>? FooterLinks { get; set; } = new();
    }
}
