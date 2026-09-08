using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class IntegrationPaymentGateway
{
    public int IntegrationPaymentGatewayId { get; set; }

    public string ConfigurationName { get; set; } = null!;

    public string ProviderName { get; set; } = null!;

    public string? MerchantId { get; set; }

    public string? ApiKey { get; set; }

    public string? ApiSecret { get; set; }

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public string? WebhookUrl { get; set; }

    public string Environment { get; set; } = null!;

    public string? CurrencyCode { get; set; }

    public bool IsActive { get; set; }

    public string? ConnectionStatus { get; set; }

    public DateTime? LastTestedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
