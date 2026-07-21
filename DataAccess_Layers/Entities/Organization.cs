using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Organization
{
    public Guid OrganizationId { get; set; }

    public string OrganizationCode { get; set; } = null!;

    public string OrganizationName { get; set; } = null!;

    public string? LegalName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Website { get; set; }

    public string? Gstnumber { get; set; }

    public string? Pannumber { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public string? LogoUrl { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? Domain { get; set; }

    public string? ContactPerson { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactMobile { get; set; }

    public string? TimeZone { get; set; }

    public string? CurrencyCode { get; set; }

    public DateOnly? SubscriptionStartDate { get; set; }

    public DateOnly? RenewalDate { get; set; }

    public int MaxUsers { get; set; }

    public int MaxStorageGb { get; set; }

    public string? BrandColor { get; set; }

    public string? Industry { get; set; }

    public int StorageUsedGb { get; set; }

    public decimal? MonthlyRevenue { get; set; }

    public string? Features { get; set; }

    public int? PlanId { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<CallRecording> CallRecordings { get; set; } = new List<CallRecording>();

    public virtual ICollection<CallingCampaign> CallingCampaigns { get; set; } = new List<CallingCampaign>();

    public virtual ICollection<CreditNote> CreditNoteCompanies { get; set; } = new List<CreditNote>();

    public virtual ICollection<CreditNote> CreditNoteOrganizations { get; set; } = new List<CreditNote>();

    public virtual ICollection<CustomerTenant> CustomerTenants { get; set; } = new List<CustomerTenant>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<DataMigrationRequest> DataMigrationRequests { get; set; } = new List<DataMigrationRequest>();

    public virtual ICollection<Department1> Department1s { get; set; } = new List<Department1>();

    public virtual ICollection<GoLiveChecklist> GoLiveChecklists { get; set; } = new List<GoLiveChecklist>();

    public virtual ICollection<Invoice> InvoiceCompanies { get; set; } = new List<Invoice>();

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public virtual ICollection<Invoice> InvoiceOrganizations { get; set; } = new List<Invoice>();

    public virtual ICollection<LeadCall> LeadCalls { get; set; } = new List<LeadCall>();

    public virtual ICollection<LeadSource> LeadSources { get; set; } = new List<LeadSource>();

    public virtual ICollection<OnboardingTask> OnboardingTasks { get; set; } = new List<OnboardingTask>();

    public virtual ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();

    public virtual ICollection<OpportunityStage> OpportunityStages { get; set; } = new List<OpportunityStage>();

    public virtual OrganizationSetting? OrganizationSetting { get; set; }

    public virtual ICollection<Payment> PaymentCompanies { get; set; } = new List<Payment>();

    public virtual ICollection<Payment> PaymentOrganizations { get; set; } = new List<Payment>();

    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<QuotationApproval> QuotationApprovals { get; set; } = new List<QuotationApproval>();

    public virtual ICollection<Quotation> QuotationCompanies { get; set; } = new List<Quotation>();

    public virtual ICollection<QuotationItem> QuotationItems { get; set; } = new List<QuotationItem>();

    public virtual ICollection<Quotation> QuotationOrganizations { get; set; } = new List<Quotation>();

    public virtual ICollection<QuotationVersion> QuotationVersions { get; set; } = new List<QuotationVersion>();

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual ICollection<Role1> Role1s { get; set; } = new List<Role1>();

    public virtual ICollection<SalesOrder> SalesOrderCompanies { get; set; } = new List<SalesOrder>();

    public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();

    public virtual ICollection<SalesOrder> SalesOrderOrganizations { get; set; } = new List<SalesOrder>();

    public virtual ICollection<Slasetting> Slasettings { get; set; } = new List<Slasetting>();

    public virtual ICollection<SubscriptionDowngrade> SubscriptionDowngrades { get; set; } = new List<SubscriptionDowngrade>();

    public virtual ICollection<SubscriptionItem> SubscriptionItems { get; set; } = new List<SubscriptionItem>();

    public virtual ICollection<SubscriptionRenewal> SubscriptionRenewals { get; set; } = new List<SubscriptionRenewal>();

    public virtual ICollection<SubscriptionUpgrade> SubscriptionUpgrades { get; set; } = new List<SubscriptionUpgrade>();

    public virtual ICollection<SubscriptionUsage> SubscriptionUsages { get; set; } = new List<SubscriptionUsage>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<SupportTicket> SupportTicketCompanies { get; set; } = new List<SupportTicket>();

    public virtual ICollection<SupportTicket> SupportTicketOrganizations { get; set; } = new List<SupportTicket>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<TenantModule> TenantModules { get; set; } = new List<TenantModule>();

    public virtual ICollection<TenantSetting> TenantSettings { get; set; } = new List<TenantSetting>();

    public virtual ICollection<TenantStorageLimit> TenantStorageLimits { get; set; } = new List<TenantStorageLimit>();

    public virtual ICollection<TenantUserLimit> TenantUserLimits { get; set; } = new List<TenantUserLimit>();

    public virtual ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();

    public virtual ICollection<TwilioCallLog> TwilioCallLogs { get; set; } = new List<TwilioCallLog>();

    public virtual ICollection<TwilioConfiguration> TwilioConfigurationCompanies { get; set; } = new List<TwilioConfiguration>();

    public virtual ICollection<TwilioConfiguration> TwilioConfigurationOrganizations { get; set; } = new List<TwilioConfiguration>();

    public virtual ICollection<TwilioPhoneNumber> TwilioPhoneNumbers { get; set; } = new List<TwilioPhoneNumber>();

    public virtual ICollection<TwilioSmslog> TwilioSmslogs { get; set; } = new List<TwilioSmslog>();

    public virtual ICollection<TwilioWebhookLog> TwilioWebhookLogs { get; set; } = new List<TwilioWebhookLog>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
