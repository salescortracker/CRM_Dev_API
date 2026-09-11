using Business_Layer.DTOs.MasterDTO_s;
using Business_Layer.DTOs.SuperAdmin;
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

        #region Branch

        Task<ApiResponse<string>> CreateBranch(BranchDto dto);

        Task<ApiResponse<string>> UpdateBranch(BranchDto dto);

        Task<ApiResponse<string>> DeleteBranch(int id);

        Task<ApiResponse<List<BranchDto>>> GetBranches();

        Task<ApiResponse<BranchDto>> GetBranchById(int id);

        #endregion

        #region Company Administrator

        Task<ApiResponse<string>> CreateCompanyAdministrator(
            CompanyAdministratorDto dto);

        Task<ApiResponse<string>> UpdateCompanyAdministrator(
            CompanyAdministratorDto dto);

        Task<ApiResponse<string>> DeleteCompanyAdministrator(
            int id);

        Task<ApiResponse<List<CompanyAdministratorDto>>>
            GetCompanyAdministrators();

        Task<ApiResponse<CompanyAdministratorDto>>
            GetCompanyAdministratorById(int id);

        #endregion

        #region Business Unit

        Task<ApiResponse<string>> CreateBusinessUnit(
            BusinessUnitDto dto);

        Task<ApiResponse<string>> UpdateBusinessUnit(
            BusinessUnitDto dto);

        Task<ApiResponse<string>> DeleteBusinessUnit(
            int id);

        Task<ApiResponse<List<BusinessUnitDto>>>
            GetBusinessUnits();

        Task<ApiResponse<BusinessUnitDto>>
            GetBusinessUnitById(int id);

        #endregion


    }
}
