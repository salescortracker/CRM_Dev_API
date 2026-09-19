using Business_Layer.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginHistoryController : ControllerBase
    {
        private readonly ILoginHistoryService _service;

        public LoginHistoryController(ILoginHistoryService service)
        {
            _service = service;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult>
            GetLoginHistories()
        {
            return Ok(await _service.GetLoginHistories());
        }
    }
}
