using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class CustomerSupportTicketDto
    {
        public int TicketId { get; set; }

        public string TicketNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string? ContactPerson { get; set; }

        public string? Email { get; set; }

        public string? MobileNumber { get; set; }

        public string Subject { get; set; } = string.Empty;

        public string? Category { get; set; }

        public string? Priority { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? AssignedTo { get; set; }

        public string? Source { get; set; }

        public string? RelatedModule { get; set; }

        public DateTime? DueDate { get; set; }

        public string? Description { get; set; }

        public string? ResolutionNotes { get; set; }
    }
}
