using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class TeamMember
{
    public int TeamMemberId { get; set; }

    public int TeamId { get; set; }

    public int AdministratorId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual CompanyAdministrator Administrator { get; set; } = null!;

    public virtual WorkTeam Team { get; set; } = null!;
}
