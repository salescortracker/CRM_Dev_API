using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class PasswordPolicyDto
    {
        public int PasswordPolicyId { get; set; }

        public string PolicyName { get; set; } = string.Empty;

        public int MinimumPasswordLength { get; set; }

        public int MaximumPasswordLength { get; set; }

        public int? PasswordExpiryDays { get; set; }

        public int? PasswordHistory { get; set; }

        public int? MaximumFailedLoginAttempts { get; set; }

        public int? AccountLockDurationMinutes { get; set; }

        public int? SessionTimeoutMinutes { get; set; }

        public bool Mfarequirement { get; set; }

        public bool RequireUppercaseLetter { get; set; }

        public bool RequireLowercaseLetter { get; set; }

        public bool RequireNumber { get; set; }

        public bool RequireSpecialCharacter { get; set; }

        public bool PreventUsernameInPassword { get; set; }

        public bool ForcePasswordChangeOnFirstLogin { get; set; }

        public bool AllowPasswordReuse { get; set; }

        public bool PolicyActive { get; set; }
    }
}
