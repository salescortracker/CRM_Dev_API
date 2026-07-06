using Business_Layer.DTOs.Menus;
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
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.MenuServices
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IAuditService _auditService;

        private readonly ICurrentUserService _currentUserService;

        public MenuService(
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
            CreateMenu(MenuDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.MenuName))
                {
                    throw new CustomException("Menu Name is required.");
                }

                var existingMenu =
                    await _unitOfWork.Repository<MenuMaster>()
                    .FindAsync(x => x.MenuName == dto.MenuName);

                if (existingMenu.Any())
                {
                    throw new CustomException("Menu already exists.");
                }

                MenuMaster menu = new MenuMaster
                {
                    MenuName = dto.MenuName,
                    ParentMenuId = dto.ParentMenuId,
                    Url = dto.Url,
                    Icon = dto.Icon,
                    OrderNo = dto.OrderNo,
                    MenuType = dto.MenuType,
                    IsActive = dto.IsActive,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,
                    CanView = dto.CanView,
                    CanAdd = dto.CanAdd,
                    CanEdit = dto.CanEdit,
                    CanDelete = dto.CanDelete,
                    CanApprove = dto.CanApprove
                };

                await _unitOfWork.Repository<MenuMaster>()
                    .AddAsync(menu);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "MenuMaster",
                    "INSERT",
                    menu.MenuId,
                    "",
                    JsonConvert.SerializeObject(menu),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Menu Created Successfully",
                    Data = menu.MenuName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating menu");
                throw;
            }
        }

        #endregion

        #region UPDATE

        public async Task<ApiResponse<string>>
            UpdateMenu(MenuDto dto)
        {
            try
            {
                var menu =
                    (await _unitOfWork.Repository<MenuMaster>()
                    .FindAsync(x => x.MenuId == dto.MenuId))
                    .FirstOrDefault();

                if (menu == null)
                {
                    throw new CustomException("Menu not found.");
                }

                string oldValues =
                    JsonConvert.SerializeObject(menu);

                menu.MenuName = dto.MenuName;
                menu.ParentMenuId = dto.ParentMenuId;
                menu.Url = dto.Url;
                menu.Icon = dto.Icon;
                menu.OrderNo = dto.OrderNo;
                menu.MenuType = dto.MenuType;
                menu.IsActive = dto.IsActive;
                menu.ModifiedBy = _currentUserService.UserId;
                menu.ModifiedAt = DateTime.Now;
                menu.CanView = dto.CanView;
                menu.CanAdd = dto.CanAdd;
                menu.CanEdit = dto.CanEdit;
                menu.CanDelete = dto.CanDelete;
                menu.CanApprove = dto.CanApprove;

                _unitOfWork.Repository<MenuMaster>()
                    .Update(menu);

                await _unitOfWork.CompleteAsync();

                string newValues =
                    JsonConvert.SerializeObject(menu);

                await _auditService.LogAsync(
                    "MenuMaster",
                    "UPDATE",
                    menu.MenuId,
                    oldValues,
                    newValues,
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Menu Updated Successfully",
                    Data = menu.MenuName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating menu");
                throw;
            }
        }

        #endregion

        #region DELETE

        public async Task<ApiResponse<string>>
            DeleteMenu(int id)
        {
            try
            {
                var menu =
                    (await _unitOfWork.Repository<MenuMaster>()
                    .FindAsync(x => x.MenuId == id))
                    .FirstOrDefault();

                if (menu == null)
                {
                    throw new CustomException("Menu not found.");
                }

                string oldValues =
                    JsonConvert.SerializeObject(menu);

                _unitOfWork.Repository<MenuMaster>()
                    .Update(menu);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "MenuMaster",
                    "DELETE",
                    menu.MenuId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Menu Deleted Successfully",
                    Data = menu.MenuName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting menu");
                throw;
            }
        }

        #endregion

        #region GET ALL

        public async Task<ApiResponse<List<MenuDto>>>
            GetMenus()
        {
            var menus =
                await _unitOfWork.Repository<MenuMaster>()
                .GetAllAsync();

            var result = menus
                .Select(x => new MenuDto
                {
                    MenuId = x.MenuId,
                    MenuName = x.MenuName,
                    ParentMenuId = x.ParentMenuId,
                    Url = x.Url,
                    Icon = x.Icon,
                    OrderNo = x.OrderNo,
                    MenuType = x.MenuType,
                    IsActive = x.IsActive,
                    CanView = x.CanView,
                    CanAdd = x.CanAdd,
                    CanEdit = x.CanEdit,
                    CanDelete = x.CanDelete,
                    CanApprove = x.CanApprove
                })
                .OrderBy(x => x.OrderNo)
                .ToList();

            return new ApiResponse<List<MenuDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        #endregion

        #region GET BY ID

        public async Task<ApiResponse<MenuDto>>
            GetMenuById(int id)
        {
            var menu =
                (await _unitOfWork.Repository<MenuMaster>()
                .FindAsync(x => x.MenuId == id))
                .FirstOrDefault();

            if (menu == null)
            {
                throw new CustomException("Menu not found.");
            }

            return new ApiResponse<MenuDto>
            {
                Success = true,
                Message = "Success",
                Data = new MenuDto
                {
                    MenuId = menu.MenuId,
                    MenuName = menu.MenuName,
                    ParentMenuId = menu.ParentMenuId,
                    Url = menu.Url,
                    Icon = menu.Icon,
                    OrderNo = menu.OrderNo,
                    MenuType = menu.MenuType,
                    IsActive = menu.IsActive,
                    CanView = menu.CanView,
                    CanAdd = menu.CanAdd,
                    CanEdit = menu.CanEdit,
                    CanDelete = menu.CanDelete,
                    CanApprove = menu.CanApprove
                }
            };
        }

        #endregion
    }
}
