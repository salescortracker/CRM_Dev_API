using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CustomerTenant
{
    public Guid CustomerTenantId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid SubscriptionId { get; set; }

    public string TenantCode { get; set; } = null!;

    public string TenantName { get; set; } = null!;

    public string SubDomain { get; set; } = null!;

    public string? CustomDomain { get; set; }

    public string? DatabaseName { get; set; }

    public string TenantUrl { get; set; } = null!;

    public byte Status { get; set; }

    public DateTime? ProvisionedOn { get; set; }

    public DateTime? GoLiveOn { get; set; }

    public Guid? CustomerAdminUserId { get; set; }

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

    public virtual User? CustomerAdminUser { get; set; }

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual ICollection<OnboardingProject> OnboardingProjects { get; set; } = new List<OnboardingProject>();

    public virtual Subscription Subscription { get; set; } = null!;

    public virtual ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();

    public virtual ICollection<TenantModule> TenantModules { get; set; } = new List<TenantModule>();

    public virtual ICollection<TenantSetting> TenantSettings { get; set; } = new List<TenantSetting>();

    public virtual TenantStorageLimit? TenantStorageLimit { get; set; }

    public virtual TenantUserLimit? TenantUserLimit { get; set; }

    public virtual TwilioConfiguration? TwilioConfiguration { get; set; }

    public virtual User User { get; set; } = null!;
}
