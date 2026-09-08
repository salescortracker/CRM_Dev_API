using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CrmSalesTargetDto
    {
        public int SalesTargetId { get; set; }

        public string TargetName { get; set; } = string.Empty;

        public string TargetCode { get; set; } = string.Empty;

        public string EmployeeName { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public decimal TargetAmount { get; set; }

        public decimal AchievedAmount { get; set; }

        public string TargetPeriod { get; set; } = string.Empty;

        public string Status { get; set; } = "Active";

        public string? Description { get; set; }
    }
}
