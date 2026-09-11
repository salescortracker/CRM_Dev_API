using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class BranchDto
    {
        public int BranchId { get; set; }

        public int OrganizationId { get; set; }

        public int CompanyId { get; set; }

        public int RegionId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        public string BranchCode { get; set; } = string.Empty;

        public string? BranchManager { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? ZipCode { get; set; }

        public TimeOnly? OpeningTime { get; set; }

        public TimeOnly? ClosingTime { get; set; }

        public bool Status { get; set; }

        public bool HeadOffice { get; set; }

        public string? Remarks { get; set; }
    }
}
