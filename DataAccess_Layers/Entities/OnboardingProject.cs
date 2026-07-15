using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class OnboardingProject
{
    public Guid OnboardingProjectId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid? CustomerTenantId { get; set; }

    public Guid SubscriptionId { get; set; }

    public string ProjectNumber { get; set; } = null!;

    public string ProjectName { get; set; } = null!;

    public Guid? ProjectManagerUserId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? ExpectedGoLiveDate { get; set; }

    public DateOnly? ActualGoLiveDate { get; set; }

    public byte Status { get; set; }

    public string? Notes { get; set; }

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

    public virtual CustomerTenant? CustomerTenant { get; set; }

    public virtual ICollection<DataMigrationRequest> DataMigrationRequests { get; set; } = new List<DataMigrationRequest>();

    public virtual ICollection<GoLiveChecklist> GoLiveChecklists { get; set; } = new List<GoLiveChecklist>();

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual ICollection<OnboardingTask> OnboardingTasks { get; set; } = new List<OnboardingTask>();

    public virtual User? ProjectManagerUser { get; set; }

    public virtual Subscription Subscription { get; set; } = null!;

    public virtual ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();

    public virtual User User { get; set; } = null!;
}
