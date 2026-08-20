namespace Business_Layer.DTOs.MasterDTO_s
{
  public class LeadTypeDto
  {
    public int LeadTypeId { get; set; }

    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public int RegionId { get; set; }

    public string? RegionName { get; set; }

    public string LeadTypeName { get; set; } = string.Empty;

    public string LeadTypeCode { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
  }
}
