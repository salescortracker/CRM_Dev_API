namespace Business_Layer.DTOs.SuperAdmin
{
    public class RefundRequestDto
    {
        public decimal RefundAmount { get; set; }

        public string? RefundReason { get; set; }
    }
}
