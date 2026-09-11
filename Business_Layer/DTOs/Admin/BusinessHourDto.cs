using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class BusinessHourDto
    {
        public int BusinessHoursId { get; set; }

        public int CompanyId { get; set; }

        public int RegionId { get; set; }

        public string BusinessHoursName { get; set; } = string.Empty;

        public int BranchId { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public TimeOnly? BreakStart { get; set; }

        public TimeOnly? BreakEnd { get; set; }

        public string WorkingDays { get; set; } = string.Empty;

        public string? Weekend { get; set; }

        public decimal? TotalWorkingHours { get; set; }

        public int? LateMarkGraceTimeMinutes { get; set; }

        public decimal? HalfDayThresholdHours { get; set; }

        public bool FlexibleHours { get; set; }

        public bool OvertimeAllowed { get; set; }

        public string? Description { get; set; }

        public bool Active { get; set; }
    }
}
