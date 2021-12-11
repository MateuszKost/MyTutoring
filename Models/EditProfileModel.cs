using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class EditProfileModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
                
        public string Email { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Numer telefonu musi miec 9 cyfr")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword")]
        public string? NewPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "RepeatPassword")]
        public string? RepeatPassword { get; set; }
    }
}
