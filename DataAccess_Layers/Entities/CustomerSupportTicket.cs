using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CustomerSupportTicket
{
    public int TicketId { get; set; }

    public string TicketNumber { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string? ContactPerson { get; set; }

    public string? Email { get; set; }

    public string? MobileNumber { get; set; }

    public string Subject { get; set; } = null!;

    public string? Category { get; set; }

    public string? Priority { get; set; }

    public string Status { get; set; } = null!;

    public string? AssignedTo { get; set; }

    public string? Source { get; set; }

    public string? RelatedModule { get; set; }

    public DateTime? DueDate { get; set; }

    public string? Description { get; set; }

    public string? ResolutionNotes { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }
}
