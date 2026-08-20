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
  public class LicenseService : ILicenseService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public LicenseService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<string>> CreateLicense(LicenseDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.LicenseName))
          throw new CustomException("License Name is required.");

        var duplicate = await _unitOfWork.Repository<License>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.LicenseName.ToLower() == dto.LicenseName.ToLower());

        if (duplicate.Any())
          throw new CustomException("License already exists.");

        License license = new License
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          LicenseName = dto.LicenseName,
          LicenseCode = dto.LicenseCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<License>().AddAsync(license);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "License",
            "INSERT",
            license.LicenseId,
            "",
            JsonConvert.SerializeObject(license),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "License Created Successfully",
          Data = license.LicenseName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating license");
        throw;
      }
    }

    public async Task<ApiResponse<string>> UpdateLicense(LicenseDto dto)
    {
      try
      {
        var license = (await _unitOfWork.Repository<License>()
            .FindAsync(x => x.LicenseId == dto.LicenseId))
            .FirstOrDefault();

        if (license == null)
          throw new CustomException("License not found.");

        var duplicate = await _unitOfWork.Repository<License>()
            .FindAsync(x =>
                x.LicenseId != dto.LicenseId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.LicenseName.ToLower() == dto.LicenseName.ToLower());

        if (duplicate.Any())
          throw new CustomException("License already exists.");

        string oldValues = JsonConvert.SerializeObject(license);

        license.CompanyId = dto.CompanyId;
        license.RegionId = dto.RegionId;
        license.LicenseName = dto.LicenseName;
        license.LicenseCode = dto.LicenseCode;
        license.Description = dto.Description;
        license.IsActive = dto.IsActive;
        license.ModifiedBy = _currentUserService.UserId;
        license.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<License>().Update(license);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "License",
            "UPDATE",
            license.LicenseId,
            oldValues,
            JsonConvert.SerializeObject(license),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "License Updated Successfully",
          Data = license.LicenseName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating license");
        throw;
      }
    }

    public async Task<ApiResponse<string>> DeleteLicense(int id)
    {
      try
      {
        var license = (await _unitOfWork.Repository<License>()
            .FindAsync(x => x.LicenseId == id))
            .FirstOrDefault();

        if (license == null)
          throw new CustomException("License not found.");

        string oldValues = JsonConvert.SerializeObject(license);

        license.IsDeleted = true;
        license.ModifiedBy = _currentUserService.UserId;
        license.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<License>().Update(license);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "License",
            "DELETE",
            license.LicenseId,
            oldValues,
            JsonConvert.SerializeObject(license),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "License Deleted Successfully",
          Data = license.LicenseName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting license");
        throw;
      }
    }

    public async Task<ApiResponse<List<LicenseDto>>> GetLicenses()
    {
      try
      {
        var licenses = await _unitOfWork.Repository<License>()
            .GetAllAsync();

        var result = licenses
            .Where(x => !x.IsDeleted)
            .Select(x => new LicenseDto
            {
              LicenseId = x.LicenseId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              LicenseName = x.LicenseName,
              LicenseCode = x.LicenseCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<LicenseDto>>
        {
          Success = true,
          Message = "Licenses Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting licenses");
        throw;
      }
    }

    public async Task<ApiResponse<LicenseDto>> GetLicenseById(int id)
    {
      try
      {
        var license = (await _unitOfWork.Repository<License>()
            .FindAsync(x => x.LicenseId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (license == null)
          throw new CustomException("License not found.");

        var result = new LicenseDto
        {
          LicenseId = license.LicenseId,
          CompanyId = license.CompanyId,
          RegionId = license.RegionId,
          LicenseName = license.LicenseName,
          LicenseCode = license.LicenseCode,
          Description = license.Description,
          IsActive = license.IsActive
        };

        return new ApiResponse<LicenseDto>
        {
          Success = true,
          Message = "License Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting license by id");
        throw;
      }
    }
  }
}
