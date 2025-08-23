using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentACar.DataContext.Entities
{
    public class FooterInfo
    {
        public int Id { get; set; }

        [Required]
        public string AboutText { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        public string? BrochureUrl { get; set; }

        public string? QuickLinksJson { get; set; } // QuickLinks saxlanacaq JSON kimi

        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? PinterestUrl { get; set; }
        public string? RssUrl { get; set; }

        public string? CopyrightText { get; set; }
    }
}
