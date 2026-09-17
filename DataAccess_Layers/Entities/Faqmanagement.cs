using System;
using System.Collections.Generic;

namespace DataAccess_Layers.Entities;

public partial class Faqmanagement
{
    public int Faqid { get; set; }

    public string Faqtitle { get; set; } = null!;

    public string Faqcode { get; set; } = null!;

    public string? Category { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public string Visibility { get; set; } = null!;

    public int? DisplayOrder { get; set; }

    public int? CreatedBy { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool IsDeleted { get; set; }
}
