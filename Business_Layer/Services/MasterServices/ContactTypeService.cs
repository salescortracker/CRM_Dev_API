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
    public class ContactTypeService : IContactTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public ContactTypeService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }
        public async Task<ApiResponse<string>> CreateContactType(ContactTypeDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.ContactTypeName))
                    throw new CustomException("Contact Type Name is required.");

                var duplicate = await _unitOfWork.Repository<ContactType>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.ContactTypeName.ToLower() == dto.ContactTypeName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Contact Type already exists.");

                ContactType contactType = new ContactType
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    ContactTypeName = dto.ContactTypeName,
                    ContactTypeCode = dto.ContactTypeCode,
                    Description = dto.Description,
                    IsActive = dto.IsActive,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<ContactType>()
                    .AddAsync(contactType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "ContactType",
                    "INSERT",
                    contactType.ContactTypeId,
                    "",
                    JsonConvert.SerializeObject(contactType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Contact Type Created Successfully",
                    Data = contactType.ContactTypeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating contact type");
                throw;
            }
        }
        public async Task<ApiResponse<string>> UpdateContactType(ContactTypeDto dto)
        {
            try
            {
                var contactType = (await _unitOfWork.Repository<ContactType>()
                    .FindAsync(x => x.ContactTypeId == dto.ContactTypeId))
                    .FirstOrDefault();

                if (contactType == null)
                    throw new CustomException("Contact Type not found.");

                var duplicate = await _unitOfWork.Repository<ContactType>()
                    .FindAsync(x =>
                        x.ContactTypeId != dto.ContactTypeId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.ContactTypeName.ToLower() == dto.ContactTypeName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Contact Type already exists.");

                string oldValues = JsonConvert.SerializeObject(contactType);

                contactType.CompanyId = dto.CompanyId;
                contactType.RegionId = dto.RegionId;
                contactType.ContactTypeName = dto.ContactTypeName;
                contactType.ContactTypeCode = dto.ContactTypeCode;
                contactType.Description = dto.Description;
                contactType.IsActive = dto.IsActive;
                contactType.ModifiedBy = _currentUserService.UserId;
                contactType.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<ContactType>().Update(contactType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "ContactType",
                    "UPDATE",
                    contactType.ContactTypeId,
                    oldValues,
                    JsonConvert.SerializeObject(contactType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Contact Type Updated Successfully",
                    Data = contactType.ContactTypeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating contact type");
                throw;
            }
        }
        public async Task<ApiResponse<string>> DeleteContactType(int id)
        {
            try
            {
                var contactType = (await _unitOfWork.Repository<ContactType>()
                    .FindAsync(x => x.ContactTypeId == id))
                    .FirstOrDefault();

                if (contactType == null)
                    throw new CustomException("Contact Type not found.");

                string oldValues = JsonConvert.SerializeObject(contactType);

                contactType.IsDeleted = true;
                contactType.ModifiedBy = _currentUserService.UserId;
                contactType.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<ContactType>().Update(contactType);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "ContactType",
                    "DELETE",
                    contactType.ContactTypeId,
                    oldValues,
                    JsonConvert.SerializeObject(contactType),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Contact Type Deleted Successfully",
                    Data = contactType.ContactTypeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting contact type");
                throw;
            }
        }

        public async Task<ApiResponse<List<ContactTypeDto>>> GetContactTypes()
        {
            try
            {
                var contactTypes = await _unitOfWork.Repository<ContactType>()
                    .GetAllAsync();

                var result = contactTypes
                    .Where(x => !x.IsDeleted)
                    .Select(x => new ContactTypeDto
                    {
                        ContactTypeId = x.ContactTypeId,
                        CompanyId = x.CompanyId,
                        RegionId = x.RegionId,
                        ContactTypeName = x.ContactTypeName,
                        ContactTypeCode = x.ContactTypeCode,
                        Description = x.Description,
                        IsActive = x.IsActive
                    })
                    .ToList();

                return new ApiResponse<List<ContactTypeDto>>
                {
                    Success = true,
                    Message = "Contact Types Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting contact types");
                throw;
            }
        }
        public async Task<ApiResponse<ContactTypeDto>> GetContactTypeById(int id)
        {
            try
            {
                var contactType = (await _unitOfWork.Repository<ContactType>()
                    .FindAsync(x => x.ContactTypeId == id && !x.IsDeleted))
                    .FirstOrDefault();

                if (contactType == null)
                    throw new CustomException("Contact Type not found.");

                var result = new ContactTypeDto
                {
                    ContactTypeId = contactType.ContactTypeId,
                    CompanyId = contactType.CompanyId,
                    RegionId = contactType.RegionId,
                    ContactTypeName = contactType.ContactTypeName,
                    ContactTypeCode = contactType.ContactTypeCode,
                    Description = contactType.Description,
                    IsActive = contactType.IsActive
                };

                return new ApiResponse<ContactTypeDto>
                {
                    Success = true,
                    Message = "Contact Type Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting contact type by id");
                throw;
            }
        }

    }

}