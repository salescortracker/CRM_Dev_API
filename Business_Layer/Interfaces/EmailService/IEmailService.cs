using Business_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(
            EmailRequest request,
            CancellationToken cancellationToken = default);
    }
}
