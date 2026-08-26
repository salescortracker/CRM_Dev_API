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
    public class ModuleConfigurationService : IModuleConfigurationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public ModuleConfigurationService(
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
    }
}
