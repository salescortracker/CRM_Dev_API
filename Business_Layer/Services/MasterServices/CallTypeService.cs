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
    public class CallTypeService : ICallTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public CallTypeService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        // CREATE
        public async Task<ApiResponse<string>> CreateCallType(CallTypeDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CallTypesName))
                    throw new CustomException("Call Type Name is required.");

                if (string.IsNullOrWhiteSpace(dto.CallTypesCode))
                    throw new CustomException("Call Type Code is required.");

                var duplicate = await _unitOfWork.Repository<CallType>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CallTypesName.ToLower() == dto.CallTypesName.ToLower() &&
                        !x.IsDeleted);

                if (duplicate.Any())
                    throw new CustomException("Call Type already exists.");

                CallType callType = new CallType
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    CallTypesName = dto.CallTypesName,
                    CallTypesCode = dto.CallTypesCode,
                    Description = dto.Description,
                    IsActive = dto.IsActive,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<CallType>()
                    .AddAsync(callType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CallType",
                    "INSERT",
                    callType.CallTypesId,
                    "",
                    JsonConvert.SerializeObject(callType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Call Type Created Successfully",
                    Data = callType.CallTypesName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating call type");
                throw;
            }
        }


        // UPDATE
        public async Task<ApiResponse<string>> UpdateCallType(CallTypeDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CallTypesName))
                    throw new CustomException("Call Type Name is required.");

                if (string.IsNullOrWhiteSpace(dto.CallTypesCode))
                    throw new CustomException("Call Type Code is required.");

                var callType = (await _unitOfWork.Repository<CallType>()
                    .FindAsync(x => x.CallTypesId == dto.CallTypesId && !x.IsDeleted))
                    .FirstOrDefault();

                if (callType == null)
                    throw new CustomException("Call Type not found.");

                var duplicate = await _unitOfWork.Repository<CallType>()
                    .FindAsync(x =>
                        x.CallTypesId != dto.CallTypesId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CallTypesName.ToLower() == dto.CallTypesName.ToLower() &&
                        !x.IsDeleted);

                if (duplicate.Any())
                    throw new CustomException("Call Type already exists.");

                string oldValues = JsonConvert.SerializeObject(callType);

                callType.CompanyId = dto.CompanyId;
                callType.RegionId = dto.RegionId;
                callType.CallTypesName = dto.CallTypesName;
                callType.CallTypesCode = dto.CallTypesCode;
                callType.Description = dto.Description;
                callType.IsActive = dto.IsActive;
                callType.ModifiedBy = _currentUserService.UserId;
                callType.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<CallType>()
                    .Update(callType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CallType",
                    "UPDATE",
                    callType.CallTypesId,
                    oldValues,
                    JsonConvert.SerializeObject(callType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Call Type Updated Successfully",
                    Data = callType.CallTypesName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating call type");
                throw;
            }
        }


        // DELETE
        public async Task<ApiResponse<string>> DeleteCallType(int id)
        {
            try
            {
                var callType = (await _unitOfWork.Repository<CallType>()
                    .FindAsync(x => x.CallTypesId == id && !x.IsDeleted))
                    .FirstOrDefault();

                if (callType == null)
                    throw new CustomException("Call Type not found.");

                string oldValues = JsonConvert.SerializeObject(callType);

                // Soft Delete
                callType.IsDeleted = true;
                callType.ModifiedBy = _currentUserService.UserId;
                callType.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<CallType>()
                    .Update(callType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CallType",
                    "DELETE",
                    callType.CallTypesId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Call Type Deleted Successfully",
                    Data = callType.CallTypesName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting call type");
                throw;
            }
        }


        // GET ALL
        public async Task<ApiResponse<List<CallTypeDto>>> GetCallTypes()
        {
            var companies = await _unitOfWork.Repository<Company>()
                .GetAllAsync();

            var regions = await _unitOfWork.Repository<Region>()
                .GetAllAsync();

            var callTypes = await _unitOfWork.Repository<CallType>()
                .GetAllAsync();

            var result = (
                from ct in callTypes

                join c in companies
                    on ct.CompanyId equals c.CompanyId

                join r in regions
                    on ct.RegionId equals r.RegionId

                where !ct.IsDeleted

                select new CallTypeDto
                {
                    CallTypesId = ct.CallTypesId,

                    CompanyId = ct.CompanyId,
                    CompanyName = c.CompanyName,

                    RegionId = ct.RegionId,
                    RegionName = r.RegionName,

                    CallTypesName = ct.CallTypesName,
                    CallTypesCode = ct.CallTypesCode,
                    Description = ct.Description,

                    IsActive = ct.IsActive
                }
            )
            .OrderByDescending(x => x.CallTypesId)
            .ToList();

            return new ApiResponse<List<CallTypeDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        // GET BY ID
        public async Task<ApiResponse<CallTypeDto>> GetCallTypeById(int id)
        {
            var callType = (await _unitOfWork.Repository<CallType>()
                .FindAsync(x =>
                    x.CallTypesId == id &&
                    !x.IsDeleted))
                .FirstOrDefault();

            if (callType == null)
                throw new CustomException("Call Type not found.");

            return new ApiResponse<CallTypeDto>
            {
                Success = true,
                Message = "Success",

                Data = new CallTypeDto
                {
                    CallTypesId = callType.CallTypesId,

                    CompanyId = callType.CompanyId,

                    RegionId = callType.RegionId,

                    CallTypesName = callType.CallTypesName,

                    CallTypesCode = callType.CallTypesCode,

                    Description = callType.Description,

                    IsActive = callType.IsActive
                }
            };
        }
    }
}
