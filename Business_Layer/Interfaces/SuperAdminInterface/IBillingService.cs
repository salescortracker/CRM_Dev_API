using Business_Layer.DTOs.SuperAdmin;
using Shared.CommonModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.SuperAdminInterface
{
    public interface IBillingService
    {
        Task<ApiResponse<string>> CreateBilling(BillingDto dto);

        Task<ApiResponse<string>> UpdateBilling(BillingDto dto);

        Task<ApiResponse<string>> DeleteBilling(int id);

        Task<ApiResponse<List<BillingDto>>> GetBillings();

        Task<ApiResponse<BillingDto>> GetBillingById(int id);
    }
}
