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

    public virtual Branch? Branch { get; set; }

    public virtual Department1? Department { get; set; }

    public virtual ICollection<LeadActivity> LeadActivities { get; set; } = new List<LeadActivity>();

    public virtual ICollection<Lead> LeadAssignedByUsers { get; set; } = new List<Lead>();

    public virtual ICollection<Lead> LeadAssignedToUsers { get; set; } = new List<Lead>();

    public virtual ICollection<LeadAssignment> LeadAssignmentAssignedByUsers { get; set; } = new List<LeadAssignment>();

    public virtual ICollection<LeadAssignment> LeadAssignmentAssignedToUsers { get; set; } = new List<LeadAssignment>();

    public virtual ICollection<LeadCall> LeadCalls { get; set; } = new List<LeadCall>();

    public virtual Organization Organization { get; set; } = null!;

    public virtual Team? Team { get; set; }

    public virtual ICollection<UserRole> UserRoleAssignedByNavigations { get; set; } = new List<UserRole>();

    public virtual ICollection<UserRole> UserRoleUsers { get; set; } = new List<UserRole>();
}
