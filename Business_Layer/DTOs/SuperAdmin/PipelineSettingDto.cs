using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class PipelineSettingDto
    {
        public int PipelineSettingId { get; set; }

        public int CompanyId { get; set; }

        public int RegionId { get; set; }

        public string PipelineName { get; set; } = string.Empty;

        public string PipelineCode { get; set; } = string.Empty;

        public string PipelineType { get; set; } = string.Empty;

        public int TotalStages { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; } = "Active";
    }
}
