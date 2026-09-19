using Business_Layer.DTOs.LoginHistories;
using Shared.CommonModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.Services
{
    public interface ILoginHistoryService
    {
        Task<ApiResponse<List<LoginHistoryDto>>> GetLoginHistories();
    }
}
