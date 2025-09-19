using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RentACar.DataContext.Entities;

namespace RentACar.Models
{
    public class BookingViewModel : IValidatableObject
    {
        [Required]
        public int? CarId { get; set; }
        public string CarType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ad əlavə edin!")]
        public string CustomerName { get; set; } = null!;

        [Required(ErrorMessage = "Email əlavə edin!")]
        [EmailAddress(ErrorMessage = "Email ünvanı düzgün yazın!")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Mobil nömrə əlavə edin!")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Götürmə ünvanı qeyd edin!")]
        public string PickupLocation { get; set; } = null!;

        [Required(ErrorMessage = "Geri qaytarma ünvanı qeyd edin!")]
        public string DropoffLocation { get; set; } = null!;

        [Required(ErrorMessage = "Götürmə gününü qeyd edin!")]
        public DateTime PickupDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Götürmə saatını qeyd edin!")]
        public string PickupTime { get; set; } = "09:00";

        [Required(ErrorMessage = "Geri qaytarma gününü qeyd edin!")]
        public DateTime ReturnDate { get; set; } = DateTime.UtcNow.AddDays(1);

        [Required(ErrorMessage = "Geri qaytarma saatını qeyd edin!")]
        public string ReturnTime { get; set; } = "09:00";

        public string? Message { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!TimeSpan.TryParse(PickupTime, out var pickupTimeSpan) ||
                !TimeSpan.TryParse(ReturnTime, out var returnTimeSpan))
            {
                yield return new ValidationResult("Zaman intervalını düzgün seçin!");
                yield break;
            }

            var pickupDateTime = PickupDate.Date.Add(pickupTimeSpan);
            var returnDateTime = ReturnDate.Date.Add(returnTimeSpan);

            if (returnDateTime <= pickupDateTime)
            {
                yield return new ValidationResult("Qaytarma tarixi və vaxtı götürmə tarixi və vaxtından sonra olmalıdır.",
                    new[] { nameof(ReturnDate), nameof(ReturnTime) });
            }

            if ((returnDateTime - pickupDateTime).TotalHours < 24)
            {
                yield return new ValidationResult("Rezervasiya müddəti ən azı 1 tam gün olmalıdır.",
                    new[] { nameof(PickupDate), nameof(ReturnDate) });
            }
        }
        public BookingSubHeader SubHeader { get; set; } = new BookingSubHeader();
    }
}
