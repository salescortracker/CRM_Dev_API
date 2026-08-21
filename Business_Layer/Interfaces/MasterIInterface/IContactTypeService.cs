using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface IContactTypeService
    {
        Task<ApiResponse<string>> CreateContactType(ContactTypeDto dto);

        Task<ApiResponse<string>> UpdateContactType(ContactTypeDto dto);

        Task<ApiResponse<string>> DeleteContactType(int id);

        Task<ApiResponse<List<ContactTypeDto>>> GetContactTypes();

        Task<ApiResponse<ContactTypeDto>> GetContactTypeById(int id);
    }
}