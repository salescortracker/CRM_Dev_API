using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class PlanDto
    {
        public int PlanId { get; set; }

        public string PlanName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int UserLimit { get; set; }

        public int StorageLimit { get; set; }

        public int ApiLimit { get; set; }

        public string Accent { get; set; } = string.Empty;

        public string? Features { get; set; }

        public bool Status { get; set; } = true;
    }
}
