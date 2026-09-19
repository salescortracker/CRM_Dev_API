using Business_Layer.DTOs.SuperAdmin;
using Shared.CommonModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.SuperAdminInterface
{
    public interface IPaymentTrackingService
    {
        Task<ApiResponse<string>> CreatePaymentTracking(PaymentTrackingDto dto);

        Task<ApiResponse<string>> UpdatePaymentTracking(PaymentTrackingDto dto);

        Task<ApiResponse<string>> DeletePaymentTracking(int id);

        Task<ApiResponse<List<PaymentTrackingDto>>> GetPaymentTrackings();

        Task<ApiResponse<PaymentTrackingDto>> GetPaymentTrackingById(int id);

        Task<ApiResponse<string>> RefundPaymentTracking(int id, decimal refundAmount, string? refundReason);
    }
}
