using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class MaterialViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Opis musi mieć minimum 5, a maksimum 100 znaków")]
        public string Description { get; set; }
        [Required]
        public int MaterialGroupId { get; set; }
        [Required]
        public int MaterialTypeId { get; set; }
        [Required]
        public string Data { get; set; }

    }
}
