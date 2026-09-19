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
    public class CompanySubscriptionService : ICompanySubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public CompanySubscriptionService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        #region CREATE

        public async Task<ApiResponse<string>> CreateCompanySubscription(CompanySubscriptionDto dto)
        {
            try
            {
                if (dto.OrganizationId <= 0)
                    throw new CustomException("Company is required.");

                if (dto.PlanId <= 0)
                    throw new CustomException("Plan is required.");

                CompanySubscription subscription = new CompanySubscription
                {
                    OrganizationId = dto.OrganizationId,
                    PlanId = dto.PlanId,
                    Status = dto.Status,
                    PaymentStatus = dto.PaymentStatus,
                    StartedDate = dto.StartedDate == default ? DateTime.Now : dto.StartedDate,
                    ExpiryDate = dto.ExpiryDate,
                    Amount = dto.Amount,
                    Seats = dto.Seats,
                    AutoRenew = dto.AutoRenew,
                    BillingCycle = dto.BillingCycle,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<CompanySubscription>().AddAsync(subscription);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Company Subscription",
                    "INSERT",
                    subscription.SubscriptionId,
                    "",
                    JsonConvert.SerializeObject(subscription),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Company Subscription Created Successfully.",
                    Data = subscription.SubscriptionId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating Company Subscription");
                throw;
            }
        }

        #endregion

        #region UPDATE

        public async Task<ApiResponse<string>> UpdateCompanySubscription(CompanySubscriptionDto dto)
        {
            try
            {
                var subscription = (await _unitOfWork.Repository<CompanySubscription>()
                    .FindAsync(x => x.SubscriptionId == dto.SubscriptionId))
                    .FirstOrDefault();

                if (subscription == null)
                    throw new CustomException("Company Subscription not found.");

                string oldValues = JsonConvert.SerializeObject(subscription);

                subscription.OrganizationId = dto.OrganizationId;
                subscription.PlanId = dto.PlanId;
                subscription.Status = dto.Status;
                subscription.PaymentStatus = dto.PaymentStatus;
                subscription.StartedDate = dto.StartedDate;
                subscription.ExpiryDate = dto.ExpiryDate;
                subscription.Amount = dto.Amount;
                subscription.Seats = dto.Seats;
                subscription.AutoRenew = dto.AutoRenew;
                subscription.BillingCycle = dto.BillingCycle;

                subscription.ModifiedBy = _currentUserService.UserId;
                subscription.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<CompanySubscription>().Update(subscription);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Company Subscription",
                    "UPDATE",
                    subscription.SubscriptionId,
                    oldValues,
                    JsonConvert.SerializeObject(subscription),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Company Subscription Updated Successfully.",
                    Data = subscription.SubscriptionId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating Company Subscription");
                throw;
            }
        }

        #endregion

        #region DELETE

        public async Task<ApiResponse<string>> DeleteCompanySubscription(int id)
        {
            try
            {
                var subscription = (await _unitOfWork.Repository<CompanySubscription>()
                    .FindAsync(x => x.SubscriptionId == id))
                    .FirstOrDefault();

                if (subscription == null)
                    throw new CustomException("Company Subscription not found.");

                string oldValues = JsonConvert.SerializeObject(subscription);

                subscription.IsDeleted = true;
                subscription.ModifiedBy = _currentUserService.UserId;
                subscription.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<CompanySubscription>().Update(subscription);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Company Subscription",
                    "DELETE",
                    subscription.SubscriptionId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Company Subscription Deleted Successfully.",
                    Data = subscription.SubscriptionId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting Company Subscription");
                throw;
            }
        }

        #endregion

        #region GET ALL

        public async Task<ApiResponse<List<CompanySubscriptionDto>>> GetCompanySubscriptions()
        {
            var organizations = await _unitOfWork.Repository<OrganizationDatum>().GetAllAsync();

            var plans = await _unitOfWork.Repository<SubscriptionPlanMaster>().GetAllAsync();

            var subscriptions = await _unitOfWork.Repository<CompanySubscription>().GetAllAsync();

            var result = (
                from s in subscriptions

                join o in organizations
                    on s.OrganizationId equals o.OrganizationId

                join p in plans
                    on s.PlanId equals p.PlanId

                where !s.IsDeleted && s.CreatedBy == _currentUserService.UserId

                select new CompanySubscriptionDto
                {
                    SubscriptionId = s.SubscriptionId,

                    OrganizationId = s.OrganizationId,
                    OrganizationName = o.OrganizationName,

                    PlanId = s.PlanId,
                    PlanName = p.PlanName,
                    PlanAccent = p.Accent,

                    Status = s.Status,
                    PaymentStatus = s.PaymentStatus,
                    StartedDate = s.StartedDate,
                    ExpiryDate = s.ExpiryDate,
                    Amount = s.Amount,
                    Seats = s.Seats,
                    AutoRenew = s.AutoRenew,
                    BillingCycle = s.BillingCycle
                }
            )
            .OrderByDescending(x => x.SubscriptionId)
            .ToList();

            return new ApiResponse<List<CompanySubscriptionDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        #endregion

        #region GET BY ID

        public async Task<ApiResponse<CompanySubscriptionDto>> GetCompanySubscriptionById(int id)
        {
            var subscription = (await _unitOfWork.Repository<CompanySubscription>()
                .FindAsync(x => x.SubscriptionId == id && !x.IsDeleted))
                .FirstOrDefault();

            if (subscription == null)
                throw new CustomException("Company Subscription not found.");

            return new ApiResponse<CompanySubscriptionDto>
            {
                Success = true,
                Message = "Success",
                Data = new CompanySubscriptionDto
                {
                    SubscriptionId = subscription.SubscriptionId,
                    OrganizationId = subscription.OrganizationId,
                    PlanId = subscription.PlanId,
                    Status = subscription.Status,
                    PaymentStatus = subscription.PaymentStatus,
                    StartedDate = subscription.StartedDate,
                    ExpiryDate = subscription.ExpiryDate,
                    Amount = subscription.Amount,
                    Seats = subscription.Seats,
                    AutoRenew = subscription.AutoRenew,
                    BillingCycle = subscription.BillingCycle
                }
            };
        }

        #endregion
    }
}
