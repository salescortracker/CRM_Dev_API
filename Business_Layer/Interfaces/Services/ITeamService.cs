using Business_Layer.DTOs.Teams;
using Shared.CommonModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.Services
{
    public interface ITeamService
    {
        Task<ApiResponse<string>> CreateTeam(WorkTeamDto dto);

        Task<ApiResponse<string>> UpdateTeam(WorkTeamDto dto);

        Task<ApiResponse<string>> DeleteTeam(int id);

        Task<ApiResponse<List<WorkTeamDto>>> GetTeams();

        Task<ApiResponse<WorkTeamDto>> GetTeamById(int id);
    }
}
