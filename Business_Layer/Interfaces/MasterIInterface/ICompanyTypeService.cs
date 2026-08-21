using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface ICompanyTypeService
    {
        Task<ApiResponse<string>> CreateCompanyType(CompanyTypeDto dto);

        Task<ApiResponse<string>> UpdateCompanyType(CompanyTypeDto dto);

        Task<ApiResponse<string>> DeleteCompanyType(int id);

        Task<ApiResponse<List<CompanyTypeDto>>> GetCompanyTypes();

        Task<ApiResponse<CompanyTypeDto>> GetCompanyTypeById(int id);
    }
}