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
  public class PriorityService : IPriorityService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public PriorityService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }
    public async Task<ApiResponse<string>> CreatePriority(PriorityDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.PriorityName))
          throw new CustomException("Priority Name is required.");

        var duplicate = await _unitOfWork.Repository<Priority>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.PriorityName.ToLower() == dto.PriorityName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Priority already exists.");

        Priority priority = new Priority
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          PriorityName = dto.PriorityName,
          PriorityCode = dto.PriorityCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<Priority>().AddAsync(priority);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "Priority",
            "INSERT",
            priority.PriorityId,
            "",
            JsonConvert.SerializeObject(priority),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Priority Created Successfully",
          Data = priority.PriorityName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating priority");
        throw;
      }
    }



    public async Task<ApiResponse<string>> UpdatePriority(PriorityDto dto)
    {
      try
      {
        var priority = (await _unitOfWork.Repository<Priority>()
            .FindAsync(x => x.PriorityId == dto.PriorityId))
            .FirstOrDefault();

        if (priority == null)
          throw new CustomException("Priority not found.");

        var duplicate = await _unitOfWork.Repository<Priority>()
            .FindAsync(x =>
                x.PriorityId != dto.PriorityId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.PriorityName.ToLower() == dto.PriorityName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Priority already exists.");

        string oldValues = JsonConvert.SerializeObject(priority);

        priority.CompanyId = dto.CompanyId;
        priority.RegionId = dto.RegionId;
        priority.PriorityName = dto.PriorityName;
        priority.PriorityCode = dto.PriorityCode;
        priority.Description = dto.Description;
        priority.IsActive = dto.IsActive;
        priority.ModifiedBy = _currentUserService.UserId;
        priority.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<Priority>().Update(priority);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "Priority",
            "UPDATE",
            priority.PriorityId,
            oldValues,
            JsonConvert.SerializeObject(priority),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Priority Updated Successfully",
          Data = priority.PriorityName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating priority");
        throw;
      }
    }

    public async Task<ApiResponse<string>> DeletePriority(int id)
    {
      try
      {
        var priority = (await _unitOfWork.Repository<Priority>()
            .FindAsync(x => x.PriorityId == id))
            .FirstOrDefault();

        if (priority == null)
          throw new CustomException("Priority not found.");

        string oldValues = JsonConvert.SerializeObject(priority);

        priority.IsDeleted = true;
        priority.ModifiedBy = _currentUserService.UserId;
        priority.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<Priority>().Update(priority);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "Priority",
            "DELETE",
            priority.PriorityId,
            oldValues,
            JsonConvert.SerializeObject(priority),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Priority Deleted Successfully",
          Data = priority.PriorityName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting priority");
        throw;
      }
    }

    public async Task<ApiResponse<List<PriorityDto>>> GetPriorities()
    {
      try
      {
        var priorities = await _unitOfWork.Repository<Priority>()
            .GetAllAsync();

        var result = priorities
            .Where(x => !x.IsDeleted)
            .Select(x => new PriorityDto
            {
              PriorityId = x.PriorityId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              PriorityName = x.PriorityName,
              PriorityCode = x.PriorityCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<PriorityDto>>
        {
          Success = true,
          Message = "Priorities Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting priorities");
        throw;
      }
    }

    public async Task<ApiResponse<PriorityDto>> GetPriorityById(int id)
    {
      try
      {
        var priority = (await _unitOfWork.Repository<Priority>()
            .FindAsync(x => x.PriorityId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (priority == null)
          throw new CustomException("Priority not found.");

        var result = new PriorityDto
        {
          PriorityId = priority.PriorityId,
          CompanyId = priority.CompanyId,
          RegionId = priority.RegionId,
          PriorityName = priority.PriorityName,
          PriorityCode = priority.PriorityCode,
          Description = priority.Description,
          IsActive = priority.IsActive
        };

        return new ApiResponse<PriorityDto>
        {
          Success = true,
          Message = "Priority Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting priority by id");
        throw;
      }
    }

  }

}
