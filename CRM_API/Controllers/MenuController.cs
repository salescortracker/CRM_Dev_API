using Business_Layer.DTOs.Menus;
using Business_Layer.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult>
            CreateMenu(MenuDto dto)
        {
            return Ok(await _service.CreateMenu(dto));
        }

        [HttpPost("update")]
        public async Task<IActionResult>
            UpdateMenu(MenuDto dto)
        {
            return Ok(await _service.UpdateMenu(dto));
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult>
            DeleteMenu(int id)
        {
            return Ok(await _service.DeleteMenu(id));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult>
            GetMenus()
        {
            return Ok(await _service.GetMenus());
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult>
            GetMenuById(int id)
        {
            return Ok(await _service.GetMenuById(id));
        }
    }
}
