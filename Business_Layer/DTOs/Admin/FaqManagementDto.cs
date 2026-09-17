using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class FaqManagementDto
    {

        public int Faqid { get; set; }

        public string Faqtitle { get; set; } = string.Empty;

        public string Faqcode { get; set; } = string.Empty;

        public string? Category { get; set; }

        public string Question { get; set; } = string.Empty;

        public string Answer { get; set; } = string.Empty;

        public string Visibility { get; set; } = string.Empty;

        public int? DisplayOrder { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
