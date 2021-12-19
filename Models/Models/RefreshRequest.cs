#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class RefreshRequest
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
