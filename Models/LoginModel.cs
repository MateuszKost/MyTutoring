#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class LoginModel
    {
        [Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}