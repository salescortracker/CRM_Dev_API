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
    public class RelationshipService : IRelationshipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public RelationshipService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }
        public async Task<ApiResponse<string>> CreateRelationship(RelationshipDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.RelationshipName))
                    throw new CustomException("Relationship Name is required.");

                var duplicate = await _unitOfWork.Repository<Relationship>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.RelationshipName.ToLower() == dto.RelationshipName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Relationship already exists.");

                Relationship relationship = new Relationship
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    RelationshipName = dto.RelationshipName,
                    RelationshipCode = dto.RelationshipCode,
                    Description = dto.Description,
                    IsActive = dto.IsActive,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<Relationship>()
                    .AddAsync(relationship);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Relationship",
                    "INSERT",
                    relationship.RelationshipId,
                    "",
                    JsonConvert.SerializeObject(relationship),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Relationship Created Successfully",
                    Data = relationship.RelationshipName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating relationship");
                throw;
            }
        }
        public async Task<ApiResponse<string>> UpdateRelationship(RelationshipDto dto)
        {
            try
            {
                var relationship = (await _unitOfWork.Repository<Relationship>()
                    .FindAsync(x => x.RelationshipId == dto.RelationshipId))
                    .FirstOrDefault();

                if (relationship == null)
                    throw new CustomException("Relationship not found.");

                var duplicate = await _unitOfWork.Repository<Relationship>()
                    .FindAsync(x =>
                        x.RelationshipId != dto.RelationshipId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.RelationshipName.ToLower() == dto.RelationshipName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Relationship already exists.");

                string oldValues = JsonConvert.SerializeObject(relationship);

                relationship.CompanyId = dto.CompanyId;
                relationship.RegionId = dto.RegionId;
                relationship.RelationshipName = dto.RelationshipName;
                relationship.RelationshipCode = dto.RelationshipCode;
                relationship.Description = dto.Description;
                relationship.IsActive = dto.IsActive;
                relationship.ModifiedBy = _currentUserService.UserId;
                relationship.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<Relationship>().Update(relationship);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Relationship",
                    "UPDATE",
                    relationship.RelationshipId,
                    oldValues,
                    JsonConvert.SerializeObject(relationship),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Relationship Updated Successfully",
                    Data = relationship.RelationshipName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating relationship");
                throw;
            }
        }
        public async Task<ApiResponse<string>> DeleteRelationship(int id)
        {
            try
            {
                var relationship = (await _unitOfWork.Repository<Relationship>()
                    .FindAsync(x => x.RelationshipId == id))
                    .FirstOrDefault();

                if (relationship == null)
                    throw new CustomException("Relationship not found.");

                string oldValues = JsonConvert.SerializeObject(relationship);

                relationship.IsDeleted = true;
                relationship.ModifiedBy = _currentUserService.UserId;
                relationship.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<Relationship>().Update(relationship);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Relationship",
                    "DELETE",
                    relationship.RelationshipId,
                    oldValues,
                    JsonConvert.SerializeObject(relationship),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Relationship Deleted Successfully",
                    Data = relationship.RelationshipName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting relationship");
                throw;
            }
        }

        public async Task<ApiResponse<List<RelationshipDto>>> GetRelationships()
        {
            try
            {
                var relationships = await _unitOfWork.Repository<Relationship>()
                    .GetAllAsync();

                var result = relationships
                    .Where(x => !x.IsDeleted)
                    .Select(x => new RelationshipDto
                    {
                        RelationshipId = x.RelationshipId,
                        CompanyId = x.CompanyId,
                        RegionId = x.RegionId,
                        RelationshipName = x.RelationshipName,
                        RelationshipCode = x.RelationshipCode,
                        Description = x.Description,
                        IsActive = x.IsActive
                    })
                    .ToList();

                return new ApiResponse<List<RelationshipDto>>
                {
                    Success = true,
                    Message = "Relationships Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting relationships");
                throw;
            }
        }

        public async Task<ApiResponse<RelationshipDto>> GetRelationshipById(int id)
        {
            try
            {
                var relationship = (await _unitOfWork.Repository<Relationship>()
                    .FindAsync(x => x.RelationshipId == id && !x.IsDeleted))
                    .FirstOrDefault();

                if (relationship == null)
                    throw new CustomException("Relationship not found.");

                var result = new RelationshipDto
                {
                    RelationshipId = relationship.RelationshipId,
                    CompanyId = relationship.CompanyId,
                    RegionId = relationship.RegionId,
                    RelationshipName = relationship.RelationshipName,
                    RelationshipCode = relationship.RelationshipCode,
                    Description = relationship.Description,
                    IsActive = relationship.IsActive
                };

                return new ApiResponse<RelationshipDto>
                {
                    Success = true,
                    Message = "Relationship Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting relationship by id");
                throw;
            }
        }
    }

}