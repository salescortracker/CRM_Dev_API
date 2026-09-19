using System;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CouponDto
    {
        public int CouponId { get; set; }

        public string CouponCode { get; set; } = string.Empty;

        public string CouponName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string DiscountType { get; set; } = "Percentage";

        public decimal DiscountValue { get; set; }

        public decimal MinimumAmount { get; set; }

        public decimal MaximumDiscount { get; set; }

        public int UsageLimit { get; set; }

        public int UsedCount { get; set; }

        public int? PlanId { get; set; }

        public string? PlanName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string Status { get; set; } = "Active";
    }
}
