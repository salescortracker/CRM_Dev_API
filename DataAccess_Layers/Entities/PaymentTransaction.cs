using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class PaymentTransaction
{
    public Guid PaymentTransactionId { get; set; }

    public Guid PaymentId { get; set; }

    public Guid InvoiceId { get; set; }

    public decimal AllocatedAmount { get; set; }

    public DateTime AllocationDate { get; set; }

    public string? Remarks { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Payment Payment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
