using System.ComponentModel.DataAnnotations;

namespace RentACar.DataContext.Entities
{
    public class TimelineStep
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public int Order { get; set; }

        // şəkil yolu DB-də saxlanacaq
        [StringLength(255)]
        public string? ImageUrl { get; set; }
    }
}
