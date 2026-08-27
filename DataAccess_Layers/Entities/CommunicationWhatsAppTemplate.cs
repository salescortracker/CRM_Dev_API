using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CommunicationWhatsAppTemplate
{
    public int WhatsAppTemplateId { get; set; }

    public string TemplateCode { get; set; } = null!;

    public string TemplateName { get; set; } = null!;

    public string? Category { get; set; }

    public string? LanguageCode { get; set; }

    public string ProviderName { get; set; } = null!;

    public string? TemplateSid { get; set; }

    public string? HeaderText { get; set; }

    public string BodyText { get; set; } = null!;

    public string? FooterText { get; set; }

    public string ApprovalStatus { get; set; } = null!;

    public string? RejectionReason { get; set; }

    public int Version { get; set; }

    public string Status { get; set; } = null!;

    public bool IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
