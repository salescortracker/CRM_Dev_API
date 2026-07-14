using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;


namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface ICompanyAndRegionService
    {
        Task<ApiResponse<string>> CreateCompany(CompanyDto dto);

        Task<ApiResponse<string>> UpdateCompany(CompanyDto dto);

        Task<ApiResponse<string>> DeleteCompany(int id);

        Task<ApiResponse<List<CompanyDto>>> GetCompanies();

        Task<ApiResponse<CompanyDto>> GetCompanyById(int id);
        Task<ApiResponse<string>> CreateRegion(RegionDto dto);

        Task<ApiResponse<string>> UpdateRegion(RegionDto dto);

        Task<ApiResponse<string>> DeleteRegion(int id);

        Task<ApiResponse<List<RegionDto>>> GetRegions();

        Task<ApiResponse<RegionDto>> GetRegionById(int id);
    }
}
