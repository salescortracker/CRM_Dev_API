using Business_Layer.DTOs.SuperAdmin;
using Business_Layer.Interfaces.SuperAdminInterface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        private readonly IPlanService _planService;
        private readonly IOrganizationService _organizationService;
        private readonly IWebHostEnvironment _env;

        public SuperAdminController(IPlanService planService, IOrganizationService organizationService, IWebHostEnvironment env)
        {
            _planService = planService;
            _organizationService = organizationService;
            _env = env;
        }

        #region SUBSCRIPTION PLAN

        [HttpPost("createplan")]
        public async Task<IActionResult> CreatePlan(PlanDto dto)
        {
            return Ok(await _planService.CreatePlan(dto));
        }

        [HttpPost("updateplan")]
        public async Task<IActionResult> UpdatePlan(PlanDto dto)
        {
            return Ok(await _planService.UpdatePlan(dto));
        }

        [HttpPost("deleteplan/{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            return Ok(await _planService.DeletePlan(id));
        }

        [HttpGet("getallplan")]
        public async Task<IActionResult> GetPlans()
        {
            return Ok(await _planService.GetPlans());
        }

        [HttpGet("getbyidplan/{id}")]
        public async Task<IActionResult> GetPlanById(int id)
        {
            return Ok(await _planService.GetPlanById(id));
        }

        #endregion
        #region ORGANIZATIONS

        [HttpPost("createorganization")]
        public async Task<IActionResult> CreateOrganization([FromForm] OrganizationDto dto)
        {
            string root = _env.WebRootPath ??
                          Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            string path = Path.Combine(root, "Uploads", "OrganizationLogo");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (dto.LogoFile != null && dto.LogoFile.Length > 0)
            {
                string fileName = $"{Guid.NewGuid()}_{dto.LogoFile.FileName}";

                string fullPath = Path.Combine(path, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);

                await dto.LogoFile.CopyToAsync(stream);

                dto.LogoUrl = $"Uploads/OrganizationLogo/{fileName}";
            }

            return Ok(await _organizationService.CreateOrganization(dto));
        }
        [HttpPost("updateorganization")]
        public async Task<IActionResult> UpdateOrganization([FromForm] OrganizationDto dto)
        {
            string root = _env.WebRootPath ??
                          Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            string path = Path.Combine(root, "Uploads", "OrganizationLogo");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (dto.LogoFile != null && dto.LogoFile.Length > 0)
            {
                string fileName = $"{Guid.NewGuid()}_{dto.LogoFile.FileName}";

                string fullPath = Path.Combine(path, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);

                await dto.LogoFile.CopyToAsync(stream);

                dto.LogoUrl = $"Uploads/OrganizationLogo/{fileName}";
            }

            return Ok(await _organizationService.UpdateOrganization(dto));
        }
        [HttpPost("deleteorganization/{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            return Ok(await _organizationService.DeleteOrganization(id));
        }
        [HttpGet("getallorganization")]
        public async Task<IActionResult> GetOrganizations()
        {
            return Ok(await _organizationService.GetOrganizations());
        }
        [HttpGet("getorganizationbyid/{id}")]
        public async Task<IActionResult> GetOrganizationById(int id)
        {
            return Ok(await _organizationService.GetOrganizationById(id));
        }
        #endregion
    }
}
