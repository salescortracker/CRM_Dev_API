using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface IBillingCycleService
  {
    Task<ApiResponse<string>> CreateBillingCycle(BillingCycleDto dto);

    Task<ApiResponse<string>> UpdateBillingCycle(BillingCycleDto dto);

    Task<ApiResponse<string>> DeleteBillingCycle(int id);

    Task<ApiResponse<List<BillingCycleDto>>> GetBillingCycles();

    Task<ApiResponse<BillingCycleDto>> GetBillingCycleById(int id);
  }
}
