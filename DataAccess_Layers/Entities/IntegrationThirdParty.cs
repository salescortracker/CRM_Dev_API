using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class IntegrationThirdParty
{
    public int IntegrationThirdPartyId { get; set; }

    public string ConfigurationName { get; set; } = null!;

    public string IntegrationType { get; set; } = null!;

    public string ProviderName { get; set; } = null!;

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public string? ApiKey { get; set; }

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public string? AuthorizationUrl { get; set; }

    public string? TokenUrl { get; set; }

    public string? ApiBaseUrl { get; set; }

    public string? RedirectUrl { get; set; }

    public string? Scopes { get; set; }

    public string? AdditionalConfiguration { get; set; }

    public string Environment { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? ConnectionStatus { get; set; }

    public DateTime? LastTestedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
