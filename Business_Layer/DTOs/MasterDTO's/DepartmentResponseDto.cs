using Business_Layer.DTOs.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.MasterDTO_s
{
    public class DepartmentResponseDto:BaseDto
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentCode { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }
    }
}
