using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface IFiscalTypeService
  {
    Task<ApiResponse<string>> CreateFiscalType(FiscalTypeDto dto);

    Task<ApiResponse<string>> UpdateFiscalType(FiscalTypeDto dto);

    Task<ApiResponse<string>> DeleteFiscalType(int id);

    Task<ApiResponse<List<FiscalTypeDto>>> GetFiscalTypes();

    Task<ApiResponse<FiscalTypeDto>> GetFiscalTypeById(int id);
  }
}
