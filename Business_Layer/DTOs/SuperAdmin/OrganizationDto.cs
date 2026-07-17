using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class OrganizationDto
    {
        public int OrganizationId { get; set; }

        public string OrganizationCode { get; set; } = string.Empty;

        public string OrganizationName { get; set; } = string.Empty;

        public string? LegalName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Website { get; set; }

        public string? GSTNumber { get; set; }

        public string? PANNumber { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? PostalCode { get; set; }

        public string? LogoUrl { get; set; }
        public IFormFile? LogoFile { get; set; }

        /// <summary>
        /// 1 = Active
        /// 2 = Trial
        /// 3 = Inactive
        /// 4 = Suspended
        /// </summary>
       // public byte Status { get; set; } = 1;

        public string? Domain { get; set; }

        public string? ContactPerson { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactMobile { get; set; }

        public string? TimeZone { get; set; }

        public string? CurrencyCode { get; set; }

        public DateOnly? SubscriptionStartDate { get; set; }

        public DateOnly? RenewalDate { get; set; }

        public int? PlanId { get; set; }

        public string? PlanName { get; set; }

        public int MaxUsers { get; set; }

        public int MaxStorageGB { get; set; }

        public int StorageUsedGB { get; set; }

        public decimal MonthlyRevenue { get; set; }

        public string? BrandColor { get; set; }

        public string? Industry { get; set; }

        /// <summary>
        /// JSON String
        /// Example:
        /// {"leads":true,"contacts":true,"reports":false}
        /// </summary>
        public string? Features { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
