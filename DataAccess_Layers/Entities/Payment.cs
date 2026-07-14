using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Payment
{
    public Guid PaymentId { get; set; }

    public Guid OrganizationId { get; set; }

    public Guid CustomerId { get; set; }

    public string PaymentNumber { get; set; } = null!;

    public DateOnly PaymentDate { get; set; }

    public byte PaymentMode { get; set; }

    public decimal Amount { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public string? TransactionReference { get; set; }

    public string? BankName { get; set; }

    public string? PaymentGatewayName { get; set; }

    public byte PaymentStatus { get; set; }

    public Guid? ReceivedByUserId { get; set; }

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

    public virtual Customer Customer { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();

    public virtual User? ReceivedByUser { get; set; }

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual User User { get; set; } = null!;
}
