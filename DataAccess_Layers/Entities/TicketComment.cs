using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class TicketComment
{
    public Guid TicketCommentId { get; set; }

    public Guid SupportTicketId { get; set; }

    public string CommentText { get; set; } = null!;

    public bool IsInternalNote { get; set; }

    public Guid CommentedByUserId { get; set; }

    public DateTime CommentedOn { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual User CommentedByUser { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual SupportTicket SupportTicket { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
