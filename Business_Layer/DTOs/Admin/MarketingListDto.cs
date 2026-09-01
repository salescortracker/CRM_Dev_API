using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class MarketingListDto
    {

        public int MarketingListId { get; set; }

        public int? CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public int? RegionId { get; set; }

        public string? RegionName { get; set; }

        public string ListName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string ListType { get; set; } = string.Empty;

        public string? Source { get; set; }

        public int TotalContacts { get; set; }

        public int ActiveContacts { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
