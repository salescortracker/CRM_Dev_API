using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class DesignationDto
    {
        public int DesignationId { get; set; }

        public int CompanyId { get; set; }

        public int RegionId { get; set; }

        public int? DepartmentId { get; set; }

        public string DesignationName { get; set; } = null!;

        public string DesignationCode { get; set; } = null!;

        public string? Description { get; set; }

        public bool Status { get; set; }

        // For displaying Department Name in Angular
        public string? DepartmentName { get; set; }
    }
}
