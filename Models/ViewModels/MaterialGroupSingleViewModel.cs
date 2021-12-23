using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class MaterialGroupSingleViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Nazwa musi mieć conajmniej 5 liter i maksimum 20.")]
        public string Name { get; set; }
        public int MaterialGroupId { get; set; }
    }
}
