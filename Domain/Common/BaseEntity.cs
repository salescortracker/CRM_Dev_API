using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
    public class Employee : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
