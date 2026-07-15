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
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.SuperAdminServices
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public PlanService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        #region CREATE

        public async Task<ApiResponse<string>> CreatePlan(PlanDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.PlanName))
                    throw new CustomException("Plan Name is required.");

                var existing = await _unitOfWork.Repository<SubscriptionPlanMaster>()
                    .FindAsync(x => x.PlanName.ToLower() == dto.PlanName.ToLower());

                if (existing.Any())
                    throw new CustomException("Plan already exists.");

                SubscriptionPlanMaster plan = new SubscriptionPlanMaster
                {
                    PlanName = dto.PlanName,
                    Description = dto.Description,
                    Price = dto.Price,
                    UserLimit = dto.UserLimit,
                    StorageLimit = dto.StorageLimit,
                    ApiLimit = dto.ApiLimit,
                    Accent = dto.Accent,
                    Features = dto.Features,
                    Status = dto.Status,

                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,

                    UserId = _currentUserService.UserId
                };

                await _unitOfWork.Repository<SubscriptionPlanMaster>().AddAsync(plan);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Subscription Plan",
                    "INSERT",
                    plan.PlanId,
                    "",
                    JsonConvert.SerializeObject(plan),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Subscription Plan Created Successfully.",
                    Data = plan.PlanName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating Subscription Plan");
                throw;
            }
        }

        #endregion

        #region UPDATE

        public async Task<ApiResponse<string>> UpdatePlan(PlanDto dto)
        {
            try
            {
                var plan = (await _unitOfWork.Repository<SubscriptionPlanMaster>()
                    .FindAsync(x => x.PlanId == dto.PlanId))
                    .FirstOrDefault();

                if (plan == null)
                    throw new CustomException("Subscription Plan not found.");

                var duplicate = await _unitOfWork.Repository<SubscriptionPlanMaster>()
                    .FindAsync(x =>
                        x.PlanId != dto.PlanId &&
                        x.PlanName.ToLower() == dto.PlanName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Plan Name already exists.");

                string oldValues = JsonConvert.SerializeObject(plan);

                plan.PlanName = dto.PlanName;
                plan.Description = dto.Description;
                plan.Price = dto.Price;
                plan.UserLimit = dto.UserLimit;
                plan.StorageLimit = dto.StorageLimit;
                plan.ApiLimit = dto.ApiLimit;
                plan.Accent = dto.Accent;
                plan.Features = dto.Features;
                plan.Status = dto.Status;

                plan.UpdatedBy = _currentUserService.UserId;
                plan.UpdatedDate = DateTime.Now;

                _unitOfWork.Repository<SubscriptionPlanMaster>().Update(plan);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Subscription Plan",
                    "UPDATE",
                    plan.PlanId,
                    oldValues,
                    JsonConvert.SerializeObject(plan),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Subscription Plan Updated Successfully.",
                    Data = plan.PlanName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating Subscription Plan");
                throw;
            }
        }

        #endregion
        #region DELETE

        public async Task<ApiResponse<string>> DeletePlan(int id)
        {
            try
            {
                var plan = (await _unitOfWork.Repository<SubscriptionPlanMaster>()
                    .FindAsync(x => x.PlanId == id))
                    .FirstOrDefault();

                if (plan == null)
                    throw new CustomException("Subscription Plan not found.");

                // Check whether this plan is assigned to any company
                var companyExists = (await _unitOfWork.Repository<Company>()
                    .FindAsync(x => x.PlanId == id))
                    .Any();

                if (companyExists)
                    throw new CustomException("This Subscription Plan is assigned to a Company and cannot be deleted.");

                string oldValues = JsonConvert.SerializeObject(plan);

                _unitOfWork.Repository<SubscriptionPlanMaster>().Remove(plan);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Subscription Plan",
                    "DELETE",
                    plan.PlanId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Subscription Plan Deleted Successfully.",
                    Data = plan.PlanName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting Subscription Plan");
                throw;
            }
        }

        #endregion

        #region GET ALL

        public async Task<ApiResponse<List<PlanDto>>> GetPlans()
        {
            var plans = (await _unitOfWork.Repository<SubscriptionPlanMaster>()
                .GetAllAsync())
                .OrderByDescending(x => x.PlanId)
                .ToList();

            var result = plans.Select(x => new PlanDto
            {
                PlanId = x.PlanId,
                PlanName = x.PlanName,
                Description = x.Description,
                Price = x.Price,
                UserLimit = x.UserLimit,
                StorageLimit = x.StorageLimit,
                ApiLimit = x.ApiLimit,
                Accent = x.Accent,
                Features = x.Features,
                Status = x.Status
            }).ToList();

            return new ApiResponse<List<PlanDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        #endregion

        #region GET BY ID

        public async Task<ApiResponse<PlanDto>> GetPlanById(int id)
        {
            var plan = (await _unitOfWork.Repository<SubscriptionPlanMaster>()
                .FindAsync(x => x.PlanId == id))
                .FirstOrDefault();

            if (plan == null)
                throw new CustomException("Subscription Plan not found.");

            return new ApiResponse<PlanDto>
            {
                Success = true,
                Message = "Success",
                Data = new PlanDto
                {
                    PlanId = plan.PlanId,
                    PlanName = plan.PlanName,
                    Description = plan.Description,
                    Price = plan.Price,
                    UserLimit = plan.UserLimit,
                    StorageLimit = plan.StorageLimit,
                    ApiLimit = plan.ApiLimit,
                    Accent = plan.Accent,
                    Features = plan.Features,
                    Status = plan.Status
                }
            };
        }

        #endregion
    }
}
