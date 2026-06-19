using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.AuditLog
{
    public interface IAuditService
    {
        Task LogAsync(
            string tableName,
            string actionType,
            int recordId,
            string oldValues,
            string newValues,
            int userId);
    }
}
