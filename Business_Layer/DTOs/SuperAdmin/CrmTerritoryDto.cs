using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CrmTerritoryDto
    {
        public int TerritoryId { get; set; }

        public string TerritoryName { get; set; } = string.Empty;

        public string TerritoryCode { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;

        public string? TerritoryManager { get; set; }

        public int CustomerCount { get; set; }

        public string Priority { get; set; } = string.Empty;

        public string Status { get; set; } = "Active";

        public string? Description { get; set; }
    }
}
