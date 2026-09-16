using Business_Layer.DTOs.Roles;
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

namespace Business_Layer.Services.RoleServices
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IAuditService _auditService;

        private readonly ICurrentUserService _currentUserService;

        public RoleService(
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
            CreateRole(RoleDto dto)
        {
            try
            {
                ValidateRole(dto);

                var existingRole =
                    await _unitOfWork.Repository<Role>()
                    .FindAsync(x => x.RoleName.ToLower() == dto.RoleName.Trim().ToLower());

                if (existingRole.Any())
                {
                    throw new CustomException("Role Name already exists.");
                }

                string roleCode =
                    await GenerateUniqueRoleCode(dto.RoleName.Trim());

                Role role = new Role
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    RoleName = dto.RoleName.Trim(),
                    RoleCode = roleCode,
                    RoleType = dto.AccessLevel,
                    HierarchyLevel = MapHierarchyLevel(dto.AccessLevel),
                    Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim(),
                    Status = dto.Status,
                    IsDefault = dto.IsDefault,
                    UserCount = dto.UserCount,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,
                    UserId = _currentUserService.UserId
                };

                await _unitOfWork.Repository<Role>()
                    .AddAsync(role);

                await _unitOfWork.CompleteAsync();

                await SyncRolePermissions(role.RoleId, dto.Permissions);

                await _auditService.LogAsync(
                    "Role",
                    "INSERT",
                    role.RoleId,
                    "",
                    JsonConvert.SerializeObject(role),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Role Created Successfully",
                    Data = role.RoleName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating role");
                throw;
            }
        }

        #endregion

        #region UPDATE

        public async Task<ApiResponse<string>>
            UpdateRole(RoleDto dto)
        {
            try
            {
                ValidateRole(dto);

                var role =
                    (await _unitOfWork.Repository<Role>()
                    .FindAsync(x => x.RoleId == dto.RoleId))
                    .FirstOrDefault();

                if (role == null)
                {
                    throw new CustomException("Role not found.");
                }

                var duplicateName =
                    await _unitOfWork.Repository<Role>()
                    .FindAsync(x =>
                        x.RoleId != dto.RoleId &&
                        x.RoleName.ToLower() == dto.RoleName.Trim().ToLower());

                if (duplicateName.Any())
                {
                    throw new CustomException("Role Name already exists.");
                }

                string oldValues =
                    JsonConvert.SerializeObject(role);

                role.CompanyId = dto.CompanyId;
                role.RegionId = dto.RegionId;
                role.RoleName = dto.RoleName.Trim();
                role.RoleType = dto.AccessLevel;
                role.HierarchyLevel = MapHierarchyLevel(dto.AccessLevel);
                role.Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim();
                role.Status = dto.Status;
                role.IsDefault = dto.IsDefault;
                role.UserCount = dto.UserCount;
                role.UpdatedBy = _currentUserService.UserId;
                role.UpdatedDate = DateTime.Now;

                _unitOfWork.Repository<Role>()
                    .Update(role);

                await _unitOfWork.CompleteAsync();

                await SyncRolePermissions(role.RoleId, dto.Permissions);

                string newValues =
                    JsonConvert.SerializeObject(role);

                await _auditService.LogAsync(
                    "Role",
                    "UPDATE",
                    role.RoleId,
                    oldValues,
                    newValues,
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Role Updated Successfully",
                    Data = role.RoleName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating role");
                throw;
            }
        }

        #endregion

        #region DELETE

        public async Task<ApiResponse<string>>
            DeleteRole(int id)
        {
            try
            {
                var role =
                    (await _unitOfWork.Repository<Role>()
                    .FindAsync(x => x.RoleId == id))
                    .FirstOrDefault();

                if (role == null)
                {
                    throw new CustomException("Role not found.");
                }

                string oldValues =
                    JsonConvert.SerializeObject(role);

                role.Status = false;
                role.UpdatedBy = _currentUserService.UserId;
                role.UpdatedDate = DateTime.Now;

                _unitOfWork.Repository<Role>()
                    .Update(role);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Role",
                    "DELETE",
                    role.RoleId,
                    oldValues,
                    JsonConvert.SerializeObject(role),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Role Deleted Successfully",
                    Data = role.RoleName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting role");
                throw;
            }
        }

        #endregion

        #region GET ALL

        public async Task<ApiResponse<List<RoleDto>>>
            GetRoles()
        {
            try
            {
                var roles =
                    (await _unitOfWork.Repository<Role>()
                    .FindAsync(x =>
                        x.Status &&
                        x.CreatedBy == _currentUserService.UserId))
                    .OrderByDescending(x => x.RoleId)
                    .ToList();

                var roleIds = roles.Select(x => x.RoleId).ToList();

                var permissions =
                    (await _unitOfWork.Repository<RoleMenuPermission>()
                    .FindAsync(x => roleIds.Contains(x.RoleId) && x.IsAllowed))
                    .ToList();

                var result = roles.Select(x => MapToDto(x, permissions)).ToList();

                return new ApiResponse<List<RoleDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting roles");
                throw;
            }
        }

        #endregion

        #region GET BY ID

        public async Task<ApiResponse<RoleDto>>
            GetRoleById(int id)
        {
            try
            {
                var role =
                    (await _unitOfWork.Repository<Role>()
                    .FindAsync(x => x.RoleId == id))
                    .FirstOrDefault();

                if (role == null)
                {
                    throw new CustomException("Role not found.");
                }

                var permissions =
                    (await _unitOfWork.Repository<RoleMenuPermission>()
                    .FindAsync(x => x.RoleId == id && x.IsAllowed))
                    .ToList();

                return new ApiResponse<RoleDto>
                {
                    Success = true,
                    Message = "Success",
                    Data = MapToDto(role, permissions)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting role by id");
                throw;
            }
        }

        #endregion

        #region HELPERS

        private static void ValidateRole(RoleDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.RoleName))
            {
                throw new CustomException("Role Name is required.");
            }

            if (dto.CompanyId <= 0)
            {
                throw new CustomException("Company is required.");
            }

            if (dto.RegionId <= 0)
            {
                throw new CustomException("Region is required.");
            }
        }

        private static RoleDto MapToDto(Role role, List<RoleMenuPermission> permissions)
        {
            return new RoleDto
            {
                RoleId = role.RoleId,
                CompanyId = role.CompanyId,
                RegionId = role.RegionId,
                RoleName = role.RoleName,
                AccessLevel = role.RoleType,
                Description = role.Description,
                Status = role.Status,
                IsDefault = role.IsDefault,
                UserCount = role.UserCount,
                Permissions = permissions
                    .Where(x => x.RoleId == role.RoleId)
                    .ToDictionary(x => x.MenuId, x => x.IsAllowed)
            };
        }

        private async Task SyncRolePermissions(int roleId, Dictionary<int, bool> permissions)
        {
            var existing =
                (await _unitOfWork.Repository<RoleMenuPermission>()
                .FindAsync(x => x.RoleId == roleId))
                .ToList();

            foreach (var item in existing)
            {
                _unitOfWork.Repository<RoleMenuPermission>().Remove(item);
            }

            if (permissions != null)
            {
                foreach (var kvp in permissions.Where(x => x.Value))
                {
                    await _unitOfWork.Repository<RoleMenuPermission>()
                        .AddAsync(new RoleMenuPermission
                        {
                            RoleId = roleId,
                            MenuId = kvp.Key,
                            IsAllowed = true,
                            CreatedBy = _currentUserService.UserId,
                            CreatedDate = DateTime.Now
                        });
                }
            }

            await _unitOfWork.CompleteAsync();
        }

        private static int MapHierarchyLevel(string accessLevel)
        {
            return (accessLevel ?? string.Empty).Trim() switch
            {
                "Full Access" => 1,
                "Admin Access" => 2,
                "Limited Access" => 3,
                "Read Only" => 4,
                _ => 5
            };
        }

        private async Task<string> GenerateUniqueRoleCode(string roleName)
        {
            const int maxLength = 20;

            string baseCode =
                new string(roleName.ToUpper().Where(char.IsLetterOrDigit).ToArray());

            if (string.IsNullOrWhiteSpace(baseCode))
            {
                baseCode = "ROLE";
            }

            if (baseCode.Length > maxLength)
            {
                baseCode = baseCode.Substring(0, maxLength);
            }

            string code = baseCode;
            int suffix = 1;

            while ((await _unitOfWork.Repository<Role>().FindAsync(x => x.RoleCode == code)).Any())
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
