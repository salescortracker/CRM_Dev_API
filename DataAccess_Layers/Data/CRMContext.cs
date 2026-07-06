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

    public virtual DbSet<ApplicationLog> ApplicationLogs { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<CompanyStatusMaster> CompanyStatusMasters { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Department1> Departments1 { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<Lead> Leads { get; set; }

    public virtual DbSet<LeadActivity> LeadActivities { get; set; }

    public virtual DbSet<LeadAssignment> LeadAssignments { get; set; }

    public virtual DbSet<LeadCall> LeadCalls { get; set; }

    public virtual DbSet<LeadSource> LeadSources { get; set; }

    public virtual DbSet<LeadStatus> LeadStatuses { get; set; }

    public virtual DbSet<MenuMaster> MenuMasters { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrganizationSetting> OrganizationSettings { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Role1> Roles1 { get; set; }

    public virtual DbSet<SubscriptionPlanMaster> SubscriptionPlanMasters { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.29.53,50491;Database=CRM_Dev;User Id=sa;Password=CtDev@2026@01;TrustServerCertificate=True;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Applicat__5E548648AFCF7092");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LogLevel).HasMaxLength(50);
            entity.Property(e => e.Source).HasMaxLength(500);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId);

            entity.ToTable("AuditLogs", "Master");

            entity.Property(e => e.ActionType).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.TableName).HasMaxLength(100);
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
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Organization).WithMany(p => p.Branches)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Masters_Branches_Organizations");
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
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DepartmentCode).HasMaxLength(50);
            entity.Property(e => e.DepartmentName).HasMaxLength(150);
            entity.Property(e => e.Description).HasMaxLength(500);
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
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
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
            entity.Property(e => e.IsCurrentAssignment).HasDefaultValue(true);

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
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.FailureReason).HasMaxLength(500);
            entity.Property(e => e.FromNumber).HasMaxLength(30);
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
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.SourceCode).HasMaxLength(50);
            entity.Property(e => e.SourceName).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Organization).WithMany(p => p.LeadSources)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CRM_LeadSources_Organizations");
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
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DisplayOrder).HasDefaultValue(1);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.StatusCode).HasMaxLength(50);
            entity.Property(e => e.StatusName).HasMaxLength(100);
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
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Gstnumber)
                .HasMaxLength(30)
                .HasColumnName("GSTNumber");
            entity.Property(e => e.LegalName).HasMaxLength(250);
            entity.Property(e => e.LogoUrl).HasMaxLength(500);
            entity.Property(e => e.OrganizationCode).HasMaxLength(50);
            entity.Property(e => e.OrganizationName).HasMaxLength(200);
            entity.Property(e => e.Pannumber)
                .HasMaxLength(30)
                .HasColumnName("PANNumber");
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.Website).HasMaxLength(250);
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
            entity.Property(e => e.QuotationPrefix).HasMaxLength(30);
            entity.Property(e => e.StorageWarningPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TimeZone).HasMaxLength(100);
            entity.Property(e => e.UserLimitWarningPercentage).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Organization).WithOne(p => p.OrganizationSetting)
                .HasForeignKey<OrganizationSetting>(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Masters_OrganizationSettings_Organizations");
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
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ModuleName).HasMaxLength(100);
            entity.Property(e => e.PermissionCode).HasMaxLength(150);
            entity.Property(e => e.ScreenName).HasMaxLength(150);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
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
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.RoleCode).HasMaxLength(50);
            entity.Property(e => e.RoleName).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Organization).WithMany(p => p.Role1s)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_Security_Roles_Organizations");
        });

        modelBuilder.Entity<SubscriptionPlanMaster>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Subscrip__755C22D75C29021A");

            entity.ToTable("SubscriptionPlanMaster", "plans");

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PlanName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
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
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
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
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNumber).HasMaxLength(30);
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
