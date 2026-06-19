using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layers.Entities
{
    public class UserLogin
    {
        [Key]
        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? MobileNumber { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
        public string? PasswordSalt { get; set; }

        public string Role { get; set; } = "User";

        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public int FailedLoginAttempts { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetExpiry { get; set; }
    }
}