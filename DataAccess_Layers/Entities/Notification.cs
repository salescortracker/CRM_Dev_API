using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Notification
{
    public Guid NotificationId { get; set; }

    public Guid OrganizationId { get; set; }

    public Guid UserId { get; set; }

    public byte NotificationType { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? ReferenceType { get; set; }

    public Guid? ReferenceId { get; set; }

    public bool IsRead { get; set; }

    public DateTime? ReadOn { get; set; }

    public DateTime SentOn { get; set; }

    public byte Channel { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
