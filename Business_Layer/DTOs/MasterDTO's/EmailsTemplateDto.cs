namespace Business_Layer.DTOs.MasterDTO_s
{
  public class EmailsTemplateDto
  {
    public int EmailsTemplatesId { get; set; }

    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public int RegionId { get; set; }

    public string? RegionName { get; set; }

    public string EmailsTemplatesName { get; set; } = string.Empty;

    public string EmailsTemplatesCode { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
  }
}
