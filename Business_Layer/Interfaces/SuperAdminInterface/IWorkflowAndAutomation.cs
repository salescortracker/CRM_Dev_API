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

        #region AUTO ASSIGNMENT RULE CRUD

        // =====================================================
        // AUTO ASSIGNMENT RULE CRUD
        // =====================================================

        Task<ApiResponse<string>> CreateAutoAssignmentRule(
            AutoAssignmentRuleDto dto);

        Task<ApiResponse<string>> UpdateAutoAssignmentRule(
            AutoAssignmentRuleDto dto);

        Task<ApiResponse<string>> DeleteAutoAssignmentRule(
            int id);

        Task<ApiResponse<List<AutoAssignmentRuleDto>>>
            GetAutoAssignmentRules();

        Task<ApiResponse<AutoAssignmentRuleDto>>
            GetAutoAssignmentRuleById(int id);

        #endregion


        #region AUTO ASSIGNMENT CONDITION CRUD

        // =====================================================
        // AUTO ASSIGNMENT CONDITION CRUD
        // =====================================================

        Task<ApiResponse<string>> CreateAutoAssignmentCondition(
            AutoAssignmentConditionDto dto);

        Task<ApiResponse<string>> UpdateAutoAssignmentCondition(
            AutoAssignmentConditionDto dto);

        Task<ApiResponse<string>> DeleteAutoAssignmentCondition(
            int id);

        Task<ApiResponse<List<AutoAssignmentConditionDto>>>
            GetAutoAssignmentConditions();

        Task<ApiResponse<AutoAssignmentConditionDto>>
            GetAutoAssignmentConditionById(int id);

        #endregion

        #region ESCALATION RULE CRUD

        // =====================================================
        // ESCALATION RULE CRUD
        // =====================================================

        Task<ApiResponse<string>> CreateEscalationRule(
            EscalationRuleDto dto);

        Task<ApiResponse<string>> UpdateEscalationRule(
            EscalationRuleDto dto);

        Task<ApiResponse<string>> DeleteEscalationRule(
            int id);

        Task<ApiResponse<List<EscalationRuleDto>>>
            GetEscalationRules();

        Task<ApiResponse<EscalationRuleDto>>
            GetEscalationRuleById(int id);

        #endregion

        #region SLA RULE CRUD

        // =====================================================
        // SLA RULE CRUD
        // =====================================================

        Task<ApiResponse<string>> CreateSlarule(
            SlaruleDto dto);

        Task<ApiResponse<string>> UpdateSlarule(
            SlaruleDto dto);

        Task<ApiResponse<string>> DeleteSlarule(
            int id);

        Task<ApiResponse<List<SlaruleDto>>>
            GetSlarules();

        Task<ApiResponse<SlaruleDto>>
            GetSlaruleById(int id);

        #endregion

        #region EMAIL AUTOMATION CRUD

        // =====================================================
        // EMAIL AUTOMATION CRUD
        // =====================================================

        Task<ApiResponse<string>> CreateEmailAutomation(
            EmailAutomationDto dto);

        Task<ApiResponse<string>> UpdateEmailAutomation(
            EmailAutomationDto dto);

        Task<ApiResponse<string>> DeleteEmailAutomation(
            int id);

        Task<ApiResponse<List<EmailAutomationDto>>>
            GetEmailAutomations();

        Task<ApiResponse<EmailAutomationDto>>
            GetEmailAutomationById(int id);

        #endregion

        #region EMAIL AUTOMATION RECIPIENT CRUD

        // =====================================================
        // EMAIL AUTOMATION RECIPIENT CRUD
        // =====================================================

        Task<ApiResponse<string>> CreateEmailAutomationRecipient(
            EmailAutomationRecipientDto dto);

        Task<ApiResponse<string>> UpdateEmailAutomationRecipient(
            EmailAutomationRecipientDto dto);

        Task<ApiResponse<string>> DeleteEmailAutomationRecipient(
            int id);

        Task<ApiResponse<List<EmailAutomationRecipientDto>>>
            GetEmailAutomationRecipients();

        Task<ApiResponse<EmailAutomationRecipientDto>>
            GetEmailAutomationRecipientById(int id);

        #endregion

        #region SCHEDULED JOB CRUD

        // =====================================================
        // SCHEDULED JOB CRUD
        // =====================================================

        Task<ApiResponse<string>> CreateScheduledJob(
            ScheduledJobDto dto);

        Task<ApiResponse<string>> UpdateScheduledJob(
            ScheduledJobDto dto);

        Task<ApiResponse<string>> DeleteScheduledJob(
            int id);

        Task<ApiResponse<List<ScheduledJobDto>>>
            GetScheduledJobs();

        Task<ApiResponse<ScheduledJobDto>>
            GetScheduledJobById(int id);

        #endregion

    }
}

