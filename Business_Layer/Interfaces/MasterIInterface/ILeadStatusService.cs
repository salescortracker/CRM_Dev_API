using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface ILeadStatusService
  {
    Task<ApiResponse<string>> CreateLeadStatus(LeadStatusDto dto);

    Task<ApiResponse<string>> UpdateLeadStatus(LeadStatusDto dto);

    Task<ApiResponse<string>> DeleteLeadStatus(int id);

    Task<ApiResponse<List<LeadStatusDto>>> GetLeadStatuses();

    Task<ApiResponse<LeadStatusDto>> GetLeadStatusById(int id);
  }
}
