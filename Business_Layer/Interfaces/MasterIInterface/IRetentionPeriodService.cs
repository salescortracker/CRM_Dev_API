using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface IRetentionPeriodService
  {
    Task<ApiResponse<string>> CreateRetentionPeriod(RetentionPeriodDto dto);

    Task<ApiResponse<string>> UpdateRetentionPeriod(RetentionPeriodDto dto);

    Task<ApiResponse<string>> DeleteRetentionPeriod(int id);

    Task<ApiResponse<List<RetentionPeriodDto>>> GetRetentionPeriods();

    Task<ApiResponse<RetentionPeriodDto>> GetRetentionPeriodById(int id);
  }
}
