using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class SubscriptionPlanMaster
{
    public int PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public decimal Price { get; set; }

    public int UserLimit { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public string? Description { get; set; }

    public int StorageLimit { get; set; }

    public int ApiLimit { get; set; }

    public string Accent { get; set; } = null!;

    public string? Features { get; set; }
}
