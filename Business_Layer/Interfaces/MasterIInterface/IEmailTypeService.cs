using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface IEmailTypeService
  {
    Task<ApiResponse<string>> CreateEmailType(EmailTypeDto dto);

    Task<ApiResponse<string>> UpdateEmailType(EmailTypeDto dto);

    Task<ApiResponse<string>> DeleteEmailType(int id);

    Task<ApiResponse<List<EmailTypeDto>>> GetEmailTypes();

    Task<ApiResponse<EmailTypeDto>> GetEmailTypeById(int id);
  }
}
