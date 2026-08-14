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
  public class BillingCycleService : IBillingCycleService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public BillingCycleService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }

         //CreateBillingCycle()
    public async Task<ApiResponse<string>> CreateBillingCycle(BillingCycleDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.BillingCycleName))
          throw new CustomException("Billing Cycle Name is required.");

        var duplicate = await _unitOfWork.Repository<BillingCycle>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.BillingCycleName.ToLower() == dto.BillingCycleName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Billing Cycle already exists.");

        BillingCycle billingCycle = new BillingCycle
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          BillingCycleName = dto.BillingCycleName,
          BillingCycleCode = dto.BillingCycleCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<BillingCycle>().AddAsync(billingCycle);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "BillingCycle",
            "INSERT",
            billingCycle.BillingCycleId,
            "",
            JsonConvert.SerializeObject(billingCycle),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Billing Cycle Created Successfully",
          Data = billingCycle.BillingCycleName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating billing cycle");
        throw;
      }
    }

    //UpdateBillingCycle()
    public async Task<ApiResponse<string>> UpdateBillingCycle(BillingCycleDto dto)
    {
      try
      {
        var billingCycle = (await _unitOfWork.Repository<BillingCycle>()
            .FindAsync(x => x.BillingCycleId == dto.BillingCycleId))
            .FirstOrDefault();

        if (billingCycle == null)
          throw new CustomException("Billing Cycle not found.");

        var duplicate = await _unitOfWork.Repository<BillingCycle>()
            .FindAsync(x =>
                x.BillingCycleId != dto.BillingCycleId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.BillingCycleName.ToLower() == dto.BillingCycleName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Billing Cycle already exists.");

        string oldValues = JsonConvert.SerializeObject(billingCycle);

        billingCycle.CompanyId = dto.CompanyId;
        billingCycle.RegionId = dto.RegionId;
        billingCycle.BillingCycleName = dto.BillingCycleName;
        billingCycle.BillingCycleCode = dto.BillingCycleCode;
        billingCycle.Description = dto.Description;
        billingCycle.IsActive = dto.IsActive;
        billingCycle.ModifiedBy = _currentUserService.UserId;
        billingCycle.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<BillingCycle>().Update(billingCycle);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "BillingCycle",
            "UPDATE",
            billingCycle.BillingCycleId,
            oldValues,
            JsonConvert.SerializeObject(billingCycle),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Billing Cycle Updated Successfully",
          Data = billingCycle.BillingCycleName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating billing cycle");
        throw;
      }
    }

    //DeleteBillingCycle()
    public async Task<ApiResponse<string>> DeleteBillingCycle(int id)
    {
      try
      {
        var billingCycle = (await _unitOfWork.Repository<BillingCycle>()
            .FindAsync(x => x.BillingCycleId == id))
            .FirstOrDefault();

        if (billingCycle == null)
          throw new CustomException("Billing Cycle not found.");

        string oldValues = JsonConvert.SerializeObject(billingCycle);

        billingCycle.IsDeleted = true;
        billingCycle.ModifiedBy = _currentUserService.UserId;
        billingCycle.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<BillingCycle>().Update(billingCycle);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "BillingCycle",
            "DELETE",
            billingCycle.BillingCycleId,
            oldValues,
            JsonConvert.SerializeObject(billingCycle),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Billing Cycle Deleted Successfully",
          Data = billingCycle.BillingCycleName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting billing cycle");
        throw;
      }
    }

    //GetBillingCycles()
    public async Task<ApiResponse<List<BillingCycleDto>>> GetBillingCycles()
    {
      try
      {
        var billingCycles = await _unitOfWork.Repository<BillingCycle>()
            .GetAllAsync();

        var result = billingCycles
            .Where(x => !x.IsDeleted)
            .Select(x => new BillingCycleDto
            {
              BillingCycleId = x.BillingCycleId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              BillingCycleName = x.BillingCycleName,
              BillingCycleCode = x.BillingCycleCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<BillingCycleDto>>
        {
          Success = true,
          Message = "Billing Cycles Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting billing cycles");
        throw;
      }
    }

    //GetBillingCycleById()
    public async Task<ApiResponse<BillingCycleDto>> GetBillingCycleById(int id)
    {
      try
      {
        var billingCycle = (await _unitOfWork.Repository<BillingCycle>()
            .FindAsync(x => x.BillingCycleId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (billingCycle == null)
          throw new CustomException("Billing Cycle not found.");

        var result = new BillingCycleDto
        {
          BillingCycleId = billingCycle.BillingCycleId,
          CompanyId = billingCycle.CompanyId,
          RegionId = billingCycle.RegionId,
          BillingCycleName = billingCycle.BillingCycleName,
          BillingCycleCode = billingCycle.BillingCycleCode,
          Description = billingCycle.Description,
          IsActive = billingCycle.IsActive
        };

        return new ApiResponse<BillingCycleDto>
        {
          Success = true,
          Message = "Billing Cycle Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting billing cycle by id");
        throw;
      }
    }

  }
}
