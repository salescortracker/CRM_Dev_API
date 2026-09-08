using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CrmmoduleConfigurationSalesTarget
{
    public int SalesTargetId { get; set; }

    public string TargetName { get; set; } = null!;

    public string TargetCode { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public string Department { get; set; } = null!;

    public decimal TargetAmount { get; set; }

    public decimal AchievedAmount { get; set; }

    public string TargetPeriod { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }
}
