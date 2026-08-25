using Business_Layer.DTOs.MasterDTO_s;
using Business_Layer.Interfaces.MasterIInterface;
using Business_Layer.Services.MasterServices;
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
    private readonly ILicenseService _licenseService;
    private readonly IBackupFrequencyService _backupFrequencyService;
    private readonly IRetentionPeriodService _retentionPeriodService;
    private readonly IPaymentMethodService _paymentMethodService;
    private readonly IFiscalTypeService _fiscalTypeService;
    private readonly IDiscountTypeService _discountTypeService;
    private readonly IIndustryService _industryService;
        private readonly ILeadTypeService _leadTypeService;
        private readonly IEmailTypeService _emailTypeService;
        private readonly IEmailDataService _emailDataService;
        private readonly ICompanyTypeService _companyTypeService;
        private readonly IContactTypeService _contactTypeService;
        private readonly IRelationshipService _relationshipService;
        private readonly IActivityTypeService _activityTypeService;
        private readonly IEmailCategoryService _emailCategoryService;
        private readonly IMeetingPurposeService _meetingPurposeService;
        private readonly ICallTypeService _callTypeService;
        private readonly ICallPurposeService _callPurposeService;
        private readonly ICallOutcomeService _callOutcomeService;




        public MasterController(ICompanyAndRegionService service, ICountryService countryService, Istateservices stateservices, ICurrencyService currencyService, IPriorityService priorityService, ILeadStatusService leadStatusService, ILeadSourceService leadSourceService, IBillingCycleService billingCycleService, ILicenseService licenseService, IBackupFrequencyService backupFrequencyService, IRetentionPeriodService retentionPeriodService, IPaymentMethodService paymentMethodService, IFiscalTypeService fiscalTypeService, IDiscountTypeService discountTypeService, IIndustryService industryService
            ,IEmailDataService emailDataService, ILeadTypeService leadTypeService, IEmailTypeService emailTypeService, ICompanyTypeService companyTypeService, IContactTypeService contactTypeService, IRelationshipService relationshipService, IActivityTypeService activityTypeService, IEmailCategoryService emailCategoryService, IMeetingPurposeService meetingPurposeService, ICallTypeService callTypeService, ICallPurposeService callPurposeService, ICallOutcomeService callOutcomeService)
        {
            _service = service;
            _countryService = countryService;
            _stateservices = stateservices;
          _currencyService = currencyService;
      _priorityService = priorityService;
      _leadStatusService = leadStatusService;
      _leadSourceService = leadSourceService;
      _billingCycleService = billingCycleService;
      _licenseService = licenseService;
      _backupFrequencyService = backupFrequencyService;
      _retentionPeriodService = retentionPeriodService;
      _paymentMethodService = paymentMethodService;
      _fiscalTypeService = fiscalTypeService;
      _discountTypeService = discountTypeService;
      _industryService = industryService;
            _leadTypeService = leadTypeService;
            _emailTypeService = emailTypeService;
            _emailDataService = emailDataService;
            _companyTypeService = companyTypeService;
            _contactTypeService = contactTypeService;
            _relationshipService = relationshipService;
            _activityTypeService = activityTypeService;
            _emailCategoryService = emailCategoryService;
            _meetingPurposeService = meetingPurposeService;
            _callTypeService = callTypeService;
            _callPurposeService = callPurposeService;
            _callOutcomeService = callOutcomeService;

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

    #region LICENSE

    [HttpPost("createlicense")]
    public async Task<IActionResult> CreateLicense(LicenseDto dto)
    {
      return Ok(await _licenseService.CreateLicense(dto));
    }

    [HttpPost("updatelicense")]
    public async Task<IActionResult> UpdateLicense(LicenseDto dto)
    {
      return Ok(await _licenseService.UpdateLicense(dto));
    }

    [HttpPost("deletelicense/{id}")]
    public async Task<IActionResult> DeleteLicense(int id)
    {
      return Ok(await _licenseService.DeleteLicense(id));
    }

    [HttpGet("getalllicense")]
    public async Task<IActionResult> GetLicenses()
    {
      return Ok(await _licenseService.GetLicenses());
    }

    [HttpGet("getbylicense/{id}")]
    public async Task<IActionResult> GetLicenseById(int id)
    {
      return Ok(await _licenseService.GetLicenseById(id));
    }

    #endregion


    #region BACKUP FREQUENCY

    [HttpPost("createbackupfrequency")]
    public async Task<IActionResult> CreateBackupFrequency(BackupFrequencyDto dto)
    {
      return Ok(await _backupFrequencyService.CreateBackupFrequency(dto));
    }

    [HttpPost("updatebackupfrequency")]
    public async Task<IActionResult> UpdateBackupFrequency(BackupFrequencyDto dto)
    {
      return Ok(await _backupFrequencyService.UpdateBackupFrequency(dto));
    }

    [HttpPost("deletebackupfrequency/{id}")]
    public async Task<IActionResult> DeleteBackupFrequency(int id)
    {
      return Ok(await _backupFrequencyService.DeleteBackupFrequency(id));
    }

    [HttpGet("getallbackupfrequency")]
    public async Task<IActionResult> GetBackupFrequencies()
    {
      return Ok(await _backupFrequencyService.GetBackupFrequencies());
    }

    [HttpGet("getbybackupfrequency/{id}")]
    public async Task<IActionResult> GetBackupFrequencyById(int id)
    {
      return Ok(await _backupFrequencyService.GetBackupFrequencyById(id));
    }

    #endregion


    #region RETENTION PERIOD

    [HttpPost("createretentionperiod")]
    public async Task<IActionResult> CreateRetentionPeriod(RetentionPeriodDto dto)
    {
      return Ok(await _retentionPeriodService.CreateRetentionPeriod(dto));
    }

    [HttpPost("updateretentionperiod")]
    public async Task<IActionResult> UpdateRetentionPeriod(RetentionPeriodDto dto)
    {
      return Ok(await _retentionPeriodService.UpdateRetentionPeriod(dto));
    }

    [HttpPost("deleteretentionperiod/{id}")]
    public async Task<IActionResult> DeleteRetentionPeriod(int id)
    {
      return Ok(await _retentionPeriodService.DeleteRetentionPeriod(id));
    }

    [HttpGet("getallretentionperiod")]
    public async Task<IActionResult> GetRetentionPeriods()
    {
      return Ok(await _retentionPeriodService.GetRetentionPeriods());
    }

    [HttpGet("getbyretentionperiod/{id}")]
    public async Task<IActionResult> GetRetentionPeriodById(int id)
    {
      return Ok(await _retentionPeriodService.GetRetentionPeriodById(id));
    }

    #endregion


    #region PAYMENT METHOD

    [HttpPost("createpaymentmethod")]
    public async Task<IActionResult> CreatePaymentMethod(PaymentMethodDto dto)
    {
      return Ok(await _paymentMethodService.CreatePaymentMethod(dto));
    }

    [HttpPost("updatepaymentmethod")]
    public async Task<IActionResult> UpdatePaymentMethod(PaymentMethodDto dto)
    {
      return Ok(await _paymentMethodService.UpdatePaymentMethod(dto));
    }

    [HttpPost("deletepaymentmethod/{id}")]
    public async Task<IActionResult> DeletePaymentMethod(int id)
    {
      return Ok(await _paymentMethodService.DeletePaymentMethod(id));
    }

    [HttpGet("getallpaymentmethod")]
    public async Task<IActionResult> GetPaymentMethods()
    {
      return Ok(await _paymentMethodService.GetPaymentMethods());
    }

    [HttpGet("getbypaymentmethod/{id}")]
    public async Task<IActionResult> GetPaymentMethodById(int id)
    {
      return Ok(await _paymentMethodService.GetPaymentMethodById(id));
    }

    #endregion

    #region FISCAL TYPE

    [HttpPost("createfiscaltype")]
    public async Task<IActionResult> CreateFiscalType(FiscalTypeDto dto)
    {
      return Ok(await _fiscalTypeService.CreateFiscalType(dto));
    }

    [HttpPost("updatefiscaltype")]
    public async Task<IActionResult> UpdateFiscalType(FiscalTypeDto dto)
    {
      return Ok(await _fiscalTypeService.UpdateFiscalType(dto));
    }

    [HttpPost("deletefiscaltype/{id}")]
    public async Task<IActionResult> DeleteFiscalType(int id)
    {
      return Ok(await _fiscalTypeService.DeleteFiscalType(id));
    }

    [HttpGet("getallfiscaltype")]
    public async Task<IActionResult> GetFiscalTypes()
    {
      return Ok(await _fiscalTypeService.GetFiscalTypes());
    }

    [HttpGet("getbyfiscaltype/{id}")]
    public async Task<IActionResult> GetFiscalTypeById(int id)
    {
      return Ok(await _fiscalTypeService.GetFiscalTypeById(id));
    }

    #endregion

    #region DISCOUNT TYPE

    [HttpPost("creatediscounttype")]
    public async Task<IActionResult> CreateDiscountType(DiscountTypeDto dto)
    {
      return Ok(await _discountTypeService.CreateDiscountType(dto));
    }

    [HttpPost("updatediscounttype")]
    public async Task<IActionResult> UpdateDiscountType(DiscountTypeDto dto)
    {
      return Ok(await _discountTypeService.UpdateDiscountType(dto));
    }

    [HttpPost("deletediscounttype/{id}")]
    public async Task<IActionResult> DeleteDiscountType(int id)
    {
      return Ok(await _discountTypeService.DeleteDiscountType(id));
    }

    [HttpGet("getalldiscounttype")]
    public async Task<IActionResult> GetDiscountTypes()
    {
      return Ok(await _discountTypeService.GetDiscountTypes());
    }

    [HttpGet("getbydiscounttype/{id}")]
    public async Task<IActionResult> GetDiscountTypeById(int id)
    {
      return Ok(await _discountTypeService.GetDiscountTypeById(id));
    }

    #endregion

    #region INDUSTRY

    [HttpPost("createindustry")]
    public async Task<IActionResult> CreateIndustry(IndustryDto dto)
    {
      return Ok(await _industryService.CreateIndustry(dto));
    }

    [HttpPost("updateindustry")]
    public async Task<IActionResult> UpdateIndustry(IndustryDto dto)
    {
      return Ok(await _industryService.UpdateIndustry(dto));
    }

    [HttpPost("deleteindustry/{id}")]
    public async Task<IActionResult> DeleteIndustry(int id)
    {
      return Ok(await _industryService.DeleteIndustry(id));
    }

    [HttpGet("getallindustry")]
    public async Task<IActionResult> GetIndustries()
    {
      return Ok(await _industryService.GetIndustries());
    }

    [HttpGet("getbyindustry/{id}")]
    public async Task<IActionResult> GetIndustryById(int id)
    {
      return Ok(await _industryService.GetIndustryById(id));
    }

        #endregion

        #region LEAD TYPE

        [HttpPost("createleadtype")]
        public async Task<IActionResult> CreateLeadType(LeadTypeDto dto)
        {
            return Ok(await _leadTypeService.CreateLeadType(dto));
        }

        [HttpPost("updateleadtype")]
        public async Task<IActionResult> UpdateLeadType(LeadTypeDto dto)
        {
            return Ok(await _leadTypeService.UpdateLeadType(dto));
        }

        [HttpPost("deleteleadtype/{id}")]
        public async Task<IActionResult> DeleteLeadType(int id)
        {
            return Ok(await _leadTypeService.DeleteLeadType(id));
        }

        [HttpGet("getallleadtype")]
        public async Task<IActionResult> GetLeadTypes()
        {
            return Ok(await _leadTypeService.GetLeadTypes());
        }

        [HttpGet("getbyleadtype/{id}")]
        public async Task<IActionResult> GetLeadTypeById(int id)
        {
            return Ok(await _leadTypeService.GetLeadTypeById(id));
        }

        #endregion


 #region EMAIL TYPE

        [HttpPost("createemailtype")]
        public async Task<IActionResult> CreateEmailType(EmailTypeDto dto)
        {
            return Ok(await _emailTypeService.CreateEmailType(dto));
        }

        [HttpPost("updateemailtype")]
        public async Task<IActionResult> UpdateEmailType(EmailTypeDto dto)
        {
            return Ok(await _emailTypeService.UpdateEmailType(dto));
        }

        [HttpPost("deleteemailtype/{id}")]
        public async Task<IActionResult> DeleteEmailType(int id)
        {
            return Ok(await _emailTypeService.DeleteEmailType(id));
        }

        [HttpGet("getallemailtype")]
        public async Task<IActionResult> GetEmailTypes()
        {
            return Ok(await _emailTypeService.GetEmailTypes());
        }

        [HttpGet("getbyemailtype/{id}")]
        public async Task<IActionResult> GetEmailTypeById(int id)
        {
            return Ok(await _emailTypeService.GetEmailTypeById(id));
        }

        #endregion
        #region EMAIL TEMPLATE

        [HttpPost("createemailtemplate")]
        public async Task<IActionResult> CreateEmailsTemplate(EmailsTemplateDto dto)
        {
            return Ok(await _emailDataService.CreateEmailsTemplate(dto));
        }

        [HttpPost("updateemailtemplate")]
        public async Task<IActionResult> UpdateEmailsTemplate(EmailsTemplateDto dto)
        {
            return Ok(await _emailDataService.UpdateEmailsTemplate(dto));
        }

        [HttpPost("deleteemailtemplate/{id}")]
        public async Task<IActionResult> DeleteEmailsTemplate(int id)
        {
            return Ok(await _emailDataService.DeleteEmailsTemplate(id));
        }

        [HttpGet("getallemailtemplate")]
        public async Task<IActionResult> GetEmailsTemplates()
        {
            return Ok(await _emailDataService.GetEmailsTemplates());
        }

        [HttpGet("getbyemailtemplate/{id}")]
        public async Task<IActionResult> GetEmailsTemplateById(int id)
        {
            return Ok(await _emailDataService.GetEmailsTemplateById(id));
        }

        #endregion
        #region COMPANY TYPE

        [HttpPost("createcompanytype")]
        public async Task<IActionResult> CreateCompanyType(CompanyTypeDto dto)
        {
            return Ok(await _companyTypeService.CreateCompanyType(dto));
        }

        [HttpPost("updatecompanytype")]
        public async Task<IActionResult> UpdateCompanyType(CompanyTypeDto dto)
        {
            return Ok(await _companyTypeService.UpdateCompanyType(dto));
        }

        [HttpPost("deletecompanytype/{id}")]
        public async Task<IActionResult> DeleteCompanyType(int id)
        {
            return Ok(await _companyTypeService.DeleteCompanyType(id));
        }

        [HttpGet("getallcompanytype")]
        public async Task<IActionResult> GetCompanyTypes()
        {
            return Ok(await _companyTypeService.GetCompanyTypes());
        }

        [HttpGet("getbycompanytype/{id}")]
        public async Task<IActionResult> GetCompanyTypeById(int id)
        {
            return Ok(await _companyTypeService.GetCompanyTypeById(id));
        }

        #endregion

        #region CONTACT TYPE

        [HttpPost("createcontacttype")]
        public async Task<IActionResult> CreateContactType(ContactTypeDto dto)
        {
            return Ok(await _contactTypeService.CreateContactType(dto));
        }

        [HttpPost("updatecontacttype")]
        public async Task<IActionResult> UpdateContactType(ContactTypeDto dto)
        {
            return Ok(await _contactTypeService.UpdateContactType(dto));
        }

        [HttpPost("deletecontacttype/{id}")]
        public async Task<IActionResult> DeleteContactType(int id)
        {
            return Ok(await _contactTypeService.DeleteContactType(id));
        }

        [HttpGet("getallcontacttype")]
        public async Task<IActionResult> GetContactTypes()
        {
            return Ok(await _contactTypeService.GetContactTypes());
        }

        [HttpGet("getbycontacttype/{id}")]
        public async Task<IActionResult> GetContactTypeById(int id)
        {
            return Ok(await _contactTypeService.GetContactTypeById(id));
        }

        #endregion

        #region RELATIONSHIP

        [HttpPost("createrelationship")]
        public async Task<IActionResult> CreateRelationship(RelationshipDto dto)
        {
            return Ok(await _relationshipService.CreateRelationship(dto));
        }

        [HttpPost("updaterelationship")]
        public async Task<IActionResult> UpdateRelationship(RelationshipDto dto)
        {
            return Ok(await _relationshipService.UpdateRelationship(dto));
        }

        [HttpPost("deleterelationship/{id}")]
        public async Task<IActionResult> DeleteRelationship(int id)
        {
            return Ok(await _relationshipService.DeleteRelationship(id));
        }

        [HttpGet("getallrelationship")]
        public async Task<IActionResult> GetRelationships()
        {
            return Ok(await _relationshipService.GetRelationships());
        }

        [HttpGet("getbyrelationship/{id}")]
        public async Task<IActionResult> GetRelationshipById(int id)
        {
            return Ok(await _relationshipService.GetRelationshipById(id));
        }

        #endregion

        #region ACTIVITY TYPE

        [HttpPost("createactivitytype")]
        public async Task<IActionResult> CreateActivityType(ActivityTypeDto dto)
        {
            return Ok(await _activityTypeService.CreateActivityType(dto));
        }

        [HttpPost("updateactivitytype")]
        public async Task<IActionResult> UpdateActivityType(ActivityTypeDto dto)
        {
            return Ok(await _activityTypeService.UpdateActivityType(dto));
        }

        [HttpPost("deleteactivitytype/{id}")]
        public async Task<IActionResult> DeleteActivityType(int id)
        {
            return Ok(await _activityTypeService.DeleteActivityType(id));
        }

        [HttpGet("getallactivitytype")]
        public async Task<IActionResult> GetActivityTypes()
        {
            return Ok(await _activityTypeService.GetActivityTypes());
        }

        [HttpGet("getbyactivitytype/{id}")]
        public async Task<IActionResult> GetActivityTypeById(int id)
        {
            return Ok(await _activityTypeService.GetActivityTypeById(id));
        }

        #endregion

        #region EMAIL CATEGORY

        [HttpPost("createemailcategory")]
        public async Task<IActionResult> CreateEmailCategory(EmailCategoryDto dto)
        {
            return Ok(await _emailCategoryService.CreateEmailCategory(dto));
        }

        [HttpPost("updateemailcategory")]
        public async Task<IActionResult> UpdateEmailCategory(EmailCategoryDto dto)
        {
            return Ok(await _emailCategoryService.UpdateEmailCategory(dto));
        }

        [HttpPost("deleteemailcategory/{id}")]
        public async Task<IActionResult> DeleteEmailCategory(int id)
        {
            return Ok(await _emailCategoryService.DeleteEmailCategory(id));
        }

        [HttpGet("getallemailcategory")]
        public async Task<IActionResult> GetEmailCategories()
        {
            return Ok(await _emailCategoryService.GetEmailCategories());
        }

        [HttpGet("getbyemailcategory/{id}")]
        public async Task<IActionResult> GetEmailCategoryById(int id)
        {
            return Ok(await _emailCategoryService.GetEmailCategoryById(id));
        }

        #endregion
        #region MEETING PURPOSE

        [HttpPost("createmeetingpurpose")]
        public async Task<IActionResult> CreateMeetingPurpose(MeetingPurposeDto dto)
        {
            return Ok(await _meetingPurposeService.CreateMeetingPurpose(dto));
        }

        [HttpPost("updatemeetingpurpose")]
        public async Task<IActionResult> UpdateMeetingPurpose(MeetingPurposeDto dto)
        {
            return Ok(await _meetingPurposeService.UpdateMeetingPurpose(dto));
        }

        [HttpPost("deletemeetingpurpose/{id}")]
        public async Task<IActionResult> DeleteMeetingPurpose(int id)
        {
            return Ok(await _meetingPurposeService.DeleteMeetingPurpose(id));
        }

        [HttpGet("getallmeetingpurpose")]
        public async Task<IActionResult> GetMeetingPurposes()
        {
            return Ok(await _meetingPurposeService.GetMeetingPurposes());
        }

        [HttpGet("getbymeetingpurpose/{id}")]
        public async Task<IActionResult> GetMeetingPurposeById(int id)
        {
            return Ok(await _meetingPurposeService.GetMeetingPurposeById(id));
        }

        #endregion
        #region CALL TYPE

        [HttpPost("createcalltype")]
        public async Task<IActionResult> CreateCallType(CallTypeDto dto)
        {
            return Ok(await _callTypeService.CreateCallType(dto));
        }

        [HttpPost("updatecalltype")]
        public async Task<IActionResult> UpdateCallType(CallTypeDto dto)
        {
            return Ok(await _callTypeService.UpdateCallType(dto));
        }

        [HttpPost("deletecalltype/{id}")]
        public async Task<IActionResult> DeleteCallType(int id)
        {
            return Ok(await _callTypeService.DeleteCallType(id));
        }

        [HttpGet("getallcalltype")]
        public async Task<IActionResult> GetCallTypes()
        {
            return Ok(await _callTypeService.GetCallTypes());
        }

        [HttpGet("getbyidcalltype/{id}")]
        public async Task<IActionResult> GetCallTypeById(int id)
        {
            return Ok(await _callTypeService.GetCallTypeById(id));
        }

        #endregion
 #region CALL PURPOSE

        [HttpPost("createcallpurpose")]
        public async Task<IActionResult> CreateCallPurpose(CallPurposeDto dto)
        {
            return Ok(await _callPurposeService.CreateCallPurpose(dto));
        }

        [HttpPost("updatecallpurpose")]
        public async Task<IActionResult> UpdateCallPurpose(CallPurposeDto dto)
        {
            return Ok(await _callPurposeService.UpdateCallPurpose(dto));
        }

        [HttpPost("deletecallpurpose/{id}")]
        public async Task<IActionResult> DeleteCallPurpose(int id)
        {
            return Ok(await _callPurposeService.DeleteCallPurpose(id));
        }

        [HttpGet("getallcallpurpose")]
        public async Task<IActionResult> GetCallPurposes()
        {
            return Ok(await _callPurposeService.GetCallPurposes());
        }

        [HttpGet("getbyidcallpurpose/{id}")]
        public async Task<IActionResult> GetCallPurposeById(int id)
        {
            return Ok(await _callPurposeService.GetCallPurposeById(id));
        }

        #endregion
        #region CALL OUTCOME

        [HttpPost("createcalloutcome")]
        public async Task<IActionResult> CreateCallOutcome(CallOutcomeDto dto)
        {
            return Ok(await _callOutcomeService.CreateCallOutcome(dto));
        }

        [HttpPost("updatecalloutcome")]
        public async Task<IActionResult> UpdateCallOutcome(CallOutcomeDto dto)
        {
            return Ok(await _callOutcomeService.UpdateCallOutcome(dto));
        }

        [HttpPost("deletecalloutcome/{id}")]
        public async Task<IActionResult> DeleteCallOutcome(int id)
        {
            return Ok(await _callOutcomeService.DeleteCallOutcome(id));
        }

        [HttpGet("getallcalloutcome")]
        public async Task<IActionResult> GetCallOutcomes()
        {
            return Ok(await _callOutcomeService.GetCallOutcomes());
        }

        [HttpGet("getbyidcalloutcome/{id}")]
        public async Task<IActionResult> GetCallOutcomeById(int id)
        {
            return Ok(await _callOutcomeService.GetCallOutcomeById(id));
        }

        #endregion
    }
}




