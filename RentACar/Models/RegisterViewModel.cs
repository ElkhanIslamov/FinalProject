using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class RegisterViewModel
    {
        public required string Username { get; set; }
        public required string FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Parol ən azı 4 simvol uzunluğunda olmalıdır.")]
        public required string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password uyğun gəlmir")]
        public required string ConfirmPassword { get; set; }
    }
}
