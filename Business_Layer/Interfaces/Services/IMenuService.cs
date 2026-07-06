using Business_Layer.DTOs.Menus;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.Services
{
    public interface IMenuService
    {
        Task<ApiResponse<string>> CreateMenu(MenuDto dto);

        Task<ApiResponse<string>> UpdateMenu(MenuDto dto);

        Task<ApiResponse<string>> DeleteMenu(int id);

        Task<ApiResponse<List<MenuDto>>> GetMenus();

        Task<ApiResponse<MenuDto>> GetMenuById(int id);
    }
}
