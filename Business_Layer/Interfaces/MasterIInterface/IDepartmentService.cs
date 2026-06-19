using Business_Layer.DTOs.MasterDTO_s;
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
        Task<ApiResponse<string>> CreateDepartment(DepartmentCreateDto dto);

        Task<ApiResponse<string>>UpdateDepartment(DepartmentUpdateDto dto);

        Task<ApiResponse<string>> DeleteDepartment(int id);

        Task<ApiResponse<List<DepartmentResponseDto>>> GetDepartments();

        Task<ApiResponse<DepartmentResponseDto>> GetDepartmentById(int id);
    }
}
