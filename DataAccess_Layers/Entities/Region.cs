using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Region
{
    public int RegionId { get; set; }

    public int CompanyId { get; set; }

    public string RegionName { get; set; } = null!;

    public string? Country { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? UserId { get; set; }

    public bool? IsActive { get; set; }

    public string? RegionCode { get; set; }

    public string? ContactPerson { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Branch1> Branch1s { get; set; } = new List<Branch1>();

    public virtual ICollection<BusinessUnit> BusinessUnits { get; set; } = new List<BusinessUnit>();

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<CompanyAdministrator> CompanyAdministrators { get; set; } = new List<CompanyAdministrator>();
}
