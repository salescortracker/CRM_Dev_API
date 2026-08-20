using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface ILicenseService
  {
    Task<ApiResponse<string>> CreateLicense(LicenseDto dto);

    Task<ApiResponse<string>> UpdateLicense(LicenseDto dto);

    Task<ApiResponse<string>> DeleteLicense(int id);

    Task<ApiResponse<List<LicenseDto>>> GetLicenses();

    Task<ApiResponse<LicenseDto>> GetLicenseById(int id);
  }
}
