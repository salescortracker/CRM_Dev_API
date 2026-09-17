using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class KnowledgeBaseDto
    {
        public int ArticleId { get; set; }

        public string ArticleTitle { get; set; } = string.Empty;

        public string? Category { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? Keywords { get; set; }

        public string Visibility { get; set; } = string.Empty;

        public string? Author { get; set; }

        public string Version { get; set; } = string.Empty;

        public DateTime? LastUpdated { get; set; }

        public string? Attachment { get; set; }

        public string? UploadType { get; set; }

        public string? Summary { get; set; }

        public string? ArticleContent { get; set; }

        public bool IsActive { get; set; }
    }
}
