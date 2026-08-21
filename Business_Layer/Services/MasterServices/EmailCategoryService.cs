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
    public class EmailCategoryService : IEmailCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public EmailCategoryService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }
        public async Task<ApiResponse<string>> CreateEmailCategory(EmailCategoryDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.EmailCategoryName))
                    throw new CustomException("Email Category Name is required.");

                var duplicate = await _unitOfWork.Repository<EmailCategory>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.EmailCategoryName.ToLower() == dto.EmailCategoryName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Email Category already exists.");

                EmailCategory emailCategory = new EmailCategory
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    EmailCategoryName = dto.EmailCategoryName,
                    EmailCategoryCode = dto.EmailCategoryCode,
                    Description = dto.Description,
                    IsActive = dto.IsActive,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<EmailCategory>()
                    .AddAsync(emailCategory);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "EmailCategory",
                    "INSERT",
                    emailCategory.EmailCategoryId,
                    "",
                    JsonConvert.SerializeObject(emailCategory),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email Category Created Successfully",
                    Data = emailCategory.EmailCategoryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating email category");
                throw;
            }
        }
        public async Task<ApiResponse<string>> UpdateEmailCategory(EmailCategoryDto dto)
        {
            try
            {
                var emailCategory = (await _unitOfWork.Repository<EmailCategory>()
                    .FindAsync(x => x.EmailCategoryId == dto.EmailCategoryId))
                    .FirstOrDefault();

                if (emailCategory == null)
                    throw new CustomException("Email Category not found.");

                var duplicate = await _unitOfWork.Repository<EmailCategory>()
                    .FindAsync(x =>
                        x.EmailCategoryId != dto.EmailCategoryId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.EmailCategoryName.ToLower() == dto.EmailCategoryName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Email Category already exists.");

                string oldValues = JsonConvert.SerializeObject(emailCategory);

                emailCategory.CompanyId = dto.CompanyId;
                emailCategory.RegionId = dto.RegionId;
                emailCategory.EmailCategoryName = dto.EmailCategoryName;
                emailCategory.EmailCategoryCode = dto.EmailCategoryCode;
                emailCategory.Description = dto.Description;
                emailCategory.IsActive = dto.IsActive;
                emailCategory.ModifiedBy = _currentUserService.UserId;
                emailCategory.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<EmailCategory>().Update(emailCategory);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "EmailCategory",
                    "UPDATE",
                    emailCategory.EmailCategoryId,
                    oldValues,
                    JsonConvert.SerializeObject(emailCategory),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email Category Updated Successfully",
                    Data = emailCategory.EmailCategoryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating email category");
                throw;
            }
        }
        public async Task<ApiResponse<string>> DeleteEmailCategory(int id)
        {
            try
            {
                var emailCategory = (await _unitOfWork.Repository<EmailCategory>()
                    .FindAsync(x => x.EmailCategoryId == id))
                    .FirstOrDefault();

                if (emailCategory == null)
                    throw new CustomException("Email Category not found.");

                string oldValues = JsonConvert.SerializeObject(emailCategory);

                emailCategory.IsDeleted = true;
                emailCategory.ModifiedBy = _currentUserService.UserId;
                emailCategory.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<EmailCategory>().Update(emailCategory);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "EmailCategory",
                    "DELETE",
                    emailCategory.EmailCategoryId,
                    oldValues,
                    JsonConvert.SerializeObject(emailCategory),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email Category Deleted Successfully",
                    Data = emailCategory.EmailCategoryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting email category");
                throw;
            }
        }

        public async Task<ApiResponse<List<EmailCategoryDto>>> GetEmailCategories()
        {
            try
            {
                var emailCategories = await _unitOfWork.Repository<EmailCategory>()
                    .GetAllAsync();

                var result = emailCategories
                    .Where(x => !x.IsDeleted)
                    .Select(x => new EmailCategoryDto
                    {
                        EmailCategoryId = x.EmailCategoryId,
                        CompanyId = x.CompanyId,
                        RegionId = x.RegionId,
                        EmailCategoryName = x.EmailCategoryName,
                        EmailCategoryCode = x.EmailCategoryCode,
                        Description = x.Description,
                        IsActive = x.IsActive
                    })
                    .ToList();

                return new ApiResponse<List<EmailCategoryDto>>
                {
                    Success = true,
                    Message = "Email Categories Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting email categories");
                throw;
            }
        }

        public async Task<ApiResponse<EmailCategoryDto>> GetEmailCategoryById(int id)
        {
            try
            {
                var emailCategory = (await _unitOfWork.Repository<EmailCategory>()
                    .FindAsync(x => x.EmailCategoryId == id && !x.IsDeleted))
                    .FirstOrDefault();

                if (emailCategory == null)
                    throw new CustomException("Email Category not found.");

                var result = new EmailCategoryDto
                {
                    EmailCategoryId = emailCategory.EmailCategoryId,
                    CompanyId = emailCategory.CompanyId,
                    RegionId = emailCategory.RegionId,
                    EmailCategoryName = emailCategory.EmailCategoryName,
                    EmailCategoryCode = emailCategory.EmailCategoryCode,
                    Description = emailCategory.Description,
                    IsActive = emailCategory.IsActive
                };

                return new ApiResponse<EmailCategoryDto>
                {
                    Success = true,
                    Message = "Email Category Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting email category by id");
                throw;
            }
        }
    }
}