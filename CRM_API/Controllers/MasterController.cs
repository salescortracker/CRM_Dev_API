using Business_Layer.DTOs.MasterDTO_s;
using Business_Layer.Interfaces.MasterIInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly ICompanyAndRegionService _service;
        private readonly ICountryService _countryService;
        private readonly Istateservices _stateservices;

        public MasterController(ICompanyAndRegionService service, ICountryService countryService, Istateservices stateservices)
        {
            _service = service;
            _countryService = countryService;
            _stateservices = stateservices;
        }

        [HttpPost("createcompany")]
        public async Task<IActionResult> CreateCompany(CompanyDto dto)
        {
            return Ok(await _service.CreateCompany(dto));
        }

        [HttpPost("updatecompany")]
        public async Task<IActionResult> UpdateCompany(CompanyDto dto)
        {
            return Ok(await _service.UpdateCompany(dto));
        }

        [HttpPost("deletecompany/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            return Ok(await _service.DeleteCompany(id));
        }

        [HttpGet("getallcompany")]
        public async Task<IActionResult> GetCompanies()
        {
            return Ok(await _service.GetCompanies());
        }

        [HttpGet("getbyidcompany/{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            return Ok(await _service.GetCompanyById(id));
        }
        #region REGION

        [HttpPost("createregion")]
        public async Task<IActionResult> CreateRegion(RegionDto dto)
        {
            return Ok(await _service.CreateRegion(dto));
        }

        [HttpPost("updateregion")]
        public async Task<IActionResult> UpdateRegion(RegionDto dto)
        {
            return Ok(await _service.UpdateRegion(dto));
        }

        [HttpPost("deleteregion/{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            return Ok(await _service.DeleteRegion(id));
        }

        [HttpGet("getallregion")]
        public async Task<IActionResult> GetRegions()
        {
            return Ok(await _service.GetRegions());
        }

        [HttpGet("getbyidregion/{id}")]
        public async Task<IActionResult> GetRegionById(int id)
        {
            return Ok(await _service.GetRegionById(id));
        }

        #endregion

        #region COUNTRY

        [HttpPost("createcountry")]
        public async Task<IActionResult> CreateCountry(CountryDto dto)
        {
            return Ok(await _countryService.CreateCountry(dto));
        }

        [HttpPost("updatecountry")]
        public async Task<IActionResult> UpdateCountry(CountryDto dto)
        {
            return Ok(await _countryService.UpdateCountry(dto));
        }

        [HttpPost("deletecountry/{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            return Ok(await _countryService.DeleteCountry(id));
        }

        [HttpGet("getallcountry")]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(await _countryService.GetCountries());
        }

        [HttpGet("getbyidcountry/{id}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            return Ok(await _countryService.GetCountryById(id));
        }

        #endregion
        #region STATE

        [HttpPost("createstate")]
        public async Task<IActionResult> CreateState(StateDto dto)
        {
            return Ok(await _stateservices.CreateState(dto));
        }

        [HttpPost("updatestate")]
        public async Task<IActionResult> UpdateState(StateDto dto)
        {
            return Ok(await _stateservices.UpdateState(dto));
        }

        [HttpPost("deletestate/{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            return Ok(await _stateservices.DeleteState(id));
        }

        [HttpGet("getallstate")]
        public async Task<IActionResult> GetStates()
        {
            return Ok(await _stateservices.GetStates());
        }

        [HttpGet("getbyidstate/{id}")]
        public async Task<IActionResult> GetStateById(int id)
        {
            return Ok(await _stateservices.GetStateById(id));
        }

        #endregion
    }
}


