using Business_Layer.DTOs.Teams;
using Business_Layer.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _service;

        public TeamController(ITeamService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult>
            CreateTeam(WorkTeamDto dto)
        {
            return Ok(await _service.CreateTeam(dto));
        }

        [HttpPost("update")]
        public async Task<IActionResult>
            UpdateTeam(WorkTeamDto dto)
        {
            return Ok(await _service.UpdateTeam(dto));
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult>
            DeleteTeam(int id)
        {
            return Ok(await _service.DeleteTeam(id));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult>
            GetTeams()
        {
            return Ok(await _service.GetTeams());
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult>
            GetTeamById(int id)
        {
            return Ok(await _service.GetTeamById(id));
        }
    }
}
