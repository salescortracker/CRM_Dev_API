using Business_Layer.DTOs.SuperAdmin;
using Shared.CommonModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.SuperAdminInterface
{
    public interface ICompanySubscriptionService
    {
        Task<ApiResponse<string>> CreateCompanySubscription(CompanySubscriptionDto dto);

        Task<ApiResponse<string>> UpdateCompanySubscription(CompanySubscriptionDto dto);

        Task<ApiResponse<string>> DeleteCompanySubscription(int id);

        Task<ApiResponse<List<CompanySubscriptionDto>>> GetCompanySubscriptions();

        Task<ApiResponse<CompanySubscriptionDto>> GetCompanySubscriptionById(int id);
    }
}
