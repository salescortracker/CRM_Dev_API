using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class OrganizationDatum
{
    public int OrganizationId { get; set; }

    public string OrganizationCode { get; set; } = null!;

    public string OrganizationName { get; set; } = null!;

    public string? LegalName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Website { get; set; }

    public string? Gstnumber { get; set; }

    public string? Pannumber { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public string? LogoUrl { get; set; }

    public byte Status { get; set; }

    public string? Domain { get; set; }

    public string? ContactPerson { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactMobile { get; set; }

    public string? TimeZone { get; set; }

    public string? CurrencyCode { get; set; }

    public DateOnly? SubscriptionStartDate { get; set; }

    public DateOnly? RenewalDate { get; set; }

    public int? PlanId { get; set; }

    public int MaxUsers { get; set; }

    public int MaxStorageGb { get; set; }

    public int StorageUsedGb { get; set; }

    public decimal? MonthlyRevenue { get; set; }

    public string? BrandColor { get; set; }

    public string? Industry { get; set; }

    public string? Features { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual SubscriptionPlanMaster? Plan { get; set; }
}
