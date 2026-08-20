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
  public class FiscalTypeService : IFiscalTypeService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public FiscalTypeService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }
    public async Task<ApiResponse<string>> CreateFiscalType(FiscalTypeDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.FiscalTypeName))
          throw new CustomException("Fiscal Type Name is required.");

        var duplicate = await _unitOfWork.Repository<FiscalType>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.FiscalTypeName.ToLower() == dto.FiscalTypeName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Fiscal Type already exists.");

        FiscalType fiscalType = new FiscalType
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          FiscalTypeName = dto.FiscalTypeName,
          FiscalTypeCode = dto.FiscalTypeCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<FiscalType>()
            .AddAsync(fiscalType);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "FiscalType",
            "INSERT",
            fiscalType.FiscalTypeId,
            "",
            JsonConvert.SerializeObject(fiscalType),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Fiscal Type Created Successfully",
          Data = fiscalType.FiscalTypeName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating fiscal type");
        throw;
      }
    }

    public async Task<ApiResponse<string>> UpdateFiscalType(FiscalTypeDto dto)
    {
      try
      {
        var fiscalType = (await _unitOfWork.Repository<FiscalType>()
            .FindAsync(x => x.FiscalTypeId == dto.FiscalTypeId))
            .FirstOrDefault();

        if (fiscalType == null)
          throw new CustomException("Fiscal Type not found.");

        var duplicate = await _unitOfWork.Repository<FiscalType>()
            .FindAsync(x =>
                x.FiscalTypeId != dto.FiscalTypeId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.FiscalTypeName.ToLower() == dto.FiscalTypeName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Fiscal Type already exists.");

        string oldValues = JsonConvert.SerializeObject(fiscalType);

        fiscalType.CompanyId = dto.CompanyId;
        fiscalType.RegionId = dto.RegionId;
        fiscalType.FiscalTypeName = dto.FiscalTypeName;
        fiscalType.FiscalTypeCode = dto.FiscalTypeCode;
        fiscalType.Description = dto.Description;
        fiscalType.IsActive = dto.IsActive;
        fiscalType.ModifiedBy = _currentUserService.UserId;
        fiscalType.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<FiscalType>().Update(fiscalType);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "FiscalType",
            "UPDATE",
            fiscalType.FiscalTypeId,
            oldValues,
            JsonConvert.SerializeObject(fiscalType),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Fiscal Type Updated Successfully",
          Data = fiscalType.FiscalTypeName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating fiscal type");
        throw;
      }
    }

    public async Task<ApiResponse<string>> DeleteFiscalType(int id)
    {
      try
      {
        var fiscalType = (await _unitOfWork.Repository<FiscalType>()
            .FindAsync(x => x.FiscalTypeId == id))
            .FirstOrDefault();

        if (fiscalType == null)
          throw new CustomException("Fiscal Type not found.");

        string oldValues = JsonConvert.SerializeObject(fiscalType);

        fiscalType.IsDeleted = true;
        fiscalType.ModifiedBy = _currentUserService.UserId;
        fiscalType.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<FiscalType>().Update(fiscalType);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "FiscalType",
            "DELETE",
            fiscalType.FiscalTypeId,
            oldValues,
            JsonConvert.SerializeObject(fiscalType),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Fiscal Type Deleted Successfully",
          Data = fiscalType.FiscalTypeName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting fiscal type");
        throw;
      }
    }

    public async Task<ApiResponse<List<FiscalTypeDto>>> GetFiscalTypes()
    {
      try
      {
        var fiscalTypes = await _unitOfWork.Repository<FiscalType>()
            .GetAllAsync();

        var result = fiscalTypes
            .Where(x => !x.IsDeleted)
            .Select(x => new FiscalTypeDto
            {
              FiscalTypeId = x.FiscalTypeId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              FiscalTypeName = x.FiscalTypeName,
              FiscalTypeCode = x.FiscalTypeCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<FiscalTypeDto>>
        {
          Success = true,
          Message = "Fiscal Types Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting fiscal types");
        throw;
      }
    }

    public async Task<ApiResponse<FiscalTypeDto>> GetFiscalTypeById(int id)
    {
      try
      {
        var fiscalType = (await _unitOfWork.Repository<FiscalType>()
            .FindAsync(x => x.FiscalTypeId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (fiscalType == null)
          throw new CustomException("Fiscal Type not found.");

        var result = new FiscalTypeDto
        {
          FiscalTypeId = fiscalType.FiscalTypeId,
          CompanyId = fiscalType.CompanyId,
          RegionId = fiscalType.RegionId,
          FiscalTypeName = fiscalType.FiscalTypeName,
          FiscalTypeCode = fiscalType.FiscalTypeCode,
          Description = fiscalType.Description,
          IsActive = fiscalType.IsActive
        };

        return new ApiResponse<FiscalTypeDto>
        {
          Success = true,
          Message = "Fiscal Type Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting fiscal type by id");
        throw;
      }
    }
  }
}
