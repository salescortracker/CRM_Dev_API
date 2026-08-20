using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface ICallTypeService
    {
        Task<ApiResponse<string>> CreateCallType(CallTypeDto dto);

        Task<ApiResponse<string>> UpdateCallType(CallTypeDto dto);

        Task<ApiResponse<string>> DeleteCallType(int id);

        Task<ApiResponse<List<CallTypeDto>>> GetCallTypes();

        Task<ApiResponse<CallTypeDto>> GetCallTypeById(int id);
    }
}
