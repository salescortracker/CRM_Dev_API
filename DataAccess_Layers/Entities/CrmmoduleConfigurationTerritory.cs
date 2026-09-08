using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CrmmoduleConfigurationTerritory
{
    public int TerritoryId { get; set; }

    public string TerritoryName { get; set; } = null!;

    public string TerritoryCode { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string? TerritoryManager { get; set; }

    public int CustomerCount { get; set; }

    public string Priority { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }
}
