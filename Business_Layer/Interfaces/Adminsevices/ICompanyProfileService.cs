using Business_Layer.DTOs.Admin;
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
    }
}
