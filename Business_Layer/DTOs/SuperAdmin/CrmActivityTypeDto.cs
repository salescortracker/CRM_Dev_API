using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CrmActivityTypeDto
    {
        public int ActivityTypeId { get; set; }

        public string ActivityName { get; set; } = string.Empty;

        public string ActivityCode { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }

        public string? Description { get; set; }

        public int ReminderBeforeMinutes { get; set; }

        public string Status { get; set; } = "Active";

        public bool ReminderRequired { get; set; }
    }
}
