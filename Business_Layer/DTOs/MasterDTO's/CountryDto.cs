using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.MasterDTO_s
{
    public class CountryDto
    {
        public int CountryId { get; set; }

        public int CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public int RegionId { get; set; }

        public string? RegionName { get; set; }

        public string CountryName { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
