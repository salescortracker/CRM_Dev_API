using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layers.Entities
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
