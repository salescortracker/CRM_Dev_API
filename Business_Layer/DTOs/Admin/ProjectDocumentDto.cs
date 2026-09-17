using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class ProjectDocumentDto
    {
        public int DocumentId { get; set; }

        public string DocumentName { get; set; } = string.Empty;

        public int ProjectId { get; set; }

        public string? Category { get; set; }

        public string? Version { get; set; }

        public int? UploadedBy { get; set; }

        public DateTime UploadDate { get; set; }

        public string? UploadFile { get; set; }

        public decimal? FileSizeKb { get; set; }

        public string? FileType { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
