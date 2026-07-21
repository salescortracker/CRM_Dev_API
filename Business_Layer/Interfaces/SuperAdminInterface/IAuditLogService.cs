using Business_Layer.DTOs.SuperAdmin;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.SuperAdminInterface
{
    public interface IAuditLogService
    {
        Task<ApiResponse<List<AuditLogDto>>> GetAuditLogs();
    }
}
