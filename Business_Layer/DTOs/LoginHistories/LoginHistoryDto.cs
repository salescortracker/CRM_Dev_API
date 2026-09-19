using System;

namespace Business_Layer.DTOs.LoginHistories
{
    public class LoginHistoryDto
    {
        public int LoginHistoryId { get; set; }

        public int? UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string LoginType { get; set; } = "Web";

        public string? Device { get; set; }

        public string? IpAddress { get; set; }

        public string? Location { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime LoginTime { get; set; }
    }
}
