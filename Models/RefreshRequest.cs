﻿#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class RefreshRequest
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
