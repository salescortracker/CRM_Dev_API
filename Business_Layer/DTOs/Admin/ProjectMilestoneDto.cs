using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class ProjectMilestoneDto
    {
        public int MilestoneId { get; set; }

        public string MilestoneName { get; set; } = string.Empty;

        public string Project { get; set; } = string.Empty;

        public int? Owner { get; set; }

        public DateOnly? DueDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public decimal CompletionPercentage { get; set; }

        public decimal? EstimatedHours { get; set; }

        public decimal? ActualHours { get; set; }

        public string Priority { get; set; } = string.Empty;

        public DateOnly? TargetDate { get; set; }

        public string? Description { get; set; }
    }
}
