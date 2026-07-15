using Business_Layer.DTOs.SuperAdmin;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.SuperAdminInterface
{
    public interface IPlanService
    {
        Task<ApiResponse<string>> CreatePlan(PlanDto dto);

        Task<ApiResponse<string>> UpdatePlan(PlanDto dto);

        Task<ApiResponse<string>> DeletePlan(int id);

        Task<ApiResponse<List<PlanDto>>> GetPlans();

        Task<ApiResponse<PlanDto>> GetPlanById(int id);
    }
}
