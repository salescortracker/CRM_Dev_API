using Business_Layer.Interfaces.EmailService;
using Business_Layer.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtp;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            IOptions<SmtpSettings> smtp,
            ILogger<EmailService> logger)
        {
            _smtp = smtp.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(EmailRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.To)) throw new ArgumentException("Recipient email is required.");

            if (!MailAddress.TryCreate(request.To, out _)) throw new ArgumentException("Invalid recipient email.");

            try
            {
                using var smtpClient = new SmtpClient
                {
                    Host = _smtp.Host,
                    Port = _smtp.Port,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        _smtp.User,
                        _smtp.Password)
                };

                using var message = new MailMessage
                {
                    From = new MailAddress(
                        _smtp.FromEmail,
                        _smtp.DisplayName),

                    Subject = request.Subject,

                    Body = request.HtmlBody,

                    IsBodyHtml = true
                };

                message.To.Add(request.To);

                if (request.Cc != null)
                {
                    foreach (var cc in request.Cc)
                    {
                        if (!string.IsNullOrWhiteSpace(cc))
                        {
                            if (MailAddress.TryCreate(cc, out _))
                            {
                                message.CC.Add(cc);
                            }
                        }
                    }
                }

                if (request.Attachments != null)
                {
                    foreach (var file in request.Attachments)
                    {
                        if (File.Exists(file))
                        {
                            message.Attachments.Add(
                                new Attachment(file));
                        }
                        else
                        {
                            _logger.LogWarning(
                                "Attachment not found : {File}",
                                file);
                        }
                    }
                }

                _logger.LogInformation(
                    "Sending email to {Email}",
                    request.To);

                cancellationToken.ThrowIfCancellationRequested();

                await smtpClient.SendMailAsync(message);

                _logger.LogInformation(
                    "Email sent successfully to {Email}",
                    request.To);
            }
            catch (SmtpException ex)
            {
                _logger.LogError(
                    ex,
                    "SMTP Error while sending email to {Email}",
                    request.To);

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unexpected Error while sending email");

                throw;
            }
        }
    }
}
