using Business_Layer.DTOs.Roles;
using Shared.CommonModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.Services
{
    public interface IRoleService
    {
        Task<ApiResponse<string>> CreateRole(RoleDto dto);

        Task<ApiResponse<string>> UpdateRole(RoleDto dto);

        Task<ApiResponse<string>> DeleteRole(int id);

        Task<ApiResponse<List<RoleDto>>> GetRoles();

        Task<ApiResponse<RoleDto>> GetRoleById(int id);
    }
}
