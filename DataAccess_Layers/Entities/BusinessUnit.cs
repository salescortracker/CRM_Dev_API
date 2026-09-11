using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class BusinessUnit
{
    public int BusinessUnitId { get; set; }

    public int OrganizationId { get; set; }

    public int CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? BranchId { get; set; }

    public string BusinessUnitName { get; set; } = null!;

    public string BusinessUnitCode { get; set; } = null!;

    public string? ParentBusinessUnit { get; set; }

    public string? BusinessUnitHead { get; set; }

    public string? UnitHead { get; set; }

    public string? Email { get; set; }

    public string? MobileNumber { get; set; }

    public string? ContactNumber { get; set; }

    public string? ExtensionNumber { get; set; }

    public string? Description { get; set; }

    public string? Remarks { get; set; }

    public bool Status { get; set; }

    public bool DefaultBusinessUnit { get; set; }

    public bool BillableUnit { get; set; }

    public int? EmployeeStrength { get; set; }

    public string? CostCenterCode { get; set; }

    public decimal? AnnualBudget { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Branch1? Branch { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<HolidayCalendar> HolidayCalendars { get; set; } = new List<HolidayCalendar>();

    public virtual OrganizationDatum Organization { get; set; } = null!;

    public virtual Region? Region { get; set; }
}
