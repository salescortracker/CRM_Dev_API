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
    public class BillingService : IBillingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public BillingService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        #region CREATE

        public async Task<ApiResponse<string>> CreateBilling(BillingDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.BillNumber))
                    throw new CustomException("Bill Number is required.");

                if (dto.OrganizationId <= 0)
                    throw new CustomException("Company is required.");

                if (dto.PlanId <= 0)
                    throw new CustomException("Plan is required.");

                var duplicate = await _unitOfWork.Repository<Billing>()
                    .FindAsync(x =>
                        !x.IsDeleted &&
                        x.BillNumber.ToLower() == dto.BillNumber.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Bill Number already exists.");

                Billing billing = new Billing
                {
                    BillNumber = dto.BillNumber,
                    OrganizationId = dto.OrganizationId,
                    PlanId = dto.PlanId,
                    BillingDate = dto.BillingDate == default ? DateTime.Now : dto.BillingDate,
                    DueDate = dto.DueDate,
                    Amount = dto.Amount,
                    Tax = dto.Tax,
                    Discount = dto.Discount,
                    TotalAmount = dto.TotalAmount,
                    PaymentStatus = dto.PaymentStatus,
                    PaymentMethod = dto.PaymentMethod,
                    BillingAddress = dto.BillingAddress,
                    Notes = dto.Notes,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<Billing>().AddAsync(billing);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Billing",
                    "INSERT",
                    billing.BillingId,
                    "",
                    JsonConvert.SerializeObject(billing),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Billing Created Successfully.",
                    Data = billing.BillNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating Billing");
                throw;
            }
        }

        #endregion

        #region UPDATE

        public async Task<ApiResponse<string>> UpdateBilling(BillingDto dto)
        {
            try
            {
                var billing = (await _unitOfWork.Repository<Billing>()
                    .FindAsync(x => x.BillingId == dto.BillingId))
                    .FirstOrDefault();

                if (billing == null)
                    throw new CustomException("Billing record not found.");

                var duplicate = await _unitOfWork.Repository<Billing>()
                    .FindAsync(x =>
                        x.BillingId != dto.BillingId &&
                        !x.IsDeleted &&
                        x.BillNumber.ToLower() == dto.BillNumber.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Bill Number already exists.");

                string oldValues = JsonConvert.SerializeObject(billing);

                billing.BillNumber = dto.BillNumber;
                billing.OrganizationId = dto.OrganizationId;
                billing.PlanId = dto.PlanId;
                billing.BillingDate = dto.BillingDate;
                billing.DueDate = dto.DueDate;
                billing.Amount = dto.Amount;
                billing.Tax = dto.Tax;
                billing.Discount = dto.Discount;
                billing.TotalAmount = dto.TotalAmount;
                billing.PaymentStatus = dto.PaymentStatus;
                billing.PaymentMethod = dto.PaymentMethod;
                billing.BillingAddress = dto.BillingAddress;
                billing.Notes = dto.Notes;

                billing.ModifiedBy = _currentUserService.UserId;
                billing.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<Billing>().Update(billing);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Billing",
                    "UPDATE",
                    billing.BillingId,
                    oldValues,
                    JsonConvert.SerializeObject(billing),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Billing Updated Successfully.",
                    Data = billing.BillNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating Billing");
                throw;
            }
        }

        #endregion

        #region DELETE

        public async Task<ApiResponse<string>> DeleteBilling(int id)
        {
            try
            {
                var billing = (await _unitOfWork.Repository<Billing>()
                    .FindAsync(x => x.BillingId == id))
                    .FirstOrDefault();

                if (billing == null)
                    throw new CustomException("Billing record not found.");

                string oldValues = JsonConvert.SerializeObject(billing);

                billing.IsDeleted = true;
                billing.ModifiedBy = _currentUserService.UserId;
                billing.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<Billing>().Update(billing);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Billing",
                    "DELETE",
                    billing.BillingId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Billing Deleted Successfully.",
                    Data = billing.BillNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting Billing");
                throw;
            }
        }

        #endregion

        #region GET ALL

        public async Task<ApiResponse<List<BillingDto>>> GetBillings()
        {
            var organizations = await _unitOfWork.Repository<OrganizationDatum>().GetAllAsync();

            var plans = await _unitOfWork.Repository<SubscriptionPlanMaster>().GetAllAsync();

            var billings = await _unitOfWork.Repository<Billing>().GetAllAsync();

            var result = (
                from x in billings

                join o in organizations
                    on x.OrganizationId equals o.OrganizationId

                join p in plans
                    on x.PlanId equals p.PlanId

                where !x.IsDeleted && x.CreatedBy == _currentUserService.UserId

                select new BillingDto
                {
                    BillingId = x.BillingId,
                    BillNumber = x.BillNumber,

                    OrganizationId = x.OrganizationId,
                    OrganizationName = o.OrganizationName,

                    PlanId = x.PlanId,
                    PlanName = p.PlanName,

                    BillingDate = x.BillingDate,
                    DueDate = x.DueDate,
                    Amount = x.Amount,
                    Tax = x.Tax,
                    Discount = x.Discount,
                    TotalAmount = x.TotalAmount,
                    PaymentStatus = x.PaymentStatus,
                    PaymentMethod = x.PaymentMethod,
                    BillingAddress = x.BillingAddress,
                    Notes = x.Notes
                }
            )
            .OrderByDescending(x => x.BillingId)
            .ToList();

            return new ApiResponse<List<BillingDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        #endregion

        #region GET BY ID

        public async Task<ApiResponse<BillingDto>> GetBillingById(int id)
        {
            var billing = (await _unitOfWork.Repository<Billing>()
                .FindAsync(x => x.BillingId == id && !x.IsDeleted))
                .FirstOrDefault();

            if (billing == null)
                throw new CustomException("Billing record not found.");

            return new ApiResponse<BillingDto>
            {
                Success = true,
                Message = "Success",
                Data = new BillingDto
                {
                    BillingId = billing.BillingId,
                    BillNumber = billing.BillNumber,
                    OrganizationId = billing.OrganizationId,
                    PlanId = billing.PlanId,
                    BillingDate = billing.BillingDate,
                    DueDate = billing.DueDate,
                    Amount = billing.Amount,
                    Tax = billing.Tax,
                    Discount = billing.Discount,
                    TotalAmount = billing.TotalAmount,
                    PaymentStatus = billing.PaymentStatus,
                    PaymentMethod = billing.PaymentMethod,
                    BillingAddress = billing.BillingAddress,
                    Notes = billing.Notes
                }
            };
        }

        #endregion
    }
}
