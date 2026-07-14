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

        public MasterController(ICompanyAndRegionService service)
        {
            _service = service;
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
    }
}
