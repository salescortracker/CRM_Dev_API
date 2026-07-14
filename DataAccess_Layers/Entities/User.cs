using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public Guid OrganizationId { get; set; }

    public Guid? BranchId { get; set; }

    public Guid? DepartmentId { get; set; }

    public Guid? TeamId { get; set; }

    public string? EmployeeCode { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string DisplayName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? MobileNumber { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? ProfileImageUrl { get; set; }

    public Guid? DesignationId { get; set; }

    public Guid? ReportingManagerId { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public DateTime? LastLoginOn { get; set; }

    public bool IsEmailVerified { get; set; }

    public bool IsMobileVerified { get; set; }

    public byte Status { get; set; }

    public bool IsSuperAdmin { get; set; }

    public bool IsOrganizationAdmin { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual ICollection<CallRecording> CallRecordingCreatedByNavigations { get; set; } = new List<CallRecording>();

    public virtual ICollection<CallRecording> CallRecordingModifiedByNavigations { get; set; } = new List<CallRecording>();

    public virtual ICollection<CallRecording> CallRecordingUsers { get; set; } = new List<CallRecording>();

    public virtual ICollection<CallingCampaignLead> CallingCampaignLeads { get; set; } = new List<CallingCampaignLead>();

    public virtual ICollection<CallingCampaign> CallingCampaigns { get; set; } = new List<CallingCampaign>();

    public virtual ICollection<CreditNote> CreditNoteApprovedByUsers { get; set; } = new List<CreditNote>();

    public virtual ICollection<CreditNote> CreditNoteCreatedByNavigations { get; set; } = new List<CreditNote>();

    public virtual ICollection<CreditNote> CreditNoteModifiedByNavigations { get; set; } = new List<CreditNote>();

    public virtual ICollection<CreditNote> CreditNoteUsers { get; set; } = new List<CreditNote>();

    public virtual ICollection<CustomerTenant> CustomerTenantCreatedByNavigations { get; set; } = new List<CustomerTenant>();

    public virtual ICollection<CustomerTenant> CustomerTenantCustomerAdminUsers { get; set; } = new List<CustomerTenant>();

    public virtual ICollection<CustomerTenant> CustomerTenantModifiedByNavigations { get; set; } = new List<CustomerTenant>();

    public virtual ICollection<CustomerTenant> CustomerTenantUsers { get; set; } = new List<CustomerTenant>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<DataMigrationRequest> DataMigrationRequestCreatedByNavigations { get; set; } = new List<DataMigrationRequest>();

    public virtual ICollection<DataMigrationRequest> DataMigrationRequestModifiedByNavigations { get; set; } = new List<DataMigrationRequest>();

    public virtual ICollection<DataMigrationRequest> DataMigrationRequestUsers { get; set; } = new List<DataMigrationRequest>();

    public virtual Department1? Department { get; set; }

    public virtual ICollection<EmailTemplate> EmailTemplateCreatedByNavigations { get; set; } = new List<EmailTemplate>();

    public virtual ICollection<EmailTemplate> EmailTemplateModifiedByNavigations { get; set; } = new List<EmailTemplate>();

    public virtual ICollection<EmailTemplate> EmailTemplateUsers { get; set; } = new List<EmailTemplate>();

    public virtual ICollection<GoLiveChecklist> GoLiveChecklistCompletedByUsers { get; set; } = new List<GoLiveChecklist>();

    public virtual ICollection<GoLiveChecklist> GoLiveChecklistCreatedByNavigations { get; set; } = new List<GoLiveChecklist>();

    public virtual ICollection<GoLiveChecklist> GoLiveChecklistModifiedByNavigations { get; set; } = new List<GoLiveChecklist>();

    public virtual ICollection<GoLiveChecklist> GoLiveChecklistUsers { get; set; } = new List<GoLiveChecklist>();

    public virtual ICollection<Invoice> InvoiceCreatedByNavigations { get; set; } = new List<Invoice>();

    public virtual ICollection<Invoice> InvoiceGeneratedByUsers { get; set; } = new List<Invoice>();

    public virtual ICollection<InvoiceItem> InvoiceItemCreatedByNavigations { get; set; } = new List<InvoiceItem>();

    public virtual ICollection<InvoiceItem> InvoiceItemModifiedByNavigations { get; set; } = new List<InvoiceItem>();

    public virtual ICollection<InvoiceItem> InvoiceItemUsers { get; set; } = new List<InvoiceItem>();

    public virtual ICollection<Invoice> InvoiceModifiedByNavigations { get; set; } = new List<Invoice>();

    public virtual ICollection<Invoice> InvoiceUsers { get; set; } = new List<Invoice>();

    public virtual ICollection<LeadActivity> LeadActivities { get; set; } = new List<LeadActivity>();

    public virtual ICollection<Lead> LeadAssignedByUsers { get; set; } = new List<Lead>();

    public virtual ICollection<Lead> LeadAssignedToUsers { get; set; } = new List<Lead>();

    public virtual ICollection<LeadAssignment> LeadAssignmentAssignedByUsers { get; set; } = new List<LeadAssignment>();

    public virtual ICollection<LeadAssignment> LeadAssignmentAssignedToUsers { get; set; } = new List<LeadAssignment>();

    public virtual ICollection<LeadCall> LeadCalls { get; set; } = new List<LeadCall>();

    public virtual ICollection<LeadFollowUp> LeadFollowUpAssignedToUsers { get; set; } = new List<LeadFollowUp>();

    public virtual ICollection<LeadFollowUp> LeadFollowUpCompletedByUsers { get; set; } = new List<LeadFollowUp>();

    public virtual ICollection<LeadNote> LeadNotes { get; set; } = new List<LeadNote>();

    public virtual ICollection<Notification> NotificationCreatedByNavigations { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> NotificationModifiedByNavigations { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> NotificationUsers { get; set; } = new List<Notification>();

    public virtual ICollection<OnboardingProject> OnboardingProjectCreatedByNavigations { get; set; } = new List<OnboardingProject>();

    public virtual ICollection<OnboardingProject> OnboardingProjectModifiedByNavigations { get; set; } = new List<OnboardingProject>();

    public virtual ICollection<OnboardingProject> OnboardingProjectProjectManagerUsers { get; set; } = new List<OnboardingProject>();

    public virtual ICollection<OnboardingProject> OnboardingProjectUsers { get; set; } = new List<OnboardingProject>();

    public virtual ICollection<OnboardingTask> OnboardingTaskAssignedToUsers { get; set; } = new List<OnboardingTask>();

    public virtual ICollection<OnboardingTask> OnboardingTaskCreatedByNavigations { get; set; } = new List<OnboardingTask>();

    public virtual ICollection<OnboardingTask> OnboardingTaskModifiedByNavigations { get; set; } = new List<OnboardingTask>();

    public virtual ICollection<OnboardingTask> OnboardingTaskUsers { get; set; } = new List<OnboardingTask>();

    public virtual ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();

    public virtual ICollection<OpportunityActivity> OpportunityActivities { get; set; } = new List<OpportunityActivity>();

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Payment> PaymentCreatedByNavigations { get; set; } = new List<Payment>();

    public virtual ICollection<Payment> PaymentModifiedByNavigations { get; set; } = new List<Payment>();

    public virtual ICollection<Payment> PaymentReceivedByUsers { get; set; } = new List<Payment>();

    public virtual ICollection<PaymentTransaction> PaymentTransactionCreatedByNavigations { get; set; } = new List<PaymentTransaction>();

    public virtual ICollection<PaymentTransaction> PaymentTransactionModifiedByNavigations { get; set; } = new List<PaymentTransaction>();

    public virtual ICollection<PaymentTransaction> PaymentTransactionUsers { get; set; } = new List<PaymentTransaction>();

    public virtual ICollection<Payment> PaymentUsers { get; set; } = new List<Payment>();

    public virtual ICollection<QuotationApproval> QuotationApprovalApproverUsers { get; set; } = new List<QuotationApproval>();

    public virtual ICollection<QuotationApproval> QuotationApprovalCreatedByNavigations { get; set; } = new List<QuotationApproval>();

    public virtual ICollection<QuotationApproval> QuotationApprovalModifiedByNavigations { get; set; } = new List<QuotationApproval>();

    public virtual ICollection<QuotationApproval> QuotationApprovalUsers { get; set; } = new List<QuotationApproval>();

    public virtual ICollection<Quotation> QuotationCreatedByNavigations { get; set; } = new List<Quotation>();

    public virtual ICollection<Quotation> QuotationCreatedByUsers { get; set; } = new List<Quotation>();

    public virtual ICollection<QuotationItem> QuotationItemCreatedByNavigations { get; set; } = new List<QuotationItem>();

    public virtual ICollection<QuotationItem> QuotationItemModifiedByNavigations { get; set; } = new List<QuotationItem>();

    public virtual ICollection<QuotationItem> QuotationItemUsers { get; set; } = new List<QuotationItem>();

    public virtual ICollection<Quotation> QuotationModifiedByNavigations { get; set; } = new List<Quotation>();

    public virtual ICollection<Quotation> QuotationUsers { get; set; } = new List<Quotation>();

    public virtual ICollection<QuotationVersion> QuotationVersionCreatedByNavigations { get; set; } = new List<QuotationVersion>();

    public virtual ICollection<QuotationVersion> QuotationVersionCreatedByUsers { get; set; } = new List<QuotationVersion>();

    public virtual ICollection<QuotationVersion> QuotationVersionModifiedByNavigations { get; set; } = new List<QuotationVersion>();

    public virtual ICollection<QuotationVersion> QuotationVersionUsers { get; set; } = new List<QuotationVersion>();

    public virtual ICollection<Refund> RefundApprovedByUsers { get; set; } = new List<Refund>();

    public virtual ICollection<Refund> RefundCreatedByNavigations { get; set; } = new List<Refund>();

    public virtual ICollection<Refund> RefundModifiedByNavigations { get; set; } = new List<Refund>();

    public virtual ICollection<Refund> RefundUsers { get; set; } = new List<Refund>();

    public virtual ICollection<SalesOrder> SalesOrderCreatedByNavigations { get; set; } = new List<SalesOrder>();

    public virtual ICollection<SalesOrder> SalesOrderCreatedByUsers { get; set; } = new List<SalesOrder>();

    public virtual ICollection<SalesOrderItem> SalesOrderItemCreatedByNavigations { get; set; } = new List<SalesOrderItem>();

    public virtual ICollection<SalesOrderItem> SalesOrderItemModifiedByNavigations { get; set; } = new List<SalesOrderItem>();

    public virtual ICollection<SalesOrderItem> SalesOrderItemUsers { get; set; } = new List<SalesOrderItem>();

    public virtual ICollection<SalesOrder> SalesOrderModifiedByNavigations { get; set; } = new List<SalesOrder>();

    public virtual ICollection<SalesOrder> SalesOrderUsers { get; set; } = new List<SalesOrder>();

    public virtual ICollection<Slasetting> SlasettingCreatedByNavigations { get; set; } = new List<Slasetting>();

    public virtual ICollection<Slasetting> SlasettingModifiedByNavigations { get; set; } = new List<Slasetting>();

    public virtual ICollection<Slasetting> SlasettingUsers { get; set; } = new List<Slasetting>();

    public virtual ICollection<Smstemplate> SmstemplateCreatedByNavigations { get; set; } = new List<Smstemplate>();

    public virtual ICollection<Smstemplate> SmstemplateModifiedByNavigations { get; set; } = new List<Smstemplate>();

    public virtual ICollection<Smstemplate> SmstemplateUsers { get; set; } = new List<Smstemplate>();

    public virtual ICollection<Subscription> SubscriptionCreatedByNavigations { get; set; } = new List<Subscription>();

    public virtual ICollection<SubscriptionDowngrade> SubscriptionDowngradeApprovedByUsers { get; set; } = new List<SubscriptionDowngrade>();

    public virtual ICollection<SubscriptionDowngrade> SubscriptionDowngradeCreatedByNavigations { get; set; } = new List<SubscriptionDowngrade>();

    public virtual ICollection<SubscriptionDowngrade> SubscriptionDowngradeModifiedByNavigations { get; set; } = new List<SubscriptionDowngrade>();

    public virtual ICollection<SubscriptionDowngrade> SubscriptionDowngradeUsers { get; set; } = new List<SubscriptionDowngrade>();

    public virtual ICollection<SubscriptionItem> SubscriptionItemCreatedByNavigations { get; set; } = new List<SubscriptionItem>();

    public virtual ICollection<SubscriptionItem> SubscriptionItemModifiedByNavigations { get; set; } = new List<SubscriptionItem>();

    public virtual ICollection<SubscriptionItem> SubscriptionItemUsers { get; set; } = new List<SubscriptionItem>();

    public virtual ICollection<Subscription> SubscriptionModifiedByNavigations { get; set; } = new List<Subscription>();

    public virtual ICollection<SubscriptionRenewal> SubscriptionRenewalAssignedSalesUsers { get; set; } = new List<SubscriptionRenewal>();

    public virtual ICollection<SubscriptionRenewal> SubscriptionRenewalCreatedByNavigations { get; set; } = new List<SubscriptionRenewal>();

    public virtual ICollection<SubscriptionRenewal> SubscriptionRenewalModifiedByNavigations { get; set; } = new List<SubscriptionRenewal>();

    public virtual ICollection<SubscriptionRenewal> SubscriptionRenewalUsers { get; set; } = new List<SubscriptionRenewal>();

    public virtual ICollection<SubscriptionUpgrade> SubscriptionUpgradeCreatedByNavigations { get; set; } = new List<SubscriptionUpgrade>();

    public virtual ICollection<SubscriptionUpgrade> SubscriptionUpgradeModifiedByNavigations { get; set; } = new List<SubscriptionUpgrade>();

    public virtual ICollection<SubscriptionUpgrade> SubscriptionUpgradeUsers { get; set; } = new List<SubscriptionUpgrade>();

    public virtual ICollection<SubscriptionUsage> SubscriptionUsageCreatedByNavigations { get; set; } = new List<SubscriptionUsage>();

    public virtual ICollection<SubscriptionUsage> SubscriptionUsageModifiedByNavigations { get; set; } = new List<SubscriptionUsage>();

    public virtual ICollection<SubscriptionUsage> SubscriptionUsageUsers { get; set; } = new List<SubscriptionUsage>();

    public virtual ICollection<Subscription> SubscriptionUsers { get; set; } = new List<Subscription>();

    public virtual ICollection<SupportTicket> SupportTicketAssignedToUsers { get; set; } = new List<SupportTicket>();

    public virtual ICollection<SupportTicket> SupportTicketCreatedByNavigations { get; set; } = new List<SupportTicket>();

    public virtual ICollection<SupportTicket> SupportTicketCreatedByUsers { get; set; } = new List<SupportTicket>();

    public virtual ICollection<SupportTicket> SupportTicketModifiedByNavigations { get; set; } = new List<SupportTicket>();

    public virtual ICollection<SupportTicket> SupportTicketUsers { get; set; } = new List<SupportTicket>();

    public virtual Team? Team { get; set; }

    public virtual ICollection<TenantModule> TenantModuleCreatedByNavigations { get; set; } = new List<TenantModule>();

    public virtual ICollection<TenantModule> TenantModuleModifiedByNavigations { get; set; } = new List<TenantModule>();

    public virtual ICollection<TenantModule> TenantModuleUsers { get; set; } = new List<TenantModule>();

    public virtual ICollection<TenantSetting> TenantSettingCreatedByNavigations { get; set; } = new List<TenantSetting>();

    public virtual ICollection<TenantSetting> TenantSettingModifiedByNavigations { get; set; } = new List<TenantSetting>();

    public virtual ICollection<TenantSetting> TenantSettingUsers { get; set; } = new List<TenantSetting>();

    public virtual ICollection<TenantStorageLimit> TenantStorageLimitCreatedByNavigations { get; set; } = new List<TenantStorageLimit>();

    public virtual ICollection<TenantStorageLimit> TenantStorageLimitModifiedByNavigations { get; set; } = new List<TenantStorageLimit>();

    public virtual ICollection<TenantStorageLimit> TenantStorageLimitUsers { get; set; } = new List<TenantStorageLimit>();

    public virtual ICollection<TenantUserLimit> TenantUserLimitCreatedByNavigations { get; set; } = new List<TenantUserLimit>();

    public virtual ICollection<TenantUserLimit> TenantUserLimitModifiedByNavigations { get; set; } = new List<TenantUserLimit>();

    public virtual ICollection<TenantUserLimit> TenantUserLimitUsers { get; set; } = new List<TenantUserLimit>();

    public virtual ICollection<TicketAttachment> TicketAttachmentCreatedByNavigations { get; set; } = new List<TicketAttachment>();

    public virtual ICollection<TicketAttachment> TicketAttachmentModifiedByNavigations { get; set; } = new List<TicketAttachment>();

    public virtual ICollection<TicketAttachment> TicketAttachmentUploadedByUsers { get; set; } = new List<TicketAttachment>();

    public virtual ICollection<TicketAttachment> TicketAttachmentUsers { get; set; } = new List<TicketAttachment>();

    public virtual ICollection<TicketComment> TicketCommentCommentedByUsers { get; set; } = new List<TicketComment>();

    public virtual ICollection<TicketComment> TicketCommentCreatedByNavigations { get; set; } = new List<TicketComment>();

    public virtual ICollection<TicketComment> TicketCommentModifiedByNavigations { get; set; } = new List<TicketComment>();

    public virtual ICollection<TicketComment> TicketCommentUsers { get; set; } = new List<TicketComment>();

    public virtual ICollection<TrainingSession> TrainingSessionCreatedByNavigations { get; set; } = new List<TrainingSession>();

    public virtual ICollection<TrainingSession> TrainingSessionModifiedByNavigations { get; set; } = new List<TrainingSession>();

    public virtual ICollection<TrainingSession> TrainingSessionTrainerUsers { get; set; } = new List<TrainingSession>();

    public virtual ICollection<TrainingSession> TrainingSessionUsers { get; set; } = new List<TrainingSession>();

    public virtual ICollection<TwilioCallLog> TwilioCallLogCreatedByNavigations { get; set; } = new List<TwilioCallLog>();

    public virtual ICollection<TwilioCallLog> TwilioCallLogModifiedByNavigations { get; set; } = new List<TwilioCallLog>();

    public virtual ICollection<TwilioCallLog> TwilioCallLogUsers { get; set; } = new List<TwilioCallLog>();

    public virtual ICollection<TwilioConfiguration> TwilioConfigurationCreatedByNavigations { get; set; } = new List<TwilioConfiguration>();

    public virtual ICollection<TwilioConfiguration> TwilioConfigurationModifiedByNavigations { get; set; } = new List<TwilioConfiguration>();

    public virtual ICollection<TwilioConfiguration> TwilioConfigurationUsers { get; set; } = new List<TwilioConfiguration>();

    public virtual ICollection<TwilioPhoneNumber> TwilioPhoneNumberAssignedToUsers { get; set; } = new List<TwilioPhoneNumber>();

    public virtual ICollection<TwilioPhoneNumber> TwilioPhoneNumberCreatedByNavigations { get; set; } = new List<TwilioPhoneNumber>();

    public virtual ICollection<TwilioPhoneNumber> TwilioPhoneNumberModifiedByNavigations { get; set; } = new List<TwilioPhoneNumber>();

    public virtual ICollection<TwilioPhoneNumber> TwilioPhoneNumberUsers { get; set; } = new List<TwilioPhoneNumber>();

    public virtual ICollection<TwilioSmslog> TwilioSmslogCreatedByNavigations { get; set; } = new List<TwilioSmslog>();

    public virtual ICollection<TwilioSmslog> TwilioSmslogModifiedByNavigations { get; set; } = new List<TwilioSmslog>();

    public virtual ICollection<TwilioSmslog> TwilioSmslogUsers { get; set; } = new List<TwilioSmslog>();

    public virtual ICollection<TwilioWebhookLog> TwilioWebhookLogCreatedByNavigations { get; set; } = new List<TwilioWebhookLog>();

    public virtual ICollection<TwilioWebhookLog> TwilioWebhookLogModifiedByNavigations { get; set; } = new List<TwilioWebhookLog>();

    public virtual ICollection<TwilioWebhookLog> TwilioWebhookLogUsers { get; set; } = new List<TwilioWebhookLog>();

    public virtual ICollection<UserRole> UserRoleAssignedByNavigations { get; set; } = new List<UserRole>();

    public virtual ICollection<UserRole> UserRoleUsers { get; set; } = new List<UserRole>();

    public virtual ICollection<WhatsAppTemplate> WhatsAppTemplateCreatedByNavigations { get; set; } = new List<WhatsAppTemplate>();

    public virtual ICollection<WhatsAppTemplate> WhatsAppTemplateModifiedByNavigations { get; set; } = new List<WhatsAppTemplate>();

    public virtual ICollection<WhatsAppTemplate> WhatsAppTemplateUsers { get; set; } = new List<WhatsAppTemplate>();
}
