using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.MasterDTO_s
{
    public class BusinessUnitDto
    {
       public int BusinessUnitId  { get; set; }

        public int OrganizationId { get; set; }

        public int CompanyId { get; set; }

        public int? RegionId { get; set; }

        public int? BranchId { get; set; }

        public string BusinessUnitName { get; set; } = string.Empty;

        public string BusinessUnitCode { get; set; } = string.Empty;

        public string? ParentBusinessUnit { get; set; }

        public string? BusinessUnitType { get; set; }
        public string? BusinessUnitHead { get; set; }

        public string? UnitHead { get; set; }

        public string? Email { get; set; }  

        public string? MobileNumber { get; set; }
        public string? ContactNumber { get; set; }

        public string? ExtensionNumber { get; set; }

        public string? Description { get; set; }

        public string? Remarks { get; set; }

        public bool Status { get; set; }

        public bool DefaultBusinessUnit { get; set; }

        public bool BillableUnit { get; set; }

        public int? EmployeeStrength { get; set; }

        public string? CostCenterCode { get; set; }

        public decimal? AnnualBudget { get; set; }

    }
}
