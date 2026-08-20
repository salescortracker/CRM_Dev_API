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
  public class RetentionPeriodService : IRetentionPeriodService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public RetentionPeriodService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<string>> CreateRetentionPeriod(RetentionPeriodDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.RetentionPeriodName))
          throw new CustomException("Retention Period Name is required.");

        var duplicate = await _unitOfWork.Repository<RetentionPeriod>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.RetentionPeriodName.ToLower() == dto.RetentionPeriodName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Retention Period already exists.");

        RetentionPeriod retentionPeriod = new RetentionPeriod
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          RetentionPeriodName = dto.RetentionPeriodName,
          RetentionPeriodCode = dto.RetentionPeriodCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<RetentionPeriod>()
            .AddAsync(retentionPeriod);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "RetentionPeriod",
            "INSERT",
            retentionPeriod.RetentionPeriodId,
            "",
            JsonConvert.SerializeObject(retentionPeriod),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Retention Period Created Successfully",
          Data = retentionPeriod.RetentionPeriodName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating retention period");
        throw;
      }
    }

    public async Task<ApiResponse<string>> UpdateRetentionPeriod(RetentionPeriodDto dto)
    {
      try
      {
        var retentionPeriod = (await _unitOfWork.Repository<RetentionPeriod>()
            .FindAsync(x => x.RetentionPeriodId == dto.RetentionPeriodId))
            .FirstOrDefault();

        if (retentionPeriod == null)
          throw new CustomException("Retention Period not found.");

        var duplicate = await _unitOfWork.Repository<RetentionPeriod>()
            .FindAsync(x =>
                x.RetentionPeriodId != dto.RetentionPeriodId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.RetentionPeriodName.ToLower() == dto.RetentionPeriodName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Retention Period already exists.");

        string oldValues = JsonConvert.SerializeObject(retentionPeriod);

        retentionPeriod.CompanyId = dto.CompanyId;
        retentionPeriod.RegionId = dto.RegionId;
        retentionPeriod.RetentionPeriodName = dto.RetentionPeriodName;
        retentionPeriod.RetentionPeriodCode = dto.RetentionPeriodCode;
        retentionPeriod.Description = dto.Description;
        retentionPeriod.IsActive = dto.IsActive;
        retentionPeriod.ModifiedBy = _currentUserService.UserId;
        retentionPeriod.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<RetentionPeriod>().Update(retentionPeriod);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "RetentionPeriod",
            "UPDATE",
            retentionPeriod.RetentionPeriodId,
            oldValues,
            JsonConvert.SerializeObject(retentionPeriod),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Retention Period Updated Successfully",
          Data = retentionPeriod.RetentionPeriodName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating retention period");
        throw;
      }
    }

    public async Task<ApiResponse<string>> DeleteRetentionPeriod(int id)
    {
      try
      {
        var retentionPeriod = (await _unitOfWork.Repository<RetentionPeriod>()
            .FindAsync(x => x.RetentionPeriodId == id))
            .FirstOrDefault();

        if (retentionPeriod == null)
          throw new CustomException("Retention Period not found.");

        string oldValues = JsonConvert.SerializeObject(retentionPeriod);

        retentionPeriod.IsDeleted = true;
        retentionPeriod.ModifiedBy = _currentUserService.UserId;
        retentionPeriod.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<RetentionPeriod>().Update(retentionPeriod);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "RetentionPeriod",
            "DELETE",
            retentionPeriod.RetentionPeriodId,
            oldValues,
            JsonConvert.SerializeObject(retentionPeriod),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Retention Period Deleted Successfully",
          Data = retentionPeriod.RetentionPeriodName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting retention period");
        throw;
      }
    }

    public async Task<ApiResponse<List<RetentionPeriodDto>>> GetRetentionPeriods()
    {
      try
      {
        var retentionPeriods = await _unitOfWork.Repository<RetentionPeriod>()
            .GetAllAsync();

        var result = retentionPeriods
            .Where(x => !x.IsDeleted)
            .Select(x => new RetentionPeriodDto
            {
              RetentionPeriodId = x.RetentionPeriodId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              RetentionPeriodName = x.RetentionPeriodName,
              RetentionPeriodCode = x.RetentionPeriodCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<RetentionPeriodDto>>
        {
          Success = true,
          Message = "Retention Periods Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting retention periods");
        throw;
      }
    }

    public async Task<ApiResponse<RetentionPeriodDto>> GetRetentionPeriodById(int id)
    {
      try
      {
        var retentionPeriod = (await _unitOfWork.Repository<RetentionPeriod>()
            .FindAsync(x => x.RetentionPeriodId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (retentionPeriod == null)
          throw new CustomException("Retention Period not found.");

        var result = new RetentionPeriodDto
        {
          RetentionPeriodId = retentionPeriod.RetentionPeriodId,
          CompanyId = retentionPeriod.CompanyId,
          RegionId = retentionPeriod.RegionId,
          RetentionPeriodName = retentionPeriod.RetentionPeriodName,
          RetentionPeriodCode = retentionPeriod.RetentionPeriodCode,
          Description = retentionPeriod.Description,
          IsActive = retentionPeriod.IsActive
        };

        return new ApiResponse<RetentionPeriodDto>
        {
          Success = true,
          Message = "Retention Period Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting retention period by id");
        throw;
      }
    }
  }
}
