using Business_Layer.DTOs.AccessPolicies;
using Shared.CommonModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.Services
{
    public interface IAccessPolicyService
    {
        Task<ApiResponse<string>> CreatePolicy(AccessPolicyDto dto);

        Task<ApiResponse<string>> UpdatePolicy(AccessPolicyDto dto);

        Task<ApiResponse<string>> DeletePolicy(int id);

        Task<ApiResponse<List<AccessPolicyDto>>> GetPolicies();

        Task<ApiResponse<AccessPolicyDto>> GetPolicyById(int id);
    }
}
