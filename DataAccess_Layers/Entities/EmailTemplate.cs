using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class EmailTemplate
{
    public Guid EmailTemplateId { get; set; }

    public Guid? OrganizationId { get; set; }

    public string TemplateCode { get; set; } = null!;

    public string TemplateName { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string BodyHtml { get; set; } = null!;

    public bool IsSystemTemplate { get; set; }

    public byte Status { get; set; }

    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? RegionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
