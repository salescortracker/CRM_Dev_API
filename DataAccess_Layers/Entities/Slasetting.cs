using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Slasetting
{
    public Guid SlasettingId { get; set; }

    public Guid OrganizationId { get; set; }

    public string Slaname { get; set; } = null!;

    public byte Priority { get; set; }

    public int FirstResponseMinutes { get; set; }

    public int ResolutionMinutes { get; set; }

    public bool BusinessHoursOnly { get; set; }

    public byte Status { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
