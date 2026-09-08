using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CrmmoduleConfigurationNumberSeries
{
    public int NumberSeriesId { get; set; }

    public string SeriesName { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public string? Prefix { get; set; }

    public long StartingNumber { get; set; }

    public long CurrentNumber { get; set; }

    public string? NumberFormat { get; set; }

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }
}
