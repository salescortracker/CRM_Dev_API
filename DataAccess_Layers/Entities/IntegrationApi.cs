using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class IntegrationApi
{
    public int IntegrationApiId { get; set; }

    public string ApiName { get; set; } = null!;

    public string ApiCode { get; set; } = null!;

    public string? Description { get; set; }

    public string BaseUrl { get; set; } = null!;

    public string? ApiVersion { get; set; }

    public string? AuthenticationType { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? ApiKey { get; set; }

    public string? BearerToken { get; set; }

    public string Environment { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? ConnectionStatus { get; set; }

    public DateTime? LastTestedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
