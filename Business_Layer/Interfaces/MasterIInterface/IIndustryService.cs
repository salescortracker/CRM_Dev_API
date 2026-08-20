using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface IIndustryService
  {
    Task<ApiResponse<string>> CreateIndustry(IndustryDto dto);

    Task<ApiResponse<string>> UpdateIndustry(IndustryDto dto);

    Task<ApiResponse<string>> DeleteIndustry(int id);

    Task<ApiResponse<List<IndustryDto>>> GetIndustries();

    Task<ApiResponse<IndustryDto>> GetIndustryById(int id);
  }
}
