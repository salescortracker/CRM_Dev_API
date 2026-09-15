using Business_Layer.DTOs.MasterDTO_s;
using Business_Layer.DTOs.SuperAdmin;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface IDepartmentService
    {
        #region Department

        Task<ApiResponse<string>> CreateDepartment(DepartmentDto dto);

        Task<ApiResponse<string>> UpdateDepartment(DepartmentDto dto);

        Task<ApiResponse<string>> DeleteDepartment(int id);

        Task<ApiResponse<List<DepartmentDto>>> GetDepartments();

        Task<ApiResponse<DepartmentDto>> GetDepartmentById(int id);

        #endregion
        #region Designation

        Task<ApiResponse<string>> CreateDesignation(DesignationDto dto);

        Task<ApiResponse<string>> UpdateDesignation(DesignationDto dto);

        Task<ApiResponse<string>> DeleteDesignation(int id);

        Task<ApiResponse<List<DesignationDto>>> GetDesignations();

        Task<ApiResponse<DesignationDto>> GetDesignationById(int id);

        #endregion
    }
}
