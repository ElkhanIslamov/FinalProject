using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class ChangePasswordViewModel
    {
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Parollar uyğun gəlmir.")]
        public required string ConfirmPassword { get; set; }
    }
}
