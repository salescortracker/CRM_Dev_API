using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CommunicationEmailTemplate
{
    public int EmailTemplateId { get; set; }

    public string TemplateCode { get; set; } = null!;

    public string TemplateName { get; set; } = null!;

    public string? Category { get; set; }

    public string? Subject { get; set; }

    public string Body { get; set; } = null!;

    public string? LanguageCode { get; set; }

    public string? ProviderName { get; set; }

    public int Version { get; set; }

    public string Status { get; set; } = null!;

    public bool IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
