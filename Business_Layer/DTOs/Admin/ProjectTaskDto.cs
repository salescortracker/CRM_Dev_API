using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class ProjectTaskDto
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; } = string.Empty;

        public int ProjectId { get; set; }

        public int? MilestoneId { get; set; }

        public string AssignedTo { get; set; } = string.Empty;

        public int PriorityId { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateOnly? StartDate { get; set; }

        public DateOnly? DueDate { get; set; }

        public decimal? EstimatedHours { get; set; }

        public decimal? ActualHours { get; set; }

        public decimal CompletionPercentage { get; set; }

        public string? Tags { get; set; }

        public string? Description { get; set; }
    }
}
