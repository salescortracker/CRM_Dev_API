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
  public class LeadStatusService : ILeadStatusService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public LeadStatusService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }

    //CreateLeadStatus()

    public async Task<ApiResponse<string>> CreateLeadStatus(LeadStatusDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.LeadStatusName))
          throw new CustomException("Lead Status Name is required.");

        var duplicate = await _unitOfWork.Repository<LeadStatusDatum>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.LeadStatusName.ToLower() == dto.LeadStatusName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Lead Status already exists.");

        LeadStatusDatum leadStatus = new LeadStatusDatum
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          LeadStatusName = dto.LeadStatusName,
          LeadStatusCode = dto.LeadStatusCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<LeadStatusDatum>().AddAsync(leadStatus);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "LeadStatus",
            "INSERT",
            leadStatus.LeadStatusId,
            "",
            JsonConvert.SerializeObject(leadStatus),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Lead Status Created Successfully",
          Data = leadStatus.LeadStatusName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating lead status");
        throw;
      }
    }

    //UpdateLeadStatus()
    public async Task<ApiResponse<string>> UpdateLeadStatus(LeadStatusDto dto)
    {
      try
      {
        var leadStatus = (await _unitOfWork.Repository<LeadStatusDatum>()
            .FindAsync(x => x.LeadStatusId == dto.LeadStatusId))
            .FirstOrDefault();

        if (leadStatus == null)
          throw new CustomException("Lead Status not found.");

        var duplicate = await _unitOfWork.Repository<LeadStatusDatum>()
            .FindAsync(x =>
                x.LeadStatusId != dto.LeadStatusId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.LeadStatusName.ToLower() == dto.LeadStatusName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Lead Status already exists.");

        string oldValues = JsonConvert.SerializeObject(leadStatus);

        leadStatus.CompanyId = dto.CompanyId;
        leadStatus.RegionId = dto.RegionId;
        leadStatus.LeadStatusName = dto.LeadStatusName;
        leadStatus.LeadStatusCode = dto.LeadStatusCode;
        leadStatus.Description = dto.Description;
        leadStatus.IsActive = dto.IsActive;
        leadStatus.ModifiedBy = _currentUserService.UserId;
        leadStatus.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<LeadStatusDatum>().Update(leadStatus);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "LeadStatus",
            "UPDATE",
            leadStatus.LeadStatusId,
            oldValues,
            JsonConvert.SerializeObject(leadStatus),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Lead Status Updated Successfully",
          Data = leadStatus.LeadStatusName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating lead status");
        throw;
      }
    }

    //DeleteLeadStatus()

    public async Task<ApiResponse<string>> DeleteLeadStatus(int id)
    {
      try
      {
        var leadStatus = (await _unitOfWork.Repository<LeadStatusDatum>()
            .FindAsync(x => x.LeadStatusId == id))
            .FirstOrDefault();

        if (leadStatus == null)
          throw new CustomException("Lead Status not found.");

        string oldValues = JsonConvert.SerializeObject(leadStatus);

        leadStatus.IsDeleted = true;
        leadStatus.ModifiedBy = _currentUserService.UserId;
        leadStatus.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<LeadStatusDatum>().Update(leadStatus);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "LeadStatus",
            "DELETE",
            leadStatus.LeadStatusId,
            oldValues,
            JsonConvert.SerializeObject(leadStatus),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Lead Status Deleted Successfully",
          Data = leadStatus.LeadStatusName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting lead status");
        throw;
      }
    }

    //GetLeadStatuses()
    public async Task<ApiResponse<List<LeadStatusDto>>> GetLeadStatuses()
    {
      try
      {
        var leadStatuses = await _unitOfWork.Repository<LeadStatusDatum>()
            .GetAllAsync();

        var result = leadStatuses
            .Where(x => !x.IsDeleted)
            .Select(x => new LeadStatusDto
            {
              LeadStatusId = x.LeadStatusId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              LeadStatusName = x.LeadStatusName,
              LeadStatusCode = x.LeadStatusCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<LeadStatusDto>>
        {
          Success = true,
          Message = "Lead Statuses Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting lead statuses");
        throw;
      }
    }

    //GetLeadStatusById()
    public async Task<ApiResponse<LeadStatusDto>> GetLeadStatusById(int id)
    {
      try
      {
        var leadStatus = (await _unitOfWork.Repository<LeadStatusDatum>()
            .FindAsync(x => x.LeadStatusId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (leadStatus == null)
          throw new CustomException("Lead Status not found.");

        var result = new LeadStatusDto
        {
          LeadStatusId = leadStatus.LeadStatusId,
          CompanyId = leadStatus.CompanyId,
          RegionId = leadStatus.RegionId,
          LeadStatusName = leadStatus.LeadStatusName,
          LeadStatusCode = leadStatus.LeadStatusCode,
          Description = leadStatus.Description,
          IsActive = leadStatus.IsActive
        };

        return new ApiResponse<LeadStatusDto>
        {
          Success = true,
          Message = "Lead Status Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting lead status by id");
        throw;
      }
    }


  }
}
