using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.User
{
    public class ContactDto
    {
        // =====================================================
        // CONTACT INFORMATION
        // =====================================================

        public int ContactInformationId { get; set; }

        public string ContactNumber { get; set; } = string.Empty;

        // =====================================================
        // PERSONAL INFORMATION
        // =====================================================

        public string Salutation { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }

        public string? Designation { get; set; }

        public string? Department { get; set; }

        // =====================================================
        // COMPANY ASSOCIATION
        // =====================================================

        public int CompanyInformationId { get; set; }

        public string? CompanyName { get; set; }

        public int? ContactTypeId { get; set; }

        public string? ContactTypeName { get; set; }

        public int? RelationshipId { get; set; }

        public string? RelationshipName { get; set; }

        // =====================================================
        // CONTACT INFORMATION
        // =====================================================

        public string BusinessEmail { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string? AlternatePhone { get; set; }

        public string? Website { get; set; }

        // =====================================================
        // ADDRESS
        // =====================================================

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? City { get; set; }

        public int? StateId { get; set; }

        public string? StateName { get; set; }

        public int? CountryId { get; set; }

        public string? CountryName { get; set; }

        public string? PostalCode { get; set; }

        // =====================================================
        // ADDITIONAL INFORMATION
        // =====================================================

        public string? Notes { get; set; }

        // =====================================================
        // COMPANY / REGION
        // =====================================================

        public int CompanyId { get; set; }

        public int RegionId { get; set; }

        // =====================================================
        // AUDIT
        // =====================================================

        public bool IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
