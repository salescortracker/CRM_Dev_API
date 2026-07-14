using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class QuotationApproval
{
    public Guid QuotationApprovalId { get; set; }

    public Guid QuotationId { get; set; }

    public int ApprovalLevel { get; set; }

    public Guid ApproverUserId { get; set; }

    public byte ApprovalStatus { get; set; }

    public string? ApprovalRemarks { get; set; }

    public DateTime? ApprovedOn { get; set; }

    public DateTime RequestedOn { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual User ApproverUser { get; set; } = null!;

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Quotation Quotation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
