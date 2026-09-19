using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class LoginHistory
{
    public int LoginHistoryId { get; set; }

    public int? UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public string LoginType { get; set; } = null!;

    public string? Device { get; set; }

    public string? IpAddress { get; set; }

    public string? Location { get; set; }

    public string Status { get; set; } = null!;

    public DateTime LoginTime { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual UserLogin? User { get; set; }
}
