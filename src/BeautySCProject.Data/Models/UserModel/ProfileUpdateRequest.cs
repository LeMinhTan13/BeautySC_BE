using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BeautySCProject.Data.Models.Configuration;

namespace BeautySCProject.Data.Models.CustomerModel
{
    public class ProfileUpdateRequest
    {
        [MaxLength(255)]
        public string? FullName { get; set; }
        [RegularExpression(@"^\+(?:[0-9]?){6,14}[0-9]$", ErrorMessage = "Must be a E.164 compliant phone number (ex: +84332338587)")]
        public string? PhoneNumber { get; set; }

        [BirthdayValidation(18, 100, ErrorMessage = "Date of birth out of range")]
        public DateOnly? Birthday { get; set; }      
    } 
}
