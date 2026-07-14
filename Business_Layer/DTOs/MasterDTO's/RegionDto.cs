using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.MasterDTO_s
{
    public class RegionDto
    {
        public int RegionId { get; set; }

        public int CompanyId { get; set; }

        public string RegionName { get; set; } = string.Empty;

        public string? Country { get; set; }

        public bool? IsActive { get; set; } = true;

        public string? RegionCode { get; set; }

        public string? ContactPerson { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        // Display purpose
        public string? CompanyName { get; set; }
    }
}
