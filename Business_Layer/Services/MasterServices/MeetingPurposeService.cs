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
    public class MeetingPurposeService : IMeetingPurposeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public MeetingPurposeService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }
        public async Task<ApiResponse<string>> CreateMeetingPurpose(MeetingPurposeDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.MeetingPurposeName))
                    throw new CustomException("Meeting Purpose Name is required.");

                var duplicate = await _unitOfWork.Repository<MeetingPurpose>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.MeetingPurposeName.ToLower() == dto.MeetingPurposeName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Meeting Purpose already exists.");

                MeetingPurpose meetingPurpose = new MeetingPurpose
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    MeetingPurposeName = dto.MeetingPurposeName,
                    MeetingPurposeCode = dto.MeetingPurposeCode,
                    Description = dto.Description,
                    IsActive = dto.IsActive,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<MeetingPurpose>()
                    .AddAsync(meetingPurpose);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "MeetingPurpose",
                    "INSERT",
                    meetingPurpose.MeetingPurposeId,
                    "",
                    JsonConvert.SerializeObject(meetingPurpose),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Meeting Purpose Created Successfully",
                    Data = meetingPurpose.MeetingPurposeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating meeting purpose");
                throw;
            }
        }

        public async Task<ApiResponse<string>> UpdateMeetingPurpose(MeetingPurposeDto dto)
        {
            try
            {
                var meetingPurpose = (await _unitOfWork.Repository<MeetingPurpose>()
                    .FindAsync(x => x.MeetingPurposeId == dto.MeetingPurposeId))
                    .FirstOrDefault();

                if (meetingPurpose == null)
                    throw new CustomException("Meeting Purpose not found.");

                var duplicate = await _unitOfWork.Repository<MeetingPurpose>()
                    .FindAsync(x =>
                        x.MeetingPurposeId != dto.MeetingPurposeId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.MeetingPurposeName.ToLower() == dto.MeetingPurposeName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Meeting Purpose already exists.");

                string oldValues = JsonConvert.SerializeObject(meetingPurpose);

                meetingPurpose.CompanyId = dto.CompanyId;
                meetingPurpose.RegionId = dto.RegionId;
                meetingPurpose.MeetingPurposeName = dto.MeetingPurposeName;
                meetingPurpose.MeetingPurposeCode = dto.MeetingPurposeCode;
                meetingPurpose.Description = dto.Description;
                meetingPurpose.IsActive = dto.IsActive;
                meetingPurpose.ModifiedBy = _currentUserService.UserId;
                meetingPurpose.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<MeetingPurpose>().Update(meetingPurpose);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "MeetingPurpose",
                    "UPDATE",
                    meetingPurpose.MeetingPurposeId,
                    oldValues,
                    JsonConvert.SerializeObject(meetingPurpose),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Meeting Purpose Updated Successfully",
                    Data = meetingPurpose.MeetingPurposeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating meeting purpose");
                throw;
            }
        }
        public async Task<ApiResponse<string>> DeleteMeetingPurpose(int id)
        {
            try
            {
                var meetingPurpose = (await _unitOfWork.Repository<MeetingPurpose>()
                    .FindAsync(x => x.MeetingPurposeId == id))
                    .FirstOrDefault();

                if (meetingPurpose == null)
                    throw new CustomException("Meeting Purpose not found.");

                string oldValues = JsonConvert.SerializeObject(meetingPurpose);

                meetingPurpose.IsDeleted = true;
                meetingPurpose.ModifiedBy = _currentUserService.UserId;
                meetingPurpose.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<MeetingPurpose>().Update(meetingPurpose);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "MeetingPurpose",
                    "DELETE",
                    meetingPurpose.MeetingPurposeId,
                    oldValues,
                    JsonConvert.SerializeObject(meetingPurpose),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Meeting Purpose Deleted Successfully",
                    Data = meetingPurpose.MeetingPurposeName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting meeting purpose");
                throw;
            }
        }
        public async Task<ApiResponse<List<MeetingPurposeDto>>> GetMeetingPurposes()
        {
            try
            {
                var meetingPurposes = await _unitOfWork.Repository<MeetingPurpose>()
                    .GetAllAsync();

                var result = meetingPurposes
                    .Where(x => !x.IsDeleted)
                    .Select(x => new MeetingPurposeDto
                    {
                        MeetingPurposeId = x.MeetingPurposeId,
                        CompanyId = x.CompanyId,
                        RegionId = x.RegionId,
                        MeetingPurposeName = x.MeetingPurposeName,
                        MeetingPurposeCode = x.MeetingPurposeCode,
                        Description = x.Description,
                        IsActive = x.IsActive
                    })
                    .ToList();

                return new ApiResponse<List<MeetingPurposeDto>>
                {
                    Success = true,
                    Message = "Meeting Purposes Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting meeting purposes");
                throw;
            }
        }
        public async Task<ApiResponse<MeetingPurposeDto>> GetMeetingPurposeById(int id)
        {
            try
            {
                var meetingPurpose = (await _unitOfWork.Repository<MeetingPurpose>()
                    .FindAsync(x => x.MeetingPurposeId == id && !x.IsDeleted))
                    .FirstOrDefault();

                if (meetingPurpose == null)
                    throw new CustomException("Meeting Purpose not found.");

                var result = new MeetingPurposeDto
                {
                    MeetingPurposeId = meetingPurpose.MeetingPurposeId,
                    CompanyId = meetingPurpose.CompanyId,
                    RegionId = meetingPurpose.RegionId,
                    MeetingPurposeName = meetingPurpose.MeetingPurposeName,
                    MeetingPurposeCode = meetingPurpose.MeetingPurposeCode,
                    Description = meetingPurpose.Description,
                    IsActive = meetingPurpose.IsActive
                };

                return new ApiResponse<MeetingPurposeDto>
                {
                    Success = true,
                    Message = "Meeting Purpose Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting meeting purpose by id");
                throw;
            }
        }
    }
}