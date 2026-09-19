using Business_Layer.DTOs.SuperAdmin;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.SuperAdminInterface;
using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Newtonsoft.Json;
using Serilog;
using Shared.CommonModels;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business_Layer.Services.SuperAdminServices
{
    public class PaymentTrackingService : IPaymentTrackingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public PaymentTrackingService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        #region CREATE

        public async Task<ApiResponse<string>> CreatePaymentTracking(PaymentTrackingDto dto)
        {
            try
            {
                if (dto.OrganizationId <= 0)
                    throw new CustomException("Company is required.");

                if (dto.PlanId <= 0)
                    throw new CustomException("Plan is required.");

                if (string.IsNullOrWhiteSpace(dto.PaymentMethod))
                    throw new CustomException("Payment Method is required.");

                PaymentTracking payment = new PaymentTracking
                {
                    OrganizationId = dto.OrganizationId,
                    PlanId = dto.PlanId,
                    InvoiceNo = dto.InvoiceNo,
                    TransactionId = dto.TransactionId,
                    Amount = dto.Amount,
                    PaymentMethod = dto.PaymentMethod,
                    Gateway = dto.Gateway,
                    Status = dto.Status,
                    PaymentDate = dto.PaymentDate == default ? DateTime.Now : dto.PaymentDate,
                    NextRenewal = dto.NextRenewal,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<PaymentTracking>().AddAsync(payment);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Payment Tracking",
                    "INSERT",
                    payment.PaymentId,
                    "",
                    JsonConvert.SerializeObject(payment),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Payment Created Successfully.",
                    Data = payment.PaymentId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating Payment Tracking record");
                throw;
            }
        }

        #endregion

        #region UPDATE

        public async Task<ApiResponse<string>> UpdatePaymentTracking(PaymentTrackingDto dto)
        {
            try
            {
                var payment = (await _unitOfWork.Repository<PaymentTracking>()
                    .FindAsync(x => x.PaymentId == dto.PaymentId))
                    .FirstOrDefault();

                if (payment == null)
                    throw new CustomException("Payment record not found.");

                string oldValues = JsonConvert.SerializeObject(payment);

                payment.OrganizationId = dto.OrganizationId;
                payment.PlanId = dto.PlanId;
                payment.InvoiceNo = dto.InvoiceNo;
                payment.TransactionId = dto.TransactionId;
                payment.Amount = dto.Amount;
                payment.PaymentMethod = dto.PaymentMethod;
                payment.Gateway = dto.Gateway;
                payment.Status = dto.Status;
                payment.PaymentDate = dto.PaymentDate;
                payment.NextRenewal = dto.NextRenewal;

                payment.ModifiedBy = _currentUserService.UserId;
                payment.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<PaymentTracking>().Update(payment);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Payment Tracking",
                    "UPDATE",
                    payment.PaymentId,
                    oldValues,
                    JsonConvert.SerializeObject(payment),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Payment Updated Successfully.",
                    Data = payment.PaymentId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating Payment Tracking record");
                throw;
            }
        }

        #endregion

        #region DELETE

        public async Task<ApiResponse<string>> DeletePaymentTracking(int id)
        {
            try
            {
                var payment = (await _unitOfWork.Repository<PaymentTracking>()
                    .FindAsync(x => x.PaymentId == id))
                    .FirstOrDefault();

                if (payment == null)
                    throw new CustomException("Payment record not found.");

                string oldValues = JsonConvert.SerializeObject(payment);

                payment.IsDeleted = true;
                payment.ModifiedBy = _currentUserService.UserId;
                payment.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<PaymentTracking>().Update(payment);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Payment Tracking",
                    "DELETE",
                    payment.PaymentId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Payment Deleted Successfully.",
                    Data = payment.PaymentId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting Payment Tracking record");
                throw;
            }
        }

        #endregion

        #region REFUND

        public async Task<ApiResponse<string>> RefundPaymentTracking(int id, decimal refundAmount, string? refundReason)
        {
            try
            {
                var payment = (await _unitOfWork.Repository<PaymentTracking>()
                    .FindAsync(x => x.PaymentId == id))
                    .FirstOrDefault();

                if (payment == null)
                    throw new CustomException("Payment record not found.");

                string oldValues = JsonConvert.SerializeObject(payment);

                payment.Status = "Refunded";
                payment.RefundAmount = refundAmount;
                payment.RefundReason = refundReason;
                payment.ModifiedBy = _currentUserService.UserId;
                payment.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<PaymentTracking>().Update(payment);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Payment Tracking",
                    "REFUND",
                    payment.PaymentId,
                    oldValues,
                    JsonConvert.SerializeObject(payment),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Payment Refunded Successfully.",
                    Data = payment.PaymentId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while refunding Payment Tracking record");
                throw;
            }
        }

        #endregion

        #region GET ALL

        public async Task<ApiResponse<List<PaymentTrackingDto>>> GetPaymentTrackings()
        {
            var organizations = await _unitOfWork.Repository<OrganizationDatum>().GetAllAsync();

            var plans = await _unitOfWork.Repository<SubscriptionPlanMaster>().GetAllAsync();

            var payments = await _unitOfWork.Repository<PaymentTracking>().GetAllAsync();

            var result = (
                from x in payments

                join o in organizations
                    on x.OrganizationId equals o.OrganizationId

                join p in plans
                    on x.PlanId equals p.PlanId

                where !x.IsDeleted && x.CreatedBy == _currentUserService.UserId

                select new PaymentTrackingDto
                {
                    PaymentId = x.PaymentId,

                    OrganizationId = x.OrganizationId,
                    OrganizationName = o.OrganizationName,

                    PlanId = x.PlanId,
                    PlanName = p.PlanName,

                    InvoiceNo = x.InvoiceNo,
                    TransactionId = x.TransactionId,
                    Amount = x.Amount,
                    PaymentMethod = x.PaymentMethod,
                    Gateway = x.Gateway,
                    Status = x.Status,
                    PaymentDate = x.PaymentDate,
                    NextRenewal = x.NextRenewal,
                    RefundAmount = x.RefundAmount,
                    RefundReason = x.RefundReason
                }
            )
            .OrderByDescending(x => x.PaymentId)
            .ToList();

            return new ApiResponse<List<PaymentTrackingDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        #endregion

        #region GET BY ID

        public async Task<ApiResponse<PaymentTrackingDto>> GetPaymentTrackingById(int id)
        {
            var payment = (await _unitOfWork.Repository<PaymentTracking>()
                .FindAsync(x => x.PaymentId == id && !x.IsDeleted))
                .FirstOrDefault();

            if (payment == null)
                throw new CustomException("Payment record not found.");

            return new ApiResponse<PaymentTrackingDto>
            {
                Success = true,
                Message = "Success",
                Data = new PaymentTrackingDto
                {
                    PaymentId = payment.PaymentId,
                    OrganizationId = payment.OrganizationId,
                    PlanId = payment.PlanId,
                    InvoiceNo = payment.InvoiceNo,
                    TransactionId = payment.TransactionId,
                    Amount = payment.Amount,
                    PaymentMethod = payment.PaymentMethod,
                    Gateway = payment.Gateway,
                    Status = payment.Status,
                    PaymentDate = payment.PaymentDate,
                    NextRenewal = payment.NextRenewal,
                    RefundAmount = payment.RefundAmount,
                    RefundReason = payment.RefundReason
                }
            };
        }

        #endregion
    }
}
