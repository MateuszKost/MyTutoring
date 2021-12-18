using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class MaterialGroupViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Nazwa musi mieć conajmniej 5 liter i maksimum 20.")]
        public string Name { get; set; }
    }
}
