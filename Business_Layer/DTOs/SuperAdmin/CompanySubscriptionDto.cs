using System;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CompanySubscriptionDto
    {
        public int SubscriptionId { get; set; }

        public int OrganizationId { get; set; }

        public string? OrganizationName { get; set; }

        public int PlanId { get; set; }

        public string? PlanName { get; set; }

        public string? PlanAccent { get; set; }

        public string Status { get; set; } = "Trial";

        public string PaymentStatus { get; set; } = "Pending";

        public DateTime StartedDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public decimal Amount { get; set; }

        public int Seats { get; set; }

        public bool AutoRenew { get; set; }

        public string BillingCycle { get; set; } = "Monthly";
    }
}
