using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class CrmCustomFieldDto
    {
        public int CustomFieldId { get; set; }

        public string FieldName { get; set; } = string.Empty;

        public string DisplayLabel { get; set; } = string.Empty;

        public string ModuleName { get; set; } = string.Empty;

        public string FieldType { get; set; } = string.Empty;

        public string? DefaultValue { get; set; }

        public string? Placeholder { get; set; }

        public string Status { get; set; } = "Active";

        public int FieldOrder { get; set; }

        public string? Description { get; set; }

        public bool RequiredField { get; set; }

        public bool UniqueField { get; set; }
    }
}
