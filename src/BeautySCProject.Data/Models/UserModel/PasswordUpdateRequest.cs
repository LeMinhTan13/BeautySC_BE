

using System.ComponentModel.DataAnnotations;

namespace BeautySCProject.Data.Models.CustomerModel
{
	public class PasswordUpdateRequest
    {
		[Required]
		[MinLength(6)]
		public string OldPassword { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; } = string.Empty;
        [Compare("NewPassword", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

	}
}
