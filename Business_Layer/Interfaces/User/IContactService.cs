using Business_Layer.DTOs.User;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.User
{
    public interface IContactService
    {
        Task<ApiResponse<string>> CreateContact(ContactDto dto);

        Task<ApiResponse<string>> UpdateContact(ContactDto dto);

        Task<ApiResponse<string>> DeleteContact(int id);

        Task<ApiResponse<List<ContactDto>>> GetContacts();

        Task<ApiResponse<ContactDto>> GetContactById(int id);

    }
}
