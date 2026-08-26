using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class WorkflowRuleActionDto
    {
        public int WorkflowRuleActionId { get; set; }


        // =====================================================
        // WORKFLOW RULE
        // =====================================================

        public int WorkflowRuleId { get; set; }

        public string? WorkflowRuleName { get; set; }


        // =====================================================
        // ACTION INFORMATION
        // =====================================================

        public string ActionType { get; set; } = string.Empty;

        public string? ActionName { get; set; }

        public string? ActionConfiguration { get; set; }

        public int ActionOrder { get; set; }


        // =====================================================
        // COMPANY / REGION
        // =====================================================

        public int CompanyId { get; set; }

        public int RegionId { get; set; }


        // =====================================================
        // STATUS
        // =====================================================

        public bool IsActive { get; set; }


        // =====================================================
        // AUDIT
        // =====================================================

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
