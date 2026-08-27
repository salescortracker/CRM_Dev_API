using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CommunicationEmail
{
    public int CommunicationEmailId { get; set; }

    public string ConfigurationName { get; set; } = null!;

    public string ProviderName { get; set; } = null!;

    public string Smtphost { get; set; } = null!;

    public int Smtpport { get; set; }

    public string? Smtpusername { get; set; }

    public string? Smtppassword { get; set; }

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
