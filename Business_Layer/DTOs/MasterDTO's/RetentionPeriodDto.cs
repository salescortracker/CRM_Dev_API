namespace Business_Layer.DTOs.MasterDTO_s
{
  public class RetentionPeriodDto
  {
    public int RetentionPeriodId { get; set; }

    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public int RegionId { get; set; }

    public string? RegionName { get; set; }

    public string RetentionPeriodName { get; set; } = string.Empty;

    public string RetentionPeriodCode { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
  }
}
