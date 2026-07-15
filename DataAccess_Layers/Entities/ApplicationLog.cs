using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class ApplicationLog
{
    public long LogId { get; set; }

    public string? LogLevel { get; set; }

    public string? Message { get; set; }

    public string? Exception { get; set; }

    public string? Source { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? RegionId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
