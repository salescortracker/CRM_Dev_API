using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface IEmailCategoryService
    {
        Task<ApiResponse<string>> CreateEmailCategory(EmailCategoryDto dto);

        Task<ApiResponse<string>> UpdateEmailCategory(EmailCategoryDto dto);

        Task<ApiResponse<string>> DeleteEmailCategory(int id);

        Task<ApiResponse<List<EmailCategoryDto>>> GetEmailCategories();

        Task<ApiResponse<EmailCategoryDto>> GetEmailCategoryById(int id);
    }
}