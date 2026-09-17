using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class LicenseManagement
{
    public int LicenseManagementId { get; set; }

    public string LicenseName { get; set; } = null!;

    public string LicenseCode { get; set; } = null!;

    public int CompanyId { get; set; }

    public int LicenseTypeId { get; set; }

    public int? MaximumUsers { get; set; }

    public decimal? StorageLimitGb { get; set; }

    public DateOnly? LicenseStartDate { get; set; }

    public DateOnly? LicenseExpiryDate { get; set; }

    public long? ApicallsPerMonth { get; set; }

    public string? SupportLevel { get; set; }

    public bool Crmmodule { get; set; }

    public bool SalesModule { get; set; }

    public bool MarketingModule { get; set; }

    public bool SupportModule { get; set; }

    public bool Apiaccess { get; set; }

    public bool MobileApp { get; set; }

    public bool ActiveLicense { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual License LicenseType { get; set; } = null!;
}
