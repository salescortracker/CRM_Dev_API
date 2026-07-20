using Business_Layer.DTOs.SuperAdmin;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.SuperAdminInterface
{
    public interface IOrganizationService
    {
        Task<ApiResponse<string>> CreateOrganization(OrganizationDto dto);

        Task<ApiResponse<string>> UpdateOrganization(OrganizationDto dto);

        Task<ApiResponse<string>> DeleteOrganization(int id);

        Task<ApiResponse<List<OrganizationDto>>> GetOrganizations();

        Task<ApiResponse<OrganizationDto>> GetOrganizationById(int id);
    }
}
