using Business_Layer.DTOs.Admin;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.Adminsevices
{
    public interface IMarketingService
    {
        Task<ApiResponse<string>> CreateCampaign(CampaignDto dto);

        Task<ApiResponse<string>> UpdateCampaign(CampaignDto dto);

        Task<ApiResponse<string>> DeleteCampaign(int id);

        Task<ApiResponse<List<CampaignDto>>> GetCampaigns();

        Task<ApiResponse<CampaignDto>> GetCampaignById(int id);

        Task<ApiResponse<string>> CreateEmailCampaign(EmailCampaignDto dto);

        Task<ApiResponse<string>> UpdateEmailCampaign(EmailCampaignDto dto);

        Task<ApiResponse<string>> DeleteEmailCampaign(int id);

        Task<ApiResponse<List<EmailCampaignDto>>> GetEmailCampaigns();

        Task<ApiResponse<EmailCampaignDto>> GetEmailCampaignById(int id);




        Task<ApiResponse<string>> CreateSmsCampaign(SmsCampaignDto dto);

        Task<ApiResponse<string>> UpdateSmsCampaign(SmsCampaignDto dto);

        Task<ApiResponse<string>> DeleteSmsCampaign(int id);

        Task<ApiResponse<List<SmsCampaignDto>>> GetSmsCampaigns();

        Task<ApiResponse<SmsCampaignDto>> GetSmsCampaignById(int id);

        Task<ApiResponse<string>> CreateWhatsAppCampaign(WhatsAppCampaignDto dto);

        Task<ApiResponse<string>> UpdateWhatsAppCampaign(WhatsAppCampaignDto dto);

        Task<ApiResponse<string>> DeleteWhatsAppCampaign(int id);

        Task<ApiResponse<List<WhatsAppCampaignDto>>> GetWhatsAppCampaigns();

        Task<ApiResponse<WhatsAppCampaignDto>> GetWhatsAppCampaignById(int id);


        Task<ApiResponse<string>> CreateMarketingList(MarketingListDto dto);

        Task<ApiResponse<string>> UpdateMarketingList(MarketingListDto dto);

        Task<ApiResponse<string>> DeleteMarketingList(int id);

        Task<ApiResponse<List<MarketingListDto>>> GetMarketingLists();

        Task<ApiResponse<MarketingListDto>> GetMarketingListById(int id);
    }
}
