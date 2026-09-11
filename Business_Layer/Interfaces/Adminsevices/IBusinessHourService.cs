using Business_Layer.DTOs.Admin;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.Adminsevices
{
    public interface IBusinessHourService
    {
        Task<ApiResponse<string>> CreateBusinessHour(BusinessHourDto dto);

        Task<ApiResponse<string>> UpdateBusinessHour(BusinessHourDto dto);

        Task<ApiResponse<string>> DeleteBusinessHour(int id);

        Task<ApiResponse<List<BusinessHourDto>>> GetBusinessHours();

        Task<ApiResponse<BusinessHourDto>> GetBusinessHourById(int id);
    }
}
