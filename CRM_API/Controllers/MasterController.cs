//using Business_Layer.DTOs.MasterDTO_s;
//using Business_Layer.Interfaces;
//using Business_Layer.Interfaces.MasterIInterface;
//using DataAccess_Layers.Entities;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace CRM_API.Controllers
//{
//    [Authorize]
//    [ApiController]
//    [Route("api/[controller]")]
//    public class DepartmentController : ControllerBase
//    {
//        private readonly
//            IDepartmentService _service;

//        public DepartmentController(
//            IDepartmentService service)
//        {
//            _service = service;
//        }

//        [HttpPost]
//        public async Task<IActionResult>
//            CreateDepartment(
//            DepartmentCreateDto dto)
//        {
//            return Ok(
//                await _service
//                    .CreateDepartment(dto));
//        }

//        [HttpPut]
//        public async Task<IActionResult>
//            UpdateDepartment(
//            DepartmentUpdateDto dto)
//        {
//            return Ok(
//                await _service
//                    .UpdateDepartment(dto));
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult>
//            DeleteDepartment(int id)
//        {
//            return Ok(
//                await _service
//                    .DeleteDepartment(id));
//        }

//        [HttpGet]
//        public async Task<IActionResult>
//            GetDepartments()
//        {
//            return Ok(
//                await _service
//                    .GetDepartments());
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult>
//            GetDepartmentById(int id)
//        {
//            return Ok(
//                await _service
//                    .GetDepartmentById(id));
//        }
//    }
//}
