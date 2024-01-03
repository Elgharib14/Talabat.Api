using System.ComponentModel.DataAnnotations;

namespace Talabat.Api.DTOS
{
    public class RegisterDto
    {

        [Required]
        public string DispalyName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumper { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",
            ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }
    }
}
