using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface Istateservices
    {

        Task<ApiResponse<string>> CreateState(StateDto dto);

        Task<ApiResponse<string>> UpdateState(StateDto dto);

        Task<ApiResponse<string>> DeleteState(int id);

        Task<ApiResponse<List<StateDto>>> GetStates();

        Task<ApiResponse<StateDto>> GetStateById(int id);
    }
}
