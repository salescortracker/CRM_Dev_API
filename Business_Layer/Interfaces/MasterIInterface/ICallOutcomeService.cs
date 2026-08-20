using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface ICallOutcomeService
    {
        Task<ApiResponse<string>> CreateCallOutcome(CallOutcomeDto dto);

        Task<ApiResponse<string>> UpdateCallOutcome(CallOutcomeDto dto);

        Task<ApiResponse<string>> DeleteCallOutcome(int id);

        Task<ApiResponse<List<CallOutcomeDto>>> GetCallOutcomes();

        Task<ApiResponse<CallOutcomeDto>> GetCallOutcomeById(int id);
    }
}
