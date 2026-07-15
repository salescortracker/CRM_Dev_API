using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class TenantModule
{
    public Guid TenantModuleId { get; set; }

    public Guid CustomerTenantId { get; set; }

    public string ModuleCode { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public DateTime EnabledOn { get; set; }

    public DateTime? DisabledOn { get; set; }

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
