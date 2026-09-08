using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CrmSourceDto
    {
        public int SourceId { get; set; }

        public string SourceName { get; set; } = string.Empty;

        public string SourceCode { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal ConversionRate { get; set; }

        public string Priority { get; set; } = string.Empty;

        public string Status { get; set; } = "Active";

        public string? Description { get; set; }
    }
}
