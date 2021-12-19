#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}