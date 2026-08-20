namespace Business_Layer.DTOs.MasterDTO_s
{
  public class IndustryDto
  {
    public int IndustryId { get; set; }

    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public int RegionId { get; set; }

    public string? RegionName { get; set; }

    public string IndustryName { get; set; } = string.Empty;

    public string IndustryCode { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
  }
}
