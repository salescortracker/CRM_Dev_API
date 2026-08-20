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
  public class IndustryService : IIndustryService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public IndustryService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<string>> CreateIndustry(IndustryDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.IndustryName))
          throw new CustomException("Industry Name is required.");

        var duplicate = await _unitOfWork.Repository<Industry>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.IndustryName.ToLower() == dto.IndustryName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Industry already exists.");

        Industry industry = new Industry
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          IndustryName = dto.IndustryName,
          IndustryCode = dto.IndustryCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<Industry>()
            .AddAsync(industry);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "Industry",
            "INSERT",
            industry.IndustryId,
            "",
            JsonConvert.SerializeObject(industry),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Industry Created Successfully",
          Data = industry.IndustryName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating industry");
        throw;
      }
    }

    public async Task<ApiResponse<string>> UpdateIndustry(IndustryDto dto)
    {
      try
      {
        var industry = (await _unitOfWork.Repository<Industry>()
            .FindAsync(x => x.IndustryId == dto.IndustryId))
            .FirstOrDefault();

        if (industry == null)
          throw new CustomException("Industry not found.");

        var duplicate = await _unitOfWork.Repository<Industry>()
            .FindAsync(x =>
                x.IndustryId != dto.IndustryId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.IndustryName.ToLower() == dto.IndustryName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Industry already exists.");

        string oldValues = JsonConvert.SerializeObject(industry);

        industry.CompanyId = dto.CompanyId;
        industry.RegionId = dto.RegionId;
        industry.IndustryName = dto.IndustryName;
        industry.IndustryCode = dto.IndustryCode;
        industry.Description = dto.Description;
        industry.IsActive = dto.IsActive;
        industry.ModifiedBy = _currentUserService.UserId;
        industry.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<Industry>().Update(industry);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "Industry",
            "UPDATE",
            industry.IndustryId,
            oldValues,
            JsonConvert.SerializeObject(industry),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Industry Updated Successfully",
          Data = industry.IndustryName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating industry");
        throw;
      }
    }

    public async Task<ApiResponse<string>> DeleteIndustry(int id)
    {
      try
      {
        var industry = (await _unitOfWork.Repository<Industry>()
            .FindAsync(x => x.IndustryId == id))
            .FirstOrDefault();

        if (industry == null)
          throw new CustomException("Industry not found.");

        string oldValues = JsonConvert.SerializeObject(industry);

        industry.IsDeleted = true;
        industry.ModifiedBy = _currentUserService.UserId;
        industry.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<Industry>().Update(industry);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "Industry",
            "DELETE",
            industry.IndustryId,
            oldValues,
            JsonConvert.SerializeObject(industry),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Industry Deleted Successfully",
          Data = industry.IndustryName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting industry");
        throw;
      }
    }

    public async Task<ApiResponse<List<IndustryDto>>> GetIndustries()
    {
      try
      {
        var industries = await _unitOfWork.Repository<Industry>()
            .GetAllAsync();

        var result = industries
            .Where(x => !x.IsDeleted)
            .Select(x => new IndustryDto
            {
              IndustryId = x.IndustryId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              IndustryName = x.IndustryName,
              IndustryCode = x.IndustryCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<IndustryDto>>
        {
          Success = true,
          Message = "Industries Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting industries");
        throw;
      }
    }

    public async Task<ApiResponse<IndustryDto>> GetIndustryById(int id)
    {
      try
      {
        var industry = (await _unitOfWork.Repository<Industry>()
            .FindAsync(x => x.IndustryId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (industry == null)
          throw new CustomException("Industry not found.");

        var result = new IndustryDto
        {
          IndustryId = industry.IndustryId,
          CompanyId = industry.CompanyId,
          RegionId = industry.RegionId,
          IndustryName = industry.IndustryName,
          IndustryCode = industry.IndustryCode,
          Description = industry.Description,
          IsActive = industry.IsActive
        };

        return new ApiResponse<IndustryDto>
        {
          Success = true,
          Message = "Industry Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting industry by id");
        throw;
      }
    }
  }
}
