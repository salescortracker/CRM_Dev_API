using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class MarketingList
{
    public int MarketingListId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public string ListName { get; set; } = null!;

    public string? Description { get; set; }

    public string ListType { get; set; } = null!;

    public string? Source { get; set; }

    public int TotalContacts { get; set; }

    public int ActiveContacts { get; set; }

    public string Status { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
