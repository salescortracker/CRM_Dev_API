using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface ICurrencyService
  {
    Task<ApiResponse<string>> CreateCurrency(CurrencyDto dto);

    Task<ApiResponse<string>> UpdateCurrency(CurrencyDto dto);

    Task<ApiResponse<string>> DeleteCurrency(int id);

    Task<ApiResponse<List<CurrencyDto>>> GetCurrencies();

    Task<ApiResponse<CurrencyDto>> GetCurrencyById(int id);
  }
}
