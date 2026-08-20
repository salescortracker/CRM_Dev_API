using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface ICallPurposeService
    {
        Task<ApiResponse<string>> CreateCallPurpose(CallPurposeDto dto);

        Task<ApiResponse<string>> UpdateCallPurpose(CallPurposeDto dto);

        Task<ApiResponse<string>> DeleteCallPurpose(int id);

        Task<ApiResponse<List<CallPurposeDto>>> GetCallPurposes();

        Task<ApiResponse<CallPurposeDto>> GetCallPurposeById(int id);
    }
}
