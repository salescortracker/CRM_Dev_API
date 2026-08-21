using Business_Layer.DTOs.MasterDTO_s;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.MasterIInterface;
using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Newtonsoft.Json;
using Serilog;
using Shared.CommonModels;
using Shared.Exceptions;

namespace Business_Layer.Services.MasterServices
{
    public class ActivityTypeService : IActivityTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public ActivityTypeService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }
        public async Task<ApiResponse<string>> CreateActivityType(ActivityTypeDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.ActivityTypeName))
                    throw new CustomException("Activity Type Name is required.");

                var duplicate = await _unitOfWork.Repository<ActivityType>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.ActivityTypeName.ToLower() == dto.ActivityTypeName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Activity Type already exists.");

                ActivityType activityType = new ActivityType
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    ActivityTypeName = dto.ActivityTypeName,
                    ActivityTypeCode = dto.ActivityTypeCode,
                    Description = dto.Description,
                    IsActive = dto.IsActive,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<ActivityType>()
                    .AddAsync(activityType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "ActivityType",
                    "INSERT",
                    activityType.ActivityTypeId,
                    "",
                    JsonConvert.SerializeObject(activityType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Activity Type Created Successfully",
                    Data = activityType.ActivityTypeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating activity type");
                throw;
            }
        }
        public async Task<ApiResponse<string>> UpdateActivityType(ActivityTypeDto dto)
        {
            try
            {
                var activityType = (await _unitOfWork.Repository<ActivityType>()
                    .FindAsync(x => x.ActivityTypeId == dto.ActivityTypeId))
                    .FirstOrDefault();

                if (activityType == null)
                    throw new CustomException("Activity Type not found.");

                var duplicate = await _unitOfWork.Repository<ActivityType>()
                    .FindAsync(x =>
                        x.ActivityTypeId != dto.ActivityTypeId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.ActivityTypeName.ToLower() == dto.ActivityTypeName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Activity Type already exists.");

                string oldValues = JsonConvert.SerializeObject(activityType);

                activityType.CompanyId = dto.CompanyId;
                activityType.RegionId = dto.RegionId;
                activityType.ActivityTypeName = dto.ActivityTypeName;
                activityType.ActivityTypeCode = dto.ActivityTypeCode;
                activityType.Description = dto.Description;
                activityType.IsActive = dto.IsActive;
                activityType.ModifiedBy = _currentUserService.UserId;
                activityType.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<ActivityType>().Update(activityType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "ActivityType",
                    "UPDATE",
                    activityType.ActivityTypeId,
                    oldValues,
                    JsonConvert.SerializeObject(activityType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Activity Type Updated Successfully",
                    Data = activityType.ActivityTypeName
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
                var activityType = (await _unitOfWork.Repository<ActivityType>()
                    .FindAsync(x => x.ActivityTypeId == id))
                    .FirstOrDefault();

                if (activityType == null)
                    throw new CustomException("Activity Type not found.");

                string oldValues = JsonConvert.SerializeObject(activityType);

                activityType.IsDeleted = true;
                activityType.ModifiedBy = _currentUserService.UserId;
                activityType.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<ActivityType>().Update(activityType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "ActivityType",
                    "DELETE",
                    activityType.ActivityTypeId,
                    oldValues,
                    JsonConvert.SerializeObject(activityType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Activity Type Deleted Successfully",
                    Data = activityType.ActivityTypeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting activity type");
                throw;
            }
        }

        public async Task<ApiResponse<List<ActivityTypeDto>>> GetActivityTypes()
        {
            try
            {
                var activityTypes = await _unitOfWork.Repository<ActivityType>()
                    .GetAllAsync();

                var result = activityTypes
                    .Where(x => !x.IsDeleted)
                    .Select(x => new ActivityTypeDto
                    {
                        ActivityTypeId = x.ActivityTypeId,
                        CompanyId = x.CompanyId,
                        RegionId = x.RegionId,
                        ActivityTypeName = x.ActivityTypeName,
                        ActivityTypeCode = x.ActivityTypeCode,
                        Description = x.Description,
                        IsActive = x.IsActive
                    })
                    .ToList();

                return new ApiResponse<List<ActivityTypeDto>>
                {
                    Success = true,
                    Message = "Activity Types Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting activity types");
                throw;
            }
        }

        public async Task<ApiResponse<ActivityTypeDto>> GetActivityTypeById(int id)
        {
            try
            {
                var activityType = (await _unitOfWork.Repository<ActivityType>()
                    .FindAsync(x => x.ActivityTypeId == id && !x.IsDeleted))
                    .FirstOrDefault();

                if (activityType == null)
                    throw new CustomException("Activity Type not found.");

                var result = new ActivityTypeDto
                {
                    ActivityTypeId = activityType.ActivityTypeId,
                    CompanyId = activityType.CompanyId,
                    RegionId = activityType.RegionId,
                    ActivityTypeName = activityType.ActivityTypeName,
                    ActivityTypeCode = activityType.ActivityTypeCode,
                    Description = activityType.Description,
                    IsActive = activityType.IsActive
                };

                return new ApiResponse<ActivityTypeDto>
                {
                    Success = true,
                    Message = "Activity Type Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting activity type by id");
                throw;
            }
        }
    }
}