using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class TwilioConfiguration
{
    public Guid TwilioConfigurationId { get; set; }

    public Guid OrganizationId { get; set; }

    public Guid? CustomerTenantId { get; set; }

    public string AccountSid { get; set; } = null!;

    public string AuthTokenEncrypted { get; set; } = null!;

    public string? ApiKeySid { get; set; }

    public string? ApiSecretEncrypted { get; set; }

    public string? DefaultFromNumber { get; set; }

    public string? VoiceWebhookUrl { get; set; }

    public string? SmsWebhookUrl { get; set; }

    public string? StatusCallbackUrl { get; set; }

    public bool IsRecordingEnabled { get; set; }

    public bool IsActive { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual CustomerTenant? CustomerTenant { get; set; }

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<TwilioCallLog> TwilioCallLogs { get; set; } = new List<TwilioCallLog>();

    public virtual ICollection<TwilioPhoneNumber> TwilioPhoneNumbers { get; set; } = new List<TwilioPhoneNumber>();

    public virtual ICollection<TwilioSmslog> TwilioSmslogs { get; set; } = new List<TwilioSmslog>();

    public virtual ICollection<TwilioWebhookLog> TwilioWebhookLogs { get; set; } = new List<TwilioWebhookLog>();

    public virtual User User { get; set; } = null!;
}
