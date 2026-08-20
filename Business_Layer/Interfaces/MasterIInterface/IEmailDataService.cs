using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface IEmailDataService
    {
        Task<ApiResponse<string>> CreateEmailsTemplate(EmailsTemplateDto dto);

        Task<ApiResponse<string>> UpdateEmailsTemplate(EmailsTemplateDto dto);

        Task<ApiResponse<string>> DeleteEmailsTemplate(int id);

        Task<ApiResponse<List<EmailsTemplateDto>>> GetEmailsTemplates();

        Task<ApiResponse<EmailsTemplateDto>> GetEmailsTemplateById(int id);
    }
}
