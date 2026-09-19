using System;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class PaymentTrackingDto
    {
        public int PaymentId { get; set; }

        public int OrganizationId { get; set; }

        public string? OrganizationName { get; set; }

        public int PlanId { get; set; }

        public string? PlanName { get; set; }

        public string? InvoiceNo { get; set; }

        public string? TransactionId { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string? Gateway { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime PaymentDate { get; set; }

        public DateTime? NextRenewal { get; set; }

        public decimal? RefundAmount { get; set; }

        public string? RefundReason { get; set; }
    }
}
