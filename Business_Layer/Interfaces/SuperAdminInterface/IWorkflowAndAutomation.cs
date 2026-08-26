using Business_Layer.DTOs.SuperAdmin;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.SuperAdminInterface
{
    public interface IWorkflowAndAutomation
    {
        #region WORKFLOW RULE CRUD
        // =====================================================
        // WORKFLOW RULE CRUD
        // =====================================================

        Task<ApiResponse<string>> CreateWorkflowRule(
            WorkflowRuleDto dto);

        Task<ApiResponse<string>> UpdateWorkflowRule(
            WorkflowRuleDto dto);

        Task<ApiResponse<string>> DeleteWorkflowRule(
            int id);

        Task<ApiResponse<List<WorkflowRuleDto>>> GetWorkflowRules();

        Task<ApiResponse<WorkflowRuleDto>> GetWorkflowRuleById(
            int id);
        #endregion
        #region WORKFLOW RULE CONDITION CRUD
        // =====================================================
        // WORKFLOW RULE CONDITION CRUD
        // =====================================================

        Task<ApiResponse<string>> CreateWorkflowRuleCondition(
            WorkflowRuleConditionDto dto);

        Task<ApiResponse<string>> UpdateWorkflowRuleCondition(
            WorkflowRuleConditionDto dto);

        Task<ApiResponse<string>> DeleteWorkflowRuleCondition(
            int id);

        Task<ApiResponse<List<WorkflowRuleConditionDto>>>
            GetWorkflowRuleConditions();

        Task<ApiResponse<WorkflowRuleConditionDto>>
            GetWorkflowRuleConditionById(int id);


        // =====================================================
        // GET CONDITIONS BY WORKFLOW RULE
        // =====================================================

        Task<ApiResponse<List<WorkflowRuleConditionDto>>>
            GetConditionsByWorkflowRuleId(int workflowRuleId);
        #endregion

        #region  WorkflowRuleAction CRUD
        // =====================================================
        // WorkflowRuleAction CRUD
        // =====================================================

        Task<ApiResponse<string>> CreateWorkflowRuleAction(
            WorkflowRuleActionDto dto);

        Task<ApiResponse<string>> UpdateWorkflowRuleAction(
            WorkflowRuleActionDto dto);

        Task<ApiResponse<string>> DeleteWorkflowRuleAction(
            int id);

        Task<ApiResponse<List<WorkflowRuleActionDto>>>
            GetWorkflowRuleActions();

        Task<ApiResponse<WorkflowRuleActionDto>>
            GetWorkflowRuleActionById(int id);
        #endregion
        #region APPROVAL WORKFLOW CRUD
        // =====================================================
        // APPROVAL WORKFLOW CRUD
        // =====================================================

        Task<ApiResponse<string>> CreateApprovalWorkflow(
            ApprovalWorkflowDto dto);

        Task<ApiResponse<string>> UpdateApprovalWorkflow(
            ApprovalWorkflowDto dto);

        Task<ApiResponse<string>> DeleteApprovalWorkflow(
            int id);

        Task<ApiResponse<List<ApprovalWorkflowDto>>>
            GetApprovalWorkflows();

        Task<ApiResponse<ApprovalWorkflowDto>>
            GetApprovalWorkflowById(int id);
        #endregion
        #region APPROVAL WORKFLOW LEVEL CRUD
        Task<ApiResponse<string>> CreateApprovalWorkflowLevel(
         ApprovalWorkflowLevelDto dto);

        Task<ApiResponse<string>> UpdateApprovalWorkflowLevel(
            ApprovalWorkflowLevelDto dto);

        Task<ApiResponse<string>> DeleteApprovalWorkflowLevel(
            int id);

        Task<ApiResponse<List<ApprovalWorkflowLevelDto>>>
            GetApprovalWorkflowLevels();

        Task<ApiResponse<ApprovalWorkflowLevelDto>>
            GetApprovalWorkflowLevelById(int id);
        #endregion
    }
}
