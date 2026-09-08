using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class CrmmoduleConfigurationCustomField
{
    public int CustomFieldId { get; set; }

    public string FieldName { get; set; } = null!;

    public string DisplayLabel { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public string FieldType { get; set; } = null!;

    public string? DefaultValue { get; set; }

    public string? Placeholder { get; set; }

    public string Status { get; set; } = null!;

    public int FieldOrder { get; set; }

    public string? Description { get; set; }

    public bool RequiredField { get; set; }

    public bool UniqueField { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }
}
