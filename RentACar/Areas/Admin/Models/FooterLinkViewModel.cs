using System.ComponentModel.DataAnnotations;

namespace RentACar.Areas.Admin.Models
{
    public class FooterLinkViewModel
    {
        public int Id { get; set; }
        public required string Title { get; set; } = "";
        public required string Url { get; set; } = "";
    }
}
