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
    public class CompanyTypeService : ICompanyTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public CompanyTypeService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<string>> CreateCompanyType(CompanyTypeDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CompanyTypeName))
                    throw new CustomException("Company Type Name is required.");

                var duplicate = await _unitOfWork.Repository<CompanyType>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CompanyTypeName.ToLower() == dto.CompanyTypeName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Company Type already exists.");

                CompanyType companyType = new CompanyType
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    CompanyTypeName = dto.CompanyTypeName,
                    CompanyTypeCode = dto.CompanyTypeCode,
                    Description = dto.Description,
                    IsActive = dto.IsActive,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<CompanyType>()
                    .AddAsync(companyType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CompanyType",
                    "INSERT",
                    companyType.CompanyTypeId,
                    "",
                    JsonConvert.SerializeObject(companyType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Company Type Created Successfully",
                    Data = companyType.CompanyTypeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating company type");
                throw;
            }
        }
        public async Task<ApiResponse<string>> UpdateCompanyType(CompanyTypeDto dto)
        {
            try
            {
                var companyType = (await _unitOfWork.Repository<CompanyType>()
                    .FindAsync(x => x.CompanyTypeId == dto.CompanyTypeId))
                    .FirstOrDefault();

                if (companyType == null)
                    throw new CustomException("Company Type not found.");

                var duplicate = await _unitOfWork.Repository<CompanyType>()
                    .FindAsync(x =>
                        x.CompanyTypeId != dto.CompanyTypeId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CompanyTypeName.ToLower() == dto.CompanyTypeName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Company Type already exists.");

                string oldValues = JsonConvert.SerializeObject(companyType);

                companyType.CompanyId = dto.CompanyId;
                companyType.RegionId = dto.RegionId;
                companyType.CompanyTypeName = dto.CompanyTypeName;
                companyType.CompanyTypeCode = dto.CompanyTypeCode;
                companyType.Description = dto.Description;
                companyType.IsActive = dto.IsActive;
                companyType.ModifiedBy = _currentUserService.UserId;
                companyType.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<CompanyType>().Update(companyType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CompanyType",
                    "UPDATE",
                    companyType.CompanyTypeId,
                    oldValues,
                    JsonConvert.SerializeObject(companyType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Company Type Updated Successfully",
                    Data = companyType.CompanyTypeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating company type");
                throw;
            }
        }

        public async Task<ApiResponse<string>> DeleteCompanyType(int id)
        {
            try
            {
                var companyType = (await _unitOfWork.Repository<CompanyType>()
                    .FindAsync(x => x.CompanyTypeId == id))
                    .FirstOrDefault();

                if (companyType == null)
                    throw new CustomException("Company Type not found.");

                string oldValues = JsonConvert.SerializeObject(companyType);

                companyType.IsDeleted = true;
                companyType.ModifiedBy = _currentUserService.UserId;
                companyType.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<CompanyType>().Update(companyType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CompanyType",
                    "DELETE",
                    companyType.CompanyTypeId,
                    oldValues,
                    JsonConvert.SerializeObject(companyType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Company Type Deleted Successfully",
                    Data = companyType.CompanyTypeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting company type");
                throw;
            }
        }

        public async Task<ApiResponse<List<CompanyTypeDto>>> GetCompanyTypes()
        {
            try
            {
                var companyTypes = await _unitOfWork.Repository<CompanyType>()
                    .GetAllAsync();

                var result = companyTypes
                    .Where(x => !x.IsDeleted)
                    .Select(x => new CompanyTypeDto
                    {
                        CompanyTypeId = x.CompanyTypeId,
                        CompanyId = x.CompanyId,
                        RegionId = x.RegionId,
                        CompanyTypeName = x.CompanyTypeName,
                        CompanyTypeCode = x.CompanyTypeCode,
                        Description = x.Description,
                        IsActive = x.IsActive
                    })
                    .ToList();

                return new ApiResponse<List<CompanyTypeDto>>
                {
                    Success = true,
                    Message = "Company Types Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting company types");
                throw;
            }
        }
        public async Task<ApiResponse<CompanyTypeDto>> GetCompanyTypeById(int id)
        {
            try
            {
                var companyType = (await _unitOfWork.Repository<CompanyType>()
                    .FindAsync(x => x.CompanyTypeId == id && !x.IsDeleted))
                    .FirstOrDefault();

                if (companyType == null)
                    throw new CustomException("Company Type not found.");

                var result = new CompanyTypeDto
                {
                    CompanyTypeId = companyType.CompanyTypeId,
                    CompanyId = companyType.CompanyId,
                    RegionId = companyType.RegionId,
                    CompanyTypeName = companyType.CompanyTypeName,
                    CompanyTypeCode = companyType.CompanyTypeCode,
                    Description = companyType.Description,
                    IsActive = companyType.IsActive
                };

                return new ApiResponse<CompanyTypeDto>
                {
                    Success = true,
                    Message = "Company Type Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting company type by id");
                throw;
            }
        }
    }
}