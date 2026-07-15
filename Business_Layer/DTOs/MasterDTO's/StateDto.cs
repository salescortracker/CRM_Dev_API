using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.MasterDTO_s
{
    public class StateDto
    {

        public int StateId { get; set; }

        public int CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public int RegionId { get; set; }

        public string? RegionName { get; set; }

        public int CountryId { get; set; }

        public string? CountryName { get; set; }

        public string StateName { get; set; } = string.Empty;

        public string StateCode { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
