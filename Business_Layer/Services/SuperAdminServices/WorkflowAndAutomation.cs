using Business_Layer.DTOs.SuperAdmin;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.SuperAdminInterface;
using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Newtonsoft.Json;
using Serilog;
using Shared.CommonModels;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.SuperAdminServices
{
    public class WorkflowAndAutomation : IWorkflowAndAutomation
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public WorkflowAndAutomation(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        #region WORKFLOW RULE CRUD
        // =====================================================
        // CREATE WORKFLOW RULE
        // =====================================================

        public async Task<ApiResponse<string>> CreateWorkflowRule(
            WorkflowRuleDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.WorkflowRuleName))
                    throw new CustomException(
                        "Workflow Rule Name is required.");

                if (string.IsNullOrWhiteSpace(dto.WorkflowRuleCode))
                    throw new CustomException(
                        "Workflow Rule Code is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (string.IsNullOrWhiteSpace(dto.TriggerEvent))
                    throw new CustomException(
                        "Trigger Event is required.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // DUPLICATE WORKFLOW RULE NAME
                // ---------------------------------------------

                var duplicateName =
                    await _unitOfWork
                        .Repository<WorkflowRule>()
                        .FindAsync(x =>
                            x.WorkflowRuleName.ToLower()
                                == dto.WorkflowRuleName.Trim().ToLower()
                            &&
                            x.CompanyId == dto.CompanyId
                            &&
                            x.RegionId == dto.RegionId
                            &&
                            !x.IsDeleted);

                if (duplicateName.Any())
                {
                    throw new CustomException(
                        "A workflow rule with this name already exists.");
                }


                // ---------------------------------------------
                // DUPLICATE WORKFLOW RULE CODE
                // ---------------------------------------------

                var duplicateCode =
                    await _unitOfWork
                        .Repository<WorkflowRule>()
                        .FindAsync(x =>
                            x.WorkflowRuleCode.ToLower()
                                == dto.WorkflowRuleCode.Trim().ToLower()
                            &&
                            x.CompanyId == dto.CompanyId
                            &&
                            x.RegionId == dto.RegionId
                            &&
                            !x.IsDeleted);

                if (duplicateCode.Any())
                {
                    throw new CustomException(
                        "A workflow rule with this code already exists.");
                }


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                WorkflowRule workflowRule = new WorkflowRule
                {
                    WorkflowRuleName =
                        dto.WorkflowRuleName.Trim(),

                    WorkflowRuleCode =
                        dto.WorkflowRuleCode.Trim(),

                    Description =
                        dto.Description?.Trim(),

                    ModuleName =
                        dto.ModuleName.Trim(),

                    TriggerEvent =
                        dto.TriggerEvent.Trim(),

                    ExecutionType =
                        string.IsNullOrWhiteSpace(dto.ExecutionType)
                            ? "Immediate"
                            : dto.ExecutionType.Trim(),

                    Priority =
                        string.IsNullOrWhiteSpace(dto.Priority)
                            ? "Medium"
                            : dto.Priority.Trim(),

                    ExecutionOrder =
                        dto.ExecutionOrder,

                    StopProcessing =
                        dto.StopProcessing,

                    CompanyId =
                        dto.CompanyId,

                    RegionId =
                        dto.RegionId,

                    IsActive =
                        true,

                    IsDeleted =
                        false,

                    CreatedBy =
                        _currentUserService.UserId,

                    CreatedAt =
                        DateTime.Now
                };


                await _unitOfWork
                    .Repository<WorkflowRule>()
                    .AddAsync(workflowRule);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "WorkflowRules",
                    "INSERT",
                    workflowRule.WorkflowRuleId,
                    "",
                    JsonConvert.SerializeObject(workflowRule),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Workflow Rule Created Successfully",
                    Data = workflowRule.WorkflowRuleCode
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating workflow rule");

                throw;
            }
        }


        // =====================================================
        // UPDATE WORKFLOW RULE
        // =====================================================

        public async Task<ApiResponse<string>> UpdateWorkflowRule(
            WorkflowRuleDto dto)
        {
            try
            {
                var workflowRule =
                    (await _unitOfWork
                        .Repository<WorkflowRule>()
                        .FindAsync(x =>
                            x.WorkflowRuleId == dto.WorkflowRuleId
                            &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (workflowRule == null)
                    throw new CustomException(
                        "Workflow Rule not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.WorkflowRuleName))
                    throw new CustomException(
                        "Workflow Rule Name is required.");

                if (string.IsNullOrWhiteSpace(dto.WorkflowRuleCode))
                    throw new CustomException(
                        "Workflow Rule Code is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (string.IsNullOrWhiteSpace(dto.TriggerEvent))
                    throw new CustomException(
                        "Trigger Event is required.");


                // ---------------------------------------------
                // DUPLICATE NAME
                // ---------------------------------------------

                var duplicateName =
                    await _unitOfWork
                        .Repository<WorkflowRule>()
                        .FindAsync(x =>
                            x.WorkflowRuleId != dto.WorkflowRuleId
                            &&
                            x.WorkflowRuleName.ToLower()
                                == dto.WorkflowRuleName.Trim().ToLower()
                            &&
                            x.CompanyId == dto.CompanyId
                            &&
                            x.RegionId == dto.RegionId
                            &&
                            !x.IsDeleted);

                if (duplicateName.Any())
                    throw new CustomException(
                        "A workflow rule with this name already exists.");


                // ---------------------------------------------
                // DUPLICATE CODE
                // ---------------------------------------------

                var duplicateCode =
                    await _unitOfWork
                        .Repository<WorkflowRule>()
                        .FindAsync(x =>
                            x.WorkflowRuleId != dto.WorkflowRuleId
                            &&
                            x.WorkflowRuleCode.ToLower()
                                == dto.WorkflowRuleCode.Trim().ToLower()
                            &&
                            x.CompanyId == dto.CompanyId
                            &&
                            x.RegionId == dto.RegionId
                            &&
                            !x.IsDeleted);

                if (duplicateCode.Any())
                    throw new CustomException(
                        "A workflow rule with this code already exists.");


                // ---------------------------------------------
                // OLD VALUES
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(workflowRule);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                workflowRule.WorkflowRuleName =
                    dto.WorkflowRuleName.Trim();

                workflowRule.WorkflowRuleCode =
                    dto.WorkflowRuleCode.Trim();

                workflowRule.Description =
                    dto.Description?.Trim();

                workflowRule.ModuleName =
                    dto.ModuleName.Trim();

                workflowRule.TriggerEvent =
                    dto.TriggerEvent.Trim();

                workflowRule.ExecutionType =
                    dto.ExecutionType;

                workflowRule.Priority =
                    dto.Priority;

                workflowRule.ExecutionOrder =
                    dto.ExecutionOrder;

                workflowRule.StopProcessing =
                    dto.StopProcessing;

                workflowRule.CompanyId =
                    dto.CompanyId;

                workflowRule.RegionId =
                    dto.RegionId;

                workflowRule.IsActive =
                    dto.IsActive;

                workflowRule.ModifiedBy =
                    _currentUserService.UserId;

                workflowRule.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<WorkflowRule>()
                    .Update(workflowRule);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "WorkflowRules",
                    "UPDATE",
                    workflowRule.WorkflowRuleId,
                    oldValues,
                    JsonConvert.SerializeObject(workflowRule),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Workflow Rule Updated Successfully",
                    Data = workflowRule.WorkflowRuleCode
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating workflow rule");

                throw;
            }
        }


        // =====================================================
        // DELETE WORKFLOW RULE
        // =====================================================

        public async Task<ApiResponse<string>> DeleteWorkflowRule(
            int id)
        {
            try
            {
                var workflowRule =
                    (await _unitOfWork
                        .Repository<WorkflowRule>()
                        .FindAsync(x =>
                            x.WorkflowRuleId == id
                            &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (workflowRule == null)
                    throw new CustomException(
                        "Workflow Rule not found.");


                string oldValues =
                    JsonConvert.SerializeObject(workflowRule);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                workflowRule.IsDeleted = true;

                workflowRule.IsActive = false;

                workflowRule.ModifiedBy =
                    _currentUserService.UserId;

                workflowRule.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<WorkflowRule>()
                    .Update(workflowRule);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "WorkflowRules",
                    "DELETE",
                    workflowRule.WorkflowRuleId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Workflow Rule Deleted Successfully",
                    Data = workflowRule.WorkflowRuleCode
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting workflow rule");

                throw;
            }
        }


        // =====================================================
        // GET ALL WORKFLOW RULES
        // =====================================================

        public async Task<ApiResponse<List<WorkflowRuleDto>>>
            GetWorkflowRules()
        {
            try
            {
                var workflowRules =
                    await _unitOfWork
                        .Repository<WorkflowRule>()
                        .GetAllAsync();


                var result =
                    workflowRules
                        .Where(x => !x.IsDeleted)
                        .OrderBy(x => x.ExecutionOrder ?? int.MaxValue)
                        .ThenByDescending(x => x.WorkflowRuleId)
                        .Select(x => new WorkflowRuleDto
                        {
                            WorkflowRuleId =
                                x.WorkflowRuleId,

                            WorkflowRuleName =
                                x.WorkflowRuleName,

                            WorkflowRuleCode =
                                x.WorkflowRuleCode,

                            Description =
                                x.Description,

                            ModuleName =
                                x.ModuleName,

                            TriggerEvent =
                                x.TriggerEvent,

                            ExecutionType =
                                x.ExecutionType,

                            Priority =
                                x.Priority,

                            ExecutionOrder =
                                x.ExecutionOrder,

                            StopProcessing =
                                x.StopProcessing,

                            CompanyId =
                                x.CompanyId,

                            RegionId =
                                x.RegionId,

                            IsActive =
                                x.IsActive,

                            CreatedAt =
                                x.CreatedAt,

                            ModifiedAt =
                                x.ModifiedAt
                        })
                        .ToList();


                return new ApiResponse<List<WorkflowRuleDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting workflow rules");

                throw;
            }
        }


        // =====================================================
        // GET WORKFLOW RULE BY ID
        // =====================================================

        public async Task<ApiResponse<WorkflowRuleDto>>
            GetWorkflowRuleById(int id)
        {
            try
            {
                var workflowRule =
                    (await _unitOfWork
                        .Repository<WorkflowRule>()
                        .FindAsync(x =>
                            x.WorkflowRuleId == id
                            &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (workflowRule == null)
                    throw new CustomException(
                        "Workflow Rule not found.");


                var result = new WorkflowRuleDto
                {
                    WorkflowRuleId =
                        workflowRule.WorkflowRuleId,

                    WorkflowRuleName =
                        workflowRule.WorkflowRuleName,

                    WorkflowRuleCode =
                        workflowRule.WorkflowRuleCode,

                    Description =
                        workflowRule.Description,

                    ModuleName =
                        workflowRule.ModuleName,

                    TriggerEvent =
                        workflowRule.TriggerEvent,

                    ExecutionType =
                        workflowRule.ExecutionType,

                    Priority =
                        workflowRule.Priority,

                    ExecutionOrder =
                        workflowRule.ExecutionOrder,

                    StopProcessing =
                        workflowRule.StopProcessing,

                    CompanyId =
                        workflowRule.CompanyId,

                    RegionId =
                        workflowRule.RegionId,

                    IsActive =
                        workflowRule.IsActive,

                    CreatedAt =
                        workflowRule.CreatedAt,

                    ModifiedAt =
                        workflowRule.ModifiedAt
                };


                return new ApiResponse<WorkflowRuleDto>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting workflow rule by id");

                throw;
            }
        }
        #endregion
        #region WORKFLOW RULE CONDITION CRUD
        // =====================================================
        // CREATE WORKFLOW RULE CONDITION
        // =====================================================

        public async Task<ApiResponse<string>>
            CreateWorkflowRuleCondition(
                WorkflowRuleConditionDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (dto.WorkflowRuleId <= 0)
                    throw new CustomException(
                        "Workflow Rule is required.");

                if (string.IsNullOrWhiteSpace(dto.FieldName))
                    throw new CustomException(
                        "Field Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Operator))
                    throw new CustomException(
                        "Operator is required.");

                if (dto.ConditionOrder <= 0)
                    throw new CustomException(
                        "Condition Order must be greater than zero.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // CHECK WORKFLOW RULE
                // ---------------------------------------------

                var workflowRule =
                    (await _unitOfWork
                        .Repository<WorkflowRule>()
                        .FindAsync(x =>
                            x.WorkflowRuleId ==
                                dto.WorkflowRuleId
                            &&
                            x.CompanyId == dto.CompanyId
                            &&
                            x.RegionId == dto.RegionId
                            &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (workflowRule == null)
                    throw new CustomException(
                        "Workflow Rule not found.");


                // ---------------------------------------------
                // DUPLICATE CONDITION ORDER
                // ---------------------------------------------

                var duplicateOrder =
                    await _unitOfWork
                        .Repository<WorkflowRuleCondition>()
                        .FindAsync(x =>
                            x.WorkflowRuleId ==
                                dto.WorkflowRuleId
                            &&
                            x.ConditionOrder ==
                                dto.ConditionOrder
                            &&
                            !x.IsDeleted);

                if (duplicateOrder.Any())
                    throw new CustomException(
                        "This Condition Order already exists for the workflow rule.");


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                WorkflowRuleCondition condition =
                    new WorkflowRuleCondition
                    {
                        WorkflowRuleId =
                            dto.WorkflowRuleId,

                        FieldName =
                            dto.FieldName.Trim(),

                        Operator =
                            dto.Operator.Trim(),

                        FieldValue =
                            dto.FieldValue?.Trim(),

                        LogicalOperator =
                            dto.LogicalOperator?.Trim(),

                        ConditionOrder =
                            dto.ConditionOrder,

                        CompanyId =
                            dto.CompanyId,

                        RegionId =
                            dto.RegionId,

                        IsActive =
                            true,

                        IsDeleted =
                            false,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedAt =
                            DateTime.Now
                    };


                await _unitOfWork
                    .Repository<WorkflowRuleCondition>()
                    .AddAsync(condition);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "WorkflowRuleConditions",
                    "INSERT",
                    condition.WorkflowRuleConditionId,
                    "",
                    JsonConvert.SerializeObject(condition),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Workflow Rule Condition Created Successfully",

                    Data =
                        condition.WorkflowRuleConditionId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating workflow rule condition");

                throw;
            }
        }


        // =====================================================
        // UPDATE WORKFLOW RULE CONDITION
        // =====================================================

        public async Task<ApiResponse<string>>
            UpdateWorkflowRuleCondition(
                WorkflowRuleConditionDto dto)
        {
            try
            {
                var condition =
                    (await _unitOfWork
                        .Repository<WorkflowRuleCondition>()
                        .FindAsync(x =>
                            x.WorkflowRuleConditionId ==
                                dto.WorkflowRuleConditionId
                            &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (condition == null)
                    throw new CustomException(
                        "Workflow Rule Condition not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (dto.WorkflowRuleId <= 0)
                    throw new CustomException(
                        "Workflow Rule is required.");

                if (string.IsNullOrWhiteSpace(dto.FieldName))
                    throw new CustomException(
                        "Field Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Operator))
                    throw new CustomException(
                        "Operator is required.");

                if (dto.ConditionOrder <= 0)
                    throw new CustomException(
                        "Condition Order must be greater than zero.");


                // ---------------------------------------------
                // CHECK WORKFLOW RULE
                // ---------------------------------------------

                var workflowRule =
                    (await _unitOfWork
                        .Repository<WorkflowRule>()
                        .FindAsync(x =>
                            x.WorkflowRuleId ==
                                dto.WorkflowRuleId
                            &&
                            x.CompanyId == dto.CompanyId
                            &&
                            x.RegionId == dto.RegionId
                            &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (workflowRule == null)
                    throw new CustomException(
                        "Workflow Rule not found.");


                // ---------------------------------------------
                // DUPLICATE ORDER
                // ---------------------------------------------

                var duplicateOrder =
                    await _unitOfWork
                        .Repository<WorkflowRuleCondition>()
                        .FindAsync(x =>
                            x.WorkflowRuleConditionId !=
                                dto.WorkflowRuleConditionId
                            &&
                            x.WorkflowRuleId ==
                                dto.WorkflowRuleId
                            &&
                            x.ConditionOrder ==
                                dto.ConditionOrder
                            &&
                            !x.IsDeleted);

                if (duplicateOrder.Any())
                    throw new CustomException(
                        "This Condition Order already exists for the workflow rule.");


                // ---------------------------------------------
                // OLD VALUES
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(condition);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                condition.WorkflowRuleId =
                    dto.WorkflowRuleId;

                condition.FieldName =
                    dto.FieldName.Trim();

                condition.Operator =
                    dto.Operator.Trim();

                condition.FieldValue =
                    dto.FieldValue?.Trim();

                condition.LogicalOperator =
                    dto.LogicalOperator?.Trim();

                condition.ConditionOrder =
                    dto.ConditionOrder;

                condition.CompanyId =
                    dto.CompanyId;

                condition.RegionId =
                    dto.RegionId;

                condition.IsActive =
                    dto.IsActive;

                condition.ModifiedBy =
                    _currentUserService.UserId;

                condition.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<WorkflowRuleCondition>()
                    .Update(condition);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "WorkflowRuleConditions",
                    "UPDATE",
                    condition.WorkflowRuleConditionId,
                    oldValues,
                    JsonConvert.SerializeObject(condition),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Workflow Rule Condition Updated Successfully",

                    Data =
                        condition.WorkflowRuleConditionId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating workflow rule condition");

                throw;
            }
        }


        // =====================================================
        // DELETE WORKFLOW RULE CONDITION
        // =====================================================

        public async Task<ApiResponse<string>>
            DeleteWorkflowRuleCondition(int id)
        {
            try
            {
                var condition =
                    (await _unitOfWork
                        .Repository<WorkflowRuleCondition>()
                        .FindAsync(x =>
                            x.WorkflowRuleConditionId == id
                            &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (condition == null)
                    throw new CustomException(
                        "Workflow Rule Condition not found.");


                string oldValues =
                    JsonConvert.SerializeObject(condition);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                condition.IsDeleted = true;

                condition.IsActive = false;

                condition.ModifiedBy =
                    _currentUserService.UserId;

                condition.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<WorkflowRuleCondition>()
                    .Update(condition);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "WorkflowRuleConditions",
                    "DELETE",
                    condition.WorkflowRuleConditionId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Workflow Rule Condition Deleted Successfully",

                    Data =
                        condition.WorkflowRuleConditionId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting workflow rule condition");

                throw;
            }
        }


        // =====================================================
        // GET ALL CONDITIONS
        // =====================================================

        public async Task<ApiResponse<List<WorkflowRuleConditionDto>>>
            GetWorkflowRuleConditions()
        {
            try
            {
                var conditions =
                    await _unitOfWork
                        .Repository<WorkflowRuleCondition>()
                        .GetAllAsync();

                var workflowRules =
                    await _unitOfWork
                        .Repository<WorkflowRule>()
                        .GetAllAsync();


                var result =
                    (from c in conditions

                     join w in workflowRules
                         on c.WorkflowRuleId
                         equals w.WorkflowRuleId
                         into workflowGroup

                     from w in workflowGroup
                         .DefaultIfEmpty()

                     where !c.IsDeleted

                     select new WorkflowRuleConditionDto
                     {
                         WorkflowRuleConditionId =
                             c.WorkflowRuleConditionId,

                         WorkflowRuleId =
                             c.WorkflowRuleId,

                         WorkflowRuleName =
                             w != null
                                 ? w.WorkflowRuleName
                                 : null,

                         FieldName =
                             c.FieldName,

                         Operator =
                             c.Operator,

                         FieldValue =
                             c.FieldValue,

                         LogicalOperator =
                             c.LogicalOperator,

                         ConditionOrder =
                             c.ConditionOrder,

                         CompanyId =
                             c.CompanyId,

                         RegionId =
                             c.RegionId,

                         IsActive =
                             c.IsActive,

                         CreatedAt =
                             c.CreatedAt,

                         ModifiedAt =
                             c.ModifiedAt
                     })
                    .OrderBy(x => x.WorkflowRuleId)
                    .ThenBy(x => x.ConditionOrder)
                    .ToList();


                return new ApiResponse<List<WorkflowRuleConditionDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting workflow rule conditions");

                throw;
            }
        }


        // =====================================================
        // GET CONDITION BY ID
        // =====================================================

        public async Task<ApiResponse<WorkflowRuleConditionDto>>
            GetWorkflowRuleConditionById(int id)
        {
            try
            {
                var condition =
                    (await _unitOfWork
                        .Repository<WorkflowRuleCondition>()
                        .FindAsync(x =>
                            x.WorkflowRuleConditionId == id
                            &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (condition == null)
                    throw new CustomException(
                        "Workflow Rule Condition not found.");


                var workflowRule =
                    (await _unitOfWork
                        .Repository<WorkflowRule>()
                        .FindAsync(x =>
                            x.WorkflowRuleId ==
                                condition.WorkflowRuleId
                            &&
                            !x.IsDeleted))
                    .FirstOrDefault();


                var result =
                    new WorkflowRuleConditionDto
                    {
                        WorkflowRuleConditionId =
                            condition.WorkflowRuleConditionId,

                        WorkflowRuleId =
                            condition.WorkflowRuleId,

                        WorkflowRuleName =
                            workflowRule?.WorkflowRuleName,

                        FieldName =
                            condition.FieldName,

                        Operator =
                            condition.Operator,

                        FieldValue =
                            condition.FieldValue,

                        LogicalOperator =
                            condition.LogicalOperator,

                        ConditionOrder =
                            condition.ConditionOrder,

                        CompanyId =
                            condition.CompanyId,

                        RegionId =
                            condition.RegionId,

                        IsActive =
                            condition.IsActive,

                        CreatedAt =
                            condition.CreatedAt,

                        ModifiedAt =
                            condition.ModifiedAt
                    };


                return new ApiResponse<WorkflowRuleConditionDto>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting workflow rule condition by id");

                throw;
            }
        }


        // =====================================================
        // GET CONDITIONS BY WORKFLOW RULE ID
        // =====================================================

        public async Task<ApiResponse<List<WorkflowRuleConditionDto>>>
            GetConditionsByWorkflowRuleId(
                int workflowRuleId)
        {
            try
            {
                if (workflowRuleId <= 0)
                    throw new CustomException(
                        "Workflow Rule is required.");


                var workflowRule =
                    (await _unitOfWork
                        .Repository<WorkflowRule>()
                        .FindAsync(x =>
                            x.WorkflowRuleId ==
                                workflowRuleId
                            &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (workflowRule == null)
                    throw new CustomException(
                        "Workflow Rule not found.");


                var conditions =
                    await _unitOfWork
                        .Repository<WorkflowRuleCondition>()
                        .FindAsync(x =>
                            x.WorkflowRuleId ==
                                workflowRuleId
                            &&
                            !x.IsDeleted);


                var result =
                    conditions
                        .OrderBy(x => x.ConditionOrder)
                        .Select(x =>
                            new WorkflowRuleConditionDto
                            {
                                WorkflowRuleConditionId =
                                    x.WorkflowRuleConditionId,

                                WorkflowRuleId =
                                    x.WorkflowRuleId,

                                WorkflowRuleName =
                                    workflowRule.WorkflowRuleName,

                                FieldName =
                                    x.FieldName,

                                Operator =
                                    x.Operator,

                                FieldValue =
                                    x.FieldValue,

                                LogicalOperator =
                                    x.LogicalOperator,

                                ConditionOrder =
                                    x.ConditionOrder,

                                CompanyId =
                                    x.CompanyId,

                                RegionId =
                                    x.RegionId,

                                IsActive =
                                    x.IsActive,

                                CreatedAt =
                                    x.CreatedAt,

                                ModifiedAt =
                                    x.ModifiedAt
                            })
                        .ToList();


                return new ApiResponse<List<WorkflowRuleConditionDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting conditions by workflow rule id");

                throw;
            }
        }
        #endregion
        #region  WorkflowRuleAction CRUD
        // =====================================================
        // CREATE
        // =====================================================

        public async Task<ApiResponse<string>>
            CreateWorkflowRuleAction(
                WorkflowRuleActionDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (dto.WorkflowRuleId <= 0)
                    throw new CustomException(
                        "Workflow Rule is required.");

                if (string.IsNullOrWhiteSpace(dto.ActionType))
                    throw new CustomException(
                        "Action Type is required.");

                if (dto.ActionOrder < 0)
                    throw new CustomException(
                        "Action Order cannot be negative.");


                // ---------------------------------------------
                // CHECK WORKFLOW RULE
                // ---------------------------------------------

                var workflowRule =
                    (await _unitOfWork
                        .Repository<WorkflowRule>()
                        .FindAsync(x =>
                            x.WorkflowRuleId == dto.WorkflowRuleId &&
                            !x.IsDeleted &&
                            x.IsActive))
                    .FirstOrDefault();

                if (workflowRule == null)
                    throw new CustomException(
                        "Workflow Rule not found.");


                // ---------------------------------------------
                // DUPLICATE ACTION ORDER
                // ---------------------------------------------

                var duplicateOrder =
                    await _unitOfWork
                        .Repository<WorkflowRuleAction>()
                        .FindAsync(x =>
                            x.WorkflowRuleId == dto.WorkflowRuleId &&
                            x.ActionOrder == dto.ActionOrder &&
                            !x.IsDeleted);

                if (duplicateOrder.Any())
                    throw new CustomException(
                        "An action with this order already exists for the selected Workflow Rule.");


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                WorkflowRuleAction action =
                    new WorkflowRuleAction
                    {
                        WorkflowRuleId =
                            dto.WorkflowRuleId,

                        ActionType =
                            dto.ActionType.Trim(),

                        ActionName =
                            string.IsNullOrWhiteSpace(dto.ActionName)
                                ? null
                                : dto.ActionName.Trim(),

                        ActionConfiguration =
                            dto.ActionConfiguration,

                        ActionOrder =
                            dto.ActionOrder,

                        CompanyId =
                            dto.CompanyId,

                        RegionId =
                            dto.RegionId,

                        IsActive = true,

                        IsDeleted = false,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedAt =
                            DateTime.Now
                    };


                await _unitOfWork
                    .Repository<WorkflowRuleAction>()
                    .AddAsync(action);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "WorkflowRuleActions",
                    "INSERT",
                    action.WorkflowRuleActionId,
                    "",
                    JsonConvert.SerializeObject(action),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Workflow Rule Action Created Successfully",
                    Data =
                        action.WorkflowRuleActionId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating workflow rule action");

                throw;
            }
        }


        // =====================================================
        // UPDATE
        // =====================================================

        public async Task<ApiResponse<string>>
            UpdateWorkflowRuleAction(
                WorkflowRuleActionDto dto)
        {
            try
            {
                var action =
                    (await _unitOfWork
                        .Repository<WorkflowRuleAction>()
                        .FindAsync(x =>
                            x.WorkflowRuleActionId ==
                                dto.WorkflowRuleActionId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (action == null)
                    throw new CustomException(
                        "Workflow Rule Action not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (dto.WorkflowRuleId <= 0)
                    throw new CustomException(
                        "Workflow Rule is required.");

                if (string.IsNullOrWhiteSpace(dto.ActionType))
                    throw new CustomException(
                        "Action Type is required.");

                if (dto.ActionOrder < 0)
                    throw new CustomException(
                        "Action Order cannot be negative.");


                // ---------------------------------------------
                // CHECK WORKFLOW RULE
                // ---------------------------------------------

                var workflowRule =
                    (await _unitOfWork
                        .Repository<WorkflowRule>()
                        .FindAsync(x =>
                            x.WorkflowRuleId ==
                                dto.WorkflowRuleId &&
                            !x.IsDeleted &&
                            x.IsActive))
                    .FirstOrDefault();

                if (workflowRule == null)
                    throw new CustomException(
                        "Workflow Rule not found.");


                // ---------------------------------------------
                // DUPLICATE ORDER
                // ---------------------------------------------

                var duplicateOrder =
                    await _unitOfWork
                        .Repository<WorkflowRuleAction>()
                        .FindAsync(x =>
                            x.WorkflowRuleId ==
                                dto.WorkflowRuleId &&
                            x.ActionOrder ==
                                dto.ActionOrder &&
                            x.WorkflowRuleActionId !=
                                dto.WorkflowRuleActionId &&
                            !x.IsDeleted);

                if (duplicateOrder.Any())
                    throw new CustomException(
                        "An action with this order already exists for the selected Workflow Rule.");


                string oldValues =
                    JsonConvert.SerializeObject(action);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                action.WorkflowRuleId =
                    dto.WorkflowRuleId;

                action.ActionType =
                    dto.ActionType.Trim();

                action.ActionName =
                    string.IsNullOrWhiteSpace(dto.ActionName)
                        ? null
                        : dto.ActionName.Trim();

                action.ActionConfiguration =
                    dto.ActionConfiguration;

                action.ActionOrder =
                    dto.ActionOrder;

                action.CompanyId =
                    dto.CompanyId;

                action.RegionId =
                    dto.RegionId;

                action.IsActive =
                    dto.IsActive;

                action.ModifiedBy =
                    _currentUserService.UserId;

                action.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<WorkflowRuleAction>()
                    .Update(action);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "WorkflowRuleActions",
                    "UPDATE",
                    action.WorkflowRuleActionId,
                    oldValues,
                    JsonConvert.SerializeObject(action),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Workflow Rule Action Updated Successfully",
                    Data =
                        action.WorkflowRuleActionId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating workflow rule action");

                throw;
            }
        }


        // =====================================================
        // DELETE
        // =====================================================

        public async Task<ApiResponse<string>>
            DeleteWorkflowRuleAction(int id)
        {
            try
            {
                var action =
                    (await _unitOfWork
                        .Repository<WorkflowRuleAction>()
                        .FindAsync(x =>
                            x.WorkflowRuleActionId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (action == null)
                    throw new CustomException(
                        "Workflow Rule Action not found.");


                string oldValues =
                    JsonConvert.SerializeObject(action);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                action.IsDeleted = true;
                action.IsActive = false;

                action.ModifiedBy =
                    _currentUserService.UserId;

                action.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<WorkflowRuleAction>()
                    .Update(action);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "WorkflowRuleActions",
                    "DELETE",
                    action.WorkflowRuleActionId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Workflow Rule Action Deleted Successfully",
                    Data =
                        action.WorkflowRuleActionId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting workflow rule action");

                throw;
            }
        }


        // =====================================================
        // GET ALL
        // =====================================================

        public async Task<ApiResponse<List<WorkflowRuleActionDto>>>
            GetWorkflowRuleActions()
        {
            var actions =
                await _unitOfWork
                    .Repository<WorkflowRuleAction>()
                    .GetAllAsync();

            var workflowRules =
                await _unitOfWork
                    .Repository<WorkflowRule>()
                    .GetAllAsync();


            var result =
                (from action in actions

                 join rule in workflowRules
                     on action.WorkflowRuleId
                     equals rule.WorkflowRuleId
                     into ruleGroup

                 from rule in ruleGroup.DefaultIfEmpty()

                 where !action.IsDeleted

                 select new WorkflowRuleActionDto
                 {
                     WorkflowRuleActionId =
                         action.WorkflowRuleActionId,

                     WorkflowRuleId =
                         action.WorkflowRuleId,

                     WorkflowRuleName =
                         rule != null
                             ? rule.WorkflowRuleName
                             : null,

                     ActionType =
                         action.ActionType,

                     ActionName =
                         action.ActionName,

                     ActionConfiguration =
                         action.ActionConfiguration,

                     ActionOrder =
                         action.ActionOrder,

                     CompanyId =
                         action.CompanyId,

                     RegionId =
                         action.RegionId,

                     IsActive =
                         action.IsActive,

                     CreatedAt =
                         action.CreatedAt,

                     ModifiedAt =
                         action.ModifiedAt
                 })
                .OrderBy(x => x.WorkflowRuleId)
                .ThenBy(x => x.ActionOrder)
                .ToList();


            return new ApiResponse<List<WorkflowRuleActionDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        // =====================================================
        // GET BY ID
        // =====================================================

        public async Task<ApiResponse<WorkflowRuleActionDto>>
            GetWorkflowRuleActionById(int id)
        {
            var action =
                (await _unitOfWork
                    .Repository<WorkflowRuleAction>()
                    .FindAsync(x =>
                        x.WorkflowRuleActionId == id &&
                        !x.IsDeleted))
                .FirstOrDefault();

            if (action == null)
                throw new CustomException(
                    "Workflow Rule Action not found.");


            var workflowRule =
                (await _unitOfWork
                    .Repository<WorkflowRule>()
                    .FindAsync(x =>
                        x.WorkflowRuleId ==
                            action.WorkflowRuleId &&
                        !x.IsDeleted))
                .FirstOrDefault();


            return new ApiResponse<WorkflowRuleActionDto>
            {
                Success = true,
                Message = "Success",

                Data = new WorkflowRuleActionDto
                {
                    WorkflowRuleActionId =
                        action.WorkflowRuleActionId,

                    WorkflowRuleId =
                        action.WorkflowRuleId,

                    WorkflowRuleName =
                        workflowRule?.WorkflowRuleName,

                    ActionType =
                        action.ActionType,

                    ActionName =
                        action.ActionName,

                    ActionConfiguration =
                        action.ActionConfiguration,

                    ActionOrder =
                        action.ActionOrder,

                    CompanyId =
                        action.CompanyId,

                    RegionId =
                        action.RegionId,

                    IsActive =
                        action.IsActive,

                    CreatedAt =
                        action.CreatedAt,

                    ModifiedAt =
                        action.ModifiedAt
                }
            };
        }
        #endregion
        #region APPROVAL WORKFLOW CRUD
        // =====================================================
        // CREATE APPROVAL WORKFLOW
        // =====================================================

        public async Task<ApiResponse<string>>
            CreateApprovalWorkflow(
                ApprovalWorkflowDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.WorkflowName))
                    throw new CustomException(
                        "Workflow Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ApprovalType))
                    throw new CustomException(
                        "Approval Type is required.");

                if (dto.ApprovalLevels <= 0)
                    throw new CustomException(
                        "Approval Levels must be greater than zero.");


                // ---------------------------------------------
                // DUPLICATE WORKFLOW
                // ---------------------------------------------

                var duplicateWorkflow =
                    await _unitOfWork
                        .Repository<ApprovalWorkflow>()
                        .FindAsync(x =>
                            x.WorkflowName.ToLower() ==
                                dto.WorkflowName.Trim().ToLower() &&
                            x.ModuleName.ToLower() ==
                                dto.ModuleName.Trim().ToLower() &&
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId &&
                            !x.IsDeleted);

                if (duplicateWorkflow.Any())
                    throw new CustomException(
                        "An approval workflow with this name already exists for the selected module.");


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                ApprovalWorkflow workflow =
                    new ApprovalWorkflow
                    {
                        WorkflowName =
                            dto.WorkflowName.Trim(),

                        ModuleName =
                            dto.ModuleName.Trim(),

                        Description =
                            dto.Description,

                        ApprovalType =
                            dto.ApprovalType.Trim(),

                        ApprovalLevels =
                            dto.ApprovalLevels,

                        FinalApprovalAction =
                            dto.FinalApprovalAction,

                        FinalRejectionAction =
                            dto.FinalRejectionAction,

                        CompanyId =
                            dto.CompanyId,

                        RegionId =
                            dto.RegionId,

                        IsActive = true,

                        IsDeleted = false,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedAt =
                            DateTime.Now
                    };


                await _unitOfWork
                    .Repository<ApprovalWorkflow>()
                    .AddAsync(workflow);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "ApprovalWorkflow",
                    "INSERT",
                    workflow.ApprovalWorkflowId,
                    "",
                    JsonConvert.SerializeObject(workflow),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Approval Workflow Created Successfully",
                    Data =
                        workflow.ApprovalWorkflowId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating approval workflow");

                throw;
            }
        }


        // =====================================================
        // UPDATE APPROVAL WORKFLOW
        // =====================================================

        public async Task<ApiResponse<string>>
            UpdateApprovalWorkflow(
                ApprovalWorkflowDto dto)
        {
            try
            {
                var workflow =
                    (await _unitOfWork
                        .Repository<ApprovalWorkflow>()
                        .FindAsync(x =>
                            x.ApprovalWorkflowId ==
                                dto.ApprovalWorkflowId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (workflow == null)
                    throw new CustomException(
                        "Approval Workflow not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.WorkflowName))
                    throw new CustomException(
                        "Workflow Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ApprovalType))
                    throw new CustomException(
                        "Approval Type is required.");

                if (dto.ApprovalLevels <= 0)
                    throw new CustomException(
                        "Approval Levels must be greater than zero.");


                // ---------------------------------------------
                // DUPLICATE WORKFLOW
                // ---------------------------------------------

                var duplicateWorkflow =
                    await _unitOfWork
                        .Repository<ApprovalWorkflow>()
                        .FindAsync(x =>
                            x.ApprovalWorkflowId !=
                                dto.ApprovalWorkflowId &&
                            x.WorkflowName.ToLower() ==
                                dto.WorkflowName.Trim().ToLower() &&
                            x.ModuleName.ToLower() ==
                                dto.ModuleName.Trim().ToLower() &&
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId &&
                            !x.IsDeleted);

                if (duplicateWorkflow.Any())
                    throw new CustomException(
                        "An approval workflow with this name already exists for the selected module.");


                string oldValues =
                    JsonConvert.SerializeObject(workflow);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                workflow.WorkflowName =
                    dto.WorkflowName.Trim();

                workflow.ModuleName =
                    dto.ModuleName.Trim();

                workflow.Description =
                    dto.Description;

                workflow.ApprovalType =
                    dto.ApprovalType.Trim();

                workflow.ApprovalLevels =
                    dto.ApprovalLevels;

                workflow.FinalApprovalAction =
                    dto.FinalApprovalAction;

                workflow.FinalRejectionAction =
                    dto.FinalRejectionAction;

                workflow.CompanyId =
                    dto.CompanyId;

                workflow.RegionId =
                    dto.RegionId;

                workflow.IsActive =
                    dto.IsActive;

                workflow.ModifiedBy =
                    _currentUserService.UserId;

                workflow.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<ApprovalWorkflow>()
                    .Update(workflow);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "ApprovalWorkflow",
                    "UPDATE",
                    workflow.ApprovalWorkflowId,
                    oldValues,
                    JsonConvert.SerializeObject(workflow),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Approval Workflow Updated Successfully",
                    Data =
                        workflow.ApprovalWorkflowId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating approval workflow");

                throw;
            }
        }


        // =====================================================
        // DELETE APPROVAL WORKFLOW
        // =====================================================

        public async Task<ApiResponse<string>>
            DeleteApprovalWorkflow(int id)
        {
            try
            {
                var workflow =
                    (await _unitOfWork
                        .Repository<ApprovalWorkflow>()
                        .FindAsync(x =>
                            x.ApprovalWorkflowId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (workflow == null)
                    throw new CustomException(
                        "Approval Workflow not found.");


                string oldValues =
                    JsonConvert.SerializeObject(workflow);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                workflow.IsDeleted = true;
                workflow.IsActive = false;

                workflow.ModifiedBy =
                    _currentUserService.UserId;

                workflow.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<ApprovalWorkflow>()
                    .Update(workflow);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "ApprovalWorkflow",
                    "DELETE",
                    workflow.ApprovalWorkflowId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Approval Workflow Deleted Successfully",
                    Data =
                        workflow.ApprovalWorkflowId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting approval workflow");

                throw;
            }
        }


        // =====================================================
        // GET ALL APPROVAL WORKFLOWS
        // =====================================================

        public async Task<ApiResponse<List<ApprovalWorkflowDto>>>
            GetApprovalWorkflows()
        {
            var workflows =
                await _unitOfWork
                    .Repository<ApprovalWorkflow>()
                    .GetAllAsync();


            var result =
                workflows
                    .Where(x => !x.IsDeleted)
                    .OrderByDescending(x =>
                        x.ApprovalWorkflowId)
                    .Select(x =>
                        new ApprovalWorkflowDto
                        {
                            ApprovalWorkflowId =
                                x.ApprovalWorkflowId,

                            WorkflowName =
                                x.WorkflowName,

                            ModuleName =
                                x.ModuleName,

                            Description =
                                x.Description,

                            ApprovalType =
                                x.ApprovalType,

                            ApprovalLevels =
                                x.ApprovalLevels,

                            FinalApprovalAction =
                                x.FinalApprovalAction,

                            FinalRejectionAction =
                                x.FinalRejectionAction,

                            CompanyId =
                                x.CompanyId,

                            RegionId =
                                x.RegionId,

                            IsActive =
                                x.IsActive,

                            CreatedAt =
                                x.CreatedAt,

                            ModifiedAt =
                                x.ModifiedAt
                        })
                    .ToList();


            return new ApiResponse<List<ApprovalWorkflowDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        // =====================================================
        // GET BY ID
        // =====================================================

        public async Task<ApiResponse<ApprovalWorkflowDto>>
            GetApprovalWorkflowById(int id)
        {
            var workflow =
                (await _unitOfWork
                    .Repository<ApprovalWorkflow>()
                    .FindAsync(x =>
                        x.ApprovalWorkflowId == id &&
                        !x.IsDeleted))
                .FirstOrDefault();

            if (workflow == null)
                throw new CustomException(
                    "Approval Workflow not found.");


            return new ApiResponse<ApprovalWorkflowDto>
            {
                Success = true,
                Message = "Success",

                Data = new ApprovalWorkflowDto
                {
                    ApprovalWorkflowId =
                        workflow.ApprovalWorkflowId,

                    WorkflowName =
                        workflow.WorkflowName,

                    ModuleName =
                        workflow.ModuleName,

                    Description =
                        workflow.Description,

                    ApprovalType =
                        workflow.ApprovalType,

                    ApprovalLevels =
                        workflow.ApprovalLevels,

                    FinalApprovalAction =
                        workflow.FinalApprovalAction,

                    FinalRejectionAction =
                        workflow.FinalRejectionAction,

                    CompanyId =
                        workflow.CompanyId,

                    RegionId =
                        workflow.RegionId,

                    IsActive =
                        workflow.IsActive,

                    CreatedAt =
                        workflow.CreatedAt,

                    ModifiedAt =
                        workflow.ModifiedAt
                }
            };
        }
        #endregion
        #region APPROVAL WORKFLOW LEVEL CRUD
        // =====================================================
        // CREATE APPROVAL WORKFLOW LEVEL
        // =====================================================

        public async Task<ApiResponse<string>>
            CreateApprovalWorkflowLevel(
                ApprovalWorkflowLevelDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (dto.ApprovalWorkflowId <= 0)
                    throw new CustomException(
                        "Approval Workflow is required.");

                if (dto.LevelNumber <= 0)
                    throw new CustomException(
                        "Level Number must be greater than zero.");

                if (string.IsNullOrWhiteSpace(dto.ApproverType))
                    throw new CustomException(
                        "Approver Type is required.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // CHECK WORKFLOW
                // ---------------------------------------------

                var workflow =
                    (await _unitOfWork
                        .Repository<ApprovalWorkflow>()
                        .FindAsync(x =>
                            x.ApprovalWorkflowId ==
                                dto.ApprovalWorkflowId &&
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (workflow == null)
                    throw new CustomException(
                        "Approval Workflow not found.");


                // ---------------------------------------------
                // DUPLICATE LEVEL
                // ---------------------------------------------

                var duplicateLevel =
                    await _unitOfWork
                        .Repository<ApprovalWorkflowLevel>()
                        .FindAsync(x =>
                            x.ApprovalWorkflowId ==
                                dto.ApprovalWorkflowId &&
                            x.LevelNumber == dto.LevelNumber &&
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId &&
                            !x.IsDeleted);

                if (duplicateLevel.Any())
                    throw new CustomException(
                        "This level number already exists for the selected workflow.");


                // ---------------------------------------------
                // APPROVER VALIDATION
                // ---------------------------------------------

                if (dto.ApproverType.Equals(
                        "User",
                        StringComparison.OrdinalIgnoreCase)
                    && (!dto.ApproverUserId.HasValue ||
                        dto.ApproverUserId.Value <= 0))
                {
                    throw new CustomException(
                        "Approver User is required.");
                }

                if (dto.ApproverType.Equals(
                        "Role",
                        StringComparison.OrdinalIgnoreCase)
                    && (!dto.ApproverRoleId.HasValue ||
                        dto.ApproverRoleId.Value <= 0))
                {
                    throw new CustomException(
                        "Approver Role is required.");
                }


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                var entity = new ApprovalWorkflowLevel
                {
                    ApprovalWorkflowId =
                        dto.ApprovalWorkflowId,

                    LevelNumber =
                        dto.LevelNumber,

                    ApproverType =
                        dto.ApproverType.Trim(),

                    ApproverUserId =
                        dto.ApproverUserId,

                    ApproverRoleId =
                        dto.ApproverRoleId,

                    ApprovalCondition =
                        dto.ApprovalCondition,

                    OnApprovalAction =
                        dto.OnApprovalAction,

                    OnRejectionAction =
                        dto.OnRejectionAction,

                    CompanyId =
                        dto.CompanyId,

                    RegionId =
                        dto.RegionId,

                    IsActive = true,

                    IsDeleted = false,

                    CreatedBy =
                        _currentUserService.UserId,

                    CreatedAt =
                        DateTime.Now
                };


                await _unitOfWork
                    .Repository<ApprovalWorkflowLevel>()
                    .AddAsync(entity);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "ApprovalWorkflowLevel",
                    "INSERT",
                    entity.ApprovalWorkflowLevelId,
                    "",
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Approval Workflow Level Created Successfully",

                    Data =
                        entity.ApprovalWorkflowLevelId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating approval workflow level");

                throw;
            }
        }


        // =====================================================
        // UPDATE APPROVAL WORKFLOW LEVEL
        // =====================================================

        public async Task<ApiResponse<string>>
            UpdateApprovalWorkflowLevel(
                ApprovalWorkflowLevelDto dto)
        {
            try
            {
                var entity =
                    (await _unitOfWork
                        .Repository<ApprovalWorkflowLevel>()
                        .FindAsync(x =>
                            x.ApprovalWorkflowLevelId ==
                                dto.ApprovalWorkflowLevelId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException(
                        "Approval Workflow Level not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (dto.ApprovalWorkflowId <= 0)
                    throw new CustomException(
                        "Approval Workflow is required.");

                if (dto.LevelNumber <= 0)
                    throw new CustomException(
                        "Level Number must be greater than zero.");

                if (string.IsNullOrWhiteSpace(dto.ApproverType))
                    throw new CustomException(
                        "Approver Type is required.");


                // ---------------------------------------------
                // DUPLICATE LEVEL
                // ---------------------------------------------

                var duplicateLevel =
                    await _unitOfWork
                        .Repository<ApprovalWorkflowLevel>()
                        .FindAsync(x =>
                            x.ApprovalWorkflowLevelId !=
                                dto.ApprovalWorkflowLevelId &&

                            x.ApprovalWorkflowId ==
                                dto.ApprovalWorkflowId &&

                            x.LevelNumber ==
                                dto.LevelNumber &&

                            x.CompanyId ==
                                dto.CompanyId &&

                            x.RegionId ==
                                dto.RegionId &&

                            !x.IsDeleted);

                if (duplicateLevel.Any())
                    throw new CustomException(
                        "This level number already exists for the selected workflow.");


                // ---------------------------------------------
                // APPROVER VALIDATION
                // ---------------------------------------------

                if (dto.ApproverType.Equals(
                        "User",
                        StringComparison.OrdinalIgnoreCase)
                    && (!dto.ApproverUserId.HasValue ||
                        dto.ApproverUserId.Value <= 0))
                {
                    throw new CustomException(
                        "Approver User is required.");
                }

                if (dto.ApproverType.Equals(
                        "Role",
                        StringComparison.OrdinalIgnoreCase)
                    && (!dto.ApproverRoleId.HasValue ||
                        dto.ApproverRoleId.Value <= 0))
                {
                    throw new CustomException(
                        "Approver Role is required.");
                }


                string oldValues =
                    JsonConvert.SerializeObject(entity);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                entity.ApprovalWorkflowId =
                    dto.ApprovalWorkflowId;

                entity.LevelNumber =
                    dto.LevelNumber;

                entity.ApproverType =
                    dto.ApproverType.Trim();

                entity.ApproverUserId =
                    dto.ApproverUserId;

                entity.ApproverRoleId =
                    dto.ApproverRoleId;

                entity.ApprovalCondition =
                    dto.ApprovalCondition;

                entity.OnApprovalAction =
                    dto.OnApprovalAction;

                entity.OnRejectionAction =
                    dto.OnRejectionAction;

                entity.CompanyId =
                    dto.CompanyId;

                entity.RegionId =
                    dto.RegionId;

                entity.IsActive =
                    dto.IsActive;

                entity.ModifiedBy =
                    _currentUserService.UserId;

                entity.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<ApprovalWorkflowLevel>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "ApprovalWorkflowLevel",
                    "UPDATE",
                    entity.ApprovalWorkflowLevelId,
                    oldValues,
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Approval Workflow Level Updated Successfully",

                    Data =
                        entity.ApprovalWorkflowLevelId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating approval workflow level");

                throw;
            }
        }


        // =====================================================
        // DELETE APPROVAL WORKFLOW LEVEL
        // =====================================================

        public async Task<ApiResponse<string>>
            DeleteApprovalWorkflowLevel(int id)
        {
            try
            {
                var entity =
                    (await _unitOfWork
                        .Repository<ApprovalWorkflowLevel>()
                        .FindAsync(x =>
                            x.ApprovalWorkflowLevelId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException(
                        "Approval Workflow Level not found.");


                string oldValues =
                    JsonConvert.SerializeObject(entity);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                entity.IsDeleted = true;
                entity.IsActive = false;

                entity.ModifiedBy =
                    _currentUserService.UserId;

                entity.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<ApprovalWorkflowLevel>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "ApprovalWorkflowLevel",
                    "DELETE",
                    entity.ApprovalWorkflowLevelId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Approval Workflow Level Deleted Successfully",

                    Data =
                        entity.ApprovalWorkflowLevelId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting approval workflow level");

                throw;
            }
        }


        // =====================================================
        // GET ALL LEVELS
        // =====================================================

        public async Task<ApiResponse<List<ApprovalWorkflowLevelDto>>>
            GetApprovalWorkflowLevels()
        {
            var levels =
                await _unitOfWork
                    .Repository<ApprovalWorkflowLevel>()
                    .GetAllAsync();

            var workflows =
                await _unitOfWork
                    .Repository<ApprovalWorkflow>()
                    .GetAllAsync();


            var result =
                (from l in levels

                 join w in workflows
                     on l.ApprovalWorkflowId
                     equals w.ApprovalWorkflowId
                     into workflowGroup

                 from w in workflowGroup
                     .DefaultIfEmpty()

                 where !l.IsDeleted

                 select new ApprovalWorkflowLevelDto
                 {
                     ApprovalWorkflowLevelId =
                         l.ApprovalWorkflowLevelId,

                     ApprovalWorkflowId =
                         l.ApprovalWorkflowId,

                     WorkflowName =
                         w != null
                             ? w.WorkflowName
                             : null,

                     LevelNumber =
                         l.LevelNumber,

                     ApproverType =
                         l.ApproverType,

                     ApproverUserId =
                         l.ApproverUserId,

                     ApproverRoleId =
                         l.ApproverRoleId,

                     ApprovalCondition =
                         l.ApprovalCondition,

                     OnApprovalAction =
                         l.OnApprovalAction,

                     OnRejectionAction =
                         l.OnRejectionAction,

                     CompanyId =
                         l.CompanyId,

                     RegionId =
                         l.RegionId,

                     IsActive =
                         l.IsActive,

                     CreatedAt =
                         l.CreatedAt,

                     ModifiedAt =
                         l.ModifiedAt
                 })
                .OrderBy(x => x.ApprovalWorkflowId)
                .ThenBy(x => x.LevelNumber)
                .ToList();


            return new ApiResponse<List<ApprovalWorkflowLevelDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        // =====================================================
        // GET LEVEL BY ID
        // =====================================================

        public async Task<ApiResponse<ApprovalWorkflowLevelDto>>
            GetApprovalWorkflowLevelById(int id)
        {
            var entity =
                (await _unitOfWork
                    .Repository<ApprovalWorkflowLevel>()
                    .FindAsync(x =>
                        x.ApprovalWorkflowLevelId == id &&
                        !x.IsDeleted))
                .FirstOrDefault();

            if (entity == null)
                throw new CustomException(
                    "Approval Workflow Level not found.");


            var workflow =
                (await _unitOfWork
                    .Repository<ApprovalWorkflow>()
                    .FindAsync(x =>
                        x.ApprovalWorkflowId ==
                            entity.ApprovalWorkflowId))
                .FirstOrDefault();


            return new ApiResponse<ApprovalWorkflowLevelDto>
            {
                Success = true,
                Message = "Success",

                Data = new ApprovalWorkflowLevelDto
                {
                    ApprovalWorkflowLevelId =
                        entity.ApprovalWorkflowLevelId,

                    ApprovalWorkflowId =
                        entity.ApprovalWorkflowId,

                    WorkflowName =
                        workflow?.WorkflowName,

                    LevelNumber =
                        entity.LevelNumber,

                    ApproverType =
                        entity.ApproverType,

                    ApproverUserId =
                        entity.ApproverUserId,

                    ApproverRoleId =
                        entity.ApproverRoleId,

                    ApprovalCondition =
                        entity.ApprovalCondition,

                    OnApprovalAction =
                        entity.OnApprovalAction,

                    OnRejectionAction =
                        entity.OnRejectionAction,

                    CompanyId =
                        entity.CompanyId,

                    RegionId =
                        entity.RegionId,

                    IsActive =
                        entity.IsActive,

                    CreatedAt =
                        entity.CreatedAt,

                    ModifiedAt =
                        entity.ModifiedAt
                }
            };
        }
        #endregion

        #region AUTO ASSIGNMENT RULE CRUD

        // =====================================================
        // CREATE AUTO ASSIGNMENT RULE
        // =====================================================

        public async Task<ApiResponse<string>>
            CreateAutoAssignmentRule(
                AutoAssignmentRuleDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.RuleName))
                    throw new CustomException(
                        "Rule Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (string.IsNullOrWhiteSpace(dto.AssignmentMethod))
                    throw new CustomException(
                        "Assignment Method is required.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // VALIDATE ASSIGNMENT TARGET
                // ---------------------------------------------

                if (dto.TeamId == null && dto.UserId == null)
                {
                    throw new CustomException(
                        "Either Team or User must be selected.");
                }


                // ---------------------------------------------
                // DUPLICATE RULE
                // ---------------------------------------------

                var duplicateRule =
                    await _unitOfWork
                        .Repository<AutoAssignmentRule>()
                        .FindAsync(x =>
                            x.RuleName.ToLower() ==
                                dto.RuleName.Trim().ToLower() &&
                            x.ModuleName.ToLower() ==
                                dto.ModuleName.Trim().ToLower() &&
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId &&
                            !x.IsDeleted);

                if (duplicateRule.Any())
                    throw new CustomException(
                        "An auto assignment rule with this name already exists for the selected module.");


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                AutoAssignmentRule rule =
                    new AutoAssignmentRule
                    {
                        RuleName =
                            dto.RuleName.Trim(),

                        ModuleName =
                            dto.ModuleName.Trim(),

                        Description =
                            dto.Description,

                        AssignmentMethod =
                            dto.AssignmentMethod.Trim(),

                        TeamId =
                            dto.TeamId,

                        UserId =
                            dto.UserId,

                        ExecutionOrder =
                            dto.ExecutionOrder,

                        CompanyId =
                            dto.CompanyId,

                        RegionId =
                            dto.RegionId,

                        IsActive = true,

                        IsDeleted = false,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedAt =
                            DateTime.Now
                    };


                await _unitOfWork
                    .Repository<AutoAssignmentRule>()
                    .AddAsync(rule);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "AutoAssignmentRule",
                    "INSERT",
                    rule.AutoAssignmentRuleId,
                    "",
                    JsonConvert.SerializeObject(rule),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Auto Assignment Rule Created Successfully",

                    Data =
                        rule.AutoAssignmentRuleId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating auto assignment rule");

                throw;
            }
        }


        // =====================================================
        // UPDATE AUTO ASSIGNMENT RULE
        // =====================================================

        public async Task<ApiResponse<string>>
            UpdateAutoAssignmentRule(
                AutoAssignmentRuleDto dto)
        {
            try
            {
                var rule =
                    (await _unitOfWork
                        .Repository<AutoAssignmentRule>()
                        .FindAsync(x =>
                            x.AutoAssignmentRuleId ==
                                dto.AutoAssignmentRuleId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (rule == null)
                    throw new CustomException(
                        "Auto Assignment Rule not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.RuleName))
                    throw new CustomException(
                        "Rule Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (string.IsNullOrWhiteSpace(dto.AssignmentMethod))
                    throw new CustomException(
                        "Assignment Method is required.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // VALIDATE ASSIGNMENT TARGET
                // ---------------------------------------------

                if (dto.TeamId == null && dto.UserId == null)
                {
                    throw new CustomException(
                        "Either Team or User must be selected.");
                }


                // ---------------------------------------------
                // DUPLICATE RULE
                // ---------------------------------------------

                var duplicateRule =
                    await _unitOfWork
                        .Repository<AutoAssignmentRule>()
                        .FindAsync(x =>
                            x.AutoAssignmentRuleId !=
                                dto.AutoAssignmentRuleId &&

                            x.RuleName.ToLower() ==
                                dto.RuleName.Trim().ToLower() &&

                            x.ModuleName.ToLower() ==
                                dto.ModuleName.Trim().ToLower() &&

                            x.CompanyId == dto.CompanyId &&

                            x.RegionId == dto.RegionId &&

                            !x.IsDeleted);

                if (duplicateRule.Any())
                    throw new CustomException(
                        "An auto assignment rule with this name already exists for the selected module.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(rule);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                rule.RuleName =
                    dto.RuleName.Trim();

                rule.ModuleName =
                    dto.ModuleName.Trim();

                rule.Description =
                    dto.Description;

                rule.AssignmentMethod =
                    dto.AssignmentMethod.Trim();

                rule.TeamId =
                    dto.TeamId;

                rule.UserId =
                    dto.UserId;

                rule.ExecutionOrder =
                    dto.ExecutionOrder;

                rule.CompanyId =
                    dto.CompanyId;

                rule.RegionId =
                    dto.RegionId;

                rule.IsActive =
                    dto.IsActive;

                rule.ModifiedBy =
                    _currentUserService.UserId;

                rule.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<AutoAssignmentRule>()
                    .Update(rule);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "AutoAssignmentRule",
                    "UPDATE",
                    rule.AutoAssignmentRuleId,
                    oldValues,
                    JsonConvert.SerializeObject(rule),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Auto Assignment Rule Updated Successfully",

                    Data =
                        rule.AutoAssignmentRuleId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating auto assignment rule");

                throw;
            }
        }


        // =====================================================
        // DELETE AUTO ASSIGNMENT RULE
        // =====================================================

        public async Task<ApiResponse<string>>
            DeleteAutoAssignmentRule(
                int id)
        {
            try
            {
                var rule =
                    (await _unitOfWork
                        .Repository<AutoAssignmentRule>()
                        .FindAsync(x =>
                            x.AutoAssignmentRuleId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (rule == null)
                    throw new CustomException(
                        "Auto Assignment Rule not found.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(rule);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                rule.IsDeleted = true;

                rule.IsActive = false;

                rule.ModifiedBy =
                    _currentUserService.UserId;

                rule.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<AutoAssignmentRule>()
                    .Update(rule);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "AutoAssignmentRule",
                    "DELETE",
                    rule.AutoAssignmentRuleId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Auto Assignment Rule Deleted Successfully",

                    Data =
                        rule.AutoAssignmentRuleId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting auto assignment rule");

                throw;
            }
        }


        // =====================================================
        // GET ALL AUTO ASSIGNMENT RULES
        // =====================================================

        public async Task<ApiResponse<List<AutoAssignmentRuleDto>>>
            GetAutoAssignmentRules()
        {
            var rules =
                await _unitOfWork
                    .Repository<AutoAssignmentRule>()
                    .GetAllAsync();


            var result =
                rules
                    .Where(x => !x.IsDeleted)
                    .OrderByDescending(x =>
                        x.AutoAssignmentRuleId)
                    .Select(x =>
                        new AutoAssignmentRuleDto
                        {
                            AutoAssignmentRuleId =
                                x.AutoAssignmentRuleId,

                            RuleName =
                                x.RuleName,

                            ModuleName =
                                x.ModuleName,

                            Description =
                                x.Description,

                            AssignmentMethod =
                                x.AssignmentMethod,

                            TeamId =
                                x.TeamId,

                            UserId =
                                x.UserId,

                            ExecutionOrder =
                                x.ExecutionOrder,

                            CompanyId =
                                x.CompanyId,

                            RegionId =
                                x.RegionId,

                            IsActive =
                                x.IsActive,

                            CreatedAt =
                                x.CreatedAt,

                            ModifiedAt =
                                x.ModifiedAt
                        })
                    .ToList();


            return new ApiResponse<List<AutoAssignmentRuleDto>>
            {
                Success = true,

                Message = "Success",

                Data = result
            };
        }


        // =====================================================
        // GET AUTO ASSIGNMENT RULE BY ID
        // =====================================================

        public async Task<ApiResponse<AutoAssignmentRuleDto>>
            GetAutoAssignmentRuleById(
                int id)
        {
            var rule =
                (await _unitOfWork
                    .Repository<AutoAssignmentRule>()
                    .FindAsync(x =>
                        x.AutoAssignmentRuleId == id &&
                        !x.IsDeleted))
                .FirstOrDefault();

            if (rule == null)
                throw new CustomException(
                    "Auto Assignment Rule not found.");


            return new ApiResponse<AutoAssignmentRuleDto>
            {
                Success = true,

                Message = "Success",

                Data =
                    new AutoAssignmentRuleDto
                    {
                        AutoAssignmentRuleId =
                            rule.AutoAssignmentRuleId,

                        RuleName =
                            rule.RuleName,

                        ModuleName =
                            rule.ModuleName,

                        Description =
                            rule.Description,

                        AssignmentMethod =
                            rule.AssignmentMethod,

                        TeamId =
                            rule.TeamId,

                        UserId =
                            rule.UserId,

                        ExecutionOrder =
                            rule.ExecutionOrder,

                        CompanyId =
                            rule.CompanyId,

                        RegionId =
                            rule.RegionId,

                        IsActive =
                            rule.IsActive,

                        CreatedAt =
                            rule.CreatedAt,

                        ModifiedAt =
                            rule.ModifiedAt
                    }
            };
        }

        #endregion

        #region AUTO ASSIGNMENT CONDITION CRUD
        // =====================================================
        // CREATE AUTO ASSIGNMENT CONDITION
        // =====================================================

        public async Task<ApiResponse<string>>
            CreateAutoAssignmentCondition(
                AutoAssignmentConditionDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (dto.AutoAssignmentRuleId <= 0)
                    throw new CustomException(
                        "Auto Assignment Rule is required.");

                if (string.IsNullOrWhiteSpace(dto.FieldName))
                    throw new CustomException(
                        "Field Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Operator))
                    throw new CustomException(
                        "Operator is required.");

                if (dto.ConditionOrder <= 0)
                    throw new CustomException(
                        "Condition Order must be greater than zero.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // CHECK AUTO ASSIGNMENT RULE
                // ---------------------------------------------

                var rule =
                    (await _unitOfWork
                        .Repository<AutoAssignmentRule>()
                        .FindAsync(x =>
                            x.AutoAssignmentRuleId ==
                                dto.AutoAssignmentRuleId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (rule == null)
                    throw new CustomException(
                        "Auto Assignment Rule not found.");


                // ---------------------------------------------
                // DUPLICATE CONDITION
                // ---------------------------------------------

                var duplicateCondition =
                    await _unitOfWork
                        .Repository<AutoAssignmentCondition>()
                        .FindAsync(x =>
                            x.AutoAssignmentRuleId ==
                                dto.AutoAssignmentRuleId &&
                            x.FieldName.ToLower() ==
                                dto.FieldName.Trim().ToLower() &&
                            x.Operator.ToLower() ==
                                dto.Operator.Trim().ToLower() &&
                            x.ConditionOrder ==
                                dto.ConditionOrder &&
                            !x.IsDeleted);

                if (duplicateCondition.Any())
                    throw new CustomException(
                        "An auto assignment condition with the same field, operator and order already exists.");


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                AutoAssignmentCondition condition =
                    new AutoAssignmentCondition
                    {
                        AutoAssignmentRuleId =
                            dto.AutoAssignmentRuleId,

                        FieldName =
                            dto.FieldName.Trim(),

                        Operator =
                            dto.Operator.Trim(),

                        FieldValue =
                            dto.FieldValue,

                        LogicalOperator =
                            dto.LogicalOperator,

                        ConditionOrder =
                            dto.ConditionOrder,

                        CompanyId =
                            dto.CompanyId,

                        RegionId =
                            dto.RegionId,

                        IsActive = true,

                        IsDeleted = false,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedAt =
                            DateTime.Now
                    };


                await _unitOfWork
                    .Repository<AutoAssignmentCondition>()
                    .AddAsync(condition);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "AutoAssignmentCondition",
                    "INSERT",
                    condition.AutoAssignmentConditionId,
                    "",
                    JsonConvert.SerializeObject(condition),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Auto Assignment Condition Created Successfully",

                    Data =
                        condition.AutoAssignmentConditionId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating auto assignment condition");

                throw;
            }
        }

        // =====================================================
        // UPDATE AUTO ASSIGNMENT CONDITION
        // =====================================================

        public async Task<ApiResponse<string>>
            UpdateAutoAssignmentCondition(
                AutoAssignmentConditionDto dto)
        {
            try
            {
                // ---------------------------------------------
                // GET EXISTING CONDITION
                // ---------------------------------------------

                var condition =
                    (await _unitOfWork
                        .Repository<AutoAssignmentCondition>()
                        .FindAsync(x =>
                            x.AutoAssignmentConditionId ==
                                dto.AutoAssignmentConditionId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (condition == null)
                    throw new CustomException(
                        "Auto Assignment Condition not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (dto.AutoAssignmentRuleId <= 0)
                    throw new CustomException(
                        "Auto Assignment Rule is required.");

                if (string.IsNullOrWhiteSpace(dto.FieldName))
                    throw new CustomException(
                        "Field Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Operator))
                    throw new CustomException(
                        "Operator is required.");

                if (dto.ConditionOrder <= 0)
                    throw new CustomException(
                        "Condition Order must be greater than zero.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // CHECK AUTO ASSIGNMENT RULE
                // ---------------------------------------------

                var rule =
                    (await _unitOfWork
                        .Repository<AutoAssignmentRule>()
                        .FindAsync(x =>
                            x.AutoAssignmentRuleId ==
                                dto.AutoAssignmentRuleId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (rule == null)
                    throw new CustomException(
                        "Auto Assignment Rule not found.");


                // ---------------------------------------------
                // DUPLICATE CONDITION
                // ---------------------------------------------

                var duplicateCondition =
                    await _unitOfWork
                        .Repository<AutoAssignmentCondition>()
                        .FindAsync(x =>
                            x.AutoAssignmentConditionId !=
                                dto.AutoAssignmentConditionId &&

                            x.AutoAssignmentRuleId ==
                                dto.AutoAssignmentRuleId &&

                            x.FieldName.ToLower() ==
                                dto.FieldName.Trim().ToLower() &&

                            x.Operator.ToLower() ==
                                dto.Operator.Trim().ToLower() &&

                            x.ConditionOrder ==
                                dto.ConditionOrder &&

                            !x.IsDeleted);

                if (duplicateCondition.Any())
                    throw new CustomException(
                        "An auto assignment condition with the same field, operator and order already exists.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(condition);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                condition.AutoAssignmentRuleId =
                    dto.AutoAssignmentRuleId;

                condition.FieldName =
                    dto.FieldName.Trim();

                condition.Operator =
                    dto.Operator.Trim();

                condition.FieldValue =
                    dto.FieldValue;

                condition.LogicalOperator =
                    dto.LogicalOperator;

                condition.ConditionOrder =
                    dto.ConditionOrder;

                condition.CompanyId =
                    dto.CompanyId;

                condition.RegionId =
                    dto.RegionId;

                condition.IsActive =
                    dto.IsActive;

                condition.ModifiedBy =
                    _currentUserService.UserId;

                condition.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<AutoAssignmentCondition>()
                    .Update(condition);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "AutoAssignmentCondition",
                    "UPDATE",
                    condition.AutoAssignmentConditionId,
                    oldValues,
                    JsonConvert.SerializeObject(condition),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Auto Assignment Condition Updated Successfully",

                    Data =
                        condition.AutoAssignmentConditionId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating auto assignment condition");

                throw;
            }
        }

        // =====================================================
        // DELETE AUTO ASSIGNMENT CONDITION
        // =====================================================

        public async Task<ApiResponse<string>>
            DeleteAutoAssignmentCondition(
                int id)
        {
            try
            {
                // ---------------------------------------------
                // GET EXISTING CONDITION
                // ---------------------------------------------

                var condition =
                    (await _unitOfWork
                        .Repository<AutoAssignmentCondition>()
                        .FindAsync(x =>
                            x.AutoAssignmentConditionId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (condition == null)
                    throw new CustomException(
                        "Auto Assignment Condition not found.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(condition);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                condition.IsDeleted = true;

                condition.IsActive = false;

                condition.ModifiedBy =
                    _currentUserService.UserId;

                condition.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<AutoAssignmentCondition>()
                    .Update(condition);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "AutoAssignmentCondition",
                    "DELETE",
                    condition.AutoAssignmentConditionId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Auto Assignment Condition Deleted Successfully",

                    Data =
                        condition.AutoAssignmentConditionId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting auto assignment condition");

                throw;
            }
        }


        // =====================================================
        // GET ALL AUTO ASSIGNMENT CONDITIONS
        // =====================================================

        public async Task<ApiResponse<List<AutoAssignmentConditionDto>>>
            GetAutoAssignmentConditions()
        {
            try
            {
                var conditions =
                    await _unitOfWork
                        .Repository<AutoAssignmentCondition>()
                        .GetAllAsync();


                var result =
                    conditions
                        .Where(x => !x.IsDeleted)
                        .OrderBy(x =>
                            x.AutoAssignmentRuleId)
                        .ThenBy(x =>
                            x.ConditionOrder)
                        .Select(x =>
                            new AutoAssignmentConditionDto
                            {
                                AutoAssignmentConditionId =
                                    x.AutoAssignmentConditionId,

                                AutoAssignmentRuleId =
                                    x.AutoAssignmentRuleId,

                                FieldName =
                                    x.FieldName,

                                Operator =
                                    x.Operator,

                                FieldValue =
                                    x.FieldValue,

                                LogicalOperator =
                                    x.LogicalOperator,

                                ConditionOrder =
                                    x.ConditionOrder,

                                CompanyId =
                                    x.CompanyId,

                                RegionId =
                                    x.RegionId,

                                IsActive =
                                    x.IsActive,

                                CreatedAt =
                                    x.CreatedAt,

                                ModifiedAt =
                                    x.ModifiedAt
                            })
                        .ToList();


                return new ApiResponse<List<AutoAssignmentConditionDto>>
                {
                    Success = true,

                    Message = "Success",

                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting auto assignment conditions");

                throw;
            }
        }


        // =====================================================
        // GET AUTO ASSIGNMENT CONDITION BY ID
        // =====================================================

        public async Task<ApiResponse<AutoAssignmentConditionDto>>
            GetAutoAssignmentConditionById(
                int id)
        {
            try
            {
                var condition =
                    (await _unitOfWork
                        .Repository<AutoAssignmentCondition>()
                        .FindAsync(x =>
                            x.AutoAssignmentConditionId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (condition == null)
                    throw new CustomException(
                        "Auto Assignment Condition not found.");


                return new ApiResponse<AutoAssignmentConditionDto>
                {
                    Success = true,

                    Message = "Success",

                    Data =
                        new AutoAssignmentConditionDto
                        {
                            AutoAssignmentConditionId =
                                condition.AutoAssignmentConditionId,

                            AutoAssignmentRuleId =
                                condition.AutoAssignmentRuleId,

                            FieldName =
                                condition.FieldName,

                            Operator =
                                condition.Operator,

                            FieldValue =
                                condition.FieldValue,

                            LogicalOperator =
                                condition.LogicalOperator,

                            ConditionOrder =
                                condition.ConditionOrder,

                            CompanyId =
                                condition.CompanyId,

                            RegionId =
                                condition.RegionId,

                            IsActive =
                                condition.IsActive,

                            CreatedAt =
                                condition.CreatedAt,

                            ModifiedAt =
                                condition.ModifiedAt
                        }
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting auto assignment condition by id");

                throw;
            }
        }

        #endregion

        #region ESCALATION RULE CRUD

        // =====================================================
        // CREATE ESCALATION RULE
        // =====================================================

        public async Task<ApiResponse<string>>
            CreateEscalationRule(
                EscalationRuleDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.RuleName))
                    throw new CustomException(
                        "Rule Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (dto.EscalateAfterMinutes <= 0)
                    throw new CustomException(
                        "Escalate After Minutes must be greater than zero.");

                if (dto.EscalationLevel <= 0)
                    throw new CustomException(
                        "Escalation Level must be greater than zero.");

                if (string.IsNullOrWhiteSpace(dto.EscalateToType))
                    throw new CustomException(
                        "Escalate To Type is required.");

                if (string.IsNullOrWhiteSpace(dto.NotificationMethod))
                    throw new CustomException(
                        "Notification Method is required.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // DUPLICATE ESCALATION RULE
                // ---------------------------------------------

                var duplicateRule =
                    await _unitOfWork
                        .Repository<EscalationRule>()
                        .FindAsync(x =>
                            x.RuleName.ToLower() ==
                                dto.RuleName.Trim().ToLower() &&

                            x.ModuleName.ToLower() ==
                                dto.ModuleName.Trim().ToLower() &&

                            x.CompanyId ==
                                dto.CompanyId &&

                            x.RegionId ==
                                dto.RegionId &&

                            !x.IsDeleted);

                if (duplicateRule.Any())
                    throw new CustomException(
                        "An escalation rule with this name already exists for the selected module.");


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                EscalationRule rule =
                    new EscalationRule
                    {
                        RuleName =
                            dto.RuleName.Trim(),

                        ModuleName =
                            dto.ModuleName.Trim(),

                        Description =
                            dto.Description,

                        EscalateAfterMinutes =
                            dto.EscalateAfterMinutes,

                        EscalationLevel =
                            dto.EscalationLevel,

                        EscalateToType =
                            dto.EscalateToType.Trim(),

                        EscalateToUserId =
                            dto.EscalateToUserId,

                        NotificationMethod =
                            dto.NotificationMethod.Trim(),

                        RepeatEscalation =
                            dto.RepeatEscalation,

                        MaximumEscalationLevel =
                            dto.MaximumEscalationLevel,

                        CompanyId =
                            dto.CompanyId,

                        RegionId =
                            dto.RegionId,

                        IsActive = true,

                        IsDeleted = false,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedAt =
                            DateTime.Now
                    };


                await _unitOfWork
                    .Repository<EscalationRule>()
                    .AddAsync(rule);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "EscalationRule",
                    "INSERT",
                    rule.EscalationRuleId,
                    "",
                    JsonConvert.SerializeObject(rule),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Escalation Rule Created Successfully",

                    Data =
                        rule.EscalationRuleId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating escalation rule");

                throw;
            }
        }

        // =====================================================
        // UPDATE ESCALATION RULE
        // =====================================================

        public async Task<ApiResponse<string>>
            UpdateEscalationRule(
                EscalationRuleDto dto)
        {
            try
            {
                // ---------------------------------------------
                // GET EXISTING RULE
                // ---------------------------------------------

                var rule =
                    (await _unitOfWork
                        .Repository<EscalationRule>()
                        .FindAsync(x =>
                            x.EscalationRuleId ==
                                dto.EscalationRuleId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (rule == null)
                    throw new CustomException(
                        "Escalation Rule not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.RuleName))
                    throw new CustomException(
                        "Rule Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (dto.EscalateAfterMinutes <= 0)
                    throw new CustomException(
                        "Escalate After Minutes must be greater than zero.");

                if (dto.EscalationLevel <= 0)
                    throw new CustomException(
                        "Escalation Level must be greater than zero.");

                if (string.IsNullOrWhiteSpace(dto.EscalateToType))
                    throw new CustomException(
                        "Escalate To Type is required.");

                if (string.IsNullOrWhiteSpace(dto.NotificationMethod))
                    throw new CustomException(
                        "Notification Method is required.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // DUPLICATE ESCALATION RULE
                // ---------------------------------------------

                var duplicateRule =
                    await _unitOfWork
                        .Repository<EscalationRule>()
                        .FindAsync(x =>
                            x.EscalationRuleId !=
                                dto.EscalationRuleId &&

                            x.RuleName.ToLower() ==
                                dto.RuleName.Trim().ToLower() &&

                            x.ModuleName.ToLower() ==
                                dto.ModuleName.Trim().ToLower() &&

                            x.CompanyId ==
                                dto.CompanyId &&

                            x.RegionId ==
                                dto.RegionId &&

                            !x.IsDeleted);

                if (duplicateRule.Any())
                    throw new CustomException(
                        "An escalation rule with this name already exists for the selected module.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(rule);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                rule.RuleName =
                    dto.RuleName.Trim();

                rule.ModuleName =
                    dto.ModuleName.Trim();

                rule.Description =
                    dto.Description;

                rule.EscalateAfterMinutes =
                    dto.EscalateAfterMinutes;

                rule.EscalationLevel =
                    dto.EscalationLevel;

                rule.EscalateToType =
                    dto.EscalateToType.Trim();

                rule.EscalateToUserId =
                    dto.EscalateToUserId;

                rule.NotificationMethod =
                    dto.NotificationMethod.Trim();

                rule.RepeatEscalation =
                    dto.RepeatEscalation;

                rule.MaximumEscalationLevel =
                    dto.MaximumEscalationLevel;

                rule.CompanyId =
                    dto.CompanyId;

                rule.RegionId =
                    dto.RegionId;

                rule.IsActive =
                    dto.IsActive;

                rule.ModifiedBy =
                    _currentUserService.UserId;

                rule.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<EscalationRule>()
                    .Update(rule);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "EscalationRule",
                    "UPDATE",
                    rule.EscalationRuleId,
                    oldValues,
                    JsonConvert.SerializeObject(rule),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Escalation Rule Updated Successfully",

                    Data =
                        rule.EscalationRuleId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating escalation rule");

                throw;
            }
        }

        // =====================================================
        // DELETE ESCALATION RULE
        // =====================================================

        public async Task<ApiResponse<string>>
            DeleteEscalationRule(
                int id)
        {
            try
            {
                // ---------------------------------------------
                // GET EXISTING RULE
                // ---------------------------------------------

                var rule =
                    (await _unitOfWork
                        .Repository<EscalationRule>()
                        .FindAsync(x =>
                            x.EscalationRuleId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (rule == null)
                    throw new CustomException(
                        "Escalation Rule not found.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(rule);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                rule.IsDeleted = true;

                rule.IsActive = false;

                rule.ModifiedBy =
                    _currentUserService.UserId;

                rule.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<EscalationRule>()
                    .Update(rule);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "EscalationRule",
                    "DELETE",
                    rule.EscalationRuleId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Escalation Rule Deleted Successfully",

                    Data =
                        rule.EscalationRuleId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting escalation rule");

                throw;
            }
        }

        // =====================================================
        // GET ALL ESCALATION RULES
        // =====================================================

        public async Task<ApiResponse<List<EscalationRuleDto>>>
            GetEscalationRules()
        {
            try
            {
                var rules =
                    await _unitOfWork
                        .Repository<EscalationRule>()
                        .GetAllAsync();


                var result =
                    rules
                        .Where(x => !x.IsDeleted)
                        .OrderByDescending(x =>
                            x.EscalationRuleId)
                        .Select(x =>
                            new EscalationRuleDto
                            {
                                EscalationRuleId =
                                    x.EscalationRuleId,

                                RuleName =
                                    x.RuleName,

                                ModuleName =
                                    x.ModuleName,

                                Description =
                                    x.Description,

                                EscalateAfterMinutes =
                                    x.EscalateAfterMinutes,

                                EscalationLevel =
                                    x.EscalationLevel,

                                EscalateToType =
                                    x.EscalateToType,

                                EscalateToUserId =
                                    x.EscalateToUserId,

                                NotificationMethod =
                                    x.NotificationMethod,

                                RepeatEscalation =
                                    x.RepeatEscalation,

                                MaximumEscalationLevel =
                                    x.MaximumEscalationLevel,

                                CompanyId =
                                    x.CompanyId,

                                RegionId =
                                    x.RegionId,

                                IsActive =
                                    x.IsActive,

                                CreatedAt =
                                    x.CreatedAt,

                                ModifiedAt =
                                    x.ModifiedAt
                            })
                        .ToList();


                return new ApiResponse<List<EscalationRuleDto>>
                {
                    Success = true,

                    Message = "Success",

                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting escalation rules");

                throw;
            }
        }

        // =====================================================
        // GET ESCALATION RULE BY ID
        // =====================================================

        public async Task<ApiResponse<EscalationRuleDto>>
            GetEscalationRuleById(
                int id)
        {
            try
            {
                var rule =
                    (await _unitOfWork
                        .Repository<EscalationRule>()
                        .FindAsync(x =>
                            x.EscalationRuleId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (rule == null)
                    throw new CustomException(
                        "Escalation Rule not found.");


                return new ApiResponse<EscalationRuleDto>
                {
                    Success = true,

                    Message = "Success",

                    Data =
                        new EscalationRuleDto
                        {
                            EscalationRuleId =
                                rule.EscalationRuleId,

                            RuleName =
                                rule.RuleName,

                            ModuleName =
                                rule.ModuleName,

                            Description =
                                rule.Description,

                            EscalateAfterMinutes =
                                rule.EscalateAfterMinutes,

                            EscalationLevel =
                                rule.EscalationLevel,

                            EscalateToType =
                                rule.EscalateToType,

                            EscalateToUserId =
                                rule.EscalateToUserId,

                            NotificationMethod =
                                rule.NotificationMethod,

                            RepeatEscalation =
                                rule.RepeatEscalation,

                            MaximumEscalationLevel =
                                rule.MaximumEscalationLevel,

                            CompanyId =
                                rule.CompanyId,

                            RegionId =
                                rule.RegionId,

                            IsActive =
                                rule.IsActive,

                            CreatedAt =
                                rule.CreatedAt,

                            ModifiedAt =
                                rule.ModifiedAt
                        }
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting escalation rule by id");

                throw;
            }
        }
        #endregion

        #region SLA RULE CRUD

        // =====================================================
        // CREATE SLA RULE
        // =====================================================

        public async Task<ApiResponse<string>>
            CreateSlarule(
                SlaruleDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.Slaname))
                    throw new CustomException(
                        "SLA Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Priority))
                    throw new CustomException(
                        "Priority is required.");

                if (dto.FirstResponseMinutes <= 0)
                    throw new CustomException(
                        "First Response Minutes must be greater than zero.");

                if (dto.ResolutionMinutes <= 0)
                    throw new CustomException(
                        "Resolution Minutes must be greater than zero.");

                if (dto.WarningMinutes.HasValue &&
                    dto.WarningMinutes.Value <= 0)
                    throw new CustomException(
                        "Warning Minutes must be greater than zero.");

                if (dto.EscalationEnabled &&
                    !dto.EscalationRuleId.HasValue)
                    throw new CustomException(
                        "Escalation Rule is required when escalation is enabled.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // DUPLICATE SLA RULE
                // ---------------------------------------------

                var duplicateRule =
                    await _unitOfWork
                        .Repository<Slarule>()
                        .FindAsync(x =>
                            x.Slaname.ToLower() ==
                                dto.Slaname.Trim().ToLower() &&

                            x.ModuleName.ToLower() ==
                                dto.ModuleName.Trim().ToLower() &&

                            x.CompanyId ==
                                dto.CompanyId &&

                            x.RegionId ==
                                dto.RegionId &&

                            !x.IsDeleted);

                if (duplicateRule.Any())
                    throw new CustomException(
                        "An SLA Rule with this name already exists for the selected module.");


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                Slarule rule =
                    new Slarule
                    {
                        Slaname =
                            dto.Slaname.Trim(),

                        ModuleName =
                            dto.ModuleName.Trim(),

                        Description =
                            dto.Description,

                        Priority =
                            dto.Priority.Trim(),

                        FirstResponseMinutes =
                            dto.FirstResponseMinutes,

                        ResolutionMinutes =
                            dto.ResolutionMinutes,

                        WarningMinutes =
                            dto.WarningMinutes,

                        BusinessHoursId =
                            dto.BusinessHoursId,

                        HolidayCalendarId =
                            dto.HolidayCalendarId,

                        EscalationEnabled =
                            dto.EscalationEnabled,

                        EscalationRuleId =
                            dto.EscalationRuleId,

                        CompanyId =
                            dto.CompanyId,

                        RegionId =
                            dto.RegionId,

                        IsActive = true,

                        IsDeleted = false,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedAt =
                            DateTime.Now
                    };


                await _unitOfWork
                    .Repository<Slarule>()
                    .AddAsync(rule);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "Slarule",
                    "INSERT",
                    rule.SlaruleId,
                    "",
                    JsonConvert.SerializeObject(rule),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "SLA Rule Created Successfully",

                    Data =
                        rule.SlaruleId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating SLA rule");

                throw;
            }
        }
        // =====================================================
        // UPDATE SLA RULE
        // =====================================================

        public async Task<ApiResponse<string>>
            UpdateSlarule(
                SlaruleDto dto)
        {
            try
            {
                // ---------------------------------------------
                // GET EXISTING SLA RULE
                // ---------------------------------------------

                var rule =
                    (await _unitOfWork
                        .Repository<Slarule>()
                        .FindAsync(x =>
                            x.SlaruleId == dto.SlaruleId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (rule == null)
                    throw new CustomException(
                        "SLA Rule not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.Slaname))
                    throw new CustomException(
                        "SLA Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Priority))
                    throw new CustomException(
                        "Priority is required.");

                if (dto.FirstResponseMinutes <= 0)
                    throw new CustomException(
                        "First Response Minutes must be greater than zero.");

                if (dto.ResolutionMinutes <= 0)
                    throw new CustomException(
                        "Resolution Minutes must be greater than zero.");

                if (dto.WarningMinutes.HasValue &&
                    dto.WarningMinutes.Value <= 0)
                    throw new CustomException(
                        "Warning Minutes must be greater than zero.");

                if (dto.EscalationEnabled &&
                    !dto.EscalationRuleId.HasValue)
                    throw new CustomException(
                        "Escalation Rule is required when escalation is enabled.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // DUPLICATE SLA RULE
                // ---------------------------------------------

                var duplicateRule =
                    await _unitOfWork
                        .Repository<Slarule>()
                        .FindAsync(x =>
                            x.SlaruleId != dto.SlaruleId &&

                            x.Slaname.ToLower() ==
                                dto.Slaname.Trim().ToLower() &&

                            x.ModuleName.ToLower() ==
                                dto.ModuleName.Trim().ToLower() &&

                            x.CompanyId ==
                                dto.CompanyId &&

                            x.RegionId ==
                                dto.RegionId &&

                            !x.IsDeleted);

                if (duplicateRule.Any())
                    throw new CustomException(
                        "An SLA Rule with this name already exists for the selected module.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(rule);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                rule.Slaname =
                    dto.Slaname.Trim();

                rule.ModuleName =
                    dto.ModuleName.Trim();

                rule.Description =
                    dto.Description;

                rule.Priority =
                    dto.Priority.Trim();

                rule.FirstResponseMinutes =
                    dto.FirstResponseMinutes;

                rule.ResolutionMinutes =
                    dto.ResolutionMinutes;

                rule.WarningMinutes =
                    dto.WarningMinutes;

                rule.BusinessHoursId =
                    dto.BusinessHoursId;

                rule.HolidayCalendarId =
                    dto.HolidayCalendarId;

                rule.EscalationEnabled =
                    dto.EscalationEnabled;

                rule.EscalationRuleId =
                    dto.EscalationRuleId;

                rule.CompanyId =
                    dto.CompanyId;

                rule.RegionId =
                    dto.RegionId;

                rule.IsActive =
                    dto.IsActive;

                rule.ModifiedBy =
                    _currentUserService.UserId;

                rule.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<Slarule>()
                    .Update(rule);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "Slarule",
                    "UPDATE",
                    rule.SlaruleId,
                    oldValues,
                    JsonConvert.SerializeObject(rule),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "SLA Rule Updated Successfully",

                    Data =
                        rule.SlaruleId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating SLA rule");

                throw;
            }
        }

        // =====================================================
        // DELETE SLA RULE
        // =====================================================

        public async Task<ApiResponse<string>>
            DeleteSlarule(
                int id)
        {
            try
            {
                // ---------------------------------------------
                // GET EXISTING SLA RULE
                // ---------------------------------------------

                var rule =
                    (await _unitOfWork
                        .Repository<Slarule>()
                        .FindAsync(x =>
                            x.SlaruleId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (rule == null)
                    throw new CustomException(
                        "SLA Rule not found.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(rule);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                rule.IsDeleted = true;

                rule.IsActive = false;

                rule.ModifiedBy =
                    _currentUserService.UserId;

                rule.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<Slarule>()
                    .Update(rule);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "Slarule",
                    "DELETE",
                    rule.SlaruleId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "SLA Rule Deleted Successfully",

                    Data =
                        rule.SlaruleId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting SLA rule");

                throw;
            }
        }

        // =====================================================
        // GET ALL SLA RULES
        // =====================================================

        public async Task<ApiResponse<List<SlaruleDto>>>
            GetSlarules()
        {
            try
            {
                var rules =
                    await _unitOfWork
                        .Repository<Slarule>()
                        .GetAllAsync();


                var result =
                    rules
                        .Where(x => !x.IsDeleted)
                        .OrderByDescending(x =>
                            x.SlaruleId)
                        .Select(x =>
                            new SlaruleDto
                            {
                                SlaruleId =
                                    x.SlaruleId,

                                Slaname =
                                    x.Slaname,

                                ModuleName =
                                    x.ModuleName,

                                Description =
                                    x.Description,

                                Priority =
                                    x.Priority,

                                FirstResponseMinutes =
                                    x.FirstResponseMinutes,

                                ResolutionMinutes =
                                    x.ResolutionMinutes,

                                WarningMinutes =
                                    x.WarningMinutes,

                                BusinessHoursId =
                                    x.BusinessHoursId,

                                HolidayCalendarId =
                                    x.HolidayCalendarId,

                                EscalationEnabled =
                                    x.EscalationEnabled,

                                EscalationRuleId =
                                    x.EscalationRuleId,

                                CompanyId =
                                    x.CompanyId,

                                RegionId =
                                    x.RegionId,

                                IsActive =
                                    x.IsActive,

                                CreatedAt =
                                    x.CreatedAt,

                                ModifiedAt =
                                    x.ModifiedAt
                            })
                        .ToList();


                return new ApiResponse<List<SlaruleDto>>
                {
                    Success = true,

                    Message = "Success",

                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting SLA rules");

                throw;
            }
        }

        // =====================================================
        // GET SLA RULE BY ID
        // =====================================================

        public async Task<ApiResponse<SlaruleDto>>
            GetSlaruleById(
                int id)
        {
            try
            {
                var rule =
                    (await _unitOfWork
                        .Repository<Slarule>()
                        .FindAsync(x =>
                            x.SlaruleId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (rule == null)
                    throw new CustomException(
                        "SLA Rule not found.");


                return new ApiResponse<SlaruleDto>
                {
                    Success = true,

                    Message = "Success",

                    Data =
                        new SlaruleDto
                        {
                            SlaruleId =
                                rule.SlaruleId,

                            Slaname =
                                rule.Slaname,

                            ModuleName =
                                rule.ModuleName,

                            Description =
                                rule.Description,

                            Priority =
                                rule.Priority,

                            FirstResponseMinutes =
                                rule.FirstResponseMinutes,

                            ResolutionMinutes =
                                rule.ResolutionMinutes,

                            WarningMinutes =
                                rule.WarningMinutes,

                            BusinessHoursId =
                                rule.BusinessHoursId,

                            HolidayCalendarId =
                                rule.HolidayCalendarId,

                            EscalationEnabled =
                                rule.EscalationEnabled,

                            EscalationRuleId =
                                rule.EscalationRuleId,

                            CompanyId =
                                rule.CompanyId,

                            RegionId =
                                rule.RegionId,

                            IsActive =
                                rule.IsActive,

                            CreatedAt =
                                rule.CreatedAt,

                            ModifiedAt =
                                rule.ModifiedAt
                        }
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting SLA rule by id");

                throw;
            }
        }
        #endregion
        #region EMAIL AUTOMATION CRUD

        // =====================================================
        // CREATE EMAIL AUTOMATION
        // =====================================================

        public async Task<ApiResponse<string>>
            CreateEmailAutomation(
                EmailAutomationDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.AutomationName))
                    throw new CustomException(
                        "Automation Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (string.IsNullOrWhiteSpace(dto.TriggerEvent))
                    throw new CustomException(
                        "Trigger Event is required.");

                if (string.IsNullOrWhiteSpace(dto.RecipientType))
                    throw new CustomException(
                        "Recipient Type is required.");

                if (string.IsNullOrWhiteSpace(dto.ScheduleType))
                    throw new CustomException(
                        "Schedule Type is required.");

                if (dto.DelayMinutes.HasValue &&
                    dto.DelayMinutes.Value < 0)
                    throw new CustomException(
                        "Delay Minutes cannot be negative.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // DUPLICATE EMAIL AUTOMATION
                // ---------------------------------------------

                var duplicateAutomation =
                    await _unitOfWork
                        .Repository<EmailAutomation>()
                        .FindAsync(x =>
                            x.AutomationName.ToLower() ==
                                dto.AutomationName.Trim().ToLower() &&

                            x.ModuleName.ToLower() ==
                                dto.ModuleName.Trim().ToLower() &&

                            x.CompanyId ==
                                dto.CompanyId &&

                            x.RegionId ==
                                dto.RegionId &&

                            !x.IsDeleted);

                if (duplicateAutomation.Any())
                    throw new CustomException(
                        "An email automation with this name already exists for the selected module.");


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                EmailAutomation automation =
                    new EmailAutomation
                    {
                        AutomationName =
                            dto.AutomationName.Trim(),

                        ModuleName =
                            dto.ModuleName.Trim(),

                        Description =
                            dto.Description,

                        TriggerEvent =
                            dto.TriggerEvent.Trim(),

                        EmailTemplateId =
                            dto.EmailTemplateId,

                        RecipientType =
                            dto.RecipientType.Trim(),

                        ScheduleType =
                            dto.ScheduleType.Trim(),

                        DelayMinutes =
                            dto.DelayMinutes,

                        FromEmail =
                            dto.FromEmail?.Trim(),

                        CompanyId =
                            dto.CompanyId,

                        RegionId =
                            dto.RegionId,

                        IsActive = true,

                        IsDeleted = false,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedAt =
                            DateTime.Now
                    };


                await _unitOfWork
                    .Repository<EmailAutomation>()
                    .AddAsync(automation);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "EmailAutomation",
                    "INSERT",
                    automation.EmailAutomationId,
                    "",
                    JsonConvert.SerializeObject(automation),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Email Automation Created Successfully",

                    Data =
                        automation.EmailAutomationId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating email automation");

                throw;
            }
        }

        // =====================================================
        // UPDATE EMAIL AUTOMATION
        // =====================================================

        public async Task<ApiResponse<string>>
            UpdateEmailAutomation(
                EmailAutomationDto dto)
        {
            try
            {
                // ---------------------------------------------
                // GET EXISTING AUTOMATION
                // ---------------------------------------------

                var automation =
                    (await _unitOfWork
                        .Repository<EmailAutomation>()
                        .FindAsync(x =>
                            x.EmailAutomationId ==
                                dto.EmailAutomationId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (automation == null)
                    throw new CustomException(
                        "Email Automation not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.AutomationName))
                    throw new CustomException(
                        "Automation Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (string.IsNullOrWhiteSpace(dto.TriggerEvent))
                    throw new CustomException(
                        "Trigger Event is required.");

                if (string.IsNullOrWhiteSpace(dto.RecipientType))
                    throw new CustomException(
                        "Recipient Type is required.");

                if (string.IsNullOrWhiteSpace(dto.ScheduleType))
                    throw new CustomException(
                        "Schedule Type is required.");

                if (dto.DelayMinutes.HasValue &&
                    dto.DelayMinutes.Value < 0)
                    throw new CustomException(
                        "Delay Minutes cannot be negative.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // DUPLICATE EMAIL AUTOMATION
                // ---------------------------------------------

                var duplicateAutomation =
                    await _unitOfWork
                        .Repository<EmailAutomation>()
                        .FindAsync(x =>
                            x.EmailAutomationId !=
                                dto.EmailAutomationId &&

                            x.AutomationName.ToLower() ==
                                dto.AutomationName.Trim().ToLower() &&

                            x.ModuleName.ToLower() ==
                                dto.ModuleName.Trim().ToLower() &&

                            x.CompanyId ==
                                dto.CompanyId &&

                            x.RegionId ==
                                dto.RegionId &&

                            !x.IsDeleted);

                if (duplicateAutomation.Any())
                    throw new CustomException(
                        "An email automation with this name already exists for the selected module.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(automation);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                automation.AutomationName =
                    dto.AutomationName.Trim();

                automation.ModuleName =
                    dto.ModuleName.Trim();

                automation.Description =
                    dto.Description;

                automation.TriggerEvent =
                    dto.TriggerEvent.Trim();

                automation.EmailTemplateId =
                    dto.EmailTemplateId;

                automation.RecipientType =
                    dto.RecipientType.Trim();

                automation.ScheduleType =
                    dto.ScheduleType.Trim();

                automation.DelayMinutes =
                    dto.DelayMinutes;

                automation.FromEmail =
                    dto.FromEmail?.Trim();

                automation.CompanyId =
                    dto.CompanyId;

                automation.RegionId =
                    dto.RegionId;

                automation.IsActive =
                    dto.IsActive;

                automation.ModifiedBy =
                    _currentUserService.UserId;

                automation.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<EmailAutomation>()
                    .Update(automation);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "EmailAutomation",
                    "UPDATE",
                    automation.EmailAutomationId,
                    oldValues,
                    JsonConvert.SerializeObject(automation),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Email Automation Updated Successfully",

                    Data =
                        automation.EmailAutomationId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating email automation");

                throw;
            }
        }

        // =====================================================
        // DELETE EMAIL AUTOMATION
        // =====================================================

        public async Task<ApiResponse<string>>
            DeleteEmailAutomation(
                int id)
        {
            try
            {
                // ---------------------------------------------
                // GET EXISTING AUTOMATION
                // ---------------------------------------------

                var automation =
                    (await _unitOfWork
                        .Repository<EmailAutomation>()
                        .FindAsync(x =>
                            x.EmailAutomationId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (automation == null)
                    throw new CustomException(
                        "Email Automation not found.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(automation);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                automation.IsDeleted = true;

                automation.IsActive = false;

                automation.ModifiedBy =
                    _currentUserService.UserId;

                automation.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<EmailAutomation>()
                    .Update(automation);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "EmailAutomation",
                    "DELETE",
                    automation.EmailAutomationId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Email Automation Deleted Successfully",

                    Data =
                        automation.EmailAutomationId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting email automation");

                throw;
            }
        }

        // =====================================================
        // GET ALL EMAIL AUTOMATIONS
        // =====================================================

        public async Task<ApiResponse<List<EmailAutomationDto>>>
            GetEmailAutomations()
        {
            try
            {
                var automations =
                    await _unitOfWork
                        .Repository<EmailAutomation>()
                        .GetAllAsync();


                var result =
                    automations
                        .Where(x => !x.IsDeleted)
                        .OrderByDescending(x =>
                            x.EmailAutomationId)
                        .Select(x =>
                            new EmailAutomationDto
                            {
                                EmailAutomationId =
                                    x.EmailAutomationId,

                                AutomationName =
                                    x.AutomationName,

                                ModuleName =
                                    x.ModuleName,

                                Description =
                                    x.Description,

                                TriggerEvent =
                                    x.TriggerEvent,

                                EmailTemplateId =
                                    x.EmailTemplateId,

                                RecipientType =
                                    x.RecipientType,

                                ScheduleType =
                                    x.ScheduleType,

                                DelayMinutes =
                                    x.DelayMinutes,

                                FromEmail =
                                    x.FromEmail,

                                CompanyId =
                                    x.CompanyId,

                                RegionId =
                                    x.RegionId,

                                IsActive =
                                    x.IsActive,

                                CreatedAt =
                                    x.CreatedAt,

                                ModifiedAt =
                                    x.ModifiedAt
                            })
                        .ToList();


                return new ApiResponse<List<EmailAutomationDto>>
                {
                    Success = true,

                    Message = "Success",

                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting email automations");

                throw;
            }
        }
        // =====================================================
        // GET EMAIL AUTOMATION BY ID
        // =====================================================

        public async Task<ApiResponse<EmailAutomationDto>>
            GetEmailAutomationById(
                int id)
        {
            try
            {
                var automation =
                    (await _unitOfWork
                        .Repository<EmailAutomation>()
                        .FindAsync(x =>
                            x.EmailAutomationId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (automation == null)
                    throw new CustomException(
                        "Email Automation not found.");


                return new ApiResponse<EmailAutomationDto>
                {
                    Success = true,

                    Message = "Success",

                    Data =
                        new EmailAutomationDto
                        {
                            EmailAutomationId =
                                automation.EmailAutomationId,

                            AutomationName =
                                automation.AutomationName,

                            ModuleName =
                                automation.ModuleName,

                            Description =
                                automation.Description,

                            TriggerEvent =
                                automation.TriggerEvent,

                            EmailTemplateId =
                                automation.EmailTemplateId,

                            RecipientType =
                                automation.RecipientType,

                            ScheduleType =
                                automation.ScheduleType,

                            DelayMinutes =
                                automation.DelayMinutes,

                            FromEmail =
                                automation.FromEmail,

                            CompanyId =
                                automation.CompanyId,

                            RegionId =
                                automation.RegionId,

                            IsActive =
                                automation.IsActive,

                            CreatedAt =
                                automation.CreatedAt,

                            ModifiedAt =
                                automation.ModifiedAt
                        }
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting email automation by id");

                throw;
            }
        }
        #endregion

        #region EMAIL AUTOMATION RECIPIENT CRUD

        // =====================================================
        // CREATE EMAIL AUTOMATION RECIPIENT
        // =====================================================

        public async Task<ApiResponse<string>>
            CreateEmailAutomationRecipient(
                EmailAutomationRecipientDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (dto.EmailAutomationId <= 0)
                    throw new CustomException(
                        "Email Automation is required.");

                if (string.IsNullOrWhiteSpace(dto.RecipientType))
                    throw new CustomException(
                        "Recipient Type is required.");

                if (string.IsNullOrWhiteSpace(dto.RecipientValue))
                    throw new CustomException(
                        "Recipient Value is required.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // CHECK EMAIL AUTOMATION
                // ---------------------------------------------

                var automation =
                    (await _unitOfWork
                        .Repository<EmailAutomation>()
                        .FindAsync(x =>
                            x.EmailAutomationId ==
                                dto.EmailAutomationId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (automation == null)
                    throw new CustomException(
                        "Email Automation not found.");


                // ---------------------------------------------
                // DUPLICATE RECIPIENT
                // ---------------------------------------------

                var duplicateRecipient =
                    await _unitOfWork
                        .Repository<EmailAutomationRecipient>()
                        .FindAsync(x =>
                            x.EmailAutomationId ==
                                dto.EmailAutomationId &&

                            x.RecipientType.ToLower() ==
                                dto.RecipientType.Trim().ToLower() &&

                            x.RecipientValue.ToLower() ==
                                dto.RecipientValue.Trim().ToLower() &&

                            !x.IsDeleted);

                if (duplicateRecipient.Any())
                    throw new CustomException(
                        "This recipient already exists for the selected email automation.");


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                EmailAutomationRecipient recipient =
                    new EmailAutomationRecipient
                    {
                        EmailAutomationId =
                            dto.EmailAutomationId,

                        RecipientType =
                            dto.RecipientType.Trim(),

                        RecipientValue =
                            dto.RecipientValue.Trim(),

                        CompanyId =
                            dto.CompanyId,

                        RegionId =
                            dto.RegionId,

                        IsActive = true,

                        IsDeleted = false,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedAt =
                            DateTime.Now
                    };


                await _unitOfWork
                    .Repository<EmailAutomationRecipient>()
                    .AddAsync(recipient);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "EmailAutomationRecipient",
                    "INSERT",
                    recipient.EmailAutomationRecipientId,
                    "",
                    JsonConvert.SerializeObject(recipient),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Email Automation Recipient Created Successfully",

                    Data =
                        recipient.EmailAutomationRecipientId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating email automation recipient");

                throw;
            }
        }

        // =====================================================
        // UPDATE EMAIL AUTOMATION RECIPIENT
        // =====================================================

        public async Task<ApiResponse<string>>
            UpdateEmailAutomationRecipient(
                EmailAutomationRecipientDto dto)
        {
            try
            {
                // ---------------------------------------------
                // GET EXISTING RECIPIENT
                // ---------------------------------------------

                var recipient =
                    (await _unitOfWork
                        .Repository<EmailAutomationRecipient>()
                        .FindAsync(x =>
                            x.EmailAutomationRecipientId ==
                                dto.EmailAutomationRecipientId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (recipient == null)
                    throw new CustomException(
                        "Email Automation Recipient not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (dto.EmailAutomationId <= 0)
                    throw new CustomException(
                        "Email Automation is required.");

                if (string.IsNullOrWhiteSpace(dto.RecipientType))
                    throw new CustomException(
                        "Recipient Type is required.");

                if (string.IsNullOrWhiteSpace(dto.RecipientValue))
                    throw new CustomException(
                        "Recipient Value is required.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // CHECK EMAIL AUTOMATION
                // ---------------------------------------------

                var automation =
                    (await _unitOfWork
                        .Repository<EmailAutomation>()
                        .FindAsync(x =>
                            x.EmailAutomationId ==
                                dto.EmailAutomationId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (automation == null)
                    throw new CustomException(
                        "Email Automation not found.");


                // ---------------------------------------------
                // DUPLICATE RECIPIENT
                // ---------------------------------------------

                var duplicateRecipient =
                    await _unitOfWork
                        .Repository<EmailAutomationRecipient>()
                        .FindAsync(x =>
                            x.EmailAutomationRecipientId !=
                                dto.EmailAutomationRecipientId &&

                            x.EmailAutomationId ==
                                dto.EmailAutomationId &&

                            x.RecipientType.ToLower() ==
                                dto.RecipientType.Trim().ToLower() &&

                            x.RecipientValue.ToLower() ==
                                dto.RecipientValue.Trim().ToLower() &&

                            !x.IsDeleted);

                if (duplicateRecipient.Any())
                    throw new CustomException(
                        "This recipient already exists for the selected email automation.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(recipient);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                recipient.EmailAutomationId =
                    dto.EmailAutomationId;

                recipient.RecipientType =
                    dto.RecipientType.Trim();

                recipient.RecipientValue =
                    dto.RecipientValue.Trim();

                recipient.CompanyId =
                    dto.CompanyId;

                recipient.RegionId =
                    dto.RegionId;

                recipient.IsActive =
                    dto.IsActive;

                recipient.ModifiedBy =
                    _currentUserService.UserId;

                recipient.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<EmailAutomationRecipient>()
                    .Update(recipient);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "EmailAutomationRecipient",
                    "UPDATE",
                    recipient.EmailAutomationRecipientId,
                    oldValues,
                    JsonConvert.SerializeObject(recipient),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Email Automation Recipient Updated Successfully",

                    Data =
                        recipient.EmailAutomationRecipientId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating email automation recipient");

                throw;
            }
        }

        // =====================================================
        // DELETE EMAIL AUTOMATION RECIPIENT
        // =====================================================

        public async Task<ApiResponse<string>>
            DeleteEmailAutomationRecipient(
                int id)
        {
            try
            {
                // ---------------------------------------------
                // GET EXISTING RECIPIENT
                // ---------------------------------------------

                var recipient =
                    (await _unitOfWork
                        .Repository<EmailAutomationRecipient>()
                        .FindAsync(x =>
                            x.EmailAutomationRecipientId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (recipient == null)
                    throw new CustomException(
                        "Email Automation Recipient not found.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(recipient);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                recipient.IsDeleted = true;

                recipient.IsActive = false;

                recipient.ModifiedBy =
                    _currentUserService.UserId;

                recipient.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<EmailAutomationRecipient>()
                    .Update(recipient);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "EmailAutomationRecipient",
                    "DELETE",
                    recipient.EmailAutomationRecipientId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Email Automation Recipient Deleted Successfully",

                    Data =
                        recipient.EmailAutomationRecipientId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting email automation recipient");

                throw;
            }
        }

        // =====================================================
        // GET ALL EMAIL AUTOMATION RECIPIENTS
        // =====================================================

        public async Task<ApiResponse<List<EmailAutomationRecipientDto>>>
            GetEmailAutomationRecipients()
        {
            try
            {
                var recipients =
                    await _unitOfWork
                        .Repository<EmailAutomationRecipient>()
                        .GetAllAsync();


                var result =
                    recipients
                        .Where(x => !x.IsDeleted)
                        .OrderByDescending(x =>
                            x.EmailAutomationRecipientId)
                        .Select(x =>
                            new EmailAutomationRecipientDto
                            {
                                EmailAutomationRecipientId =
                                    x.EmailAutomationRecipientId,

                                EmailAutomationId =
                                    x.EmailAutomationId,

                                RecipientType =
                                    x.RecipientType,

                                RecipientValue =
                                    x.RecipientValue,

                                CompanyId =
                                    x.CompanyId,

                                RegionId =
                                    x.RegionId,

                                IsActive =
                                    x.IsActive,

                                CreatedAt =
                                    x.CreatedAt,

                                ModifiedAt =
                                    x.ModifiedAt
                            })
                        .ToList();


                return new ApiResponse<List<EmailAutomationRecipientDto>>
                {
                    Success = true,

                    Message = "Success",

                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting email automation recipients");

                throw;
            }
        }

        // =====================================================
        // GET EMAIL AUTOMATION RECIPIENT BY ID
        // =====================================================

        public async Task<ApiResponse<EmailAutomationRecipientDto>>
            GetEmailAutomationRecipientById(
                int id)
        {
            try
            {
                var recipient =
                    (await _unitOfWork
                        .Repository<EmailAutomationRecipient>()
                        .FindAsync(x =>
                            x.EmailAutomationRecipientId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (recipient == null)
                    throw new CustomException(
                        "Email Automation Recipient not found.");


                return new ApiResponse<EmailAutomationRecipientDto>
                {
                    Success = true,

                    Message = "Success",

                    Data =
                        new EmailAutomationRecipientDto
                        {
                            EmailAutomationRecipientId =
                                recipient.EmailAutomationRecipientId,

                            EmailAutomationId =
                                recipient.EmailAutomationId,

                            RecipientType =
                                recipient.RecipientType,

                            RecipientValue =
                                recipient.RecipientValue,

                            CompanyId =
                                recipient.CompanyId,

                            RegionId =
                                recipient.RegionId,

                            IsActive =
                                recipient.IsActive,

                            CreatedAt =
                                recipient.CreatedAt,

                            ModifiedAt =
                                recipient.ModifiedAt
                        }
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting email automation recipient by id");

                throw;
            }
        }
        #endregion

        #region SCHEDULED JOB CRUD

        // =====================================================
        // CREATE SCHEDULED JOB
        // =====================================================

        public async Task<ApiResponse<string>>
            CreateScheduledJob(
                ScheduledJobDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.JobName))
                    throw new CustomException(
                        "Job Name is required.");

                if (string.IsNullOrWhiteSpace(dto.JobType))
                    throw new CustomException(
                        "Job Type is required.");

                if (string.IsNullOrWhiteSpace(dto.Frequency))
                    throw new CustomException(
                        "Frequency is required.");

                if (string.IsNullOrWhiteSpace(dto.ActionType))
                    throw new CustomException(
                        "Action Type is required.");

                if (dto.RepeatEvery.HasValue &&
                    dto.RepeatEvery.Value <= 0)
                    throw new CustomException(
                        "Repeat Every must be greater than zero.");

                if (dto.DayOfWeek.HasValue &&
                    (dto.DayOfWeek.Value < 0 ||
                     dto.DayOfWeek.Value > 6))
                    throw new CustomException(
                        "Day Of Week must be between 0 and 6.");

                if (dto.DayOfMonth.HasValue &&
                    (dto.DayOfMonth.Value < 1 ||
                     dto.DayOfMonth.Value > 31))
                    throw new CustomException(
                        "Day Of Month must be between 1 and 31.");

                if (dto.RetryCount.HasValue &&
                    dto.RetryCount.Value < 0)
                    throw new CustomException(
                        "Retry Count cannot be negative.");

                if (dto.TimeoutMinutes.HasValue &&
                    dto.TimeoutMinutes.Value <= 0)
                    throw new CustomException(
                        "Timeout Minutes must be greater than zero.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // DUPLICATE SCHEDULED JOB
                // ---------------------------------------------

                var duplicateJob =
                    await _unitOfWork
                        .Repository<ScheduledJob>()
                        .FindAsync(x =>
                            x.JobName.ToLower() ==
                                dto.JobName.Trim().ToLower() &&

                            x.CompanyId ==
                                dto.CompanyId &&

                            x.RegionId ==
                                dto.RegionId &&

                            !x.IsDeleted);

                if (duplicateJob.Any())
                    throw new CustomException(
                        "A scheduled job with this name already exists.");


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                ScheduledJob job =
                    new ScheduledJob
                    {
                        JobName =
                            dto.JobName.Trim(),

                        JobType =
                            dto.JobType.Trim(),

                        Description =
                            dto.Description,

                        Frequency =
                            dto.Frequency.Trim(),

                        StartDate =
                            dto.StartDate,

                        StartTime =
                            dto.StartTime,

                        RepeatEvery =
                            dto.RepeatEvery,

                        DayOfWeek =
                            dto.DayOfWeek,

                        DayOfMonth =
                            dto.DayOfMonth,

                        ActionType =
                            dto.ActionType.Trim(),

                        Parameters =
                            dto.Parameters,

                        RetryCount =
                            dto.RetryCount,

                        TimeoutMinutes =
                            dto.TimeoutMinutes,

                        LastRunAt =
                            dto.LastRunAt,

                        NextRunAt =
                            dto.NextRunAt,

                        LastRunStatus =
                            dto.LastRunStatus,

                        CompanyId =
                            dto.CompanyId,

                        RegionId =
                            dto.RegionId,

                        IsActive = true,

                        IsDeleted = false,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedAt =
                            DateTime.Now
                    };


                await _unitOfWork
                    .Repository<ScheduledJob>()
                    .AddAsync(job);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "ScheduledJob",
                    "INSERT",
                    job.ScheduledJobId,
                    "",
                    JsonConvert.SerializeObject(job),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Scheduled Job Created Successfully",

                    Data =
                        job.ScheduledJobId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating scheduled job");

                throw;
            }
        }
        // =====================================================
        // UPDATE SCHEDULED JOB
        // =====================================================

        public async Task<ApiResponse<string>>
            UpdateScheduledJob(
                ScheduledJobDto dto)
        {
            try
            {
                // ---------------------------------------------
                // GET EXISTING JOB
                // ---------------------------------------------

                var job =
                    (await _unitOfWork
                        .Repository<ScheduledJob>()
                        .FindAsync(x =>
                            x.ScheduledJobId ==
                                dto.ScheduledJobId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (job == null)
                    throw new CustomException(
                        "Scheduled Job not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.JobName))
                    throw new CustomException(
                        "Job Name is required.");

                if (string.IsNullOrWhiteSpace(dto.JobType))
                    throw new CustomException(
                        "Job Type is required.");

                if (string.IsNullOrWhiteSpace(dto.Frequency))
                    throw new CustomException(
                        "Frequency is required.");

                if (string.IsNullOrWhiteSpace(dto.ActionType))
                    throw new CustomException(
                        "Action Type is required.");

                if (dto.RepeatEvery.HasValue &&
                    dto.RepeatEvery.Value <= 0)
                    throw new CustomException(
                        "Repeat Every must be greater than zero.");

                if (dto.DayOfWeek.HasValue &&
                    (dto.DayOfWeek.Value < 0 ||
                     dto.DayOfWeek.Value > 6))
                    throw new CustomException(
                        "Day Of Week must be between 0 and 6.");

                if (dto.DayOfMonth.HasValue &&
                    (dto.DayOfMonth.Value < 1 ||
                     dto.DayOfMonth.Value > 31))
                    throw new CustomException(
                        "Day Of Month must be between 1 and 31.");

                if (dto.RetryCount.HasValue &&
                    dto.RetryCount.Value < 0)
                    throw new CustomException(
                        "Retry Count cannot be negative.");

                if (dto.TimeoutMinutes.HasValue &&
                    dto.TimeoutMinutes.Value <= 0)
                    throw new CustomException(
                        "Timeout Minutes must be greater than zero.");

                if (dto.CompanyId <= 0)
                    throw new CustomException(
                        "Company is required.");

                if (dto.RegionId <= 0)
                    throw new CustomException(
                        "Region is required.");


                // ---------------------------------------------
                // DUPLICATE SCHEDULED JOB
                // ---------------------------------------------

                var duplicateJob =
                    await _unitOfWork
                        .Repository<ScheduledJob>()
                        .FindAsync(x =>
                            x.ScheduledJobId !=
                                dto.ScheduledJobId &&

                            x.JobName.ToLower() ==
                                dto.JobName.Trim().ToLower() &&

                            x.CompanyId ==
                                dto.CompanyId &&

                            x.RegionId ==
                                dto.RegionId &&

                            !x.IsDeleted);

                if (duplicateJob.Any())
                    throw new CustomException(
                        "A scheduled job with this name already exists.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(job);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                job.JobName =
                    dto.JobName.Trim();

                job.JobType =
                    dto.JobType.Trim();

                job.Description =
                    dto.Description;

                job.Frequency =
                    dto.Frequency.Trim();

                job.StartDate =
                    dto.StartDate;

                job.StartTime =
                    dto.StartTime;

                job.RepeatEvery =
                    dto.RepeatEvery;

                job.DayOfWeek =
                    dto.DayOfWeek;

                job.DayOfMonth =
                    dto.DayOfMonth;

                job.ActionType =
                    dto.ActionType.Trim();

                job.Parameters =
                    dto.Parameters;

                job.RetryCount =
                    dto.RetryCount;

                job.TimeoutMinutes =
                    dto.TimeoutMinutes;

                job.LastRunAt =
                    dto.LastRunAt;

                job.NextRunAt =
                    dto.NextRunAt;

                job.LastRunStatus =
                    dto.LastRunStatus;

                job.CompanyId =
                    dto.CompanyId;

                job.RegionId =
                    dto.RegionId;

                job.IsActive =
                    dto.IsActive;

                job.ModifiedBy =
                    _currentUserService.UserId;

                job.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<ScheduledJob>()
                    .Update(job);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "ScheduledJob",
                    "UPDATE",
                    job.ScheduledJobId,
                    oldValues,
                    JsonConvert.SerializeObject(job),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Scheduled Job Updated Successfully",

                    Data =
                        job.ScheduledJobId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating scheduled job");

                throw;
            }
        }

        // =====================================================
        // DELETE SCHEDULED JOB
        // =====================================================

        public async Task<ApiResponse<string>>
            DeleteScheduledJob(
                int id)
        {
            try
            {
                // ---------------------------------------------
                // GET EXISTING JOB
                // ---------------------------------------------

                var job =
                    (await _unitOfWork
                        .Repository<ScheduledJob>()
                        .FindAsync(x =>
                            x.ScheduledJobId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (job == null)
                    throw new CustomException(
                        "Scheduled Job not found.");


                // ---------------------------------------------
                // OLD VALUES FOR AUDIT
                // ---------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(job);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                job.IsDeleted = true;

                job.IsActive = false;

                job.ModifiedBy =
                    _currentUserService.UserId;

                job.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<ScheduledJob>()
                    .Update(job);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "ScheduledJob",
                    "DELETE",
                    job.ScheduledJobId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Scheduled Job Deleted Successfully",

                    Data =
                        job.ScheduledJobId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting scheduled job");

                throw;
            }
        }
        // =====================================================
        // GET ALL SCHEDULED JOBS
        // =====================================================

        public async Task<ApiResponse<List<ScheduledJobDto>>>
            GetScheduledJobs()
        {
            try
            {
                var jobs =
                    await _unitOfWork
                        .Repository<ScheduledJob>()
                        .GetAllAsync();


                var result =
                    jobs
                        .Where(x => !x.IsDeleted)
                        .OrderByDescending(x =>
                            x.ScheduledJobId)
                        .Select(x =>
                            new ScheduledJobDto
                            {
                                ScheduledJobId =
                                    x.ScheduledJobId,

                                JobName =
                                    x.JobName,

                                JobType =
                                    x.JobType,

                                Description =
                                    x.Description,

                                Frequency =
                                    x.Frequency,

                                StartDate =
                                    x.StartDate,

                                StartTime =
                                    x.StartTime,

                                RepeatEvery =
                                    x.RepeatEvery,

                                DayOfWeek =
                                    x.DayOfWeek,

                                DayOfMonth =
                                    x.DayOfMonth,

                                ActionType =
                                    x.ActionType,

                                Parameters =
                                    x.Parameters,

                                RetryCount =
                                    x.RetryCount,

                                TimeoutMinutes =
                                    x.TimeoutMinutes,

                                LastRunAt =
                                    x.LastRunAt,

                                NextRunAt =
                                    x.NextRunAt,

                                LastRunStatus =
                                    x.LastRunStatus,

                                CompanyId =
                                    x.CompanyId,

                                RegionId =
                                    x.RegionId,

                                IsActive =
                                    x.IsActive,

                                CreatedAt =
                                    x.CreatedAt,

                                ModifiedAt =
                                    x.ModifiedAt
                            })
                        .ToList();


                return new ApiResponse<List<ScheduledJobDto>>
                {
                    Success = true,

                    Message = "Success",

                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting scheduled jobs");

                throw;
            }
        }
        // =====================================================
        // GET SCHEDULED JOB BY ID
        // =====================================================

        public async Task<ApiResponse<ScheduledJobDto>>
            GetScheduledJobById(
                int id)
        {
            try
            {
                var job =
                    (await _unitOfWork
                        .Repository<ScheduledJob>()
                        .FindAsync(x =>
                            x.ScheduledJobId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (job == null)
                    throw new CustomException(
                        "Scheduled Job not found.");


                return new ApiResponse<ScheduledJobDto>
                {
                    Success = true,

                    Message = "Success",

                    Data =
                        new ScheduledJobDto
                        {
                            ScheduledJobId =
                                job.ScheduledJobId,

                            JobName =
                                job.JobName,

                            JobType =
                                job.JobType,

                            Description =
                                job.Description,

                            Frequency =
                                job.Frequency,

                            StartDate =
                                job.StartDate,

                            StartTime =
                                job.StartTime,

                            RepeatEvery =
                                job.RepeatEvery,

                            DayOfWeek =
                                job.DayOfWeek,

                            DayOfMonth =
                                job.DayOfMonth,

                            ActionType =
                                job.ActionType,

                            Parameters =
                                job.Parameters,

                            RetryCount =
                                job.RetryCount,

                            TimeoutMinutes =
                                job.TimeoutMinutes,

                            LastRunAt =
                                job.LastRunAt,

                            NextRunAt =
                                job.NextRunAt,

                            LastRunStatus =
                                job.LastRunStatus,

                            CompanyId =
                                job.CompanyId,

                            RegionId =
                                job.RegionId,

                            IsActive =
                                job.IsActive,

                            CreatedAt =
                                job.CreatedAt,

                            ModifiedAt =
                                job.ModifiedAt
                        }
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting scheduled job by id");

                throw;
            }
        }

        #endregion
    }
}

