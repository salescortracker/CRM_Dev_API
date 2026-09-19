using Business_Layer.DTOs.SuperAdmin;
using Shared.CommonModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.SuperAdminInterface
{
    public interface ICouponService
    {
        Task<ApiResponse<string>> CreateCoupon(CouponDto dto);

        Task<ApiResponse<string>> UpdateCoupon(CouponDto dto);

        Task<ApiResponse<string>> DeleteCoupon(int id);

        Task<ApiResponse<List<CouponDto>>> GetCoupons();

        Task<ApiResponse<CouponDto>> GetCouponById(int id);
    }
}
