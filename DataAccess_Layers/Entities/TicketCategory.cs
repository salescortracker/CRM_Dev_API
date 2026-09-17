using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class TicketCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CategoryCode { get; set; } = null!;

    public string? ParentCategory { get; set; }

    public string? CategoryType { get; set; }

    public string? Priority { get; set; }

    public decimal? SlaHours { get; set; }

    public string? AssignedTeam { get; set; }

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
