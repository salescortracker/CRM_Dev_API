using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class IntegrationTwilio
{
    public int IntegrationTwilioId { get; set; }

    public string ConfigurationName { get; set; } = null!;

    public string AccountSid { get; set; } = null!;

    public string? AuthToken { get; set; }

    public string? SmsFromNumber { get; set; }

    public string? WhatsAppFromNumber { get; set; }

    public string? VoiceFromNumber { get; set; }

    public string? MessagingServiceSid { get; set; }

    public string? VoiceApplicationSid { get; set; }

    public string? TwimlAppSid { get; set; }

    public string? WebhookUrl { get; set; }

    public bool IsSmsEnabled { get; set; }

    public bool IsWhatsAppEnabled { get; set; }

    public bool IsVoiceEnabled { get; set; }

    public bool IsActive { get; set; }

    public string? ConnectionStatus { get; set; }

    public DateTime? LastTestedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
