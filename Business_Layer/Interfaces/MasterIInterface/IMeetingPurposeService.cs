using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface IMeetingPurposeService
    {
        Task<ApiResponse<string>> CreateMeetingPurpose(MeetingPurposeDto dto);

        Task<ApiResponse<string>> UpdateMeetingPurpose(MeetingPurposeDto dto);

        Task<ApiResponse<string>> DeleteMeetingPurpose(int id);

        Task<ApiResponse<List<MeetingPurposeDto>>> GetMeetingPurposes();

        Task<ApiResponse<MeetingPurposeDto>> GetMeetingPurposeById(int id);
    }
}