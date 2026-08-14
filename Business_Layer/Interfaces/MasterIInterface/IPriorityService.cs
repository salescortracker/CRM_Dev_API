using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface IPriorityService
  {
    Task<ApiResponse<string>> CreatePriority(PriorityDto dto);

    Task<ApiResponse<string>> UpdatePriority(PriorityDto dto);

    Task<ApiResponse<string>> DeletePriority(int id);

    Task<ApiResponse<List<PriorityDto>>> GetPriorities();

    Task<ApiResponse<PriorityDto>> GetPriorityById(int id);
  }
}
