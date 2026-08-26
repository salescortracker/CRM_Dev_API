using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class WorkflowRuleDto
    {
        public int WorkflowRuleId { get; set; }

        public string WorkflowRuleName { get; set; } = string.Empty;

        public string WorkflowRuleCode { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string ModuleName { get; set; } = string.Empty;

        public string TriggerEvent { get; set; } = string.Empty;

        public string ExecutionType { get; set; } = "Immediate";

        public string Priority { get; set; } = "Medium";

        public int? ExecutionOrder { get; set; }

        public bool StopProcessing { get; set; }


        // =====================================================
        // COMPANY / REGION
        // =====================================================

        public int CompanyId { get; set; }

        public int RegionId { get; set; }


        // =====================================================
        // STATUS
        // =====================================================

        public bool IsActive { get; set; } = true;


        // =====================================================
        // AUDIT
        // =====================================================

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
