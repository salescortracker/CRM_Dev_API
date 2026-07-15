using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Smstemplate
{
    public Guid SmstemplateId { get; set; }

    public Guid? OrganizationId { get; set; }

    public string TemplateCode { get; set; } = null!;

    public string TemplateName { get; set; } = null!;

    public string MessageText { get; set; } = null!;

    public string? DlttemplateId { get; set; }

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
