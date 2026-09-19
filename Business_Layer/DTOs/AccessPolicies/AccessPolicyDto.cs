namespace Business_Layer.DTOs.AccessPolicies
{
    public class AccessPolicyDto
    {
        public int PolicyId { get; set; }

        public string PolicyName { get; set; } = string.Empty;

        public string PolicyCode { get; set; } = string.Empty;

        public int CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public int? RegionId { get; set; }

        public string? RegionName { get; set; }

        public string RoleName { get; set; } = "User";

        public int? DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public string? Modules { get; set; }

        public bool IpRestriction { get; set; }

        public string? AllowedIp { get; set; }

        public string LoginRestriction { get; set; } = "24 Hours";

        public int SessionTimeout { get; set; } = 30;

        public bool Status { get; set; } = true;

        public bool IsDefault { get; set; }
    }
}
