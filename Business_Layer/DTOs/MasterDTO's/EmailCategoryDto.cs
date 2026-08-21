namespace Business_Layer.DTOs.MasterDTO_s
{
    public class EmailCategoryDto
    {
        public int EmailCategoryId { get; set; }

        public int CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public int RegionId { get; set; }

        public string? RegionName { get; set; }

        public string EmailCategoryName { get; set; } = string.Empty;

        public string EmailCategoryCode { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}