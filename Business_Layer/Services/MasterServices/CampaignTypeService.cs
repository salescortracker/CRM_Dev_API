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
    public class CampaignTypeService : ICampaignTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public CampaignTypeService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<string>> CreateCampaignType(CampaignTypeDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CampaignTypeName))
                    throw new CustomException("Campaign Type Name is required.");

                var duplicate = await _unitOfWork.Repository<CampaignType>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CampaignTypeName.ToLower() == dto.CampaignTypeName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Campaign Type already exists.");

                CampaignType campaignType = new CampaignType
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    CampaignTypeName = dto.CampaignTypeName,
                    CampaignTypeCode = dto.CampaignTypeCode,
                    Description = dto.Description,
                    IsActive = dto.IsActive,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<CampaignType>()
                    .AddAsync(campaignType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CampaignType",
                    "INSERT",
                    campaignType.CampaignTypeId,
                    "",
                    JsonConvert.SerializeObject(campaignType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Campaign Type Created Successfully",
                    Data = campaignType.CampaignTypeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating campaign type");
                throw;
            }
        }
        public async Task<ApiResponse<string>> UpdateCampaignType(CampaignTypeDto dto)
        {
            try
            {
                var campaignType = (await _unitOfWork.Repository<CampaignType>()
                    .FindAsync(x => x.CampaignTypeId == dto.CampaignTypeId))
                    .FirstOrDefault();

                if (campaignType == null)
                    throw new CustomException("Campaign Type not found.");

                var duplicate = await _unitOfWork.Repository<CampaignType>()
                    .FindAsync(x =>
                        x.CampaignTypeId != dto.CampaignTypeId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CampaignTypeName.ToLower() == dto.CampaignTypeName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Campaign Type already exists.");

                string oldValues = JsonConvert.SerializeObject(campaignType);

                campaignType.CompanyId = dto.CompanyId;
                campaignType.RegionId = dto.RegionId;
                campaignType.CampaignTypeName = dto.CampaignTypeName;
                campaignType.CampaignTypeCode = dto.CampaignTypeCode;
                campaignType.Description = dto.Description;
                campaignType.IsActive = dto.IsActive;
                campaignType.ModifiedBy = _currentUserService.UserId;
                campaignType.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<CampaignType>().Update(campaignType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CampaignType",
                    "UPDATE",
                    campaignType.CampaignTypeId,
                    oldValues,
                    JsonConvert.SerializeObject(campaignType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Campaign Type Updated Successfully",
                    Data = campaignType.CampaignTypeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating campaign type");
                throw;
            }
        }
        public async Task<ApiResponse<string>> DeleteCampaignType(int id)
        {
            try
            {
                var campaignType = (await _unitOfWork.Repository<CampaignType>()
                    .FindAsync(x => x.CampaignTypeId == id))
                    .FirstOrDefault();

                if (campaignType == null)
                    throw new CustomException("Campaign Type not found.");

                string oldValues = JsonConvert.SerializeObject(campaignType);

                campaignType.IsDeleted = true;
                campaignType.ModifiedBy = _currentUserService.UserId;
                campaignType.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<CampaignType>().Update(campaignType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CampaignType",
                    "DELETE",
                    campaignType.CampaignTypeId,
                    oldValues,
                    JsonConvert.SerializeObject(campaignType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Campaign Type Deleted Successfully",
                    Data = campaignType.CampaignTypeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting campaign type");
                throw;
            }
        }
        public async Task<ApiResponse<List<CampaignTypeDto>>> GetCampaignTypes()
        {
            try
            {
                var campaignTypes = await _unitOfWork.Repository<CampaignType>()
                    .GetAllAsync();

                var result = campaignTypes
                    .Where(x => !x.IsDeleted)
                    .Select(x => new CampaignTypeDto
                    {
                        CampaignTypeId = x.CampaignTypeId,
                        CompanyId = x.CompanyId,
                        RegionId = x.RegionId,
                        CampaignTypeName = x.CampaignTypeName,
                        CampaignTypeCode = x.CampaignTypeCode,
                        Description = x.Description,
                        IsActive = x.IsActive
                    })
                    .ToList();

                return new ApiResponse<List<CampaignTypeDto>>
                {
                    Success = true,
                    Message = "Campaign Types Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting campaign types");
                throw;
            }
        }
        public async Task<ApiResponse<CampaignTypeDto>> GetCampaignTypeById(int id)
        {
            try
            {
                var campaignType = (await _unitOfWork.Repository<CampaignType>()
                    .FindAsync(x => x.CampaignTypeId == id && !x.IsDeleted))
                    .FirstOrDefault();

                if (campaignType == null)
                    throw new CustomException("Campaign Type not found.");

                var result = new CampaignTypeDto
                {
                    CampaignTypeId = campaignType.CampaignTypeId,
                    CompanyId = campaignType.CompanyId,
                    RegionId = campaignType.RegionId,
                    CampaignTypeName = campaignType.CampaignTypeName,
                    CampaignTypeCode = campaignType.CampaignTypeCode,
                    Description = campaignType.Description,
                    IsActive = campaignType.IsActive
                };

                return new ApiResponse<CampaignTypeDto>
                {
                    Success = true,
                    Message = "Campaign Type Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting campaign type by id");
                throw;
            }
        }
    }
}