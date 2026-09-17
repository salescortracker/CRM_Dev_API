using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class TicketCategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string CategoryCode { get; set; } = string.Empty;

        public string? ParentCategory { get; set; }

        public string? CategoryType { get; set; }

        public string? Priority { get; set; }

        public decimal? SlaHours { get; set; }

        public string? AssignedTeam { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
