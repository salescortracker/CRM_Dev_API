using Business_Layer.DTOs.Admin;
using Business_Layer.Interfaces.Adminsevices;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
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

namespace Business_Layer.Services.Adminservices
{
    public class MarketingService : IMarketingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public MarketingService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        // CREATE
        public async Task<ApiResponse<string>> CreateCampaign(CampaignDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CampaignName))
                    throw new CustomException("Campaign Name is required.");

                var duplicate = await _unitOfWork.Repository<Campaign>()
                    .FindAsync(x =>
                        x.CampaignName.ToLower() == dto.CampaignName.ToLower() &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId);

                if (duplicate.Any())
                    throw new CustomException("Campaign already exists.");

                Campaign campaign = new Campaign
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    CampaignName = dto.CampaignName,
                    CampaignType = dto.CampaignType,
                    MarketingListId = dto.MarketingListId,
                    TotalRecipients = dto.TotalRecipients,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    Status = dto.Status,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now
                };

                await _unitOfWork.Repository<Campaign>()
                    .AddAsync(campaign);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Campaign",
                    "INSERT",
                    campaign.CampaignId,
                    "",
                    JsonConvert.SerializeObject(campaign),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Campaign Created Successfully",
                    Data = campaign.CampaignName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating campaign");
                throw;
            }
        }

        // UPDATE
        public async Task<ApiResponse<string>> UpdateCampaign(CampaignDto dto)
        {
            try
            {
                var campaign = (await _unitOfWork.Repository<Campaign>()
                    .FindAsync(x => x.CampaignId == dto.CampaignId))
                    .FirstOrDefault();

                if (campaign == null)
                    throw new CustomException("Campaign not found.");

                var duplicate = await _unitOfWork.Repository<Campaign>()
                    .FindAsync(x =>
                        x.CampaignId != dto.CampaignId &&
                        x.CampaignName.ToLower() == dto.CampaignName.ToLower() &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId);

                if (duplicate.Any())
                    throw new CustomException("Campaign already exists.");

                string oldValues = JsonConvert.SerializeObject(campaign);

                campaign.CompanyId = dto.CompanyId;
                campaign.RegionId = dto.RegionId;
                campaign.CampaignName = dto.CampaignName;
                campaign.CampaignType = dto.CampaignType;
                campaign.MarketingListId = dto.MarketingListId;
                campaign.TotalRecipients = dto.TotalRecipients;
                campaign.StartDate = dto.StartDate;
                campaign.EndDate = dto.EndDate;
                campaign.Status = dto.Status;
                campaign.UpdatedBy = _currentUserService.UserId;
                campaign.UpdatedDate = DateTime.Now;

                _unitOfWork.Repository<Campaign>().Update(campaign);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Campaign",
                    "UPDATE",
                    campaign.CampaignId,
                    oldValues,
                    JsonConvert.SerializeObject(campaign),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Campaign Updated Successfully",
                    Data = campaign.CampaignName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating campaign");
                throw;
            }
        }

        // DELETE
        public async Task<ApiResponse<string>> DeleteCampaign(int id)
        {
            try
            {
                var campaign = (await _unitOfWork.Repository<Campaign>()
                    .FindAsync(x => x.CampaignId == id))
                    .FirstOrDefault();

                if (campaign == null)
                    throw new CustomException("Campaign not found.");

                string oldValues = JsonConvert.SerializeObject(campaign);

                _unitOfWork.Repository<Campaign>().Remove(campaign);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Campaign",
                    "DELETE",
                    campaign.CampaignId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Campaign Deleted Successfully",
                    Data = campaign.CampaignName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting campaign");
                throw;
            }
        }

        // GET ALL
        public async Task<ApiResponse<List<CampaignDto>>> GetCampaigns()
        {
            try
            {
                var campaigns = await _unitOfWork.Repository<Campaign>()
                    .GetAllAsync();

                var result = campaigns
                    .Select(x => new CampaignDto
                    {
                        CampaignId = x.CampaignId,
                        CompanyId = x.CompanyId,
                        RegionId = x.RegionId,
                        CampaignName = x.CampaignName,
                        CampaignType = x.CampaignType,
                        MarketingListId = x.MarketingListId,
                        TotalRecipients = x.TotalRecipients,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        Status = x.Status
                    })
                    .ToList();

                return new ApiResponse<List<CampaignDto>>
                {
                    Success = true,
                    Message = "Campaigns Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting campaigns");
                throw;
            }
        }

        // GET BY ID
        public async Task<ApiResponse<CampaignDto>> GetCampaignById(int id)
        {
            try
            {
                var campaign = (await _unitOfWork.Repository<Campaign>()
                    .FindAsync(x => x.CampaignId == id))
                    .FirstOrDefault();

                if (campaign == null)
                    throw new CustomException("Campaign not found.");

                var result = new CampaignDto
                {
                    CampaignId = campaign.CampaignId,
                    CompanyId = campaign.CompanyId,
                    RegionId = campaign.RegionId,
                    CampaignName = campaign.CampaignName,
                    CampaignType = campaign.CampaignType,
                    MarketingListId = campaign.MarketingListId,
                    TotalRecipients = campaign.TotalRecipients,
                    StartDate = campaign.StartDate,
                    EndDate = campaign.EndDate,
                    Status = campaign.Status
                };

                return new ApiResponse<CampaignDto>
                {
                    Success = true,
                    Message = "Campaign Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting campaign by id");
                throw;
            }
        }

        // CREATE
        public async Task<ApiResponse<string>> CreateEmailCampaign(EmailCampaignDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CampaignName))
                    throw new CustomException("Campaign Name is required.");

                var duplicate = await _unitOfWork.Repository<EmailCampaign>()
                    .FindAsync(x =>
                        x.CampaignName.ToLower() == dto.CampaignName.ToLower() &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId);

                if (duplicate.Any())
                    throw new CustomException("Email Campaign already exists.");

                EmailCampaign emailCampaign = new EmailCampaign
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    CampaignName = dto.CampaignName,
                    MarketingListId = dto.MarketingListId,
                    EmailTemplateId = dto.EmailTemplateId,
                    Subject = dto.Subject,
                    FromName = dto.FromName,
                    FromEmail = dto.FromEmail,
                    ReplyToEmail = dto.ReplyToEmail,
                    TotalRecipients = dto.TotalRecipients,
                    SentCount = dto.SentCount,
                    DeliveredCount = dto.DeliveredCount,
                    OpenedCount = dto.OpenedCount,
                    ClickedCount = dto.ClickedCount,
                    BouncedCount = dto.BouncedCount,
                    UnsubscribedCount = dto.UnsubscribedCount,
                    ScheduledDate = dto.ScheduledDate,
                    Status = dto.Status,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now
                };

                await _unitOfWork.Repository<EmailCampaign>()
                    .AddAsync(emailCampaign);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "EmailCampaign",
                    "INSERT",
                    emailCampaign.EmailCampaignId,
                    "",
                    JsonConvert.SerializeObject(emailCampaign),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email Campaign Created Successfully",
                    Data = emailCampaign.CampaignName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating email campaign");
                throw;
            }
        }

        // UPDATE
        public async Task<ApiResponse<string>> UpdateEmailCampaign(EmailCampaignDto dto)
        {
            try
            {
                var emailCampaign = (await _unitOfWork.Repository<EmailCampaign>()
                    .FindAsync(x => x.EmailCampaignId == dto.EmailCampaignId))
                    .FirstOrDefault();

                if (emailCampaign == null)
                    throw new CustomException("Email Campaign not found.");

                var duplicate = await _unitOfWork.Repository<EmailCampaign>()
                    .FindAsync(x =>
                        x.EmailCampaignId != dto.EmailCampaignId &&
                        x.CampaignName.ToLower() == dto.CampaignName.ToLower() &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId);

                if (duplicate.Any())
                    throw new CustomException("Email Campaign already exists.");

                string oldValues = JsonConvert.SerializeObject(emailCampaign);

                emailCampaign.CompanyId = dto.CompanyId;
                emailCampaign.RegionId = dto.RegionId;
                emailCampaign.CampaignName = dto.CampaignName;
                emailCampaign.MarketingListId = dto.MarketingListId;
                emailCampaign.EmailTemplateId = dto.EmailTemplateId;
                emailCampaign.Subject = dto.Subject;
                emailCampaign.FromName = dto.FromName;
                emailCampaign.FromEmail = dto.FromEmail;
                emailCampaign.ReplyToEmail = dto.ReplyToEmail;
                emailCampaign.TotalRecipients = dto.TotalRecipients;
                emailCampaign.SentCount = dto.SentCount;
                emailCampaign.DeliveredCount = dto.DeliveredCount;
                emailCampaign.OpenedCount = dto.OpenedCount;
                emailCampaign.ClickedCount = dto.ClickedCount;
                emailCampaign.BouncedCount = dto.BouncedCount;
                emailCampaign.UnsubscribedCount = dto.UnsubscribedCount;
                emailCampaign.ScheduledDate = dto.ScheduledDate;
                emailCampaign.Status = dto.Status;
                emailCampaign.UpdatedBy = _currentUserService.UserId;
                emailCampaign.UpdatedDate = DateTime.Now;

                _unitOfWork.Repository<EmailCampaign>()
                    .Update(emailCampaign);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "EmailCampaign",
                    "UPDATE",
                    emailCampaign.EmailCampaignId,
                    oldValues,
                    JsonConvert.SerializeObject(emailCampaign),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email Campaign Updated Successfully",
                    Data = emailCampaign.CampaignName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating email campaign");
                throw;
            }
        }

        // DELETE
        public async Task<ApiResponse<string>> DeleteEmailCampaign(int id)
        {
            try
            {
                var emailCampaign = (await _unitOfWork.Repository<EmailCampaign>()
                    .FindAsync(x => x.EmailCampaignId == id))
                    .FirstOrDefault();

                if (emailCampaign == null)
                    throw new CustomException("Email Campaign not found.");

                string oldValues = JsonConvert.SerializeObject(emailCampaign);

                _unitOfWork.Repository<EmailCampaign>()
                    .Remove(emailCampaign);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "EmailCampaign",
                    "DELETE",
                    emailCampaign.EmailCampaignId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email Campaign Deleted Successfully",
                    Data = emailCampaign.CampaignName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting email campaign");
                throw;
            }
        }

        // GET ALL
        public async Task<ApiResponse<List<EmailCampaignDto>>> GetEmailCampaigns()
        {
            try
            {
                var emailCampaigns = await _unitOfWork.Repository<EmailCampaign>()
                    .GetAllAsync();

                var result = emailCampaigns
                    .Select(x => new EmailCampaignDto
                    {
                        EmailCampaignId = x.EmailCampaignId,
                        CompanyId = x.CompanyId,
                        RegionId = x.RegionId,
                        CampaignName = x.CampaignName,
                        MarketingListId = x.MarketingListId,
                        EmailTemplateId = x.EmailTemplateId,
                        Subject = x.Subject,
                        FromName = x.FromName,
                        FromEmail = x.FromEmail,
                        ReplyToEmail = x.ReplyToEmail,
                        TotalRecipients = x.TotalRecipients,
                        SentCount = x.SentCount,
                        DeliveredCount = x.DeliveredCount,
                        OpenedCount = x.OpenedCount,
                        ClickedCount = x.ClickedCount,
                        BouncedCount = x.BouncedCount,
                        UnsubscribedCount = x.UnsubscribedCount,
                        ScheduledDate = x.ScheduledDate,
                        Status = x.Status
                    })
                    .ToList();

                return new ApiResponse<List<EmailCampaignDto>>
                {
                    Success = true,
                    Message = "Email Campaigns Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting email campaigns");
                throw;
            }
        }

        // GET BY ID
        public async Task<ApiResponse<EmailCampaignDto>> GetEmailCampaignById(int id)
        {
            try
            {
                var emailCampaign = (await _unitOfWork.Repository<EmailCampaign>()
                    .FindAsync(x => x.EmailCampaignId == id))
                    .FirstOrDefault();

                if (emailCampaign == null)
                    throw new CustomException("Email Campaign not found.");

                var result = new EmailCampaignDto
                {
                    EmailCampaignId = emailCampaign.EmailCampaignId,
                    CompanyId = emailCampaign.CompanyId,
                    RegionId = emailCampaign.RegionId,
                    CampaignName = emailCampaign.CampaignName,
                    MarketingListId = emailCampaign.MarketingListId,
                    EmailTemplateId = emailCampaign.EmailTemplateId,
                    Subject = emailCampaign.Subject,
                    FromName = emailCampaign.FromName,
                    FromEmail = emailCampaign.FromEmail,
                    ReplyToEmail = emailCampaign.ReplyToEmail,
                    TotalRecipients = emailCampaign.TotalRecipients,
                    SentCount = emailCampaign.SentCount,
                    DeliveredCount = emailCampaign.DeliveredCount,
                    OpenedCount = emailCampaign.OpenedCount,
                    ClickedCount = emailCampaign.ClickedCount,
                    BouncedCount = emailCampaign.BouncedCount,
                    UnsubscribedCount = emailCampaign.UnsubscribedCount,
                    ScheduledDate = emailCampaign.ScheduledDate,
                    Status = emailCampaign.Status
                };

                return new ApiResponse<EmailCampaignDto>
                {
                    Success = true,
                    Message = "Email Campaign Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting email campaign by id");
                throw;
            }
        }
        public async Task<ApiResponse<string>> CreateSmsCampaign(SmsCampaignDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CampaignName))
                    throw new CustomException("SMS Campaign Name is required.");

                var duplicate = await _unitOfWork.Repository<Smscampaign>()
                    .FindAsync(x =>
                        x.CampaignName.ToLower() == dto.CampaignName.ToLower() &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId);

                if (duplicate.Any())
                    throw new CustomException("SMS Campaign already exists.");

                Smscampaign smsCampaign = new Smscampaign
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    CampaignName = dto.CampaignName,
                    MarketingListId = dto.MarketingListId,
                    SmstemplateId = dto.SmstemplateId,
                    Sender = dto.Sender,
                    Message = dto.Message,
                    TotalRecipients = dto.TotalRecipients,
                    SentCount = dto.SentCount,
                    DeliveredCount = dto.DeliveredCount,
                    FailedCount = dto.FailedCount,
                    ScheduledDate = dto.ScheduledDate,
                    Status = dto.Status,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now
                };

                await _unitOfWork.Repository<Smscampaign>()
                    .AddAsync(smsCampaign);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Smscampaign",
                    "INSERT",
                    smsCampaign.SmscampaignId,
                    "",
                    JsonConvert.SerializeObject(smsCampaign),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "SMS Campaign Created Successfully",
                    Data = smsCampaign.CampaignName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating SMS campaign");
                throw;
            }
        }

        // UPDATE SMS CAMPAIGN
        public async Task<ApiResponse<string>> UpdateSmsCampaign(SmsCampaignDto dto)
        {
            try
            {
                var smsCampaign = (await _unitOfWork.Repository<Smscampaign>()
                    .FindAsync(x => x.SmscampaignId == dto.SmscampaignId))
                    .FirstOrDefault();

                if (smsCampaign == null)
                    throw new CustomException("SMS Campaign not found.");

                var duplicate = await _unitOfWork.Repository<Smscampaign>()
                    .FindAsync(x =>
                        x.SmscampaignId != dto.SmscampaignId &&
                        x.CampaignName.ToLower() == dto.CampaignName.ToLower() &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId);

                if (duplicate.Any())
                    throw new CustomException("SMS Campaign already exists.");

                string oldValues = JsonConvert.SerializeObject(smsCampaign);

                smsCampaign.CompanyId = dto.CompanyId;
                smsCampaign.RegionId = dto.RegionId;
                smsCampaign.CampaignName = dto.CampaignName;
                smsCampaign.MarketingListId = dto.MarketingListId;
                smsCampaign.SmstemplateId = dto.SmstemplateId;
                smsCampaign.Sender = dto.Sender;
                smsCampaign.Message = dto.Message;
                smsCampaign.TotalRecipients = dto.TotalRecipients;
                smsCampaign.SentCount = dto.SentCount;
                smsCampaign.DeliveredCount = dto.DeliveredCount;
                smsCampaign.FailedCount = dto.FailedCount;
                smsCampaign.ScheduledDate = dto.ScheduledDate;
                smsCampaign.Status = dto.Status;
                smsCampaign.UpdatedBy = _currentUserService.UserId;
                smsCampaign.UpdatedDate = DateTime.Now;

                _unitOfWork.Repository<Smscampaign>()
                    .Update(smsCampaign);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Smscampaign",
                    "UPDATE",
                    smsCampaign.SmscampaignId,
                    oldValues,
                    JsonConvert.SerializeObject(smsCampaign),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "SMS Campaign Updated Successfully",
                    Data = smsCampaign.CampaignName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating SMS campaign");
                throw;
            }
        }

        // DELETE SMS CAMPAIGN
        public async Task<ApiResponse<string>> DeleteSmsCampaign(int id)
        {
            try
            {
                var smsCampaign = (await _unitOfWork.Repository<Smscampaign>()
                    .FindAsync(x => x.SmscampaignId == id))
                    .FirstOrDefault();

                if (smsCampaign == null)
                    throw new CustomException("SMS Campaign not found.");

                string oldValues = JsonConvert.SerializeObject(smsCampaign);

                _unitOfWork.Repository<Smscampaign>()
                    .Remove(smsCampaign);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Smscampaign",
                    "DELETE",
                    smsCampaign.SmscampaignId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "SMS Campaign Deleted Successfully",
                    Data = smsCampaign.CampaignName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting SMS campaign");
                throw;
            }
        }


        // GET ALL SMS CAMPAIGNS
        public async Task<ApiResponse<List<SmsCampaignDto>>> GetSmsCampaigns()
        {
            try
            {
                var smsCampaigns = await _unitOfWork.Repository<Smscampaign>()
                    .GetAllAsync();

                var result = smsCampaigns
                    .Select(x => new SmsCampaignDto
                    {
                        SmscampaignId = x.SmscampaignId,
                        CompanyId = x.CompanyId,
                        RegionId = x.RegionId,
                        CampaignName = x.CampaignName,
                        MarketingListId = x.MarketingListId,
                        SmstemplateId = x.SmstemplateId,
                        Sender = x.Sender,
                        Message = x.Message,
                        TotalRecipients = x.TotalRecipients,
                        SentCount = x.SentCount,
                        DeliveredCount = x.DeliveredCount,
                        FailedCount = x.FailedCount,
                        ScheduledDate = x.ScheduledDate,
                        Status = x.Status
                    })
                    .ToList();

                return new ApiResponse<List<SmsCampaignDto>>
                {
                    Success = true,
                    Message = "SMS Campaigns Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting SMS campaigns");
                throw;
            }
        }

        // GET SMS CAMPAIGN BY ID
        public async Task<ApiResponse<SmsCampaignDto>> GetSmsCampaignById(int id)
        {
            try
            {
                var smsCampaign = (await _unitOfWork.Repository<Smscampaign>()
                    .FindAsync(x => x.SmscampaignId == id))
                    .FirstOrDefault();

                if (smsCampaign == null)
                    throw new CustomException("SMS Campaign not found.");

                var result = new SmsCampaignDto
                {
                    SmscampaignId = smsCampaign.SmscampaignId,
                    CompanyId = smsCampaign.CompanyId,
                    RegionId = smsCampaign.RegionId,
                    CampaignName = smsCampaign.CampaignName,
                    MarketingListId = smsCampaign.MarketingListId,
                    SmstemplateId = smsCampaign.SmstemplateId,
                    Sender = smsCampaign.Sender,
                    Message = smsCampaign.Message,
                    TotalRecipients = smsCampaign.TotalRecipients,
                    SentCount = smsCampaign.SentCount,
                    DeliveredCount = smsCampaign.DeliveredCount,
                    FailedCount = smsCampaign.FailedCount,
                    ScheduledDate = smsCampaign.ScheduledDate,
                    Status = smsCampaign.Status
                };

                return new ApiResponse<SmsCampaignDto>
                {
                    Success = true,
                    Message = "SMS Campaign Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting SMS campaign by id");
                throw;
            }
        }

        // CREATE WHATSAPP CAMPAIGN
        public async Task<ApiResponse<string>> CreateWhatsAppCampaign(WhatsAppCampaignDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CampaignName))
                    throw new CustomException("WhatsApp Campaign Name is required.");

                var duplicate = await _unitOfWork.Repository<WhatsAppCampaign>()
                    .FindAsync(x =>
                        x.CampaignName.ToLower() == dto.CampaignName.ToLower() &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId);

                if (duplicate.Any())
                    throw new CustomException("WhatsApp Campaign already exists.");

                WhatsAppCampaign whatsappCampaign = new WhatsAppCampaign
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    CampaignName = dto.CampaignName,
                    MarketingListId = dto.MarketingListId,
                    WhatsAppTemplateId = dto.WhatsAppTemplateId,
                    Language = dto.Language,
                    Message = dto.Message,
                    MediaUrl = dto.MediaUrl,
                    MediaType = dto.MediaType,
                    TotalRecipients = dto.TotalRecipients,
                    SentCount = dto.SentCount,
                    DeliveredCount = dto.DeliveredCount,
                    ReadCount = dto.ReadCount,
                    RepliedCount = dto.RepliedCount,
                    FailedCount = dto.FailedCount,
                    ScheduledDate = dto.ScheduledDate,
                    Status = dto.Status,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now
                };

                await _unitOfWork.Repository<WhatsAppCampaign>()
                    .AddAsync(whatsappCampaign);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "WhatsAppCampaign",
                    "INSERT",
                    whatsappCampaign.WhatsAppCampaignId,
                    "",
                    JsonConvert.SerializeObject(whatsappCampaign),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "WhatsApp Campaign Created Successfully",
                    Data = whatsappCampaign.CampaignName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating WhatsApp campaign");
                throw;
            }
        }

        // UPDATE WHATSAPP CAMPAIGN
        public async Task<ApiResponse<string>> UpdateWhatsAppCampaign(WhatsAppCampaignDto dto)
        {
            try
            {
                var whatsappCampaign = (await _unitOfWork.Repository<WhatsAppCampaign>()
                    .FindAsync(x => x.WhatsAppCampaignId == dto.WhatsAppCampaignId))
                    .FirstOrDefault();

                if (whatsappCampaign == null)
                    throw new CustomException("WhatsApp Campaign not found.");

                var duplicate = await _unitOfWork.Repository<WhatsAppCampaign>()
                    .FindAsync(x =>
                        x.WhatsAppCampaignId != dto.WhatsAppCampaignId &&
                        x.CampaignName.ToLower() == dto.CampaignName.ToLower() &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId);

                if (duplicate.Any())
                    throw new CustomException("WhatsApp Campaign already exists.");

                string oldValues = JsonConvert.SerializeObject(whatsappCampaign);

                whatsappCampaign.CompanyId = dto.CompanyId;
                whatsappCampaign.RegionId = dto.RegionId;
                whatsappCampaign.CampaignName = dto.CampaignName;
                whatsappCampaign.MarketingListId = dto.MarketingListId;
                whatsappCampaign.WhatsAppTemplateId = dto.WhatsAppTemplateId;
                whatsappCampaign.Language = dto.Language;
                whatsappCampaign.Message = dto.Message;
                whatsappCampaign.MediaUrl = dto.MediaUrl;
                whatsappCampaign.MediaType = dto.MediaType;
                whatsappCampaign.TotalRecipients = dto.TotalRecipients;
                whatsappCampaign.SentCount = dto.SentCount;
                whatsappCampaign.DeliveredCount = dto.DeliveredCount;
                whatsappCampaign.ReadCount = dto.ReadCount;
                whatsappCampaign.RepliedCount = dto.RepliedCount;
                whatsappCampaign.FailedCount = dto.FailedCount;
                whatsappCampaign.ScheduledDate = dto.ScheduledDate;
                whatsappCampaign.Status = dto.Status;
                whatsappCampaign.UpdatedBy = _currentUserService.UserId;
                whatsappCampaign.UpdatedDate = DateTime.Now;

                _unitOfWork.Repository<WhatsAppCampaign>()
                    .Update(whatsappCampaign);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "WhatsAppCampaign",
                    "UPDATE",
                    whatsappCampaign.WhatsAppCampaignId,
                    oldValues,
                    JsonConvert.SerializeObject(whatsappCampaign),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "WhatsApp Campaign Updated Successfully",
                    Data = whatsappCampaign.CampaignName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating WhatsApp campaign");
                throw;
            }
        }

        // DELETE WHATSAPP CAMPAIGN
        public async Task<ApiResponse<string>> DeleteWhatsAppCampaign(int id)
        {
            try
            {
                var whatsappCampaign = (await _unitOfWork.Repository<WhatsAppCampaign>()
                    .FindAsync(x => x.WhatsAppCampaignId == id))
                    .FirstOrDefault();

                if (whatsappCampaign == null)
                    throw new CustomException("WhatsApp Campaign not found.");

                string oldValues = JsonConvert.SerializeObject(whatsappCampaign);

                _unitOfWork.Repository<WhatsAppCampaign>()
                    .Remove(whatsappCampaign);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "WhatsAppCampaign",
                    "DELETE",
                    whatsappCampaign.WhatsAppCampaignId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "WhatsApp Campaign Deleted Successfully",
                    Data = whatsappCampaign.CampaignName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting WhatsApp campaign");
                throw;
            }
        }

        // GET ALL WHATSAPP CAMPAIGNS
        public async Task<ApiResponse<List<WhatsAppCampaignDto>>> GetWhatsAppCampaigns()
        {
            try
            {
                var whatsappCampaigns = await _unitOfWork.Repository<WhatsAppCampaign>()
                    .GetAllAsync();

                var result = whatsappCampaigns
                    .Select(x => new WhatsAppCampaignDto
                    {
                        WhatsAppCampaignId = x.WhatsAppCampaignId,
                        CompanyId = x.CompanyId,
                        RegionId = x.RegionId,
                        CampaignName = x.CampaignName,
                        MarketingListId = x.MarketingListId,
                        WhatsAppTemplateId = x.WhatsAppTemplateId,
                        Language = x.Language,
                        Message = x.Message,
                        MediaUrl = x.MediaUrl,
                        MediaType = x.MediaType,
                        TotalRecipients = x.TotalRecipients,
                        SentCount = x.SentCount,
                        DeliveredCount = x.DeliveredCount,
                        ReadCount = x.ReadCount,
                        RepliedCount = x.RepliedCount,
                        FailedCount = x.FailedCount,
                        ScheduledDate = x.ScheduledDate,
                        Status = x.Status
                    })
                    .ToList();

                return new ApiResponse<List<WhatsAppCampaignDto>>
                {
                    Success = true,
                    Message = "WhatsApp Campaigns Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting WhatsApp campaigns");
                throw;
            }
        }

        // GET WHATSAPP CAMPAIGN BY ID
        public async Task<ApiResponse<WhatsAppCampaignDto>> GetWhatsAppCampaignById(int id)
        {
            try
            {
                var whatsappCampaign = (await _unitOfWork.Repository<WhatsAppCampaign>()
                    .FindAsync(x => x.WhatsAppCampaignId == id))
                    .FirstOrDefault();

                if (whatsappCampaign == null)
                    throw new CustomException("WhatsApp Campaign not found.");

                var result = new WhatsAppCampaignDto
                {
                    WhatsAppCampaignId = whatsappCampaign.WhatsAppCampaignId,
                    CompanyId = whatsappCampaign.CompanyId,
                    RegionId = whatsappCampaign.RegionId,
                    CampaignName = whatsappCampaign.CampaignName,
                    MarketingListId = whatsappCampaign.MarketingListId,
                    WhatsAppTemplateId = whatsappCampaign.WhatsAppTemplateId,
                    Language = whatsappCampaign.Language,
                    Message = whatsappCampaign.Message,
                    MediaUrl = whatsappCampaign.MediaUrl,
                    MediaType = whatsappCampaign.MediaType,
                    TotalRecipients = whatsappCampaign.TotalRecipients,
                    SentCount = whatsappCampaign.SentCount,
                    DeliveredCount = whatsappCampaign.DeliveredCount,
                    ReadCount = whatsappCampaign.ReadCount,
                    RepliedCount = whatsappCampaign.RepliedCount,
                    FailedCount = whatsappCampaign.FailedCount,
                    ScheduledDate = whatsappCampaign.ScheduledDate,
                    Status = whatsappCampaign.Status
                };

                return new ApiResponse<WhatsAppCampaignDto>
                {
                    Success = true,
                    Message = "WhatsApp Campaign Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting WhatsApp campaign by id");
                throw;
            }
        }

        // CREATE MARKETING LIST
        public async Task<ApiResponse<string>> CreateMarketingList(MarketingListDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.ListName))
                    throw new CustomException("Marketing List Name is required.");

                var duplicate = await _unitOfWork.Repository<MarketingList>()
                    .FindAsync(x =>
                        x.ListName.ToLower() == dto.ListName.ToLower() &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId);

                if (duplicate.Any())
                    throw new CustomException("Marketing List already exists.");

                MarketingList marketingList = new MarketingList
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    ListName = dto.ListName,
                    Description = dto.Description,
                    ListType = dto.ListType,
                    Source = dto.Source,
                    TotalContacts = dto.TotalContacts,
                    ActiveContacts = dto.ActiveContacts,
                    Status = dto.Status,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now
                };

                await _unitOfWork.Repository<MarketingList>()
                    .AddAsync(marketingList);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "MarketingList",
                    "INSERT",
                    marketingList.MarketingListId,
                    "",
                    JsonConvert.SerializeObject(marketingList),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Marketing List Created Successfully",
                    Data = marketingList.ListName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating marketing list");
                throw;
            }
        }

        // UPDATE MARKETING LIST
        public async Task<ApiResponse<string>> UpdateMarketingList(MarketingListDto dto)
        {
            try
            {
                var marketingList = (await _unitOfWork.Repository<MarketingList>()
                    .FindAsync(x => x.MarketingListId == dto.MarketingListId))
                    .FirstOrDefault();

                if (marketingList == null)
                    throw new CustomException("Marketing List not found.");

                var duplicate = await _unitOfWork.Repository<MarketingList>()
                    .FindAsync(x =>
                        x.MarketingListId != dto.MarketingListId &&
                        x.ListName.ToLower() == dto.ListName.ToLower() &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId);

                if (duplicate.Any())
                    throw new CustomException("Marketing List already exists.");

                string oldValues = JsonConvert.SerializeObject(marketingList);

                marketingList.CompanyId = dto.CompanyId;
                marketingList.RegionId = dto.RegionId;
                marketingList.ListName = dto.ListName;
                marketingList.Description = dto.Description;
                marketingList.ListType = dto.ListType;
                marketingList.Source = dto.Source;
                marketingList.TotalContacts = dto.TotalContacts;
                marketingList.ActiveContacts = dto.ActiveContacts;
                marketingList.Status = dto.Status;
                marketingList.UpdatedBy = _currentUserService.UserId;
                marketingList.UpdatedDate = DateTime.Now;

                _unitOfWork.Repository<MarketingList>()
                    .Update(marketingList);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "MarketingList",
                    "UPDATE",
                    marketingList.MarketingListId,
                    oldValues,
                    JsonConvert.SerializeObject(marketingList),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Marketing List Updated Successfully",
                    Data = marketingList.ListName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating marketing list");
                throw;
            }
        }

        // DELETE MARKETING LIST
        public async Task<ApiResponse<string>> DeleteMarketingList(int id)
        {
            try
            {
                var marketingList = (await _unitOfWork.Repository<MarketingList>()
                    .FindAsync(x => x.MarketingListId == id))
                    .FirstOrDefault();

                if (marketingList == null)
                    throw new CustomException("Marketing List not found.");

                string oldValues = JsonConvert.SerializeObject(marketingList);

                _unitOfWork.Repository<MarketingList>()
                    .Remove(marketingList);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "MarketingList",
                    "DELETE",
                    marketingList.MarketingListId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Marketing List Deleted Successfully",
                    Data = marketingList.ListName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting marketing list");
                throw;
            }
        }

        // GET ALL MARKETING LISTS
        public async Task<ApiResponse<List<MarketingListDto>>> GetMarketingLists()
        {
            try
            {
                var marketingLists = await _unitOfWork.Repository<MarketingList>()
                    .GetAllAsync();

                var result = marketingLists
                    .Select(x => new MarketingListDto
                    {
                        MarketingListId = x.MarketingListId,
                        CompanyId = x.CompanyId,
                        RegionId = x.RegionId,
                        ListName = x.ListName,
                        Description = x.Description,
                        ListType = x.ListType,
                        Source = x.Source,
                        TotalContacts = x.TotalContacts,
                        ActiveContacts = x.ActiveContacts,
                        Status = x.Status
                    })
                    .ToList();

                return new ApiResponse<List<MarketingListDto>>
                {
                    Success = true,
                    Message = "Marketing Lists Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting marketing lists");
                throw;
            }
        }

        // GET MARKETING LIST BY ID
        public async Task<ApiResponse<MarketingListDto>> GetMarketingListById(int id)
        {
            try
            {
                var marketingList = (await _unitOfWork.Repository<MarketingList>()
                    .FindAsync(x => x.MarketingListId == id))
                    .FirstOrDefault();

                if (marketingList == null)
                    throw new CustomException("Marketing List not found.");

                var result = new MarketingListDto
                {
                    MarketingListId = marketingList.MarketingListId,
                    CompanyId = marketingList.CompanyId,
                    RegionId = marketingList.RegionId,
                    ListName = marketingList.ListName,
                    Description = marketingList.Description,
                    ListType = marketingList.ListType,
                    Source = marketingList.Source,
                    TotalContacts = marketingList.TotalContacts,
                    ActiveContacts = marketingList.ActiveContacts,
                    Status = marketingList.Status
                };

                return new ApiResponse<MarketingListDto>
                {
                    Success = true,
                    Message = "Marketing List Retrieved Successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting marketing list by id");
                throw;
            }
        }
    }
}
