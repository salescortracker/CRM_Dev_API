using System;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class InvoiceMasterDto
    {
        public int InvoiceId { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;

        public int OrganizationId { get; set; }

        public string? OrganizationName { get; set; }

        public string? CompanyEmail { get; set; }

        public string? CompanyPhone { get; set; }

        public int PlanId { get; set; }

        public string? PlanName { get; set; }

        public string BillingCycle { get; set; } = "Monthly";

        public DateTime InvoiceDate { get; set; }

        public DateTime? DueDate { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; } = 1;

        public decimal UnitPrice { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GstPercentage { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal BalanceAmount { get; set; }

        public string PaymentStatus { get; set; } = "Pending";

        public string? PaymentMethod { get; set; }

        public string? TransactionId { get; set; }

        public string Currency { get; set; } = "INR";

        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
