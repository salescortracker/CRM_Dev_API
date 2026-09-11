using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class CompanyProfileDto
    {
        public int CompanyProfileId { get; set; }

        public int CompanyId { get; set; }

        public int RegionId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string CompanyCode { get; set; } = string.Empty;

        public string? LegalName { get; set; }

        public string? RegistrationNumber { get; set; }

        public string? Gstnumber { get; set; }

        public string? Pannumber { get; set; }

        public int? IndustryId { get; set; }

        public int? CompanyTypeId { get; set; }

        public DateOnly? EstablishedDate { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Mobile { get; set; }

        public string? Website { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public int? CountryId { get; set; }

        public int? StateId { get; set; }

        public string? City { get; set; }

        public string? Pincode { get; set; }

        public int? CurrencyId { get; set; }

        public string? FinancialYear { get; set; }

        public string? CompanyLogoPath { get; set; }

        public string? Description { get; set; }

        public bool Active { get; set; }
    }
}
