using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.User
{
    public class CompanyInformationDto
    {
        public int CompanyInformationId { get; set; }

        // Company Information
        public string CompanyName { get; set; } = string.Empty;

        public string? LegalCompanyName { get; set; }

        public int IndustryId { get; set; }
        public string? IndustryName { get; set; }

        public int CompanyTypeId { get; set; }
        public string? CompanyTypeName { get; set; }

        public string CompanyOwner { get; set; } = string.Empty;

        public string CompanyStatus { get; set; } = "Active";

        public string? Website { get; set; }

        public string? CompanyPhone { get; set; }

        public string? CompanyEmail { get; set; }

        public string? CompanyDescription { get; set; }


        // Company Address
        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string City { get; set; } = string.Empty;

        public int StateId { get; set; }
        public string? StateName { get; set; }

        public int CountryId { get; set; }
        public string? CountryName { get; set; }

        public string? PostalCode { get; set; }


        // Business Details
        public string? NumberOfEmployees { get; set; }

        public decimal? AnnualRevenue { get; set; }


        // Registration & Social
        public string? Gstnumber { get; set; }

        public string? Pannumber { get; set; }

        public string? CinregistrationNumber { get; set; }

        public string? LinkedInCompanyUrl { get; set; }


        // Primary Contact
        public string? PrimaryContactName { get; set; }

        public string? PrimaryContactDesignation { get; set; }

        public string? PrimaryContactEmail { get; set; }

        public string? PrimaryContactPhone { get; set; }


        // Multi Tenant
        public int CompanyId { get; set; }

        public int RegionId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
