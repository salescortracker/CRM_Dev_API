using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.EmailService
{
    public interface IEmailTemplateService
    {
        string ForgotPasswordOtpTemplate(
            string employeeName,
            string otp);
    }
}
