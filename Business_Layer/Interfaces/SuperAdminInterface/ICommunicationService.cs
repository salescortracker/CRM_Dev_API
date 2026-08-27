using Business_Layer.DTOs.SuperAdmin;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.SuperAdminInterface
{
    public interface ICommunicationService
    {
        #region CommunicationEmail
        Task<ApiResponse<string>> CreateCommunicationEmail(CommunicationEmailDto dto);

        Task<ApiResponse<string>> UpdateCommunicationEmail(CommunicationEmailDto dto);

        Task<ApiResponse<string>> DeleteCommunicationEmail(int id);

        Task<ApiResponse<List<CommunicationEmailDto>>> GetCommunicationEmails();

        Task<ApiResponse<CommunicationEmailDto>> GetCommunicationEmailById(int id);
        #endregion
        #region CommunicationSMS
        Task<ApiResponse<string>> CreateCommunicationSMS(CommunicationSMSDto dto);

        Task<ApiResponse<string>> UpdateCommunicationSMS(CommunicationSMSDto dto);

        Task<ApiResponse<string>> DeleteCommunicationSMS(int id);

        Task<ApiResponse<List<CommunicationSMSDto>>> GetCommunicationSMS();

        Task<ApiResponse<CommunicationSMSDto>> GetCommunicationSMSById(int id);
        #endregion
        #region CommunicationWhatsApp
        Task<ApiResponse<string>> CreateCommunicationWhatsApp(CommunicationWhatsAppDto dto);

        Task<ApiResponse<string>> UpdateCommunicationWhatsApp(CommunicationWhatsAppDto dto);

        Task<ApiResponse<string>> DeleteCommunicationWhatsApp(int id);

        Task<ApiResponse<List<CommunicationWhatsAppDto>>> GetCommunicationWhatsApps();

        Task<ApiResponse<CommunicationWhatsAppDto>> GetCommunicationWhatsAppById(int id);
        #endregion
        #region CommunicationVoice
        Task<ApiResponse<string>> CreateCommunicationVoice(CommunicationVoiceDto dto);

        Task<ApiResponse<string>> UpdateCommunicationVoice(CommunicationVoiceDto dto);

        Task<ApiResponse<string>> DeleteCommunicationVoice(int id);

        Task<ApiResponse<List<CommunicationVoiceDto>>> GetCommunicationVoices();

        Task<ApiResponse<CommunicationVoiceDto>> GetCommunicationVoiceById(int id);
        #endregion
        #region CommunicationEmailTemplate
        Task<ApiResponse<string>> CreateEmailTemplate(CommunicationEmailTemplateDto dto);

        Task<ApiResponse<string>> UpdateEmailTemplate(CommunicationEmailTemplateDto dto);

        Task<ApiResponse<string>> DeleteEmailTemplate(int id);

        Task<ApiResponse<List<CommunicationEmailTemplateDto>>> GetEmailTemplates();

        Task<ApiResponse<CommunicationEmailTemplateDto>> GetEmailTemplateById(int id);
        #endregion
        #region CommunicationSMSTemplate
        Task<ApiResponse<string>> CreateSMSTemplate(CommunicationSMSTemplateDto dto);

        Task<ApiResponse<string>> UpdateSMSTemplate(CommunicationSMSTemplateDto dto);

        Task<ApiResponse<string>> DeleteSMSTemplate(int id);

        Task<ApiResponse<List<CommunicationSMSTemplateDto>>> GetSMSTemplates();

        Task<ApiResponse<CommunicationSMSTemplateDto>> GetSMSTemplateById(int id);
        #endregion
        #region CommunicationWhatsAppTemplate
        Task<ApiResponse<string>> CreateWhatsAppTemplate(CommunicationWhatsAppTemplateDto dto);

        Task<ApiResponse<string>> UpdateWhatsAppTemplate(CommunicationWhatsAppTemplateDto dto);

        Task<ApiResponse<string>> DeleteWhatsAppTemplate(int id);

        Task<ApiResponse<List<CommunicationWhatsAppTemplateDto>>> GetWhatsAppTemplates();

        Task<ApiResponse<CommunicationWhatsAppTemplateDto>> GetWhatsAppTemplateById(int id);
        #endregion
        #region CommunicationNotificationTemplate
        Task<ApiResponse<string>> CreateNotificationTemplate(CommunicationNotificationTemplateDto dto);

        Task<ApiResponse<string>> UpdateNotificationTemplate(CommunicationNotificationTemplateDto dto);

        Task<ApiResponse<string>> DeleteNotificationTemplate(int id);

        Task<ApiResponse<List<CommunicationNotificationTemplateDto>>> GetNotificationTemplates();

        Task<ApiResponse<CommunicationNotificationTemplateDto>> GetNotificationTemplateById(int id);
        #endregion


    }
}
