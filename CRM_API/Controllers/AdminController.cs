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
        private readonly ICustomerSupportTicketService _customerSupportTicketService;
        private readonly IProjectManagementService _projectManagementService;   

        public AdminController(ICompanyProfileService companyProfileService, IBusinessHourService businessHourService, IHolidayCalendarService holidayCalendarService, ICustomerSupportTicketService customerSupportTicketService, IProjectManagementService projectManagementService)
        {
            _companyProfileService = companyProfileService;
            _businessHourService = businessHourService;
            _holidayCalendarService = holidayCalendarService;
            _customerSupportTicketService = customerSupportTicketService;
            _projectManagementService = projectManagementService;   

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

        #region CREATE

        [HttpPost("createusergroup")]
        public async Task<IActionResult> CreateUserGroup(
            [FromBody] UserGroupDto dto)
        {
            var response =
                await _companyProfileService.CreateUserGroup(dto);

            return Ok(response);
        }

        #endregion

        #region UPDATE

        [HttpPost("updateusergroup")]
        public async Task<IActionResult> UpdateUserGroup(
            [FromBody] UserGroupDto dto)
        {
            var response =
                await _companyProfileService.UpdateUserGroup(dto);

            return Ok(response);
        }

        #endregion

        #region DELETE

        [HttpPost("deleteusergroup/{id}")]
        public async Task<IActionResult> DeleteUserGroup(
            int id)
        {
            var response =
                await _companyProfileService.DeleteUserGroup(id);

            return Ok(response);
        }

        #endregion

        #region GET ALL

        [HttpGet("getallusergroup")]
        public async Task<IActionResult> GetUserGroups()
        {
            var response =
                await _companyProfileService.GetUserGroups();

            return Ok(response);
        }

        #endregion

        #region GET BY ID

        [HttpGet("getbyusergroup/{id}")]
        public async Task<IActionResult> GetUserGroupById(
            int id)
        {
            var response =
                await _companyProfileService.GetUserGroupById(id);

            return Ok(response);
        }

        #endregion

        [HttpPost("createlicensmanagement")]
        public async Task<IActionResult> CreateLicenseManagement(
           [FromBody] LicenseManagementDto dto)
        {
            var response =
                await _companyProfileService
                    .CreateLicenseManagement(dto);

            return Ok(response);
        }

        [HttpPost("updatelicenseManagement")]
        public async Task<IActionResult> UpdateLicenseManagement(
            [FromBody] LicenseManagementDto dto)
        {
            var response =
                await _companyProfileService
                    .UpdateLicenseManagement(dto);

            return Ok(response);
        }

        [HttpPost("deletelicenseManagement/{id}")]
        public async Task<IActionResult> DeleteLicenseManagement(
            int id)
        {
            var response =
                await _companyProfileService
                    .DeleteLicenseManagement(id);

            return Ok(response);
        }

        [HttpGet("getalllicenseManagement")]
        public async Task<IActionResult> GetLicenseManagements()
        {
            var response =
                await _companyProfileService
                    .GetLicenseManagements();

            return Ok(response);
        }

        [HttpGet("getbylicenseManagement/{id}")]
        public async Task<IActionResult> GetLicenseManagementById(
            int id)
        {
            var response =
                await _companyProfileService
                    .GetLicenseManagementById(id);

            return Ok(response);
        }


        #region CREATE

        [HttpPost("createpasswordpolicy")]
        public async Task<IActionResult> CreatePasswordPolicy(
            [FromBody] PasswordPolicyDto dto)
        {
            var response =
                await _companyProfileService.CreatePasswordPolicy(dto);

            return Ok(response);
        }

        #endregion

        #region UPDATE

        [HttpPost("updatepasswordpolicy")]
        public async Task<IActionResult> UpdatePasswordPolicy(
            [FromBody] PasswordPolicyDto dto)
        {
            var response =
                await _companyProfileService.UpdatePasswordPolicy(dto);

            return Ok(response);
        }

        #endregion

        #region DELETE

        [HttpPost("deletepasswordpolicy/{id}")]
        public async Task<IActionResult> DeletePasswordPolicy(
            int id)
        {
            var response =
                await _companyProfileService.DeletePasswordPolicy(id);

            return Ok(response);
        }

        #endregion

        #region GET ALL

        [HttpGet("getallpasswordpolicy")]
        public async Task<IActionResult> GetPasswordPolicies()
        {
            var response =
                await _companyProfileService.GetPasswordPolicies();

            return Ok(response);
        }

        #endregion

        #region GET BY ID

        [HttpGet("getbypasswordpolicy/{id}")]
        public async Task<IActionResult> GetPasswordPolicyById(
            int id)
        {
            var response =
                await _companyProfileService.GetPasswordPolicyById(id);

            return Ok(response);
        }

        #endregion

        [HttpPost("createcustomersupportticket")]
        public async Task<IActionResult> CreateCustomerSupportTicket(
            [FromBody] CustomerSupportTicketDto dto)
        {
            var response =
                await _customerSupportTicketService
                    .CreateCustomerSupportTicket(dto);

            return Ok(response);
        }

        [HttpPost("updatecustomersupportticket")]
        public async Task<IActionResult> UpdateCustomerSupportTicket(
            [FromBody] CustomerSupportTicketDto dto)
        {
            var response =
                await _customerSupportTicketService
                    .UpdateCustomerSupportTicket(dto);

            return Ok(response);
        }

        [HttpPost("deletecustomersupportticket/{id}")]
        public async Task<IActionResult> DeleteCustomerSupportTicket(
            int id)
        {
            var response =
                await _customerSupportTicketService
                    .DeleteCustomerSupportTicket(id);

            return Ok(response);
        }

        [HttpGet("getallcustomersupporttickets")]
        public async Task<IActionResult> GetCustomerSupportTickets()
        {
            var response =
                await _customerSupportTicketService
                    .GetCustomerSupportTickets();

            return Ok(response);
        }

        [HttpGet("getbycustomersupportticket/{id}")]
        public async Task<IActionResult> GetCustomerSupportTicketById(
            int id)
        {
            var response =
                await _customerSupportTicketService
                    .GetCustomerSupportTicketById(id);

            return Ok(response);
        }

        [HttpPost("createticketcategory")]
        public async Task<IActionResult> CreateTicketCategory(
            [FromBody] TicketCategoryDto dto)
        {
            var response =
                await _customerSupportTicketService
                    .CreateTicketCategory(dto);

            return Ok(response);
        }

        [HttpPost("updateticketcategory")]
        public async Task<IActionResult> UpdateTicketCategory(
            [FromBody] TicketCategoryDto dto)
        {
            var response =
                await _customerSupportTicketService
                    .UpdateTicketCategory(dto);

            return Ok(response);
        }

        [HttpPost("deleteticketcategory/{id}")]
        public async Task<IActionResult> DeleteTicketCategory(
            int id)
        {
            var response =
                await _customerSupportTicketService
                    .DeleteTicketCategory(id);

            return Ok(response);
        }

        [HttpGet("getallticketcategories")]
        public async Task<IActionResult> GetTicketCategories()
        {
            var response =
                await _customerSupportTicketService
                    .GetTicketCategories();

            return Ok(response);
        }

        [HttpGet("getbyticketcategory/{id}")]
        public async Task<IActionResult> GetTicketCategoryById(
            int id)
        {
            var response =
                await _customerSupportTicketService
                    .GetTicketCategoryById(id);

            return Ok(response);
        }

        [HttpPost("createknowledgebase")]
        public async Task<IActionResult> CreateKnowledgeBase(
           [FromBody] KnowledgeBaseDto dto)
        {
            var response =
                await _customerSupportTicketService
                    .CreateKnowledgeBase(dto);

            return Ok(response);
        }

        [HttpPost("updateknowledgebase")]
        public async Task<IActionResult> UpdateKnowledgeBase(
            [FromBody] KnowledgeBaseDto dto)
        {
            var response =
                await _customerSupportTicketService
                    .UpdateKnowledgeBase(dto);

            return Ok(response);
        }

        [HttpPost("deleteknowledgebase/{id}")]
        public async Task<IActionResult> DeleteKnowledgeBase(
            int id)
        {
            var response =
                await _customerSupportTicketService
                    .DeleteKnowledgeBase(id);

            return Ok(response);
        }

        [HttpGet("getallknowledgebases")]
        public async Task<IActionResult> GetKnowledgeBases()
        {
            var response =
                await _customerSupportTicketService
                    .GetKnowledgeBases();

            return Ok(response);
        }

        [HttpGet("getbyknowledgebase/{id}")]
        public async Task<IActionResult> GetKnowledgeBaseById(
            int id)
        {
            var response =
                await _customerSupportTicketService
                    .GetKnowledgeBaseById(id);

            return Ok(response);
        }

        // CREATE
        [HttpPost("createfaqmanagement")]
        public async Task<IActionResult> CreateFaqManagement(
            [FromBody] FaqManagementDto dto)
        {
            var response = await _customerSupportTicketService
                .CreateFaqManagement(dto);

            return Ok(response);
        }

        // UPDATE
        [HttpPost("updatefaqmanagement")]
        public async Task<IActionResult> UpdateFaqManagement(
            [FromBody] FaqManagementDto dto)
        {
            var response = await _customerSupportTicketService
                .UpdateFaqManagement(dto);

            return Ok(response);
        }

        // DELETE
        [HttpPost("deletefaqmanagement/{id}")]
        public async Task<IActionResult> DeleteFaqManagement(
            int id)
        {
            var response = await _customerSupportTicketService
                .DeleteFaqManagement(id);

            return Ok(response);
        }

        // GET ALL
        [HttpGet("getallfaqmanagements")]
        public async Task<IActionResult> GetFaqManagements()
        {
            var response = await _customerSupportTicketService
                .GetFaqManagements();

            return Ok(response);
        }

        // GET BY ID
        [HttpGet("getbyfaqmanagement/{id}")]
        public async Task<IActionResult> GetFaqManagementById(
            int id)
        {
            var response = await _customerSupportTicketService
                .GetFaqManagementById(id);

            return Ok(response);
        }

        [HttpPost("createproject")]
        public async Task<IActionResult> CreateProjectManagement(
            [FromBody] ProjectManagementDto dto)
        {
            var response =
                await _projectManagementService
                    .CreateProjectManagement(dto);

            return Ok(response);
        }

        [HttpPost("updateproject")]
        public async Task<IActionResult> UpdateProjectManagement(
            [FromBody] ProjectManagementDto dto)
        {
            var response =
                await _projectManagementService
                    .UpdateProjectManagement(dto);

            return Ok(response);
        }

        [HttpPost("deleteproject/{id}")]
        public async Task<IActionResult> DeleteProjectManagement(
            int id)
        {
            var response =
                await _projectManagementService
                    .DeleteProjectManagement(id);

            return Ok(response);
        }

        [HttpGet("getallprojects")]
        public async Task<IActionResult> GetProjectManagements()
        {
            var response =
                await _projectManagementService
                    .GetProjectManagements();

            return Ok(response);
        }

        [HttpGet("getbyproject/{id}")]
        public async Task<IActionResult> GetProjectManagementById(
            int id)
        {
            var response =
                await _projectManagementService
                    .GetProjectManagementById(id);

            return Ok(response);
        }

        [HttpPost("createprojectmilestone")]
        public async Task<IActionResult> CreateProjectMilestone(
           [FromBody] ProjectMilestoneDto dto)
        {
            var response =
                await _projectManagementService
                    .CreateProjectMilestone(dto);

            return Ok(response);
        }

        [HttpPost("updateprojectmilestone")]
        public async Task<IActionResult> UpdateProjectMilestone(
            [FromBody] ProjectMilestoneDto dto)
        {
            var response =
                await _projectManagementService
                    .UpdateProjectMilestone(dto);

            return Ok(response);
        }

        [HttpPost("deleteprojectmilestone/{id}")]
        public async Task<IActionResult> DeleteProjectMilestone(
            int id)
        {
            var response =
                await _projectManagementService
                    .DeleteProjectMilestone(id);

            return Ok(response);
        }

        [HttpGet("getallprojectmilestones")]
        public async Task<IActionResult> GetProjectMilestones()
        {
            var response =
                await _projectManagementService
                    .GetProjectMilestones();

            return Ok(response);
        }

        [HttpGet("getbyprojectmilestone/{id}")]
        public async Task<IActionResult> GetProjectMilestoneById(
            int id)
        {
            var response =
                await _projectManagementService
                    .GetProjectMilestoneById(id);

            return Ok(response);
        }

        [HttpPost("createprojecttask")]
        public async Task<IActionResult> CreateProjectTask(
            [FromBody] ProjectTaskDto dto)
        {
            var response =
                await _projectManagementService
                    .CreateProjectTask(dto);

            return Ok(response);
        }

        [HttpPost("updateprojecttask")]
        public async Task<IActionResult> UpdateProjectTask(
            [FromBody] ProjectTaskDto dto)
        {
            var response =
                await _projectManagementService
                    .UpdateProjectTask(dto);

            return Ok(response);
        }

        [HttpPost("deleteprojecttask/{id}")]
        public async Task<IActionResult> DeleteProjectTask(
            int id)
        {
            var response =
                await _projectManagementService
                    .DeleteProjectTask(id);

            return Ok(response);
        }

        [HttpGet("getallprojecttasks")]
        public async Task<IActionResult> GetProjectTasks()
        {
            var response =
                await _projectManagementService
                    .GetProjectTasks();

            return Ok(response);
        }

        [HttpGet("getbyprojecttask/{id}")]
        public async Task<IActionResult> GetProjectTaskById(
            int id)
        {
            var response =
                await _projectManagementService
                    .GetProjectTaskById(id);

            return Ok(response);
        }
    
    [HttpPost("createprojectdocument")]
        public async Task<IActionResult> CreateProjectDocument(
            [FromBody] ProjectDocumentDto dto)
        {
            var response =
                await _projectManagementService
                    .CreateProjectDocument(dto);

            return Ok(response);
        }

        [HttpPost("updateprojectdocument")]
        public async Task<IActionResult> UpdateProjectDocument(
            [FromBody] ProjectDocumentDto dto)
        {
            var response =
                await _projectManagementService
                    .UpdateProjectDocument(dto);

            return Ok(response);
        }

        [HttpPost("deleteprojectdocument/{id}")]
        public async Task<IActionResult> DeleteProjectDocument(
            int id)
        {
            var response =
                await _projectManagementService
                    .DeleteProjectDocument(id);

            return Ok(response);
        }

        [HttpGet("getallprojectdocuments")]
        public async Task<IActionResult> GetProjectDocuments()
        {
            var response =
                await _projectManagementService
                    .GetProjectDocuments();

            return Ok(response);
        }

        [HttpGet("getbyprojectdocument/{id}")]
        public async Task<IActionResult> GetProjectDocumentById(
            int id)
        {
            var response =
                await _projectManagementService
                    .GetProjectDocumentById(id);

            return Ok(response);
        }

        [HttpPost("createtimesheet")]
        public async Task<IActionResult> CreateTimesheet(
           [FromBody] TimesheetManagementDto dto)
        {
            var response =
                await _projectManagementService
                    .CreateTimesheet(dto);

            return Ok(response);
        }

        [HttpPost("updatetimesheet")]
        public async Task<IActionResult> UpdateTimesheet(
            [FromBody] TimesheetManagementDto dto)
        {
            var response =
                await _projectManagementService
                    .UpdateTimesheet(dto);

            return Ok(response);
        }

        [HttpPost("deletetimesheet/{id}")]
        public async Task<IActionResult> DeleteTimesheet(
            int id)
        {
            var response =
                await _projectManagementService
                    .DeleteTimesheet(id);

            return Ok(response);
        }

        [HttpGet("getalltimesheets")]
        public async Task<IActionResult> GetTimesheets()
        {
            var response =
                await _projectManagementService
                    .GetTimesheets();

            return Ok(response);
        }

        [HttpGet("getbytimesheet/{id}")]
        public async Task<IActionResult> GetTimesheetById(
            int id)
        {
            var response =
                await _projectManagementService
                    .GetTimesheetById(id);

            return Ok(response);
        }
    }
}





