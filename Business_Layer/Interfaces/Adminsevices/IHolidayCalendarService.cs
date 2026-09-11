using Business_Layer.DTOs.Admin;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.Adminsevices
{
    public interface IHolidayCalendarService
    {
        Task<ApiResponse<string>> CreateHolidayCalendar(
       HolidayCalendarDto dto);

        Task<ApiResponse<string>> UpdateHolidayCalendar(
            HolidayCalendarDto dto);

        Task<ApiResponse<string>> DeleteHolidayCalendar(int id);

        Task<ApiResponse<List<HolidayCalendarDto>>> GetHolidayCalendars();

        Task<ApiResponse<HolidayCalendarDto>> GetHolidayCalendarById(
            int id);
    }
}
