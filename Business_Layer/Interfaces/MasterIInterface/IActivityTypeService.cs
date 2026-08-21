using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface IActivityTypeService
    {
        Task<ApiResponse<string>> CreateActivityType(ActivityTypeDto dto);

        Task<ApiResponse<string>> UpdateActivityType(ActivityTypeDto dto);

        Task<ApiResponse<string>> DeleteActivityType(int id);

        Task<ApiResponse<List<ActivityTypeDto>>> GetActivityTypes();

        Task<ApiResponse<ActivityTypeDto>> GetActivityTypeById(int id);
    }
}