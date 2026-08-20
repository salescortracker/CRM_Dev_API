using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
  public interface IBackupFrequencyService
  {
    Task<ApiResponse<string>> CreateBackupFrequency(BackupFrequencyDto dto);

    Task<ApiResponse<string>> UpdateBackupFrequency(BackupFrequencyDto dto);

    Task<ApiResponse<string>> DeleteBackupFrequency(int id);

    Task<ApiResponse<List<BackupFrequencyDto>>> GetBackupFrequencies();

    Task<ApiResponse<BackupFrequencyDto>> GetBackupFrequencyById(int id);
  }
}
