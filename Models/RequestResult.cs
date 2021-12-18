﻿#nullable disable

namespace Models
{
    public class RequestResult
    {
        public bool Successful { get; set; }
        public string Error { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}