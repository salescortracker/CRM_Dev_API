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
  public class DiscountTypeService : IDiscountTypeService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public DiscountTypeService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<string>> CreateDiscountType(DiscountTypeDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.DiscountTypeName))
          throw new CustomException("Discount Type Name is required.");

        var duplicate = await _unitOfWork.Repository<DiscountType>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.DiscountTypeName.ToLower() == dto.DiscountTypeName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Discount Type already exists.");

        DiscountType discountType = new DiscountType
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          DiscountTypeName = dto.DiscountTypeName,
          DiscountTypeCode = dto.DiscountTypeCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<DiscountType>()
            .AddAsync(discountType);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "DiscountType",
            "INSERT",
            discountType.DiscountTypeId,
            "",
            JsonConvert.SerializeObject(discountType),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Discount Type Created Successfully",
          Data = discountType.DiscountTypeName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating discount type");
        throw;
      }
    }

    public async Task<ApiResponse<string>> UpdateDiscountType(DiscountTypeDto dto)
    {
      try
      {
        var discountType = (await _unitOfWork.Repository<DiscountType>()
            .FindAsync(x => x.DiscountTypeId == dto.DiscountTypeId))
            .FirstOrDefault();

        if (discountType == null)
          throw new CustomException("Discount Type not found.");

        var duplicate = await _unitOfWork.Repository<DiscountType>()
            .FindAsync(x =>
                x.DiscountTypeId != dto.DiscountTypeId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.DiscountTypeName.ToLower() == dto.DiscountTypeName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Discount Type already exists.");

        string oldValues = JsonConvert.SerializeObject(discountType);

        discountType.CompanyId = dto.CompanyId;
        discountType.RegionId = dto.RegionId;
        discountType.DiscountTypeName = dto.DiscountTypeName;
        discountType.DiscountTypeCode = dto.DiscountTypeCode;
        discountType.Description = dto.Description;
        discountType.IsActive = dto.IsActive;
        discountType.ModifiedBy = _currentUserService.UserId;
        discountType.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<DiscountType>().Update(discountType);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "DiscountType",
            "UPDATE",
            discountType.DiscountTypeId,
            oldValues,
            JsonConvert.SerializeObject(discountType),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Discount Type Updated Successfully",
          Data = discountType.DiscountTypeName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating discount type");
        throw;
      }
    }

    public async Task<ApiResponse<string>> DeleteDiscountType(int id)
    {
      try
      {
        var discountType = (await _unitOfWork.Repository<DiscountType>()
            .FindAsync(x => x.DiscountTypeId == id))
            .FirstOrDefault();

        if (discountType == null)
          throw new CustomException("Discount Type not found.");

        string oldValues = JsonConvert.SerializeObject(discountType);

        discountType.IsDeleted = true;
        discountType.ModifiedBy = _currentUserService.UserId;
        discountType.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<DiscountType>().Update(discountType);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "DiscountType",
            "DELETE",
            discountType.DiscountTypeId,
            oldValues,
            JsonConvert.SerializeObject(discountType),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Discount Type Deleted Successfully",
          Data = discountType.DiscountTypeName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting discount type");
        throw;
      }
    }

    public async Task<ApiResponse<List<DiscountTypeDto>>> GetDiscountTypes()
    {
      try
      {
        var discountTypes = await _unitOfWork.Repository<DiscountType>()
            .GetAllAsync();

        var result = discountTypes
            .Where(x => !x.IsDeleted)
            .Select(x => new DiscountTypeDto
            {
              DiscountTypeId = x.DiscountTypeId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              DiscountTypeName = x.DiscountTypeName,
              DiscountTypeCode = x.DiscountTypeCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<DiscountTypeDto>>
        {
          Success = true,
          Message = "Discount Types Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting discount types");
        throw;
      }
    }

    public async Task<ApiResponse<DiscountTypeDto>> GetDiscountTypeById(int id)
    {
      try
      {
        var discountType = (await _unitOfWork.Repository<DiscountType>()
            .FindAsync(x => x.DiscountTypeId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (discountType == null)
          throw new CustomException("Discount Type not found.");

        var result = new DiscountTypeDto
        {
          DiscountTypeId = discountType.DiscountTypeId,
          CompanyId = discountType.CompanyId,
          RegionId = discountType.RegionId,
          DiscountTypeName = discountType.DiscountTypeName,
          DiscountTypeCode = discountType.DiscountTypeCode,
          Description = discountType.Description,
          IsActive = discountType.IsActive
        };

        return new ApiResponse<DiscountTypeDto>
        {
          Success = true,
          Message = "Discount Type Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting discount type by id");
        throw;
      }
    }
  }
}
