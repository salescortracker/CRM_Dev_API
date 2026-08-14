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
         private readonly ICurrencyService _currencyService;
        private readonly IPriorityService _priorityService;
        private readonly ILeadStatusService _leadStatusService;
    private readonly ILeadSourceService _leadSourceService;
    private readonly IBillingCycleService _billingCycleService;

    public MasterController(ICompanyAndRegionService service, ICountryService countryService, Istateservices stateservices, ICurrencyService currencyService, IPriorityService priorityService, ILeadStatusService leadStatusService, ILeadSourceService leadSourceService, IBillingCycleService billingCycleService)
        {
            _service = service;
            _countryService = countryService;
            _stateservices = stateservices;
          _currencyService = currencyService;
      _priorityService = priorityService;
      _leadStatusService = leadStatusService;
      _leadSourceService = leadSourceService;
      _billingCycleService = billingCycleService;
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

    #region CURRENCY

    [HttpPost("createcurrency")]
    public async Task<IActionResult> CreateCurrency(CurrencyDto dto)
    {
      return Ok(await _currencyService.CreateCurrency(dto));
    }

    [HttpPost("updatecurrency")]
    public async Task<IActionResult> UpdateCurrency(CurrencyDto dto)
    {
      return Ok(await _currencyService.UpdateCurrency(dto));
    }

    [HttpPost("deletecurrency/{id}")]
    public async Task<IActionResult> DeleteCurrency(int id)
    {
      return Ok(await _currencyService.DeleteCurrency(id));
    }

    [HttpGet("getallcurrency")]
    public async Task<IActionResult> GetCurrencies()
    {
      return Ok(await _currencyService.GetCurrencies());
    }

    [HttpGet("getbyidcurrency/{id}")]
    public async Task<IActionResult> GetCurrencyById(int id)
    {
      return Ok(await _currencyService.GetCurrencyById(id));
    }

    #endregion

    #region PRIORITY

    [HttpPost("createpriority")]
    public async Task<IActionResult> CreatePriority(PriorityDto dto)
    {
      return Ok(await _priorityService.CreatePriority(dto));
    }

    [HttpPost("updatepriority")]
    public async Task<IActionResult> UpdatePriority(PriorityDto dto)
    {
      return Ok(await _priorityService.UpdatePriority(dto));
    }

    [HttpPost("deletepriority/{id}")]
    public async Task<IActionResult> DeletePriority(int id)
    {
      return Ok(await _priorityService.DeletePriority(id));
    }

    [HttpGet("getallpriority")]
    public async Task<IActionResult> GetPriorities()
    {
      return Ok(await _priorityService.GetPriorities());
    }

    [HttpGet("getbyidpriority/{id}")]
    public async Task<IActionResult> GetPriorityById(int id)
    {
      return Ok(await _priorityService.GetPriorityById(id));
    }

    #endregion


    #region LEAD STATUS

    [HttpPost("createleadstatus")]
    public async Task<IActionResult> CreateLeadStatus(LeadStatusDto dto)
    {
      return Ok(await _leadStatusService.CreateLeadStatus(dto));
    }

    [HttpPost("updateleadstatus")]
    public async Task<IActionResult> UpdateLeadStatus(LeadStatusDto dto)
    {
      return Ok(await _leadStatusService.UpdateLeadStatus(dto));
    }

    [HttpPost("deleteleadstatus/{id}")]
    public async Task<IActionResult> DeleteLeadStatus(int id)
    {
      return Ok(await _leadStatusService.DeleteLeadStatus(id));
    }

    [HttpGet("getallleadstatus")]
    public async Task<IActionResult> GetLeadStatuses()
    {
      return Ok(await _leadStatusService.GetLeadStatuses());
    }

    [HttpGet("getbyleadstatus/{id}")]
    public async Task<IActionResult> GetLeadStatusById(int id)
    {
      return Ok(await _leadStatusService.GetLeadStatusById(id));
    }

    #endregion


    #region LEAD SOURCE

    [HttpPost("createleadsource")]
    public async Task<IActionResult> CreateLeadSource(LeadSourceDto dto)
    {
      return Ok(await _leadSourceService.CreateLeadSource(dto));
    }

    [HttpPost("updateleadsource")]
    public async Task<IActionResult> UpdateLeadSource(LeadSourceDto dto)
    {
      return Ok(await _leadSourceService.UpdateLeadSource(dto));
    }

    [HttpPost("deleteleadsource/{id}")]
    public async Task<IActionResult> DeleteLeadSource(int id)
    {
      return Ok(await _leadSourceService.DeleteLeadSource(id));
    }

    [HttpGet("getallleadsource")]
    public async Task<IActionResult> GetLeadSources()
    {
      return Ok(await _leadSourceService.GetLeadSources());
    }

    [HttpGet("getbyleadsource/{id}")]
    public async Task<IActionResult> GetLeadSourceById(int id)
    {
      return Ok(await _leadSourceService.GetLeadSourceById(id));
    }

    #endregion


    #region BILLING CYCLE

    [HttpPost("createbillingcycle")]
    public async Task<IActionResult> CreateBillingCycle(BillingCycleDto dto)
    {
      return Ok(await _billingCycleService.CreateBillingCycle(dto));
    }

    [HttpPost("updatebillingcycle")]
    public async Task<IActionResult> UpdateBillingCycle(BillingCycleDto dto)
    {
      return Ok(await _billingCycleService.UpdateBillingCycle(dto));
    }

    [HttpPost("deletebillingcycle/{id}")]
    public async Task<IActionResult> DeleteBillingCycle(int id)
    {
      return Ok(await _billingCycleService.DeleteBillingCycle(id));
    }

    [HttpGet("getallbillingcycle")]
    public async Task<IActionResult> GetBillingCycles()
    {
      return Ok(await _billingCycleService.GetBillingCycles());
    }

    [HttpGet("getbybillingcycle/{id}")]
    public async Task<IActionResult> GetBillingCycleById(int id)
    {
      return Ok(await _billingCycleService.GetBillingCycleById(id));
    }

    #endregion

  }
}




