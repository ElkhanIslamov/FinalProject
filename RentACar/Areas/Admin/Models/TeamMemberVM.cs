using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RentACar.Areas.Admin.Models
{
    public class TeamMemberVM
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Position { get; set; } = string.Empty;      

        [Url]
        [Display(Name = "Facebook URL")]
        public string FacebookUrl { get; set; } = string.Empty;

        [Url]
        [Display(Name = "Twitter URL")]
        public string TwitterUrl { get; set; } = string.Empty;

        [Url]
        [Display(Name = "Linkedin URL")]
        public string LinkedinUrl { get; set; } = string.Empty;

        [Url]
        [Display(Name = "Pinterest URL")]
        public string PinterestUrl { get; set; } = string.Empty;

        [Display(Name = "Current Image")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }  // şəkil yükləmək üçün
    }
}
