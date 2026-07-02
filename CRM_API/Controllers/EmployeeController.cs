using Business_Layer.DTOs.MasterDTO_s;
using Business_Layer.Interfaces.MasterIInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Department")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;


        #region Department CRUD
        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDepartment(DepartmentCreateDto dto)
        {
            return Ok(await _service.CreateDepartment(dto));
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateDepartment(DepartmentUpdateDto dto)
        {
            return Ok(await _service.UpdateDepartment(dto));
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            return Ok(await _service.DeleteDepartment(id));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetDepartments()
        {
            return Ok(await _service.GetDepartments());
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            return Ok(await _service.GetDepartmentById(id));
        }

        #endregion
    }
}