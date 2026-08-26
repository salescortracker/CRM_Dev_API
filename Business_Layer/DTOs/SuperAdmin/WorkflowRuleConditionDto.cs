using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class WorkflowRuleConditionDto
    {
        public int WorkflowRuleConditionId { get; set; }

        public int WorkflowRuleId { get; set; }

        public string? WorkflowRuleName { get; set; }

        public string FieldName { get; set; } = string.Empty;

        public string Operator { get; set; } = string.Empty;

        public string? FieldValue { get; set; }

        public string? LogicalOperator { get; set; }

        public int ConditionOrder { get; set; }


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
