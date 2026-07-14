using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.MasterDTO_s
{
    public class CompanyDto
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string? CompanyCode { get; set; }

        public string? IndustryType { get; set; }

        public string? Headquarters { get; set; }

        public bool? IsActive { get; set; } = true;

        public int? IsDefault { get; set; }

        public int? PlanId { get; set; }

        public DateTime? PlanStartDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string? CompanyEmail { get; set; }

        public string? CompanyContact { get; set; }

        public string? CompanyAddress { get; set; }

        public string? CompanyLogo { get; set; }
    }
}
