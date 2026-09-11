using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class HolidayCalendar
{
    public int HolidayCalendarId { get; set; }

    public string HolidayName { get; set; } = null!;

    public DateOnly HolidayDate { get; set; }

    public string? HolidayType { get; set; }

    public int? BranchId { get; set; }

    public int? BusinessUnitId { get; set; }

    public int? DepartmentId { get; set; }

    public string? HolidayCategory { get; set; }

    public string? ApplicableFor { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public bool RecurringHoliday { get; set; }

    public int Year { get; set; }

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Branch1? Branch { get; set; }

    public virtual BusinessUnit? BusinessUnit { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Department? Department { get; set; }

    public virtual StateMaster? State { get; set; }
}
