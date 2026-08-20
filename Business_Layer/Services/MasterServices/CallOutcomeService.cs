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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.MasterServices
{
    public class CallOutcomeService : ICallOutcomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public CallOutcomeService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        // CREATE
        public async Task<ApiResponse<string>> CreateCallOutcome(CallOutcomeDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CallOutcomesName))
                    throw new CustomException("Call Outcome Name is required.");

                if (string.IsNullOrWhiteSpace(dto.CallOutcomesCode))
                    throw new CustomException("Call Outcome Code is required.");

                var duplicate = await _unitOfWork.Repository<CallOutcome>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CallOutcomesName.ToLower() == dto.CallOutcomesName.ToLower() &&
                        !x.IsDeleted);

                if (duplicate.Any())
                    throw new CustomException("Call Outcome already exists.");

                CallOutcome callOutcome = new CallOutcome
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    CallOutcomesName = dto.CallOutcomesName,
                    CallOutcomesCode = dto.CallOutcomesCode,
                    Description = dto.Description,
                    IsActive = dto.IsActive,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<CallOutcome>()
                    .AddAsync(callOutcome);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CallOutcome",
                    "INSERT",
                    callOutcome.CallOutcomesId,
                    "",
                    JsonConvert.SerializeObject(callOutcome),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Call Outcome Created Successfully",
                    Data = callOutcome.CallOutcomesName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating call outcome");
                throw;
            }
        }


        // UPDATE
        public async Task<ApiResponse<string>> UpdateCallOutcome(CallOutcomeDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CallOutcomesName))
                    throw new CustomException("Call Outcome Name is required.");

                if (string.IsNullOrWhiteSpace(dto.CallOutcomesCode))
                    throw new CustomException("Call Outcome Code is required.");

                var callOutcome = (await _unitOfWork.Repository<CallOutcome>()
                    .FindAsync(x =>
                        x.CallOutcomesId == dto.CallOutcomesId &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (callOutcome == null)
                    throw new CustomException("Call Outcome not found.");

                var duplicate = await _unitOfWork.Repository<CallOutcome>()
                    .FindAsync(x =>
                        x.CallOutcomesId != dto.CallOutcomesId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CallOutcomesName.ToLower() == dto.CallOutcomesName.ToLower() &&
                        !x.IsDeleted);

                if (duplicate.Any())
                    throw new CustomException("Call Outcome already exists.");

                string oldValues = JsonConvert.SerializeObject(callOutcome);

                callOutcome.CompanyId = dto.CompanyId;
                callOutcome.RegionId = dto.RegionId;
                callOutcome.CallOutcomesName = dto.CallOutcomesName;
                callOutcome.CallOutcomesCode = dto.CallOutcomesCode;
                callOutcome.Description = dto.Description;
                callOutcome.IsActive = dto.IsActive;
                callOutcome.ModifiedBy = _currentUserService.UserId;
                callOutcome.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<CallOutcome>()
                    .Update(callOutcome);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CallOutcome",
                    "UPDATE",
                    callOutcome.CallOutcomesId,
                    oldValues,
                    JsonConvert.SerializeObject(callOutcome),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Call Outcome Updated Successfully",
                    Data = callOutcome.CallOutcomesName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating call outcome");
                throw;
            }
        }


        // DELETE
        public async Task<ApiResponse<string>> DeleteCallOutcome(int id)
        {
            try
            {
                var callOutcome = (await _unitOfWork.Repository<CallOutcome>()
                    .FindAsync(x =>
                        x.CallOutcomesId == id &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (callOutcome == null)
                    throw new CustomException("Call Outcome not found.");

                string oldValues = JsonConvert.SerializeObject(callOutcome);

                // Soft Delete
                callOutcome.IsDeleted = true;
                callOutcome.ModifiedBy = _currentUserService.UserId;
                callOutcome.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<CallOutcome>()
                    .Update(callOutcome);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CallOutcome",
                    "DELETE",
                    callOutcome.CallOutcomesId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Call Outcome Deleted Successfully",
                    Data = callOutcome.CallOutcomesName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting call outcome");
                throw;
            }
        }


        // GET ALL
        public async Task<ApiResponse<List<CallOutcomeDto>>> GetCallOutcomes()
        {
            var companies = await _unitOfWork.Repository<Company>()
                .GetAllAsync();

            var regions = await _unitOfWork.Repository<Region>()
                .GetAllAsync();

            var callOutcomes = await _unitOfWork.Repository<CallOutcome>()
                .GetAllAsync();

            var result = (
                from co in callOutcomes

                join c in companies
                    on co.CompanyId equals c.CompanyId

                join r in regions
                    on co.RegionId equals r.RegionId

                where !co.IsDeleted

                select new CallOutcomeDto
                {
                    CallOutcomesId = co.CallOutcomesId,

                    CompanyId = co.CompanyId,
                    CompanyName = c.CompanyName,

                    RegionId = co.RegionId,
                    RegionName = r.RegionName,

                    CallOutcomesName = co.CallOutcomesName,
                    CallOutcomesCode = co.CallOutcomesCode,
                    Description = co.Description,

                    IsActive = co.IsActive
                }
            )
            .OrderByDescending(x => x.CallOutcomesId)
            .ToList();

            return new ApiResponse<List<CallOutcomeDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        // GET BY ID
        public async Task<ApiResponse<CallOutcomeDto>> GetCallOutcomeById(int id)
        {
            var callOutcome = (await _unitOfWork.Repository<CallOutcome>()
                .FindAsync(x =>
                    x.CallOutcomesId == id &&
                    !x.IsDeleted))
                .FirstOrDefault();

            if (callOutcome == null)
                throw new CustomException("Call Outcome not found.");

            return new ApiResponse<CallOutcomeDto>
            {
                Success = true,
                Message = "Success",

                Data = new CallOutcomeDto
                {
                    CallOutcomesId = callOutcome.CallOutcomesId,

                    CompanyId = callOutcome.CompanyId,

                    RegionId = callOutcome.RegionId,

                    CallOutcomesName = callOutcome.CallOutcomesName,

                    CallOutcomesCode = callOutcome.CallOutcomesCode,

                    Description = callOutcome.Description,

                    IsActive = callOutcome.IsActive
                }
            };
        }
    }
}
