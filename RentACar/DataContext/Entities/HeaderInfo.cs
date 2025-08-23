using System.ComponentModel.DataAnnotations;

namespace RentACar.DataContext.Entities
{
    public class HeaderInfo
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Phone { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Email { get; set; } = null!;
        public DateTime WorkingTime { get; set; } = DateTime.UtcNow;
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? YoutubeUrl { get; set; }
        public string? PinterestUrl { get; set; }
        public string? InstagramUrl { get; set; }
    }
}
