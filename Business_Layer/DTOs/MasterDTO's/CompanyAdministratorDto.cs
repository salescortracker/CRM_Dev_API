using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.MasterDTO_s
{
    public class CompanyAdministratorDto
    {
        public int AdministratorId { get; set; }

        public int CompanyId { get; set; }

        public int? DepartmentId { get; set; }

        public int? DesignationId { get; set; }

        public int? RegionId { get; set; }

        public int? BranchId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }

        public string Email { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public string? RoleName { get; set; }

        public string? ReportingManager { get; set; }

        public string? ProfileImagePath { get; set; }

        public bool Status { get; set; }

        public bool EmailVerified { get; set; }

        public bool MobileVerified { get; set; }

        public byte TwoFactorAuthentication { get; set; }

        public string? Remarks { get; set; }
    }
}
