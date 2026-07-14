using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class TwilioPhoneNumber
{
    public Guid TwilioPhoneNumberId { get; set; }

    public Guid TwilioConfigurationId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? FriendlyName { get; set; }

    public string? Capabilities { get; set; }

    public Guid? AssignedToUserId { get; set; }

    public bool IsDefault { get; set; }

    public byte Status { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual User? AssignedToUser { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual TwilioConfiguration TwilioConfiguration { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
