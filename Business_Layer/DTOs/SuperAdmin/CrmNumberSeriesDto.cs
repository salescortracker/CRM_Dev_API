using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CrmNumberSeriesDto
    {
        public int NumberSeriesId { get; set; }

        public string SeriesName { get; set; } = string.Empty;

        public string ModuleName { get; set; } = string.Empty;

        public string? Prefix { get; set; }

        public long StartingNumber { get; set; }

        public long CurrentNumber { get; set; }

        public string? NumberFormat { get; set; }

        public string Status { get; set; } = "Active";

        public string? Description { get; set; }
    }
}
