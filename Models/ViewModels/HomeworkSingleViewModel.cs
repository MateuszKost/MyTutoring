using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class HomeworkSingleViewModel
    {
        [Required]
        public string FileName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Nazwa musi mieć minimum 5, a maksimum 20 znaków")]
        public string Name { get; set; }
        [Required]
        public string StudentId { get; set; }
        [Required]
        public string TutorId { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public string? DataTask { get; set; }
        public string? TaskSolutionFileName { get; set; }
        public Uri? UrlTask { get; set; }
        public Uri? UrlTaskSolution { get; set; }
        public float? Grade { get; set; }
        public bool? Status { get; set; }
        public int? Id { get; set; }
    }
}
