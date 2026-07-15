using Business_Layer.Interfaces.EmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.EmailService
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public string ForgotPasswordOtpTemplate(
            string employeeName,
            string otp)
        {
            return $@"

<!DOCTYPE html>

<html>

<head>

<meta charset='UTF-8'>

<title>CRM Password Reset</title>

</head>

<body style='margin:0;
padding:0;
background:#f4f6f9;
font-family:Segoe UI'>

<table width='100%'>

<tr>

<td align='center'>

<table
style='width:600px;
background:white;
border-radius:10px;
overflow:hidden;
box-shadow:0 0 10px rgba(0,0,0,.15)'>

<tr>

<td
style='background:#0d6efd;
padding:25px;
text-align:center;
color:white;
font-size:28px;
font-weight:bold;'>

CRM Application

</td>

</tr>

<tr>

<td style='padding:40px;'>

<h2>Hello {employeeName},</h2>

<p>

We received a request to reset your password.

</p>

<p>

Please use the following OTP.

</p>

<div
style='margin:35px 0;
text-align:center;'>

<span
style='background:#0d6efd;
color:white;
padding:18px 40px;
font-size:34px;
letter-spacing:8px;
border-radius:8px;
font-weight:bold;'>

{otp}

</span>

</div>

<p>

This OTP is valid for

<b>5 Minutes</b>

</p>

<p>

Do not share this OTP with anyone.

</p>

<hr/>

<p
style='font-size:13px;
color:gray'>

If you didn't request this,

please ignore this email.

</p>

</td>

</tr>

<tr>

<td
style='background:#f4f6f9;
text-align:center;
padding:18px;
font-size:12px;
color:gray;'>

© 2026 CRM Application

</td>

</tr>

</table>

</td>

</tr>

</table>

</body>

</html>

";
        }
    }
}
