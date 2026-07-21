using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class AuditLogDto
    {
        public int AuditId { get; set; }

        public string? TableName { get; set; }

        public string? ActionType { get; set; }

        public int? RecordId { get; set; }

        public string? OldValues { get; set; }

        public string? NewValues { get; set; }

        public int? UserId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CompanyId { get; set; }

        public int? RegionId { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }
        public string UserName { get; set; }
    }
}
