using Business_Layer.DTOs.AccessPolicies;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.Services;
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

namespace Business_Layer.Services.AccessPolicyServices
{
    public class AccessPolicyService : IAccessPolicyService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IAuditService _auditService;

        private readonly ICurrentUserService _currentUserService;

        public AccessPolicyService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        #region CREATE

        public async Task<ApiResponse<string>>
            CreatePolicy(AccessPolicyDto dto)
        {
            try
            {
                ValidatePolicy(dto);

                var existing =
                    await _unitOfWork.Repository<AccessPolicy>()
                    .FindAsync(x => x.PolicyName.ToLower() == dto.PolicyName.Trim().ToLower());

                if (existing.Any())
                {
                    throw new CustomException("Policy Name already exists.");
                }

                string policyCode =
                    await GenerateUniquePolicyCode(dto.PolicyName.Trim());

                AccessPolicy policy = new AccessPolicy
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    RoleName = string.IsNullOrWhiteSpace(dto.RoleName) ? "User" : dto.RoleName.Trim(),
                    DepartmentId = dto.DepartmentId,
                    PolicyName = dto.PolicyName.Trim(),
                    PolicyCode = policyCode,
                    Modules = dto.Modules,
                    IpRestriction = dto.IpRestriction,
                    AllowedIp = string.IsNullOrWhiteSpace(dto.AllowedIp) ? null : dto.AllowedIp.Trim(),
                    LoginRestriction = string.IsNullOrWhiteSpace(dto.LoginRestriction) ? "24 Hours" : dto.LoginRestriction,
                    SessionTimeout = dto.SessionTimeout,
                    Status = dto.Status,
                    IsDefault = dto.IsDefault,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now
                };

                await _unitOfWork.Repository<AccessPolicy>()
                    .AddAsync(policy);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "AccessPolicy",
                    "INSERT",
                    policy.PolicyId,
                    "",
                    JsonConvert.SerializeObject(policy),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Access Policy Created Successfully",
                    Data = policy.PolicyName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating access policy");
                throw;
            }
        }

        #endregion

        #region UPDATE

        public async Task<ApiResponse<string>>
            UpdatePolicy(AccessPolicyDto dto)
        {
            try
            {
                ValidatePolicy(dto);

                var policy =
                    (await _unitOfWork.Repository<AccessPolicy>()
                    .FindAsync(x => x.PolicyId == dto.PolicyId))
                    .FirstOrDefault();

                if (policy == null)
                {
                    throw new CustomException("Access Policy not found.");
                }

                var duplicateName =
                    await _unitOfWork.Repository<AccessPolicy>()
                    .FindAsync(x =>
                        x.PolicyId != dto.PolicyId &&
                        x.PolicyName.ToLower() == dto.PolicyName.Trim().ToLower());

                if (duplicateName.Any())
                {
                    throw new CustomException("Policy Name already exists.");
                }

                string oldValues =
                    JsonConvert.SerializeObject(policy);

                policy.CompanyId = dto.CompanyId;
                policy.RegionId = dto.RegionId;
                policy.RoleName = string.IsNullOrWhiteSpace(dto.RoleName) ? "User" : dto.RoleName.Trim();
                policy.DepartmentId = dto.DepartmentId;
                policy.PolicyName = dto.PolicyName.Trim();
                policy.Modules = dto.Modules;
                policy.IpRestriction = dto.IpRestriction;
                policy.AllowedIp = string.IsNullOrWhiteSpace(dto.AllowedIp) ? null : dto.AllowedIp.Trim();
                policy.LoginRestriction = string.IsNullOrWhiteSpace(dto.LoginRestriction) ? "24 Hours" : dto.LoginRestriction;
                policy.SessionTimeout = dto.SessionTimeout;
                policy.Status = dto.Status;
                policy.IsDefault = dto.IsDefault;
                policy.ModifiedBy = _currentUserService.UserId;
                policy.ModifiedDate = DateTime.Now;

                _unitOfWork.Repository<AccessPolicy>()
                    .Update(policy);

                await _unitOfWork.CompleteAsync();

                string newValues =
                    JsonConvert.SerializeObject(policy);

                await _auditService.LogAsync(
                    "AccessPolicy",
                    "UPDATE",
                    policy.PolicyId,
                    oldValues,
                    newValues,
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Access Policy Updated Successfully",
                    Data = policy.PolicyName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating access policy");
                throw;
            }
        }

        #endregion

        #region DELETE

        public async Task<ApiResponse<string>>
            DeletePolicy(int id)
        {
            try
            {
                var policy =
                    (await _unitOfWork.Repository<AccessPolicy>()
                    .FindAsync(x => x.PolicyId == id))
                    .FirstOrDefault();

                if (policy == null)
                {
                    throw new CustomException("Access Policy not found.");
                }

                string oldValues =
                    JsonConvert.SerializeObject(policy);

                policy.Status = false;
                policy.ModifiedBy = _currentUserService.UserId;
                policy.ModifiedDate = DateTime.Now;

                _unitOfWork.Repository<AccessPolicy>()
                    .Update(policy);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "AccessPolicy",
                    "DELETE",
                    policy.PolicyId,
                    oldValues,
                    JsonConvert.SerializeObject(policy),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Access Policy Deleted Successfully",
                    Data = policy.PolicyName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting access policy");
                throw;
            }
        }

        #endregion

        #region GET ALL

        public async Task<ApiResponse<List<AccessPolicyDto>>>
            GetPolicies()
        {
            try
            {
                var policies =
                    (await _unitOfWork.Repository<AccessPolicy>()
                    .FindAsync(x =>
                        x.Status &&
                        x.CreatedBy == _currentUserService.UserId))
                    .OrderByDescending(x => x.PolicyId)
                    .ToList();

                var companies = (await _unitOfWork.Repository<Company>().GetAllAsync()).ToList();

                var regions = (await _unitOfWork.Repository<Region>().GetAllAsync()).ToList();

                var departments = (await _unitOfWork.Repository<Department>().GetAllAsync()).ToList();

                var result = policies.Select(x => MapToDto(x, companies, regions, departments)).ToList();

                return new ApiResponse<List<AccessPolicyDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting access policies");
                throw;
            }
        }

        #endregion

        #region GET BY ID

        public async Task<ApiResponse<AccessPolicyDto>>
            GetPolicyById(int id)
        {
            try
            {
                var policy =
                    (await _unitOfWork.Repository<AccessPolicy>()
                    .FindAsync(x => x.PolicyId == id))
                    .FirstOrDefault();

                if (policy == null)
                {
                    throw new CustomException("Access Policy not found.");
                }

                var companies = (await _unitOfWork.Repository<Company>().GetAllAsync()).ToList();

                var regions = (await _unitOfWork.Repository<Region>().GetAllAsync()).ToList();

                var departments = (await _unitOfWork.Repository<Department>().GetAllAsync()).ToList();

                return new ApiResponse<AccessPolicyDto>
                {
                    Success = true,
                    Message = "Success",
                    Data = MapToDto(policy, companies, regions, departments)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting access policy by id");
                throw;
            }
        }

        #endregion

        #region HELPERS

        private static void ValidatePolicy(AccessPolicyDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.PolicyName))
            {
                throw new CustomException("Policy Name is required.");
            }

            if (dto.CompanyId <= 0)
            {
                throw new CustomException("Company is required.");
            }
        }

        private static AccessPolicyDto MapToDto(
            AccessPolicy policy,
            List<Company> companies,
            List<Region> regions,
            List<Department> departments)
        {
            return new AccessPolicyDto
            {
                PolicyId = policy.PolicyId,
                CompanyId = policy.CompanyId,
                CompanyName = companies.FirstOrDefault(c => c.CompanyId == policy.CompanyId)?.CompanyName,
                RegionId = policy.RegionId,
                RegionName = policy.RegionId.HasValue
                    ? regions.FirstOrDefault(r => r.RegionId == policy.RegionId.Value)?.RegionName
                    : null,
                RoleName = policy.RoleName,
                DepartmentId = policy.DepartmentId,
                DepartmentName = policy.DepartmentId.HasValue
                    ? departments.FirstOrDefault(d => d.DepartmentId == policy.DepartmentId.Value)?.DepartmentName
                    : null,
                PolicyName = policy.PolicyName,
                PolicyCode = policy.PolicyCode,
                Modules = policy.Modules,
                IpRestriction = policy.IpRestriction,
                AllowedIp = policy.AllowedIp,
                LoginRestriction = policy.LoginRestriction,
                SessionTimeout = policy.SessionTimeout,
                Status = policy.Status,
                IsDefault = policy.IsDefault
            };
        }

        private async Task<string> GenerateUniquePolicyCode(string policyName)
        {
            const int maxLength = 20;

            string baseCode =
                "POL" + new string(policyName.ToUpper().Where(char.IsLetterOrDigit).ToArray());

            if (baseCode.Length > maxLength)
            {
                baseCode = baseCode.Substring(0, maxLength);
            }

            string code = baseCode;
            int suffix = 1;

            while ((await _unitOfWork.Repository<AccessPolicy>().FindAsync(x => x.PolicyCode == code)).Any())
            {
                suffix++;

                string suffixText = suffix.ToString();

                int maxBaseLength = maxLength - suffixText.Length;

                string trimmedBase =
                    baseCode.Length > maxBaseLength
                        ? baseCode.Substring(0, maxBaseLength)
                        : baseCode;

                code = trimmedBase + suffixText;
            }

            return code;
        }

        #endregion
    }
}
