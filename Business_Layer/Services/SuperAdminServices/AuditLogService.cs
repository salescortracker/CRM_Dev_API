using Business_Layer.DTOs.SuperAdmin;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.SuperAdminInterface;
using DataAccess_Layers.Repositories;
using Serilog;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.SuperAdminServices
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public AuditLogService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        #region AUDIT LOGS

        public async Task<ApiResponse<List<AuditLogDto>>> GetAuditLogs()
        {
            try
            {
                var logs = (await _unitOfWork.Repository<DataAccess_Layers.Entities.AuditLog>()
                    .GetAllAsync())
                    .OrderByDescending(x => x.AuditId)
                    .ToList();

                var result = logs.Select(x => new AuditLogDto
                {
                    AuditId = x.AuditId,
                    TableName = x.TableName,
                    ActionType = x.ActionType,
                    RecordId = x.RecordId,
                    OldValues = x.OldValues,
                    NewValues = x.NewValues,
                    UserId = x.UserId,
                    CreatedDate = x.CreatedDate,
                    CompanyId = x.CompanyId,
                    RegionId = x.RegionId,
                    CreatedBy = x.CreatedBy,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedAt = x.ModifiedAt
                }).ToList();

                return new ApiResponse<List<AuditLogDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while loading audit logs");
                throw;
            }
        }

        #endregion
    }
}
