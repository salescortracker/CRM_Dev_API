using Business_Layer.Interfaces.AuditLog;
using DataAccess_Layers.Repositories;
using AuditLogEntity = DataAccess_Layers.Entities.AuditLog;
using System;

namespace Business_Layer.Services.AuditLog
{
    public class AuditService : IAuditService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LogAsync(
            string tableName,
            string actionType,
            int recordId,
            string oldValues,
            string newValues,
            int userId)
        {
            var audit = new AuditLogEntity();

            audit.TableName = tableName;
            audit.ActionType = actionType;
            audit.RecordId = recordId;
            audit.OldValues = oldValues;
            audit.NewValues = newValues;
            audit.UserId = userId;
            audit.CreatedDate = DateTime.Now;

            await _unitOfWork
                .Repository<AuditLogEntity>()
                .AddAsync(audit);

            await _unitOfWork
                .CompleteAsync();
        }
    }
}