using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.CommonDto
{
    public class BaseDto
    {
        public int CreatedBy { get; set; }
        public DateTime createdDate { get; set; }
    }
}
