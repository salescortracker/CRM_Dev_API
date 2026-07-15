using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class StateMaster
{
    public int StateId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public int CountryId { get; set; }

    public string StateName { get; set; } = null!;

    public string StateCode { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
