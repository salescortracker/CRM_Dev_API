using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Branch
{
    public Guid BranchId { get; set; }

    public Guid OrganizationId { get; set; }

    public string BranchCode { get; set; } = null!;

    public string BranchName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? AddressLine1 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public Guid? BranchHeadUserId { get; set; }

    public bool IsHeadOffice { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual ICollection<Department1> Department1s { get; set; } = new List<Department1>();

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
