using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class UserGroupDto
    {
        public int UserGroupId { get; set; }

        public string GroupName { get; set; } = string.Empty;

        public string GroupCode { get; set; } = string.Empty;

        public string? GroupType { get; set; }

        public int? DepartmentId { get; set; }

        public string? Team { get; set; }

        public string? ReportingManager { get; set; }

        public string? DefaultRole { get; set; }

        public int? UserLimit { get; set; }

        public int? PriorityId { get; set; }

        public bool Status { get; set; }

        public string? Description { get; set; }

    }
}
