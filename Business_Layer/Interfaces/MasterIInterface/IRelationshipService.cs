using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface IRelationshipService
    {
        Task<ApiResponse<string>> CreateRelationship(RelationshipDto dto);

        Task<ApiResponse<string>> UpdateRelationship(RelationshipDto dto);

        Task<ApiResponse<string>> DeleteRelationship(int id);

        Task<ApiResponse<List<RelationshipDto>>> GetRelationships();

        Task<ApiResponse<RelationshipDto>> GetRelationshipById(int id);
    }
}