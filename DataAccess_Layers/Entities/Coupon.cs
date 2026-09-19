using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Coupon
{
    public int CouponId { get; set; }

    public string CouponCode { get; set; } = null!;

    public string CouponName { get; set; } = null!;

    public string? Description { get; set; }

    public string DiscountType { get; set; } = null!;

    public decimal DiscountValue { get; set; }

    public decimal MinimumAmount { get; set; }

    public decimal MaximumDiscount { get; set; }

    public int UsageLimit { get; set; }

    public int UsedCount { get; set; }

    public int? PlanId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string Status { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual SubscriptionPlanMaster? Plan { get; set; }
}
