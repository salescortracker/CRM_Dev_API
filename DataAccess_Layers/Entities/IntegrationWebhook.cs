using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class IntegrationWebhook
{
    public int IntegrationWebhookId { get; set; }

    public string WebhookName { get; set; } = null!;

    public string WebhookCode { get; set; } = null!;

    public string? Description { get; set; }

    public string EndpointUrl { get; set; } = null!;

    public string HttpMethod { get; set; } = null!;

    public string? AuthenticationType { get; set; }

    public string? AuthenticationToken { get; set; }

    public string? SecretKey { get; set; }

    public string? SubscribedEvents { get; set; }

    public bool IsActive { get; set; }

    public string? ConnectionStatus { get; set; }

    public DateTime? LastTestedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
