using Business_Layer.DTOs.SuperAdmin;
using Business_Layer.Interfaces.SuperAdminInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        private readonly IPlanService _planService;
        public SuperAdminController(IPlanService planService)
        {
            _planService = planService;
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
    }
}
