using Business_Layer.DTOs.Admin;
using Business_Layer.Interfaces.Adminsevices;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Newtonsoft.Json;
using Serilog;
using Shared.CommonModels;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business_Layer.Services.Adminservices
{
    public class BusinessHourService : IBusinessHourService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public BusinessHourService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        #region Business Hour

        #region CREATE

        public async Task<ApiResponse<string>> CreateBusinessHour(
            BusinessHourDto dto)
        {
            try
            {
                // Validate DTO
                if (dto == null)
                    throw new CustomException(
                        "Business Hour details are required.");

                // Validate Business Hours Name
                if (string.IsNullOrWhiteSpace(dto.BusinessHoursName))
                    throw new CustomException(
                        "Business Hours Name is required.");

                // Validate Working Days
                if (string.IsNullOrWhiteSpace(dto.WorkingDays))
                    throw new CustomException(
                        "Working Days are required.");

                // Validate Start and End Time
                if (dto.StartTime >= dto.EndTime)
                    throw new CustomException(
                        "Start Time must be earlier than End Time.");

                // Validate Break Time
                if (dto.BreakStart.HasValue &&
                    dto.BreakEnd.HasValue)
                {
                    if (dto.BreakStart.Value >= dto.BreakEnd.Value)
                        throw new CustomException(
                            "Break Start Time must be earlier than Break End Time.");

                    if (dto.BreakStart.Value < dto.StartTime ||
                        dto.BreakEnd.Value > dto.EndTime)
                    {
                        throw new CustomException(
                            "Break Time must be within Business Hours.");
                    }
                }

                // Validate Total Working Hours
                if (dto.TotalWorkingHours.HasValue &&
                    dto.TotalWorkingHours.Value < 0)
                {
                    throw new CustomException(
                        "Total Working Hours cannot be negative.");
                }

                // Validate Late Mark Grace Time
                if (dto.LateMarkGraceTimeMinutes.HasValue &&
                    dto.LateMarkGraceTimeMinutes.Value < 0)
                {
                    throw new CustomException(
                        "Late Mark Grace Time cannot be negative.");
                }

                // Validate Half Day Threshold
                if (dto.HalfDayThresholdHours.HasValue &&
                    dto.HalfDayThresholdHours.Value < 0)
                {
                    throw new CustomException(
                        "Half Day Threshold Hours cannot be negative.");
                }

                // Validate Company
                var company =
                    (await _unitOfWork.Repository<Company>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (company == null)
                    throw new CustomException(
                        "Company not found.");

                // Validate Region
                var region =
                    (await _unitOfWork.Repository<Region>()
                        .FindAsync(x =>
                            x.RegionId == dto.RegionId &&
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (region == null)
                    throw new CustomException(
                        "Region not found for the selected company.");

                // Validate Branch
                var branch =
                    (await _unitOfWork.Repository<Branch1>()
                        .FindAsync(x =>
                            x.BranchId == dto.BranchId &&
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId))
                    .FirstOrDefault();

                if (branch == null)
                    throw new CustomException(
                        "Branch not found for the selected company and region.");

                // Duplicate Business Hours Name
                var duplicate =
                    await _unitOfWork.Repository<BusinessHour>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId &&
                            x.BranchId == dto.BranchId &&
                            x.BusinessHoursName.ToLower() ==
                            dto.BusinessHoursName.Trim().ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "Business Hours with the same name already exists for the selected branch.");

                // Create Entity
                BusinessHour businessHour =
                    new BusinessHour
                    {
                        CompanyId =
                            dto.CompanyId,

                        RegionId =
                            dto.RegionId,

                        BusinessHoursName =
                            dto.BusinessHoursName.Trim(),

                        BranchId =
                            dto.BranchId,

                        StartTime =
                            dto.StartTime,

                        EndTime =
                            dto.EndTime,

                        BreakStart =
                            dto.BreakStart,

                        BreakEnd =
                            dto.BreakEnd,

                        WorkingDays =
                            dto.WorkingDays.Trim(),

                        Weekend =
                            dto.Weekend?.Trim(),

                        TotalWorkingHours =
                            dto.TotalWorkingHours,

                        LateMarkGraceTimeMinutes =
                            dto.LateMarkGraceTimeMinutes,

                        HalfDayThresholdHours =
                            dto.HalfDayThresholdHours,

                        FlexibleHours =
                            dto.FlexibleHours,

                        OvertimeAllowed =
                            dto.OvertimeAllowed,

                        Description =
                            dto.Description?.Trim(),

                        Active =
                            dto.Active,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedDate =
                            DateTime.Now
                    };

                await _unitOfWork.Repository<BusinessHour>()
                    .AddAsync(businessHour);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "BusinessHour",
                    "INSERT",
                    businessHour.BusinessHoursId,
                    "",
                    JsonConvert.SerializeObject(businessHour),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Business Hours Created Successfully",

                    Data =
                        businessHour.BusinessHoursName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating business hours");

                throw;
            }
        }

        #endregion


        #region UPDATE

        public async Task<ApiResponse<string>> UpdateBusinessHour(
            BusinessHourDto dto)
        {
            try
            {
                // Required Fields
                if (dto == null)
                    throw new CustomException(
                        "Business Hour details are required.");

                if (dto.BusinessHoursId <= 0)
                    throw new CustomException(
                        "Valid Business Hours ID is required.");

                if (string.IsNullOrWhiteSpace(dto.BusinessHoursName))
                    throw new CustomException(
                        "Business Hours Name is required.");

                if (string.IsNullOrWhiteSpace(dto.WorkingDays))
                    throw new CustomException(
                        "Working Days are required.");

                // Validate Start and End Time
                if (dto.StartTime >= dto.EndTime)
                    throw new CustomException(
                        "Start Time must be earlier than End Time.");

                // Validate Break Time
                if (dto.BreakStart.HasValue &&
                    dto.BreakEnd.HasValue)
                {
                    if (dto.BreakStart.Value >= dto.BreakEnd.Value)
                        throw new CustomException(
                            "Break Start Time must be earlier than Break End Time.");

                    if (dto.BreakStart.Value < dto.StartTime ||
                        dto.BreakEnd.Value > dto.EndTime)
                    {
                        throw new CustomException(
                            "Break Time must be within Business Hours.");
                    }
                }

                // Validate Total Working Hours
                if (dto.TotalWorkingHours.HasValue &&
                    dto.TotalWorkingHours.Value < 0)
                {
                    throw new CustomException(
                        "Total Working Hours cannot be negative.");
                }

                // Validate Late Mark Grace Time
                if (dto.LateMarkGraceTimeMinutes.HasValue &&
                    dto.LateMarkGraceTimeMinutes.Value < 0)
                {
                    throw new CustomException(
                        "Late Mark Grace Time cannot be negative.");
                }

                // Validate Half Day Threshold
                if (dto.HalfDayThresholdHours.HasValue &&
                    dto.HalfDayThresholdHours.Value < 0)
                {
                    throw new CustomException(
                        "Half Day Threshold Hours cannot be negative.");
                }

                // Get Existing Business Hour
                var businessHour =
                    (await _unitOfWork.Repository<BusinessHour>()
                        .FindAsync(x =>
                            x.BusinessHoursId ==
                            dto.BusinessHoursId))
                    .FirstOrDefault();

                if (businessHour == null)
                    throw new CustomException(
                        "Business Hours not found.");

                // Validate Company
                var company =
                    (await _unitOfWork.Repository<Company>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (company == null)
                    throw new CustomException(
                        "Company not found.");

                // Validate Region
                var region =
                    (await _unitOfWork.Repository<Region>()
                        .FindAsync(x =>
                            x.RegionId == dto.RegionId &&
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (region == null)
                    throw new CustomException(
                        "Region not found for the selected company.");

                // Validate Branch
                var branch =
                    (await _unitOfWork.Repository<Branch1>()
                        .FindAsync(x =>
                            x.BranchId == dto.BranchId &&
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId))
                    .FirstOrDefault();

                if (branch == null)
                    throw new CustomException(
                        "Branch not found for the selected company and region.");

                // Duplicate Business Hours Name
                var duplicate =
                    await _unitOfWork.Repository<BusinessHour>()
                        .FindAsync(x =>
                            x.BusinessHoursId !=
                            dto.BusinessHoursId &&

                            x.CompanyId ==
                            dto.CompanyId &&

                            x.RegionId ==
                            dto.RegionId &&

                            x.BranchId ==
                            dto.BranchId &&

                            x.BusinessHoursName.ToLower() ==
                            dto.BusinessHoursName.Trim().ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "Business Hours with the same name already exists for the selected branch.");

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(businessHour);

                // Update Entity
                businessHour.CompanyId =
                    dto.CompanyId;

                businessHour.RegionId =
                    dto.RegionId;

                businessHour.BusinessHoursName =
                    dto.BusinessHoursName.Trim();

                businessHour.BranchId =
                    dto.BranchId;

                businessHour.StartTime =
                    dto.StartTime;

                businessHour.EndTime =
                    dto.EndTime;

                businessHour.BreakStart =
                    dto.BreakStart;

                businessHour.BreakEnd =
                    dto.BreakEnd;

                businessHour.WorkingDays =
                    dto.WorkingDays.Trim();

                businessHour.Weekend =
                    dto.Weekend?.Trim();

                businessHour.TotalWorkingHours =
                    dto.TotalWorkingHours;

                businessHour.LateMarkGraceTimeMinutes =
                    dto.LateMarkGraceTimeMinutes;

                businessHour.HalfDayThresholdHours =
                    dto.HalfDayThresholdHours;

                businessHour.FlexibleHours =
                    dto.FlexibleHours;

                businessHour.OvertimeAllowed =
                    dto.OvertimeAllowed;

                businessHour.Description =
                    dto.Description?.Trim();

                businessHour.Active =
                    dto.Active;

                businessHour.UpdatedBy =
                    _currentUserService.UserId;

                businessHour.UpdatedDate =
                    DateTime.Now;

                _unitOfWork.Repository<BusinessHour>()
                    .Update(businessHour);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "BusinessHour",
                    "UPDATE",
                    businessHour.BusinessHoursId,
                    oldValues,
                    JsonConvert.SerializeObject(businessHour),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Business Hours Updated Successfully",

                    Data =
                        businessHour.BusinessHoursName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating business hours");

                throw;
            }
        }

        #endregion


        #region DELETE

        public async Task<ApiResponse<string>> DeleteBusinessHour(
            int id)
        {
            try
            {
                var businessHour =
                    (await _unitOfWork.Repository<BusinessHour>()
                        .FindAsync(x =>
                            x.BusinessHoursId == id))
                    .FirstOrDefault();

                if (businessHour == null)
                    throw new CustomException(
                        "Business Hours not found.");

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(businessHour);

                // Hard Delete
                _unitOfWork.Repository<BusinessHour>()
                    .Remove(businessHour);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "BusinessHour",
                    "DELETE",
                    businessHour.BusinessHoursId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Business Hours Deleted Successfully",

                    Data =
                        businessHour.BusinessHoursName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting business hours");

                throw;
            }
        }

        #endregion


        #region GET ALL

        public async Task<ApiResponse<List<BusinessHourDto>>>
            GetBusinessHours()
        {
            try
            {
                var businessHours =
                    (await _unitOfWork.Repository<BusinessHour>()
                        .GetAllAsync())
                    .OrderByDescending(x =>
                        x.BusinessHoursId)
                    .ToList();

                var result =
                    businessHours.Select(x =>
                        new BusinessHourDto
                        {
                            BusinessHoursId =
                                x.BusinessHoursId,

                            CompanyId =
                                x.CompanyId,

                            RegionId =
                                x.RegionId,

                            BusinessHoursName =
                                x.BusinessHoursName,

                            BranchId =
                                x.BranchId,

                            StartTime =
                                x.StartTime,

                            EndTime =
                                x.EndTime,

                            BreakStart =
                                x.BreakStart,

                            BreakEnd =
                                x.BreakEnd,

                            WorkingDays =
                                x.WorkingDays,

                            Weekend =
                                x.Weekend,

                            TotalWorkingHours =
                                x.TotalWorkingHours,

                            LateMarkGraceTimeMinutes =
                                x.LateMarkGraceTimeMinutes,

                            HalfDayThresholdHours =
                                x.HalfDayThresholdHours,

                            FlexibleHours =
                                x.FlexibleHours,

                            OvertimeAllowed =
                                x.OvertimeAllowed,

                            Description =
                                x.Description,

                            Active =
                                x.Active
                        })
                    .ToList();

                return new ApiResponse<List<BusinessHourDto>>
                {
                    Success = true,

                    Message =
                        "Success",

                    Data =
                        result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting business hours");

                throw;
            }
        }

        #endregion


        #region GET BY ID

        public async Task<ApiResponse<BusinessHourDto>>
            GetBusinessHourById(int id)
        {
            try
            {
                var businessHour =
                    (await _unitOfWork.Repository<BusinessHour>()
                        .FindAsync(x =>
                            x.BusinessHoursId == id))
                    .FirstOrDefault();

                if (businessHour == null)
                    throw new CustomException(
                        "Business Hours not found.");

                var result =
                    new BusinessHourDto
                    {
                        BusinessHoursId =
                            businessHour.BusinessHoursId,

                        CompanyId =
                            businessHour.CompanyId,

                        RegionId =
                            businessHour.RegionId,

                        BusinessHoursName =
                            businessHour.BusinessHoursName,

                        BranchId =
                            businessHour.BranchId,

                        StartTime =
                            businessHour.StartTime,

                        EndTime =
                            businessHour.EndTime,

                        BreakStart =
                            businessHour.BreakStart,

                        BreakEnd =
                            businessHour.BreakEnd,

                        WorkingDays =
                            businessHour.WorkingDays,

                        Weekend =
                            businessHour.Weekend,

                        TotalWorkingHours =
                            businessHour.TotalWorkingHours,

                        LateMarkGraceTimeMinutes =
                            businessHour.LateMarkGraceTimeMinutes,

                        HalfDayThresholdHours =
                            businessHour.HalfDayThresholdHours,

                        FlexibleHours =
                            businessHour.FlexibleHours,

                        OvertimeAllowed =
                            businessHour.OvertimeAllowed,

                        Description =
                            businessHour.Description,

                        Active =
                            businessHour.Active
                    };

                return new ApiResponse<BusinessHourDto>
                {
                    Success = true,

                    Message =
                        "Success",

                    Data =
                        result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting business hour by id");

                throw;
            }
        }

        #endregion

        #endregion
    }
}