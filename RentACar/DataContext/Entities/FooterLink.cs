using System.ComponentModel.DataAnnotations;

namespace RentACar.DataContext.Entities
{
    public class FooterLink
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!; // məsələn: "About"

        [Required]
        [MaxLength(200)]
        public string Url { get; set; } = null!;   // məsələn: "/about"
    }
}
