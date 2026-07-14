

namespace DataAccess_Layers.Entities;

public partial class Region
{
    public int RegionId { get; set; }

    public int CompanyId { get; set; }

    public string RegionName { get; set; } = null!;

    public string? Country { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? UserId { get; set; }

    public bool? IsActive { get; set; }

    public string? RegionCode { get; set; }

    public string? ContactPerson { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public virtual Company Company { get; set; } = null!;
}
