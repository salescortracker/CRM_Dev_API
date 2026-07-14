using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class TenantStorageLimit
{
    public Guid TenantStorageLimitId { get; set; }

    public Guid CustomerTenantId { get; set; }

    public decimal TotalStorageLimitGb { get; set; }

    public decimal UsedStorageGb { get; set; }

    public decimal AvailableStorageGb { get; set; }

    public decimal WarningThresholdPercentage { get; set; }

    public DateTime LastCalculatedOn { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual CustomerTenant CustomerTenant { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
