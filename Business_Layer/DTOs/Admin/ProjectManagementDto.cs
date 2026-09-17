using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class ProjectManagementDto
    {
        public int ProjectId { get; set; }

        public string ProjectCode { get; set; } = string.Empty;

        public string ProjectName { get; set; } = string.Empty;

        public string? Customer { get; set; }

        public int? ProjectManager { get; set; }

        public string? ProjectType { get; set; }

        public string Priority { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public decimal? Budget { get; set; }

        public decimal CompletionPercentage { get; set; }

        public string? TeamMembers { get; set; }

        public string? ProjectDescription { get; set; }
    }
}
