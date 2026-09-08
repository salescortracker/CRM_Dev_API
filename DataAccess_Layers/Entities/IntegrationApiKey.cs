using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class IntegrationApiKey
{
    public int IntegrationApiKeyId { get; set; }

    public string KeyName { get; set; } = null!;

    public string KeyCode { get; set; } = null!;

    public string ApiKey { get; set; } = null!;

    public string? Description { get; set; }

    public string Environment { get; set; } = null!;

    public DateTime? ExpirationDate { get; set; }

    public string? AllowedIpAddresses { get; set; }

    public string? Permissions { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastUsedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
