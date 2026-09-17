using Business_Layer.DTOs.Admin;
using DataAccess_Layers.Entities;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.Adminsevices
{
    public interface ICompanyProfileService
    {
        #region Company Profile

        Task<ApiResponse<string>> CreateCompanyProfile(
            CompanyProfileDto dto);

        Task<ApiResponse<string>> UpdateCompanyProfile(
            CompanyProfileDto dto);

        Task<ApiResponse<string>> DeleteCompanyProfile(
            int id);

        Task<ApiResponse<List<CompanyProfileDto>>>
            GetCompanyProfiles();

        Task<ApiResponse<CompanyProfileDto>>
            GetCompanyProfileById(int id);

        #endregion

        #region User Group
        Task<ApiResponse<string>> CreateUserGroup(
            UserGroupDto dto);

        Task<ApiResponse<string>> UpdateUserGroup(
            UserGroupDto dto);

        Task<ApiResponse<string>> DeleteUserGroup(
            int id);

        Task<ApiResponse<List<UserGroupDto>>> GetUserGroups();

        Task<ApiResponse<UserGroupDto>> GetUserGroupById(
            int id);
        #endregion

        #region License Management
        Task<ApiResponse<string>> CreateLicenseManagement(
           LicenseManagementDto dto);

        Task<ApiResponse<string>> UpdateLicenseManagement(
            LicenseManagementDto dto);

        Task<ApiResponse<string>> DeleteLicenseManagement(
            int id);

        Task<ApiResponse<List<LicenseManagementDto>>>
            GetLicenseManagements();

        Task<ApiResponse<LicenseManagementDto>>
            GetLicenseManagementById(int id);
        #endregion

        #region Password Policy
        Task<ApiResponse<string>> CreatePasswordPolicy(
           PasswordPolicyDto dto);

        Task<ApiResponse<string>> UpdatePasswordPolicy(
            PasswordPolicyDto dto);

        Task<ApiResponse<string>> DeletePasswordPolicy(
            int id);

        Task<ApiResponse<List<PasswordPolicyDto>>>
            GetPasswordPolicies();

        Task<ApiResponse<PasswordPolicyDto>>
            GetPasswordPolicyById(int id);
        #endregion
    }
}
