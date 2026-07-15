using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Customer
{
    public Guid CustomerId { get; set; }

    public Guid OrganizationId { get; set; }

    public string CustomerCode { get; set; } = null!;

    public byte CustomerType { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? LegalName { get; set; }

    public string PrimaryContactName { get; set; } = null!;

    public string? PrimaryEmail { get; set; }

    public string? PrimaryMobileNumber { get; set; }

    public string? Website { get; set; }

    public string? Industry { get; set; }

    public string? Gstnumber { get; set; }

    public string? Pannumber { get; set; }

    public byte CustomerStatus { get; set; }

    public Guid? AccountManagerUserId { get; set; }

    public Guid? SourceLeadId { get; set; }

    public DateTime? OnboardedOn { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual User? AccountManagerUser { get; set; }

    public virtual ICollection<CreditNote> CreditNotes { get; set; } = new List<CreditNote>();

    public virtual ICollection<CustomerAddOn> CustomerAddOns { get; set; } = new List<CustomerAddOn>();

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();

    public virtual CustomerBillingDetail? CustomerBillingDetail { get; set; }

    public virtual ICollection<CustomerContact> CustomerContacts { get; set; } = new List<CustomerContact>();

    public virtual ICollection<CustomerTenant> CustomerTenants { get; set; } = new List<CustomerTenant>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<OnboardingProject> OnboardingProjects { get; set; } = new List<OnboardingProject>();

    public virtual ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Quotation> Quotations { get; set; } = new List<Quotation>();

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();

    public virtual Lead? SourceLead { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();

    public virtual ICollection<TwilioSmslog> TwilioSmslogs { get; set; } = new List<TwilioSmslog>();
}
