using System.Collections.Generic;

namespace Business_Layer.DTOs.Roles
{
    public class RoleDto
    {
        public int RoleId { get; set; }

        public int CompanyId { get; set; }

        public int RegionId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public string AccessLevel { get; set; } = "Full Access";

        public string? Description { get; set; }

        public bool Status { get; set; } = true;

        public bool IsDefault { get; set; }

        public int UserCount { get; set; }

        public Dictionary<int, bool> Permissions { get; set; } = new();
    }
}
