using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface IPaymentMethodService
  {
    Task<ApiResponse<string>> CreatePaymentMethod(PaymentMethodDto dto);

    Task<ApiResponse<string>> UpdatePaymentMethod(PaymentMethodDto dto);

    Task<ApiResponse<string>> DeletePaymentMethod(int id);

    Task<ApiResponse<List<PaymentMethodDto>>> GetPaymentMethods();

    Task<ApiResponse<PaymentMethodDto>> GetPaymentMethodById(int id);
  }
}
