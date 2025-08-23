using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace RentACar.Models
{
    public class BookingViewModel : IValidatableObject
    {
        [Required]
        public int? CarId { get; set; }
        public required string CarType { get; set; }

        [Required(ErrorMessage = "Your name is required.")]
        public string CustomerName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Pickup location is required.")]
        public string PickupLocation { get; set; } = null!;

        [Required(ErrorMessage = "Dropoff location is required.")]
        public string DropoffLocation { get; set; } = null!;

        [Required(ErrorMessage = "Pickup date is required.")]
        public DateTime PickupDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Pickup time is required.")]
        public string PickupTime { get; set; } = "09:00";

        [Required(ErrorMessage = "Return date is required.")]
        public DateTime ReturnDate { get; set; } = DateTime.UtcNow.AddDays(1);

        [Required(ErrorMessage = "Return time is required.")]
        public string ReturnTime { get; set; } = "09:00";

        public string? Message { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!TimeSpan.TryParse(PickupTime, out var pickupTimeSpan) ||
                !TimeSpan.TryParse(ReturnTime, out var returnTimeSpan))
            {
                yield return new ValidationResult("Invalid time format.");
                yield break;
            }

            var pickupDateTime = PickupDate.Date.Add(pickupTimeSpan);
            var returnDateTime = ReturnDate.Date.Add(returnTimeSpan);

            if (returnDateTime <= pickupDateTime)
            {
                yield return new ValidationResult("Return date and time must be after pickup date and time.",
                    new[] { nameof(ReturnDate), nameof(ReturnTime) });
            }

            if ((returnDateTime - pickupDateTime).TotalHours < 24)
            {
                yield return new ValidationResult("Booking duration must be at least 1 full day.",
                    new[] { nameof(PickupDate), nameof(ReturnDate) });
            }
        }
    }
}
