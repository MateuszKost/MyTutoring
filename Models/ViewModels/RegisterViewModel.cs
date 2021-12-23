using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Imię jest wymagane")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres email")]
        public string Email { get; set; }

        public string AccountType { get; set; }

        [StringLength(9, MinimumLength = 9,  ErrorMessage = "Numer telefonu musi miec 9 cyfr")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

    }
}
