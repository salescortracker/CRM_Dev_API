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
        #region LeadSetting
        #region CREATE

        public async Task<ApiResponse<string>> CreateLeadSetting(LeadSettingDto dto)
        {
            try
            {
                // Validate Setting Name
                if (string.IsNullOrWhiteSpace(dto.SettingName))
                    throw new CustomException("Setting Name is required.");

                //// Validate Company
                //var company = (await _unitOfWork.Repository<Company>()
                //    .FindAsync(x => x.CompanyId == dto.CompanyId))
                //    .FirstOrDefault();

                //if (company == null)
                //    throw new CustomException("Company not found.");

                //// Validate Region
                //var region = (await _unitOfWork.Repository<Region>()
                //    .FindAsync(x =>
                //        x.RegionId == dto.RegionId &&
                //        x.CompanyId == dto.CompanyId))
                //    .FirstOrDefault();

                //if (region == null)
                //    throw new CustomException(
                //        "Region not found for the selected company.");

                // Validate Follow-Up Days
                if (dto.FollowUpDays < 0)
                    throw new CustomException(
                        "Follow-Up Days cannot be negative.");

                // Duplicate Setting Name
                var duplicate = await _unitOfWork.Repository<LeadSetting>()
                    .FindAsync(x =>
                        //x.CompanyId == dto.CompanyId &&
                        //x.RegionId == dto.RegionId &&
                        !x.IsDeleted &&
                        x.SettingName.ToLower() ==
                        dto.SettingName.ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "Lead Setting already exists .");

                // Create Entity
                LeadSetting leadSetting = new LeadSetting
                {
                    //CompanyId = dto.CompanyId,
                    //RegionId = dto.RegionId,
                    SettingName = dto.SettingName.Trim(),
                    LeadStatus = dto.LeadStatus,
                    LeadPriority = dto.LeadPriority,
                    AssignmentRule = dto.AssignmentRule,
                    FollowUpDays = dto.FollowUpDays,
                    Status = dto.Status,
                    EnableAutoAssignment = dto.EnableAutoAssignment,
                    EmailNotification = dto.EmailNotification,

                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                await _unitOfWork.Repository<LeadSetting>()
                    .AddAsync(leadSetting);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "LeadSetting",
                    "INSERT",
                    leadSetting.LeadSettingId,
                    "",
                    JsonConvert.SerializeObject(leadSetting),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Lead Setting Created Successfully",
                    Data = leadSetting.SettingName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating lead setting");
                throw;
            }
        }

        #endregion


        #region UPDATE

        public async Task<ApiResponse<string>> UpdateLeadSetting(
            LeadSettingDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.SettingName))
                    throw new CustomException("Setting Name is required.");

                // Get existing record
                var leadSetting =
                    (await _unitOfWork.Repository<LeadSetting>()
                        .FindAsync(x =>
                            x.LeadSettingId == dto.LeadSettingId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (leadSetting == null)
                    throw new CustomException("Lead Setting not found.");

                //// Validate Company
                //var company = (await _unitOfWork.Repository<Company>()
                //    .FindAsync(x => x.CompanyId == dto.CompanyId))
                //    .FirstOrDefault();

                //if (company == null)
                //    throw new CustomException("Company not found.");

                //// Validate Region
                //var region = (await _unitOfWork.Repository<Region>()
                //    .FindAsync(x =>
                //        x.RegionId == dto.RegionId &&
                //        x.CompanyId == dto.CompanyId))
                //    .FirstOrDefault();

                //if (region == null)
                //    throw new CustomException(
                //        "Region not found for the selected company.");

                // Validate Follow-Up Days
                if (dto.FollowUpDays < 0)
                    throw new CustomException(
                        "Follow-Up Days cannot be negative.");

                // Duplicate Setting Name
                var duplicate =
                    await _unitOfWork.Repository<LeadSetting>()
                        .FindAsync(x =>
                            x.LeadSettingId != dto.LeadSettingId &&
                            //x.CompanyId == dto.CompanyId &&
                            //x.RegionId == dto.RegionId &&
                            !x.IsDeleted &&
                            x.SettingName.ToLower() ==
                            dto.SettingName.ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "Lead Setting already exists .");

                // Old Values for Audit
                string oldValues =
                    JsonConvert.SerializeObject(leadSetting);

                // Update
                //leadSetting.CompanyId = dto.CompanyId;
                //leadSetting.RegionId = dto.RegionId;
                leadSetting.SettingName = dto.SettingName.Trim();
                leadSetting.LeadStatus = dto.LeadStatus;
                leadSetting.LeadPriority = dto.LeadPriority;
                leadSetting.AssignmentRule = dto.AssignmentRule;
                leadSetting.FollowUpDays = dto.FollowUpDays;
                leadSetting.Status = dto.Status;
                leadSetting.EnableAutoAssignment =
                    dto.EnableAutoAssignment;
                leadSetting.EmailNotification =
                    dto.EmailNotification;

                leadSetting.ModifiedBy =
                    _currentUserService.UserId;

                leadSetting.ModifiedDate =
                    DateTime.Now;

                _unitOfWork.Repository<LeadSetting>()
                    .Update(leadSetting);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "LeadSetting",
                    "UPDATE",
                    leadSetting.LeadSettingId,
                    oldValues,
                    JsonConvert.SerializeObject(leadSetting),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Lead Setting Updated Successfully",
                    Data = leadSetting.SettingName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating lead setting");
                throw;
            }
        }

        #endregion


        #region DELETE

        public async Task<ApiResponse<string>> DeleteLeadSetting(int id)
        {
            try
            {
                var leadSetting =
                    (await _unitOfWork.Repository<LeadSetting>()
                        .FindAsync(x =>
                            x.LeadSettingId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (leadSetting == null)
                    throw new CustomException(
                        "Lead Setting not found.");

                // Old Values for Audit
                string oldValues =
                    JsonConvert.SerializeObject(leadSetting);

                // Soft Delete
                leadSetting.IsDeleted = true;
                leadSetting.ModifiedBy =
                    _currentUserService.UserId;
                leadSetting.ModifiedDate =
                    DateTime.Now;

                _unitOfWork.Repository<LeadSetting>()
                    .Update(leadSetting);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "LeadSetting",
                    "DELETE",
                    leadSetting.LeadSettingId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Lead Setting Deleted Successfully",
                    Data = leadSetting.SettingName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting lead setting");
                throw;
            }
        }

        #endregion


        #region GET ALL

        public async Task<ApiResponse<List<LeadSettingDto>>>
            GetLeadSettings()
        {
            try
            {
                var leadSettings =
                    (await _unitOfWork.Repository<LeadSetting>()
                        .FindAsync(x => !x.IsDeleted))
                    .OrderByDescending(x => x.LeadSettingId)
                    .ToList();

                var result = leadSettings.Select(x =>
                    new LeadSettingDto
                    {
                        LeadSettingId = x.LeadSettingId,
                        //CompanyId = x.CompanyId,
                        //RegionId = x.RegionId,
                        SettingName = x.SettingName,
                        LeadStatus = x.LeadStatus,
                        LeadPriority = x.LeadPriority,
                        AssignmentRule = x.AssignmentRule,
                        FollowUpDays = x.FollowUpDays,
                        Status = x.Status,
                        EnableAutoAssignment =
                            x.EnableAutoAssignment,
                        EmailNotification =
                            x.EmailNotification
                    }).ToList();

                return new ApiResponse<List<LeadSettingDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while getting lead settings");
                throw;
            }
        }

        #endregion


        #region GET BY ID

        public async Task<ApiResponse<LeadSettingDto>>
            GetLeadSettingById(int id)
        {
            try
            {
                var leadSetting =
                    (await _unitOfWork.Repository<LeadSetting>()
                        .FindAsync(x =>
                            x.LeadSettingId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (leadSetting == null)
                    throw new CustomException(
                        "Lead Setting not found.");

                var result = new LeadSettingDto
                {
                    LeadSettingId = leadSetting.LeadSettingId,
                    //CompanyId = leadSetting.CompanyId,
                    //RegionId = leadSetting.RegionId,
                    SettingName = leadSetting.SettingName,
                    LeadStatus = leadSetting.LeadStatus,
                    LeadPriority = leadSetting.LeadPriority,
                    AssignmentRule = leadSetting.AssignmentRule,
                    FollowUpDays = leadSetting.FollowUpDays,
                    Status = leadSetting.Status,
                    EnableAutoAssignment =
                        leadSetting.EnableAutoAssignment,
                    EmailNotification =
                        leadSetting.EmailNotification
                };

                return new ApiResponse<LeadSettingDto>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while getting lead setting by id");
                throw;
            }
        }

        #endregion
        #endregion
        #region PipelineSetting
        #region CREATE

        public async Task<ApiResponse<string>> CreatePipelineSetting(
            PipelineSettingDto dto)
        {
            try
            {
                // Pipeline Name validation
                if (string.IsNullOrWhiteSpace(dto.PipelineName))
                    throw new CustomException(
                        "Pipeline Name is required.");

                // Pipeline Code validation
                if (string.IsNullOrWhiteSpace(dto.PipelineCode))
                    throw new CustomException(
                        "Pipeline Code is required.");

                // Pipeline Type validation
                if (string.IsNullOrWhiteSpace(dto.PipelineType))
                    throw new CustomException(
                        "Pipeline Type is required.");

                // Total Stages validation
                if (dto.TotalStages < 0)
                    throw new CustomException(
                        "Total Stages cannot be negative.");

                // Company validation
                //var company =
                //    (await _unitOfWork.Repository<Company>()
                //        .FindAsync(x =>
                //            x.CompanyId == dto.CompanyId))
                //    .FirstOrDefault();

                //if (company == null)
                //    throw new CustomException(
                //        "Company not found.");

                // Region validation
                //var region =
                //    (await _unitOfWork.Repository<Region>()
                //        .FindAsync(x =>
                //            x.RegionId == dto.RegionId &&
                //            x.CompanyId == dto.CompanyId))
                //    .FirstOrDefault();

                //if (region == null)
                //    throw new CustomException(
                //        "Region not found for the selected company.");

                // Duplicate Pipeline Name
                var duplicateName =
                    await _unitOfWork.Repository<PipelineSetting>()
                        .FindAsync(x =>
                            //x.CompanyId == dto.CompanyId &&
                            //x.RegionId == dto.RegionId &&
                            !x.IsDeleted &&
                            x.PipelineName.ToLower() ==
                            dto.PipelineName.Trim().ToLower());

                if (duplicateName.Any())
                    throw new CustomException(
                        "Pipeline Name already exists.");

                // Duplicate Pipeline Code
                var duplicateCode =
                    await _unitOfWork.Repository<PipelineSetting>()
                        .FindAsync(x =>
                            //x.CompanyId == dto.CompanyId &&
                            //x.RegionId == dto.RegionId &&
                            !x.IsDeleted &&
                            x.PipelineCode.ToLower() ==
                            dto.PipelineCode.Trim().ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Pipeline Code already exists for this company and region.");

                // Create entity
                PipelineSetting pipelineSetting =
                    new PipelineSetting
                    {
                        //CompanyId = dto.CompanyId,
                        //RegionId = dto.RegionId,

                        PipelineName = dto.PipelineName.Trim(),

                        PipelineCode = dto.PipelineCode.Trim(),

                        PipelineType = dto.PipelineType.Trim(),

                        TotalStages = dto.TotalStages,

                        Description =
                            string.IsNullOrWhiteSpace(dto.Description)
                                ? null
                                : dto.Description.Trim(),

                        Status = dto.Status,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedDate = DateTime.Now,

                        IsDeleted = false
                    };

                await _unitOfWork.Repository<PipelineSetting>()
                    .AddAsync(pipelineSetting);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "PipelineSetting",
                    "INSERT",
                    pipelineSetting.PipelineSettingId,
                    "",
                    JsonConvert.SerializeObject(
                        pipelineSetting),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Pipeline Setting Created Successfully",
                    Data = pipelineSetting.PipelineName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating pipeline setting");

                throw;
            }
        }

        #endregion


        #region UPDATE

        public async Task<ApiResponse<string>> UpdatePipelineSetting(
            PipelineSettingDto dto)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(dto.PipelineName))
                    throw new CustomException(
                        "Pipeline Name is required.");

                if (string.IsNullOrWhiteSpace(dto.PipelineCode))
                    throw new CustomException(
                        "Pipeline Code is required.");

                if (string.IsNullOrWhiteSpace(dto.PipelineType))
                    throw new CustomException(
                        "Pipeline Type is required.");

                if (dto.TotalStages < 0)
                    throw new CustomException(
                        "Total Stages cannot be negative.");

                // Get existing pipeline
                var pipelineSetting =
                    (await _unitOfWork.Repository<PipelineSetting>()
                        .FindAsync(x =>
                            x.PipelineSettingId ==
                            dto.PipelineSettingId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (pipelineSetting == null)
                    throw new CustomException(
                        "Pipeline Setting not found.");

                // Company validation
                //var company =
                //    (await _unitOfWork.Repository<Company>()
                //        .FindAsync(x =>
                //            x.CompanyId == dto.CompanyId))
                //    .FirstOrDefault();

                //if (company == null)
                //    throw new CustomException(
                //        "Company not found.");

                // Region validation
                //var region =
                //    (await _unitOfWork.Repository<Region>()
                //        .FindAsync(x =>
                //            x.RegionId == dto.RegionId &&
                //            x.CompanyId == dto.CompanyId))
                //    .FirstOrDefault();

                //if (region == null)
                //    throw new CustomException(
                //        "Region not found for the selected company.");

                // Duplicate Name
                var duplicateName =
                    await _unitOfWork.Repository<PipelineSetting>()
                        .FindAsync(x =>
                            x.PipelineSettingId !=
                            dto.PipelineSettingId &&

                            //x.CompanyId ==
                            //dto.CompanyId &&

                            //x.RegionId ==
                            //dto.RegionId &&

                            !x.IsDeleted &&

                            x.PipelineName.ToLower() ==
                            dto.PipelineName.Trim().ToLower());

                if (duplicateName.Any())
                    throw new CustomException(
                        "Pipeline Name already exists.");

                // Duplicate Code
                var duplicateCode =
                    await _unitOfWork.Repository<PipelineSetting>()
                        .FindAsync(x =>
                            x.PipelineSettingId !=
                            dto.PipelineSettingId &&

                            //x.CompanyId ==
                            //dto.CompanyId &&

                            //x.RegionId ==
                            //dto.RegionId &&

                            !x.IsDeleted &&

                            x.PipelineCode.ToLower() ==
                            dto.PipelineCode.Trim().ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Pipeline Code already exists.");

                // Old values
                string oldValues =
                    JsonConvert.SerializeObject(
                        pipelineSetting);

                // Update entity
                //pipelineSetting.CompanyId =
                //    dto.CompanyId;

                //pipelineSetting.RegionId =
                //    dto.RegionId;

                pipelineSetting.PipelineName =
                    dto.PipelineName.Trim();

                pipelineSetting.PipelineCode =
                    dto.PipelineCode.Trim();

                pipelineSetting.PipelineType =
                    dto.PipelineType.Trim();

                pipelineSetting.TotalStages =
                    dto.TotalStages;

                pipelineSetting.Description =
                    string.IsNullOrWhiteSpace(dto.Description)
                        ? null
                        : dto.Description.Trim();

                pipelineSetting.Status =
                    dto.Status;

                pipelineSetting.ModifiedBy =
                    _currentUserService.UserId;

                pipelineSetting.ModifiedDate =
                    DateTime.Now;

                _unitOfWork.Repository<PipelineSetting>()
                    .Update(pipelineSetting);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "PipelineSetting",
                    "UPDATE",
                    pipelineSetting.PipelineSettingId,
                    oldValues,
                    JsonConvert.SerializeObject(
                        pipelineSetting),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Pipeline Setting Updated Successfully",
                    Data = pipelineSetting.PipelineName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating pipeline setting");

                throw;
            }
        }

        #endregion


        #region DELETE

        public async Task<ApiResponse<string>> DeletePipelineSetting(
            int id)
        {
            try
            {
                var pipelineSetting =
                    (await _unitOfWork.Repository<PipelineSetting>()
                        .FindAsync(x =>
                            x.PipelineSettingId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (pipelineSetting == null)
                    throw new CustomException(
                        "Pipeline Setting not found.");

                // Old values
                string oldValues =
                    JsonConvert.SerializeObject(
                        pipelineSetting);

                // Soft delete
                pipelineSetting.IsDeleted = true;

                pipelineSetting.ModifiedBy =
                    _currentUserService.UserId;

                pipelineSetting.ModifiedDate =
                    DateTime.Now;

                _unitOfWork.Repository<PipelineSetting>()
                    .Update(pipelineSetting);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "PipelineSetting",
                    "DELETE",
                    pipelineSetting.PipelineSettingId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Pipeline Setting Deleted Successfully",
                    Data = pipelineSetting.PipelineName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting pipeline setting");

                throw;
            }
        }

        #endregion


        #region GET ALL

        public async Task<ApiResponse<List<PipelineSettingDto>>>
            GetPipelineSettings()
        {
            try
            {
                var pipelineSettings =
                    (await _unitOfWork.Repository<PipelineSetting>()
                        .FindAsync(x => !x.IsDeleted))
                    .OrderByDescending(
                        x => x.PipelineSettingId)
                    .ToList();

                var result =
                    pipelineSettings
                    .Select(x => new PipelineSettingDto
                    {
                        PipelineSettingId =
                            x.PipelineSettingId,

                        //CompanyId =
                        //    x.CompanyId,

                        //RegionId =
                        //    x.RegionId,

                        PipelineName =
                            x.PipelineName,

                        PipelineCode =
                            x.PipelineCode,

                        PipelineType =
                            x.PipelineType,

                        TotalStages =
                            x.TotalStages,

                        Description =
                            x.Description,

                        Status =
                            x.Status

                    }).ToList();

                return new ApiResponse<List<PipelineSettingDto>>
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
                    "Error while getting pipeline settings");

                throw;
            }
        }

        #endregion


        #region GET BY ID

        public async Task<ApiResponse<PipelineSettingDto>>
            GetPipelineSettingById(int id)
        {
            try
            {
                var pipelineSetting =
                    (await _unitOfWork.Repository<PipelineSetting>()
                        .FindAsync(x =>
                            x.PipelineSettingId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (pipelineSetting == null)
                    throw new CustomException(
                        "Pipeline Setting not found.");

                var result = new PipelineSettingDto
                {
                    PipelineSettingId =
                        pipelineSetting.PipelineSettingId,

                    //CompanyId =
                    //    pipelineSetting.CompanyId,

                    //RegionId =
                    //    pipelineSetting.RegionId,

                    PipelineName =
                        pipelineSetting.PipelineName,

                    PipelineCode =
                        pipelineSetting.PipelineCode,

                    PipelineType =
                        pipelineSetting.PipelineType,

                    TotalStages =
                        pipelineSetting.TotalStages,

                    Description =
                        pipelineSetting.Description,

                    Status =
                        pipelineSetting.Status
                };

                return new ApiResponse<PipelineSettingDto>
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
                    "Error while getting pipeline setting by id");

                throw;
            }
        }

        #endregion
        #endregion
        #region OpportunityStage
        #region CREATE

        public async Task<ApiResponse<string>> CreateOpportunityStage(
            OpportunityStageDto dto)
        {
            try
            {
                //var currentUserId = _currentUserService.UserId;

                //if (currentUserId <= 0)
                //{
                //    throw new CustomException(
                //        "Unable to identify the logged-in user. Please login again.");
                //}
                // Stage Name validation
                if (string.IsNullOrWhiteSpace(dto.StageName))
                    throw new CustomException(
                        "Stage Name is required.");

                // Stage Code validation
                if (string.IsNullOrWhiteSpace(dto.StageCode))
                    throw new CustomException(
                        "Stage Code is required.");

                // Forecast Category validation
                if (string.IsNullOrWhiteSpace(dto.ForecastCategory))
                    throw new CustomException(
                        "Forecast Category is required.");

                // Stage Type validation
                if (string.IsNullOrWhiteSpace(dto.StageType))
                    throw new CustomException(
                        "Stage Type is required.");

                // Probability validation
                if (dto.Probability < 0 || dto.Probability > 100)
                    throw new CustomException(
                        "Probability must be between 0 and 100.");

                // Stage Order validation
                if (dto.StageOrder <= 0)
                    throw new CustomException(
                        "Stage Order must be greater than 0.");

                // Won and Lost cannot both be true
                if (dto.WonStage && dto.LostStage)
                    throw new CustomException(
                        "A stage cannot be both Won and Lost.");

                // Duplicate Stage Name
                var duplicateName =
                    await _unitOfWork.Repository<OpportunityStage1>()
                        .FindAsync(x =>
                            !x.IsDeleted &&
                            x.StageName.ToLower() ==
                            dto.StageName.Trim().ToLower());

                if (duplicateName.Any())
                    throw new CustomException(
                        "Stage Name already exists.");

                // Duplicate Stage Code
                var duplicateCode =
                    await _unitOfWork.Repository<OpportunityStage1>()
                        .FindAsync(x =>
                            !x.IsDeleted &&
                            x.StageCode.ToLower() ==
                            dto.StageCode.Trim().ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Stage Code already exists.");

                // Duplicate Stage Order
                var duplicateOrder =
                    await _unitOfWork.Repository<OpportunityStage1>()
                        .FindAsync(x =>
                            !x.IsDeleted &&
                            x.StageOrder == dto.StageOrder);

                if (duplicateOrder.Any())
                    throw new CustomException(
                        "Stage Order already exists.");

                // Create entity
                OpportunityStage1 opportunityStage =
                    new OpportunityStage1
                    {
                        StageName = dto.StageName.Trim(),

                        StageCode = dto.StageCode.Trim(),

                        Probability = dto.Probability,

                        StageOrder = dto.StageOrder,

                        ForecastCategory =
                            dto.ForecastCategory.Trim(),

                        StageType =
                            dto.StageType.Trim(),

                        Status = dto.Status,

                        WonStage = dto.WonStage,

                        LostStage = dto.LostStage,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedDate = DateTime.Now,

                        IsDeleted = false
                    };

                await _unitOfWork.Repository<OpportunityStage1>()
                    .AddAsync(opportunityStage);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "OpportunityStage",
                    "INSERT",
                    opportunityStage.OpportunityStageId,
                    "",
                    JsonConvert.SerializeObject(
                        opportunityStage),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Opportunity Stage Created Successfully",
                    Data = opportunityStage.StageName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating opportunity stage");

                throw;
            }
        }

        #endregion


        #region UPDATE

        public async Task<ApiResponse<string>> UpdateOpportunityStage(
            OpportunityStageDto dto)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(dto.StageName))
                    throw new CustomException(
                        "Stage Name is required.");

                if (string.IsNullOrWhiteSpace(dto.StageCode))
                    throw new CustomException(
                        "Stage Code is required.");

                if (string.IsNullOrWhiteSpace(dto.ForecastCategory))
                    throw new CustomException(
                        "Forecast Category is required.");

                if (string.IsNullOrWhiteSpace(dto.StageType))
                    throw new CustomException(
                        "Stage Type is required.");

                if (dto.Probability < 0 || dto.Probability > 100)
                    throw new CustomException(
                        "Probability must be between 0 and 100.");

                if (dto.StageOrder <= 0)
                    throw new CustomException(
                        "Stage Order must be greater than 0.");

                if (dto.WonStage && dto.LostStage)
                    throw new CustomException(
                        "A stage cannot be both Won and Lost.");

                // Get existing record
                var opportunityStage =
                    (await _unitOfWork.Repository<OpportunityStage1>()
                        .FindAsync(x =>
                            x.OpportunityStageId ==
                            dto.OpportunityStageId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (opportunityStage == null)
                    throw new CustomException(
                        "Opportunity Stage not found.");

                // Duplicate Stage Name
                var duplicateName =
                    await _unitOfWork.Repository<OpportunityStage1>()
                        .FindAsync(x =>
                            x.OpportunityStageId !=
                            dto.OpportunityStageId &&

                            !x.IsDeleted &&

                            x.StageName.ToLower() ==
                            dto.StageName.Trim().ToLower());

                if (duplicateName.Any())
                    throw new CustomException(
                        "Stage Name already exists.");

                // Duplicate Stage Code
                var duplicateCode =
                    await _unitOfWork.Repository<OpportunityStage1>()
                        .FindAsync(x =>
                            x.OpportunityStageId !=
                            dto.OpportunityStageId &&

                            !x.IsDeleted &&

                            x.StageCode.ToLower() ==
                            dto.StageCode.Trim().ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Stage Code already exists.");

                // Duplicate Stage Order
                var duplicateOrder =
                    await _unitOfWork.Repository<OpportunityStage1>()
                        .FindAsync(x =>
                            x.OpportunityStageId !=
                            dto.OpportunityStageId &&

                            !x.IsDeleted &&

                            x.StageOrder ==
                            dto.StageOrder);

                if (duplicateOrder.Any())
                    throw new CustomException(
                        "Stage Order already exists.");

                // Old values
                string oldValues =
                    JsonConvert.SerializeObject(
                        opportunityStage);

                // Update
                opportunityStage.StageName =
                    dto.StageName.Trim();

                opportunityStage.StageCode =
                    dto.StageCode.Trim();

                opportunityStage.Probability =
                    dto.Probability;

                opportunityStage.StageOrder =
                    dto.StageOrder;

                opportunityStage.ForecastCategory =
                    dto.ForecastCategory.Trim();

                opportunityStage.StageType =
                    dto.StageType.Trim();

                opportunityStage.Status =
                    dto.Status;

                opportunityStage.WonStage =
                    dto.WonStage;

                opportunityStage.LostStage =
                    dto.LostStage;

                opportunityStage.ModifiedBy =
                    _currentUserService.UserId;

                opportunityStage.ModifiedDate =
                    DateTime.Now;

                _unitOfWork.Repository<OpportunityStage1>()
                    .Update(opportunityStage);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "OpportunityStage",
                    "UPDATE",
                    opportunityStage.OpportunityStageId,
                    oldValues,
                    JsonConvert.SerializeObject(
                        opportunityStage),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Opportunity Stage Updated Successfully",
                    Data = opportunityStage.StageName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating opportunity stage");

                throw;
            }
        }

        #endregion


        #region DELETE

        public async Task<ApiResponse<string>> DeleteOpportunityStage(
            int id)
        {
            try
            {
                var opportunityStage =
                    (await _unitOfWork.Repository<OpportunityStage1>()
                        .FindAsync(x =>
                            x.OpportunityStageId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (opportunityStage == null)
                    throw new CustomException(
                        "Opportunity Stage not found.");

                // Old values
                string oldValues =
                    JsonConvert.SerializeObject(
                        opportunityStage);

                // Soft delete
                opportunityStage.IsDeleted = true;

                opportunityStage.ModifiedBy =
                    _currentUserService.UserId;

                opportunityStage.ModifiedDate =
                    DateTime.Now;

                _unitOfWork.Repository<OpportunityStage1>()
                    .Update(opportunityStage);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "OpportunityStage",
                    "DELETE",
                    opportunityStage.OpportunityStageId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Opportunity Stage Deleted Successfully",
                    Data = opportunityStage.StageName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting opportunity stage");

                throw;
            }
        }

        #endregion


        #region GET ALL

        public async Task<ApiResponse<List<OpportunityStageDto>>>
            GetOpportunityStages()
        {
            try
            {
                var opportunityStages =
                    (await _unitOfWork.Repository<OpportunityStage1>()
                        .FindAsync(x => !x.IsDeleted))
                    .OrderBy(x => x.StageOrder)
                    .ToList();

                var result =
                    opportunityStages
                    .Select(x => new OpportunityStageDto
                    {
                        OpportunityStageId =
                            x.OpportunityStageId,

                        StageName =
                            x.StageName,

                        StageCode =
                            x.StageCode,

                        Probability =
                            x.Probability,

                        StageOrder =
                            x.StageOrder,

                        ForecastCategory =
                            x.ForecastCategory,

                        StageType =
                            x.StageType,

                        Status =
                            x.Status,

                        WonStage =
                            x.WonStage,

                        LostStage =
                            x.LostStage

                    }).ToList();

                return new ApiResponse<List<OpportunityStageDto>>
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
                    "Error while getting opportunity stages");

                throw;
            }
        }

        #endregion


        #region GET BY ID

        public async Task<ApiResponse<OpportunityStageDto>>
            GetOpportunityStageById(int id)
        {
            try
            {
                var opportunityStage =
                    (await _unitOfWork.Repository<OpportunityStage1>()
                        .FindAsync(x =>
                            x.OpportunityStageId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (opportunityStage == null)
                    throw new CustomException(
                        "Opportunity Stage not found.");

                var result = new OpportunityStageDto
                {
                    OpportunityStageId =
                        opportunityStage.OpportunityStageId,

                    StageName =
                        opportunityStage.StageName,

                    StageCode =
                        opportunityStage.StageCode,

                    Probability =
                        opportunityStage.Probability,

                    StageOrder =
                        opportunityStage.StageOrder,

                    ForecastCategory =
                        opportunityStage.ForecastCategory,

                    StageType =
                        opportunityStage.StageType,

                    Status =
                        opportunityStage.Status,

                    WonStage =
                        opportunityStage.WonStage,

                    LostStage =
                        opportunityStage.LostStage
                };

                return new ApiResponse<OpportunityStageDto>
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
                    "Error while getting opportunity stage by id");

                throw;
            }
        }

        #endregion
        #endregion
        #region Activity Type

        public async Task<ApiResponse<string>> CreateActivityType(
            CrmActivityTypeDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.ActivityName))
                    throw new CustomException("Activity Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ActivityCode))
                    throw new CustomException("Activity Code is required.");

                if (string.IsNullOrWhiteSpace(dto.Category))
                    throw new CustomException("Category is required.");

                if (dto.DurationMinutes < 0)
                    throw new CustomException(
                        "Duration Minutes cannot be negative.");

                if (dto.ReminderBeforeMinutes < 0)
                    throw new CustomException(
                        "Reminder Before Minutes cannot be negative.");

                if (dto.ReminderBeforeMinutes > dto.DurationMinutes &&
                    dto.ReminderRequired)
                {
                    throw new CustomException(
                        "Reminder Before Minutes cannot be greater than Duration Minutes.");
                }

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationActivityType>()
                    .FindAsync(x =>
                        !x.IsDeleted &&
                        (x.ActivityName.ToLower() ==
                         dto.ActivityName.Trim().ToLower()
                         ||
                         x.ActivityCode.ToLower() ==
                         dto.ActivityCode.Trim().ToLower()));

                if (duplicate.Any())
                    throw new CustomException(
                        "Activity Name or Activity Code already exists.");

                var entity = new CrmmoduleConfigurationActivityType
                {
                    ActivityName = dto.ActivityName.Trim(),
                    ActivityCode = dto.ActivityCode.Trim(),
                    Category = dto.Category.Trim(),
                    DurationMinutes = dto.DurationMinutes,
                    Description = dto.Description,
                    ReminderBeforeMinutes =
                        dto.ReminderBeforeMinutes,
                    Status = dto.Status,
                    ReminderRequired = dto.ReminderRequired,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                await _unitOfWork
                    .Repository<CrmmoduleConfigurationActivityType>()
                    .AddAsync(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "ActivityType",
                    "INSERT",
                    entity.ActivityTypeId,
                    "",
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Activity Type Created Successfully",
                    Data = entity.ActivityName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating activity type");
                throw;
            }
        }


        public async Task<ApiResponse<string>> UpdateActivityType(
            CrmActivityTypeDto dto)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationActivityType>()
                    .FindAsync(x =>
                        x.ActivityTypeId == dto.ActivityTypeId &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException(
                        "Activity Type not found.");

                if (string.IsNullOrWhiteSpace(dto.ActivityName))
                    throw new CustomException("Activity Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ActivityCode))
                    throw new CustomException("Activity Code is required.");

                if (dto.DurationMinutes < 0 ||
                    dto.ReminderBeforeMinutes < 0)
                    throw new CustomException(
                        "Duration and Reminder Before Minutes cannot be negative.");

                if (dto.ReminderRequired &&
                    dto.ReminderBeforeMinutes > dto.DurationMinutes)
                    throw new CustomException(
                        "Reminder Before Minutes cannot be greater than Duration Minutes.");

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationActivityType>()
                    .FindAsync(x =>
                        x.ActivityTypeId != dto.ActivityTypeId &&
                        !x.IsDeleted &&
                        (x.ActivityName.ToLower() ==
                         dto.ActivityName.Trim().ToLower()
                         ||
                         x.ActivityCode.ToLower() ==
                         dto.ActivityCode.Trim().ToLower()));

                if (duplicate.Any())
                    throw new CustomException(
                        "Activity Name or Activity Code already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.ActivityName = dto.ActivityName.Trim();
                entity.ActivityCode = dto.ActivityCode.Trim();
                entity.Category = dto.Category.Trim();
                entity.DurationMinutes = dto.DurationMinutes;
                entity.Description = dto.Description;
                entity.ReminderBeforeMinutes =
                    dto.ReminderBeforeMinutes;
                entity.Status = dto.Status;
                entity.ReminderRequired = dto.ReminderRequired;
                entity.ModifiedBy = _currentUserService.UserId;
                entity.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationActivityType>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "ActivityType",
                    "UPDATE",
                    entity.ActivityTypeId,
                    oldValues,
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Activity Type Updated Successfully",
                    Data = entity.ActivityName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating activity type");
                throw;
            }
        }


        public async Task<ApiResponse<string>> DeleteActivityType(int id)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationActivityType>()
                    .FindAsync(x =>
                        x.ActivityTypeId == id &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException(
                        "Activity Type not found.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.IsDeleted = true;
                entity.ModifiedBy = _currentUserService.UserId;
                entity.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationActivityType>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "ActivityType",
                    "DELETE",
                    entity.ActivityTypeId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Activity Type Deleted Successfully",
                    Data = entity.ActivityName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting activity type");
                throw;
            }
        }


        public async Task<ApiResponse<List<CrmActivityTypeDto>>>
            GetActivityTypes()
        {
            var entities = (await _unitOfWork
                .Repository<CrmmoduleConfigurationActivityType>()
                .FindAsync(x => !x.IsDeleted))
                .OrderByDescending(x => x.ActivityTypeId)
                .ToList();

            var result = entities.Select(x =>
                new CrmActivityTypeDto
                {
                    ActivityTypeId = x.ActivityTypeId,
                    ActivityName = x.ActivityName,
                    ActivityCode = x.ActivityCode,
                    Category = x.Category,
                    DurationMinutes = x.DurationMinutes,
                    Description = x.Description,
                    ReminderBeforeMinutes =
                        x.ReminderBeforeMinutes,
                    Status = x.Status,
                    ReminderRequired = x.ReminderRequired
                }).ToList();

            return new ApiResponse<List<CrmActivityTypeDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        public async Task<ApiResponse<CrmActivityTypeDto>>
            GetActivityTypeById(int id)
        {
            var entity = (await _unitOfWork
                .Repository<CrmmoduleConfigurationActivityType>()
                .FindAsync(x =>
                    x.ActivityTypeId == id &&
                    !x.IsDeleted))
                .FirstOrDefault();

            if (entity == null)
                throw new CustomException(
                    "Activity Type not found.");

            return new ApiResponse<CrmActivityTypeDto>
            {
                Success = true,
                Message = "Success",
                Data = new CrmActivityTypeDto
                {
                    ActivityTypeId = entity.ActivityTypeId,
                    ActivityName = entity.ActivityName,
                    ActivityCode = entity.ActivityCode,
                    Category = entity.Category,
                    DurationMinutes = entity.DurationMinutes,
                    Description = entity.Description,
                    ReminderBeforeMinutes =
                        entity.ReminderBeforeMinutes,
                    Status = entity.Status,
                    ReminderRequired = entity.ReminderRequired
                }
            };
        }

        #endregion
        #region Source

        public async Task<ApiResponse<string>> CreateSource(
            CrmSourceDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.SourceName))
                    throw new CustomException("Source Name is required.");

                if (string.IsNullOrWhiteSpace(dto.SourceCode))
                    throw new CustomException("Source Code is required.");

                if (string.IsNullOrWhiteSpace(dto.Category))
                    throw new CustomException("Category is required.");

                if (dto.ConversionRate < 0 ||
                    dto.ConversionRate > 100)
                    throw new CustomException(
                        "Conversion Rate must be between 0 and 100.");

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationSource>()
                    .FindAsync(x =>
                        !x.IsDeleted &&
                        (x.SourceName.ToLower() ==
                         dto.SourceName.Trim().ToLower()
                         ||
                         x.SourceCode.ToLower() ==
                         dto.SourceCode.Trim().ToLower()));

                if (duplicate.Any())
                    throw new CustomException(
                        "Source Name or Source Code already exists.");

                var entity = new CrmmoduleConfigurationSource
                {
                    SourceName = dto.SourceName.Trim(),
                    SourceCode = dto.SourceCode.Trim(),
                    Category = dto.Category.Trim(),
                    ConversionRate = dto.ConversionRate,
                    Priority = dto.Priority.Trim(),
                    Status = dto.Status,
                    Description = dto.Description,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                await _unitOfWork
                    .Repository<CrmmoduleConfigurationSource>()
                    .AddAsync(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Source",
                    "INSERT",
                    entity.SourceId,
                    "",
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Source Created Successfully",
                    Data = entity.SourceName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating source");
                throw;
            }
        }


        public async Task<ApiResponse<string>> UpdateSource(
            CrmSourceDto dto)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationSource>()
                    .FindAsync(x =>
                        x.SourceId == dto.SourceId &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException("Source not found.");

                if (dto.ConversionRate < 0 ||
                    dto.ConversionRate > 100)
                    throw new CustomException(
                        "Conversion Rate must be between 0 and 100.");

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationSource>()
                    .FindAsync(x =>
                        x.SourceId != dto.SourceId &&
                        !x.IsDeleted &&
                        (x.SourceName.ToLower() ==
                         dto.SourceName.Trim().ToLower()
                         ||
                         x.SourceCode.ToLower() ==
                         dto.SourceCode.Trim().ToLower()));

                if (duplicate.Any())
                    throw new CustomException(
                        "Source Name or Source Code already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.SourceName = dto.SourceName.Trim();
                entity.SourceCode = dto.SourceCode.Trim();
                entity.Category = dto.Category.Trim();
                entity.ConversionRate = dto.ConversionRate;
                entity.Priority = dto.Priority.Trim();
                entity.Status = dto.Status;
                entity.Description = dto.Description;
                entity.ModifiedBy = _currentUserService.UserId;
                entity.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationSource>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Source",
                    "UPDATE",
                    entity.SourceId,
                    oldValues,
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Source Updated Successfully",
                    Data = entity.SourceName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating source");
                throw;
            }
        }


        public async Task<ApiResponse<string>> DeleteSource(int id)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationSource>()
                    .FindAsync(x =>
                        x.SourceId == id &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException("Source not found.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.IsDeleted = true;
                entity.ModifiedBy = _currentUserService.UserId;
                entity.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationSource>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Source",
                    "DELETE",
                    entity.SourceId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Source Deleted Successfully",
                    Data = entity.SourceName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting source");
                throw;
            }
        }


        public async Task<ApiResponse<List<CrmSourceDto>>> GetSources()
        {
            var entities = (await _unitOfWork
                .Repository<CrmmoduleConfigurationSource>()
                .FindAsync(x => !x.IsDeleted))
                .OrderByDescending(x => x.SourceId)
                .ToList();

            var result = entities.Select(x =>
                new CrmSourceDto
                {
                    SourceId = x.SourceId,
                    SourceName = x.SourceName,
                    SourceCode = x.SourceCode,
                    Category = x.Category,
                    ConversionRate = x.ConversionRate,
                    Priority = x.Priority,
                    Status = x.Status,
                    Description = x.Description
                }).ToList();

            return new ApiResponse<List<CrmSourceDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        public async Task<ApiResponse<CrmSourceDto>> GetSourceById(int id)
        {
            var entity = (await _unitOfWork
                .Repository<CrmmoduleConfigurationSource>()
                .FindAsync(x =>
                    x.SourceId == id &&
                    !x.IsDeleted))
                .FirstOrDefault();

            if (entity == null)
                throw new CustomException("Source not found.");

            return new ApiResponse<CrmSourceDto>
            {
                Success = true,
                Message = "Success",
                Data = new CrmSourceDto
                {
                    SourceId = entity.SourceId,
                    SourceName = entity.SourceName,
                    SourceCode = entity.SourceCode,
                    Category = entity.Category,
                    ConversionRate = entity.ConversionRate,
                    Priority = entity.Priority,
                    Status = entity.Status,
                    Description = entity.Description
                }
            };
        }

        #endregion
        #region Industry

        public async Task<ApiResponse<string>> CreateIndustry(
            CrmIndustryDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.IndustryName))
                    throw new CustomException("Industry Name is required.");

                if (string.IsNullOrWhiteSpace(dto.IndustryCode))
                    throw new CustomException("Industry Code is required.");

                if (string.IsNullOrWhiteSpace(dto.IndustryCategory))
                    throw new CustomException(
                        "Industry Category is required.");

                if (dto.CustomerCount < 0)
                    throw new CustomException(
                        "Customer Count cannot be negative.");

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationIndustry>()
                    .FindAsync(x =>
                        !x.IsDeleted &&
                        (x.IndustryName.ToLower() ==
                         dto.IndustryName.Trim().ToLower()
                         ||
                         x.IndustryCode.ToLower() ==
                         dto.IndustryCode.Trim().ToLower()));

                if (duplicate.Any())
                    throw new CustomException(
                        "Industry Name or Industry Code already exists.");

                var entity = new CrmmoduleConfigurationIndustry
                {
                    IndustryName = dto.IndustryName.Trim(),
                    IndustryCode = dto.IndustryCode.Trim(),
                    IndustryCategory =
                        dto.IndustryCategory.Trim(),
                    CustomerCount = dto.CustomerCount,
                    Priority = dto.Priority.Trim(),
                    Status = dto.Status,
                    Description = dto.Description,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                await _unitOfWork
                    .Repository<CrmmoduleConfigurationIndustry>()
                    .AddAsync(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Industry",
                    "INSERT",
                    entity.IndustryId,
                    "",
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Industry Created Successfully",
                    Data = entity.IndustryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating industry");
                throw;
            }
        }


        public async Task<ApiResponse<string>> UpdateIndustry(
            CrmIndustryDto dto)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationIndustry>()
                    .FindAsync(x =>
                        x.IndustryId == dto.IndustryId &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException("Industry not found.");

                if (dto.CustomerCount < 0)
                    throw new CustomException(
                        "Customer Count cannot be negative.");

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationIndustry>()
                    .FindAsync(x =>
                        x.IndustryId != dto.IndustryId &&
                        !x.IsDeleted &&
                        (x.IndustryName.ToLower() ==
                         dto.IndustryName.Trim().ToLower()
                         ||
                         x.IndustryCode.ToLower() ==
                         dto.IndustryCode.Trim().ToLower()));

                if (duplicate.Any())
                    throw new CustomException(
                        "Industry Name or Industry Code already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.IndustryName = dto.IndustryName.Trim();
                entity.IndustryCode = dto.IndustryCode.Trim();
                entity.IndustryCategory =
                    dto.IndustryCategory.Trim();
                entity.CustomerCount = dto.CustomerCount;
                entity.Priority = dto.Priority.Trim();
                entity.Status = dto.Status;
                entity.Description = dto.Description;
                entity.ModifiedBy = _currentUserService.UserId;
                entity.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationIndustry>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Industry",
                    "UPDATE",
                    entity.IndustryId,
                    oldValues,
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Industry Updated Successfully",
                    Data = entity.IndustryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating industry");
                throw;
            }
        }


        public async Task<ApiResponse<string>> DeleteIndustry(int id)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationIndustry>()
                    .FindAsync(x =>
                        x.IndustryId == id &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException("Industry not found.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.IsDeleted = true;
                entity.ModifiedBy = _currentUserService.UserId;
                entity.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationIndustry>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Industry",
                    "DELETE",
                    entity.IndustryId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Industry Deleted Successfully",
                    Data = entity.IndustryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting industry");
                throw;
            }
        }


        public async Task<ApiResponse<List<CrmIndustryDto>>>
            GetIndustries()
        {
            var entities = (await _unitOfWork
                .Repository<CrmmoduleConfigurationIndustry>()
                .FindAsync(x => !x.IsDeleted))
                .OrderByDescending(x => x.IndustryId)
                .ToList();

            var result = entities.Select(x =>
                new CrmIndustryDto
                {
                    IndustryId = x.IndustryId,
                    IndustryName = x.IndustryName,
                    IndustryCode = x.IndustryCode,
                    IndustryCategory = x.IndustryCategory,
                    CustomerCount = x.CustomerCount,
                    Priority = x.Priority,
                    Status = x.Status,
                    Description = x.Description
                }).ToList();

            return new ApiResponse<List<CrmIndustryDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        public async Task<ApiResponse<CrmIndustryDto>>
            GetIndustryById(int id)
        {
            var entity = (await _unitOfWork
                .Repository<CrmmoduleConfigurationIndustry>()
                .FindAsync(x =>
                    x.IndustryId == id &&
                    !x.IsDeleted))
                .FirstOrDefault();

            if (entity == null)
                throw new CustomException("Industry not found.");

            return new ApiResponse<CrmIndustryDto>
            {
                Success = true,
                Message = "Success",
                Data = new CrmIndustryDto
                {
                    IndustryId = entity.IndustryId,
                    IndustryName = entity.IndustryName,
                    IndustryCode = entity.IndustryCode,
                    IndustryCategory = entity.IndustryCategory,
                    CustomerCount = entity.CustomerCount,
                    Priority = entity.Priority,
                    Status = entity.Status,
                    Description = entity.Description
                }
            };
        }

        #endregion
        #region Territory

        public async Task<ApiResponse<string>> CreateTerritory(
            CrmTerritoryDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.TerritoryName))
                    throw new CustomException(
                        "Territory Name is required.");

                if (string.IsNullOrWhiteSpace(dto.TerritoryCode))
                    throw new CustomException(
                        "Territory Code is required.");

                if (string.IsNullOrWhiteSpace(dto.Region))
                    throw new CustomException("Region is required.");

                if (dto.CustomerCount < 0)
                    throw new CustomException(
                        "Customer Count cannot be negative.");

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationTerritory>()
                    .FindAsync(x =>
                        !x.IsDeleted &&
                        (x.TerritoryName.ToLower() ==
                         dto.TerritoryName.Trim().ToLower()
                         ||
                         x.TerritoryCode.ToLower() ==
                         dto.TerritoryCode.Trim().ToLower()));

                if (duplicate.Any())
                    throw new CustomException(
                        "Territory Name or Territory Code already exists.");

                var entity = new CrmmoduleConfigurationTerritory
                {
                    TerritoryName = dto.TerritoryName.Trim(),
                    TerritoryCode = dto.TerritoryCode.Trim(),
                    Region = dto.Region.Trim(),
                    TerritoryManager =
                        dto.TerritoryManager,
                    CustomerCount = dto.CustomerCount,
                    Priority = dto.Priority.Trim(),
                    Status = dto.Status,
                    Description = dto.Description,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                await _unitOfWork
                    .Repository<CrmmoduleConfigurationTerritory>()
                    .AddAsync(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Territory",
                    "INSERT",
                    entity.TerritoryId,
                    "",
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Territory Created Successfully",
                    Data = entity.TerritoryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating territory");
                throw;
            }
        }


        public async Task<ApiResponse<string>> UpdateTerritory(
            CrmTerritoryDto dto)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationTerritory>()
                    .FindAsync(x =>
                        x.TerritoryId == dto.TerritoryId &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException(
                        "Territory not found.");

                if (dto.CustomerCount < 0)
                    throw new CustomException(
                        "Customer Count cannot be negative.");

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationTerritory>()
                    .FindAsync(x =>
                        x.TerritoryId != dto.TerritoryId &&
                        !x.IsDeleted &&
                        (x.TerritoryName.ToLower() ==
                         dto.TerritoryName.Trim().ToLower()
                         ||
                         x.TerritoryCode.ToLower() ==
                         dto.TerritoryCode.Trim().ToLower()));

                if (duplicate.Any())
                    throw new CustomException(
                        "Territory Name or Territory Code already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.TerritoryName =
                    dto.TerritoryName.Trim();

                entity.TerritoryCode =
                    dto.TerritoryCode.Trim();

                entity.Region =
                    dto.Region.Trim();

                entity.TerritoryManager =
                    dto.TerritoryManager;

                entity.CustomerCount =
                    dto.CustomerCount;

                entity.Priority =
                    dto.Priority.Trim();

                entity.Status =
                    dto.Status;

                entity.Description =
                    dto.Description;

                entity.ModifiedBy =
                    _currentUserService.UserId;

                entity.ModifiedDate =
                    DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationTerritory>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Territory",
                    "UPDATE",
                    entity.TerritoryId,
                    oldValues,
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Territory Updated Successfully",
                    Data = entity.TerritoryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating territory");
                throw;
            }
        }


        public async Task<ApiResponse<string>> DeleteTerritory(int id)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationTerritory>()
                    .FindAsync(x =>
                        x.TerritoryId == id &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException(
                        "Territory not found.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.IsDeleted = true;
                entity.ModifiedBy = _currentUserService.UserId;
                entity.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationTerritory>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Territory",
                    "DELETE",
                    entity.TerritoryId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Territory Deleted Successfully",
                    Data = entity.TerritoryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting territory");
                throw;
            }
        }


        public async Task<ApiResponse<List<CrmTerritoryDto>>>
            GetTerritories()
        {
            var entities = (await _unitOfWork
                .Repository<CrmmoduleConfigurationTerritory>()
                .FindAsync(x => !x.IsDeleted))
                .OrderByDescending(x => x.TerritoryId)
                .ToList();

            var result = entities.Select(x =>
                new CrmTerritoryDto
                {
                    TerritoryId = x.TerritoryId,
                    TerritoryName = x.TerritoryName,
                    TerritoryCode = x.TerritoryCode,
                    Region = x.Region,
                    TerritoryManager = x.TerritoryManager,
                    CustomerCount = x.CustomerCount,
                    Priority = x.Priority,
                    Status = x.Status,
                    Description = x.Description
                }).ToList();

            return new ApiResponse<List<CrmTerritoryDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        public async Task<ApiResponse<CrmTerritoryDto>>
            GetTerritoryById(int id)
        {
            var entity = (await _unitOfWork
                .Repository<CrmmoduleConfigurationTerritory>()
                .FindAsync(x =>
                    x.TerritoryId == id &&
                    !x.IsDeleted))
                .FirstOrDefault();

            if (entity == null)
                throw new CustomException(
                    "Territory not found.");

            return new ApiResponse<CrmTerritoryDto>
            {
                Success = true,
                Message = "Success",
                Data = new CrmTerritoryDto
                {
                    TerritoryId = entity.TerritoryId,
                    TerritoryName = entity.TerritoryName,
                    TerritoryCode = entity.TerritoryCode,
                    Region = entity.Region,
                    TerritoryManager = entity.TerritoryManager,
                    CustomerCount = entity.CustomerCount,
                    Priority = entity.Priority,
                    Status = entity.Status,
                    Description = entity.Description
                }
            };
        }

        #endregion
        #region Sales Target

        public async Task<ApiResponse<string>> CreateSalesTarget(
            CrmSalesTargetDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.TargetName))
                    throw new CustomException("Target Name is required.");

                if (string.IsNullOrWhiteSpace(dto.TargetCode))
                    throw new CustomException("Target Code is required.");

                if (string.IsNullOrWhiteSpace(dto.EmployeeName))
                    throw new CustomException("Employee Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Department))
                    throw new CustomException("Department is required.");

                if (string.IsNullOrWhiteSpace(dto.TargetPeriod))
                    throw new CustomException("Target Period is required.");

                if (dto.TargetAmount < 0)
                    throw new CustomException(
                        "Target Amount cannot be negative.");

                if (dto.AchievedAmount < 0)
                    throw new CustomException(
                        "Achieved Amount cannot be negative.");

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationSalesTarget>()
                    .FindAsync(x =>
                        !x.IsDeleted &&
                        (x.TargetName.ToLower() ==
                         dto.TargetName.Trim().ToLower()
                         ||
                         x.TargetCode.ToLower() ==
                         dto.TargetCode.Trim().ToLower()));

                if (duplicate.Any())
                    throw new CustomException(
                        "Target Name or Target Code already exists.");

                var entity = new CrmmoduleConfigurationSalesTarget
                {
                    TargetName = dto.TargetName.Trim(),
                    TargetCode = dto.TargetCode.Trim(),
                    EmployeeName = dto.EmployeeName.Trim(),
                    Department = dto.Department.Trim(),
                    TargetAmount = dto.TargetAmount,
                    AchievedAmount = dto.AchievedAmount,
                    TargetPeriod = dto.TargetPeriod.Trim(),
                    Status = dto.Status,
                    Description = dto.Description,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                await _unitOfWork
                    .Repository<CrmmoduleConfigurationSalesTarget>()
                    .AddAsync(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "SalesTarget",
                    "INSERT",
                    entity.SalesTargetId,
                    "",
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Sales Target Created Successfully",
                    Data = entity.TargetName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating sales target");
                throw;
            }
        }


        public async Task<ApiResponse<string>> UpdateSalesTarget(
            CrmSalesTargetDto dto)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationSalesTarget>()
                    .FindAsync(x =>
                        x.SalesTargetId == dto.SalesTargetId &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException(
                        "Sales Target not found.");

                if (dto.TargetAmount < 0 ||
                    dto.AchievedAmount < 0)
                    throw new CustomException(
                        "Target Amount and Achieved Amount cannot be negative.");

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationSalesTarget>()
                    .FindAsync(x =>
                        x.SalesTargetId != dto.SalesTargetId &&
                        !x.IsDeleted &&
                        (x.TargetName.ToLower() ==
                         dto.TargetName.Trim().ToLower()
                         ||
                         x.TargetCode.ToLower() ==
                         dto.TargetCode.Trim().ToLower()));

                if (duplicate.Any())
                    throw new CustomException(
                        "Target Name or Target Code already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.TargetName = dto.TargetName.Trim();
                entity.TargetCode = dto.TargetCode.Trim();
                entity.EmployeeName = dto.EmployeeName.Trim();
                entity.Department = dto.Department.Trim();
                entity.TargetAmount = dto.TargetAmount;
                entity.AchievedAmount = dto.AchievedAmount;
                entity.TargetPeriod = dto.TargetPeriod.Trim();
                entity.Status = dto.Status;
                entity.Description = dto.Description;
                entity.ModifiedBy = _currentUserService.UserId;
                entity.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationSalesTarget>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "SalesTarget",
                    "UPDATE",
                    entity.SalesTargetId,
                    oldValues,
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Sales Target Updated Successfully",
                    Data = entity.TargetName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating sales target");
                throw;
            }
        }


        public async Task<ApiResponse<string>> DeleteSalesTarget(int id)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationSalesTarget>()
                    .FindAsync(x =>
                        x.SalesTargetId == id &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException(
                        "Sales Target not found.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.IsDeleted = true;
                entity.ModifiedBy = _currentUserService.UserId;
                entity.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationSalesTarget>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "SalesTarget",
                    "DELETE",
                    entity.SalesTargetId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Sales Target Deleted Successfully",
                    Data = entity.TargetName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting sales target");
                throw;
            }
        }


        public async Task<ApiResponse<List<CrmSalesTargetDto>>>
            GetSalesTargets()
        {
            var entities = (await _unitOfWork
                .Repository<CrmmoduleConfigurationSalesTarget>()
                .FindAsync(x => !x.IsDeleted))
                .OrderByDescending(x => x.SalesTargetId)
                .ToList();

            var result = entities.Select(x =>
                new CrmSalesTargetDto
                {
                    SalesTargetId = x.SalesTargetId,
                    TargetName = x.TargetName,
                    TargetCode = x.TargetCode,
                    EmployeeName = x.EmployeeName,
                    Department = x.Department,
                    TargetAmount = x.TargetAmount,
                    AchievedAmount = x.AchievedAmount,
                    TargetPeriod = x.TargetPeriod,
                    Status = x.Status,
                    Description = x.Description
                }).ToList();

            return new ApiResponse<List<CrmSalesTargetDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        public async Task<ApiResponse<CrmSalesTargetDto>>
            GetSalesTargetById(int id)
        {
            var entity = (await _unitOfWork
                .Repository<CrmmoduleConfigurationSalesTarget>()
                .FindAsync(x =>
                    x.SalesTargetId == id &&
                    !x.IsDeleted))
                .FirstOrDefault();

            if (entity == null)
                throw new CustomException(
                    "Sales Target not found.");

            return new ApiResponse<CrmSalesTargetDto>
            {
                Success = true,
                Message = "Success",
                Data = new CrmSalesTargetDto
                {
                    SalesTargetId = entity.SalesTargetId,
                    TargetName = entity.TargetName,
                    TargetCode = entity.TargetCode,
                    EmployeeName = entity.EmployeeName,
                    Department = entity.Department,
                    TargetAmount = entity.TargetAmount,
                    AchievedAmount = entity.AchievedAmount,
                    TargetPeriod = entity.TargetPeriod,
                    Status = entity.Status,
                    Description = entity.Description
                }
            };
        }

        #endregion
        #region Number Series

        public async Task<ApiResponse<string>> CreateNumberSeries(
            CrmNumberSeriesDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.SeriesName))
                    throw new CustomException(
                        "Series Name is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (dto.StartingNumber < 0)
                    throw new CustomException(
                        "Starting Number cannot be negative.");

                if (dto.CurrentNumber < dto.StartingNumber)
                    throw new CustomException(
                        "Current Number cannot be less than Starting Number.");

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationNumberSeries>()
                    .FindAsync(x =>
                        !x.IsDeleted &&
                        (x.SeriesName.ToLower() ==
                         dto.SeriesName.Trim().ToLower()
                         ||
                         (x.ModuleName.ToLower() ==
                          dto.ModuleName.Trim().ToLower()
                          &&
                          (x.Prefix ?? "").ToLower() ==
                          (dto.Prefix ?? "").Trim().ToLower())));

                if (duplicate.Any())
                    throw new CustomException(
                        "Number Series already exists.");

                var entity =
                    new CrmmoduleConfigurationNumberSeries
                    {
                        SeriesName = dto.SeriesName.Trim(),
                        ModuleName = dto.ModuleName.Trim(),
                        Prefix = dto.Prefix?.Trim(),
                        StartingNumber = dto.StartingNumber,
                        CurrentNumber = dto.CurrentNumber,
                        NumberFormat = dto.NumberFormat,
                        Status = dto.Status,
                        Description = dto.Description,
                        CreatedBy = _currentUserService.UserId,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    };

                await _unitOfWork
                    .Repository<CrmmoduleConfigurationNumberSeries>()
                    .AddAsync(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "NumberSeries",
                    "INSERT",
                    entity.NumberSeriesId,
                    "",
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Number Series Created Successfully",
                    Data = entity.SeriesName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating number series");
                throw;
            }
        }


        public async Task<ApiResponse<string>> UpdateNumberSeries(
            CrmNumberSeriesDto dto)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationNumberSeries>()
                    .FindAsync(x =>
                        x.NumberSeriesId ==
                        dto.NumberSeriesId &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException(
                        "Number Series not found.");

                if (dto.StartingNumber < 0)
                    throw new CustomException(
                        "Starting Number cannot be negative.");

                if (dto.CurrentNumber < dto.StartingNumber)
                    throw new CustomException(
                        "Current Number cannot be less than Starting Number.");

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationNumberSeries>()
                    .FindAsync(x =>
                        x.NumberSeriesId !=
                        dto.NumberSeriesId &&
                        !x.IsDeleted &&
                        (x.SeriesName.ToLower() ==
                         dto.SeriesName.Trim().ToLower()
                         ||
                         (x.ModuleName.ToLower() ==
                          dto.ModuleName.Trim().ToLower()
                          &&
                          (x.Prefix ?? "").ToLower() ==
                          (dto.Prefix ?? "").Trim().ToLower())));

                if (duplicate.Any())
                    throw new CustomException(
                        "Number Series already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.SeriesName = dto.SeriesName.Trim();
                entity.ModuleName = dto.ModuleName.Trim();
                entity.Prefix = dto.Prefix?.Trim();
                entity.StartingNumber = dto.StartingNumber;
                entity.CurrentNumber = dto.CurrentNumber;
                entity.NumberFormat = dto.NumberFormat;
                entity.Status = dto.Status;
                entity.Description = dto.Description;
                entity.ModifiedBy = _currentUserService.UserId;
                entity.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationNumberSeries>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "NumberSeries",
                    "UPDATE",
                    entity.NumberSeriesId,
                    oldValues,
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Number Series Updated Successfully",
                    Data = entity.SeriesName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating number series");
                throw;
            }
        }


        public async Task<ApiResponse<string>> DeleteNumberSeries(int id)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationNumberSeries>()
                    .FindAsync(x =>
                        x.NumberSeriesId == id &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException(
                        "Number Series not found.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.IsDeleted = true;
                entity.ModifiedBy = _currentUserService.UserId;
                entity.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationNumberSeries>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "NumberSeries",
                    "DELETE",
                    entity.NumberSeriesId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Number Series Deleted Successfully",
                    Data = entity.SeriesName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting number series");
                throw;
            }
        }


        public async Task<ApiResponse<List<CrmNumberSeriesDto>>>
            GetNumberSeries()
        {
            var entities = (await _unitOfWork
                .Repository<CrmmoduleConfigurationNumberSeries>()
                .FindAsync(x => !x.IsDeleted))
                .OrderByDescending(x => x.NumberSeriesId)
                .ToList();

            var result = entities.Select(x =>
                new CrmNumberSeriesDto
                {
                    NumberSeriesId = x.NumberSeriesId,
                    SeriesName = x.SeriesName,
                    ModuleName = x.ModuleName,
                    Prefix = x.Prefix,
                    StartingNumber = x.StartingNumber,
                    CurrentNumber = x.CurrentNumber,
                    NumberFormat = x.NumberFormat,
                    Status = x.Status,
                    Description = x.Description
                }).ToList();

            return new ApiResponse<List<CrmNumberSeriesDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        public async Task<ApiResponse<CrmNumberSeriesDto>>
            GetNumberSeriesById(int id)
        {
            var entity = (await _unitOfWork
                .Repository<CrmmoduleConfigurationNumberSeries>()
                .FindAsync(x =>
                    x.NumberSeriesId == id &&
                    !x.IsDeleted))
                .FirstOrDefault();

            if (entity == null)
                throw new CustomException(
                    "Number Series not found.");

            return new ApiResponse<CrmNumberSeriesDto>
            {
                Success = true,
                Message = "Success",
                Data = new CrmNumberSeriesDto
                {
                    NumberSeriesId = entity.NumberSeriesId,
                    SeriesName = entity.SeriesName,
                    ModuleName = entity.ModuleName,
                    Prefix = entity.Prefix,
                    StartingNumber = entity.StartingNumber,
                    CurrentNumber = entity.CurrentNumber,
                    NumberFormat = entity.NumberFormat,
                    Status = entity.Status,
                    Description = entity.Description
                }
            };
        }

        #endregion
        #region Custom Field

        public async Task<ApiResponse<string>> CreateCustomField(
            CrmCustomFieldDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.FieldName))
                    throw new CustomException(
                        "Field Name is required.");

                if (string.IsNullOrWhiteSpace(dto.DisplayLabel))
                    throw new CustomException(
                        "Display Label is required.");

                if (string.IsNullOrWhiteSpace(dto.ModuleName))
                    throw new CustomException(
                        "Module Name is required.");

                if (string.IsNullOrWhiteSpace(dto.FieldType))
                    throw new CustomException(
                        "Field Type is required.");

                if (dto.FieldOrder <= 0)
                    throw new CustomException(
                        "Field Order must be greater than 0.");

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationCustomField>()
                    .FindAsync(x =>
                        !x.IsDeleted &&
                        x.ModuleName.ToLower() ==
                        dto.ModuleName.Trim().ToLower() &&
                        (x.FieldName.ToLower() ==
                         dto.FieldName.Trim().ToLower()
                         ||
                         x.DisplayLabel.ToLower() ==
                         dto.DisplayLabel.Trim().ToLower()));

                if (duplicate.Any())
                    throw new CustomException(
                        "Field Name or Display Label already exists for this module.");

                var duplicateOrder = await _unitOfWork
                    .Repository<CrmmoduleConfigurationCustomField>()
                    .FindAsync(x =>
                        !x.IsDeleted &&
                        x.ModuleName.ToLower() ==
                        dto.ModuleName.Trim().ToLower() &&
                        x.FieldOrder == dto.FieldOrder);

                if (duplicateOrder.Any())
                    throw new CustomException(
                        "Field Order already exists for this module.");

                var entity =
                    new CrmmoduleConfigurationCustomField
                    {
                        FieldName = dto.FieldName.Trim(),
                        DisplayLabel = dto.DisplayLabel.Trim(),
                        ModuleName = dto.ModuleName.Trim(),
                        FieldType = dto.FieldType.Trim(),
                        DefaultValue = dto.DefaultValue,
                        Placeholder = dto.Placeholder,
                        Status = dto.Status,
                        FieldOrder = dto.FieldOrder,
                        Description = dto.Description,
                        RequiredField = dto.RequiredField,
                        UniqueField = dto.UniqueField,
                        CreatedBy = _currentUserService.UserId,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    };

                await _unitOfWork
                    .Repository<CrmmoduleConfigurationCustomField>()
                    .AddAsync(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CustomField",
                    "INSERT",
                    entity.CustomFieldId,
                    "",
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Custom Field Created Successfully",
                    Data = entity.FieldName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating custom field");
                throw;
            }
        }


        public async Task<ApiResponse<string>> UpdateCustomField(
            CrmCustomFieldDto dto)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationCustomField>()
                    .FindAsync(x =>
                        x.CustomFieldId ==
                        dto.CustomFieldId &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException(
                        "Custom Field not found.");

                if (dto.FieldOrder <= 0)
                    throw new CustomException(
                        "Field Order must be greater than 0.");

                var duplicate = await _unitOfWork
                    .Repository<CrmmoduleConfigurationCustomField>()
                    .FindAsync(x =>
                        x.CustomFieldId !=
                        dto.CustomFieldId &&
                        !x.IsDeleted &&
                        x.ModuleName.ToLower() ==
                        dto.ModuleName.Trim().ToLower() &&
                        (x.FieldName.ToLower() ==
                         dto.FieldName.Trim().ToLower()
                         ||
                         x.DisplayLabel.ToLower() ==
                         dto.DisplayLabel.Trim().ToLower()));

                if (duplicate.Any())
                    throw new CustomException(
                        "Field Name or Display Label already exists for this module.");

                var duplicateOrder = await _unitOfWork
                    .Repository<CrmmoduleConfigurationCustomField>()
                    .FindAsync(x =>
                        x.CustomFieldId !=
                        dto.CustomFieldId &&
                        !x.IsDeleted &&
                        x.ModuleName.ToLower() ==
                        dto.ModuleName.Trim().ToLower() &&
                        x.FieldOrder == dto.FieldOrder);

                if (duplicateOrder.Any())
                    throw new CustomException(
                        "Field Order already exists for this module.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.FieldName = dto.FieldName.Trim();
                entity.DisplayLabel = dto.DisplayLabel.Trim();
                entity.ModuleName = dto.ModuleName.Trim();
                entity.FieldType = dto.FieldType.Trim();
                entity.DefaultValue = dto.DefaultValue;
                entity.Placeholder = dto.Placeholder;
                entity.Status = dto.Status;
                entity.FieldOrder = dto.FieldOrder;
                entity.Description = dto.Description;
                entity.RequiredField = dto.RequiredField;
                entity.UniqueField = dto.UniqueField;
                entity.ModifiedBy = _currentUserService.UserId;
                entity.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationCustomField>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CustomField",
                    "UPDATE",
                    entity.CustomFieldId,
                    oldValues,
                    JsonConvert.SerializeObject(entity),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Custom Field Updated Successfully",
                    Data = entity.FieldName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating custom field");
                throw;
            }
        }


        public async Task<ApiResponse<string>> DeleteCustomField(int id)
        {
            try
            {
                var entity = (await _unitOfWork
                    .Repository<CrmmoduleConfigurationCustomField>()
                    .FindAsync(x =>
                        x.CustomFieldId == id &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (entity == null)
                    throw new CustomException(
                        "Custom Field not found.");

                string oldValues =
                    JsonConvert.SerializeObject(entity);

                entity.IsDeleted = true;
                entity.ModifiedBy = _currentUserService.UserId;
                entity.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CrmmoduleConfigurationCustomField>()
                    .Update(entity);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CustomField",
                    "DELETE",
                    entity.CustomFieldId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Custom Field Deleted Successfully",
                    Data = entity.FieldName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting custom field");
                throw;
            }
        }


        public async Task<ApiResponse<List<CrmCustomFieldDto>>>
            GetCustomFields()
        {
            var entities = (await _unitOfWork
                .Repository<CrmmoduleConfigurationCustomField>()
                .FindAsync(x => !x.IsDeleted))
                .OrderBy(x => x.ModuleName)
                .ThenBy(x => x.FieldOrder)
                .ToList();

            var result = entities.Select(x =>
                new CrmCustomFieldDto
                {
                    CustomFieldId = x.CustomFieldId,
                    FieldName = x.FieldName,
                    DisplayLabel = x.DisplayLabel,
                    ModuleName = x.ModuleName,
                    FieldType = x.FieldType,
                    DefaultValue = x.DefaultValue,
                    Placeholder = x.Placeholder,
                    Status = x.Status,
                    FieldOrder = x.FieldOrder,
                    Description = x.Description,
                    RequiredField = x.RequiredField,
                    UniqueField = x.UniqueField
                }).ToList();

            return new ApiResponse<List<CrmCustomFieldDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        public async Task<ApiResponse<CrmCustomFieldDto>>
            GetCustomFieldById(int id)
        {
            var entity = (await _unitOfWork
                .Repository<CrmmoduleConfigurationCustomField>()
                .FindAsync(x =>
                    x.CustomFieldId == id &&
                    !x.IsDeleted))
                .FirstOrDefault();

            if (entity == null)
                throw new CustomException(
                    "Custom Field not found.");

            return new ApiResponse<CrmCustomFieldDto>
            {
                Success = true,
                Message = "Success",
                Data = new CrmCustomFieldDto
                {
                    CustomFieldId = entity.CustomFieldId,
                    FieldName = entity.FieldName,
                    DisplayLabel = entity.DisplayLabel,
                    ModuleName = entity.ModuleName,
                    FieldType = entity.FieldType,
                    DefaultValue = entity.DefaultValue,
                    Placeholder = entity.Placeholder,
                    Status = entity.Status,
                    FieldOrder = entity.FieldOrder,
                    Description = entity.Description,
                    RequiredField = entity.RequiredField,
                    UniqueField = entity.UniqueField
                }
            };
        }

        #endregion
    }
}
    
