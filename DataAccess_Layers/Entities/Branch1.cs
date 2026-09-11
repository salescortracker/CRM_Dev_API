using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Branch1
{
    public int BranchId { get; set; }

    public int OrganizationId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public string BranchName { get; set; } = null!;

    public string BranchCode { get; set; } = null!;

    public string? BranchManager { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? ZipCode { get; set; }

    public TimeOnly? OpeningTime { get; set; }

    public TimeOnly? ClosingTime { get; set; }

    public bool Status { get; set; }

    public bool HeadOffice { get; set; }

    public string? Remarks { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<BusinessHour> BusinessHours { get; set; } = new List<BusinessHour>();

    public virtual ICollection<BusinessUnit> BusinessUnits { get; set; } = new List<BusinessUnit>();

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<CompanyAdministrator> CompanyAdministrators { get; set; } = new List<CompanyAdministrator>();

    public virtual ICollection<HolidayCalendar> HolidayCalendars { get; set; } = new List<HolidayCalendar>();

    public virtual OrganizationDatum Organization { get; set; } = null!;

    public virtual Region Region { get; set; } = null!;
}
