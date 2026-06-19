using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.CommonInterfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
    }
}
