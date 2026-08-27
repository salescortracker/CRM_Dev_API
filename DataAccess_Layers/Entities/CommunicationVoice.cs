using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CommunicationVoice
{
    public int CommunicationVoiceId { get; set; }

    public string ConfigurationName { get; set; } = null!;

    public string ProviderName { get; set; } = null!;

    public string AccountSid { get; set; } = null!;

    public string? AuthToken { get; set; }

    public string FromNumber { get; set; } = null!;

    public string? VoiceApplicationSid { get; set; }

    public string? TwiMlappSid { get; set; }

    public string? TwiMlurl { get; set; }

    public string? WebhookUrl { get; set; }

    public bool IsActive { get; set; }

    public string? ConnectionStatus { get; set; }

    public DateTime? LastTestedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
