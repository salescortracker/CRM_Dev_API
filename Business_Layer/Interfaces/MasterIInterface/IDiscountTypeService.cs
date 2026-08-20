using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface IDiscountTypeService
  {
    Task<ApiResponse<string>> CreateDiscountType(DiscountTypeDto dto);

    Task<ApiResponse<string>> UpdateDiscountType(DiscountTypeDto dto);

    Task<ApiResponse<string>> DeleteDiscountType(int id);

    Task<ApiResponse<List<DiscountTypeDto>>> GetDiscountTypes();

    Task<ApiResponse<DiscountTypeDto>> GetDiscountTypeById(int id);
  }
}
