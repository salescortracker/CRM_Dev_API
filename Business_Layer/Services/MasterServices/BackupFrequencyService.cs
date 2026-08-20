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
  public class BackupFrequencyService : IBackupFrequencyService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public BackupFrequencyService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<string>> CreateBackupFrequency(BackupFrequencyDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.BackupFrequencyName))
          throw new CustomException("Backup Frequency Name is required.");

        var duplicate = await _unitOfWork.Repository<BackupFrequency>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.BackupFrequencyName.ToLower() == dto.BackupFrequencyName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Backup Frequency already exists.");

        BackupFrequency backupFrequency = new BackupFrequency
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          BackupFrequencyName = dto.BackupFrequencyName,
          BackupFrequencyCode = dto.BackupFrequencyCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<BackupFrequency>()
            .AddAsync(backupFrequency);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "BackupFrequency",
            "INSERT",
            backupFrequency.BackupFrequencyId,
            "",
            JsonConvert.SerializeObject(backupFrequency),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Backup Frequency Created Successfully",
          Data = backupFrequency.BackupFrequencyName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating backup frequency");
        throw;
      }
    }

    public async Task<ApiResponse<string>> UpdateBackupFrequency(BackupFrequencyDto dto)
    {
      try
      {
        var backupFrequency = (await _unitOfWork.Repository<BackupFrequency>()
            .FindAsync(x => x.BackupFrequencyId == dto.BackupFrequencyId))
            .FirstOrDefault();

        if (backupFrequency == null)
          throw new CustomException("Backup Frequency not found.");

        var duplicate = await _unitOfWork.Repository<BackupFrequency>()
            .FindAsync(x =>
                x.BackupFrequencyId != dto.BackupFrequencyId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.BackupFrequencyName.ToLower() == dto.BackupFrequencyName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Backup Frequency already exists.");

        string oldValues = JsonConvert.SerializeObject(backupFrequency);

        backupFrequency.CompanyId = dto.CompanyId;
        backupFrequency.RegionId = dto.RegionId;
        backupFrequency.BackupFrequencyName = dto.BackupFrequencyName;
        backupFrequency.BackupFrequencyCode = dto.BackupFrequencyCode;
        backupFrequency.Description = dto.Description;
        backupFrequency.IsActive = dto.IsActive;
        backupFrequency.ModifiedBy = _currentUserService.UserId;
        backupFrequency.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<BackupFrequency>().Update(backupFrequency);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "BackupFrequency",
            "UPDATE",
            backupFrequency.BackupFrequencyId,
            oldValues,
            JsonConvert.SerializeObject(backupFrequency),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Backup Frequency Updated Successfully",
          Data = backupFrequency.BackupFrequencyName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating backup frequency");
        throw;
      }
    }

    public async Task<ApiResponse<string>> DeleteBackupFrequency(int id)
    {
      try
      {
        var backupFrequency = (await _unitOfWork.Repository<BackupFrequency>()
            .FindAsync(x => x.BackupFrequencyId == id))
            .FirstOrDefault();

        if (backupFrequency == null)
          throw new CustomException("Backup Frequency not found.");

        string oldValues = JsonConvert.SerializeObject(backupFrequency);

        backupFrequency.IsDeleted = true;
        backupFrequency.ModifiedBy = _currentUserService.UserId;
        backupFrequency.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<BackupFrequency>().Update(backupFrequency);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "BackupFrequency",
            "DELETE",
            backupFrequency.BackupFrequencyId,
            oldValues,
            JsonConvert.SerializeObject(backupFrequency),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Backup Frequency Deleted Successfully",
          Data = backupFrequency.BackupFrequencyName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting backup frequency");
        throw;
      }
    }

    public async Task<ApiResponse<List<BackupFrequencyDto>>> GetBackupFrequencies()
    {
      try
      {
        var backupFrequencies = await _unitOfWork.Repository<BackupFrequency>()
            .GetAllAsync();

        var result = backupFrequencies
            .Where(x => !x.IsDeleted)
            .Select(x => new BackupFrequencyDto
            {
              BackupFrequencyId = x.BackupFrequencyId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              BackupFrequencyName = x.BackupFrequencyName,
              BackupFrequencyCode = x.BackupFrequencyCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<BackupFrequencyDto>>
        {
          Success = true,
          Message = "Backup Frequencies Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting backup frequencies");
        throw;
      }
    }

    public async Task<ApiResponse<BackupFrequencyDto>> GetBackupFrequencyById(int id)
    {
      try
      {
        var backupFrequency = (await _unitOfWork.Repository<BackupFrequency>()
            .FindAsync(x => x.BackupFrequencyId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (backupFrequency == null)
          throw new CustomException("Backup Frequency not found.");

        var result = new BackupFrequencyDto
        {
          BackupFrequencyId = backupFrequency.BackupFrequencyId,
          CompanyId = backupFrequency.CompanyId,
          RegionId = backupFrequency.RegionId,
          BackupFrequencyName = backupFrequency.BackupFrequencyName,
          BackupFrequencyCode = backupFrequency.BackupFrequencyCode,
          Description = backupFrequency.Description,
          IsActive = backupFrequency.IsActive
        };

        return new ApiResponse<BackupFrequencyDto>
        {
          Success = true,
          Message = "Backup Frequency Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting backup frequency by id");
        throw;
      }
    }
  }
}
