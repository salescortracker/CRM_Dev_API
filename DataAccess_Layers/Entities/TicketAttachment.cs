using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class TicketAttachment
{
    public Guid TicketAttachmentId { get; set; }

    public Guid SupportTicketId { get; set; }

    public string FileName { get; set; } = null!;

    public string FileUrl { get; set; } = null!;

    public string? FileType { get; set; }

    public long? FileSizeBytes { get; set; }

    public Guid UploadedByUserId { get; set; }

    public DateTime UploadedOn { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual SupportTicket SupportTicket { get; set; } = null!;

    public virtual User UploadedByUser { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
