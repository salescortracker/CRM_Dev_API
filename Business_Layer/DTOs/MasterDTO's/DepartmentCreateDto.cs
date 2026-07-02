using Business_Layer.DTOs.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.MasterDTO_s
{
    public class DepartmentCreateDto:BaseDto
    {
        public string DepartmentName { get; set; }

        public string DepartmentCode { get; set; }

        public string Description { get; set; }

        public int CompanyId { get; set; }

        public int RegionId { get; set; }

        //public int CreatedBy { get; set; }
    }
}
