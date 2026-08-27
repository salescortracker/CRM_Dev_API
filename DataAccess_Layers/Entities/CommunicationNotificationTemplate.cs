using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CommunicationNotificationTemplate
{
    public int NotificationTemplateId { get; set; }

    public string TemplateCode { get; set; } = null!;

    public string TemplateName { get; set; } = null!;

    public string? NotificationType { get; set; }

    public string? Category { get; set; }

    public string? Title { get; set; }

    public string MessageBody { get; set; } = null!;

    public string? Channel { get; set; }

    public string? LanguageCode { get; set; }

    public int Version { get; set; }

    public string Status { get; set; } = null!;

    public bool IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
