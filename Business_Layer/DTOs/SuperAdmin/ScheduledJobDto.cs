using System;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class ScheduledJobDto
    {
        // =====================================================
        // PRIMARY KEY
        // =====================================================

        public int ScheduledJobId { get; set; }


        // =====================================================
        // JOB INFORMATION
        // =====================================================

        public string JobName { get; set; } = string.Empty;

        public string JobType { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Frequency { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public int? RepeatEvery { get; set; }

        public int? DayOfWeek { get; set; }

        public int? DayOfMonth { get; set; }

        public string ActionType { get; set; } = string.Empty;

        public string? Parameters { get; set; }

        public int? RetryCount { get; set; }

        public int? TimeoutMinutes { get; set; }


        // =====================================================
        // EXECUTION INFORMATION
        // =====================================================

        public DateTime? LastRunAt { get; set; }

        public DateTime? NextRunAt { get; set; }

        public string? LastRunStatus { get; set; }


        // =====================================================
        // COMPANY / REGION
        // =====================================================

        public int CompanyId { get; set; }

        public int RegionId { get; set; }


        // =====================================================
        // STATUS
        // =====================================================

        public bool IsActive { get; set; }


        // =====================================================
        // AUDIT
        // =====================================================

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}