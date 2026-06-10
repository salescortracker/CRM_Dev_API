using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.CommonModels
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
    }
//    return Ok(new ApiResponse<EmployeeDto>
//{
//    Success = true,
//    Message = "Employee fetched successfully",
//    Data = employee
//});
}
