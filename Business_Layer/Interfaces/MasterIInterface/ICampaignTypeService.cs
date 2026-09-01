using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface ICampaignTypeService
    {
        Task<ApiResponse<string>> CreateCampaignType(CampaignTypeDto dto);

        Task<ApiResponse<string>> UpdateCampaignType(CampaignTypeDto dto);

        Task<ApiResponse<string>> DeleteCampaignType(int id);

        Task<ApiResponse<List<CampaignTypeDto>>> GetCampaignTypes();

        Task<ApiResponse<CampaignTypeDto>> GetCampaignTypeById(int id);
    }
}