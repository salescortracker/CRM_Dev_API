using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class OrganizationSetting
{
    public Guid OrganizationSettingId { get; set; }

    public Guid OrganizationId { get; set; }

    public string TimeZone { get; set; } = null!;

    public string CurrencyCode { get; set; } = null!;

    public string CurrencySymbol { get; set; } = null!;

    public string DateFormat { get; set; } = null!;

    public byte FinancialYearStartMonth { get; set; }

    public string DefaultLanguage { get; set; } = null!;

    public string? DefaultCountryCode { get; set; }

    public string? InvoicePrefix { get; set; }

    public string? QuotationPrefix { get; set; }

    public bool IsGstenabled { get; set; }

    public decimal? DefaultGstpercentage { get; set; }

    public decimal? StorageWarningPercentage { get; set; }

    public decimal? UserLimitWarningPercentage { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;
}
