using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Refund
{
    public Guid RefundId { get; set; }

    public Guid PaymentId { get; set; }

    public Guid CustomerId { get; set; }

    public string RefundNumber { get; set; } = null!;

    public DateOnly RefundDate { get; set; }

    public decimal RefundAmount { get; set; }

    public byte RefundMode { get; set; }

    public string? TransactionReference { get; set; }

    public string Reason { get; set; } = null!;

    public byte Status { get; set; }

    public Guid? ApprovedByUserId { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual User? ApprovedByUser { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Payment Payment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
