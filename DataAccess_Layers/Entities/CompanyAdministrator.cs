using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CompanyAdministrator
{
    public int AdministratorId { get; set; }

    public int CompanyId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public int? RegionId { get; set; }

    public int? BranchId { get; set; }

    public string EmployeeCode { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public string? RoleName { get; set; }

    public string? ReportingManager { get; set; }

    public string? ProfileImagePath { get; set; }

    public bool Status { get; set; }

    public bool EmailVerified { get; set; }

    public bool MobileVerified { get; set; }

    public byte TwoFactorAuthentication { get; set; }

    public string? Remarks { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Branch1? Branch { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Department? Department { get; set; }

    public virtual Designation? Designation { get; set; }

    public virtual Region? Region { get; set; }
}
