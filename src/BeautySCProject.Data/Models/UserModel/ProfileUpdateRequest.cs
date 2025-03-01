using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BeautySCProject.Data.Models.Configuration;

namespace BeautySCProject.Data.Models.CustomerModel
{
    public class ProfileUpdateRequest
    {
        [MaxLength(255)]
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly? Birthday { get; set; } 
        public string? Image { get; set; }
    } 
}
