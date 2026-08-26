using System;
using System.Collections.Generic;
using DataAccess_Layers.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess_Layers.Data;

public partial class CRMContext : DbContext
{
    public CRMContext()
    {
    }

    public CRMContext(DbContextOptions<CRMContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityType> ActivityTypes { get; set; }

    public virtual DbSet<AddOn> AddOns { get; set; }

    public virtual DbSet<AddOnPricing> AddOnPricings { get; set; }

    public virtual DbSet<ApplicationLog> ApplicationLogs { get; set; }

    public virtual DbSet<ApprovalWorkflow> ApprovalWorkflows { get; set; }

    public virtual DbSet<ApprovalWorkflowLevel> ApprovalWorkflowLevels { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<AutoAssignmentCondition> AutoAssignmentConditions { get; set; }

    public virtual DbSet<AutoAssignmentRule> AutoAssignmentRules { get; set; }

    public virtual DbSet<BackupFrequency> BackupFrequencies { get; set; }

    public virtual DbSet<BillingCycle> BillingCycles { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<CallOutcome> CallOutcomes { get; set; }

    public virtual DbSet<CallPurpose> CallPurposes { get; set; }

    public virtual DbSet<CallRecording> CallRecordings { get; set; }

    public virtual DbSet<CallType> CallTypes { get; set; }

    public virtual DbSet<CallingCampaign> CallingCampaigns { get; set; }

    public virtual DbSet<CallingCampaignLead> CallingCampaignLeads { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyInformation> CompanyInformations { get; set; }

    public virtual DbSet<CompanyStatusMaster> CompanyStatusMasters { get; set; }

    public virtual DbSet<CompanyType> CompanyTypes { get; set; }

    public virtual DbSet<ContactInformation> ContactInformations { get; set; }

    public virtual DbSet<ContactType> ContactTypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CreditNote> CreditNotes { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAddOn> CustomerAddOns { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<CustomerBillingDetail> CustomerBillingDetails { get; set; }

    public virtual DbSet<CustomerContact> CustomerContacts { get; set; }

    public virtual DbSet<CustomerTenant> CustomerTenants { get; set; }

    public virtual DbSet<DataMigrationRequest> DataMigrationRequests { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Department1> Departments1 { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<DiscountType> DiscountTypes { get; set; }

    public virtual DbSet<EmailAutomation> EmailAutomations { get; set; }

    public virtual DbSet<EmailAutomationRecipient> EmailAutomationRecipients { get; set; }

    public virtual DbSet<EmailCategory> EmailCategories { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<EmailType> EmailTypes { get; set; }

    public virtual DbSet<EmailsTemplate> EmailsTemplates { get; set; }

    public virtual DbSet<EscalationRule> EscalationRules { get; set; }

    public virtual DbSet<FiscalType> FiscalTypes { get; set; }

    public virtual DbSet<GoLiveChecklist> GoLiveChecklists { get; set; }

    public virtual DbSet<Industry> Industries { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<Lead> Leads { get; set; }

    public virtual DbSet<LeadActivity> LeadActivities { get; set; }

    public virtual DbSet<LeadAssignment> LeadAssignments { get; set; }

    public virtual DbSet<LeadCall> LeadCalls { get; set; }

    public virtual DbSet<LeadFollowUp> LeadFollowUps { get; set; }

    public virtual DbSet<LeadInformation> LeadInformations { get; set; }

    public virtual DbSet<LeadNote> LeadNotes { get; set; }

    public virtual DbSet<LeadSource> LeadSources { get; set; }

    public virtual DbSet<LeadSourceDatum> LeadSourceData { get; set; }

    public virtual DbSet<LeadStatus> LeadStatuses { get; set; }

    public virtual DbSet<LeadStatusDatum> LeadStatusData { get; set; }

    public virtual DbSet<LeadType> LeadTypes { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    public virtual DbSet<MeetingPurpose> MeetingPurposes { get; set; }

    public virtual DbSet<MenuMaster> MenuMasters { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationMaster> NotificationMasters { get; set; }

    public virtual DbSet<OnboardingProject> OnboardingProjects { get; set; }

    public virtual DbSet<OnboardingTask> OnboardingTasks { get; set; }

    public virtual DbSet<Opportunity> Opportunities { get; set; }

    public virtual DbSet<OpportunityActivity> OpportunityActivities { get; set; }

    public virtual DbSet<OpportunityProduct> OpportunityProducts { get; set; }

    public virtual DbSet<OpportunityStage> OpportunityStages { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrganizationDatum> OrganizationData { get; set; }

    public virtual DbSet<OrganizationSetting> OrganizationSettings { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PlanFeature> PlanFeatures { get; set; }

    public virtual DbSet<PlanModule> PlanModules { get; set; }

    public virtual DbSet<PlanPricing> PlanPricings { get; set; }

    public virtual DbSet<PlanStorageLimit> PlanStorageLimits { get; set; }

    public virtual DbSet<PlanUserLimit> PlanUserLimits { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Quotation> Quotations { get; set; }

    public virtual DbSet<QuotationApproval> QuotationApprovals { get; set; }

    public virtual DbSet<QuotationItem> QuotationItems { get; set; }

    public virtual DbSet<QuotationVersion> QuotationVersions { get; set; }

    public virtual DbSet<Refund> Refunds { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Relationship> Relationships { get; set; }

    public virtual DbSet<RetentionPeriod> RetentionPeriods { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Role1> Roles1 { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<RolesPermission> RolesPermissions { get; set; }

    public virtual DbSet<SalesOrder> SalesOrders { get; set; }

    public virtual DbSet<SalesOrderItem> SalesOrderItems { get; set; }

    public virtual DbSet<ScheduledJob> ScheduledJobs { get; set; }

    public virtual DbSet<Slarule> Slarules { get; set; }

    public virtual DbSet<Slasetting> Slasettings { get; set; }

    public virtual DbSet<Smstemplate> Smstemplates { get; set; }

    public virtual DbSet<StateMaster> StateMasters { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<SubscriptionDowngrade> SubscriptionDowngrades { get; set; }

    public virtual DbSet<SubscriptionItem> SubscriptionItems { get; set; }

    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public virtual DbSet<SubscriptionPlanMaster> SubscriptionPlanMasters { get; set; }

    public virtual DbSet<SubscriptionRenewal> SubscriptionRenewals { get; set; }

    public virtual DbSet<SubscriptionUpgrade> SubscriptionUpgrades { get; set; }

    public virtual DbSet<SubscriptionUsage> SubscriptionUsages { get; set; }

    public virtual DbSet<SupportTicket> SupportTickets { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TenantModule> TenantModules { get; set; }

    public virtual DbSet<TenantSetting> TenantSettings { get; set; }

    public virtual DbSet<TenantStorageLimit> TenantStorageLimits { get; set; }

    public virtual DbSet<TenantUserLimit> TenantUserLimits { get; set; }

    public virtual DbSet<TicketAttachment> TicketAttachments { get; set; }

    public virtual DbSet<TicketComment> TicketComments { get; set; }

    public virtual DbSet<TrainingSession> TrainingSessions { get; set; }

    public virtual DbSet<TwilioCallLog> TwilioCallLogs { get; set; }

    public virtual DbSet<TwilioConfiguration> TwilioConfigurations { get; set; }

    public virtual DbSet<TwilioPhoneNumber> TwilioPhoneNumbers { get; set; }

    public virtual DbSet<TwilioSmslog> TwilioSmslogs { get; set; }

    public virtual DbSet<TwilioWebhookLog> TwilioWebhookLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<WhatsAppTemplate> WhatsAppTemplates { get; set; }

    public virtual DbSet<WorkflowRule> WorkflowRules { get; set; }

    public virtual DbSet<WorkflowRuleAction> WorkflowRuleActions { get; set; }

    public virtual DbSet<WorkflowRuleCondition> WorkflowRuleConditions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.29.53,50491;Database=CRM_Dev;User Id=sa;Password=CtDev@2026@01;TrustServerCertificate=True;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityType>(entity =>
        {
            entity.HasKey(e => e.ActivityTypeId).HasName("PK__Activity__95CEDE6E0C907985");

            entity.ToTable("ActivityType", "Masters");

            entity.Property(e => e.ActivityTypeId).HasColumnName("ActivityTypeID");
            entity.Property(e => e.ActivityTypeCode).HasMaxLength(150);
            entity.Property(e => e.ActivityTypeName).HasMaxLength(150);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<AddOn>(entity =>
        {
            entity.HasKey(e => e.AddOnId).HasName("PK_CRM_AddOns");

            entity.ToTable("AddOns", "CRM");

            entity.HasIndex(e => e.AddOnCode, "IX_CRM_AddOns_AddOnCode");

            entity.HasIndex(e => e.AddOnName, "IX_CRM_AddOns_AddOnName");

            entity.HasIndex(e => e.AddOnType, "IX_CRM_AddOns_AddOnType");

            entity.HasIndex(e => e.IsRecurring, "IX_CRM_AddOns_IsRecurring");

            entity.HasIndex(e => e.ProductId, "IX_CRM_AddOns_ProductId");

            entity.HasIndex(e => e.Status, "IX_CRM_AddOns_Status");

            entity.HasIndex(e => new { e.ProductId, e.AddOnCode }, "UQ_CRM_AddOns_Product_AddOnCode").IsUnique();

            entity.HasIndex(e => new { e.ProductId, e.AddOnName }, "UQ_CRM_AddOns_Product_AddOnName").IsUnique();

            entity.Property(e => e.AddOnId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AddOnCode).HasMaxLength(50);
            entity.Property(e => e.AddOnName).HasMaxLength(150);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Product).WithMany(p => p.AddOns)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_AddOns_Products");
        });

        modelBuilder.Entity<AddOnPricing>(entity =>
        {
            entity.HasKey(e => e.AddOnPricingId).HasName("PK_CRM_AddOnPricing");

            entity.ToTable("AddOnPricing", "CRM");

            entity.HasIndex(e => e.AddOnId, "IX_CRM_AddOnPricing_AddOnId");

            entity.HasIndex(e => e.BillingCycle, "IX_CRM_AddOnPricing_BillingCycle");

            entity.HasIndex(e => e.CurrencyCode, "IX_CRM_AddOnPricing_CurrencyCode");

            entity.HasIndex(e => e.EffectiveFrom, "IX_CRM_AddOnPricing_EffectiveFrom");

            entity.HasIndex(e => e.Status, "IX_CRM_AddOnPricing_Status");

            entity.HasIndex(e => e.UnitPrice, "IX_CRM_AddOnPricing_UnitPrice");

            entity.HasIndex(e => e.UnitType, "IX_CRM_AddOnPricing_UnitType");

            entity.HasIndex(e => new { e.AddOnId, e.CurrencyCode, e.UnitType, e.BillingCycle, e.EffectiveFrom }, "UQ_CRM_AddOnPricing").IsUnique();

            entity.Property(e => e.AddOnPricingId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CurrencyCode).HasMaxLength(10);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.TaxPercentage)
                .HasDefaultValue(18.00m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AddOn).WithMany(p => p.AddOnPricings)
                .HasForeignKey(d => d.AddOnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_AddOnPricing_AddOns");
        });

        modelBuilder.Entity<ApplicationLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Applicat__5E548648AFCF7092");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LogLevel).HasMaxLength(50);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Source).HasMaxLength(500);
        });

        modelBuilder.Entity<ApprovalWorkflow>(entity =>
        {
            entity.ToTable("ApprovalWorkflows", "Superadmin");

            entity.Property(e => e.ApprovalWorkflowId).HasColumnName("ApprovalWorkflowID");
            entity.Property(e => e.ApprovalLevels).HasDefaultValue(1);
            entity.Property(e => e.ApprovalType).HasMaxLength(50);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.FinalApprovalAction).HasMaxLength(100);
            entity.Property(e => e.FinalRejectionAction).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModuleName).HasMaxLength(100);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.WorkflowName).HasMaxLength(200);
        });

        modelBuilder.Entity<ApprovalWorkflowLevel>(entity =>
        {
            entity.ToTable("ApprovalWorkflowLevels", "Superadmin");

            entity.Property(e => e.ApprovalWorkflowLevelId).HasColumnName("ApprovalWorkflowLevelID");
            entity.Property(e => e.ApprovalCondition).HasMaxLength(500);
            entity.Property(e => e.ApprovalWorkflowId).HasColumnName("ApprovalWorkflowID");
            entity.Property(e => e.ApproverRoleId).HasColumnName("ApproverRoleID");
            entity.Property(e => e.ApproverType).HasMaxLength(50);
            entity.Property(e => e.ApproverUserId).HasColumnName("ApproverUserID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.OnApprovalAction).HasMaxLength(100);
            entity.Property(e => e.OnRejectionAction).HasMaxLength(100);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");

            entity.HasOne(d => d.ApprovalWorkflow).WithMany(p => p.ApprovalWorkflowLevels)
                .HasForeignKey(d => d.ApprovalWorkflowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApprovalWorkflowLevels_ApprovalWorkflows");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId);

            entity.ToTable("AuditLogs", "Master");

            entity.Property(e => e.ActionType).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.TableName).HasMaxLength(100);
        });

        modelBuilder.Entity<AutoAssignmentCondition>(entity =>
        {
            entity.ToTable("AutoAssignmentConditions", "Superadmin");

            entity.Property(e => e.AutoAssignmentConditionId).HasColumnName("AutoAssignmentConditionID");
            entity.Property(e => e.AutoAssignmentRuleId).HasColumnName("AutoAssignmentRuleID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FieldName).HasMaxLength(150);
            entity.Property(e => e.FieldValue).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LogicalOperator).HasMaxLength(10);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Operator).HasMaxLength(50);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");

            entity.HasOne(d => d.AutoAssignmentRule).WithMany(p => p.AutoAssignmentConditions)
                .HasForeignKey(d => d.AutoAssignmentRuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AutoAssignmentConditions_AutoAssignmentRules");
        });

        modelBuilder.Entity<AutoAssignmentRule>(entity =>
        {
            entity.ToTable("AutoAssignmentRules", "Superadmin");

            entity.Property(e => e.AutoAssignmentRuleId).HasColumnName("AutoAssignmentRuleID");
            entity.Property(e => e.AssignmentMethod).HasMaxLength(50);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModuleName).HasMaxLength(100);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.RuleName).HasMaxLength(200);
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<BackupFrequency>(entity =>
        {
            entity.HasKey(e => e.BackupFrequencyId).HasName("PK__BackupFr__059CA931A8CC62CE");

            entity.ToTable("BackupFrequency", "Masters");

            entity.Property(e => e.BackupFrequencyId).HasColumnName("BackupFrequencyID");
            entity.Property(e => e.BackupFrequencyCode).HasMaxLength(150);
            entity.Property(e => e.BackupFrequencyName).HasMaxLength(150);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<BillingCycle>(entity =>
        {
            entity.HasKey(e => e.BillingCycleId).HasName("PK__BillingC__E471AF20374381CB");

            entity.ToTable("BillingCycle", "Masters");

            entity.Property(e => e.BillingCycleId).HasColumnName("BillingCycleID");
            entity.Property(e => e.BillingCycleCode).HasMaxLength(150);
            entity.Property(e => e.BillingCycleName).HasMaxLength(150);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PK_Masters_Branches");

            entity.ToTable("Branches", "Masters");

            entity.HasIndex(e => new { e.OrganizationId, e.BranchCode }, "UQ_Masters_Branches_OrganizationId_BranchCode").IsUnique();

            entity.Property(e => e.BranchId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AddressLine1).HasMaxLength(250);
            entity.Property(e => e.BranchCode).HasMaxLength(50);
            entity.Property(e => e.BranchName).HasMaxLength(150);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Organization).WithMany(p => p.Branches)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Masters_Branches_Organizations");
        });

        modelBuilder.Entity<CallOutcome>(entity =>
        {
            entity.HasKey(e => e.CallOutcomesId).HasName("PK__CallOutc__542E7D8D3C89AF37");

            entity.ToTable("CallOutcomes", "Masters");

            entity.Property(e => e.CallOutcomesId).HasColumnName("CallOutcomesID");
            entity.Property(e => e.CallOutcomesCode).HasMaxLength(150);
            entity.Property(e => e.CallOutcomesName).HasMaxLength(150);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<CallPurpose>(entity =>
        {
            entity.HasKey(e => e.CallPurposesId).HasName("PK__CallPurp__6EB41391F090860B");

            entity.ToTable("CallPurposes", "Masters");

            entity.Property(e => e.CallPurposesId).HasColumnName("CallPurposesID");
            entity.Property(e => e.CallPurposesCode).HasMaxLength(150);
            entity.Property(e => e.CallPurposesName).HasMaxLength(150);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<CallRecording>(entity =>
        {
            entity.HasKey(e => e.CallRecordingId).HasName("PK_CRM_CallRecordings");

            entity.ToTable("CallRecordings", "CRM");

            entity.HasIndex(e => e.LeadCallId, "IX_CRM_CallRecordings_LeadCallId");

            entity.HasIndex(e => e.RecordingStatus, "IX_CRM_CallRecordings_RecordingStatus");

            entity.HasIndex(e => e.TwilioCallLogId, "IX_CRM_CallRecordings_TwilioCallLogId");

            entity.HasIndex(e => e.RecordingSid, "UX_CRM_CallRecordings_RecordingSid").IsUnique();

            entity.Property(e => e.CallRecordingId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.RecordingSid).HasMaxLength(100);
            entity.Property(e => e.RecordingUrl).HasMaxLength(1000);

            entity.HasOne(d => d.Company).WithMany(p => p.CallRecordings)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CallRecordings_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CallRecordingCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CallRecordings_CreatedBy");

            entity.HasOne(d => d.LeadCall).WithMany(p => p.CallRecordings)
                .HasForeignKey(d => d.LeadCallId)
                .HasConstraintName("FK_CRM_CallRecordings_LeadCall");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.CallRecordingModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_CallRecordings_ModifiedBy");

            entity.HasOne(d => d.TwilioCallLog).WithMany(p => p.CallRecordings)
                .HasForeignKey(d => d.TwilioCallLogId)
                .HasConstraintName("FK_CRM_CallRecordings_TwilioCallLog");

            entity.HasOne(d => d.User).WithMany(p => p.CallRecordingUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CallRecordings_User");
        });

        modelBuilder.Entity<CallType>(entity =>
        {
            entity.HasKey(e => e.CallTypesId).HasName("PK__CallType__9A26F3845B00609E");

            entity.ToTable("CallTypes", "Masters");

            entity.Property(e => e.CallTypesId).HasColumnName("CallTypesID");
            entity.Property(e => e.CallTypesCode).HasMaxLength(150);
            entity.Property(e => e.CallTypesName).HasMaxLength(150);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<CallingCampaign>(entity =>
        {
            entity.HasKey(e => e.CallingCampaignId).HasName("PK_CRM_CallingCampaigns");

            entity.ToTable("CallingCampaigns", "CRM");

            entity.HasIndex(e => e.AssignedTeamId, "IX_CRM_CallingCampaigns_AssignedTeamId");

            entity.HasIndex(e => e.AssignedUserId, "IX_CRM_CallingCampaigns_AssignedUserId");

            entity.HasIndex(e => e.CampaignCode, "IX_CRM_CallingCampaigns_CampaignCode");

            entity.HasIndex(e => e.CampaignName, "IX_CRM_CallingCampaigns_CampaignName");

            entity.HasIndex(e => e.EndDate, "IX_CRM_CallingCampaigns_EndDate");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_CallingCampaigns_OrganizationId");

            entity.HasIndex(e => e.StartDate, "IX_CRM_CallingCampaigns_StartDate");

            entity.HasIndex(e => e.Status, "IX_CRM_CallingCampaigns_Status");

            entity.HasIndex(e => new { e.OrganizationId, e.CampaignCode }, "UQ_CRM_CallingCampaigns_Organization_CampaignCode").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.CampaignName }, "UQ_CRM_CallingCampaigns_Organization_CampaignName").IsUnique();

            entity.Property(e => e.CallingCampaignId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CampaignCode).HasMaxLength(50);
            entity.Property(e => e.CampaignName).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.AssignedTeam).WithMany(p => p.CallingCampaigns)
                .HasForeignKey(d => d.AssignedTeamId)
                .HasConstraintName("FK_CRM_CallingCampaigns_Teams");

            entity.HasOne(d => d.AssignedUser).WithMany(p => p.CallingCampaigns)
                .HasForeignKey(d => d.AssignedUserId)
                .HasConstraintName("FK_CRM_CallingCampaigns_Users");

            entity.HasOne(d => d.Organization).WithMany(p => p.CallingCampaigns)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CallingCampaigns_Organizations");
        });

        modelBuilder.Entity<CallingCampaignLead>(entity =>
        {
            entity.HasKey(e => e.CallingCampaignLeadId).HasName("PK_CRM_CallingCampaignLeads");

            entity.ToTable("CallingCampaignLeads", "CRM");

            entity.HasIndex(e => e.AssignedToUserId, "IX_CRM_CallingCampaignLeads_AssignedToUserId");

            entity.HasIndex(e => e.CallingCampaignId, "IX_CRM_CallingCampaignLeads_CallingCampaignId");

            entity.HasIndex(e => e.LastAttemptOn, "IX_CRM_CallingCampaignLeads_LastAttemptOn");

            entity.HasIndex(e => e.LeadId, "IX_CRM_CallingCampaignLeads_LeadId");

            entity.HasIndex(e => e.NextAttemptOn, "IX_CRM_CallingCampaignLeads_NextAttemptOn");

            entity.HasIndex(e => e.QueueOrder, "IX_CRM_CallingCampaignLeads_QueueOrder");

            entity.HasIndex(e => e.Status, "IX_CRM_CallingCampaignLeads_Status");

            entity.HasIndex(e => new { e.CallingCampaignId, e.LeadId }, "UQ_CRM_CallingCampaignLeads_Campaign_Lead").IsUnique();

            entity.Property(e => e.CallingCampaignLeadId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.QueueOrder).HasDefaultValue(1);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.CallingCampaignLeads)
                .HasForeignKey(d => d.AssignedToUserId)
                .HasConstraintName("FK_CRM_CallingCampaignLeads_AssignedToUser");

            entity.HasOne(d => d.CallingCampaign).WithMany(p => p.CallingCampaignLeads)
                .HasForeignKey(d => d.CallingCampaignId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CallingCampaignLeads_CallingCampaigns");

            entity.HasOne(d => d.Lead).WithMany(p => p.CallingCampaignLeads)
                .HasForeignKey(d => d.LeadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CallingCampaignLeads_Leads");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__2D971C4C22DA527E");

            entity.ToTable("Company", "Security");

            entity.HasIndex(e => e.CompanyCode, "UQ__Company__11A0134BC69D0B04").IsUnique();

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CompanyAddress)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CompanyCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CompanyContact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CompanyEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Headquarters)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IndustryType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDefault).HasColumnName("isDefault");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.PlanStartDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<CompanyInformation>(entity =>
        {
            entity.ToTable("CompanyInformation", "CRM");

            entity.Property(e => e.CompanyInformationId).HasColumnName("CompanyInformationID");
            entity.Property(e => e.AddressLine1).HasMaxLength(300);
            entity.Property(e => e.AddressLine2).HasMaxLength(300);
            entity.Property(e => e.AnnualRevenue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CinregistrationNumber)
                .HasMaxLength(100)
                .HasColumnName("CINRegistrationNumber");
            entity.Property(e => e.City).HasMaxLength(150);
            entity.Property(e => e.CompanyEmail).HasMaxLength(255);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CompanyName).HasMaxLength(200);
            entity.Property(e => e.CompanyOwner).HasMaxLength(150);
            entity.Property(e => e.CompanyPhone).HasMaxLength(30);
            entity.Property(e => e.CompanyStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
            entity.Property(e => e.CompanyTypeId).HasColumnName("CompanyTypeID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Gstnumber)
                .HasMaxLength(50)
                .HasColumnName("GSTNumber");
            entity.Property(e => e.IndustryId).HasColumnName("IndustryID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LegalCompanyName).HasMaxLength(250);
            entity.Property(e => e.LinkedInCompanyUrl)
                .HasMaxLength(500)
                .HasColumnName("LinkedInCompanyURL");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.NumberOfEmployees).HasMaxLength(50);
            entity.Property(e => e.Pannumber)
                .HasMaxLength(50)
                .HasColumnName("PANNumber");
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.PrimaryContactDesignation).HasMaxLength(150);
            entity.Property(e => e.PrimaryContactEmail).HasMaxLength(255);
            entity.Property(e => e.PrimaryContactName).HasMaxLength(150);
            entity.Property(e => e.PrimaryContactPhone).HasMaxLength(30);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.Website).HasMaxLength(500);

            entity.HasOne(d => d.CompanyType).WithMany(p => p.CompanyInformations)
                .HasForeignKey(d => d.CompanyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyInformation_CompanyType");

            entity.HasOne(d => d.Country).WithMany(p => p.CompanyInformations)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyInformation_Country");

            entity.HasOne(d => d.Industry).WithMany(p => p.CompanyInformations)
                .HasForeignKey(d => d.IndustryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyInformation_Industry");

            entity.HasOne(d => d.State).WithMany(p => p.CompanyInformations)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyInformation_State");
        });

        modelBuilder.Entity<CompanyStatusMaster>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__CompanyS__C8EE204319D3B7FD");

            entity.ToTable("CompanyStatusMaster", "company");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CompanyType>(entity =>
        {
            entity.HasKey(e => e.CompanyTypeId).HasName("PK__CompanyT__060199385177EC0E");

            entity.ToTable("CompanyType", "Masters");

            entity.Property(e => e.CompanyTypeId).HasColumnName("CompanyTypeID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CompanyTypeCode).HasMaxLength(150);
            entity.Property(e => e.CompanyTypeName).HasMaxLength(150);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<ContactInformation>(entity =>
        {
            entity.ToTable("ContactInformation", "CRM");

            entity.Property(e => e.ContactInformationId).HasColumnName("ContactInformationID");
            entity.Property(e => e.AddressLine1).HasMaxLength(300);
            entity.Property(e => e.AddressLine2).HasMaxLength(300);
            entity.Property(e => e.AlternatePhone).HasMaxLength(30);
            entity.Property(e => e.BusinessEmail).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(150);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CompanyInformationId).HasColumnName("CompanyInformationID");
            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.ContactTypeId).HasColumnName("ContactTypeID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Department).HasMaxLength(150);
            entity.Property(e => e.Designation).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.RelationshipId).HasColumnName("RelationshipID");
            entity.Property(e => e.Salutation).HasMaxLength(100);
            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.Website).HasMaxLength(500);

            entity.HasOne(d => d.CompanyInformation).WithMany(p => p.ContactInformations)
                .HasForeignKey(d => d.CompanyInformationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContactInformation_CompanyInformation");

            entity.HasOne(d => d.ContactType).WithMany(p => p.ContactInformations)
                .HasForeignKey(d => d.ContactTypeId)
                .HasConstraintName("FK_ContactInformation_ContactType");

            entity.HasOne(d => d.Country).WithMany(p => p.ContactInformations)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_ContactInformation_Country");

            entity.HasOne(d => d.Relationship).WithMany(p => p.ContactInformations)
                .HasForeignKey(d => d.RelationshipId)
                .HasConstraintName("FK_ContactInformation_Relationship");

            entity.HasOne(d => d.State).WithMany(p => p.ContactInformations)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_ContactInformation_State");
        });

        modelBuilder.Entity<ContactType>(entity =>
        {
            entity.HasKey(e => e.ContactTypeId).HasName("PK__ContactT__17E1EE7289774AD1");

            entity.ToTable("ContactType", "Masters");

            entity.Property(e => e.ContactTypeId).HasColumnName("ContactTypeID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.ContactTypeCode).HasMaxLength(150);
            entity.Property(e => e.ContactTypeName).HasMaxLength(150);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Country__10D160BFCB7B834C");

            entity.ToTable("Country", "Masters");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CountryCode).HasMaxLength(150);
            entity.Property(e => e.CountryName).HasMaxLength(150);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<CreditNote>(entity =>
        {
            entity.HasKey(e => e.CreditNoteId).HasName("PK_CRM_CreditNotes");

            entity.ToTable("CreditNotes", "CRM");

            entity.HasIndex(e => e.ApprovedByUserId, "IX_CRM_CreditNotes_ApprovedByUserId");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_CreditNotes_CompanyId");

            entity.HasIndex(e => e.CreditNoteDate, "IX_CRM_CreditNotes_CreditNoteDate");

            entity.HasIndex(e => e.CreditNoteNumber, "IX_CRM_CreditNotes_CreditNoteNumber");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_CreditNotes_CustomerId");

            entity.HasIndex(e => e.InvoiceId, "IX_CRM_CreditNotes_InvoiceId");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_CreditNotes_OrganizationId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_CreditNotes_RegionId");

            entity.HasIndex(e => e.Status, "IX_CRM_CreditNotes_Status");

            entity.HasIndex(e => e.UserId, "IX_CRM_CreditNotes_UserId");

            entity.HasIndex(e => new { e.OrganizationId, e.CreditNoteNumber }, "UQ_CRM_CreditNotes").IsUnique();

            entity.Property(e => e.CreditNoteId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CreditNoteNumber).HasMaxLength(50);
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ApprovedByUser).WithMany(p => p.CreditNoteApprovedByUsers)
                .HasForeignKey(d => d.ApprovedByUserId)
                .HasConstraintName("FK_CRM_CreditNotes_ApprovedByUser");

            entity.HasOne(d => d.Company).WithMany(p => p.CreditNoteCompanies)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CreditNotes_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CreditNoteCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CreditNotes_CreatedBy");

            entity.HasOne(d => d.Customer).WithMany(p => p.CreditNotes)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CreditNotes_Customer");

            entity.HasOne(d => d.Invoice).WithMany(p => p.CreditNotes)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CreditNotes_Invoice");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.CreditNoteModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_CreditNotes_ModifiedBy");

            entity.HasOne(d => d.Organization).WithMany(p => p.CreditNoteOrganizations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CreditNotes_Organization");

            entity.HasOne(d => d.User).WithMany(p => p.CreditNoteUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CreditNotes_User");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyId).HasName("PK__Currency__14470B100311A801");

            entity.ToTable("Currency", "Masters");

            entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrencyCode).HasMaxLength(150);
            entity.Property(e => e.CurrencyName).HasMaxLength(150);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_CRM_Customers");

            entity.ToTable("Customers", "CRM");

            entity.HasIndex(e => e.AccountManagerUserId, "IX_CRM_Customers_AccountManagerUserId");

            entity.HasIndex(e => e.CompanyName, "IX_CRM_Customers_CompanyName");

            entity.HasIndex(e => e.CustomerCode, "IX_CRM_Customers_CustomerCode");

            entity.HasIndex(e => e.CustomerStatus, "IX_CRM_Customers_CustomerStatus");

            entity.HasIndex(e => e.OnboardedOn, "IX_CRM_Customers_OnboardedOn");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_Customers_OrganizationId");

            entity.HasIndex(e => e.PrimaryContactName, "IX_CRM_Customers_PrimaryContactName");

            entity.HasIndex(e => e.PrimaryEmail, "IX_CRM_Customers_PrimaryEmail");

            entity.HasIndex(e => e.PrimaryMobileNumber, "IX_CRM_Customers_PrimaryMobileNumber");

            entity.HasIndex(e => e.SourceLeadId, "IX_CRM_Customers_SourceLeadId");

            entity.HasIndex(e => new { e.OrganizationId, e.CustomerCode }, "UQ_CRM_Customers_Organization_CustomerCode").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.PrimaryEmail }, "UQ_CRM_Customers_Organization_Email").IsUnique();

            entity.Property(e => e.CustomerId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CompanyName).HasMaxLength(250);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CustomerCode).HasMaxLength(50);
            entity.Property(e => e.CustomerStatus).HasDefaultValue((byte)1);
            entity.Property(e => e.Gstnumber)
                .HasMaxLength(30)
                .HasColumnName("GSTNumber");
            entity.Property(e => e.Industry).HasMaxLength(150);
            entity.Property(e => e.LegalName).HasMaxLength(250);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Pannumber)
                .HasMaxLength(30)
                .HasColumnName("PANNumber");
            entity.Property(e => e.PrimaryContactName).HasMaxLength(200);
            entity.Property(e => e.PrimaryEmail).HasMaxLength(150);
            entity.Property(e => e.PrimaryMobileNumber).HasMaxLength(30);
            entity.Property(e => e.Website).HasMaxLength(250);

            entity.HasOne(d => d.AccountManagerUser).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AccountManagerUserId)
                .HasConstraintName("FK_CRM_Customers_AccountManager");

            entity.HasOne(d => d.Organization).WithMany(p => p.Customers)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Customers_Organizations");

            entity.HasOne(d => d.SourceLead).WithMany(p => p.Customers)
                .HasForeignKey(d => d.SourceLeadId)
                .HasConstraintName("FK_CRM_Customers_SourceLead");
        });

        modelBuilder.Entity<CustomerAddOn>(entity =>
        {
            entity.HasKey(e => e.CustomerAddOnId).HasName("PK_CRM_CustomerAddOns");

            entity.ToTable("CustomerAddOns", "CRM");

            entity.HasIndex(e => e.AddOnId, "IX_CRM_CustomerAddOns_AddOnId");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_CustomerAddOns_CustomerId");

            entity.HasIndex(e => e.EndDate, "IX_CRM_CustomerAddOns_EndDate");

            entity.HasIndex(e => e.StartDate, "IX_CRM_CustomerAddOns_StartDate");

            entity.HasIndex(e => e.Status, "IX_CRM_CustomerAddOns_Status");

            entity.HasIndex(e => e.SubscriptionId, "IX_CRM_CustomerAddOns_SubscriptionId");

            entity.HasIndex(e => new { e.CustomerId, e.SubscriptionId, e.AddOnId, e.StartDate }, "UQ_CRM_CustomerAddOns").IsUnique();

            entity.Property(e => e.CustomerAddOnId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AddOn).WithMany(p => p.CustomerAddOns)
                .HasForeignKey(d => d.AddOnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CustomerAddOns_AddOns");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerAddOns)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CustomerAddOns_Customers");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => e.CustomerAddressId).HasName("PK_CRM_CustomerAddresses");

            entity.ToTable("CustomerAddresses", "CRM");

            entity.HasIndex(e => e.AddressType, "IX_CRM_CustomerAddresses_AddressType");

            entity.HasIndex(e => e.City, "IX_CRM_CustomerAddresses_City");

            entity.HasIndex(e => e.Country, "IX_CRM_CustomerAddresses_Country");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_CustomerAddresses_CustomerId");

            entity.HasIndex(e => e.IsDefault, "IX_CRM_CustomerAddresses_IsDefault");

            entity.HasIndex(e => e.PostalCode, "IX_CRM_CustomerAddresses_PostalCode");

            entity.HasIndex(e => e.State, "IX_CRM_CustomerAddresses_State");

            entity.Property(e => e.CustomerAddressId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AddressLine1).HasMaxLength(250);
            entity.Property(e => e.AddressLine2).HasMaxLength(250);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.State).HasMaxLength(100);

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CustomerAddresses_Customers");
        });

        modelBuilder.Entity<CustomerBillingDetail>(entity =>
        {
            entity.HasKey(e => e.CustomerBillingDetailId).HasName("PK_CRM_CustomerBillingDetails");

            entity.ToTable("CustomerBillingDetails", "CRM");

            entity.HasIndex(e => e.BillingCompanyName, "IX_CRM_CustomerBillingDetails_BillingCompanyName");

            entity.HasIndex(e => e.BillingEmail, "IX_CRM_CustomerBillingDetails_BillingEmail");

            entity.HasIndex(e => e.BillingMobileNumber, "IX_CRM_CustomerBillingDetails_BillingMobileNumber");

            entity.HasIndex(e => e.CreditLimit, "IX_CRM_CustomerBillingDetails_CreditLimit");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_CustomerBillingDetails_CustomerId");

            entity.HasIndex(e => e.Gstnumber, "IX_CRM_CustomerBillingDetails_GSTNumber");

            entity.HasIndex(e => e.IsTaxExempt, "IX_CRM_CustomerBillingDetails_IsTaxExempt");

            entity.HasIndex(e => e.Pannumber, "IX_CRM_CustomerBillingDetails_PANNumber");

            entity.HasIndex(e => e.PaymentTermsDays, "IX_CRM_CustomerBillingDetails_PaymentTermsDays");

            entity.HasIndex(e => e.CustomerId, "UQ_CRM_CustomerBillingDetails_Customer").IsUnique();

            entity.Property(e => e.CustomerBillingDetailId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BillingCompanyName).HasMaxLength(250);
            entity.Property(e => e.BillingEmail).HasMaxLength(150);
            entity.Property(e => e.BillingMobileNumber).HasMaxLength(30);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CreditLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Gstnumber)
                .HasMaxLength(30)
                .HasColumnName("GSTNumber");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Pannumber)
                .HasMaxLength(30)
                .HasColumnName("PANNumber");
            entity.Property(e => e.TaxExemptionNumber).HasMaxLength(100);

            entity.HasOne(d => d.Customer).WithOne(p => p.CustomerBillingDetail)
                .HasForeignKey<CustomerBillingDetail>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CustomerBillingDetails_Customers");
        });

        modelBuilder.Entity<CustomerContact>(entity =>
        {
            entity.HasKey(e => e.CustomerContactId).HasName("PK_CRM_CustomerContacts");

            entity.ToTable("CustomerContacts", "CRM");

            entity.HasIndex(e => e.ContactName, "IX_CRM_CustomerContacts_ContactName");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_CustomerContacts_CustomerId");

            entity.HasIndex(e => e.Email, "IX_CRM_CustomerContacts_Email");

            entity.HasIndex(e => e.IsBillingContact, "IX_CRM_CustomerContacts_IsBillingContact");

            entity.HasIndex(e => e.IsPrimaryContact, "IX_CRM_CustomerContacts_IsPrimaryContact");

            entity.HasIndex(e => e.IsTechnicalContact, "IX_CRM_CustomerContacts_IsTechnicalContact");

            entity.HasIndex(e => e.MobileNumber, "IX_CRM_CustomerContacts_MobileNumber");

            entity.Property(e => e.CustomerContactId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AlternateNumber).HasMaxLength(30);
            entity.Property(e => e.ContactName).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Designation).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.MobileNumber).HasMaxLength(30);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerContacts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CustomerContacts_Customers");
        });

        modelBuilder.Entity<CustomerTenant>(entity =>
        {
            entity.HasKey(e => e.CustomerTenantId).HasName("PK_CRM_CustomerTenants");

            entity.ToTable("CustomerTenants", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_CustomerTenants_CompanyId");

            entity.HasIndex(e => e.CustomerAdminUserId, "IX_CRM_CustomerTenants_CustomerAdminUserId");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_CustomerTenants_CustomerId");

            entity.HasIndex(e => e.GoLiveOn, "IX_CRM_CustomerTenants_GoLiveOn");

            entity.HasIndex(e => e.ProvisionedOn, "IX_CRM_CustomerTenants_ProvisionedOn");

            entity.HasIndex(e => e.RegionId, "IX_CRM_CustomerTenants_RegionId");

            entity.HasIndex(e => e.Status, "IX_CRM_CustomerTenants_Status");

            entity.HasIndex(e => e.SubscriptionId, "IX_CRM_CustomerTenants_SubscriptionId");

            entity.HasIndex(e => e.UserId, "IX_CRM_CustomerTenants_UserId");

            entity.HasIndex(e => e.CustomDomain, "UQ_CRM_CustomerTenants_CustomDomain").IsUnique();

            entity.HasIndex(e => e.SubDomain, "UQ_CRM_CustomerTenants_SubDomain").IsUnique();

            entity.HasIndex(e => e.TenantCode, "UQ_CRM_CustomerTenants_TenantCode").IsUnique();

            entity.Property(e => e.CustomerTenantId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CustomDomain).HasMaxLength(250);
            entity.Property(e => e.DatabaseName).HasMaxLength(150);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.SubDomain).HasMaxLength(150);
            entity.Property(e => e.TenantCode).HasMaxLength(50);
            entity.Property(e => e.TenantName).HasMaxLength(200);
            entity.Property(e => e.TenantUrl).HasMaxLength(500);

            entity.HasOne(d => d.Company).WithMany(p => p.CustomerTenants)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CustomerTenants_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CustomerTenantCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CustomerTenants_CreatedBy");

            entity.HasOne(d => d.CustomerAdminUser).WithMany(p => p.CustomerTenantCustomerAdminUsers)
                .HasForeignKey(d => d.CustomerAdminUserId)
                .HasConstraintName("FK_CRM_CustomerTenants_CustomerAdminUser");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerTenants)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CustomerTenants_Customer");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.CustomerTenantModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_CustomerTenants_ModifiedBy");

            entity.HasOne(d => d.Subscription).WithMany(p => p.CustomerTenants)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CustomerTenants_Subscription");

            entity.HasOne(d => d.User).WithMany(p => p.CustomerTenantUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_CustomerTenants_User");
        });

        modelBuilder.Entity<DataMigrationRequest>(entity =>
        {
            entity.HasKey(e => e.DataMigrationRequestId).HasName("PK_CRM_DataMigrationRequests");

            entity.ToTable("DataMigrationRequests", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_DataMigrationRequests_CompanyId");

            entity.HasIndex(e => e.CompletedOn, "IX_CRM_DataMigrationRequests_CompletedOn");

            entity.HasIndex(e => e.MigrationType, "IX_CRM_DataMigrationRequests_MigrationType");

            entity.HasIndex(e => e.OnboardingProjectId, "IX_CRM_DataMigrationRequests_OnboardingProjectId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_DataMigrationRequests_RegionId");

            entity.HasIndex(e => e.RequestedOn, "IX_CRM_DataMigrationRequests_RequestedOn");

            entity.HasIndex(e => e.Status, "IX_CRM_DataMigrationRequests_Status");

            entity.HasIndex(e => e.UserId, "IX_CRM_DataMigrationRequests_UserId");

            entity.Property(e => e.DataMigrationRequestId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.FileUrl).HasMaxLength(1000);
            entity.Property(e => e.RequestedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.SourceSystem).HasMaxLength(150);

            entity.HasOne(d => d.Company).WithMany(p => p.DataMigrationRequests)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_DataMigrationRequests_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DataMigrationRequestCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_DataMigrationRequests_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.DataMigrationRequestModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_DataMigrationRequests_ModifiedBy");

            entity.HasOne(d => d.OnboardingProject).WithMany(p => p.DataMigrationRequests)
                .HasForeignKey(d => d.OnboardingProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_DataMigrationRequests_OnboardingProject");

            entity.HasOne(d => d.User).WithMany(p => p.DataMigrationRequestUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_DataMigrationRequests_User");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCDBA724C1B");

            entity.ToTable("Departments", "Master");

            entity.HasIndex(e => e.DepartmentCode, "UQ__Departme__6EA8896DACF90FA3").IsUnique();

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Department1>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK_Masters_Departments");

            entity.ToTable("Departments", "Masters");

            entity.HasIndex(e => e.BranchId, "IX_Masters_Departments_BranchId");

            entity.HasIndex(e => e.DepartmentHeadUserId, "IX_Masters_Departments_DepartmentHeadUserId");

            entity.HasIndex(e => e.DepartmentName, "IX_Masters_Departments_DepartmentName");

            entity.HasIndex(e => e.OrganizationId, "IX_Masters_Departments_OrganizationId");

            entity.HasIndex(e => e.Status, "IX_Masters_Departments_Status");

            entity.HasIndex(e => new { e.OrganizationId, e.DepartmentCode }, "UQ_Masters_Departments_OrganizationId_DepartmentCode").IsUnique();

            entity.Property(e => e.DepartmentId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DepartmentCode).HasMaxLength(50);
            entity.Property(e => e.DepartmentName).HasMaxLength(150);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Branch).WithMany(p => p.Department1s)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK_Masters_Departments_Branches");

            entity.HasOne(d => d.Organization).WithMany(p => p.Department1s)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Masters_Departments_Organizations");
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.HasKey(e => e.DesignationId).HasName("PK__Designat__BABD603EB368B3C8");

            entity.ToTable("Designations", "Master");

            entity.HasIndex(e => e.DesignationCode, "UQ__Designat__B676DA1FD57B823A").IsUnique();

            entity.Property(e => e.DesignationId).HasColumnName("DesignationID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DesignationCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DesignationName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DiscountType>(entity =>
        {
            entity.HasKey(e => e.DiscountTypeId).HasName("PK__Discount__6CCE1DD6C62D2DCE");

            entity.ToTable("DiscountType", "Masters");

            entity.Property(e => e.DiscountTypeId).HasColumnName("DiscountTypeID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.DiscountTypeCode).HasMaxLength(150);
            entity.Property(e => e.DiscountTypeName).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<EmailAutomation>(entity =>
        {
            entity.ToTable("EmailAutomations", "Superadmin");

            entity.Property(e => e.EmailAutomationId).HasColumnName("EmailAutomationID");
            entity.Property(e => e.AutomationName).HasMaxLength(200);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EmailTemplateId).HasColumnName("EmailTemplateID");
            entity.Property(e => e.FromEmail).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModuleName).HasMaxLength(100);
            entity.Property(e => e.RecipientType).HasMaxLength(100);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.ScheduleType).HasMaxLength(50);
            entity.Property(e => e.TriggerEvent).HasMaxLength(100);
        });

        modelBuilder.Entity<EmailAutomationRecipient>(entity =>
        {
            entity.ToTable("EmailAutomationRecipients", "Superadmin");

            entity.Property(e => e.EmailAutomationRecipientId).HasColumnName("EmailAutomationRecipientID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmailAutomationId).HasColumnName("EmailAutomationID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RecipientType).HasMaxLength(10);
            entity.Property(e => e.RecipientValue).HasMaxLength(500);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");

            entity.HasOne(d => d.EmailAutomation).WithMany(p => p.EmailAutomationRecipients)
                .HasForeignKey(d => d.EmailAutomationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailAutomationRecipients_EmailAutomations");
        });

        modelBuilder.Entity<EmailCategory>(entity =>
        {
            entity.HasKey(e => e.EmailCategoryId).HasName("PK__EmailCat__7DD10E15B6CAC68A");

            entity.ToTable("EmailCategory", "Masters");

            entity.Property(e => e.EmailCategoryId).HasColumnName("EmailCategoryID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.EmailCategoryCode).HasMaxLength(150);
            entity.Property(e => e.EmailCategoryName).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.HasKey(e => e.EmailTemplateId).HasName("PK_CRM_EmailTemplates");

            entity.ToTable("EmailTemplates", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_EmailTemplates_CompanyId");

            entity.HasIndex(e => e.IsSystemTemplate, "IX_CRM_EmailTemplates_IsSystemTemplate");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_EmailTemplates_OrganizationId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_EmailTemplates_RegionId");

            entity.HasIndex(e => e.Status, "IX_CRM_EmailTemplates_Status");

            entity.HasIndex(e => e.TemplateCode, "IX_CRM_EmailTemplates_TemplateCode");

            entity.HasIndex(e => e.UserId, "IX_CRM_EmailTemplates_UserId");

            entity.HasIndex(e => new { e.OrganizationId, e.TemplateCode }, "UQ_CRM_EmailTemplates_TemplateCode").IsUnique();

            entity.Property(e => e.EmailTemplateId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Subject).HasMaxLength(250);
            entity.Property(e => e.TemplateCode).HasMaxLength(100);
            entity.Property(e => e.TemplateName).HasMaxLength(150);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EmailTemplateCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_EmailTemplates_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.EmailTemplateModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_EmailTemplates_ModifiedBy");

            entity.HasOne(d => d.User).WithMany(p => p.EmailTemplateUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_EmailTemplates_User");
        });

        modelBuilder.Entity<EmailType>(entity =>
        {
            entity.HasKey(e => e.EmailTypesId).HasName("PK__EmailTyp__0F92B0B12024020B");

            entity.ToTable("EmailTypes", "Masters");

            entity.Property(e => e.EmailTypesId).HasColumnName("EmailTypesID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.EmailTypesCode).HasMaxLength(150);
            entity.Property(e => e.EmailTypesName).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<EmailsTemplate>(entity =>
        {
            entity.HasKey(e => e.EmailsTemplatesId).HasName("PK__EmailsTe__5E83C46163F020A4");

            entity.ToTable("EmailsTemplates", "Masters");

            entity.Property(e => e.EmailsTemplatesId).HasColumnName("EmailsTemplatesID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.EmailsTemplatesCode).HasMaxLength(150);
            entity.Property(e => e.EmailsTemplatesName).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<EscalationRule>(entity =>
        {
            entity.ToTable("EscalationRules", "Superadmin");

            entity.Property(e => e.EscalationRuleId).HasColumnName("EscalationRuleID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EscalateToType).HasMaxLength(50);
            entity.Property(e => e.EscalateToUserId).HasColumnName("EscalateToUserID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModuleName).HasMaxLength(100);
            entity.Property(e => e.NotificationMethod).HasMaxLength(50);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.RuleName).HasMaxLength(200);
        });

        modelBuilder.Entity<FiscalType>(entity =>
        {
            entity.HasKey(e => e.FiscalTypeId).HasName("PK__FiscalTy__2D1E582082E8A219");

            entity.ToTable("FiscalType", "Masters");

            entity.Property(e => e.FiscalTypeId).HasColumnName("FiscalTypeID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.FiscalTypeCode).HasMaxLength(150);
            entity.Property(e => e.FiscalTypeName).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<GoLiveChecklist>(entity =>
        {
            entity.HasKey(e => e.GoLiveChecklistId).HasName("PK_CRM_GoLiveChecklists");

            entity.ToTable("GoLiveChecklists", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_GoLiveChecklists_CompanyId");

            entity.HasIndex(e => e.CompletedByUserId, "IX_CRM_GoLiveChecklists_CompletedByUserId");

            entity.HasIndex(e => e.DisplayOrder, "IX_CRM_GoLiveChecklists_DisplayOrder");

            entity.HasIndex(e => e.IsCompleted, "IX_CRM_GoLiveChecklists_IsCompleted");

            entity.HasIndex(e => e.IsMandatory, "IX_CRM_GoLiveChecklists_IsMandatory");

            entity.HasIndex(e => e.OnboardingProjectId, "IX_CRM_GoLiveChecklists_OnboardingProjectId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_GoLiveChecklists_RegionId");

            entity.HasIndex(e => e.UserId, "IX_CRM_GoLiveChecklists_UserId");

            entity.Property(e => e.GoLiveChecklistId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ChecklistItem).HasMaxLength(250);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DisplayOrder).HasDefaultValue(1);
            entity.Property(e => e.IsMandatory).HasDefaultValue(true);
            entity.Property(e => e.Remarks).HasMaxLength(500);

            entity.HasOne(d => d.Company).WithMany(p => p.GoLiveChecklists)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_GoLiveChecklists_Company");

            entity.HasOne(d => d.CompletedByUser).WithMany(p => p.GoLiveChecklistCompletedByUsers)
                .HasForeignKey(d => d.CompletedByUserId)
                .HasConstraintName("FK_CRM_GoLiveChecklists_CompletedByUser");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GoLiveChecklistCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_GoLiveChecklists_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.GoLiveChecklistModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_GoLiveChecklists_ModifiedBy");

            entity.HasOne(d => d.OnboardingProject).WithMany(p => p.GoLiveChecklists)
                .HasForeignKey(d => d.OnboardingProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_GoLiveChecklists_OnboardingProject");

            entity.HasOne(d => d.User).WithMany(p => p.GoLiveChecklistUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_GoLiveChecklists_User");
        });

        modelBuilder.Entity<Industry>(entity =>
        {
            entity.HasKey(e => e.IndustryId).HasName("PK__Industry__808DEC2CA66AB5E3");

            entity.ToTable("Industry", "Masters");

            entity.Property(e => e.IndustryId).HasColumnName("IndustryID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IndustryCode).HasMaxLength(150);
            entity.Property(e => e.IndustryName).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK_CRM_Invoices");

            entity.ToTable("Invoices", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_Invoices_CompanyId");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_Invoices_CustomerId");

            entity.HasIndex(e => e.DueDate, "IX_CRM_Invoices_DueDate");

            entity.HasIndex(e => e.InvoiceDate, "IX_CRM_Invoices_InvoiceDate");

            entity.HasIndex(e => e.InvoiceNumber, "IX_CRM_Invoices_InvoiceNumber");

            entity.HasIndex(e => e.InvoiceStatus, "IX_CRM_Invoices_InvoiceStatus");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_Invoices_OrganizationId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_Invoices_RegionId");

            entity.HasIndex(e => e.SalesOrderId, "IX_CRM_Invoices_SalesOrderId");

            entity.HasIndex(e => e.UserId, "IX_CRM_Invoices_UserId");

            entity.HasIndex(e => new { e.OrganizationId, e.InvoiceNumber }, "UQ_CRM_Invoices").IsUnique();

            entity.Property(e => e.InvoiceId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BalanceAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceNumber).HasMaxLength(50);
            entity.Property(e => e.InvoiceStatus).HasDefaultValue((byte)1);
            entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentTerms).HasMaxLength(500);
            entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Company).WithMany(p => p.InvoiceCompanies)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Invoices_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InvoiceCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Invoices_CreatedBy");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Invoices_Customer");

            entity.HasOne(d => d.GeneratedByUser).WithMany(p => p.InvoiceGeneratedByUsers)
                .HasForeignKey(d => d.GeneratedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Invoices_GeneratedByUser");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.InvoiceModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_Invoices_ModifiedBy");

            entity.HasOne(d => d.Organization).WithMany(p => p.InvoiceOrganizations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Invoices_Organizations");

            entity.HasOne(d => d.SalesOrder).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.SalesOrderId)
                .HasConstraintName("FK_CRM_Invoices_SalesOrder");

            entity.HasOne(d => d.User).WithMany(p => p.InvoiceUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Invoices_User");
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.InvoiceItemId).HasName("PK_CRM_InvoiceItems");

            entity.ToTable("InvoiceItems", "CRM");

            entity.HasIndex(e => e.AddOnId, "IX_CRM_InvoiceItems_AddOnId");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_InvoiceItems_CompanyId");

            entity.HasIndex(e => e.InvoiceId, "IX_CRM_InvoiceItems_InvoiceId");

            entity.HasIndex(e => e.ProductId, "IX_CRM_InvoiceItems_ProductId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_InvoiceItems_RegionId");

            entity.HasIndex(e => e.UserId, "IX_CRM_InvoiceItems_UserId");

            entity.HasIndex(e => new { e.InvoiceId, e.ProductId, e.AddOnId }, "UQ_CRM_InvoiceItems").IsUnique();

            entity.Property(e => e.InvoiceItemId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ItemName).HasMaxLength(250);
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AddOn).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.AddOnId)
                .HasConstraintName("FK_CRM_InvoiceItems_AddOn");

            entity.HasOne(d => d.Company).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_InvoiceItems_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InvoiceItemCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_InvoiceItems_CreatedBy");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_InvoiceItems_Invoice");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.InvoiceItemModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_InvoiceItems_ModifiedBy");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_CRM_InvoiceItems_Product");

            entity.HasOne(d => d.User).WithMany(p => p.InvoiceItemUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_InvoiceItems_User");
        });

        modelBuilder.Entity<Lead>(entity =>
        {
            entity.HasKey(e => e.LeadId).HasName("PK_CRM_Leads");

            entity.ToTable("Leads", "CRM");

            entity.HasIndex(e => e.AssignedToUserId, "IX_CRM_Leads_AssignedToUserId");

            entity.HasIndex(e => e.CompanyName, "IX_CRM_Leads_CompanyName");

            entity.HasIndex(e => e.ContactPersonName, "IX_CRM_Leads_ContactPersonName");

            entity.HasIndex(e => e.Email, "IX_CRM_Leads_Email");

            entity.HasIndex(e => e.IsConverted, "IX_CRM_Leads_IsConverted");

            entity.HasIndex(e => e.LeadNumber, "IX_CRM_Leads_LeadNumber");

            entity.HasIndex(e => e.LeadSourceId, "IX_CRM_Leads_LeadSourceId");

            entity.HasIndex(e => e.LeadStatusId, "IX_CRM_Leads_LeadStatusId");

            entity.HasIndex(e => e.MobileNumber, "IX_CRM_Leads_MobileNumber");

            entity.HasIndex(e => e.NextFollowUpOn, "IX_CRM_Leads_NextFollowUpOn");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_Leads_OrganizationId");

            entity.HasIndex(e => e.Status, "IX_CRM_Leads_Status");

            entity.HasIndex(e => new { e.OrganizationId, e.LeadNumber }, "UQ_CRM_Leads_Organization_LeadNumber").IsUnique();

            entity.Property(e => e.LeadId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AlternateMobileNumber).HasMaxLength(30);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CompanyName).HasMaxLength(200);
            entity.Property(e => e.ContactPersonName).HasMaxLength(200);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Designation).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.ExpectedValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Industry).HasMaxLength(150);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LeadNumber).HasMaxLength(50);
            entity.Property(e => e.LostReason).HasMaxLength(500);
            entity.Property(e => e.MobileNumber).HasMaxLength(30);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Priority).HasDefaultValue((byte)2);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.Website).HasMaxLength(250);

            entity.HasOne(d => d.AssignedByUser).WithMany(p => p.LeadAssignedByUsers)
                .HasForeignKey(d => d.AssignedByUserId)
                .HasConstraintName("FK_CRM_Leads_AssignedByUser");

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.LeadAssignedToUsers)
                .HasForeignKey(d => d.AssignedToUserId)
                .HasConstraintName("FK_CRM_Leads_AssignedToUser");
        });

        modelBuilder.Entity<LeadActivity>(entity =>
        {
            entity.HasKey(e => e.LeadActivityId).HasName("PK_CRM_LeadActivities");

            entity.ToTable("LeadActivities", "CRM");

            entity.HasIndex(e => e.ActivityDate, "IX_CRM_LeadActivities_ActivityDate");

            entity.HasIndex(e => e.ActivityType, "IX_CRM_LeadActivities_ActivityType");

            entity.HasIndex(e => e.LeadId, "IX_CRM_LeadActivities_LeadId");

            entity.HasIndex(e => e.NextFollowUpOn, "IX_CRM_LeadActivities_NextFollowUpOn");

            entity.HasIndex(e => e.PerformedByUserId, "IX_CRM_LeadActivities_PerformedByUserId");

            entity.HasIndex(e => e.Status, "IX_CRM_LeadActivities_Status");

            entity.Property(e => e.LeadActivityId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ActivityDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Outcome).HasMaxLength(250);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.Subject).HasMaxLength(250);

            entity.HasOne(d => d.Lead).WithMany(p => p.LeadActivities)
                .HasForeignKey(d => d.LeadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadActivities_Leads");

            entity.HasOne(d => d.PerformedByUser).WithMany(p => p.LeadActivities)
                .HasForeignKey(d => d.PerformedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadActivities_PerformedByUser");
        });

        modelBuilder.Entity<LeadAssignment>(entity =>
        {
            entity.HasKey(e => e.LeadAssignmentId).HasName("PK_CRM_LeadAssignments");

            entity.ToTable("LeadAssignments", "CRM");

            entity.HasIndex(e => e.AssignedByUserId, "IX_CRM_LeadAssignments_AssignedByUserId");

            entity.HasIndex(e => e.AssignedOn, "IX_CRM_LeadAssignments_AssignedOn");

            entity.HasIndex(e => e.AssignedToUserId, "IX_CRM_LeadAssignments_AssignedToUserId");

            entity.HasIndex(e => e.IsCurrentAssignment, "IX_CRM_LeadAssignments_IsCurrentAssignment");

            entity.HasIndex(e => e.LeadId, "IX_CRM_LeadAssignments_LeadId");

            entity.Property(e => e.LeadAssignmentId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AssignedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.AssignmentReason).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.IsCurrentAssignment).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

            entity.HasOne(d => d.AssignedByUser).WithMany(p => p.LeadAssignmentAssignedByUsers)
                .HasForeignKey(d => d.AssignedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadAssignments_AssignedByUser");

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.LeadAssignmentAssignedToUsers)
                .HasForeignKey(d => d.AssignedToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadAssignments_AssignedToUser");

            entity.HasOne(d => d.Lead).WithMany(p => p.LeadAssignments)
                .HasForeignKey(d => d.LeadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadAssignments_Leads");
        });

        modelBuilder.Entity<LeadCall>(entity =>
        {
            entity.HasKey(e => e.LeadCallId).HasName("PK_CRM_LeadCalls");

            entity.ToTable("LeadCalls", "CRM");

            entity.HasIndex(e => e.CallEndedOn, "IX_CRM_LeadCalls_CallEndedOn");

            entity.HasIndex(e => e.CallStartedOn, "IX_CRM_LeadCalls_CallStartedOn");

            entity.HasIndex(e => e.CallStatus, "IX_CRM_LeadCalls_CallStatus");

            entity.HasIndex(e => e.LeadId, "IX_CRM_LeadCalls_LeadId");

            entity.HasIndex(e => e.NextFollowUpOn, "IX_CRM_LeadCalls_NextFollowUpOn");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_LeadCalls_OrganizationId");

            entity.HasIndex(e => e.SalesUserId, "IX_CRM_LeadCalls_SalesUserId");

            entity.HasIndex(e => e.Status, "IX_CRM_LeadCalls_Status");

            entity.HasIndex(e => e.TwilioCallSid, "IX_CRM_LeadCalls_TwilioCallSid");

            entity.Property(e => e.LeadCallId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CallOutcome).HasMaxLength(100);
            entity.Property(e => e.CallStatus).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.FailureReason).HasMaxLength(500);
            entity.Property(e => e.FromNumber).HasMaxLength(30);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RecordingUrl).HasMaxLength(1000);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.ToNumber).HasMaxLength(30);
            entity.Property(e => e.TwilioCallSid).HasMaxLength(100);

            entity.HasOne(d => d.Lead).WithMany(p => p.LeadCalls)
                .HasForeignKey(d => d.LeadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadCalls_Leads");

            entity.HasOne(d => d.Organization).WithMany(p => p.LeadCalls)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadCalls_Organizations");

            entity.HasOne(d => d.SalesUser).WithMany(p => p.LeadCalls)
                .HasForeignKey(d => d.SalesUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadCalls_SalesUser");
        });

        modelBuilder.Entity<LeadFollowUp>(entity =>
        {
            entity.HasKey(e => e.LeadFollowUpId).HasName("PK_CRM_LeadFollowUps");

            entity.ToTable("LeadFollowUps", "CRM");

            entity.HasIndex(e => e.AssignedToUserId, "IX_CRM_LeadFollowUps_AssignedToUserId");

            entity.HasIndex(e => e.CompletedByUserId, "IX_CRM_LeadFollowUps_CompletedByUserId");

            entity.HasIndex(e => e.CompletedOn, "IX_CRM_LeadFollowUps_CompletedOn");

            entity.HasIndex(e => e.FollowUpDate, "IX_CRM_LeadFollowUps_FollowUpDate");

            entity.HasIndex(e => e.LeadId, "IX_CRM_LeadFollowUps_LeadId");

            entity.HasIndex(e => e.ReminderOn, "IX_CRM_LeadFollowUps_ReminderOn");

            entity.HasIndex(e => e.Status, "IX_CRM_LeadFollowUps_Status");

            entity.Property(e => e.LeadFollowUpId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.LeadFollowUpAssignedToUsers)
                .HasForeignKey(d => d.AssignedToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadFollowUps_AssignedToUser");

            entity.HasOne(d => d.CompletedByUser).WithMany(p => p.LeadFollowUpCompletedByUsers)
                .HasForeignKey(d => d.CompletedByUserId)
                .HasConstraintName("FK_CRM_LeadFollowUps_CompletedByUser");

            entity.HasOne(d => d.Lead).WithMany(p => p.LeadFollowUps)
                .HasForeignKey(d => d.LeadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadFollowUps_Leads");
        });

        modelBuilder.Entity<LeadInformation>(entity =>
        {
            entity.HasKey(e => e.LeadId);

            entity.ToTable("LeadInformation", "CRM");

            entity.HasIndex(e => e.LeadNumber, "UQ_LeadInformation_LeadNumber").IsUnique();

            entity.Property(e => e.LeadId).HasColumnName("LeadID");
            entity.Property(e => e.AnnualRevenue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.City).HasMaxLength(150);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CompanyName).HasMaxLength(200);
            entity.Property(e => e.CompanySize).HasMaxLength(50);
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CrmcompanyId).HasColumnName("CRMCompanyID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EstimatedDealValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IndustryId).HasColumnName("IndustryID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.JobTitle).HasMaxLength(150);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LeadNumber).HasMaxLength(50);
            entity.Property(e => e.LeadOwnerId).HasColumnName("LeadOwnerID");
            entity.Property(e => e.LeadRating).HasMaxLength(30);
            entity.Property(e => e.LeadSourceId).HasColumnName("LeadSourceID");
            entity.Property(e => e.LeadStatus).HasMaxLength(50);
            entity.Property(e => e.LeadTypeId).HasColumnName("LeadTypeID");
            entity.Property(e => e.Mobile).HasMaxLength(30);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.PreferredContactMethod).HasMaxLength(50);
            entity.Property(e => e.PrimaryContactId).HasColumnName("PrimaryContactID");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.Salutation).HasMaxLength(20);
            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.StreetAddress).HasMaxLength(300);
            entity.Property(e => e.Website).HasMaxLength(500);

            entity.HasOne(d => d.Country).WithMany(p => p.LeadInformations)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_LeadInformation_Country");

            entity.HasOne(d => d.Industry).WithMany(p => p.LeadInformations)
                .HasForeignKey(d => d.IndustryId)
                .HasConstraintName("FK_LeadInformation_Industry");

            entity.HasOne(d => d.LeadSource).WithMany(p => p.LeadInformations)
                .HasForeignKey(d => d.LeadSourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeadInformation_LeadSource");

            entity.HasOne(d => d.LeadType).WithMany(p => p.LeadInformations)
                .HasForeignKey(d => d.LeadTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeadInformation_LeadType");

            entity.HasOne(d => d.State).WithMany(p => p.LeadInformations)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_LeadInformation_State");
        });

        modelBuilder.Entity<LeadNote>(entity =>
        {
            entity.HasKey(e => e.LeadNoteId).HasName("PK_CRM_LeadNotes");

            entity.ToTable("LeadNotes", "CRM");

            entity.HasIndex(e => e.CreatedByUserId, "IX_CRM_LeadNotes_CreatedByUserId");

            entity.HasIndex(e => e.CreatedOn, "IX_CRM_LeadNotes_CreatedOn");

            entity.HasIndex(e => e.IsImportant, "IX_CRM_LeadNotes_IsImportant");

            entity.HasIndex(e => e.LeadId, "IX_CRM_LeadNotes_LeadId");

            entity.Property(e => e.LeadNoteId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.LeadNotes)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadNotes_CreatedByUser");

            entity.HasOne(d => d.Lead).WithMany(p => p.LeadNotes)
                .HasForeignKey(d => d.LeadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadNotes_Leads");
        });

        modelBuilder.Entity<LeadSource>(entity =>
        {
            entity.HasKey(e => e.LeadSourceId).HasName("PK_CRM_LeadSources");

            entity.ToTable("LeadSources", "CRM");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_LeadSources_OrganizationId");

            entity.HasIndex(e => e.SourceCode, "IX_CRM_LeadSources_SourceCode");

            entity.HasIndex(e => e.SourceName, "IX_CRM_LeadSources_SourceName");

            entity.HasIndex(e => e.Status, "IX_CRM_LeadSources_Status");

            entity.HasIndex(e => new { e.OrganizationId, e.SourceCode }, "UQ_CRM_LeadSources_Organization_SourceCode").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.SourceName }, "UQ_CRM_LeadSources_Organization_SourceName").IsUnique();

            entity.Property(e => e.LeadSourceId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.SourceCode).HasMaxLength(50);
            entity.Property(e => e.SourceName).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Organization).WithMany(p => p.LeadSources)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadSources_Organizations");
        });

        modelBuilder.Entity<LeadSourceDatum>(entity =>
        {
            entity.HasKey(e => e.LeadSourceId).HasName("PK__LeadSour__9FB37DB3DD34C711");

            entity.ToTable("LeadSourceData", "Masters");

            entity.Property(e => e.LeadSourceId).HasColumnName("LeadSourceID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LeadSourceCode).HasMaxLength(150);
            entity.Property(e => e.LeadSourceName).HasMaxLength(150);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<LeadStatus>(entity =>
        {
            entity.HasKey(e => e.LeadStatusId).HasName("PK_CRM_LeadStatuses");

            entity.ToTable("LeadStatuses", "CRM");

            entity.HasIndex(e => e.DisplayOrder, "IX_CRM_LeadStatuses_DisplayOrder");

            entity.HasIndex(e => e.IsDefault, "IX_CRM_LeadStatuses_IsDefault");

            entity.HasIndex(e => e.IsFinalStatus, "IX_CRM_LeadStatuses_IsFinalStatus");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_LeadStatuses_OrganizationId");

            entity.HasIndex(e => e.Status, "IX_CRM_LeadStatuses_Status");

            entity.HasIndex(e => e.StatusCategory, "IX_CRM_LeadStatuses_StatusCategory");

            entity.HasIndex(e => e.StatusCode, "IX_CRM_LeadStatuses_StatusCode");

            entity.HasIndex(e => e.StatusName, "IX_CRM_LeadStatuses_StatusName");

            entity.HasIndex(e => new { e.OrganizationId, e.StatusCode }, "UQ_CRM_LeadStatuses_Organization_StatusCode").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.StatusName }, "UQ_CRM_LeadStatuses_Organization_StatusName").IsUnique();

            entity.Property(e => e.LeadStatusId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DisplayOrder).HasDefaultValue(1);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.StatusCode).HasMaxLength(50);
            entity.Property(e => e.StatusName).HasMaxLength(100);
        });

        modelBuilder.Entity<LeadStatusDatum>(entity =>
        {
            entity.HasKey(e => e.LeadStatusId).HasName("PK__LeadStat__33EE656BF7A34EFE");

            entity.ToTable("LeadStatusData", "Masters");

            entity.Property(e => e.LeadStatusId).HasColumnName("LeadStatusID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LeadStatusCode).HasMaxLength(150);
            entity.Property(e => e.LeadStatusName).HasMaxLength(150);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<LeadType>(entity =>
        {
            entity.HasKey(e => e.LeadTypeId).HasName("PK__LeadType__0236086877582BA5");

            entity.ToTable("LeadType", "Masters");

            entity.Property(e => e.LeadTypeId).HasColumnName("LeadTypeID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LeadTypeCode).HasMaxLength(150);
            entity.Property(e => e.LeadTypeName).HasMaxLength(150);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.LicenseId).HasName("PK__License__72D600A2E7DA0099");

            entity.ToTable("License", "Masters");

            entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LicenseCode).HasMaxLength(150);
            entity.Property(e => e.LicenseName).HasMaxLength(150);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<MeetingPurpose>(entity =>
        {
            entity.HasKey(e => e.MeetingPurposeId).HasName("PK__MeetingP__86ACEA0250DB46F4");

            entity.ToTable("MeetingPurpose", "Masters");

            entity.Property(e => e.MeetingPurposeId).HasColumnName("MeetingPurposeID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MeetingPurposeCode).HasMaxLength(150);
            entity.Property(e => e.MeetingPurposeName).HasMaxLength(150);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<MenuMaster>(entity =>
        {
            entity.HasKey(e => e.MenuId);

            entity.ToTable("MenuMaster", "Masters");

            entity.Property(e => e.MenuId).HasColumnName("MenuID");
            entity.Property(e => e.CanView).HasDefaultValue(true);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MenuName).HasMaxLength(100);
            entity.Property(e => e.MenuType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Common");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ParentMenuId).HasColumnName("ParentMenuID");
            entity.Property(e => e.Url).HasMaxLength(255);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK_CRM_Notifications");

            entity.ToTable("Notifications", "CRM");

            entity.HasIndex(e => e.Channel, "IX_CRM_Notifications_Channel");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_Notifications_CompanyId");

            entity.HasIndex(e => e.IsRead, "IX_CRM_Notifications_IsRead");

            entity.HasIndex(e => e.NotificationType, "IX_CRM_Notifications_NotificationType");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_Notifications_OrganizationId");

            entity.HasIndex(e => e.ReferenceId, "IX_CRM_Notifications_ReferenceId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_Notifications_RegionId");

            entity.HasIndex(e => e.SentOn, "IX_CRM_Notifications_SentOn");

            entity.HasIndex(e => e.UserId, "IX_CRM_Notifications_UserId");

            entity.Property(e => e.NotificationId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ReferenceType).HasMaxLength(100);
            entity.Property(e => e.SentOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.NotificationCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Notifications_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.NotificationModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_Notifications_ModifiedBy");

            entity.HasOne(d => d.User).WithMany(p => p.NotificationUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Notifications_User");
        });

        modelBuilder.Entity<NotificationMaster>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E1261619AD3");

            entity.ToTable("NotificationMaster", "Master");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.NotificationType).HasMaxLength(50);
            entity.Property(e => e.ReadAt).HasColumnType("datetime");
            entity.Property(e => e.RedirectUrl).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<OnboardingProject>(entity =>
        {
            entity.HasKey(e => e.OnboardingProjectId).HasName("PK_CRM_OnboardingProjects");

            entity.ToTable("OnboardingProjects", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_OnboardingProjects_CompanyId");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_OnboardingProjects_CustomerId");

            entity.HasIndex(e => e.CustomerTenantId, "IX_CRM_OnboardingProjects_CustomerTenantId");

            entity.HasIndex(e => e.ExpectedGoLiveDate, "IX_CRM_OnboardingProjects_ExpectedGoLiveDate");

            entity.HasIndex(e => e.ProjectManagerUserId, "IX_CRM_OnboardingProjects_ProjectManagerUserId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_OnboardingProjects_RegionId");

            entity.HasIndex(e => e.StartDate, "IX_CRM_OnboardingProjects_StartDate");

            entity.HasIndex(e => e.Status, "IX_CRM_OnboardingProjects_Status");

            entity.HasIndex(e => e.SubscriptionId, "IX_CRM_OnboardingProjects_SubscriptionId");

            entity.HasIndex(e => e.UserId, "IX_CRM_OnboardingProjects_UserId");

            entity.HasIndex(e => e.ProjectNumber, "UQ_CRM_OnboardingProjects_ProjectNumber").IsUnique();

            entity.Property(e => e.OnboardingProjectId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ProjectName).HasMaxLength(250);
            entity.Property(e => e.ProjectNumber).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OnboardingProjectCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_OnboardingProjects_CreatedBy");

            entity.HasOne(d => d.Customer).WithMany(p => p.OnboardingProjects)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_OnboardingProjects_Customer");

            entity.HasOne(d => d.CustomerTenant).WithMany(p => p.OnboardingProjects)
                .HasForeignKey(d => d.CustomerTenantId)
                .HasConstraintName("FK_CRM_OnboardingProjects_CustomerTenant");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.OnboardingProjectModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_OnboardingProjects_ModifiedBy");

            entity.HasOne(d => d.ProjectManagerUser).WithMany(p => p.OnboardingProjectProjectManagerUsers)
                .HasForeignKey(d => d.ProjectManagerUserId)
                .HasConstraintName("FK_CRM_OnboardingProjects_ProjectManager");

            entity.HasOne(d => d.Subscription).WithMany(p => p.OnboardingProjects)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_OnboardingProjects_Subscription");

            entity.HasOne(d => d.User).WithMany(p => p.OnboardingProjectUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_OnboardingProjects_User");
        });

        modelBuilder.Entity<OnboardingTask>(entity =>
        {
            entity.HasKey(e => e.OnboardingTaskId).HasName("PK_CRM_OnboardingTasks");

            entity.ToTable("OnboardingTasks", "CRM");

            entity.HasIndex(e => e.AssignedToUserId, "IX_CRM_OnboardingTasks_AssignedToUserId");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_OnboardingTasks_CompanyId");

            entity.HasIndex(e => e.DisplayOrder, "IX_CRM_OnboardingTasks_DisplayOrder");

            entity.HasIndex(e => e.DueDate, "IX_CRM_OnboardingTasks_DueDate");

            entity.HasIndex(e => e.OnboardingProjectId, "IX_CRM_OnboardingTasks_OnboardingProjectId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_OnboardingTasks_RegionId");

            entity.HasIndex(e => e.Status, "IX_CRM_OnboardingTasks_Status");

            entity.HasIndex(e => e.UserId, "IX_CRM_OnboardingTasks_UserId");

            entity.Property(e => e.OnboardingTaskId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DisplayOrder).HasDefaultValue(1);
            entity.Property(e => e.TaskName).HasMaxLength(250);

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.OnboardingTaskAssignedToUsers)
                .HasForeignKey(d => d.AssignedToUserId)
                .HasConstraintName("FK_CRM_OnboardingTasks_AssignedUser");

            entity.HasOne(d => d.Company).WithMany(p => p.OnboardingTasks)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_OnboardingTasks_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OnboardingTaskCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_OnboardingTasks_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.OnboardingTaskModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_OnboardingTasks_ModifiedBy");

            entity.HasOne(d => d.OnboardingProject).WithMany(p => p.OnboardingTasks)
                .HasForeignKey(d => d.OnboardingProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_OnboardingTasks_Project");

            entity.HasOne(d => d.User).WithMany(p => p.OnboardingTaskUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_OnboardingTasks_User");
        });

        modelBuilder.Entity<Opportunity>(entity =>
        {
            entity.HasKey(e => e.OpportunityId).HasName("PK_CRM_Opportunities");

            entity.ToTable("Opportunities", "CRM");

            entity.HasIndex(e => e.ActualCloseDate, "IX_CRM_Opportunities_ActualCloseDate");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_Opportunities_CustomerId");

            entity.HasIndex(e => e.ExpectedAmount, "IX_CRM_Opportunities_ExpectedAmount");

            entity.HasIndex(e => e.ExpectedCloseDate, "IX_CRM_Opportunities_ExpectedCloseDate");

            entity.HasIndex(e => e.LeadId, "IX_CRM_Opportunities_LeadId");

            entity.HasIndex(e => e.OpportunityNumber, "IX_CRM_Opportunities_OpportunityNumber");

            entity.HasIndex(e => e.OpportunityStageId, "IX_CRM_Opportunities_OpportunityStageId");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_Opportunities_OrganizationId");

            entity.HasIndex(e => e.OwnerUserId, "IX_CRM_Opportunities_OwnerUserId");

            entity.HasIndex(e => e.Status, "IX_CRM_Opportunities_Status");

            entity.HasIndex(e => new { e.OrganizationId, e.OpportunityNumber }, "UQ_CRM_Opportunities_Organization_OpportunityNumber").IsUnique();

            entity.Property(e => e.OpportunityId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ExpectedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LostReason).HasMaxLength(500);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.OpportunityName).HasMaxLength(250);
            entity.Property(e => e.OpportunityNumber).HasMaxLength(50);
            entity.Property(e => e.ProbabilityPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.RequiredStorageGb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("RequiredStorageGB");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Customer).WithMany(p => p.Opportunities)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CRM_Opportunities_Customers");

            entity.HasOne(d => d.Lead).WithMany(p => p.Opportunities)
                .HasForeignKey(d => d.LeadId)
                .HasConstraintName("FK_CRM_Opportunities_Leads");

            entity.HasOne(d => d.Organization).WithMany(p => p.Opportunities)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Opportunities_Organizations");

            entity.HasOne(d => d.OwnerUser).WithMany(p => p.Opportunities)
                .HasForeignKey(d => d.OwnerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Opportunities_OwnerUser");
        });

        modelBuilder.Entity<OpportunityActivity>(entity =>
        {
            entity.HasKey(e => e.OpportunityActivityId).HasName("PK_CRM_OpportunityActivities");

            entity.ToTable("OpportunityActivities", "CRM");

            entity.HasIndex(e => e.ActivityDate, "IX_CRM_OpportunityActivities_ActivityDate");

            entity.HasIndex(e => e.ActivityType, "IX_CRM_OpportunityActivities_ActivityType");

            entity.HasIndex(e => e.NextActionDate, "IX_CRM_OpportunityActivities_NextActionDate");

            entity.HasIndex(e => e.OpportunityId, "IX_CRM_OpportunityActivities_OpportunityId");

            entity.HasIndex(e => e.PerformedByUserId, "IX_CRM_OpportunityActivities_PerformedByUserId");

            entity.Property(e => e.OpportunityActivityId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Subject).HasMaxLength(250);

            entity.HasOne(d => d.Opportunity).WithMany(p => p.OpportunityActivities)
                .HasForeignKey(d => d.OpportunityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_OpportunityActivities_Opportunities");

            entity.HasOne(d => d.PerformedByUser).WithMany(p => p.OpportunityActivities)
                .HasForeignKey(d => d.PerformedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_OpportunityActivities_PerformedByUser");
        });

        modelBuilder.Entity<OpportunityProduct>(entity =>
        {
            entity.HasKey(e => e.OpportunityProductId).HasName("PK_CRM_OpportunityProducts");

            entity.ToTable("OpportunityProducts", "CRM");

            entity.HasIndex(e => e.DiscountPercentage, "IX_CRM_OpportunityProducts_DiscountPercentage");

            entity.HasIndex(e => e.OpportunityId, "IX_CRM_OpportunityProducts_OpportunityId");

            entity.HasIndex(e => e.ProductId, "IX_CRM_OpportunityProducts_ProductId");

            entity.HasIndex(e => e.Quantity, "IX_CRM_OpportunityProducts_Quantity");

            entity.HasIndex(e => e.TaxPercentage, "IX_CRM_OpportunityProducts_TaxPercentage");

            entity.HasIndex(e => e.TotalAmount, "IX_CRM_OpportunityProducts_TotalAmount");

            entity.HasIndex(e => e.UnitPrice, "IX_CRM_OpportunityProducts_UnitPrice");

            entity.HasIndex(e => new { e.OpportunityId, e.ProductId }, "UQ_CRM_OpportunityProducts_Opportunity_Product").IsUnique();

            entity.Property(e => e.OpportunityProductId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Opportunity).WithMany(p => p.OpportunityProducts)
                .HasForeignKey(d => d.OpportunityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_OpportunityProducts_Opportunities");
        });

        modelBuilder.Entity<OpportunityStage>(entity =>
        {
            entity.HasKey(e => e.OpportunityStageId).HasName("PK_CRM_OpportunityStages");

            entity.ToTable("OpportunityStages", "CRM");

            entity.HasIndex(e => e.DisplayOrder, "IX_CRM_OpportunityStages_DisplayOrder");

            entity.HasIndex(e => e.IsLostStage, "IX_CRM_OpportunityStages_IsLostStage");

            entity.HasIndex(e => e.IsWonStage, "IX_CRM_OpportunityStages_IsWonStage");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_OpportunityStages_OrganizationId");

            entity.HasIndex(e => e.ProbabilityPercentage, "IX_CRM_OpportunityStages_ProbabilityPercentage");

            entity.HasIndex(e => e.StageCode, "IX_CRM_OpportunityStages_StageCode");

            entity.HasIndex(e => e.StageName, "IX_CRM_OpportunityStages_StageName");

            entity.HasIndex(e => e.Status, "IX_CRM_OpportunityStages_Status");

            entity.HasIndex(e => new { e.OrganizationId, e.StageCode }, "UQ_CRM_OpportunityStages_Organization_StageCode").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.StageName }, "UQ_CRM_OpportunityStages_Organization_StageName").IsUnique();

            entity.Property(e => e.OpportunityStageId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ProbabilityPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.StageCode).HasMaxLength(50);
            entity.Property(e => e.StageName).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Organization).WithMany(p => p.OpportunityStages)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_OpportunityStages_Organizations");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PK_Masters_Organizations");

            entity.ToTable("Organizations", "Masters");

            entity.HasIndex(e => e.City, "IX_Masters_Organizations_City");

            entity.HasIndex(e => e.OrganizationName, "IX_Masters_Organizations_OrganizationName");

            entity.HasIndex(e => e.Status, "IX_Masters_Organizations_Status");

            entity.HasIndex(e => e.OrganizationCode, "UQ_Masters_Organizations_OrganizationCode").IsUnique();

            entity.Property(e => e.OrganizationId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AddressLine1).HasMaxLength(250);
            entity.Property(e => e.AddressLine2).HasMaxLength(250);
            entity.Property(e => e.BrandColor).HasMaxLength(20);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.ContactEmail).HasMaxLength(150);
            entity.Property(e => e.ContactMobile).HasMaxLength(30);
            entity.Property(e => e.ContactPerson).HasMaxLength(150);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CurrencyCode).HasMaxLength(10);
            entity.Property(e => e.Domain).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Gstnumber)
                .HasMaxLength(30)
                .HasColumnName("GSTNumber");
            entity.Property(e => e.Industry).HasMaxLength(100);
            entity.Property(e => e.LegalName).HasMaxLength(250);
            entity.Property(e => e.LogoUrl).HasMaxLength(500);
            entity.Property(e => e.MaxStorageGb).HasColumnName("MaxStorageGB");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.MonthlyRevenue)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OrganizationCode).HasMaxLength(50);
            entity.Property(e => e.OrganizationName).HasMaxLength(200);
            entity.Property(e => e.Pannumber)
                .HasMaxLength(30)
                .HasColumnName("PANNumber");
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.StorageUsedGb).HasColumnName("StorageUsedGB");
            entity.Property(e => e.TimeZone).HasMaxLength(100);
            entity.Property(e => e.Website).HasMaxLength(250);
        });

        modelBuilder.Entity<OrganizationDatum>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PK_CRM_Organization");

            entity.ToTable("OrganizationData", "CRM");

            entity.HasIndex(e => e.OrganizationCode, "UQ_CRM_Organization_Code").IsUnique();

            entity.Property(e => e.AddressLine1).HasMaxLength(250);
            entity.Property(e => e.AddressLine2).HasMaxLength(250);
            entity.Property(e => e.BrandColor).HasMaxLength(20);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.ContactEmail).HasMaxLength(150);
            entity.Property(e => e.ContactMobile).HasMaxLength(30);
            entity.Property(e => e.ContactPerson).HasMaxLength(150);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrencyCode).HasMaxLength(10);
            entity.Property(e => e.Domain).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Gstnumber)
                .HasMaxLength(30)
                .HasColumnName("GSTNumber");
            entity.Property(e => e.Industry).HasMaxLength(100);
            entity.Property(e => e.LegalName).HasMaxLength(250);
            entity.Property(e => e.LogoUrl).HasMaxLength(500);
            entity.Property(e => e.MaxStorageGb).HasColumnName("MaxStorageGB");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.MonthlyRevenue)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OrganizationCode).HasMaxLength(50);
            entity.Property(e => e.OrganizationName).HasMaxLength(200);
            entity.Property(e => e.Pannumber)
                .HasMaxLength(30)
                .HasColumnName("PANNumber");
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.StorageUsedGb).HasColumnName("StorageUsedGB");
            entity.Property(e => e.TimeZone).HasMaxLength(100);
            entity.Property(e => e.Website).HasMaxLength(250);

            entity.HasOne(d => d.Plan).WithMany(p => p.OrganizationData)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK_CRM_Organization_Plan");
        });

        modelBuilder.Entity<OrganizationSetting>(entity =>
        {
            entity.HasKey(e => e.OrganizationSettingId).HasName("PK_Masters_OrganizationSettings");

            entity.ToTable("OrganizationSettings", "Masters");

            entity.HasIndex(e => e.CurrencyCode, "IX_Masters_OrganizationSettings_CurrencyCode");

            entity.HasIndex(e => e.DefaultLanguage, "IX_Masters_OrganizationSettings_DefaultLanguage");

            entity.HasIndex(e => e.IsGstenabled, "IX_Masters_OrganizationSettings_IsGSTEnabled");

            entity.HasIndex(e => e.OrganizationId, "UX_Masters_OrganizationSettings_OrganizationId").IsUnique();

            entity.Property(e => e.OrganizationSettingId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CurrencyCode).HasMaxLength(10);
            entity.Property(e => e.CurrencySymbol).HasMaxLength(10);
            entity.Property(e => e.DateFormat).HasMaxLength(30);
            entity.Property(e => e.DefaultCountryCode).HasMaxLength(10);
            entity.Property(e => e.DefaultGstpercentage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("DefaultGSTPercentage");
            entity.Property(e => e.DefaultLanguage).HasMaxLength(20);
            entity.Property(e => e.InvoicePrefix).HasMaxLength(30);
            entity.Property(e => e.IsGstenabled)
                .HasDefaultValue(true)
                .HasColumnName("IsGSTEnabled");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.QuotationPrefix).HasMaxLength(30);
            entity.Property(e => e.StorageWarningPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TimeZone).HasMaxLength(100);
            entity.Property(e => e.UserLimitWarningPercentage).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Organization).WithOne(p => p.OrganizationSetting)
                .HasForeignKey<OrganizationSetting>(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Masters_OrganizationSettings_Organizations");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK_CRM_Payments");

            entity.ToTable("Payments", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_Payments_CompanyId");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_Payments_CustomerId");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_Payments_OrganizationId");

            entity.HasIndex(e => e.PaymentDate, "IX_CRM_Payments_PaymentDate");

            entity.HasIndex(e => e.PaymentMode, "IX_CRM_Payments_PaymentMode");

            entity.HasIndex(e => e.PaymentNumber, "IX_CRM_Payments_PaymentNumber");

            entity.HasIndex(e => e.PaymentStatus, "IX_CRM_Payments_PaymentStatus");

            entity.HasIndex(e => e.ReceivedByUserId, "IX_CRM_Payments_ReceivedByUserId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_Payments_RegionId");

            entity.HasIndex(e => e.UserId, "IX_CRM_Payments_UserId");

            entity.HasIndex(e => new { e.OrganizationId, e.PaymentNumber }, "UQ_CRM_Payments").IsUnique();

            entity.Property(e => e.PaymentId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BankName).HasMaxLength(150);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CurrencyCode).HasMaxLength(10);
            entity.Property(e => e.PaymentGatewayName).HasMaxLength(100);
            entity.Property(e => e.PaymentNumber).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasDefaultValue((byte)1);
            entity.Property(e => e.Remarks).HasMaxLength(1000);
            entity.Property(e => e.TransactionReference).HasMaxLength(150);

            entity.HasOne(d => d.Company).WithMany(p => p.PaymentCompanies)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Payments_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PaymentCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Payments_CreatedBy");

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Payments_Customers");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PaymentModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_Payments_ModifiedBy");

            entity.HasOne(d => d.Organization).WithMany(p => p.PaymentOrganizations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Payments_Organizations");

            entity.HasOne(d => d.ReceivedByUser).WithMany(p => p.PaymentReceivedByUsers)
                .HasForeignKey(d => d.ReceivedByUserId)
                .HasConstraintName("FK_CRM_Payments_ReceivedByUser");

            entity.HasOne(d => d.User).WithMany(p => p.PaymentUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Payments_User");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1F3649AB0DC");

            entity.ToTable("PaymentMethod", "Masters");

            entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethodCode).HasMaxLength(150);
            entity.Property(e => e.PaymentMethodName).HasMaxLength(150);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.HasKey(e => e.PaymentTransactionId).HasName("PK_CRM_PaymentTransactions");

            entity.ToTable("PaymentTransactions", "CRM");

            entity.HasIndex(e => e.AllocationDate, "IX_CRM_PaymentTransactions_AllocationDate");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_PaymentTransactions_CompanyId");

            entity.HasIndex(e => e.InvoiceId, "IX_CRM_PaymentTransactions_InvoiceId");

            entity.HasIndex(e => e.PaymentId, "IX_CRM_PaymentTransactions_PaymentId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_PaymentTransactions_RegionId");

            entity.HasIndex(e => e.UserId, "IX_CRM_PaymentTransactions_UserId");

            entity.HasIndex(e => new { e.PaymentId, e.InvoiceId }, "UQ_CRM_PaymentTransactions").IsUnique();

            entity.Property(e => e.PaymentTransactionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AllocatedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AllocationDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Remarks).HasMaxLength(500);

            entity.HasOne(d => d.Company).WithMany(p => p.PaymentTransactions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_PaymentTransactions_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PaymentTransactionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_PaymentTransactions_CreatedBy");

            entity.HasOne(d => d.Invoice).WithMany(p => p.PaymentTransactions)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_PaymentTransactions_Invoice");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PaymentTransactionModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_PaymentTransactions_ModifiedBy");

            entity.HasOne(d => d.Payment).WithMany(p => p.PaymentTransactions)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_PaymentTransactions_Payment");

            entity.HasOne(d => d.User).WithMany(p => p.PaymentTransactionUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_PaymentTransactions_User");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK_Security_Permissions");

            entity.ToTable("Permissions", "Security");

            entity.HasIndex(e => e.ModuleName, "IX_Security_Permissions_ModuleName");

            entity.HasIndex(e => e.PermissionCode, "IX_Security_Permissions_PermissionCode");

            entity.HasIndex(e => e.ScreenName, "IX_Security_Permissions_ScreenName");

            entity.HasIndex(e => e.Status, "IX_Security_Permissions_Status");

            entity.HasIndex(e => e.PermissionCode, "UQ_Security_Permissions_PermissionCode").IsUnique();

            entity.Property(e => e.PermissionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModuleName).HasMaxLength(100);
            entity.Property(e => e.PermissionCode).HasMaxLength(150);
            entity.Property(e => e.ScreenName).HasMaxLength(150);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<PlanFeature>(entity =>
        {
            entity.HasKey(e => e.PlanFeatureId).HasName("PK_CRM_PlanFeatures");

            entity.ToTable("PlanFeatures", "CRM");

            entity.HasIndex(e => e.DisplayOrder, "IX_CRM_PlanFeatures_DisplayOrder");

            entity.HasIndex(e => e.FeatureCode, "IX_CRM_PlanFeatures_FeatureCode");

            entity.HasIndex(e => e.FeatureName, "IX_CRM_PlanFeatures_FeatureName");

            entity.HasIndex(e => e.IsEnabled, "IX_CRM_PlanFeatures_IsEnabled");

            entity.HasIndex(e => e.SubscriptionPlanId, "IX_CRM_PlanFeatures_SubscriptionPlanId");

            entity.HasIndex(e => new { e.SubscriptionPlanId, e.FeatureCode }, "UQ_CRM_PlanFeatures_SubscriptionPlan_FeatureCode").IsUnique();

            entity.Property(e => e.PlanFeatureId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DisplayOrder).HasDefaultValue(1);
            entity.Property(e => e.FeatureCode).HasMaxLength(100);
            entity.Property(e => e.FeatureName).HasMaxLength(150);
            entity.Property(e => e.FeatureValue).HasMaxLength(250);
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.PlanFeatures)
                .HasForeignKey(d => d.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_PlanFeatures_SubscriptionPlans");
        });

        modelBuilder.Entity<PlanModule>(entity =>
        {
            entity.HasKey(e => e.PlanModuleId).HasName("PK_CRM_PlanModules");

            entity.ToTable("PlanModules", "CRM");

            entity.HasIndex(e => e.IsEnabled, "IX_CRM_PlanModules_IsEnabled");

            entity.HasIndex(e => e.ModuleCode, "IX_CRM_PlanModules_ModuleCode");

            entity.HasIndex(e => e.ModuleName, "IX_CRM_PlanModules_ModuleName");

            entity.HasIndex(e => e.SubscriptionPlanId, "IX_CRM_PlanModules_SubscriptionPlanId");

            entity.HasIndex(e => new { e.SubscriptionPlanId, e.ModuleCode }, "UQ_CRM_PlanModules_SubscriptionPlan_ModuleCode").IsUnique();

            entity.Property(e => e.PlanModuleId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModuleCode).HasMaxLength(100);
            entity.Property(e => e.ModuleName).HasMaxLength(150);

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.PlanModules)
                .HasForeignKey(d => d.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_PlanModules_SubscriptionPlans");
        });

        modelBuilder.Entity<PlanPricing>(entity =>
        {
            entity.HasKey(e => e.PlanPricingId).HasName("PK_CRM_PlanPricing");

            entity.ToTable("PlanPricing", "CRM");

            entity.HasIndex(e => e.BillingCycle, "IX_CRM_PlanPricing_BillingCycle");

            entity.HasIndex(e => e.CurrencyCode, "IX_CRM_PlanPricing_CurrencyCode");

            entity.HasIndex(e => e.EffectiveFrom, "IX_CRM_PlanPricing_EffectiveFrom");

            entity.HasIndex(e => e.EffectiveTo, "IX_CRM_PlanPricing_EffectiveTo");

            entity.HasIndex(e => e.Price, "IX_CRM_PlanPricing_Price");

            entity.HasIndex(e => e.Status, "IX_CRM_PlanPricing_Status");

            entity.HasIndex(e => e.SubscriptionPlanId, "IX_CRM_PlanPricing_SubscriptionPlanId");

            entity.HasIndex(e => new { e.SubscriptionPlanId, e.BillingCycle, e.CurrencyCode, e.EffectiveFrom }, "UQ_CRM_PlanPricing").IsUnique();

            entity.Property(e => e.PlanPricingId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CurrencyCode).HasMaxLength(10);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.TaxPercentage)
                .HasDefaultValue(18.00m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.PlanPricings)
                .HasForeignKey(d => d.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_PlanPricing_SubscriptionPlans");
        });

        modelBuilder.Entity<PlanStorageLimit>(entity =>
        {
            entity.HasKey(e => e.PlanStorageLimitId).HasName("PK_CRM_PlanStorageLimits");

            entity.ToTable("PlanStorageLimits", "CRM");

            entity.HasIndex(e => e.BillingCycle, "IX_CRM_PlanStorageLimits_BillingCycle");

            entity.HasIndex(e => e.IncludedStorageGb, "IX_CRM_PlanStorageLimits_IncludedStorageGB");

            entity.HasIndex(e => e.MaximumStorageGb, "IX_CRM_PlanStorageLimits_MaximumStorageGB");

            entity.HasIndex(e => e.Status, "IX_CRM_PlanStorageLimits_Status");

            entity.HasIndex(e => e.SubscriptionPlanId, "IX_CRM_PlanStorageLimits_SubscriptionPlanId");

            entity.HasIndex(e => new { e.SubscriptionPlanId, e.BillingCycle }, "UQ_CRM_PlanStorageLimits").IsUnique();

            entity.Property(e => e.PlanStorageLimitId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AdditionalStoragePricePerGb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("AdditionalStoragePricePerGB");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IncludedStorageGb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("IncludedStorageGB");
            entity.Property(e => e.MaximumStorageGb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MaximumStorageGB");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.PlanStorageLimits)
                .HasForeignKey(d => d.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_PlanStorageLimits_SubscriptionPlans");
        });

        modelBuilder.Entity<PlanUserLimit>(entity =>
        {
            entity.HasKey(e => e.PlanUserLimitId).HasName("PK_CRM_PlanUserLimits");

            entity.ToTable("PlanUserLimits", "CRM");

            entity.HasIndex(e => e.BillingCycle, "IX_CRM_PlanUserLimits_BillingCycle");

            entity.HasIndex(e => e.IncludedUserCount, "IX_CRM_PlanUserLimits_IncludedUserCount");

            entity.HasIndex(e => e.MaximumUserCount, "IX_CRM_PlanUserLimits_MaximumUserCount");

            entity.HasIndex(e => e.Status, "IX_CRM_PlanUserLimits_Status");

            entity.HasIndex(e => e.SubscriptionPlanId, "IX_CRM_PlanUserLimits_SubscriptionPlanId");

            entity.HasIndex(e => new { e.SubscriptionPlanId, e.BillingCycle }, "UQ_CRM_PlanUserLimits").IsUnique();

            entity.Property(e => e.PlanUserLimitId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AdditionalUserPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.PlanUserLimits)
                .HasForeignKey(d => d.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_PlanUserLimits_SubscriptionPlans");
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.PriorityId).HasName("PK__Priority__D0A3D0DE3A00E927");

            entity.ToTable("Priority", "Masters");

            entity.Property(e => e.PriorityId).HasColumnName("PriorityID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.PriorityCode).HasMaxLength(150);
            entity.Property(e => e.PriorityName).HasMaxLength(150);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_CRM_Products");

            entity.ToTable("Products", "CRM");

            entity.HasIndex(e => e.IsSubscriptionProduct, "IX_CRM_Products_IsSubscriptionProduct");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_Products_OrganizationId");

            entity.HasIndex(e => e.ProductCode, "IX_CRM_Products_ProductCode");

            entity.HasIndex(e => e.ProductName, "IX_CRM_Products_ProductName");

            entity.HasIndex(e => e.ProductType, "IX_CRM_Products_ProductType");

            entity.HasIndex(e => e.Status, "IX_CRM_Products_Status");

            entity.HasIndex(e => e.ProductCode, "UQ_CRM_Products_ProductCode").IsUnique();

            entity.HasIndex(e => e.ProductName, "UQ_CRM_Products_ProductName").IsUnique();

            entity.Property(e => e.ProductId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ProductCode).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(200);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Organization).WithMany(p => p.Products)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_CRM_Products_Organizations");
        });

        modelBuilder.Entity<Quotation>(entity =>
        {
            entity.HasKey(e => e.QuotationId).HasName("PK_CRM_Quotations");

            entity.ToTable("Quotations", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_Quotations_CompanyId");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_Quotations_CustomerId");

            entity.HasIndex(e => e.OpportunityId, "IX_CRM_Quotations_OpportunityId");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_Quotations_OrganizationId");

            entity.HasIndex(e => e.QuotationDate, "IX_CRM_Quotations_QuotationDate");

            entity.HasIndex(e => e.RegionId, "IX_CRM_Quotations_RegionId");

            entity.HasIndex(e => e.Status, "IX_CRM_Quotations_Status");

            entity.HasIndex(e => e.UserId, "IX_CRM_Quotations_UserId");

            entity.HasIndex(e => e.ValidUntil, "IX_CRM_Quotations_ValidUntil");

            entity.HasIndex(e => new { e.OrganizationId, e.QuotationNumber }, "UQ_CRM_Quotations_QuotationNumber").IsUnique();

            entity.Property(e => e.QuotationId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.QuotationNumber).HasMaxLength(50);
            entity.Property(e => e.RejectionReason).HasMaxLength(500);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VersionNumber).HasDefaultValue(1);

            entity.HasOne(d => d.Company).WithMany(p => p.QuotationCompanies)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Quotations_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.QuotationCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Quotations_CreatedBy");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.QuotationCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Quotations_CreatedByUser");

            entity.HasOne(d => d.Customer).WithMany(p => p.Quotations)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CRM_Quotations_Customer");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.QuotationModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_Quotations_ModifiedBy");

            entity.HasOne(d => d.Opportunity).WithMany(p => p.Quotations)
                .HasForeignKey(d => d.OpportunityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Quotations_Opportunity");

            entity.HasOne(d => d.Organization).WithMany(p => p.QuotationOrganizations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Quotations_Organizations");

            entity.HasOne(d => d.User).WithMany(p => p.QuotationUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Quotations_User");
        });

        modelBuilder.Entity<QuotationApproval>(entity =>
        {
            entity.HasKey(e => e.QuotationApprovalId).HasName("PK_CRM_QuotationApprovals");

            entity.ToTable("QuotationApprovals", "CRM");

            entity.HasIndex(e => e.ApprovalLevel, "IX_CRM_QuotationApprovals_ApprovalLevel");

            entity.HasIndex(e => e.ApprovalStatus, "IX_CRM_QuotationApprovals_ApprovalStatus");

            entity.HasIndex(e => e.ApproverUserId, "IX_CRM_QuotationApprovals_ApproverUserId");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_QuotationApprovals_CompanyId");

            entity.HasIndex(e => e.QuotationId, "IX_CRM_QuotationApprovals_QuotationId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_QuotationApprovals_RegionId");

            entity.HasIndex(e => e.RequestedOn, "IX_CRM_QuotationApprovals_RequestedOn");

            entity.HasIndex(e => e.UserId, "IX_CRM_QuotationApprovals_UserId");

            entity.HasIndex(e => new { e.QuotationId, e.ApprovalLevel }, "UQ_CRM_QuotationApprovals").IsUnique();

            entity.Property(e => e.QuotationApprovalId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ApprovalRemarks).HasMaxLength(1000);
            entity.Property(e => e.ApprovalStatus).HasDefaultValue((byte)1);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.RequestedOn).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.ApproverUser).WithMany(p => p.QuotationApprovalApproverUsers)
                .HasForeignKey(d => d.ApproverUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationApprovals_ApproverUser");

            entity.HasOne(d => d.Company).WithMany(p => p.QuotationApprovals)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationApprovals_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.QuotationApprovalCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationApprovals_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.QuotationApprovalModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_QuotationApprovals_ModifiedBy");

            entity.HasOne(d => d.Quotation).WithMany(p => p.QuotationApprovals)
                .HasForeignKey(d => d.QuotationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationApprovals_Quotation");

            entity.HasOne(d => d.User).WithMany(p => p.QuotationApprovalUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationApprovals_User");
        });

        modelBuilder.Entity<QuotationItem>(entity =>
        {
            entity.HasKey(e => e.QuotationItemId).HasName("PK_CRM_QuotationItems");

            entity.ToTable("QuotationItems", "CRM");

            entity.HasIndex(e => e.AddOnId, "IX_CRM_QuotationItems_AddOnId");

            entity.HasIndex(e => e.BillingCycle, "IX_CRM_QuotationItems_BillingCycle");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_QuotationItems_CompanyId");

            entity.HasIndex(e => e.ItemType, "IX_CRM_QuotationItems_ItemType");

            entity.HasIndex(e => e.ProductId, "IX_CRM_QuotationItems_ProductId");

            entity.HasIndex(e => e.QuotationId, "IX_CRM_QuotationItems_QuotationId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_QuotationItems_RegionId");

            entity.HasIndex(e => e.UserId, "IX_CRM_QuotationItems_UserId");

            entity.HasIndex(e => new { e.QuotationId, e.ItemType, e.ProductId, e.AddOnId }, "UQ_CRM_QuotationItems").IsUnique();

            entity.Property(e => e.QuotationItemId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ItemName).HasMaxLength(250);
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AddOn).WithMany(p => p.QuotationItems)
                .HasForeignKey(d => d.AddOnId)
                .HasConstraintName("FK_CRM_QuotationItems_AddOn");

            entity.HasOne(d => d.Company).WithMany(p => p.QuotationItems)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationItems_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.QuotationItemCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationItems_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.QuotationItemModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_QuotationItems_ModifiedBy");

            entity.HasOne(d => d.Product).WithMany(p => p.QuotationItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_CRM_QuotationItems_Product");

            entity.HasOne(d => d.Quotation).WithMany(p => p.QuotationItems)
                .HasForeignKey(d => d.QuotationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationItems_Quotation");

            entity.HasOne(d => d.User).WithMany(p => p.QuotationItemUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationItems_User");
        });

        modelBuilder.Entity<QuotationVersion>(entity =>
        {
            entity.HasKey(e => e.QuotationVersionId).HasName("PK_CRM_QuotationVersions");

            entity.ToTable("QuotationVersions", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_QuotationVersions_CompanyId");

            entity.HasIndex(e => e.QuotationId, "IX_CRM_QuotationVersions_QuotationId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_QuotationVersions_RegionId");

            entity.HasIndex(e => e.UserId, "IX_CRM_QuotationVersions_UserId");

            entity.HasIndex(e => e.VersionDate, "IX_CRM_QuotationVersions_VersionDate");

            entity.HasIndex(e => e.VersionNumber, "IX_CRM_QuotationVersions_VersionNumber");

            entity.HasIndex(e => new { e.QuotationId, e.VersionNumber }, "UQ_CRM_QuotationVersions").IsUnique();

            entity.Property(e => e.QuotationVersionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ChangeReason).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.VersionDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Company).WithMany(p => p.QuotationVersions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationVersions_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.QuotationVersionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationVersions_CreatedBy");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.QuotationVersionCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationVersions_CreatedByUser");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.QuotationVersionModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_QuotationVersions_ModifiedBy");

            entity.HasOne(d => d.Quotation).WithMany(p => p.QuotationVersions)
                .HasForeignKey(d => d.QuotationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationVersions_Quotation");

            entity.HasOne(d => d.User).WithMany(p => p.QuotationVersionUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_QuotationVersions_User");
        });

        modelBuilder.Entity<Refund>(entity =>
        {
            entity.HasKey(e => e.RefundId).HasName("PK_CRM_Refunds");

            entity.ToTable("Refunds", "CRM");

            entity.HasIndex(e => e.ApprovedByUserId, "IX_CRM_Refunds_ApprovedByUserId");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_Refunds_CompanyId");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_Refunds_CustomerId");

            entity.HasIndex(e => e.PaymentId, "IX_CRM_Refunds_PaymentId");

            entity.HasIndex(e => e.RefundDate, "IX_CRM_Refunds_RefundDate");

            entity.HasIndex(e => e.RefundNumber, "IX_CRM_Refunds_RefundNumber");

            entity.HasIndex(e => e.RegionId, "IX_CRM_Refunds_RegionId");

            entity.HasIndex(e => e.Status, "IX_CRM_Refunds_Status");

            entity.HasIndex(e => e.UserId, "IX_CRM_Refunds_UserId");

            entity.HasIndex(e => new { e.CompanyId, e.RefundNumber }, "UQ_CRM_Refunds").IsUnique();

            entity.Property(e => e.RefundId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.RefundAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RefundNumber).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.TransactionReference).HasMaxLength(150);

            entity.HasOne(d => d.ApprovedByUser).WithMany(p => p.RefundApprovedByUsers)
                .HasForeignKey(d => d.ApprovedByUserId)
                .HasConstraintName("FK_CRM_Refunds_ApprovedByUser");

            entity.HasOne(d => d.Company).WithMany(p => p.Refunds)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Refunds_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RefundCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Refunds_CreatedBy");

            entity.HasOne(d => d.Customer).WithMany(p => p.Refunds)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Refunds_Customer");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.RefundModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_Refunds_ModifiedBy");

            entity.HasOne(d => d.Payment).WithMany(p => p.Refunds)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Refunds_Payment");

            entity.HasOne(d => d.User).WithMany(p => p.RefundUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Refunds_User");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("PK__Region__ACD84443E8644F56");

            entity.ToTable("Region", "Security");

            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.ContactPerson).HasMaxLength(100);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RegionCode).HasMaxLength(50);
            entity.Property(e => e.RegionName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Company).WithMany(p => p.Regions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Region__CompanyI__11BF94B6");
        });

        modelBuilder.Entity<Relationship>(entity =>
        {
            entity.HasKey(e => e.RelationshipId).HasName("PK__Relation__31FEB861686BC27A");

            entity.ToTable("Relationship", "Masters");

            entity.Property(e => e.RelationshipId).HasColumnName("RelationshipID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.RelationshipCode).HasMaxLength(150);
            entity.Property(e => e.RelationshipName).HasMaxLength(150);
        });

        modelBuilder.Entity<RetentionPeriod>(entity =>
        {
            entity.HasKey(e => e.RetentionPeriodId).HasName("PK__Retentio__E3A25FA9F04C8197");

            entity.ToTable("RetentionPeriod", "Masters");

            entity.Property(e => e.RetentionPeriodId).HasColumnName("RetentionPeriodID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.RetentionPeriodCode).HasMaxLength(150);
            entity.Property(e => e.RetentionPeriodName).HasMaxLength(150);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A9D5CC512");

            entity.ToTable("Roles", "Master");

            entity.HasIndex(e => e.RoleCode, "UQ__Roles__D62CB59CB60B7C8E").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.RoleCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoleType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Role1>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Security_Roles");

            entity.ToTable("Roles", "Security");

            entity.HasIndex(e => e.IsSystemRole, "IX_Security_Roles_IsSystemRole");

            entity.HasIndex(e => e.OrganizationId, "IX_Security_Roles_OrganizationId");

            entity.HasIndex(e => e.RoleCode, "IX_Security_Roles_RoleCode");

            entity.HasIndex(e => e.RoleName, "IX_Security_Roles_RoleName");

            entity.HasIndex(e => e.Status, "IX_Security_Roles_Status");

            entity.HasIndex(e => new { e.OrganizationId, e.RoleCode }, "UQ_Security_Roles_OrganizationId_RoleCode").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.RoleName }, "UQ_Security_Roles_OrganizationId_RoleName").IsUnique();

            entity.Property(e => e.RoleId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RoleCode).HasMaxLength(50);
            entity.Property(e => e.RoleName).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Organization).WithMany(p => p.Role1s)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_Security_Roles_Organizations");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.RolePermissionId).HasName("PK__RolePerm__120F46BA60B808D7");

            entity.ToTable("RolePermissions", "Security");

            entity.HasIndex(e => new { e.RoleId, e.PermissionId }, "UQ_RolePermission").IsUnique();

            entity.Property(e => e.CanAdd).HasDefaultValue(false);
            entity.Property(e => e.CanApprove).HasDefaultValue(false);
            entity.Property(e => e.CanDelete).HasDefaultValue(false);
            entity.Property(e => e.CanEdit).HasDefaultValue(false);
            entity.Property(e => e.CanExport).HasDefaultValue(false);
            entity.Property(e => e.CanView).HasDefaultValue(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<RolesPermission>(entity =>
        {
            entity.HasKey(e => e.RolePermissionId).HasName("PK__RolesPer__120F46BAE916B589");

            entity.ToTable("RolesPermissions", "Security");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<SalesOrder>(entity =>
        {
            entity.HasKey(e => e.SalesOrderId).HasName("PK_CRM_SalesOrders");

            entity.ToTable("SalesOrders", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_SalesOrders_CompanyId");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_SalesOrders_CustomerId");

            entity.HasIndex(e => e.OrderDate, "IX_CRM_SalesOrders_OrderDate");

            entity.HasIndex(e => e.OrderStatus, "IX_CRM_SalesOrders_OrderStatus");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_SalesOrders_OrganizationId");

            entity.HasIndex(e => e.QuotationId, "IX_CRM_SalesOrders_QuotationId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_SalesOrders_RegionId");

            entity.HasIndex(e => e.SalesOrderNumber, "IX_CRM_SalesOrders_SalesOrderNumber");

            entity.HasIndex(e => e.UserId, "IX_CRM_SalesOrders_UserId");

            entity.HasIndex(e => new { e.OrganizationId, e.SalesOrderNumber }, "UQ_CRM_SalesOrders_SalesOrderNumber").IsUnique();

            entity.Property(e => e.SalesOrderId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OrderStatus).HasDefaultValue((byte)1);
            entity.Property(e => e.SalesOrderNumber).HasMaxLength(50);
            entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Company).WithMany(p => p.SalesOrderCompanies)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SalesOrders_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SalesOrderCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SalesOrders_CreatedBy");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.SalesOrderCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SalesOrders_CreatedByUser");

            entity.HasOne(d => d.Customer).WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SalesOrders_Customer");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SalesOrderModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_SalesOrders_ModifiedBy");

            entity.HasOne(d => d.Organization).WithMany(p => p.SalesOrderOrganizations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SalesOrders_Organizations");

            entity.HasOne(d => d.Quotation).WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.QuotationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SalesOrders_Quotation");

            entity.HasOne(d => d.User).WithMany(p => p.SalesOrderUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SalesOrders_User");
        });

        modelBuilder.Entity<SalesOrderItem>(entity =>
        {
            entity.HasKey(e => e.SalesOrderItemId).HasName("PK_CRM_SalesOrderItems");

            entity.ToTable("SalesOrderItems", "CRM");

            entity.HasIndex(e => e.AddOnId, "IX_CRM_SalesOrderItems_AddOnId");

            entity.HasIndex(e => e.BillingCycle, "IX_CRM_SalesOrderItems_BillingCycle");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_SalesOrderItems_CompanyId");

            entity.HasIndex(e => e.ProductId, "IX_CRM_SalesOrderItems_ProductId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_SalesOrderItems_RegionId");

            entity.HasIndex(e => e.SalesOrderId, "IX_CRM_SalesOrderItems_SalesOrderId");

            entity.HasIndex(e => e.UserId, "IX_CRM_SalesOrderItems_UserId");

            entity.HasIndex(e => new { e.SalesOrderId, e.ProductId, e.AddOnId }, "UQ_CRM_SalesOrderItems").IsUnique();

            entity.Property(e => e.SalesOrderItemId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ItemName).HasMaxLength(250);
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AddOn).WithMany(p => p.SalesOrderItems)
                .HasForeignKey(d => d.AddOnId)
                .HasConstraintName("FK_CRM_SalesOrderItems_AddOn");

            entity.HasOne(d => d.Company).WithMany(p => p.SalesOrderItems)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SalesOrderItems_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SalesOrderItemCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SalesOrderItems_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SalesOrderItemModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_SalesOrderItems_ModifiedBy");

            entity.HasOne(d => d.Product).WithMany(p => p.SalesOrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_CRM_SalesOrderItems_Product");

            entity.HasOne(d => d.SalesOrder).WithMany(p => p.SalesOrderItems)
                .HasForeignKey(d => d.SalesOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SalesOrderItems_SalesOrder");

            entity.HasOne(d => d.User).WithMany(p => p.SalesOrderItemUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SalesOrderItems_User");
        });

        modelBuilder.Entity<ScheduledJob>(entity =>
        {
            entity.ToTable("ScheduledJobs", "Superadmin");

            entity.Property(e => e.ScheduledJobId).HasColumnName("ScheduledJobID");
            entity.Property(e => e.ActionType).HasMaxLength(100);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Frequency).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.JobName).HasMaxLength(200);
            entity.Property(e => e.JobType).HasMaxLength(100);
            entity.Property(e => e.LastRunAt).HasColumnType("datetime");
            entity.Property(e => e.LastRunStatus).HasMaxLength(50);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.NextRunAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.RetryCount).HasDefaultValue(0);
            entity.Property(e => e.TimeoutMinutes).HasDefaultValue(30);
        });

        modelBuilder.Entity<Slarule>(entity =>
        {
            entity.ToTable("SLARules", "Superadmin");

            entity.Property(e => e.SlaruleId).HasColumnName("SLARuleID");
            entity.Property(e => e.BusinessHoursId).HasColumnName("BusinessHoursID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EscalationRuleId).HasColumnName("EscalationRuleID");
            entity.Property(e => e.HolidayCalendarId).HasColumnName("HolidayCalendarID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModuleName).HasMaxLength(100);
            entity.Property(e => e.Priority).HasMaxLength(50);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.Slaname)
                .HasMaxLength(200)
                .HasColumnName("SLAName");

            entity.HasOne(d => d.EscalationRule).WithMany(p => p.Slarules)
                .HasForeignKey(d => d.EscalationRuleId)
                .HasConstraintName("FK_SLARules_EscalationRules");
        });

        modelBuilder.Entity<Slasetting>(entity =>
        {
            entity.HasKey(e => e.SlasettingId).HasName("PK_CRM_SLASettings");

            entity.ToTable("SLASettings", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_SLASettings_CompanyId");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_SLASettings_OrganizationId");

            entity.HasIndex(e => e.Priority, "IX_CRM_SLASettings_Priority");

            entity.HasIndex(e => e.RegionId, "IX_CRM_SLASettings_RegionId");

            entity.HasIndex(e => e.Status, "IX_CRM_SLASettings_Status");

            entity.HasIndex(e => e.UserId, "IX_CRM_SLASettings_UserId");

            entity.HasIndex(e => new { e.OrganizationId, e.Slaname }, "UQ_CRM_SLASettings_SLAName").IsUnique();

            entity.Property(e => e.SlasettingId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("SLASettingId");
            entity.Property(e => e.BusinessHoursOnly).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Slaname)
                .HasMaxLength(150)
                .HasColumnName("SLAName");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SlasettingCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SLASettings_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SlasettingModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_SLASettings_ModifiedBy");

            entity.HasOne(d => d.Organization).WithMany(p => p.Slasettings)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SLASettings_Organization");

            entity.HasOne(d => d.User).WithMany(p => p.SlasettingUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SLASettings_User");
        });

        modelBuilder.Entity<Smstemplate>(entity =>
        {
            entity.HasKey(e => e.SmstemplateId).HasName("PK_CRM_SMSTemplates");

            entity.ToTable("SMSTemplates", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_SMSTemplates_CompanyId");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_SMSTemplates_OrganizationId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_SMSTemplates_RegionId");

            entity.HasIndex(e => e.Status, "IX_CRM_SMSTemplates_Status");

            entity.HasIndex(e => e.TemplateCode, "IX_CRM_SMSTemplates_TemplateCode");

            entity.HasIndex(e => e.UserId, "IX_CRM_SMSTemplates_UserId");

            entity.HasIndex(e => new { e.OrganizationId, e.TemplateCode }, "UQ_CRM_SMSTemplates_TemplateCode").IsUnique();

            entity.Property(e => e.SmstemplateId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("SMSTemplateId");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DlttemplateId)
                .HasMaxLength(100)
                .HasColumnName("DLTTemplateId");
            entity.Property(e => e.MessageText).HasMaxLength(1000);
            entity.Property(e => e.TemplateCode).HasMaxLength(100);
            entity.Property(e => e.TemplateName).HasMaxLength(150);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SmstemplateCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SMSTemplates_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SmstemplateModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_SMSTemplates_ModifiedBy");

            entity.HasOne(d => d.User).WithMany(p => p.SmstemplateUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SMSTemplates_User");
        });

        modelBuilder.Entity<StateMaster>(entity =>
        {
            entity.HasKey(e => e.StateId);

            entity.ToTable("StateMaster", "Masters");

            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.StateCode).HasMaxLength(150);
            entity.Property(e => e.StateName).HasMaxLength(150);
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK_CRM_Subscriptions");

            entity.ToTable("Subscriptions", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_Subscriptions_CompanyId");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_Subscriptions_CustomerId");

            entity.HasIndex(e => e.EndDate, "IX_CRM_Subscriptions_EndDate");

            entity.HasIndex(e => e.RegionId, "IX_CRM_Subscriptions_RegionId");

            entity.HasIndex(e => e.SalesOrderId, "IX_CRM_Subscriptions_SalesOrderId");

            entity.HasIndex(e => e.StartDate, "IX_CRM_Subscriptions_StartDate");

            entity.HasIndex(e => e.Status, "IX_CRM_Subscriptions_Status");

            entity.HasIndex(e => e.SubscriptionNumber, "IX_CRM_Subscriptions_SubscriptionNumber");

            entity.HasIndex(e => e.SubscriptionPlanId, "IX_CRM_Subscriptions_SubscriptionPlanId");

            entity.HasIndex(e => e.UserId, "IX_CRM_Subscriptions_UserId");

            entity.HasIndex(e => new { e.CompanyId, e.SubscriptionNumber }, "UQ_CRM_Subscriptions").IsUnique();

            entity.Property(e => e.SubscriptionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AdditionalStorageGb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("AdditionalStorageGB");
            entity.Property(e => e.AutoRenew).HasDefaultValue(true);
            entity.Property(e => e.CancellationReason).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IncludedStorageGb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("IncludedStorageGB");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.SubscriptionNumber).HasMaxLength(50);
            entity.Property(e => e.TotalStorageLimitGb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TotalStorageLimitGB");

            entity.HasOne(d => d.Company).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Subscriptions_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SubscriptionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Subscriptions_CreatedBy");

            entity.HasOne(d => d.Customer).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Subscriptions_Customer");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SubscriptionModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_Subscriptions_ModifiedBy");

            entity.HasOne(d => d.SalesOrder).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.SalesOrderId)
                .HasConstraintName("FK_CRM_Subscriptions_SalesOrder");

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Subscriptions_SubscriptionPlan");

            entity.HasOne(d => d.User).WithMany(p => p.SubscriptionUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_Subscriptions_User");
        });

        modelBuilder.Entity<SubscriptionDowngrade>(entity =>
        {
            entity.HasKey(e => e.SubscriptionDowngradeId).HasName("PK_CRM_SubscriptionDowngrades");

            entity.ToTable("SubscriptionDowngrades", "CRM");

            entity.HasIndex(e => e.ApprovedByUserId, "IX_CRM_SubscriptionDowngrades_ApprovedByUserId");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_SubscriptionDowngrades_CompanyId");

            entity.HasIndex(e => e.EffectiveDate, "IX_CRM_SubscriptionDowngrades_EffectiveDate");

            entity.HasIndex(e => e.NewPlanId, "IX_CRM_SubscriptionDowngrades_NewPlanId");

            entity.HasIndex(e => e.PreviousPlanId, "IX_CRM_SubscriptionDowngrades_PreviousPlanId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_SubscriptionDowngrades_RegionId");

            entity.HasIndex(e => e.RequestedOn, "IX_CRM_SubscriptionDowngrades_RequestedOn");

            entity.HasIndex(e => e.Status, "IX_CRM_SubscriptionDowngrades_Status");

            entity.HasIndex(e => e.SubscriptionId, "IX_CRM_SubscriptionDowngrades_SubscriptionId");

            entity.HasIndex(e => e.UserId, "IX_CRM_SubscriptionDowngrades_UserId");

            entity.HasIndex(e => new { e.SubscriptionId, e.RequestedOn }, "UQ_CRM_SubscriptionDowngrades").IsUnique();

            entity.Property(e => e.SubscriptionDowngradeId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.RequestedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.ApprovedByUser).WithMany(p => p.SubscriptionDowngradeApprovedByUsers)
                .HasForeignKey(d => d.ApprovedByUserId)
                .HasConstraintName("FK_CRM_SubscriptionDowngrades_ApprovedByUser");

            entity.HasOne(d => d.Company).WithMany(p => p.SubscriptionDowngrades)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionDowngrades_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SubscriptionDowngradeCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionDowngrades_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SubscriptionDowngradeModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_SubscriptionDowngrades_ModifiedBy");

            entity.HasOne(d => d.NewPlan).WithMany(p => p.SubscriptionDowngradeNewPlans)
                .HasForeignKey(d => d.NewPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionDowngrades_NewPlan");

            entity.HasOne(d => d.PreviousPlan).WithMany(p => p.SubscriptionDowngradePreviousPlans)
                .HasForeignKey(d => d.PreviousPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionDowngrades_PreviousPlan");

            entity.HasOne(d => d.Subscription).WithMany(p => p.SubscriptionDowngrades)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionDowngrades_Subscription");

            entity.HasOne(d => d.User).WithMany(p => p.SubscriptionDowngradeUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionDowngrades_User");
        });

        modelBuilder.Entity<SubscriptionItem>(entity =>
        {
            entity.HasKey(e => e.SubscriptionItemId).HasName("PK_CRM_SubscriptionItems");

            entity.ToTable("SubscriptionItems", "CRM");

            entity.HasIndex(e => e.AddOnId, "IX_CRM_SubscriptionItems_AddOnId");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_SubscriptionItems_CompanyId");

            entity.HasIndex(e => e.EndDate, "IX_CRM_SubscriptionItems_EndDate");

            entity.HasIndex(e => e.ProductId, "IX_CRM_SubscriptionItems_ProductId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_SubscriptionItems_RegionId");

            entity.HasIndex(e => e.StartDate, "IX_CRM_SubscriptionItems_StartDate");

            entity.HasIndex(e => e.Status, "IX_CRM_SubscriptionItems_Status");

            entity.HasIndex(e => e.SubscriptionId, "IX_CRM_SubscriptionItems_SubscriptionId");

            entity.HasIndex(e => e.UserId, "IX_CRM_SubscriptionItems_UserId");

            entity.HasIndex(e => new { e.SubscriptionId, e.ProductId, e.AddOnId }, "UQ_CRM_SubscriptionItems").IsUnique();

            entity.Property(e => e.SubscriptionItemId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsRecurring).HasDefaultValue(true);
            entity.Property(e => e.ItemName).HasMaxLength(250);
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AddOn).WithMany(p => p.SubscriptionItems)
                .HasForeignKey(d => d.AddOnId)
                .HasConstraintName("FK_CRM_SubscriptionItems_AddOn");

            entity.HasOne(d => d.Company).WithMany(p => p.SubscriptionItems)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionItems_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SubscriptionItemCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionItems_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SubscriptionItemModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_SubscriptionItems_ModifiedBy");

            entity.HasOne(d => d.Product).WithMany(p => p.SubscriptionItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_CRM_SubscriptionItems_Product");

            entity.HasOne(d => d.Subscription).WithMany(p => p.SubscriptionItems)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionItems_Subscription");

            entity.HasOne(d => d.User).WithMany(p => p.SubscriptionItemUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionItems_User");
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(e => e.SubscriptionPlanId).HasName("PK_CRM_SubscriptionPlans");

            entity.ToTable("SubscriptionPlans", "CRM");

            entity.HasIndex(e => e.BasePrice, "IX_CRM_SubscriptionPlans_BasePrice");

            entity.HasIndex(e => e.BillingCycle, "IX_CRM_SubscriptionPlans_BillingCycle");

            entity.HasIndex(e => e.IsPopular, "IX_CRM_SubscriptionPlans_IsPopular");

            entity.HasIndex(e => e.PlanCode, "IX_CRM_SubscriptionPlans_PlanCode");

            entity.HasIndex(e => e.PlanName, "IX_CRM_SubscriptionPlans_PlanName");

            entity.HasIndex(e => e.ProductId, "IX_CRM_SubscriptionPlans_ProductId");

            entity.HasIndex(e => e.Status, "IX_CRM_SubscriptionPlans_Status");

            entity.HasIndex(e => new { e.ProductId, e.PlanCode }, "UQ_CRM_SubscriptionPlans_Product_PlanCode").IsUnique();

            entity.HasIndex(e => new { e.ProductId, e.PlanName }, "UQ_CRM_SubscriptionPlans_Product_PlanName").IsUnique();

            entity.Property(e => e.SubscriptionPlanId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ApirequestLimit).HasColumnName("APIRequestLimit");
            entity.Property(e => e.BasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IncludedStorageGb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("IncludedStorageGB");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.PlanCode).HasMaxLength(50);
            entity.Property(e => e.PlanName).HasMaxLength(150);
            entity.Property(e => e.Smslimit).HasColumnName("SMSLimit");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Product).WithMany(p => p.SubscriptionPlans)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionPlans_Products");
        });

        modelBuilder.Entity<SubscriptionPlanMaster>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Subscrip__755C22D75C29021A");

            entity.ToTable("SubscriptionPlanMaster", "plans");

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.Accent)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("blue");
            entity.Property(e => e.ApiLimit).HasDefaultValue(10000);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PlanName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.StorageLimit).HasDefaultValue(10);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<SubscriptionRenewal>(entity =>
        {
            entity.HasKey(e => e.SubscriptionRenewalId).HasName("PK_CRM_SubscriptionRenewals");

            entity.ToTable("SubscriptionRenewals", "CRM");

            entity.HasIndex(e => e.AssignedSalesUserId, "IX_CRM_SubscriptionRenewals_AssignedSalesUserId");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_SubscriptionRenewals_CompanyId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_SubscriptionRenewals_RegionId");

            entity.HasIndex(e => e.RenewalDate, "IX_CRM_SubscriptionRenewals_RenewalDate");

            entity.HasIndex(e => e.RenewalInvoiceId, "IX_CRM_SubscriptionRenewals_RenewalInvoiceId");

            entity.HasIndex(e => e.RenewalQuotationId, "IX_CRM_SubscriptionRenewals_RenewalQuotationId");

            entity.HasIndex(e => e.Status, "IX_CRM_SubscriptionRenewals_Status");

            entity.HasIndex(e => e.SubscriptionId, "IX_CRM_SubscriptionRenewals_SubscriptionId");

            entity.HasIndex(e => e.UserId, "IX_CRM_SubscriptionRenewals_UserId");

            entity.HasIndex(e => new { e.SubscriptionId, e.RenewalDate }, "UQ_CRM_SubscriptionRenewals").IsUnique();

            entity.Property(e => e.SubscriptionRenewalId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.RenewalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.AssignedSalesUser).WithMany(p => p.SubscriptionRenewalAssignedSalesUsers)
                .HasForeignKey(d => d.AssignedSalesUserId)
                .HasConstraintName("FK_CRM_SubscriptionRenewals_AssignedSalesUser");

            entity.HasOne(d => d.Company).WithMany(p => p.SubscriptionRenewals)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionRenewals_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SubscriptionRenewalCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionRenewals_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SubscriptionRenewalModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_SubscriptionRenewals_ModifiedBy");

            entity.HasOne(d => d.RenewalInvoice).WithMany(p => p.SubscriptionRenewals)
                .HasForeignKey(d => d.RenewalInvoiceId)
                .HasConstraintName("FK_CRM_SubscriptionRenewals_Invoice");

            entity.HasOne(d => d.RenewalQuotation).WithMany(p => p.SubscriptionRenewals)
                .HasForeignKey(d => d.RenewalQuotationId)
                .HasConstraintName("FK_CRM_SubscriptionRenewals_Quotation");

            entity.HasOne(d => d.Subscription).WithMany(p => p.SubscriptionRenewals)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionRenewals_Subscription");

            entity.HasOne(d => d.User).WithMany(p => p.SubscriptionRenewalUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionRenewals_User");
        });

        modelBuilder.Entity<SubscriptionUpgrade>(entity =>
        {
            entity.HasKey(e => e.SubscriptionUpgradeId).HasName("PK_CRM_SubscriptionUpgrades");

            entity.ToTable("SubscriptionUpgrades", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_SubscriptionUpgrades_CompanyId");

            entity.HasIndex(e => e.InvoiceId, "IX_CRM_SubscriptionUpgrades_InvoiceId");

            entity.HasIndex(e => e.NewPlanId, "IX_CRM_SubscriptionUpgrades_NewPlanId");

            entity.HasIndex(e => e.PreviousPlanId, "IX_CRM_SubscriptionUpgrades_PreviousPlanId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_SubscriptionUpgrades_RegionId");

            entity.HasIndex(e => e.Status, "IX_CRM_SubscriptionUpgrades_Status");

            entity.HasIndex(e => e.SubscriptionId, "IX_CRM_SubscriptionUpgrades_SubscriptionId");

            entity.HasIndex(e => e.UpgradeDate, "IX_CRM_SubscriptionUpgrades_UpgradeDate");

            entity.HasIndex(e => e.UserId, "IX_CRM_SubscriptionUpgrades_UserId");

            entity.HasIndex(e => new { e.SubscriptionId, e.UpgradeDate }, "UQ_CRM_SubscriptionUpgrades").IsUnique();

            entity.Property(e => e.SubscriptionUpgradeId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ProratedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Company).WithMany(p => p.SubscriptionUpgrades)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionUpgrades_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SubscriptionUpgradeCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionUpgrades_CreatedBy");

            entity.HasOne(d => d.Invoice).WithMany(p => p.SubscriptionUpgrades)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK_CRM_SubscriptionUpgrades_Invoice");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SubscriptionUpgradeModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_SubscriptionUpgrades_ModifiedBy");

            entity.HasOne(d => d.NewPlan).WithMany(p => p.SubscriptionUpgradeNewPlans)
                .HasForeignKey(d => d.NewPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionUpgrades_NewPlan");

            entity.HasOne(d => d.PreviousPlan).WithMany(p => p.SubscriptionUpgradePreviousPlans)
                .HasForeignKey(d => d.PreviousPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionUpgrades_PreviousPlan");

            entity.HasOne(d => d.Subscription).WithMany(p => p.SubscriptionUpgrades)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionUpgrades_Subscription");

            entity.HasOne(d => d.User).WithMany(p => p.SubscriptionUpgradeUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionUpgrades_User");
        });

        modelBuilder.Entity<SubscriptionUsage>(entity =>
        {
            entity.HasKey(e => e.SubscriptionUsageId).HasName("PK_CRM_SubscriptionUsage");

            entity.ToTable("SubscriptionUsage", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_SubscriptionUsage_CompanyId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_SubscriptionUsage_RegionId");

            entity.HasIndex(e => e.SubscriptionId, "IX_CRM_SubscriptionUsage_SubscriptionId");

            entity.HasIndex(e => e.UsageDate, "IX_CRM_SubscriptionUsage_UsageDate");

            entity.HasIndex(e => e.UserId, "IX_CRM_SubscriptionUsage_UserId");

            entity.HasIndex(e => new { e.SubscriptionId, e.UsageDate }, "UQ_CRM_SubscriptionUsage").IsUnique();

            entity.Property(e => e.SubscriptionUsageId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ApirequestsCount).HasColumnName("APIRequestsCount");
            entity.Property(e => e.CallingMinutesUsed).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.SmsusedCount).HasColumnName("SMSUsedCount");
            entity.Property(e => e.StorageUsedGb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("StorageUsedGB");

            entity.HasOne(d => d.Company).WithMany(p => p.SubscriptionUsages)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionUsage_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SubscriptionUsageCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionUsage_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SubscriptionUsageModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_SubscriptionUsage_ModifiedBy");

            entity.HasOne(d => d.Subscription).WithMany(p => p.SubscriptionUsages)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionUsage_Subscription");

            entity.HasOne(d => d.User).WithMany(p => p.SubscriptionUsageUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SubscriptionUsage_User");
        });

        modelBuilder.Entity<SupportTicket>(entity =>
        {
            entity.HasKey(e => e.SupportTicketId).HasName("PK_CRM_SupportTickets");

            entity.ToTable("SupportTickets", "CRM");

            entity.HasIndex(e => e.AssignedToUserId, "IX_CRM_SupportTickets_AssignedToUserId");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_SupportTickets_CompanyId");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_SupportTickets_CustomerId");

            entity.HasIndex(e => e.CustomerTenantId, "IX_CRM_SupportTickets_CustomerTenantId");

            entity.HasIndex(e => e.DueOn, "IX_CRM_SupportTickets_DueOn");

            entity.HasIndex(e => e.Priority, "IX_CRM_SupportTickets_Priority");

            entity.HasIndex(e => e.RegionId, "IX_CRM_SupportTickets_RegionId");

            entity.HasIndex(e => e.SlasettingId, "IX_CRM_SupportTickets_SLASettingId");

            entity.HasIndex(e => e.Status, "IX_CRM_SupportTickets_Status");

            entity.HasIndex(e => e.UserId, "IX_CRM_SupportTickets_UserId");

            entity.HasIndex(e => e.TicketNumber, "UQ_CRM_SupportTickets_TicketNumber").IsUnique();

            entity.Property(e => e.SupportTicketId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.SlasettingId).HasColumnName("SLASettingId");
            entity.Property(e => e.Subject).HasMaxLength(250);
            entity.Property(e => e.TicketNumber).HasMaxLength(50);

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.SupportTicketAssignedToUsers)
                .HasForeignKey(d => d.AssignedToUserId)
                .HasConstraintName("FK_CRM_SupportTickets_AssignedUser");

            entity.HasOne(d => d.Company).WithMany(p => p.SupportTicketCompanies)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SupportTickets_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SupportTicketCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SupportTickets_CreatedBy");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.SupportTicketCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SupportTickets_CreatedByUser");

            entity.HasOne(d => d.Customer).WithMany(p => p.SupportTickets)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SupportTickets_Customer");

            entity.HasOne(d => d.CustomerTenant).WithMany(p => p.SupportTickets)
                .HasForeignKey(d => d.CustomerTenantId)
                .HasConstraintName("FK_CRM_SupportTickets_CustomerTenant");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SupportTicketModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_SupportTickets_ModifiedBy");

            entity.HasOne(d => d.Organization).WithMany(p => p.SupportTicketOrganizations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SupportTickets_Organization");

            entity.HasOne(d => d.User).WithMany(p => p.SupportTicketUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_SupportTickets_User");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK_Masters_Teams");

            entity.ToTable("Teams", "Masters");

            entity.HasIndex(e => e.BranchId, "IX_Masters_Teams_BranchId");

            entity.HasIndex(e => e.DepartmentId, "IX_Masters_Teams_DepartmentId");

            entity.HasIndex(e => e.OrganizationId, "IX_Masters_Teams_OrganizationId");

            entity.HasIndex(e => e.Status, "IX_Masters_Teams_Status");

            entity.HasIndex(e => e.TeamLeadUserId, "IX_Masters_Teams_TeamLeadUserId");

            entity.HasIndex(e => e.TeamName, "IX_Masters_Teams_TeamName");

            entity.HasIndex(e => new { e.OrganizationId, e.TeamCode }, "UQ_Masters_Teams_OrganizationId_TeamCode").IsUnique();

            entity.Property(e => e.TeamId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.TeamCode).HasMaxLength(50);
            entity.Property(e => e.TeamName).HasMaxLength(150);

            entity.HasOne(d => d.Branch).WithMany(p => p.Teams)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK_Masters_Teams_Branches");

            entity.HasOne(d => d.Department).WithMany(p => p.Teams)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Masters_Teams_Departments");

            entity.HasOne(d => d.Organization).WithMany(p => p.Teams)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Masters_Teams_Organizations");
        });

        modelBuilder.Entity<TenantModule>(entity =>
        {
            entity.HasKey(e => e.TenantModuleId).HasName("PK_CRM_TenantModules");

            entity.ToTable("TenantModules", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_TenantModules_CompanyId");

            entity.HasIndex(e => e.CustomerTenantId, "IX_CRM_TenantModules_CustomerTenantId");

            entity.HasIndex(e => e.IsEnabled, "IX_CRM_TenantModules_IsEnabled");

            entity.HasIndex(e => e.ModuleCode, "IX_CRM_TenantModules_ModuleCode");

            entity.HasIndex(e => e.RegionId, "IX_CRM_TenantModules_RegionId");

            entity.HasIndex(e => e.UserId, "IX_CRM_TenantModules_UserId");

            entity.HasIndex(e => new { e.CustomerTenantId, e.ModuleCode }, "UQ_CRM_TenantModules").IsUnique();

            entity.Property(e => e.TenantModuleId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.EnabledOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);
            entity.Property(e => e.ModuleCode).HasMaxLength(100);
            entity.Property(e => e.ModuleName).HasMaxLength(150);

            entity.HasOne(d => d.Company).WithMany(p => p.TenantModules)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantModules_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TenantModuleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantModules_CreatedBy");

            entity.HasOne(d => d.CustomerTenant).WithMany(p => p.TenantModules)
                .HasForeignKey(d => d.CustomerTenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantModules_CustomerTenant");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TenantModuleModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_TenantModules_ModifiedBy");

            entity.HasOne(d => d.User).WithMany(p => p.TenantModuleUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantModules_User");
        });

        modelBuilder.Entity<TenantSetting>(entity =>
        {
            entity.HasKey(e => e.TenantSettingId).HasName("PK_CRM_TenantSettings");

            entity.ToTable("TenantSettings", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_TenantSettings_CompanyId");

            entity.HasIndex(e => e.CustomerTenantId, "IX_CRM_TenantSettings_CustomerTenantId");

            entity.HasIndex(e => e.DataType, "IX_CRM_TenantSettings_DataType");

            entity.HasIndex(e => e.IsEncrypted, "IX_CRM_TenantSettings_IsEncrypted");

            entity.HasIndex(e => e.RegionId, "IX_CRM_TenantSettings_RegionId");

            entity.HasIndex(e => e.SettingKey, "IX_CRM_TenantSettings_SettingKey");

            entity.HasIndex(e => e.UserId, "IX_CRM_TenantSettings_UserId");

            entity.HasIndex(e => new { e.CustomerTenantId, e.SettingKey }, "UQ_CRM_TenantSettings").IsUnique();

            entity.Property(e => e.TenantSettingId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DataType).HasMaxLength(50);
            entity.Property(e => e.SettingKey).HasMaxLength(150);

            entity.HasOne(d => d.Company).WithMany(p => p.TenantSettings)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantSettings_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TenantSettingCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantSettings_CreatedBy");

            entity.HasOne(d => d.CustomerTenant).WithMany(p => p.TenantSettings)
                .HasForeignKey(d => d.CustomerTenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantSettings_CustomerTenant");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TenantSettingModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_TenantSettings_ModifiedBy");

            entity.HasOne(d => d.User).WithMany(p => p.TenantSettingUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantSettings_User");
        });

        modelBuilder.Entity<TenantStorageLimit>(entity =>
        {
            entity.HasKey(e => e.TenantStorageLimitId).HasName("PK_CRM_TenantStorageLimits");

            entity.ToTable("TenantStorageLimits", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_TenantStorageLimits_CompanyId");

            entity.HasIndex(e => e.CustomerTenantId, "IX_CRM_TenantStorageLimits_CustomerTenantId");

            entity.HasIndex(e => e.LastCalculatedOn, "IX_CRM_TenantStorageLimits_LastCalculatedOn");

            entity.HasIndex(e => e.RegionId, "IX_CRM_TenantStorageLimits_RegionId");

            entity.HasIndex(e => e.TotalStorageLimitGb, "IX_CRM_TenantStorageLimits_TotalStorageLimitGB");

            entity.HasIndex(e => e.UsedStorageGb, "IX_CRM_TenantStorageLimits_UsedStorageGB");

            entity.HasIndex(e => e.UserId, "IX_CRM_TenantStorageLimits_UserId");

            entity.HasIndex(e => e.CustomerTenantId, "UQ_CRM_TenantStorageLimits").IsUnique();

            entity.Property(e => e.TenantStorageLimitId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AvailableStorageGb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("AvailableStorageGB");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.LastCalculatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.TotalStorageLimitGb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TotalStorageLimitGB");
            entity.Property(e => e.UsedStorageGb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("UsedStorageGB");
            entity.Property(e => e.WarningThresholdPercentage)
                .HasDefaultValue(80.00m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Company).WithMany(p => p.TenantStorageLimits)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantStorageLimits_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TenantStorageLimitCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantStorageLimits_CreatedBy");

            entity.HasOne(d => d.CustomerTenant).WithOne(p => p.TenantStorageLimit)
                .HasForeignKey<TenantStorageLimit>(d => d.CustomerTenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantStorageLimits_CustomerTenant");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TenantStorageLimitModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_TenantStorageLimits_ModifiedBy");

            entity.HasOne(d => d.User).WithMany(p => p.TenantStorageLimitUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantStorageLimits_User");
        });

        modelBuilder.Entity<TenantUserLimit>(entity =>
        {
            entity.HasKey(e => e.TenantUserLimitId).HasName("PK_CRM_TenantUserLimits");

            entity.ToTable("TenantUserLimits", "CRM");

            entity.HasIndex(e => e.ActiveUserCount, "IX_CRM_TenantUserLimits_ActiveUserCount");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_TenantUserLimits_CompanyId");

            entity.HasIndex(e => e.CustomerTenantId, "IX_CRM_TenantUserLimits_CustomerTenantId");

            entity.HasIndex(e => e.LastCalculatedOn, "IX_CRM_TenantUserLimits_LastCalculatedOn");

            entity.HasIndex(e => e.RegionId, "IX_CRM_TenantUserLimits_RegionId");

            entity.HasIndex(e => e.TotalUserLimit, "IX_CRM_TenantUserLimits_TotalUserLimit");

            entity.HasIndex(e => e.UserId, "IX_CRM_TenantUserLimits_UserId");

            entity.HasIndex(e => e.CustomerTenantId, "UQ_CRM_TenantUserLimits").IsUnique();

            entity.Property(e => e.TenantUserLimitId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.LastCalculatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.WarningThresholdPercentage)
                .HasDefaultValue(80.00m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Company).WithMany(p => p.TenantUserLimits)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantUserLimits_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TenantUserLimitCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantUserLimits_CreatedBy");

            entity.HasOne(d => d.CustomerTenant).WithOne(p => p.TenantUserLimit)
                .HasForeignKey<TenantUserLimit>(d => d.CustomerTenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantUserLimits_CustomerTenant");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TenantUserLimitModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_TenantUserLimits_ModifiedBy");

            entity.HasOne(d => d.User).WithMany(p => p.TenantUserLimitUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TenantUserLimits_User");
        });

        modelBuilder.Entity<TicketAttachment>(entity =>
        {
            entity.HasKey(e => e.TicketAttachmentId).HasName("PK_CRM_TicketAttachments");

            entity.ToTable("TicketAttachments", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_TicketAttachments_CompanyId");

            entity.HasIndex(e => e.FileType, "IX_CRM_TicketAttachments_FileType");

            entity.HasIndex(e => e.RegionId, "IX_CRM_TicketAttachments_RegionId");

            entity.HasIndex(e => e.SupportTicketId, "IX_CRM_TicketAttachments_SupportTicketId");

            entity.HasIndex(e => e.UploadedByUserId, "IX_CRM_TicketAttachments_UploadedByUserId");

            entity.HasIndex(e => e.UploadedOn, "IX_CRM_TicketAttachments_UploadedOn");

            entity.HasIndex(e => e.UserId, "IX_CRM_TicketAttachments_UserId");

            entity.Property(e => e.TicketAttachmentId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.FileName).HasMaxLength(250);
            entity.Property(e => e.FileType).HasMaxLength(100);
            entity.Property(e => e.FileUrl).HasMaxLength(1000);
            entity.Property(e => e.UploadedOn).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TicketAttachmentCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TicketAttachments_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TicketAttachmentModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_TicketAttachments_ModifiedBy");

            entity.HasOne(d => d.SupportTicket).WithMany(p => p.TicketAttachments)
                .HasForeignKey(d => d.SupportTicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TicketAttachments_SupportTicket");

            entity.HasOne(d => d.UploadedByUser).WithMany(p => p.TicketAttachmentUploadedByUsers)
                .HasForeignKey(d => d.UploadedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TicketAttachments_UploadedByUser");

            entity.HasOne(d => d.User).WithMany(p => p.TicketAttachmentUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TicketAttachments_User");
        });

        modelBuilder.Entity<TicketComment>(entity =>
        {
            entity.HasKey(e => e.TicketCommentId).HasName("PK_CRM_TicketComments");

            entity.ToTable("TicketComments", "CRM");

            entity.HasIndex(e => e.CommentedByUserId, "IX_CRM_TicketComments_CommentedByUserId");

            entity.HasIndex(e => e.CommentedOn, "IX_CRM_TicketComments_CommentedOn");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_TicketComments_CompanyId");

            entity.HasIndex(e => e.IsInternalNote, "IX_CRM_TicketComments_IsInternalNote");

            entity.HasIndex(e => e.RegionId, "IX_CRM_TicketComments_RegionId");

            entity.HasIndex(e => e.SupportTicketId, "IX_CRM_TicketComments_SupportTicketId");

            entity.HasIndex(e => e.UserId, "IX_CRM_TicketComments_UserId");

            entity.Property(e => e.TicketCommentId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CommentedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.CommentedByUser).WithMany(p => p.TicketCommentCommentedByUsers)
                .HasForeignKey(d => d.CommentedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TicketComments_CommentedByUser");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TicketCommentCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TicketComments_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TicketCommentModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_TicketComments_ModifiedBy");

            entity.HasOne(d => d.SupportTicket).WithMany(p => p.TicketComments)
                .HasForeignKey(d => d.SupportTicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TicketComments_SupportTicket");

            entity.HasOne(d => d.User).WithMany(p => p.TicketCommentUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TicketComments_User");
        });

        modelBuilder.Entity<TrainingSession>(entity =>
        {
            entity.HasKey(e => e.TrainingSessionId).HasName("PK_CRM_TrainingSessions");

            entity.ToTable("TrainingSessions", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_TrainingSessions_CompanyId");

            entity.HasIndex(e => e.OnboardingProjectId, "IX_CRM_TrainingSessions_OnboardingProjectId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_TrainingSessions_RegionId");

            entity.HasIndex(e => e.SessionDate, "IX_CRM_TrainingSessions_SessionDate");

            entity.HasIndex(e => e.Status, "IX_CRM_TrainingSessions_Status");

            entity.HasIndex(e => e.TrainerUserId, "IX_CRM_TrainingSessions_TrainerUserId");

            entity.HasIndex(e => e.UserId, "IX_CRM_TrainingSessions_UserId");

            entity.Property(e => e.TrainingSessionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.MeetingLink).HasMaxLength(500);
            entity.Property(e => e.RecordingUrl).HasMaxLength(1000);

            entity.HasOne(d => d.Company).WithMany(p => p.TrainingSessions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TrainingSessions_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TrainingSessionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TrainingSessions_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TrainingSessionModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_TrainingSessions_ModifiedBy");

            entity.HasOne(d => d.OnboardingProject).WithMany(p => p.TrainingSessions)
                .HasForeignKey(d => d.OnboardingProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TrainingSessions_OnboardingProject");

            entity.HasOne(d => d.TrainerUser).WithMany(p => p.TrainingSessionTrainerUsers)
                .HasForeignKey(d => d.TrainerUserId)
                .HasConstraintName("FK_CRM_TrainingSessions_Trainer");

            entity.HasOne(d => d.User).WithMany(p => p.TrainingSessionUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TrainingSessions_User");
        });

        modelBuilder.Entity<TwilioCallLog>(entity =>
        {
            entity.HasKey(e => e.TwilioCallLogId).HasName("PK_CRM_TwilioCallLogs");

            entity.ToTable("TwilioCallLogs", "CRM");

            entity.HasIndex(e => e.CallSid, "IX_CRM_TwilioCallLogs_CallSid");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_TwilioCallLogs_CompanyId");

            entity.HasIndex(e => e.Direction, "IX_CRM_TwilioCallLogs_Direction");

            entity.HasIndex(e => e.EndedOn, "IX_CRM_TwilioCallLogs_EndedOn");

            entity.HasIndex(e => e.LeadCallId, "IX_CRM_TwilioCallLogs_LeadCallId");

            entity.HasIndex(e => e.ParentCallSid, "IX_CRM_TwilioCallLogs_ParentCallSid");

            entity.HasIndex(e => e.RegionId, "IX_CRM_TwilioCallLogs_RegionId");

            entity.HasIndex(e => e.StartedOn, "IX_CRM_TwilioCallLogs_StartedOn");

            entity.HasIndex(e => e.Status, "IX_CRM_TwilioCallLogs_Status");

            entity.HasIndex(e => e.TwilioConfigurationId, "IX_CRM_TwilioCallLogs_TwilioConfigurationId");

            entity.HasIndex(e => e.UserId, "IX_CRM_TwilioCallLogs_UserId");

            entity.HasIndex(e => e.CallSid, "UQ_CRM_TwilioCallLogs_CallSid").IsUnique();

            entity.Property(e => e.TwilioCallLogId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CallSid).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Direction).HasMaxLength(50);
            entity.Property(e => e.FromNumber).HasMaxLength(30);
            entity.Property(e => e.ParentCallSid).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.PriceUnit).HasMaxLength(10);
            entity.Property(e => e.RecordingSid).HasMaxLength(100);
            entity.Property(e => e.RecordingUrl).HasMaxLength(1000);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.ToNumber).HasMaxLength(30);

            entity.HasOne(d => d.Company).WithMany(p => p.TwilioCallLogs)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioCallLogs_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TwilioCallLogCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioCallLogs_CreatedBy");

            entity.HasOne(d => d.LeadCall).WithMany(p => p.TwilioCallLogs)
                .HasForeignKey(d => d.LeadCallId)
                .HasConstraintName("FK_CRM_TwilioCallLogs_LeadCall");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TwilioCallLogModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_TwilioCallLogs_ModifiedBy");

            entity.HasOne(d => d.TwilioConfiguration).WithMany(p => p.TwilioCallLogs)
                .HasForeignKey(d => d.TwilioConfigurationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioCallLogs_TwilioConfiguration");

            entity.HasOne(d => d.User).WithMany(p => p.TwilioCallLogUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioCallLogs_User");
        });

        modelBuilder.Entity<TwilioConfiguration>(entity =>
        {
            entity.HasKey(e => e.TwilioConfigurationId).HasName("PK_CRM_TwilioConfigurations");

            entity.ToTable("TwilioConfigurations", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_TwilioConfigurations_CompanyId");

            entity.HasIndex(e => e.CustomerTenantId, "IX_CRM_TwilioConfigurations_CustomerTenantId");

            entity.HasIndex(e => e.DefaultFromNumber, "IX_CRM_TwilioConfigurations_DefaultFromNumber");

            entity.HasIndex(e => e.IsActive, "IX_CRM_TwilioConfigurations_IsActive");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_TwilioConfigurations_OrganizationId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_TwilioConfigurations_RegionId");

            entity.HasIndex(e => e.UserId, "IX_CRM_TwilioConfigurations_UserId");

            entity.HasIndex(e => e.AccountSid, "UQ_CRM_TwilioConfigurations_AccountSid").IsUnique();

            entity.HasIndex(e => e.CustomerTenantId, "UQ_CRM_TwilioConfigurations_Tenant").IsUnique();

            entity.Property(e => e.TwilioConfigurationId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AccountSid).HasMaxLength(150);
            entity.Property(e => e.ApiKeySid).HasMaxLength(150);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DefaultFromNumber).HasMaxLength(30);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.SmsWebhookUrl).HasMaxLength(500);
            entity.Property(e => e.StatusCallbackUrl).HasMaxLength(500);
            entity.Property(e => e.VoiceWebhookUrl).HasMaxLength(500);

            entity.HasOne(d => d.Company).WithMany(p => p.TwilioConfigurationCompanies)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioConfigurations_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TwilioConfigurationCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioConfigurations_CreatedBy");

            entity.HasOne(d => d.CustomerTenant).WithOne(p => p.TwilioConfiguration)
                .HasForeignKey<TwilioConfiguration>(d => d.CustomerTenantId)
                .HasConstraintName("FK_CRM_TwilioConfigurations_CustomerTenant");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TwilioConfigurationModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_TwilioConfigurations_ModifiedBy");

            entity.HasOne(d => d.Organization).WithMany(p => p.TwilioConfigurationOrganizations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioConfigurations_Organization");

            entity.HasOne(d => d.User).WithMany(p => p.TwilioConfigurationUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioConfigurations_User");
        });

        modelBuilder.Entity<TwilioPhoneNumber>(entity =>
        {
            entity.HasKey(e => e.TwilioPhoneNumberId).HasName("PK_CRM_TwilioPhoneNumbers");

            entity.ToTable("TwilioPhoneNumbers", "CRM");

            entity.HasIndex(e => e.AssignedToUserId, "IX_CRM_TwilioPhoneNumbers_AssignedToUserId");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_TwilioPhoneNumbers_CompanyId");

            entity.HasIndex(e => e.IsDefault, "IX_CRM_TwilioPhoneNumbers_IsDefault");

            entity.HasIndex(e => e.RegionId, "IX_CRM_TwilioPhoneNumbers_RegionId");

            entity.HasIndex(e => e.Status, "IX_CRM_TwilioPhoneNumbers_Status");

            entity.HasIndex(e => e.TwilioConfigurationId, "IX_CRM_TwilioPhoneNumbers_TwilioConfigurationId");

            entity.HasIndex(e => e.UserId, "IX_CRM_TwilioPhoneNumbers_UserId");

            entity.HasIndex(e => e.PhoneNumber, "UQ_CRM_TwilioPhoneNumbers_PhoneNumber").IsUnique();

            entity.Property(e => e.TwilioPhoneNumberId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Capabilities).HasMaxLength(250);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.FriendlyName).HasMaxLength(150);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.TwilioPhoneNumberAssignedToUsers)
                .HasForeignKey(d => d.AssignedToUserId)
                .HasConstraintName("FK_CRM_TwilioPhoneNumbers_AssignedToUser");

            entity.HasOne(d => d.Company).WithMany(p => p.TwilioPhoneNumbers)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioPhoneNumbers_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TwilioPhoneNumberCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioPhoneNumbers_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TwilioPhoneNumberModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_TwilioPhoneNumbers_ModifiedBy");

            entity.HasOne(d => d.TwilioConfiguration).WithMany(p => p.TwilioPhoneNumbers)
                .HasForeignKey(d => d.TwilioConfigurationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioPhoneNumbers_TwilioConfiguration");

            entity.HasOne(d => d.User).WithMany(p => p.TwilioPhoneNumberUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioPhoneNumbers_User");
        });

        modelBuilder.Entity<TwilioSmslog>(entity =>
        {
            entity.HasKey(e => e.TwilioSmslogId).HasName("PK_CRM_TwilioSMSLogs");

            entity.ToTable("TwilioSMSLogs", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_TwilioSMSLogs_CompanyId");

            entity.HasIndex(e => e.CustomerId, "IX_CRM_TwilioSMSLogs_CustomerId");

            entity.HasIndex(e => e.DeliveredOn, "IX_CRM_TwilioSMSLogs_DeliveredOn");

            entity.HasIndex(e => e.Direction, "IX_CRM_TwilioSMSLogs_Direction");

            entity.HasIndex(e => e.LeadId, "IX_CRM_TwilioSMSLogs_LeadId");

            entity.HasIndex(e => e.MessageSid, "IX_CRM_TwilioSMSLogs_MessageSid");

            entity.HasIndex(e => e.RegionId, "IX_CRM_TwilioSMSLogs_RegionId");

            entity.HasIndex(e => e.SentOn, "IX_CRM_TwilioSMSLogs_SentOn");

            entity.HasIndex(e => e.Status, "IX_CRM_TwilioSMSLogs_Status");

            entity.HasIndex(e => e.TwilioConfigurationId, "IX_CRM_TwilioSMSLogs_TwilioConfigurationId");

            entity.HasIndex(e => e.UserId, "IX_CRM_TwilioSMSLogs_UserId");

            entity.HasIndex(e => e.MessageSid, "UQ_CRM_TwilioSMSLogs_MessageSid").IsUnique();

            entity.Property(e => e.TwilioSmslogId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("TwilioSMSLogId");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.FailureReason).HasMaxLength(500);
            entity.Property(e => e.FromNumber).HasMaxLength(30);
            entity.Property(e => e.MessageSid).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.ToNumber).HasMaxLength(30);

            entity.HasOne(d => d.Company).WithMany(p => p.TwilioSmslogs)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioSMSLogs_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TwilioSmslogCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioSMSLogs_CreatedBy");

            entity.HasOne(d => d.Customer).WithMany(p => p.TwilioSmslogs)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CRM_TwilioSMSLogs_Customer");

            entity.HasOne(d => d.Lead).WithMany(p => p.TwilioSmslogs)
                .HasForeignKey(d => d.LeadId)
                .HasConstraintName("FK_CRM_TwilioSMSLogs_Lead");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TwilioSmslogModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_TwilioSMSLogs_ModifiedBy");

            entity.HasOne(d => d.TwilioConfiguration).WithMany(p => p.TwilioSmslogs)
                .HasForeignKey(d => d.TwilioConfigurationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioSMSLogs_TwilioConfiguration");

            entity.HasOne(d => d.User).WithMany(p => p.TwilioSmslogUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioSMSLogs_User");
        });

        modelBuilder.Entity<TwilioWebhookLog>(entity =>
        {
            entity.HasKey(e => e.TwilioWebhookLogId).HasName("PK_CRM_TwilioWebhookLogs");

            entity.ToTable("TwilioWebhookLogs", "CRM");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_TwilioWebhookLogs_CompanyId");

            entity.HasIndex(e => e.ProcessedOn, "IX_CRM_TwilioWebhookLogs_ProcessedOn");

            entity.HasIndex(e => e.ProcessingStatus, "IX_CRM_TwilioWebhookLogs_ProcessingStatus");

            entity.HasIndex(e => e.ReceivedOn, "IX_CRM_TwilioWebhookLogs_ReceivedOn");

            entity.HasIndex(e => e.RegionId, "IX_CRM_TwilioWebhookLogs_RegionId");

            entity.HasIndex(e => e.TwilioConfigurationId, "IX_CRM_TwilioWebhookLogs_TwilioConfigurationId");

            entity.HasIndex(e => e.UserId, "IX_CRM_TwilioWebhookLogs_UserId");

            entity.HasIndex(e => e.WebhookType, "IX_CRM_TwilioWebhookLogs_WebhookType");

            entity.Property(e => e.TwilioWebhookLogId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ProcessingStatus).HasDefaultValue((byte)1);
            entity.Property(e => e.ReceivedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.RequestMethod).HasMaxLength(20);
            entity.Property(e => e.RequestUrl).HasMaxLength(500);
            entity.Property(e => e.WebhookType).HasMaxLength(100);

            entity.HasOne(d => d.Company).WithMany(p => p.TwilioWebhookLogs)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioWebhookLogs_Company");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TwilioWebhookLogCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioWebhookLogs_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TwilioWebhookLogModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_TwilioWebhookLogs_ModifiedBy");

            entity.HasOne(d => d.TwilioConfiguration).WithMany(p => p.TwilioWebhookLogs)
                .HasForeignKey(d => d.TwilioConfigurationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioWebhookLogs_TwilioConfiguration");

            entity.HasOne(d => d.User).WithMany(p => p.TwilioWebhookLogUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_TwilioWebhookLogs_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Security_Users");

            entity.ToTable("Users", "Security");

            entity.HasIndex(e => e.BranchId, "IX_Security_Users_BranchId");

            entity.HasIndex(e => e.DepartmentId, "IX_Security_Users_DepartmentId");

            entity.HasIndex(e => e.DisplayName, "IX_Security_Users_DisplayName");

            entity.HasIndex(e => e.Email, "IX_Security_Users_Email");

            entity.HasIndex(e => e.OrganizationId, "IX_Security_Users_OrganizationId");

            entity.HasIndex(e => e.ReportingManagerId, "IX_Security_Users_ReportingManagerId");

            entity.HasIndex(e => e.Status, "IX_Security_Users_Status");

            entity.HasIndex(e => e.TeamId, "IX_Security_Users_TeamId");

            entity.HasIndex(e => new { e.OrganizationId, e.Email }, "UQ_Security_Users_Organization_Email").IsUnique();

            entity.HasIndex(e => new { e.OrganizationId, e.EmployeeCode }, "UQ_Security_Users_Organization_EmployeeCode").IsUnique();

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNumber).HasMaxLength(30);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.ProfileImageUrl).HasMaxLength(500);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Branch).WithMany(p => p.Users)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK_Security_Users_Branches");

            entity.HasOne(d => d.Department).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Security_Users_Departments");

            entity.HasOne(d => d.Organization).WithMany(p => p.Users)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Security_Users_Organizations");

            entity.HasOne(d => d.Team).WithMany(p => p.Users)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_Security_Users_Teams");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserLogi__1788CC4CDD22AE27");

            entity.ToTable("UserLogin");

            entity.HasIndex(e => e.Email, "UQ__UserLogi__A9D10534A0F4EAD1").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__UserLogi__C9F284568073A02F").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.MobileNumber).HasMaxLength(15);
            entity.Property(e => e.OtpCode).HasMaxLength(10);
            entity.Property(e => e.OtpExpiry).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.PasswordResetExpiry).HasColumnType("datetime");
            entity.Property(e => e.PasswordResetToken).HasMaxLength(500);
            entity.Property(e => e.PasswordSalt).HasMaxLength(500);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("User");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK_Security_UserRoles");

            entity.ToTable("UserRoles", "Security");

            entity.HasIndex(e => e.AssignedBy, "IX_Security_UserRoles_AssignedBy");

            entity.HasIndex(e => e.AssignedOn, "IX_Security_UserRoles_AssignedOn");

            entity.HasIndex(e => e.IsPrimaryRole, "IX_Security_UserRoles_IsPrimaryRole");

            entity.HasIndex(e => e.RoleId, "IX_Security_UserRoles_RoleId");

            entity.HasIndex(e => e.UserId, "IX_Security_UserRoles_UserId");

            entity.HasIndex(e => new { e.UserId, e.RoleId }, "UQ_Security_UserRoles_User_Role").IsUnique();

            entity.Property(e => e.UserRoleId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AssignedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

            entity.HasOne(d => d.AssignedByNavigation).WithMany(p => p.UserRoleAssignedByNavigations)
                .HasForeignKey(d => d.AssignedBy)
                .HasConstraintName("FK_Security_UserRoles_AssignedBy");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Security_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoleUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Security_UserRoles_Users");
        });

        modelBuilder.Entity<WhatsAppTemplate>(entity =>
        {
            entity.HasKey(e => e.WhatsAppTemplateId).HasName("PK_CRM_WhatsAppTemplates");

            entity.ToTable("WhatsAppTemplates", "CRM");

            entity.HasIndex(e => e.ApprovalStatus, "IX_CRM_WhatsAppTemplates_ApprovalStatus");

            entity.HasIndex(e => e.CompanyId, "IX_CRM_WhatsAppTemplates_CompanyId");

            entity.HasIndex(e => e.OrganizationId, "IX_CRM_WhatsAppTemplates_OrganizationId");

            entity.HasIndex(e => e.RegionId, "IX_CRM_WhatsAppTemplates_RegionId");

            entity.HasIndex(e => e.Status, "IX_CRM_WhatsAppTemplates_Status");

            entity.HasIndex(e => e.TemplateCode, "IX_CRM_WhatsAppTemplates_TemplateCode");

            entity.HasIndex(e => e.TemplateLanguage, "IX_CRM_WhatsAppTemplates_TemplateLanguage");

            entity.HasIndex(e => e.UserId, "IX_CRM_WhatsAppTemplates_UserId");

            entity.HasIndex(e => new { e.OrganizationId, e.TemplateCode }, "UQ_CRM_WhatsAppTemplates_TemplateCode").IsUnique();

            entity.Property(e => e.WhatsAppTemplateId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.MetaTemplateName).HasMaxLength(150);
            entity.Property(e => e.TemplateCode).HasMaxLength(100);
            entity.Property(e => e.TemplateLanguage).HasMaxLength(20);
            entity.Property(e => e.TemplateName).HasMaxLength(150);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.WhatsAppTemplateCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_WhatsAppTemplates_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.WhatsAppTemplateModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_CRM_WhatsAppTemplates_ModifiedBy");

            entity.HasOne(d => d.User).WithMany(p => p.WhatsAppTemplateUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_WhatsAppTemplates_User");
        });

        modelBuilder.Entity<WorkflowRule>(entity =>
        {
            entity.ToTable("WorkflowRules", "Superadmin");

            entity.Property(e => e.WorkflowRuleId).HasColumnName("WorkflowRuleID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.ExecutionType)
                .HasMaxLength(50)
                .HasDefaultValue("Immediate");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModuleName).HasMaxLength(100);
            entity.Property(e => e.Priority)
                .HasMaxLength(50)
                .HasDefaultValue("Medium");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.TriggerEvent).HasMaxLength(100);
            entity.Property(e => e.WorkflowRuleCode).HasMaxLength(100);
            entity.Property(e => e.WorkflowRuleName).HasMaxLength(200);
        });

        modelBuilder.Entity<WorkflowRuleAction>(entity =>
        {
            entity.ToTable("WorkflowRuleActions", "Superadmin");

            entity.Property(e => e.WorkflowRuleActionId).HasColumnName("WorkflowRuleActionID");
            entity.Property(e => e.ActionName).HasMaxLength(200);
            entity.Property(e => e.ActionType).HasMaxLength(100);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.WorkflowRuleId).HasColumnName("WorkflowRuleID");

            entity.HasOne(d => d.WorkflowRule).WithMany(p => p.WorkflowRuleActions)
                .HasForeignKey(d => d.WorkflowRuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowRuleActions_WorkflowRules");
        });

        modelBuilder.Entity<WorkflowRuleCondition>(entity =>
        {
            entity.ToTable("WorkflowRuleConditions", "Superadmin");

            entity.Property(e => e.WorkflowRuleConditionId).HasColumnName("WorkflowRuleConditionID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FieldName).HasMaxLength(150);
            entity.Property(e => e.FieldValue).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LogicalOperator).HasMaxLength(10);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Operator).HasMaxLength(50);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.WorkflowRuleId).HasColumnName("WorkflowRuleID");

            entity.HasOne(d => d.WorkflowRule).WithMany(p => p.WorkflowRuleConditions)
                .HasForeignKey(d => d.WorkflowRuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowRuleConditions_WorkflowRules");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
