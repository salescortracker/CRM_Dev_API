namespace Business_Layer.DTOs.MasterDTO_s
{
    public class CompanyTypeDto
    {
        public int CompanyTypeId { get; set; }

        public int CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public int RegionId { get; set; }

        public string? RegionName { get; set; }

        public string CompanyTypeName { get; set; } = string.Empty;

        public string CompanyTypeCode { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}