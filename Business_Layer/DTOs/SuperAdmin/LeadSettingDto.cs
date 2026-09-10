using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class LeadSettingDto
    {
        public int LeadSettingId { get; set; }

        //public int CompanyId { get; set; }

        //public int RegionId { get; set; }

        public string SettingName { get; set; } = string.Empty;

        public string LeadStatus { get; set; } = string.Empty;

        public string LeadPriority { get; set; } = string.Empty;

        public string AssignmentRule { get; set; } = string.Empty;

        public int FollowUpDays { get; set; }

        public string Status { get; set; } = "Active";

        public bool EnableAutoAssignment { get; set; }

        public bool EmailNotification { get; set; }
    }
}
