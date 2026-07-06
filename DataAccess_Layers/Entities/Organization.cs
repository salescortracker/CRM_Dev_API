using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Organization
{
    public Guid OrganizationId { get; set; }

    public string OrganizationCode { get; set; } = null!;

    public string OrganizationName { get; set; } = null!;

    public string? LegalName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Website { get; set; }

    public string? Gstnumber { get; set; }

    public string? Pannumber { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public string? LogoUrl { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Department1> Department1s { get; set; } = new List<Department1>();

    public virtual ICollection<LeadCall> LeadCalls { get; set; } = new List<LeadCall>();

    public virtual ICollection<LeadSource> LeadSources { get; set; } = new List<LeadSource>();

    public virtual OrganizationSetting? OrganizationSetting { get; set; }

    public virtual ICollection<Role1> Role1s { get; set; } = new List<Role1>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
