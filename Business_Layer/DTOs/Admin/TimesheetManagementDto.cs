using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class TimesheetManagementDto
    {
        public int TimesheetId { get; set; }

        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }

        public int? TaskId { get; set; }

        public DateOnly TimesheetDate { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public decimal TotalHours { get; set; }

        public string BillingType { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int? ApprovedBy { get; set; }

        public string? WorkDescription { get; set; }
    }
}
