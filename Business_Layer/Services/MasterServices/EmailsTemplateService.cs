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
  public class EmailsTemplateService : IEmailsTemplateService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public EmailsTemplateService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<string>> CreateEmailsTemplate(EmailsTemplateDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.EmailsTemplatesName))
          throw new CustomException("Email Template Name is required.");

        var duplicate = await _unitOfWork.Repository<EmailsTemplate>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.EmailsTemplatesName.ToLower() == dto.EmailsTemplatesName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Email Template already exists.");

        EmailsTemplate emailTemplate = new EmailsTemplate
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          EmailsTemplatesName = dto.EmailsTemplatesName,
          EmailsTemplatesCode = dto.EmailsTemplatesCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<EmailsTemplate>()
            .AddAsync(emailTemplate);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "EmailsTemplate",
            "INSERT",
            emailTemplate.EmailsTemplatesId,
            "",
            JsonConvert.SerializeObject(emailTemplate),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Email Template Created Successfully",
          Data = emailTemplate.EmailsTemplatesName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating email template");
        throw;
      }
    }
    public async Task<ApiResponse<string>> UpdateEmailsTemplate(EmailsTemplateDto dto)
    {
      try
      {
        var emailTemplate = (await _unitOfWork.Repository<EmailsTemplate>()
            .FindAsync(x => x.EmailsTemplatesId == dto.EmailsTemplatesId))
            .FirstOrDefault();

        if (emailTemplate == null)
          throw new CustomException("Email Template not found.");

        var duplicate = await _unitOfWork.Repository<EmailsTemplate>()
            .FindAsync(x =>
                x.EmailsTemplatesId != dto.EmailsTemplatesId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.EmailsTemplatesName.ToLower() == dto.EmailsTemplatesName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Email Template already exists.");

        string oldValues = JsonConvert.SerializeObject(emailTemplate);

        emailTemplate.CompanyId = dto.CompanyId;
        emailTemplate.RegionId = dto.RegionId;
        emailTemplate.EmailsTemplatesName = dto.EmailsTemplatesName;
        emailTemplate.EmailsTemplatesCode = dto.EmailsTemplatesCode;
        emailTemplate.Description = dto.Description;
        emailTemplate.IsActive = dto.IsActive;
        emailTemplate.ModifiedBy = _currentUserService.UserId;
        emailTemplate.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<EmailsTemplate>().Update(emailTemplate);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "EmailsTemplate",
            "UPDATE",
            emailTemplate.EmailsTemplatesId,
            oldValues,
            JsonConvert.SerializeObject(emailTemplate),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Email Template Updated Successfully",
          Data = emailTemplate.EmailsTemplatesName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating email template");
        throw;
      }
    }

    public async Task<ApiResponse<string>> DeleteEmailsTemplate(int id)
    {
      try
      {
        var emailTemplate = (await _unitOfWork.Repository<EmailsTemplate>()
            .FindAsync(x => x.EmailsTemplatesId == id))
            .FirstOrDefault();

        if (emailTemplate == null)
          throw new CustomException("Email Template not found.");

        string oldValues = JsonConvert.SerializeObject(emailTemplate);

        emailTemplate.IsDeleted = true;
        emailTemplate.ModifiedBy = _currentUserService.UserId;
        emailTemplate.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<EmailsTemplate>().Update(emailTemplate);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "EmailsTemplate",
            "DELETE",
            emailTemplate.EmailsTemplatesId,
            oldValues,
            JsonConvert.SerializeObject(emailTemplate),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Email Template Deleted Successfully",
          Data = emailTemplate.EmailsTemplatesName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting email template");
        throw;
      }
    }

    public async Task<ApiResponse<List<EmailsTemplateDto>>> GetEmailsTemplates()
    {
      try
      {
        var emailTemplates = await _unitOfWork.Repository<EmailsTemplate>()
            .GetAllAsync();

        var result = emailTemplates
            .Where(x => !x.IsDeleted)
            .Select(x => new EmailsTemplateDto
            {
              EmailsTemplatesId = x.EmailsTemplatesId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              EmailsTemplatesName = x.EmailsTemplatesName,
              EmailsTemplatesCode = x.EmailsTemplatesCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<EmailsTemplateDto>>
        {
          Success = true,
          Message = "Email Templates Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting email templates");
        throw;
      }
    }

    public async Task<ApiResponse<EmailsTemplateDto>> GetEmailsTemplateById(int id)
    {
      try
      {
        var emailTemplate = (await _unitOfWork.Repository<EmailsTemplate>()
            .FindAsync(x => x.EmailsTemplatesId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (emailTemplate == null)
          throw new CustomException("Email Template not found.");

        var result = new EmailsTemplateDto
        {
          EmailsTemplatesId = emailTemplate.EmailsTemplatesId,
          CompanyId = emailTemplate.CompanyId,
          RegionId = emailTemplate.RegionId,
          EmailsTemplatesName = emailTemplate.EmailsTemplatesName,
          EmailsTemplatesCode = emailTemplate.EmailsTemplatesCode,
          Description = emailTemplate.Description,
          IsActive = emailTemplate.IsActive
        };

        return new ApiResponse<EmailsTemplateDto>
        {
          Success = true,
          Message = "Email Template Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting email template by id");
        throw;
      }
    }
  }
}
