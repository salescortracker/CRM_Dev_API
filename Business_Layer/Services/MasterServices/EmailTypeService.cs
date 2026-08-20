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
  public class EmailTypeService : IEmailTypeService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public EmailTypeService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<string>> CreateEmailType(EmailTypeDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.EmailTypesName))
          throw new CustomException("Email Type Name is required.");

        var duplicate = await _unitOfWork.Repository<EmailType>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.EmailTypesName.ToLower() == dto.EmailTypesName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Email Type already exists.");

        EmailType emailType = new EmailType
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          EmailTypesName = dto.EmailTypesName,
          EmailTypesCode = dto.EmailTypesCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<EmailType>()
            .AddAsync(emailType);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "EmailType",
            "INSERT",
            emailType.EmailTypesId,
            "",
            JsonConvert.SerializeObject(emailType),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Email Type Created Successfully",
          Data = emailType.EmailTypesName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating email type");
        throw;
      }
    }
    public async Task<ApiResponse<string>> UpdateEmailType(EmailTypeDto dto)
    {
      try
      {
        var emailType = (await _unitOfWork.Repository<EmailType>()
            .FindAsync(x => x.EmailTypesId == dto.EmailTypesId))
            .FirstOrDefault();

        if (emailType == null)
          throw new CustomException("Email Type not found.");

        var duplicate = await _unitOfWork.Repository<EmailType>()
            .FindAsync(x =>
                x.EmailTypesId != dto.EmailTypesId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.EmailTypesName.ToLower() == dto.EmailTypesName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Email Type already exists.");

        string oldValues = JsonConvert.SerializeObject(emailType);

        emailType.CompanyId = dto.CompanyId;
        emailType.RegionId = dto.RegionId;
        emailType.EmailTypesName = dto.EmailTypesName;
        emailType.EmailTypesCode = dto.EmailTypesCode;
        emailType.Description = dto.Description;
        emailType.IsActive = dto.IsActive;
        emailType.ModifiedBy = _currentUserService.UserId;
        emailType.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<EmailType>().Update(emailType);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "EmailType",
            "UPDATE",
            emailType.EmailTypesId,
            oldValues,
            JsonConvert.SerializeObject(emailType),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Email Type Updated Successfully",
          Data = emailType.EmailTypesName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating email type");
        throw;
      }
    }

    public async Task<ApiResponse<string>> DeleteEmailType(int id)
    {
      try
      {
        var emailType = (await _unitOfWork.Repository<EmailType>()
            .FindAsync(x => x.EmailTypesId == id))
            .FirstOrDefault();

        if (emailType == null)
          throw new CustomException("Email Type not found.");

        string oldValues = JsonConvert.SerializeObject(emailType);

        emailType.IsDeleted = true;
        emailType.ModifiedBy = _currentUserService.UserId;
        emailType.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<EmailType>().Update(emailType);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "EmailType",
            "DELETE",
            emailType.EmailTypesId,
            oldValues,
            JsonConvert.SerializeObject(emailType),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Email Type Deleted Successfully",
          Data = emailType.EmailTypesName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting email type");
        throw;
      }
    }

    public async Task<ApiResponse<List<EmailTypeDto>>> GetEmailTypes()
    {
      try
      {
        var emailTypes = await _unitOfWork.Repository<EmailType>()
            .GetAllAsync();

        var result = emailTypes
            .Where(x => !x.IsDeleted)
            .Select(x => new EmailTypeDto
            {
              EmailTypesId = x.EmailTypesId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              EmailTypesName = x.EmailTypesName,
              EmailTypesCode = x.EmailTypesCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<EmailTypeDto>>
        {
          Success = true,
          Message = "Email Types Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting email types");
        throw;
      }
    }

    public async Task<ApiResponse<EmailTypeDto>> GetEmailTypeById(int id)
    {
      try
      {
        var emailType = (await _unitOfWork.Repository<EmailType>()
            .FindAsync(x => x.EmailTypesId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (emailType == null)
          throw new CustomException("Email Type not found.");

        var result = new EmailTypeDto
        {
          EmailTypesId = emailType.EmailTypesId,
          CompanyId = emailType.CompanyId,
          RegionId = emailType.RegionId,
          EmailTypesName = emailType.EmailTypesName,
          EmailTypesCode = emailType.EmailTypesCode,
          Description = emailType.Description,
          IsActive = emailType.IsActive
        };

        return new ApiResponse<EmailTypeDto>
        {
          Success = true,
          Message = "Email Type Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting email type by id");
        throw;
      }
    }
  }
}
