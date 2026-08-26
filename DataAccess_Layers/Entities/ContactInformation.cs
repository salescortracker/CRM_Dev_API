using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class ContactInformation
{
    public int ContactInformationId { get; set; }

    public string ContactNumber { get; set; } = null!;

    public string Salutation { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? Designation { get; set; }

    public string? Department { get; set; }

    public int CompanyInformationId { get; set; }

    public int? ContactTypeId { get; set; }

    public int? RelationshipId { get; set; }

    public string BusinessEmail { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? AlternatePhone { get; set; }

    public string? Website { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public int? StateId { get; set; }

    public int? CountryId { get; set; }

    public string? PostalCode { get; set; }

    public string? Notes { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual CompanyInformation CompanyInformation { get; set; } = null!;

    public virtual ContactType? ContactType { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Relationship? Relationship { get; set; }

    public virtual StateMaster? State { get; set; }
}
