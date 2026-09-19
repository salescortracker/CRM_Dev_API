using Business_Layer.DTOs.SuperAdmin;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.SuperAdminInterface;
using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Newtonsoft.Json;
using Serilog;
using Shared.CommonModels;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business_Layer.Services.SuperAdminServices
{
    public class CouponService : ICouponService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public CouponService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        #region CREATE

        public async Task<ApiResponse<string>> CreateCoupon(CouponDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CouponCode))
                    throw new CustomException("Coupon Code is required.");

                if (string.IsNullOrWhiteSpace(dto.CouponName))
                    throw new CustomException("Coupon Name is required.");

                var duplicate = await _unitOfWork.Repository<Coupon>()
                    .FindAsync(x =>
                        !x.IsDeleted &&
                        x.CouponCode.ToLower() == dto.CouponCode.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Coupon Code already exists.");

                Coupon coupon = new Coupon
                {
                    CouponCode = dto.CouponCode,
                    CouponName = dto.CouponName,
                    Description = dto.Description,
                    DiscountType = dto.DiscountType,
                    DiscountValue = dto.DiscountValue,
                    MinimumAmount = dto.MinimumAmount,
                    MaximumDiscount = dto.MaximumDiscount,
                    UsageLimit = dto.UsageLimit,
                    UsedCount = dto.UsedCount,
                    PlanId = dto.PlanId,
                    StartDate = dto.StartDate,
                    ExpiryDate = dto.ExpiryDate,
                    Status = dto.Status,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<Coupon>().AddAsync(coupon);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Coupon",
                    "INSERT",
                    coupon.CouponId,
                    "",
                    JsonConvert.SerializeObject(coupon),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Coupon Created Successfully.",
                    Data = coupon.CouponCode
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating Coupon");
                throw;
            }
        }

        #endregion

        #region UPDATE

        public async Task<ApiResponse<string>> UpdateCoupon(CouponDto dto)
        {
            try
            {
                var coupon = (await _unitOfWork.Repository<Coupon>()
                    .FindAsync(x => x.CouponId == dto.CouponId))
                    .FirstOrDefault();

                if (coupon == null)
                    throw new CustomException("Coupon not found.");

                var duplicate = await _unitOfWork.Repository<Coupon>()
                    .FindAsync(x =>
                        x.CouponId != dto.CouponId &&
                        !x.IsDeleted &&
                        x.CouponCode.ToLower() == dto.CouponCode.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Coupon Code already exists.");

                string oldValues = JsonConvert.SerializeObject(coupon);

                coupon.CouponCode = dto.CouponCode;
                coupon.CouponName = dto.CouponName;
                coupon.Description = dto.Description;
                coupon.DiscountType = dto.DiscountType;
                coupon.DiscountValue = dto.DiscountValue;
                coupon.MinimumAmount = dto.MinimumAmount;
                coupon.MaximumDiscount = dto.MaximumDiscount;
                coupon.UsageLimit = dto.UsageLimit;
                coupon.UsedCount = dto.UsedCount;
                coupon.PlanId = dto.PlanId;
                coupon.StartDate = dto.StartDate;
                coupon.ExpiryDate = dto.ExpiryDate;
                coupon.Status = dto.Status;

                coupon.ModifiedBy = _currentUserService.UserId;
                coupon.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<Coupon>().Update(coupon);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Coupon",
                    "UPDATE",
                    coupon.CouponId,
                    oldValues,
                    JsonConvert.SerializeObject(coupon),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Coupon Updated Successfully.",
                    Data = coupon.CouponCode
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating Coupon");
                throw;
            }
        }

        #endregion

        #region DELETE

        public async Task<ApiResponse<string>> DeleteCoupon(int id)
        {
            try
            {
                var coupon = (await _unitOfWork.Repository<Coupon>()
                    .FindAsync(x => x.CouponId == id))
                    .FirstOrDefault();

                if (coupon == null)
                    throw new CustomException("Coupon not found.");

                string oldValues = JsonConvert.SerializeObject(coupon);

                coupon.IsDeleted = true;
                coupon.ModifiedBy = _currentUserService.UserId;
                coupon.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<Coupon>().Update(coupon);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Coupon",
                    "DELETE",
                    coupon.CouponId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Coupon Deleted Successfully.",
                    Data = coupon.CouponCode
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting Coupon");
                throw;
            }
        }

        #endregion

        #region GET ALL

        public async Task<ApiResponse<List<CouponDto>>> GetCoupons()
        {
            var plans = await _unitOfWork.Repository<SubscriptionPlanMaster>().GetAllAsync();

            var coupons = (await _unitOfWork.Repository<Coupon>().GetAllAsync())
                .Where(x => !x.IsDeleted && x.CreatedBy == _currentUserService.UserId)
                .OrderByDescending(x => x.CouponId)
                .ToList();

            var result = coupons.Select(x => new CouponDto
            {
                CouponId = x.CouponId,
                CouponCode = x.CouponCode,
                CouponName = x.CouponName,
                Description = x.Description,
                DiscountType = x.DiscountType,
                DiscountValue = x.DiscountValue,
                MinimumAmount = x.MinimumAmount,
                MaximumDiscount = x.MaximumDiscount,
                UsageLimit = x.UsageLimit,
                UsedCount = x.UsedCount,
                PlanId = x.PlanId,
                PlanName = x.PlanId.HasValue
                    ? plans.FirstOrDefault(p => p.PlanId == x.PlanId.Value)?.PlanName
                    : "All Plans",
                StartDate = x.StartDate,
                ExpiryDate = x.ExpiryDate,
                Status = x.Status
            }).ToList();

            return new ApiResponse<List<CouponDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        #endregion

        #region GET BY ID

        public async Task<ApiResponse<CouponDto>> GetCouponById(int id)
        {
            var coupon = (await _unitOfWork.Repository<Coupon>()
                .FindAsync(x => x.CouponId == id && !x.IsDeleted))
                .FirstOrDefault();

            if (coupon == null)
                throw new CustomException("Coupon not found.");

            return new ApiResponse<CouponDto>
            {
                Success = true,
                Message = "Success",
                Data = new CouponDto
                {
                    CouponId = coupon.CouponId,
                    CouponCode = coupon.CouponCode,
                    CouponName = coupon.CouponName,
                    Description = coupon.Description,
                    DiscountType = coupon.DiscountType,
                    DiscountValue = coupon.DiscountValue,
                    MinimumAmount = coupon.MinimumAmount,
                    MaximumDiscount = coupon.MaximumDiscount,
                    UsageLimit = coupon.UsageLimit,
                    UsedCount = coupon.UsedCount,
                    PlanId = coupon.PlanId,
                    StartDate = coupon.StartDate,
                    ExpiryDate = coupon.ExpiryDate,
                    Status = coupon.Status
                }
            };
        }

        #endregion
    }
}
