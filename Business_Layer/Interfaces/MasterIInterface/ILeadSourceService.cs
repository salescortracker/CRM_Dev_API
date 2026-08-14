using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface ILeadSourceService
  {
    Task<ApiResponse<string>> CreateLeadSource(LeadSourceDto dto);

    Task<ApiResponse<string>> UpdateLeadSource(LeadSourceDto dto);

    Task<ApiResponse<string>> DeleteLeadSource(int id);

    Task<ApiResponse<List<LeadSourceDto>>> GetLeadSources();

    Task<ApiResponse<LeadSourceDto>> GetLeadSourceById(int id);
  }
}
