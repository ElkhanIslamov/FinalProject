using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class LoginViewModel
    {
        [Required( ErrorMessage = "Ad daxil edin")]
        public  string UserName { get; set; } = null!;   
        [Required( ErrorMessage = "Parol daxil edin")]
        [DataType(DataType.Password)]
        public  string Password { get; set; }= null!;
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
