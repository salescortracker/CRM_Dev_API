using Business_Layer.DTOs.Admin;
using Business_Layer.Interfaces;
using Business_Layer.Interfaces.Adminsevices;
using Business_Layer.Interfaces.SuperAdminInterface;
using Business_Layer.Services.Adminservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
      
       
        private readonly ICompanyProfileService _companyProfileService;
        private readonly IBusinessHourService _businessHourService;
        private readonly IHolidayCalendarService _holidayCalendarService;   


        public AdminController(ICompanyProfileService companyProfileService, IBusinessHourService businessHourService, IHolidayCalendarService holidayCalendarService)
        {
            _companyProfileService = companyProfileService;
            _businessHourService = businessHourService;
            _holidayCalendarService = holidayCalendarService;
        }
        #region Company Profile CRUD

        [HttpPost("createcompanyprofile")]
        public async Task<IActionResult> CreateCompanyProfile(
            CompanyProfileDto dto)
        {
            return Ok(
                await _companyProfileService
                    .CreateCompanyProfile(dto)
            );
        }


        [HttpPost("updatecompanyprofile")]
        public async Task<IActionResult> UpdateCompanyProfile(
            CompanyProfileDto dto)
        {
            return Ok(
                await _companyProfileService
                    .UpdateCompanyProfile(dto)
            );
        }


        [HttpPost("deletecompanyprofile/{id}")]
        public async Task<IActionResult> DeleteCompanyProfile(
            int id)
        {
            return Ok(
                await _companyProfileService
                    .DeleteCompanyProfile(id)
            );
        }


        [HttpGet("getallcompanyprofile")]
        public async Task<IActionResult> GetCompanyProfiles()
        {
            return Ok(
                await _companyProfileService
                    .GetCompanyProfiles()
            );
        }


        [HttpGet("getbycompanyprofile/{id}")]
        public async Task<IActionResult>
            GetCompanyProfileById(int id)
        {
            return Ok(
                await _companyProfileService
                    .GetCompanyProfileById(id)
            );
        }

        
        #endregion

        [HttpPost("createbusinesshour")]
        public async Task<IActionResult> CreateBusinessHour(
           [FromBody] BusinessHourDto dto)
        {
            var response =
                await _businessHourService.CreateBusinessHour(dto);

            return Ok(response);
        }

        [HttpPost("updatebusinesshour")]
        public async Task<IActionResult> UpdateBusinessHour(
            [FromBody] BusinessHourDto dto)
        {
            var response =
                await _businessHourService.UpdateBusinessHour(dto);

            return Ok(response);
        }

        [HttpPost("deletebusinesshour/{id}")]
        public async Task<IActionResult> DeleteBusinessHour(int id)
        {
            var response =
                await _businessHourService.DeleteBusinessHour(id);

            return Ok(response);
        }

        [HttpGet("getallbusinesshour")]
        public async Task<IActionResult> GetBusinessHours()
        {
            var response =
                await _businessHourService.GetBusinessHours();

            return Ok(response);
        }

        [HttpGet("getbybusinesshour/{id}")]
        public async Task<IActionResult> GetBusinessHourById(int id)
        {
            var response =
                await _businessHourService.GetBusinessHourById(id);

            return Ok(response);
        }
        [HttpPost("createholidaycalendar")]
        public async Task<IActionResult> CreateHolidayCalendar(
           [FromBody] HolidayCalendarDto dto)
        {
            var response =
                await _holidayCalendarService
                    .CreateHolidayCalendar(dto);

            return Ok(response);
        }

        [HttpPost("updateholidaycalendar")]
        public async Task<IActionResult> UpdateHolidayCalendar(
            [FromBody] HolidayCalendarDto dto)
        {
            var response =
                await _holidayCalendarService
                    .UpdateHolidayCalendar(dto);

            return Ok(response);
        }

        [HttpPost("deleteholidaycalendar/{id}")]
        public async Task<IActionResult> DeleteHolidayCalendar(
            int id)
        {
            var response =
                await _holidayCalendarService
                    .DeleteHolidayCalendar(id);

            return Ok(response);
        }

        [HttpGet("getallholidaycalendar")]
        public async Task<IActionResult> GetHolidayCalendars()
        {
            var response =
                await _holidayCalendarService
                    .GetHolidayCalendars();

            return Ok(response);
        }

        [HttpGet("getbyholidaycalendar/{id}")]
        public async Task<IActionResult> GetHolidayCalendarById(
            int id)
        {
            var response =
                await _holidayCalendarService
                    .GetHolidayCalendarById(id);

            return Ok(response);
        }
    }
}
