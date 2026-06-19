using Business_Layer.Interfaces.CommonInterfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.CommonServices
{
    public class CurrentUserService
           : ICurrentUserService
    {
        private readonly IHttpContextAccessor
            _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var userId = _httpContextAccessor
                    .HttpContext?
                    .User?
                    .FindFirst("UserId")?
                    .Value;

                return string.IsNullOrEmpty(userId) ? 0 : Convert.ToInt32(userId);
            }
        }
    }
}
