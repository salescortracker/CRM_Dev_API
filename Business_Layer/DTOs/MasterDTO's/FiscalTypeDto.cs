namespace Business_Layer.DTOs.MasterDTO_s
{
  public class FiscalTypeDto
  {
    public int FiscalTypeId { get; set; }

    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public int RegionId { get; set; }

    public string? RegionName { get; set; }

    public string FiscalTypeName { get; set; } = string.Empty;

    public string FiscalTypeCode { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
  }
}
