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
  public class LeadTypeService : ILeadTypeService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public LeadTypeService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<string>> CreateLeadType(LeadTypeDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.LeadTypeName))
          throw new CustomException("Lead Type Name is required.");

        var duplicate = await _unitOfWork.Repository<LeadType>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.LeadTypeName.ToLower() == dto.LeadTypeName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Lead Type already exists.");

        LeadType leadType = new LeadType
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          LeadTypeName = dto.LeadTypeName,
          LeadTypeCode = dto.LeadTypeCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<LeadType>()
            .AddAsync(leadType);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "LeadType",
            "INSERT",
            leadType.LeadTypeId,
            "",
            JsonConvert.SerializeObject(leadType),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Lead Type Created Successfully",
          Data = leadType.LeadTypeName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating lead type");
        throw;
      }
    }

    public async Task<ApiResponse<string>> UpdateLeadType(LeadTypeDto dto)
    {
      try
      {
        var leadType = (await _unitOfWork.Repository<LeadType>()
            .FindAsync(x => x.LeadTypeId == dto.LeadTypeId))
            .FirstOrDefault();

        if (leadType == null)
          throw new CustomException("Lead Type not found.");

        var duplicate = await _unitOfWork.Repository<LeadType>()
            .FindAsync(x =>
                x.LeadTypeId != dto.LeadTypeId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.LeadTypeName.ToLower() == dto.LeadTypeName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Lead Type already exists.");

        string oldValues = JsonConvert.SerializeObject(leadType);

        leadType.CompanyId = dto.CompanyId;
        leadType.RegionId = dto.RegionId;
        leadType.LeadTypeName = dto.LeadTypeName;
        leadType.LeadTypeCode = dto.LeadTypeCode;
        leadType.Description = dto.Description;
        leadType.IsActive = dto.IsActive;
        leadType.ModifiedBy = _currentUserService.UserId;
        leadType.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<LeadType>().Update(leadType);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "LeadType",
            "UPDATE",
            leadType.LeadTypeId,
            oldValues,
            JsonConvert.SerializeObject(leadType),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Lead Type Updated Successfully",
          Data = leadType.LeadTypeName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating lead type");
        throw;
      }
    }

    public async Task<ApiResponse<string>> DeleteLeadType(int id)
    {
      try
      {
        var leadType = (await _unitOfWork.Repository<LeadType>()
            .FindAsync(x => x.LeadTypeId == id))
            .FirstOrDefault();

        if (leadType == null)
          throw new CustomException("Lead Type not found.");

        string oldValues = JsonConvert.SerializeObject(leadType);

        leadType.IsDeleted = true;
        leadType.ModifiedBy = _currentUserService.UserId;
        leadType.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<LeadType>().Update(leadType);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "LeadType",
            "DELETE",
            leadType.LeadTypeId,
            oldValues,
            JsonConvert.SerializeObject(leadType),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Lead Type Deleted Successfully",
          Data = leadType.LeadTypeName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting lead type");
        throw;
      }
    }

    public async Task<ApiResponse<List<LeadTypeDto>>> GetLeadTypes()
    {
      try
      {
        var leadTypes = await _unitOfWork.Repository<LeadType>()
            .GetAllAsync();

        var result = leadTypes
            .Where(x => !x.IsDeleted)
            .Select(x => new LeadTypeDto
            {
              LeadTypeId = x.LeadTypeId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              LeadTypeName = x.LeadTypeName,
              LeadTypeCode = x.LeadTypeCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<LeadTypeDto>>
        {
          Success = true,
          Message = "Lead Types Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting lead types");
        throw;
      }
    }
    public async Task<ApiResponse<LeadTypeDto>> GetLeadTypeById(int id)
    {
      try
      {
        var leadType = (await _unitOfWork.Repository<LeadType>()
            .FindAsync(x => x.LeadTypeId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (leadType == null)
          throw new CustomException("Lead Type not found.");

        var result = new LeadTypeDto
        {
          LeadTypeId = leadType.LeadTypeId,
          CompanyId = leadType.CompanyId,
          RegionId = leadType.RegionId,
          LeadTypeName = leadType.LeadTypeName,
          LeadTypeCode = leadType.LeadTypeCode,
          Description = leadType.Description,
          IsActive = leadType.IsActive
        };

        return new ApiResponse<LeadTypeDto>
        {
          Success = true,
          Message = "Lead Type Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting lead type by id");
        throw;
      }
    }
  }
}
