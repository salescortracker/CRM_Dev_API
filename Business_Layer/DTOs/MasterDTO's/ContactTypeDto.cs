namespace Business_Layer.DTOs.MasterDTO_s
{
    public class ContactTypeDto
    {
        public int ContactTypeId { get; set; }

        public int CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public int RegionId { get; set; }

        public string? RegionName { get; set; }

        public string ContactTypeName { get; set; } = string.Empty;

        public string ContactTypeCode { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}