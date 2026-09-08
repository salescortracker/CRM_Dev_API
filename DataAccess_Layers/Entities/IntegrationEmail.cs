using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class IntegrationEmail
{
    public int IntegrationEmailId { get; set; }

    public string ConfigurationName { get; set; } = null!;

    public string ProviderName { get; set; } = null!;

    public string SmtpHost { get; set; } = null!;

    public int SmtpPort { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string FromEmail { get; set; } = null!;

    public string? FromName { get; set; }

    public string? EncryptionType { get; set; }

    public bool EnableAuthentication { get; set; }

    public bool IsActive { get; set; }

    public string? ConnectionStatus { get; set; }

    public DateTime? LastTestedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
