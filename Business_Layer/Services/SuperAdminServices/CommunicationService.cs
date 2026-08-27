using Business_Layer.DTOs.SuperAdmin;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.SuperAdminInterface;
using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Newtonsoft.Json;
using Serilog;
using Shared.CommonModels;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.SuperAdminServices
{
    public class CommunicationService : ICommunicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public CommunicationService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }
        #region Communication Email 
        public async Task<ApiResponse<string>> CreateCommunicationEmail(
            CommunicationEmailDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.ConfigurationName))
                    throw new CustomException("Configuration Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Smtphost))
                    throw new CustomException("SMTP Host is required.");

                if (dto.Smtpport <= 0)
                    throw new CustomException("SMTP Port is required.");

                if (string.IsNullOrWhiteSpace(dto.FromEmail))
                    throw new CustomException("From Email is required.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationEmail>()
                    .FindAsync(x =>
                        x.ConfigurationName.ToLower() ==
                        dto.ConfigurationName.ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "Email configuration already exists.");

                var email = new CommunicationEmail
                {
                    ConfigurationName = dto.ConfigurationName,
                    ProviderName = dto.ProviderName,
                    Smtphost = dto.Smtphost,
                    Smtpport = dto.Smtpport,
                    Smtpusername = dto.Smtpusername,
                    Smtppassword = dto.Smtppassword,
                    FromEmail = dto.FromEmail,
                    FromName = dto.FromName,
                    EncryptionType = dto.EncryptionType,
                    EnableAuthentication = dto.EnableAuthentication,
                    IsActive = dto.IsActive,
                    ConnectionStatus = "Not Tested",
                    CreatedBy = _currentUserService.UserId,
                    CreatedOn = DateTime.Now
                };

                await _unitOfWork.Repository<CommunicationEmail>()
                    .AddAsync(email);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationEmail",
                    "INSERT",
                    email.CommunicationEmailId,
                    "",
                    JsonConvert.SerializeObject(email),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email Configuration Created Successfully",
                    Data = email.ConfigurationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while creating email configuration");

                throw;
            }
        }

        public async Task<ApiResponse<string>> UpdateCommunicationEmail(
            CommunicationEmailDto dto)
        {
            try
            {
                var email = (await _unitOfWork
                    .Repository<CommunicationEmail>()
                    .FindAsync(x =>
                        x.CommunicationEmailId ==
                        dto.CommunicationEmailId))
                    .FirstOrDefault();

                if (email == null)
                    throw new CustomException(
                        "Email configuration not found.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationEmail>()
                    .FindAsync(x =>
                        x.CommunicationEmailId !=
                        dto.CommunicationEmailId &&
                        x.ConfigurationName.ToLower() ==
                        dto.ConfigurationName.ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "Email configuration already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(email);

                email.ConfigurationName = dto.ConfigurationName;
                email.ProviderName = dto.ProviderName;
                email.Smtphost = dto.Smtphost;
                email.Smtpport = dto.Smtpport;
                email.Smtpusername = dto.Smtpusername;

                if (!string.IsNullOrWhiteSpace(dto.Smtppassword))
                    email.Smtppassword = dto.Smtppassword;

                email.FromEmail = dto.FromEmail;
                email.FromName = dto.FromName;
                email.EncryptionType = dto.EncryptionType;
                email.EnableAuthentication =
                    dto.EnableAuthentication;
                email.IsActive = dto.IsActive;

                email.UpdatedBy = _currentUserService.UserId;
                email.UpdatedOn = DateTime.Now;

                _unitOfWork.Repository<CommunicationEmail>()
                    .Update(email);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationEmail",
                    "UPDATE",
                    email.CommunicationEmailId,
                    oldValues,
                    JsonConvert.SerializeObject(email),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email Configuration Updated Successfully",
                    Data = email.ConfigurationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while updating email configuration");

                throw;
            }
        }

        public async Task<ApiResponse<string>> DeleteCommunicationEmail(int id)
        {
            try
            {
                var email = (await _unitOfWork
                    .Repository<CommunicationEmail>()
                    .FindAsync(x =>
                        x.CommunicationEmailId == id))
                    .FirstOrDefault();

                if (email == null)
                    throw new CustomException(
                        "Email configuration not found.");

                string oldValues =
                    JsonConvert.SerializeObject(email);

                _unitOfWork.Repository<CommunicationEmail>()
                    .Remove(email);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationEmail",
                    "DELETE",
                    email.CommunicationEmailId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email Configuration Deleted Successfully",
                    Data = email.ConfigurationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while deleting email configuration");

                throw;
            }
        }

        public async Task<ApiResponse<List<CommunicationEmailDto>>>
            GetCommunicationEmails()
        {
            var emails = await _unitOfWork
                .Repository<CommunicationEmail>()
                .GetAllAsync();

            var result = emails
                .OrderByDescending(x =>
                    x.CommunicationEmailId)
                .Select(x => new CommunicationEmailDto
                {
                    CommunicationEmailId =
                        x.CommunicationEmailId,

                    ConfigurationName =
                        x.ConfigurationName,

                    ProviderName =
                        x.ProviderName,

                    Smtphost =
                        x.Smtphost,

                    Smtpport =
                        x.Smtpport,

                    Smtpusername =
                        x.Smtpusername,

                    FromEmail =
                        x.FromEmail,

                    FromName =
                        x.FromName,

                    EncryptionType =
                        x.EncryptionType,

                    EnableAuthentication =
                        x.EnableAuthentication,

                    IsActive =
                        x.IsActive,

                    ConnectionStatus =
                        x.ConnectionStatus,

                    LastTestedOn =
                        x.LastTestedOn
                })
                .ToList();

            return new ApiResponse<List<CommunicationEmailDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        public async Task<ApiResponse<CommunicationEmailDto>>
            GetCommunicationEmailById(int id)
        {
            var email = (await _unitOfWork
                .Repository<CommunicationEmail>()
                .FindAsync(x =>
                    x.CommunicationEmailId == id))
                .FirstOrDefault();

            if (email == null)
                throw new CustomException(
                    "Email configuration not found.");

            return new ApiResponse<CommunicationEmailDto>
            {
                Success = true,
                Message = "Success",

                Data = new CommunicationEmailDto
                {
                    CommunicationEmailId =
                        email.CommunicationEmailId,

                    ConfigurationName =
                        email.ConfigurationName,

                    ProviderName =
                        email.ProviderName,

                    Smtphost =
                        email.Smtphost,

                    Smtpport =
                        email.Smtpport,

                    Smtpusername =
                        email.Smtpusername,

                    FromEmail =
                        email.FromEmail,

                    FromName =
                        email.FromName,

                    EncryptionType =
                        email.EncryptionType,

                    EnableAuthentication =
                        email.EnableAuthentication,

                    IsActive =
                        email.IsActive,

                    ConnectionStatus =
                        email.ConnectionStatus,

                    LastTestedOn =
                        email.LastTestedOn
                }
            };
        }
        #endregion
        #region Communication SMS
        public async Task<ApiResponse<string>> CreateCommunicationSMS(
          CommunicationSMSDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.ConfigurationName))
                    throw new CustomException("Configuration Name is required.");

                if (string.IsNullOrWhiteSpace(dto.AccountSid))
                    throw new CustomException("Account SID is required.");

                if (string.IsNullOrWhiteSpace(dto.FromNumber))
                    throw new CustomException("From Number is required.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationSm>()
                    .FindAsync(x =>
                        x.ConfigurationName.ToLower() ==
                        dto.ConfigurationName.ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "SMS configuration already exists.");

                var sms = new CommunicationSm
                {
                    ConfigurationName = dto.ConfigurationName,
                    ProviderName = dto.ProviderName,
                    AccountSid = dto.AccountSid,
                    AuthToken = dto.AuthToken,
                    FromNumber = dto.FromNumber,
                    MessagingServiceSid = dto.MessagingServiceSid,
                    WebhookUrl = dto.WebhookUrl,
                    IsActive = dto.IsActive,
                    ConnectionStatus = "Not Tested",
                    CreatedBy = _currentUserService.UserId,
                    CreatedOn = DateTime.Now
                };

                await _unitOfWork.Repository<CommunicationSm>()
                    .AddAsync(sms);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationSMS",
                    "INSERT",
                    sms.CommunicationSmsid,
                    "",
                    JsonConvert.SerializeObject(sms),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "SMS Configuration Created Successfully",
                    Data = sms.ConfigurationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating SMS configuration");
                throw;
            }
        }

        public async Task<ApiResponse<string>> UpdateCommunicationSMS(
            CommunicationSMSDto dto)
        {
            try
            {
                var sms = (await _unitOfWork
                    .Repository<CommunicationSm>()
                    .FindAsync(x =>
                        x.CommunicationSmsid ==
                        dto.CommunicationSmsid))
                    .FirstOrDefault();

                if (sms == null)
                    throw new CustomException(
                        "SMS configuration not found.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationSm>()
                    .FindAsync(x =>
                        x.CommunicationSmsid !=
                        dto.CommunicationSmsid &&
                        x.ConfigurationName.ToLower() ==
                        dto.ConfigurationName.ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "SMS configuration already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(sms);

                sms.ConfigurationName = dto.ConfigurationName;
                sms.ProviderName = dto.ProviderName;
                sms.AccountSid = dto.AccountSid;

                if (!string.IsNullOrWhiteSpace(dto.AuthToken))
                    sms.AuthToken = dto.AuthToken;

                sms.FromNumber = dto.FromNumber;
                sms.MessagingServiceSid =
                    dto.MessagingServiceSid;

                sms.WebhookUrl = dto.WebhookUrl;
                sms.IsActive = dto.IsActive;

                sms.UpdatedBy = _currentUserService.UserId;
                sms.UpdatedOn = DateTime.Now;

                _unitOfWork.Repository<CommunicationSm>()
                    .Update(sms);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationSMS",
                    "UPDATE",
                    sms.CommunicationSmsid,
                    oldValues,
                    JsonConvert.SerializeObject(sms),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "SMS Configuration Updated Successfully",
                    Data = sms.ConfigurationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating SMS configuration");
                throw;
            }
        }

        public async Task<ApiResponse<string>> DeleteCommunicationSMS(int id)
        {
            try
            {
                var sms = (await _unitOfWork
                    .Repository<CommunicationSm>()
                    .FindAsync(x =>
                        x.CommunicationSmsid == id))
                    .FirstOrDefault();

                if (sms == null)
                    throw new CustomException(
                        "SMS configuration not found.");

                string oldValues =
                    JsonConvert.SerializeObject(sms);

                _unitOfWork.Repository<CommunicationSm>()
                    .Remove(sms);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationSMS",
                    "DELETE",
                    sms.CommunicationSmsid,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "SMS Configuration Deleted Successfully",
                    Data = sms.ConfigurationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting SMS configuration");
                throw;
            }
        }

        public async Task<ApiResponse<List<CommunicationSMSDto>>>
            GetCommunicationSMS()
        {
            var smsList = await _unitOfWork
                .Repository<CommunicationSm>()
                .GetAllAsync();

            var result = smsList
                .OrderByDescending(x => x.CommunicationSmsid)
                .Select(x => new CommunicationSMSDto
                {
                    CommunicationSmsid = x.CommunicationSmsid,
                    ConfigurationName = x.ConfigurationName,
                    ProviderName = x.ProviderName,
                    AccountSid = x.AccountSid,
                    FromNumber = x.FromNumber,
                    MessagingServiceSid =
                        x.MessagingServiceSid,
                    WebhookUrl = x.WebhookUrl,
                    IsActive = x.IsActive,
                    ConnectionStatus =
                        x.ConnectionStatus,
                    LastTestedOn =
                        x.LastTestedOn
                })
                .ToList();

            return new ApiResponse<List<CommunicationSMSDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        public async Task<ApiResponse<CommunicationSMSDto>>
            GetCommunicationSMSById(int id)
        {
            var sms = (await _unitOfWork
                .Repository<CommunicationSm>()
                .FindAsync(x =>
                    x.CommunicationSmsid == id))
                .FirstOrDefault();

            if (sms == null)
                throw new CustomException(
                    "SMS configuration not found.");

            return new ApiResponse<CommunicationSMSDto>
            {
                Success = true,
                Message = "Success",

                Data = new CommunicationSMSDto
                {
                    CommunicationSmsid =
                        sms.CommunicationSmsid,

                    ConfigurationName =
                        sms.ConfigurationName,

                    ProviderName =
                        sms.ProviderName,

                    AccountSid =
                        sms.AccountSid,

                    FromNumber =
                        sms.FromNumber,

                    MessagingServiceSid =
                        sms.MessagingServiceSid,

                    WebhookUrl =
                        sms.WebhookUrl,

                    IsActive =
                        sms.IsActive,

                    ConnectionStatus =
                        sms.ConnectionStatus,

                    LastTestedOn =
                        sms.LastTestedOn
                }
            };
        }
        #endregion
        #region Communication WhatsApp
        public async Task<ApiResponse<string>> CreateCommunicationWhatsApp(
           CommunicationWhatsAppDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.ConfigurationName))
                    throw new CustomException("Configuration Name is required.");

                if (string.IsNullOrWhiteSpace(dto.AccountSid))
                    throw new CustomException("Account SID is required.");

                if (string.IsNullOrWhiteSpace(dto.WhatsAppNumber))
                    throw new CustomException("WhatsApp Number is required.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationWhatsApp>()
                    .FindAsync(x =>
                        x.ConfigurationName.ToLower() ==
                        dto.ConfigurationName.ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "WhatsApp configuration already exists.");

                var whatsapp = new CommunicationWhatsApp
                {
                    ConfigurationName = dto.ConfigurationName,
                    ProviderName = dto.ProviderName,
                    AccountSid = dto.AccountSid,
                    AuthToken = dto.AuthToken,
                    WhatsAppNumber = dto.WhatsAppNumber,
                    MessagingServiceSid =
                        dto.MessagingServiceSid,
                    WebhookUrl = dto.WebhookUrl,
                    BusinessAccountId =
                        dto.BusinessAccountId,
                    IsActive = dto.IsActive,
                    ConnectionStatus = "Not Tested",
                    CreatedBy = _currentUserService.UserId,
                    CreatedOn = DateTime.Now
                };

                await _unitOfWork.Repository<CommunicationWhatsApp>()
                    .AddAsync(whatsapp);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationWhatsApp",
                    "INSERT",
                    whatsapp.CommunicationWhatsAppId,
                    "",
                    JsonConvert.SerializeObject(whatsapp),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "WhatsApp Configuration Created Successfully",
                    Data = whatsapp.ConfigurationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while creating WhatsApp configuration");

                throw;
            }
        }

        public async Task<ApiResponse<string>> UpdateCommunicationWhatsApp(
            CommunicationWhatsAppDto dto)
        {
            try
            {
                var whatsapp = (await _unitOfWork
                    .Repository<CommunicationWhatsApp>()
                    .FindAsync(x =>
                        x.CommunicationWhatsAppId ==
                        dto.CommunicationWhatsAppId))
                    .FirstOrDefault();

                if (whatsapp == null)
                    throw new CustomException(
                        "WhatsApp configuration not found.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationWhatsApp>()
                    .FindAsync(x =>
                        x.CommunicationWhatsAppId !=
                        dto.CommunicationWhatsAppId &&
                        x.ConfigurationName.ToLower() ==
                        dto.ConfigurationName.ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "WhatsApp configuration already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(whatsapp);

                whatsapp.ConfigurationName =
                    dto.ConfigurationName;

                whatsapp.ProviderName =
                    dto.ProviderName;

                whatsapp.AccountSid =
                    dto.AccountSid;

                if (!string.IsNullOrWhiteSpace(dto.AuthToken))
                    whatsapp.AuthToken = dto.AuthToken;

                whatsapp.WhatsAppNumber =
                    dto.WhatsAppNumber;

                whatsapp.MessagingServiceSid =
                    dto.MessagingServiceSid;

                whatsapp.WebhookUrl =
                    dto.WebhookUrl;

                whatsapp.BusinessAccountId =
                    dto.BusinessAccountId;

                whatsapp.IsActive =
                    dto.IsActive;

                whatsapp.UpdatedBy =
                    _currentUserService.UserId;

                whatsapp.UpdatedOn =
                    DateTime.Now;

                _unitOfWork.Repository<CommunicationWhatsApp>()
                    .Update(whatsapp);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationWhatsApp",
                    "UPDATE",
                    whatsapp.CommunicationWhatsAppId,
                    oldValues,
                    JsonConvert.SerializeObject(whatsapp),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "WhatsApp Configuration Updated Successfully",
                    Data = whatsapp.ConfigurationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while updating WhatsApp configuration");

                throw;
            }
        }

        public async Task<ApiResponse<string>> DeleteCommunicationWhatsApp(
            int id)
        {
            try
            {
                var whatsapp = (await _unitOfWork
                    .Repository<CommunicationWhatsApp>()
                    .FindAsync(x =>
                        x.CommunicationWhatsAppId == id))
                    .FirstOrDefault();

                if (whatsapp == null)
                    throw new CustomException(
                        "WhatsApp configuration not found.");

                string oldValues =
                    JsonConvert.SerializeObject(whatsapp);

                _unitOfWork.Repository<CommunicationWhatsApp>()
                    .Remove(whatsapp);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationWhatsApp",
                    "DELETE",
                    whatsapp.CommunicationWhatsAppId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "WhatsApp Configuration Deleted Successfully",
                    Data = whatsapp.ConfigurationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while deleting WhatsApp configuration");

                throw;
            }
        }

        public async Task<ApiResponse<List<CommunicationWhatsAppDto>>>
            GetCommunicationWhatsApps()
        {
            var list = await _unitOfWork
                .Repository<CommunicationWhatsApp>()
                .GetAllAsync();

            var result = list
                .OrderByDescending(x =>
                    x.CommunicationWhatsAppId)
                .Select(x => new CommunicationWhatsAppDto
                {
                    CommunicationWhatsAppId =
                        x.CommunicationWhatsAppId,

                    ConfigurationName =
                        x.ConfigurationName,

                    ProviderName =
                        x.ProviderName,

                    AccountSid =
                        x.AccountSid,

                    WhatsAppNumber =
                        x.WhatsAppNumber,

                    MessagingServiceSid =
                        x.MessagingServiceSid,

                    WebhookUrl =
                        x.WebhookUrl,

                    BusinessAccountId =
                        x.BusinessAccountId,

                    IsActive =
                        x.IsActive,

                    ConnectionStatus =
                        x.ConnectionStatus,

                    LastTestedOn =
                        x.LastTestedOn
                })
                .ToList();

            return new ApiResponse<List<CommunicationWhatsAppDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        public async Task<ApiResponse<CommunicationWhatsAppDto>>
            GetCommunicationWhatsAppById(int id)
        {
            var whatsapp = (await _unitOfWork
                .Repository<CommunicationWhatsApp>()
                .FindAsync(x =>
                    x.CommunicationWhatsAppId == id))
                .FirstOrDefault();

            if (whatsapp == null)
                throw new CustomException(
                    "WhatsApp configuration not found.");

            return new ApiResponse<CommunicationWhatsAppDto>
            {
                Success = true,
                Message = "Success",

                Data = new CommunicationWhatsAppDto
                {
                    CommunicationWhatsAppId =
                        whatsapp.CommunicationWhatsAppId,

                    ConfigurationName =
                        whatsapp.ConfigurationName,

                    ProviderName =
                        whatsapp.ProviderName,

                    AccountSid =
                        whatsapp.AccountSid,

                    WhatsAppNumber =
                        whatsapp.WhatsAppNumber,

                    MessagingServiceSid =
                        whatsapp.MessagingServiceSid,

                    WebhookUrl =
                        whatsapp.WebhookUrl,

                    BusinessAccountId =
                        whatsapp.BusinessAccountId,

                    IsActive =
                        whatsapp.IsActive,

                    ConnectionStatus =
                        whatsapp.ConnectionStatus,

                    LastTestedOn =
                        whatsapp.LastTestedOn
                }
            };
        }
        #endregion
        #region Communication Voice
        public async Task<ApiResponse<string>> CreateCommunicationVoice(
           CommunicationVoiceDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.ConfigurationName))
                    throw new CustomException("Configuration Name is required.");

                if (string.IsNullOrWhiteSpace(dto.AccountSid))
                    throw new CustomException("Account SID is required.");

                if (string.IsNullOrWhiteSpace(dto.FromNumber))
                    throw new CustomException("From Number is required.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationVoice>()
                    .FindAsync(x =>
                        x.ConfigurationName.ToLower() ==
                        dto.ConfigurationName.ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "Voice configuration already exists.");

                var voice = new CommunicationVoice
                {
                    ConfigurationName = dto.ConfigurationName,
                    ProviderName = dto.ProviderName,
                    AccountSid = dto.AccountSid,
                    AuthToken = dto.AuthToken,
                    FromNumber = dto.FromNumber,
                    VoiceApplicationSid =
                        dto.VoiceApplicationSid,
                    TwiMlappSid =
                        dto.TwiMlappSid,
                    TwiMlurl =
                        dto.TwiMlurl,
                    WebhookUrl =
                        dto.WebhookUrl,
                    IsActive =
                        dto.IsActive,
                    ConnectionStatus = "Not Tested",
                    CreatedBy =
                        _currentUserService.UserId,
                    CreatedOn =
                        DateTime.Now
                };

                await _unitOfWork.Repository<CommunicationVoice>()
                    .AddAsync(voice);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationVoice",
                    "INSERT",
                    voice.CommunicationVoiceId,
                    "",
                    JsonConvert.SerializeObject(voice),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Voice Configuration Created Successfully",
                    Data = voice.ConfigurationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while creating voice configuration");

                throw;
            }
        }

        public async Task<ApiResponse<string>> UpdateCommunicationVoice(
            CommunicationVoiceDto dto)
        {
            try
            {
                var voice = (await _unitOfWork
                    .Repository<CommunicationVoice>()
                    .FindAsync(x =>
                        x.CommunicationVoiceId ==
                        dto.CommunicationVoiceId))
                    .FirstOrDefault();

                if (voice == null)
                    throw new CustomException(
                        "Voice configuration not found.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationVoice>()
                    .FindAsync(x =>
                        x.CommunicationVoiceId !=
                        dto.CommunicationVoiceId &&
                        x.ConfigurationName.ToLower() ==
                        dto.ConfigurationName.ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "Voice configuration already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(voice);

                voice.ConfigurationName =
                    dto.ConfigurationName;

                voice.ProviderName =
                    dto.ProviderName;

                voice.AccountSid =
                    dto.AccountSid;

                if (!string.IsNullOrWhiteSpace(dto.AuthToken))
                    voice.AuthToken =
                        dto.AuthToken;

                voice.FromNumber =
                    dto.FromNumber;

                voice.VoiceApplicationSid =
                    dto.VoiceApplicationSid;

                voice.TwiMlappSid =
                    dto.TwiMlappSid;

                voice.TwiMlurl =
                    dto.TwiMlurl;

                voice.WebhookUrl =
                    dto.WebhookUrl;

                voice.IsActive =
                    dto.IsActive;

                voice.UpdatedBy =
                    _currentUserService.UserId;

                voice.UpdatedOn =
                    DateTime.Now;

                _unitOfWork.Repository<CommunicationVoice>()
                    .Update(voice);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationVoice",
                    "UPDATE",
                    voice.CommunicationVoiceId,
                    oldValues,
                    JsonConvert.SerializeObject(voice),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Voice Configuration Updated Successfully",
                    Data = voice.ConfigurationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while updating voice configuration");

                throw;
            }
        }

        public async Task<ApiResponse<string>> DeleteCommunicationVoice(
            int id)
        {
            try
            {
                var voice = (await _unitOfWork
                    .Repository<CommunicationVoice>()
                    .FindAsync(x =>
                        x.CommunicationVoiceId == id))
                    .FirstOrDefault();

                if (voice == null)
                    throw new CustomException(
                        "Voice configuration not found.");

                string oldValues =
                    JsonConvert.SerializeObject(voice);

                _unitOfWork.Repository<CommunicationVoice>()
                    .Remove(voice);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationVoice",
                    "DELETE",
                    voice.CommunicationVoiceId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Voice Configuration Deleted Successfully",
                    Data = voice.ConfigurationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while deleting voice configuration");

                throw;
            }
        }

        public async Task<ApiResponse<List<CommunicationVoiceDto>>>
            GetCommunicationVoices()
        {
            var list = await _unitOfWork
                .Repository<CommunicationVoice>()
                .GetAllAsync();

            var result = list
                .OrderByDescending(x =>
                    x.CommunicationVoiceId)
                .Select(x => new CommunicationVoiceDto
                {
                    CommunicationVoiceId =
                        x.CommunicationVoiceId,

                    ConfigurationName =
                        x.ConfigurationName,

                    ProviderName =
                        x.ProviderName,

                    AccountSid =
                        x.AccountSid,

                    FromNumber =
                        x.FromNumber,

                    VoiceApplicationSid =
                        x.VoiceApplicationSid,

                    TwiMlappSid =
                        x.TwiMlappSid,

                    TwiMlurl =
                        x.TwiMlurl,

                    WebhookUrl =
                        x.WebhookUrl,

                    IsActive =
                        x.IsActive,

                    ConnectionStatus =
                        x.ConnectionStatus,

                    LastTestedOn =
                        x.LastTestedOn
                })
                .ToList();

            return new ApiResponse<List<CommunicationVoiceDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        public async Task<ApiResponse<CommunicationVoiceDto>>
            GetCommunicationVoiceById(int id)
        {
            var voice = (await _unitOfWork
                .Repository<CommunicationVoice>()
                .FindAsync(x =>
                    x.CommunicationVoiceId == id))
                .FirstOrDefault();

            if (voice == null)
                throw new CustomException(
                    "Voice configuration not found.");

            return new ApiResponse<CommunicationVoiceDto>
            {
                Success = true,
                Message = "Success",

                Data = new CommunicationVoiceDto
                {
                    CommunicationVoiceId =
                        voice.CommunicationVoiceId,

                    ConfigurationName =
                        voice.ConfigurationName,

                    ProviderName =
                        voice.ProviderName,

                    AccountSid =
                        voice.AccountSid,

                    FromNumber =
                        voice.FromNumber,

                    VoiceApplicationSid =
                        voice.VoiceApplicationSid,

                    TwiMlappSid =
                        voice.TwiMlappSid,

                    TwiMlurl =
                        voice.TwiMlurl,

                    WebhookUrl =
                        voice.WebhookUrl,

                    IsActive =
                        voice.IsActive,

                    ConnectionStatus =
                        voice.ConnectionStatus,

                    LastTestedOn =
                        voice.LastTestedOn
                }
            };
        }
        #endregion
        #region Communication Email Template
        public async Task<ApiResponse<string>> CreateEmailTemplate(
           CommunicationEmailTemplateDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.TemplateCode))
                    throw new CustomException("Template Code is required.");

                if (string.IsNullOrWhiteSpace(dto.TemplateName))
                    throw new CustomException("Template Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Body))
                    throw new CustomException("Template Body is required.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationEmailTemplate>()
                    .FindAsync(x =>
                        x.TemplateCode.ToLower() ==
                        dto.TemplateCode.ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "Template Code already exists.");

                var template = new CommunicationEmailTemplate
                {
                    TemplateCode = dto.TemplateCode,
                    TemplateName = dto.TemplateName,
                    Category = dto.Category,
                    Subject = dto.Subject,
                    Body = dto.Body,
                    LanguageCode = dto.LanguageCode,
                    ProviderName = dto.ProviderName,
                    Version = dto.Version,
                    Status = dto.Status,
                    IsActive = dto.IsActive,
                    CreatedBy = _currentUserService.UserId,
                    CreatedOn = DateTime.Now
                };

                await _unitOfWork
                    .Repository<CommunicationEmailTemplate>()
                    .AddAsync(template);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationEmailTemplate",
                    "INSERT",
                    template.EmailTemplateId,
                    "",
                    JsonConvert.SerializeObject(template),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email Template Created Successfully",
                    Data = template.TemplateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while creating email template");

                throw;
            }
        }

        public async Task<ApiResponse<string>> UpdateEmailTemplate(
            CommunicationEmailTemplateDto dto)
        {
            try
            {
                var template = (await _unitOfWork
                    .Repository<CommunicationEmailTemplate>()
                    .FindAsync(x =>
                        x.EmailTemplateId ==
                        dto.EmailTemplateId))
                    .FirstOrDefault();

                if (template == null)
                    throw new CustomException(
                        "Email template not found.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationEmailTemplate>()
                    .FindAsync(x =>
                        x.EmailTemplateId != dto.EmailTemplateId &&
                        x.TemplateCode.ToLower() ==
                        dto.TemplateCode.ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "Template Code already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(template);

                template.TemplateCode = dto.TemplateCode;
                template.TemplateName = dto.TemplateName;
                template.Category = dto.Category;
                template.Subject = dto.Subject;
                template.Body = dto.Body;
                template.LanguageCode = dto.LanguageCode;
                template.ProviderName = dto.ProviderName;
                template.Version = dto.Version;
                template.Status = dto.Status;
                template.IsActive = dto.IsActive;
                template.UpdatedBy = _currentUserService.UserId;
                template.UpdatedOn = DateTime.Now;

                _unitOfWork.Repository<CommunicationEmailTemplate>()
                    .Update(template);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationEmailTemplate",
                    "UPDATE",
                    template.EmailTemplateId,
                    oldValues,
                    JsonConvert.SerializeObject(template),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email Template Updated Successfully",
                    Data = template.TemplateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while updating email template");

                throw;
            }
        }

        public async Task<ApiResponse<string>> DeleteEmailTemplate(int id)
        {
            try
            {
                var template = (await _unitOfWork
                    .Repository<CommunicationEmailTemplate>()
                    .FindAsync(x =>
                        x.EmailTemplateId == id))
                    .FirstOrDefault();

                if (template == null)
                    throw new CustomException(
                        "Email template not found.");

                string oldValues =
                    JsonConvert.SerializeObject(template);

                _unitOfWork
                    .Repository<CommunicationEmailTemplate>()
                    .Remove(template);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationEmailTemplate",
                    "DELETE",
                    template.EmailTemplateId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email Template Deleted Successfully",
                    Data = template.TemplateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while deleting email template");

                throw;
            }
        }

        public async Task<ApiResponse<List<CommunicationEmailTemplateDto>>>
            GetEmailTemplates()
        {
            var templates = await _unitOfWork
                .Repository<CommunicationEmailTemplate>()
                .GetAllAsync();

            var result = templates
                .OrderByDescending(x => x.EmailTemplateId)
                .Select(x => new CommunicationEmailTemplateDto
                {
                    EmailTemplateId = x.EmailTemplateId,
                    TemplateCode = x.TemplateCode,
                    TemplateName = x.TemplateName,
                    Category = x.Category,
                    Subject = x.Subject,
                    Body = x.Body,
                    LanguageCode = x.LanguageCode,
                    ProviderName = x.ProviderName,
                    Version = x.Version,
                    Status = x.Status,
                    IsActive = x.IsActive
                })
                .ToList();

            return new ApiResponse<List<CommunicationEmailTemplateDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        public async Task<ApiResponse<CommunicationEmailTemplateDto>>
            GetEmailTemplateById(int id)
        {
            var template = (await _unitOfWork
                .Repository<CommunicationEmailTemplate>()
                .FindAsync(x =>
                    x.EmailTemplateId == id))
                .FirstOrDefault();

            if (template == null)
                throw new CustomException(
                    "Email template not found.");

            return new ApiResponse<CommunicationEmailTemplateDto>
            {
                Success = true,
                Message = "Success",

                Data = new CommunicationEmailTemplateDto
                {
                    EmailTemplateId =
                        template.EmailTemplateId,

                    TemplateCode =
                        template.TemplateCode,

                    TemplateName =
                        template.TemplateName,

                    Category =
                        template.Category,

                    Subject =
                        template.Subject,

                    Body =
                        template.Body,

                    LanguageCode =
                        template.LanguageCode,

                    ProviderName =
                        template.ProviderName,

                    Version =
                        template.Version,

                    Status =
                        template.Status,

                    IsActive =
                        template.IsActive
                }
            };
        }
        #endregion
        #region Communication SMS Template
        #region Create SMS Template

        public async Task<ApiResponse<string>> CreateSMSTemplate(
            CommunicationSMSTemplateDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.TemplateCode))
                    throw new CustomException("Template Code is required.");

                if (string.IsNullOrWhiteSpace(dto.TemplateName))
                    throw new CustomException("Template Name is required.");

                if (string.IsNullOrWhiteSpace(dto.MessageBody))
                    throw new CustomException("Message Body is required.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationSmstemplate>()
                    .FindAsync(x =>
                        x.TemplateCode.ToLower() ==
                        dto.TemplateCode.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Template Code already exists.");

                var characterCount = dto.MessageBody.Length;

                var template = new CommunicationSmstemplate
                {
                    TemplateCode = dto.TemplateCode,
                    TemplateName = dto.TemplateName,
                    Category = dto.Category,
                    MessageBody = dto.MessageBody,
                    CharacterCount = characterCount,
                    LanguageCode = dto.LanguageCode,
                    ProviderName = dto.ProviderName,
                    Version = dto.Version,
                    Status = dto.Status,
                    IsActive = dto.IsActive,
                    CreatedBy = _currentUserService.UserId,
                    CreatedOn = DateTime.Now
                };

                await _unitOfWork
                    .Repository<CommunicationSmstemplate>()
                    .AddAsync(template);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationSMSTemplate",
                    "INSERT",
                    template.SmstemplateId,
                    "",
                    JsonConvert.SerializeObject(template),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "SMS Template Created Successfully",
                    Data = template.TemplateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating SMS template");
                throw;
            }
        }

        #endregion

        #region Update SMS Template

        public async Task<ApiResponse<string>> UpdateSMSTemplate(
            CommunicationSMSTemplateDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.TemplateCode))
                    throw new CustomException("Template Code is required.");

                if (string.IsNullOrWhiteSpace(dto.TemplateName))
                    throw new CustomException("Template Name is required.");

                if (string.IsNullOrWhiteSpace(dto.MessageBody))
                    throw new CustomException("Message Body is required.");

                var template = (await _unitOfWork
                    .Repository<CommunicationSmstemplate>()
                    .FindAsync(x =>
                        x.SmstemplateId == dto.SmstemplateId))
                    .FirstOrDefault();

                if (template == null)
                    throw new CustomException("SMS Template not found.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationSmstemplate>()
                    .FindAsync(x =>
                        x.SmstemplateId != dto.SmstemplateId &&
                        x.TemplateCode.ToLower() ==
                        dto.TemplateCode.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Template Code already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(template);

                template.TemplateCode = dto.TemplateCode;
                template.TemplateName = dto.TemplateName;
                template.Category = dto.Category;
                template.MessageBody = dto.MessageBody;
                template.CharacterCount = dto.MessageBody.Length;
                template.LanguageCode = dto.LanguageCode;
                template.ProviderName = dto.ProviderName;
                template.Version = dto.Version;
                template.Status = dto.Status;
                template.IsActive = dto.IsActive;
                template.UpdatedBy = _currentUserService.UserId;
                template.UpdatedOn = DateTime.Now;

                _unitOfWork
                    .Repository<CommunicationSmstemplate>()
                    .Update(template);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationSMSTemplate",
                    "UPDATE",
                    template.SmstemplateId,
                    oldValues,
                    JsonConvert.SerializeObject(template),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "SMS Template Updated Successfully",
                    Data = template.TemplateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating SMS template");
                throw;
            }
        }

        #endregion

        #region Delete SMS Template

        public async Task<ApiResponse<string>> DeleteSMSTemplate(int id)
        {
            try
            {
                var template = (await _unitOfWork
                    .Repository<CommunicationSmstemplate>()
                    .FindAsync(x =>
                        x.SmstemplateId == id))
                    .FirstOrDefault();

                if (template == null)
                    throw new CustomException("SMS Template not found.");

                string oldValues =
                    JsonConvert.SerializeObject(template);

                _unitOfWork
                    .Repository<CommunicationSmstemplate>()
                    .Remove(template);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationSMSTemplate",
                    "DELETE",
                    template.SmstemplateId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "SMS Template Deleted Successfully",
                    Data = template.TemplateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting SMS template");
                throw;
            }
        }

        #endregion

        #region Get SMS Templates

        public async Task<ApiResponse<List<CommunicationSMSTemplateDto>>>
            GetSMSTemplates()
        {
            var templates = await _unitOfWork
                .Repository<CommunicationSmstemplate>()
                .GetAllAsync();

            var result = templates
                .OrderByDescending(x => x.SmstemplateId)
                .Select(x => new CommunicationSMSTemplateDto
                {
                    SmstemplateId = x.SmstemplateId,
                    TemplateCode = x.TemplateCode,
                    TemplateName = x.TemplateName,
                    Category = x.Category,
                    MessageBody = x.MessageBody,
                    CharacterCount = x.CharacterCount,
                    LanguageCode = x.LanguageCode,
                    ProviderName = x.ProviderName,
                    Version = x.Version,
                    Status = x.Status,
                    IsActive = x.IsActive
                })
                .ToList();

            return new ApiResponse<List<CommunicationSMSTemplateDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        #endregion

        #region Get SMS Template By Id

        public async Task<ApiResponse<CommunicationSMSTemplateDto>>
            GetSMSTemplateById(int id)
        {
            var template = (await _unitOfWork
                .Repository<CommunicationSmstemplate>()
                .FindAsync(x =>
                    x.SmstemplateId == id))
                .FirstOrDefault();

            if (template == null)
                throw new CustomException("SMS Template not found.");

            return new ApiResponse<CommunicationSMSTemplateDto>
            {
                Success = true,
                Message = "Success",
                Data = new CommunicationSMSTemplateDto
                {
                    SmstemplateId = template.SmstemplateId,
                    TemplateCode = template.TemplateCode,
                    TemplateName = template.TemplateName,
                    Category = template.Category,
                    MessageBody = template.MessageBody,
                    CharacterCount = template.CharacterCount,
                    LanguageCode = template.LanguageCode,
                    ProviderName = template.ProviderName,
                    Version = template.Version,
                    Status = template.Status,
                    IsActive = template.IsActive
                }
            };
        }

        #endregion
        #endregion
        #region Communication WhatsApp Template
        #region Create WhatsApp Template

        public async Task<ApiResponse<string>> CreateWhatsAppTemplate(
            CommunicationWhatsAppTemplateDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.TemplateCode))
                    throw new CustomException("Template Code is required.");

                if (string.IsNullOrWhiteSpace(dto.TemplateName))
                    throw new CustomException("Template Name is required.");

                if (string.IsNullOrWhiteSpace(dto.BodyText))
                    throw new CustomException("Body Text is required.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationWhatsAppTemplate>()
                    .FindAsync(x =>
                        x.TemplateCode.ToLower() ==
                        dto.TemplateCode.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Template Code already exists.");

                var template = new CommunicationWhatsAppTemplate
                {
                    TemplateCode = dto.TemplateCode,
                    TemplateName = dto.TemplateName,
                    Category = dto.Category,
                    LanguageCode = dto.LanguageCode,
                    ProviderName = dto.ProviderName,
                    TemplateSid = dto.TemplateSid,
                    HeaderText = dto.HeaderText,
                    BodyText = dto.BodyText,
                    FooterText = dto.FooterText,
                    ApprovalStatus = dto.ApprovalStatus,
                    RejectionReason = dto.RejectionReason,
                    Version = dto.Version,
                    Status = dto.Status,
                    IsActive = dto.IsActive,
                    CreatedBy = _currentUserService.UserId,
                    CreatedOn = DateTime.Now
                };

                await _unitOfWork
                    .Repository<CommunicationWhatsAppTemplate>()
                    .AddAsync(template);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationWhatsAppTemplate",
                    "INSERT",
                    template.WhatsAppTemplateId,
                    "",
                    JsonConvert.SerializeObject(template),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "WhatsApp Template Created Successfully",
                    Data = template.TemplateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating WhatsApp template");
                throw;
            }
        }

        #endregion

        #region Update WhatsApp Template

        public async Task<ApiResponse<string>> UpdateWhatsAppTemplate(
            CommunicationWhatsAppTemplateDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.TemplateCode))
                    throw new CustomException("Template Code is required.");

                if (string.IsNullOrWhiteSpace(dto.TemplateName))
                    throw new CustomException("Template Name is required.");

                if (string.IsNullOrWhiteSpace(dto.BodyText))
                    throw new CustomException("Body Text is required.");

                var template = (await _unitOfWork
                    .Repository<CommunicationWhatsAppTemplate>()
                    .FindAsync(x =>
                        x.WhatsAppTemplateId == dto.WhatsAppTemplateId))
                    .FirstOrDefault();

                if (template == null)
                    throw new CustomException("WhatsApp Template not found.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationWhatsAppTemplate>()
                    .FindAsync(x =>
                        x.WhatsAppTemplateId != dto.WhatsAppTemplateId &&
                        x.TemplateCode.ToLower() ==
                        dto.TemplateCode.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Template Code already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(template);

                template.TemplateCode = dto.TemplateCode;
                template.TemplateName = dto.TemplateName;
                template.Category = dto.Category;
                template.LanguageCode = dto.LanguageCode;
                template.ProviderName = dto.ProviderName;
                template.TemplateSid = dto.TemplateSid;
                template.HeaderText = dto.HeaderText;
                template.BodyText = dto.BodyText;
                template.FooterText = dto.FooterText;
                template.ApprovalStatus = dto.ApprovalStatus;
                template.RejectionReason = dto.RejectionReason;
                template.Version = dto.Version;
                template.Status = dto.Status;
                template.IsActive = dto.IsActive;
                template.UpdatedBy = _currentUserService.UserId;
                template.UpdatedOn = DateTime.Now;

                _unitOfWork
                    .Repository<CommunicationWhatsAppTemplate>()
                    .Update(template);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationWhatsAppTemplate",
                    "UPDATE",
                    template.WhatsAppTemplateId,
                    oldValues,
                    JsonConvert.SerializeObject(template),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "WhatsApp Template Updated Successfully",
                    Data = template.TemplateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating WhatsApp template");
                throw;
            }
        }

        #endregion

        #region Delete WhatsApp Template

        public async Task<ApiResponse<string>> DeleteWhatsAppTemplate(int id)
        {
            try
            {
                var template = (await _unitOfWork
                    .Repository<CommunicationWhatsAppTemplate>()
                    .FindAsync(x =>
                        x.WhatsAppTemplateId == id))
                    .FirstOrDefault();

                if (template == null)
                    throw new CustomException("WhatsApp Template not found.");

                string oldValues =
                    JsonConvert.SerializeObject(template);

                _unitOfWork
                    .Repository<CommunicationWhatsAppTemplate>()
                    .Remove(template);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationWhatsAppTemplate",
                    "DELETE",
                    template.WhatsAppTemplateId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "WhatsApp Template Deleted Successfully",
                    Data = template.TemplateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting WhatsApp template");
                throw;
            }
        }

        #endregion

        #region Get WhatsApp Templates

        public async Task<ApiResponse<List<CommunicationWhatsAppTemplateDto>>>
            GetWhatsAppTemplates()
        {
            var templates = await _unitOfWork
                .Repository<CommunicationWhatsAppTemplate>()
                .GetAllAsync();

            var result = templates
                .OrderByDescending(x => x.WhatsAppTemplateId)
                .Select(x => new CommunicationWhatsAppTemplateDto
                {
                    WhatsAppTemplateId = x.WhatsAppTemplateId,
                    TemplateCode = x.TemplateCode,
                    TemplateName = x.TemplateName,
                    Category = x.Category,
                    LanguageCode = x.LanguageCode,
                    ProviderName = x.ProviderName,
                    TemplateSid = x.TemplateSid,
                    HeaderText = x.HeaderText,
                    BodyText = x.BodyText,
                    FooterText = x.FooterText,
                    ApprovalStatus = x.ApprovalStatus,
                    RejectionReason = x.RejectionReason,
                    Version = x.Version,
                    Status = x.Status,
                    IsActive = x.IsActive
                })
                .ToList();

            return new ApiResponse<List<CommunicationWhatsAppTemplateDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        #endregion

        #region Get WhatsApp Template By Id

        public async Task<ApiResponse<CommunicationWhatsAppTemplateDto>>
            GetWhatsAppTemplateById(int id)
        {
            var template = (await _unitOfWork
                .Repository<CommunicationWhatsAppTemplate>()
                .FindAsync(x =>
                    x.WhatsAppTemplateId == id))
                .FirstOrDefault();

            if (template == null)
                throw new CustomException("WhatsApp Template not found.");

            return new ApiResponse<CommunicationWhatsAppTemplateDto>
            {
                Success = true,
                Message = "Success",
                Data = new CommunicationWhatsAppTemplateDto
                {
                    WhatsAppTemplateId = template.WhatsAppTemplateId,
                    TemplateCode = template.TemplateCode,
                    TemplateName = template.TemplateName,
                    Category = template.Category,
                    LanguageCode = template.LanguageCode,
                    ProviderName = template.ProviderName,
                    TemplateSid = template.TemplateSid,
                    HeaderText = template.HeaderText,
                    BodyText = template.BodyText,
                    FooterText = template.FooterText,
                    ApprovalStatus = template.ApprovalStatus,
                    RejectionReason = template.RejectionReason,
                    Version = template.Version,
                    Status = template.Status,
                    IsActive = template.IsActive
                }
            };
        }

        #endregion
        #endregion
        #region Communication Notification Template
        #region Create Notification Template

        public async Task<ApiResponse<string>> CreateNotificationTemplate(
            CommunicationNotificationTemplateDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.TemplateCode))
                    throw new CustomException("Template Code is required.");

                if (string.IsNullOrWhiteSpace(dto.TemplateName))
                    throw new CustomException("Template Name is required.");

                if (string.IsNullOrWhiteSpace(dto.MessageBody))
                    throw new CustomException("Message Body is required.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationNotificationTemplate>()
                    .FindAsync(x =>
                        x.TemplateCode.ToLower() ==
                        dto.TemplateCode.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Template Code already exists.");

                var template = new CommunicationNotificationTemplate
                {
                    TemplateCode = dto.TemplateCode,
                    TemplateName = dto.TemplateName,
                    NotificationType = dto.NotificationType,
                    Category = dto.Category,
                    Title = dto.Title,
                    MessageBody = dto.MessageBody,
                    Channel = dto.Channel,
                    LanguageCode = dto.LanguageCode,
                    Version = dto.Version,
                    Status = dto.Status,
                    IsActive = dto.IsActive,
                    CreatedBy = _currentUserService.UserId,
                    CreatedOn = DateTime.Now
                };

                await _unitOfWork
                    .Repository<CommunicationNotificationTemplate>()
                    .AddAsync(template);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationNotificationTemplate",
                    "INSERT",
                    template.NotificationTemplateId,
                    "",
                    JsonConvert.SerializeObject(template),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Notification Template Created Successfully",
                    Data = template.TemplateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating notification template");

                throw;
            }
        }

        #endregion

        #region Update Notification Template

        public async Task<ApiResponse<string>> UpdateNotificationTemplate(
            CommunicationNotificationTemplateDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.TemplateCode))
                    throw new CustomException("Template Code is required.");

                if (string.IsNullOrWhiteSpace(dto.TemplateName))
                    throw new CustomException("Template Name is required.");

                if (string.IsNullOrWhiteSpace(dto.MessageBody))
                    throw new CustomException("Message Body is required.");

                var template = (await _unitOfWork
                    .Repository<CommunicationNotificationTemplate>()
                    .FindAsync(x =>
                        x.NotificationTemplateId ==
                        dto.NotificationTemplateId))
                    .FirstOrDefault();

                if (template == null)
                    throw new CustomException(
                        "Notification Template not found.");

                var duplicate = await _unitOfWork
                    .Repository<CommunicationNotificationTemplate>()
                    .FindAsync(x =>
                        x.NotificationTemplateId !=
                        dto.NotificationTemplateId &&
                        x.TemplateCode.ToLower() ==
                        dto.TemplateCode.ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "Template Code already exists.");

                string oldValues =
                    JsonConvert.SerializeObject(template);

                template.TemplateCode = dto.TemplateCode;
                template.TemplateName = dto.TemplateName;
                template.NotificationType = dto.NotificationType;
                template.Category = dto.Category;
                template.Title = dto.Title;
                template.MessageBody = dto.MessageBody;
                template.Channel = dto.Channel;
                template.LanguageCode = dto.LanguageCode;
                template.Version = dto.Version;
                template.Status = dto.Status;
                template.IsActive = dto.IsActive;
                template.UpdatedBy = _currentUserService.UserId;
                template.UpdatedOn = DateTime.Now;

                _unitOfWork
                    .Repository<CommunicationNotificationTemplate>()
                    .Update(template);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationNotificationTemplate",
                    "UPDATE",
                    template.NotificationTemplateId,
                    oldValues,
                    JsonConvert.SerializeObject(template),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Notification Template Updated Successfully",
                    Data = template.TemplateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating notification template");

                throw;
            }
        }

        #endregion

        #region Delete Notification Template

        public async Task<ApiResponse<string>> DeleteNotificationTemplate(int id)
        {
            try
            {
                var template = (await _unitOfWork
                    .Repository<CommunicationNotificationTemplate>()
                    .FindAsync(x =>
                        x.NotificationTemplateId == id))
                    .FirstOrDefault();

                if (template == null)
                    throw new CustomException(
                        "Notification Template not found.");

                string oldValues =
                    JsonConvert.SerializeObject(template);

                _unitOfWork
                    .Repository<CommunicationNotificationTemplate>()
                    .Remove(template);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "CommunicationNotificationTemplate",
                    "DELETE",
                    template.NotificationTemplateId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Notification Template Deleted Successfully",
                    Data = template.TemplateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting notification template");

                throw;
            }
        }

        #endregion

        #region Get Notification Templates

        public async Task<ApiResponse<List<CommunicationNotificationTemplateDto>>>
            GetNotificationTemplates()
        {
            var templates = await _unitOfWork
                .Repository<CommunicationNotificationTemplate>()
                .GetAllAsync();

            var result = templates
                .OrderByDescending(x => x.NotificationTemplateId)
                .Select(x => new CommunicationNotificationTemplateDto
                {
                    NotificationTemplateId = x.NotificationTemplateId,
                    TemplateCode = x.TemplateCode,
                    TemplateName = x.TemplateName,
                    NotificationType = x.NotificationType,
                    Category = x.Category,
                    Title = x.Title,
                    MessageBody = x.MessageBody,
                    Channel = x.Channel,
                    LanguageCode = x.LanguageCode,
                    Version = x.Version,
                    Status = x.Status,
                    IsActive = x.IsActive
                })
                .ToList();

            return new ApiResponse<
                List<CommunicationNotificationTemplateDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        #endregion

        #region Get Notification Template By Id

        public async Task<ApiResponse<CommunicationNotificationTemplateDto>>
            GetNotificationTemplateById(int id)
        {
            var template = (await _unitOfWork
                .Repository<CommunicationNotificationTemplate>()
                .FindAsync(x =>
                    x.NotificationTemplateId == id))
                .FirstOrDefault();

            if (template == null)
                throw new CustomException(
                    "Notification Template not found.");

            return new ApiResponse<CommunicationNotificationTemplateDto>
            {
                Success = true,
                Message = "Success",
                Data = new CommunicationNotificationTemplateDto
                {
                    NotificationTemplateId =
                        template.NotificationTemplateId,

                    TemplateCode = template.TemplateCode,

                    TemplateName = template.TemplateName,

                    NotificationType =
                        template.NotificationType,

                    Category = template.Category,

                    Title = template.Title,

                    MessageBody =
                        template.MessageBody,

                    Channel =
                        template.Channel,

                    LanguageCode =
                        template.LanguageCode,

                    Version =
                        template.Version,

                    Status =
                        template.Status,

                    IsActive =
                        template.IsActive
                }
            };
        }

        #endregion
        #endregion
    }
}
