using Business_Layer.DTOs.Admin;
using Business_Layer.Interfaces.Adminsevices;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Shared.CommonModels;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Adminservices
{
    public class HolidayCalendarService : IHolidayCalendarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<HolidayCalendarService> _logger;

        public HolidayCalendarService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService,
            ILogger<HolidayCalendarService> logger)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        #region Holiday Calendar

        #region CREATE

        public async Task<ApiResponse<string>> CreateHolidayCalendar(
            HolidayCalendarDto dto)
        {
            try
            {
                // Validate Holiday Name
                if (string.IsNullOrWhiteSpace(dto.HolidayName))
                    throw new CustomException(
                        "Holiday Name is required.");

                // Validate Year
                if (dto.Year <= 0)
                    throw new CustomException(
                        "Valid Holiday Year is required.");

                // Validate Holiday Date and Year
                if (dto.HolidayDate.Year != dto.Year)
                    throw new CustomException(
                        "Holiday Date year must match the selected year.");

                // Validate Branch
                if (dto.BranchId.HasValue)
                {
                    var branch =
                        (await _unitOfWork.Repository<Branch1>()
                            .FindAsync(x =>
                                x.BranchId == dto.BranchId.Value))
                        .FirstOrDefault();

                    if (branch == null)
                        throw new CustomException(
                            "Branch not found.");
                }

                // Validate Business Unit
                if (dto.BusinessUnitId.HasValue)
                {
                    var businessUnit =
                        (await _unitOfWork.Repository<BusinessUnit>()
                            .FindAsync(x =>
                                x.BusinessUnitId ==
                                dto.BusinessUnitId.Value))
                        .FirstOrDefault();

                    if (businessUnit == null)
                        throw new CustomException(
                            "Business Unit not found.");
                }

                // Validate Department
                if (dto.DepartmentId.HasValue)
                {
                    var department =
                        (await _unitOfWork.Repository<Department>()
                            .FindAsync(x =>
                                x.DepartmentId ==
                                dto.DepartmentId.Value))
                        .FirstOrDefault();

                    if (department == null)
                        throw new CustomException(
                            "Department not found.");
                }

                // Validate Country
                if (dto.CountryId.HasValue)
                {
                    var country =
                        (await _unitOfWork.Repository<Country>()
                            .FindAsync(x =>
                                x.CountryId ==
                                dto.CountryId.Value))
                        .FirstOrDefault();

                    if (country == null)
                        throw new CustomException(
                            "Country not found.");
                }

                // Validate State
                if (dto.StateId.HasValue)
                {
                    var state =
                        (await _unitOfWork.Repository<StateMaster>()
                            .FindAsync(x =>
                                x.StateId ==
                                dto.StateId.Value))
                        .FirstOrDefault();

                    if (state == null)
                        throw new CustomException(
                            "State not found.");
                }

                // Duplicate Holiday
                var duplicate =
                    await _unitOfWork.Repository<HolidayCalendar>()
                        .FindAsync(x =>
                            x.HolidayName.ToLower() ==
                            dto.HolidayName.Trim().ToLower()
                            &&
                            x.HolidayDate == dto.HolidayDate
                            &&
                            x.Year == dto.Year
                            &&
                            x.BranchId == dto.BranchId
                            &&
                            x.BusinessUnitId == dto.BusinessUnitId
                            &&
                            x.DepartmentId == dto.DepartmentId);

                if (duplicate.Any())
                    throw new CustomException(
                        "Holiday with the same name and date already exists.");

                // Create Entity
                HolidayCalendar holiday =
                    new HolidayCalendar
                    {
                        HolidayName =
                            dto.HolidayName.Trim(),

                        HolidayDate =
                            dto.HolidayDate,

                        HolidayType =
                            dto.HolidayType?.Trim(),

                        BranchId =
                            dto.BranchId,

                        BusinessUnitId =
                            dto.BusinessUnitId,

                        DepartmentId =
                            dto.DepartmentId,

                        HolidayCategory =
                            dto.HolidayCategory?.Trim(),

                        ApplicableFor =
                            dto.ApplicableFor?.Trim(),

                        CountryId =
                            dto.CountryId,

                        StateId =
                            dto.StateId,

                        RecurringHoliday =
                            dto.RecurringHoliday,

                        Year =
                            dto.Year,

                        Description =
                            dto.Description?.Trim(),

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedDate =
                            DateTime.Now
                    };

                await _unitOfWork.Repository<HolidayCalendar>()
                    .AddAsync(holiday);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "HolidayCalendar",
                    "INSERT",
                    holiday.HolidayCalendarId,
                    "",
                    JsonConvert.SerializeObject(holiday),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Holiday Calendar Created Successfully",

                    Data =
                        holiday.HolidayName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating holiday calendar");

                throw;
            }
        }

        #endregion


        #region UPDATE

        public async Task<ApiResponse<string>> UpdateHolidayCalendar(
            HolidayCalendarDto dto)
        {
            try
            {
                // Required fields
                if (string.IsNullOrWhiteSpace(dto.HolidayName))
                    throw new CustomException(
                        "Holiday Name is required.");

                if (dto.HolidayCalendarId <= 0)
                    throw new CustomException(
                        "Valid Holiday Calendar ID is required.");

                if (dto.Year <= 0)
                    throw new CustomException(
                        "Valid Holiday Year is required.");

                // Validate Holiday Date and Year
                if (dto.HolidayDate.Year != dto.Year)
                    throw new CustomException(
                        "Holiday Date year must match the selected year.");

                // Get Existing Holiday
                var holiday =
                    (await _unitOfWork.Repository<HolidayCalendar>()
                        .FindAsync(x =>
                            x.HolidayCalendarId ==
                            dto.HolidayCalendarId))
                    .FirstOrDefault();

                if (holiday == null)
                    throw new CustomException(
                        "Holiday Calendar not found.");

                // Validate Branch
                if (dto.BranchId.HasValue)
                {
                    var branch =
                        (await _unitOfWork.Repository<Branch1>()
                            .FindAsync(x =>
                                x.BranchId ==
                                dto.BranchId.Value))
                        .FirstOrDefault();

                    if (branch == null)
                        throw new CustomException(
                            "Branch not found.");
                }

                // Validate Business Unit
                if (dto.BusinessUnitId.HasValue)
                {
                    var businessUnit =
                        (await _unitOfWork.Repository<BusinessUnit>()
                            .FindAsync(x =>
                                x.BusinessUnitId ==
                                dto.BusinessUnitId.Value))
                        .FirstOrDefault();

                    if (businessUnit == null)
                        throw new CustomException(
                            "Business Unit not found.");
                }

                // Validate Department
                if (dto.DepartmentId.HasValue)
                {
                    var department =
                        (await _unitOfWork.Repository<Department>()
                            .FindAsync(x =>
                                x.DepartmentId ==
                                dto.DepartmentId.Value))
                        .FirstOrDefault();

                    if (department == null)
                        throw new CustomException(
                            "Department not found.");
                }

                // Validate Country
                if (dto.CountryId.HasValue)
                {
                    var country =
                        (await _unitOfWork.Repository<Country>()
                            .FindAsync(x =>
                                x.CountryId ==
                                dto.CountryId.Value))
                        .FirstOrDefault();

                    if (country == null)
                        throw new CustomException(
                            "Country not found.");
                }

                // Validate State
                if (dto.StateId.HasValue)
                {
                    var state =
                        (await _unitOfWork.Repository<StateMaster>()
                            .FindAsync(x =>
                                x.StateId ==
                                dto.StateId.Value))
                        .FirstOrDefault();

                    if (state == null)
                        throw new CustomException(
                            "State not found.");
                }

                // Duplicate Holiday
                var duplicate =
                    await _unitOfWork.Repository<HolidayCalendar>()
                        .FindAsync(x =>
                            x.HolidayCalendarId !=
                            dto.HolidayCalendarId
                            &&
                            x.HolidayName.ToLower() ==
                            dto.HolidayName.Trim().ToLower()
                            &&
                            x.HolidayDate ==
                            dto.HolidayDate
                            &&
                            x.Year ==
                            dto.Year
                            &&
                            x.BranchId ==
                            dto.BranchId
                            &&
                            x.BusinessUnitId ==
                            dto.BusinessUnitId
                            &&
                            x.DepartmentId ==
                            dto.DepartmentId);

                if (duplicate.Any())
                    throw new CustomException(
                        "Holiday with the same name and date already exists.");

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(holiday);

                // Update
                holiday.HolidayName =
                    dto.HolidayName.Trim();

                holiday.HolidayDate =
                    dto.HolidayDate;

                holiday.HolidayType =
                    dto.HolidayType?.Trim();

                holiday.BranchId =
                    dto.BranchId;

                holiday.BusinessUnitId =
                    dto.BusinessUnitId;

                holiday.DepartmentId =
                    dto.DepartmentId;

                holiday.HolidayCategory =
                    dto.HolidayCategory?.Trim();

                holiday.ApplicableFor =
                    dto.ApplicableFor?.Trim();

                holiday.CountryId =
                    dto.CountryId;

                holiday.StateId =
                    dto.StateId;

                holiday.RecurringHoliday =
                    dto.RecurringHoliday;

                holiday.Year =
                    dto.Year;

                holiday.Description =
                    dto.Description?.Trim();

                holiday.UpdatedBy =
                    _currentUserService.UserId;

                holiday.UpdatedDate =
                    DateTime.Now;

                _unitOfWork.Repository<HolidayCalendar>()
                    .Update(holiday);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "HolidayCalendar",
                    "UPDATE",
                    holiday.HolidayCalendarId,
                    oldValues,
                    JsonConvert.SerializeObject(holiday),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Holiday Calendar Updated Successfully",

                    Data =
                        holiday.HolidayName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating holiday calendar");

                throw;
            }
        }

        #endregion


        #region DELETE

        public async Task<ApiResponse<string>> DeleteHolidayCalendar(
            int id)
        {
            try
            {
                var holiday =
                    (await _unitOfWork.Repository<HolidayCalendar>()
                        .FindAsync(x =>
                            x.HolidayCalendarId == id))
                    .FirstOrDefault();

                if (holiday == null)
                    throw new CustomException(
                        "Holiday Calendar not found.");

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(holiday);

                // Hard Delete
                _unitOfWork.Repository<HolidayCalendar>()
                    .Remove(holiday);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "HolidayCalendar",
                    "DELETE",
                    holiday.HolidayCalendarId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Holiday Calendar Deleted Successfully",

                    Data =
                        holiday.HolidayName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting holiday calendar");

                throw;
            }
        }

        #endregion


        #region GET ALL

        public async Task<ApiResponse<List<HolidayCalendarDto>>>
            GetHolidayCalendars()
        {
            try
            {
                var holidays =
                    (await _unitOfWork.Repository<HolidayCalendar>()
                        .GetAllAsync())
                    .OrderByDescending(x =>
                        x.HolidayCalendarId)
                    .ToList();

                var result =
                    holidays.Select(x =>
                        new HolidayCalendarDto
                        {
                            HolidayCalendarId =
                                x.HolidayCalendarId,

                            HolidayName =
                                x.HolidayName,

                            HolidayDate =
                                x.HolidayDate,

                            HolidayType =
                                x.HolidayType,

                            BranchId =
                                x.BranchId,

                            BusinessUnitId =
                                x.BusinessUnitId,

                            DepartmentId =
                                x.DepartmentId,

                            HolidayCategory =
                                x.HolidayCategory,

                            ApplicableFor =
                                x.ApplicableFor,

                            CountryId =
                                x.CountryId,

                            StateId =
                                x.StateId,

                            RecurringHoliday =
                                x.RecurringHoliday,

                            Year =
                                x.Year,

                            Description =
                                x.Description
                        })
                    .ToList();

                return new ApiResponse<List<HolidayCalendarDto>>
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
                    "Error while getting holiday calendars");

                throw;
            }
        }

        #endregion


        #region GET BY ID

        public async Task<ApiResponse<HolidayCalendarDto>>
            GetHolidayCalendarById(int id)
        {
            try
            {
                var holiday =
                    (await _unitOfWork.Repository<HolidayCalendar>()
                        .FindAsync(x =>
                            x.HolidayCalendarId == id))
                    .FirstOrDefault();

                if (holiday == null)
                    throw new CustomException(
                        "Holiday Calendar not found.");

                var result =
                    new HolidayCalendarDto
                    {
                        HolidayCalendarId =
                            holiday.HolidayCalendarId,

                        HolidayName =
                            holiday.HolidayName,

                        HolidayDate =
                            holiday.HolidayDate,

                        HolidayType =
                            holiday.HolidayType,

                        BranchId =
                            holiday.BranchId,

                        BusinessUnitId =
                            holiday.BusinessUnitId,

                        DepartmentId =
                            holiday.DepartmentId,

                        HolidayCategory =
                            holiday.HolidayCategory,

                        ApplicableFor =
                            holiday.ApplicableFor,

                        CountryId =
                            holiday.CountryId,

                        StateId =
                            holiday.StateId,

                        RecurringHoliday =
                            holiday.RecurringHoliday,

                        Year =
                            holiday.Year,

                        Description =
                            holiday.Description
                    };

                return new ApiResponse<HolidayCalendarDto>
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
                    "Error while getting holiday calendar by id");

                throw;
            }
        }

        #endregion

        #endregion
    }
}