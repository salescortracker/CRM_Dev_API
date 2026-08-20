using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface ILeadTypeService
  {
    Task<ApiResponse<string>> CreateLeadType(LeadTypeDto dto);

    Task<ApiResponse<string>> UpdateLeadType(LeadTypeDto dto);

    Task<ApiResponse<string>> DeleteLeadType(int id);

    Task<ApiResponse<List<LeadTypeDto>>> GetLeadTypes();

    Task<ApiResponse<LeadTypeDto>> GetLeadTypeById(int id);
  }
}
