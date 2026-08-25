using Business_Layer.DTOs.User;
using Business_Layer.Interfaces.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly ILeadService _leadService;
        private readonly ICompanyInformationService _companyInformationService;

        public UserManagementController(
            ILeadService leadService, ICompanyInformationService companyInformationService)
        {
            _leadService = leadService;
            _companyInformationService = companyInformationService;
        }

        #region LEAD

        [HttpPost("createlead")]
        public async Task<IActionResult> CreateLead(LeadDto dto)
        {
            return Ok(await _leadService.CreateLead(dto));
        }


        [HttpPost("updatelead")]
        public async Task<IActionResult> UpdateLead(LeadDto dto)
        {
            return Ok(await _leadService.UpdateLead(dto));
        }


        [HttpPost("deletelead/{id}")]
        public async Task<IActionResult> DeleteLead(int id)
        {
            return Ok(await _leadService.DeleteLead(id));
        }


        [HttpGet("getallleads")]
        public async Task<IActionResult> GetLeads()
        {
            return Ok(await _leadService.GetLeads());
        }


        [HttpGet("getbyleadid/{id}")]
        public async Task<IActionResult> GetLeadById(int id)
        {
            return Ok(await _leadService.GetLeadById(id));
        }
        #endregion

        #region COMPANY INFORMATION

        [HttpPost("createcompany")]
        public async Task<IActionResult> CreateCompany(
            CompanyInformationDto dto)
        {
            return Ok(
                await _companyInformationService.CreateCompany(dto));
        }


        [HttpPost("updatecompany")]
        public async Task<IActionResult> UpdateCompany(
            CompanyInformationDto dto)
        {
            return Ok(
                await _companyInformationService.UpdateCompany(dto));
        }


        [HttpPost("deletecompany/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            return Ok(
                await _companyInformationService.DeleteCompany(id));
        }


        [HttpGet("getallcompanies")]
        public async Task<IActionResult> GetCompanies()
        {
            return Ok(
                await _companyInformationService.GetCompanies());
        }


        [HttpGet("getcompanybyid/{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            return Ok(
                await _companyInformationService.GetCompanyById(id));
        }
        #endregion

        #region DropDowns
        // =====================================================
        // DROPDOWNS
        // =====================================================

        [HttpGet("getleadtypes")]
        public async Task<IActionResult> GetLeadTypes()
        {
            return Ok(await _leadService.GetLeadTypes());
        }
        [HttpGet("getcompanytypes")]
        public async Task<IActionResult> GetCompanyTypes()
        {
            return Ok(
                await _leadService.GetCompanyTypes());
        }


        [HttpGet("getleadsources")]
        public async Task<IActionResult> GetLeadSources()
        {
            return Ok(await _leadService.GetLeadSources());
        }


        [HttpGet("getindustries")]
        public async Task<IActionResult> GetIndustries()
        {
            return Ok(await _leadService.GetIndustries());
        }


        [HttpGet("getcountries")]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(await _leadService.GetCountries());
        }


        [HttpGet("getstates")]
        public async Task<IActionResult> GetStates(int? countryId)
        {
            return Ok(await _leadService.GetStates(countryId));
        }
        #endregion



    }
}
