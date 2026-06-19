using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }

        public CustomException(
            string message,
            int statusCode = 400)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
