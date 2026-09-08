using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CrmIndustryDto
    {
        public int IndustryId { get; set; }

        public string IndustryName { get; set; } = string.Empty;

        public string IndustryCode { get; set; } = string.Empty;

        public string IndustryCategory { get; set; } = string.Empty;

        public int CustomerCount { get; set; }

        public string Priority { get; set; } = string.Empty;

        public string Status { get; set; } = "Active";

        public string? Description { get; set; }
    }
}
