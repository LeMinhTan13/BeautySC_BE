using BeautySCProject.Data.Models.Configuration;
using System.ComponentModel.DataAnnotations;

namespace BeautySCProject.Data.Models.AuthenticationModel
{
    public class SignupRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required"), DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must contain at least 6 characters")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255)]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Birthday is required")]
        [BirthdayValidation(18, 100, ErrorMessage = "Date of birth out of range")]
        public DateOnly Birthday { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;               
    }
}
