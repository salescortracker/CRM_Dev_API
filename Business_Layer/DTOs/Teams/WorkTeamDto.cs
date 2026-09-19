using System.Collections.Generic;

namespace Business_Layer.DTOs.Teams
{
    public class WorkTeamDto
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public string TeamCode { get; set; } = string.Empty;

        public int CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public int? RegionId { get; set; }

        public string? RegionName { get; set; }

        public int? DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public int? TeamLeadId { get; set; }

        public string? TeamLeadName { get; set; }

        public string? Description { get; set; }

        public bool Status { get; set; } = true;

        public bool IsDefault { get; set; }

        public List<int> MemberIds { get; set; } = new();

        public List<string> MemberNames { get; set; } = new();
    }
}
