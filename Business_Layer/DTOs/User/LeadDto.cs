using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.User
{
    public class LeadDto
    {
        // =====================================================
        // LEAD INFORMATION
        // =====================================================

        public int LeadId { get; set; }

        public string LeadNumber { get; set; } = string.Empty;

        public string? Salutation { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? JobTitle { get; set; }

        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Mobile { get; set; }

        public int LeadTypeId { get; set; }

        public string? LeadTypeName { get; set; }

        public int? LeadOwnerId { get; set; }

        public string? LeadOwnerName { get; set; }

        public int LeadSourceId { get; set; }

        public string? LeadSourceName { get; set; }

        public string LeadStatus { get; set; } = string.Empty;

        public string? LeadRating { get; set; }

        public int? LeadScore { get; set; }

        public string? PreferredContactMethod { get; set; }


        // =====================================================
        // COMPANY INFORMATION
        // =====================================================

        public string CompanyName { get; set; } = string.Empty;

        public string? Website { get; set; }

        public int? IndustryId { get; set; }

        public string? IndustryName { get; set; }

        public string? CompanySize { get; set; }

        public decimal? AnnualRevenue { get; set; }

        public string? StreetAddress { get; set; }

        public string? City { get; set; }

        public int? StateId { get; set; }

        public string? StateName { get; set; }

        public string? PostalCode { get; set; }

        public int? CountryId { get; set; }

        public string? CountryName { get; set; }


        // =====================================================
        // ADDITIONAL INFORMATION
        // =====================================================

        public decimal? EstimatedDealValue { get; set; }

        public DateOnly? ExpectedCloseDate { get; set; }

        public string? Description { get; set; }


        // =====================================================
        // CRM RELATIONSHIP
        // =====================================================

        public int? CompanyId { get; set; }

        public int? PrimaryContactId { get; set; }


        // =====================================================
        // COMPANY / REGION
        // =====================================================

        public int CrmcompanyId { get; set; }

        public int RegionId { get; set; }


        // =====================================================
        // AUDIT
        // =====================================================

        public bool IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
