using System;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class BillingDto
    {
        public int BillingId { get; set; }

        public string BillNumber { get; set; } = string.Empty;

        public int OrganizationId { get; set; }

        public string? OrganizationName { get; set; }

        public int PlanId { get; set; }

        public string? PlanName { get; set; }

        public DateTime BillingDate { get; set; }

        public DateTime? DueDate { get; set; }

        public decimal Amount { get; set; }

        public decimal Tax { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentStatus { get; set; } = "Pending";

        public string? PaymentMethod { get; set; }

        public string? BillingAddress { get; set; }

        public string? Notes { get; set; }
    }
}
