namespace Business_Layer.DTOs.MasterDTO_s
{
    public class ActivityTypeDto
    {
        public int ActivityTypeId { get; set; }

        public int CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public int RegionId { get; set; }

        public string? RegionName { get; set; }

        public string ActivityTypeName { get; set; } = string.Empty;

        public string ActivityTypeCode { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}