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
    public class CallPurposeService : ICallPurposeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public CallPurposeService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        // CREATE
        public async Task<ApiResponse<string>> CreateCallPurpose(CallPurposeDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CallPurposesName))
                    throw new CustomException("Call Purpose Name is required.");

                if (string.IsNullOrWhiteSpace(dto.CallPurposesCode))
                    throw new CustomException("Call Purpose Code is required.");

                var duplicate = await _unitOfWork.Repository<CallPurpose>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CallPurposesName.ToLower() == dto.CallPurposesName.ToLower() &&
                        !x.IsDeleted);

                if (duplicate.Any())
                    throw new CustomException("Call Purpose already exists.");

                CallPurpose callPurpose = new CallPurpose
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    CallPurposesName = dto.CallPurposesName,
                    CallPurposesCode = dto.CallPurposesCode,
                    Description = dto.Description,
                    IsActive = dto.IsActive,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<CallPurpose>()
                    .AddAsync(callPurpose);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CallPurpose",
                    "INSERT",
                    callPurpose.CallPurposesId,
                    "",
                    JsonConvert.SerializeObject(callPurpose),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Call Purpose Created Successfully",
                    Data = callPurpose.CallPurposesName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating call purpose");
                throw;
            }
        }


        // UPDATE
        public async Task<ApiResponse<string>> UpdateCallPurpose(CallPurposeDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CallPurposesName))
                    throw new CustomException("Call Purpose Name is required.");

                if (string.IsNullOrWhiteSpace(dto.CallPurposesCode))
                    throw new CustomException("Call Purpose Code is required.");

                var callPurpose = (await _unitOfWork.Repository<CallPurpose>()
                    .FindAsync(x =>
                        x.CallPurposesId == dto.CallPurposesId &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (callPurpose == null)
                    throw new CustomException("Call Purpose not found.");

                var duplicate = await _unitOfWork.Repository<CallPurpose>()
                    .FindAsync(x =>
                        x.CallPurposesId != dto.CallPurposesId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CallPurposesName.ToLower() == dto.CallPurposesName.ToLower() &&
                        !x.IsDeleted);

                if (duplicate.Any())
                    throw new CustomException("Call Purpose already exists.");

                string oldValues = JsonConvert.SerializeObject(callPurpose);

                callPurpose.CompanyId = dto.CompanyId;
                callPurpose.RegionId = dto.RegionId;
                callPurpose.CallPurposesName = dto.CallPurposesName;
                callPurpose.CallPurposesCode = dto.CallPurposesCode;
                callPurpose.Description = dto.Description;
                callPurpose.IsActive = dto.IsActive;
                callPurpose.ModifiedBy = _currentUserService.UserId;
                callPurpose.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<CallPurpose>()
                    .Update(callPurpose);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CallPurpose",
                    "UPDATE",
                    callPurpose.CallPurposesId,
                    oldValues,
                    JsonConvert.SerializeObject(callPurpose),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Call Purpose Updated Successfully",
                    Data = callPurpose.CallPurposesName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating call purpose");
                throw;
            }
        }


        // DELETE
        public async Task<ApiResponse<string>> DeleteCallPurpose(int id)
        {
            try
            {
                var callPurpose = (await _unitOfWork.Repository<CallPurpose>()
                    .FindAsync(x =>
                        x.CallPurposesId == id &&
                        !x.IsDeleted))
                    .FirstOrDefault();

                if (callPurpose == null)
                    throw new CustomException("Call Purpose not found.");

                string oldValues = JsonConvert.SerializeObject(callPurpose);

                // Soft Delete
                callPurpose.IsDeleted = true;
                callPurpose.ModifiedBy = _currentUserService.UserId;
                callPurpose.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<CallPurpose>()
                    .Update(callPurpose);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CallPurpose",
                    "DELETE",
                    callPurpose.CallPurposesId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Call Purpose Deleted Successfully",
                    Data = callPurpose.CallPurposesName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting call purpose");
                throw;
            }
        }


        // GET ALL
        public async Task<ApiResponse<List<CallPurposeDto>>> GetCallPurposes()
        {
            var companies = await _unitOfWork.Repository<Company>()
                .GetAllAsync();

            var regions = await _unitOfWork.Repository<Region>()
                .GetAllAsync();

            var callPurposes = await _unitOfWork.Repository<CallPurpose>()
                .GetAllAsync();

            var result = (
                from cp in callPurposes

                join c in companies
                    on cp.CompanyId equals c.CompanyId

                join r in regions
                    on cp.RegionId equals r.RegionId

                where !cp.IsDeleted

                select new CallPurposeDto
                {
                    CallPurposesId = cp.CallPurposesId,

                    CompanyId = cp.CompanyId,
                    CompanyName = c.CompanyName,

                    RegionId = cp.RegionId,
                    RegionName = r.RegionName,

                    CallPurposesName = cp.CallPurposesName,
                    CallPurposesCode = cp.CallPurposesCode,
                    Description = cp.Description,

                    IsActive = cp.IsActive
                }
            )
            .OrderByDescending(x => x.CallPurposesId)
            .ToList();

            return new ApiResponse<List<CallPurposeDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        // GET BY ID
        public async Task<ApiResponse<CallPurposeDto>> GetCallPurposeById(int id)
        {
            var callPurpose = (await _unitOfWork.Repository<CallPurpose>()
                .FindAsync(x =>
                    x.CallPurposesId == id &&
                    !x.IsDeleted))
                .FirstOrDefault();

            if (callPurpose == null)
                throw new CustomException("Call Purpose not found.");

            return new ApiResponse<CallPurposeDto>
            {
                Success = true,
                Message = "Success",

                Data = new CallPurposeDto
                {
                    CallPurposesId = callPurpose.CallPurposesId,

                    CompanyId = callPurpose.CompanyId,

                    RegionId = callPurpose.RegionId,

                    CallPurposesName = callPurpose.CallPurposesName,

                    CallPurposesCode = callPurpose.CallPurposesCode,

                    Description = callPurpose.Description,

                    IsActive = callPurpose.IsActive
                }
            };
        }
    }
}
