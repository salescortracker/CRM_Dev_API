using Business_Layer.DTOs.User;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.User
{
    public interface ICompanyInformationService
    {
        Task<ApiResponse<string>> CreateCompany(CompanyInformationDto dto);

        Task<ApiResponse<string>> UpdateCompany(CompanyInformationDto dto);

        Task<ApiResponse<string>> DeleteCompany(int id);

        Task<ApiResponse<List<CompanyInformationDto>>> GetCompanies();

        Task<ApiResponse<CompanyInformationDto>> GetCompanyById(int id);
    }
}
