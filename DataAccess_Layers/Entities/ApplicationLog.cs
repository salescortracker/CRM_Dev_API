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
}
