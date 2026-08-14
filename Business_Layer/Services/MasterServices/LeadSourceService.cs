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
  public class LeadSourceService : ILeadSourceService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public LeadSourceService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }

    //CreateLeadSource():
    public async Task<ApiResponse<string>> CreateLeadSource(LeadSourceDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.LeadSourceName))
          throw new CustomException("Lead Source Name is required.");

        var duplicate = await _unitOfWork.Repository<LeadSourceDatum>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.LeadSourceName.ToLower() == dto.LeadSourceName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Lead Source already exists.");

        LeadSourceDatum leadSource = new LeadSourceDatum
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          LeadSourceName = dto.LeadSourceName,
          LeadSourceCode = dto.LeadSourceCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<LeadSourceDatum>().AddAsync(leadSource);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "LeadSource",
            "INSERT",
            leadSource.LeadSourceId,
            "",
            JsonConvert.SerializeObject(leadSource),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Lead Source Created Successfully",
          Data = leadSource.LeadSourceName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating lead source");
        throw;
      }
    }

    //UpdateLeadSource()
    public async Task<ApiResponse<string>> UpdateLeadSource(LeadSourceDto dto)
    {
      try
      {
        var leadSource = (await _unitOfWork.Repository<LeadSourceDatum>()
            .FindAsync(x => x.LeadSourceId == dto.LeadSourceId))
            .FirstOrDefault();

        if (leadSource == null)
          throw new CustomException("Lead Source not found.");

        var duplicate = await _unitOfWork.Repository<LeadSourceDatum>()
            .FindAsync(x =>
                x.LeadSourceId != dto.LeadSourceId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.LeadSourceName.ToLower() == dto.LeadSourceName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Lead Source already exists.");

        string oldValues = JsonConvert.SerializeObject(leadSource);

        leadSource.CompanyId = dto.CompanyId;
        leadSource.RegionId = dto.RegionId;
        leadSource.LeadSourceName = dto.LeadSourceName;
        leadSource.LeadSourceCode = dto.LeadSourceCode;
        leadSource.Description = dto.Description;
        leadSource.IsActive = dto.IsActive;
        leadSource.ModifiedBy = _currentUserService.UserId;
        leadSource.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<LeadSourceDatum>().Update(leadSource);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "LeadSource",
            "UPDATE",
            leadSource.LeadSourceId,
            oldValues,
            JsonConvert.SerializeObject(leadSource),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Lead Source Updated Successfully",
          Data = leadSource.LeadSourceName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating lead source");
        throw;
      }
    }
    //DeleteLeadSource()
    public async Task<ApiResponse<string>> DeleteLeadSource(int id)
    {
      try
      {
        var leadSource = (await _unitOfWork.Repository<LeadSourceDatum>()
            .FindAsync(x => x.LeadSourceId == id))
            .FirstOrDefault();

        if (leadSource == null)
          throw new CustomException("Lead Source not found.");

        string oldValues = JsonConvert.SerializeObject(leadSource);

        leadSource.IsDeleted = true;
        leadSource.ModifiedBy = _currentUserService.UserId;
        leadSource.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<LeadSourceDatum>().Update(leadSource);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "LeadSource",
            "DELETE",
            leadSource.LeadSourceId,
            oldValues,
            JsonConvert.SerializeObject(leadSource),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Lead Source Deleted Successfully",
          Data = leadSource.LeadSourceName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting lead source");
        throw;
      }
    }

    //GetLeadSources()
    public async Task<ApiResponse<List<LeadSourceDto>>> GetLeadSources()
    {
      try
      {
        var leadSources = await _unitOfWork.Repository<LeadSourceDatum>()
            .GetAllAsync();

        var result = leadSources
            .Where(x => !x.IsDeleted)
            .Select(x => new LeadSourceDto
            {
              LeadSourceId = x.LeadSourceId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              LeadSourceName = x.LeadSourceName,
              LeadSourceCode = x.LeadSourceCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<LeadSourceDto>>
        {
          Success = true,
          Message = "Lead Sources Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting lead sources");
        throw;
      }
    }

    //GetLeadSourceById()
    public async Task<ApiResponse<LeadSourceDto>> GetLeadSourceById(int id)
    {
      try
      {
        var leadSource = (await _unitOfWork.Repository<LeadSourceDatum>()
            .FindAsync(x => x.LeadSourceId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (leadSource == null)
          throw new CustomException("Lead Source not found.");

        var result = new LeadSourceDto
        {
          LeadSourceId = leadSource.LeadSourceId,
          CompanyId = leadSource.CompanyId,
          RegionId = leadSource.RegionId,
          LeadSourceName = leadSource.LeadSourceName,
          LeadSourceCode = leadSource.LeadSourceCode,
          Description = leadSource.Description,
          IsActive = leadSource.IsActive
        };

        return new ApiResponse<LeadSourceDto>
        {
          Success = true,
          Message = "Lead Source Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting lead source by id");
        throw;
      }
    }

  }
}
