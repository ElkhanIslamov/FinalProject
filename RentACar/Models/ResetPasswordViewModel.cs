using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        public required string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; }
        public required string ResetToken { get; set; }
        public required string Email { get; set; }
    }
}
