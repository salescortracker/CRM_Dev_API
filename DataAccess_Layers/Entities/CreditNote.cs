using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CreditNote
{
    public Guid CreditNoteId { get; set; }

    public Guid OrganizationId { get; set; }

    public string CreditNoteNumber { get; set; } = null!;

    public Guid InvoiceId { get; set; }

    public Guid CustomerId { get; set; }

    public DateOnly CreditNoteDate { get; set; }

    public string Reason { get; set; } = null!;

    public decimal Amount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal TotalAmount { get; set; }

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

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
