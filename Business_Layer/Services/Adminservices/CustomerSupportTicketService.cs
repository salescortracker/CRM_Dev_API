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
    public class CustomerSupportTicketService : ICustomerSupportTicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public CustomerSupportTicketService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        // CREATE 
        #region 
        public async Task<ApiResponse<string>> CreateCustomerSupportTicket(
            CustomerSupportTicketDto dto)
        {
            try
            {
                // Required field validations
                if (string.IsNullOrWhiteSpace(dto.TicketNumber))
                    throw new CustomException("Ticket Number is required.");

                if (string.IsNullOrWhiteSpace(dto.CustomerName))
                    throw new CustomException("Customer Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Subject))
                    throw new CustomException("Subject is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException("Status is required.");

                // Ticket number duplicate validation
                var duplicateTicket =
                    (await _unitOfWork.Repository<CustomerSupportTicket>()
                        .FindAsync(x =>
                            x.TicketNumber.ToLower() ==
                            dto.TicketNumber.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateTicket != null)
                    throw new CustomException(
                        "Ticket Number already exists.");

                // Due date validation
                if (dto.DueDate.HasValue &&
                    dto.DueDate.Value < DateTime.Now)
                {
                    throw new CustomException(
                        "Due Date cannot be in the past.");
                }

                var customerSupportTicket =
                    new CustomerSupportTicket
                    {
                        TicketNumber = dto.TicketNumber.Trim(),
                        CustomerName = dto.CustomerName.Trim(),
                        ContactPerson = dto.ContactPerson?.Trim(),
                        Email = dto.Email?.Trim(),
                        MobileNumber = dto.MobileNumber?.Trim(),
                        Subject = dto.Subject.Trim(),
                        Category = dto.Category?.Trim(),
                        Priority = dto.Priority?.Trim(),
                        Status = dto.Status.Trim(),
                        AssignedTo = dto.AssignedTo?.Trim(),
                        Source = dto.Source?.Trim(),
                        RelatedModule = dto.RelatedModule?.Trim(),
                        DueDate = dto.DueDate,
                        Description = dto.Description?.Trim(),
                        ResolutionNotes =
                            dto.ResolutionNotes?.Trim(),

                        CreatedDate = DateTime.Now,
                        ModifiedDate = null,
                        IsDeleted = false
                    };

                await _unitOfWork
                    .Repository<CustomerSupportTicket>()
                    .AddAsync(customerSupportTicket);

                await _unitOfWork.CompleteAsync();

                // Audit log
                await _auditService.LogAsync(
                    "CustomerSupportTicket",
                    "INSERT",
                    customerSupportTicket.TicketId,
                    "",
                    JsonConvert.SerializeObject(customerSupportTicket),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Customer support ticket created successfully.",
                    Data = customerSupportTicket.TicketNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating customer support ticket.");

                throw;
            }
        }

        // UPDATE
        public async Task<ApiResponse<string>> UpdateCustomerSupportTicket(
            CustomerSupportTicketDto dto)
        {
            try
            {
                if (dto.TicketId <= 0)
                    throw new CustomException(
                        "Valid Ticket Id is required.");

                if (string.IsNullOrWhiteSpace(dto.TicketNumber))
                    throw new CustomException("Ticket Number is required.");

                if (string.IsNullOrWhiteSpace(dto.CustomerName))
                    throw new CustomException("Customer Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Subject))
                    throw new CustomException("Subject is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException("Status is required.");

                var ticket =
                    (await _unitOfWork.Repository<CustomerSupportTicket>()
                        .FindAsync(x =>
                            x.TicketId == dto.TicketId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (ticket == null)
                    throw new CustomException(
                        "Customer support ticket not found.");

                // Duplicate Ticket Number validation
                var duplicateTicket =
                    (await _unitOfWork.Repository<CustomerSupportTicket>()
                        .FindAsync(x =>
                            x.TicketId != dto.TicketId &&
                            x.TicketNumber.ToLower() ==
                            dto.TicketNumber.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateTicket != null)
                    throw new CustomException(
                        "Ticket Number already exists.");

                if (dto.DueDate.HasValue &&
                    dto.DueDate.Value < DateTime.Now)
                {
                    throw new CustomException(
                        "Due Date cannot be in the past.");
                }

                // Capture old values for audit
                var oldValues =
                    JsonConvert.SerializeObject(ticket);

                ticket.TicketNumber = dto.TicketNumber.Trim();
                ticket.CustomerName = dto.CustomerName.Trim();
                ticket.ContactPerson = dto.ContactPerson?.Trim();
                ticket.Email = dto.Email?.Trim();
                ticket.MobileNumber = dto.MobileNumber?.Trim();
                ticket.Subject = dto.Subject.Trim();
                ticket.Category = dto.Category?.Trim();
                ticket.Priority = dto.Priority?.Trim();
                ticket.Status = dto.Status.Trim();
                ticket.AssignedTo = dto.AssignedTo?.Trim();
                ticket.Source = dto.Source?.Trim();
                ticket.RelatedModule = dto.RelatedModule?.Trim();
                ticket.DueDate = dto.DueDate;
                ticket.Description = dto.Description?.Trim();
                ticket.ResolutionNotes =
                    dto.ResolutionNotes?.Trim();

                ticket.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CustomerSupportTicket>()
                    .Update(ticket);

                await _unitOfWork.CompleteAsync();

                // Audit log
                await _auditService.LogAsync(
                    "CustomerSupportTicket",
                    "UPDATE",
                    ticket.TicketId,
                    oldValues,
                    JsonConvert.SerializeObject(ticket),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Customer support ticket updated successfully.",
                    Data = ticket.TicketNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating customer support ticket.");

                throw;
            }
        }

        // DELETE - Soft Delete
        public async Task<ApiResponse<string>> DeleteCustomerSupportTicket(
            int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Ticket Id is required.");

                var ticket =
                    (await _unitOfWork.Repository<CustomerSupportTicket>()
                        .FindAsync(x =>
                            x.TicketId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (ticket == null)
                    throw new CustomException(
                        "Customer support ticket not found.");

                var oldValues =
                    JsonConvert.SerializeObject(ticket);

                ticket.IsDeleted = true;
                ticket.ModifiedDate = DateTime.Now;

                _unitOfWork
                    .Repository<CustomerSupportTicket>()
                    .Update(ticket);

                await _unitOfWork.CompleteAsync();

                // Audit log
                await _auditService.LogAsync(
                    "CustomerSupportTicket",
                    "DELETE",
                    ticket.TicketId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Customer support ticket deleted successfully.",
                    Data = ticket.TicketNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting customer support ticket.");

                throw;
            }
        }

        // GET ALL
        public async Task<ApiResponse<List<CustomerSupportTicketDto>>>
            GetCustomerSupportTickets()
        {
            try
            {
                var tickets =
                    await _unitOfWork
                        .Repository<CustomerSupportTicket>()
                        .FindAsync(x => !x.IsDeleted);

                var result = tickets
                    .Select(x => new CustomerSupportTicketDto
                    {
                        TicketId = x.TicketId,
                        TicketNumber = x.TicketNumber,
                        CustomerName = x.CustomerName,
                        ContactPerson = x.ContactPerson,
                        Email = x.Email,
                        MobileNumber = x.MobileNumber,
                        Subject = x.Subject,
                        Category = x.Category,
                        Priority = x.Priority,
                        Status = x.Status,
                        AssignedTo = x.AssignedTo,
                        Source = x.Source,
                        RelatedModule = x.RelatedModule,
                        DueDate = x.DueDate,
                        Description = x.Description,
                        ResolutionNotes =
                            x.ResolutionNotes
                    })
                    .ToList();

                return new ApiResponse<List<CustomerSupportTicketDto>>
                {
                    Success = true,
                    Message = "Customer support tickets retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting customer support tickets.");

                throw;
            }
        }

        // GET BY ID
        public async Task<ApiResponse<CustomerSupportTicketDto>>
            GetCustomerSupportTicketById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Ticket Id is required.");

                var ticket =
                    (await _unitOfWork
                        .Repository<CustomerSupportTicket>()
                        .FindAsync(x =>
                            x.TicketId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (ticket == null)
                    throw new CustomException(
                        "Customer support ticket not found.");

                var result =
                    new CustomerSupportTicketDto
                    {
                        TicketId = ticket.TicketId,
                        TicketNumber = ticket.TicketNumber,
                        CustomerName = ticket.CustomerName,
                        ContactPerson = ticket.ContactPerson,
                        Email = ticket.Email,
                        MobileNumber = ticket.MobileNumber,
                        Subject = ticket.Subject,
                        Category = ticket.Category,
                        Priority = ticket.Priority,
                        Status = ticket.Status,
                        AssignedTo = ticket.AssignedTo,
                        Source = ticket.Source,
                        RelatedModule = ticket.RelatedModule,
                        DueDate = ticket.DueDate,
                        Description = ticket.Description,
                        ResolutionNotes =
                            ticket.ResolutionNotes
                    };

                return new ApiResponse<CustomerSupportTicketDto>
                {
                    Success = true,
                    Message = "Customer support ticket retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting customer support ticket by id.");

                throw;

            }
        }

        #endregion


        // CREATE
        public async Task<ApiResponse<string>> CreateTicketCategory(
            TicketCategoryDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CategoryName))
                    throw new CustomException(
                        "Category Name is required.");

                if (string.IsNullOrWhiteSpace(dto.CategoryCode))
                    throw new CustomException(
                        "Category Code is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                if (dto.SlaHours.HasValue &&
                    dto.SlaHours.Value < 0)
                {
                    throw new CustomException(
                        "SLA Hours cannot be negative.");
                }

                // Duplicate Category Name
                var duplicateName =
                    (await _unitOfWork
                        .Repository<TicketCategory>()
                        .FindAsync(x =>
                            x.CategoryName.ToLower() ==
                            dto.CategoryName.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateName != null)
                    throw new CustomException(
                        "Category Name already exists.");

                // Duplicate Category Code
                var duplicateCode =
                    (await _unitOfWork
                        .Repository<TicketCategory>()
                        .FindAsync(x =>
                            x.CategoryCode.ToLower() ==
                            dto.CategoryCode.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateCode != null)
                    throw new CustomException(
                        "Category Code already exists.");

                var ticketCategory = new TicketCategory
                {
                    CategoryName = dto.CategoryName.Trim(),
                    CategoryCode = dto.CategoryCode.Trim(),
                    ParentCategory = dto.ParentCategory?.Trim(),
                    CategoryType = dto.CategoryType?.Trim(),
                    Priority = dto.Priority?.Trim(),
                    SlaHours = dto.SlaHours,
                    AssignedTeam = dto.AssignedTeam?.Trim(),
                    Status = dto.Status.Trim(),
                    Description = dto.Description?.Trim(),

                    IsDeleted = false,

                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork
                    .Repository<TicketCategory>()
                    .AddAsync(ticketCategory);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "TicketCategory",
                    "INSERT",
                    ticketCategory.CategoryId,
                    "",
                    JsonConvert.SerializeObject(ticketCategory),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Ticket category created successfully.",
                    Data = ticketCategory.CategoryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating ticket category.");

                throw;
            }
        }

        // UPDATE
        public async Task<ApiResponse<string>> UpdateTicketCategory(
            TicketCategoryDto dto)
        {
            try
            {
                if (dto.CategoryId <= 0)
                    throw new CustomException(
                        "Valid Category Id is required.");

                if (string.IsNullOrWhiteSpace(dto.CategoryName))
                    throw new CustomException(
                        "Category Name is required.");

                if (string.IsNullOrWhiteSpace(dto.CategoryCode))
                    throw new CustomException(
                        "Category Code is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                if (dto.SlaHours.HasValue &&
                    dto.SlaHours.Value < 0)
                {
                    throw new CustomException(
                        "SLA Hours cannot be negative.");
                }

                var ticketCategory =
                    (await _unitOfWork
                        .Repository<TicketCategory>()
                        .FindAsync(x =>
                            x.CategoryId == dto.CategoryId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (ticketCategory == null)
                    throw new CustomException(
                        "Ticket category not found.");

                // Duplicate Category Name
                var duplicateName =
                    (await _unitOfWork
                        .Repository<TicketCategory>()
                        .FindAsync(x =>
                            x.CategoryId != dto.CategoryId &&
                            x.CategoryName.ToLower() ==
                            dto.CategoryName.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateName != null)
                    throw new CustomException(
                        "Category Name already exists.");

                // Duplicate Category Code
                var duplicateCode =
                    (await _unitOfWork
                        .Repository<TicketCategory>()
                        .FindAsync(x =>
                            x.CategoryId != dto.CategoryId &&
                            x.CategoryCode.ToLower() ==
                            dto.CategoryCode.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateCode != null)
                    throw new CustomException(
                        "Category Code already exists.");

                var oldValues =
                    JsonConvert.SerializeObject(ticketCategory);

                ticketCategory.CategoryName =
                    dto.CategoryName.Trim();

                ticketCategory.CategoryCode =
                    dto.CategoryCode.Trim();

                ticketCategory.ParentCategory =
                    dto.ParentCategory?.Trim();

                ticketCategory.CategoryType =
                    dto.CategoryType?.Trim();

                ticketCategory.Priority =
                    dto.Priority?.Trim();

                ticketCategory.SlaHours =
                    dto.SlaHours;

                ticketCategory.AssignedTeam =
                    dto.AssignedTeam?.Trim();

                ticketCategory.Status =
                    dto.Status.Trim();

                ticketCategory.Description =
                    dto.Description?.Trim();

                ticketCategory.ModifiedBy =
                    _currentUserService.UserId;

                ticketCategory.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<TicketCategory>()
                    .Update(ticketCategory);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "TicketCategory",
                    "UPDATE",
                    ticketCategory.CategoryId,
                    oldValues,
                    JsonConvert.SerializeObject(ticketCategory),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Ticket category updated successfully.",
                    Data = ticketCategory.CategoryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating ticket category.");

                throw;
            }
        }

        // DELETE - Soft Delete
        public async Task<ApiResponse<string>> DeleteTicketCategory(
            int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Category Id is required.");

                var ticketCategory =
                    (await _unitOfWork
                        .Repository<TicketCategory>()
                        .FindAsync(x =>
                            x.CategoryId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (ticketCategory == null)
                    throw new CustomException(
                        "Ticket category not found.");

                var oldValues =
                    JsonConvert.SerializeObject(ticketCategory);

                ticketCategory.IsDeleted = true;

                ticketCategory.ModifiedBy =
                    _currentUserService.UserId;

                ticketCategory.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<TicketCategory>()
                    .Update(ticketCategory);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "TicketCategory",
                    "DELETE",
                    ticketCategory.CategoryId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Ticket category deleted successfully.",
                    Data = ticketCategory.CategoryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting ticket category.");

                throw;
            }
        }

        // GET ALL
        public async Task<ApiResponse<List<TicketCategoryDto>>>
            GetTicketCategories()
        {
            try
            {
                var categories =
                    await _unitOfWork
                        .Repository<TicketCategory>()
                        .FindAsync(x =>
                            !x.IsDeleted &&
                            x.CreatedBy == _currentUserService.UserId);

                var result = categories
                    .Select(x => new TicketCategoryDto
                    {
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryName,
                        CategoryCode = x.CategoryCode,
                        ParentCategory = x.ParentCategory,
                        CategoryType = x.CategoryType,
                        Priority = x.Priority,
                        SlaHours = x.SlaHours,
                        AssignedTeam = x.AssignedTeam,
                        Status = x.Status,
                        Description = x.Description
                    })
                    .ToList();

                return new ApiResponse<List<TicketCategoryDto>>
                {
                    Success = true,
                    Message =
                        "Ticket categories retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting ticket categories.");

                throw;
            }
        }

        // GET BY ID
        public async Task<ApiResponse<TicketCategoryDto>>
            GetTicketCategoryById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Category Id is required.");

                var ticketCategory =
                    (await _unitOfWork
                        .Repository<TicketCategory>()
                        .FindAsync(x =>
                            x.CategoryId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (ticketCategory == null)
                    throw new CustomException(
                        "Ticket category not found.");

                var result = new TicketCategoryDto
                {
                    CategoryId = ticketCategory.CategoryId,
                    CategoryName = ticketCategory.CategoryName,
                    CategoryCode = ticketCategory.CategoryCode,
                    ParentCategory = ticketCategory.ParentCategory,
                    CategoryType = ticketCategory.CategoryType,
                    Priority = ticketCategory.Priority,
                    SlaHours = ticketCategory.SlaHours,
                    AssignedTeam = ticketCategory.AssignedTeam,
                    Status = ticketCategory.Status,
                    Description = ticketCategory.Description
                };

                return new ApiResponse<TicketCategoryDto>
                {
                    Success = true,
                    Message =
                        "Ticket category retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting ticket category by id.");

                throw;
            }

        }

        // CREATE
        public async Task<ApiResponse<string>> CreateKnowledgeBase(
            KnowledgeBaseDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.ArticleTitle))
                    throw new CustomException(
                        "Article Title is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                if (string.IsNullOrWhiteSpace(dto.Visibility))
                    throw new CustomException(
                        "Visibility is required.");

                if (string.IsNullOrWhiteSpace(dto.Version))
                    throw new CustomException(
                        "Version is required.");

                // Duplicate Article Title validation
                var duplicateArticle =
                    (await _unitOfWork
                        .Repository<KnowledgeBase>()
                        .FindAsync(x =>
                            x.ArticleTitle.ToLower() ==
                            dto.ArticleTitle.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateArticle != null)
                    throw new CustomException(
                        "Article Title already exists.");

                var knowledgeBase = new KnowledgeBase
                {
                    ArticleTitle =
                        dto.ArticleTitle.Trim(),

                    Category =
                        dto.Category?.Trim(),

                    Status =
                        dto.Status.Trim(),

                    Keywords =
                        dto.Keywords?.Trim(),

                    Visibility =
                        dto.Visibility.Trim(),

                    Author =
                        dto.Author?.Trim(),

                    Version =
                        dto.Version.Trim(),

                    LastUpdated =
                        dto.LastUpdated ?? DateTime.Now,

                    Attachment =
                        dto.Attachment?.Trim(),

                    UploadType =
                        dto.UploadType?.Trim(),

                    Summary =
                        dto.Summary?.Trim(),

                    ArticleContent =
                        dto.ArticleContent,

                    IsActive =
                        dto.IsActive,

                    CreatedBy =
                        _currentUserService.UserId,

                    CreatedDate =
                        DateTime.Now,

                    ModifiedBy = null,

                    ModifiedAt = null,

                    IsDeleted = false
                };

                await _unitOfWork
                    .Repository<KnowledgeBase>()
                    .AddAsync(knowledgeBase);

                await _unitOfWork.CompleteAsync();

                // Audit Log
                await _auditService.LogAsync(
                    "KnowledgeBase",
                    "INSERT",
                    knowledgeBase.ArticleId,
                    "",
                    JsonConvert.SerializeObject(knowledgeBase),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Knowledge base article created successfully.",
                    Data =
                        knowledgeBase.ArticleTitle
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating knowledge base article.");

                throw;
            }
        }

        // UPDATE
        public async Task<ApiResponse<string>> UpdateKnowledgeBase(
            KnowledgeBaseDto dto)
        {
            try
            {
                if (dto.ArticleId <= 0)
                    throw new CustomException(
                        "Valid Article Id is required.");

                if (string.IsNullOrWhiteSpace(dto.ArticleTitle))
                    throw new CustomException(
                        "Article Title is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                if (string.IsNullOrWhiteSpace(dto.Visibility))
                    throw new CustomException(
                        "Visibility is required.");

                if (string.IsNullOrWhiteSpace(dto.Version))
                    throw new CustomException(
                        "Version is required.");

                var knowledgeBase =
                    (await _unitOfWork
                        .Repository<KnowledgeBase>()
                        .FindAsync(x =>
                            x.ArticleId == dto.ArticleId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (knowledgeBase == null)
                    throw new CustomException(
                        "Knowledge base article not found.");

                // Duplicate Article Title validation
                var duplicateArticle =
                    (await _unitOfWork
                        .Repository<KnowledgeBase>()
                        .FindAsync(x =>
                            x.ArticleId != dto.ArticleId &&
                            x.ArticleTitle.ToLower() ==
                            dto.ArticleTitle.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateArticle != null)
                    throw new CustomException(
                        "Article Title already exists.");

                // Old values for audit
                var oldValues =
                    JsonConvert.SerializeObject(knowledgeBase);

                knowledgeBase.ArticleTitle =
                    dto.ArticleTitle.Trim();

                knowledgeBase.Category =
                    dto.Category?.Trim();

                knowledgeBase.Status =
                    dto.Status.Trim();

                knowledgeBase.Keywords =
                    dto.Keywords?.Trim();

                knowledgeBase.Visibility =
                    dto.Visibility.Trim();

                knowledgeBase.Author =
                    dto.Author?.Trim();

                knowledgeBase.Version =
                    dto.Version.Trim();

                knowledgeBase.LastUpdated =
                    dto.LastUpdated ?? DateTime.Now;

                knowledgeBase.Attachment =
                    dto.Attachment?.Trim();

                knowledgeBase.UploadType =
                    dto.UploadType?.Trim();

                knowledgeBase.Summary =
                    dto.Summary?.Trim();

                knowledgeBase.ArticleContent =
                    dto.ArticleContent;

                knowledgeBase.IsActive =
                    dto.IsActive;

                knowledgeBase.ModifiedBy =
                    _currentUserService.UserId;

                knowledgeBase.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<KnowledgeBase>()
                    .Update(knowledgeBase);

                await _unitOfWork.CompleteAsync();

                // Audit Log
                await _auditService.LogAsync(
                    "KnowledgeBase",
                    "UPDATE",
                    knowledgeBase.ArticleId,
                    oldValues,
                    JsonConvert.SerializeObject(knowledgeBase),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Knowledge base article updated successfully.",
                    Data =
                        knowledgeBase.ArticleTitle
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating knowledge base article.");

                throw;
            }
        }

        // DELETE - SOFT DELETE
        public async Task<ApiResponse<string>> DeleteKnowledgeBase(
            int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Article Id is required.");

                var knowledgeBase =
                    (await _unitOfWork
                        .Repository<KnowledgeBase>()
                        .FindAsync(x =>
                            x.ArticleId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (knowledgeBase == null)
                    throw new CustomException(
                        "Knowledge base article not found.");

                var oldValues =
                    JsonConvert.SerializeObject(knowledgeBase);

                knowledgeBase.IsDeleted = true;

                knowledgeBase.IsActive = false;

                knowledgeBase.ModifiedBy =
                    _currentUserService.UserId;

                knowledgeBase.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<KnowledgeBase>()
                    .Update(knowledgeBase);

                await _unitOfWork.CompleteAsync();

                // Audit Log
                await _auditService.LogAsync(
                    "KnowledgeBase",
                    "DELETE",
                    knowledgeBase.ArticleId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Knowledge base article deleted successfully.",
                    Data =
                        knowledgeBase.ArticleTitle
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting knowledge base article.");

                throw;
            }
        }

        // GET ALL
        public async Task<ApiResponse<List<KnowledgeBaseDto>>>
            GetKnowledgeBases()
        {
            try
            {
                var articles =
                    await _unitOfWork
                        .Repository<KnowledgeBase>()
                        .FindAsync(x =>
                            !x.IsDeleted &&
                            x.CreatedBy == _currentUserService.UserId);

                var result = articles
                    .Select(x => new KnowledgeBaseDto
                    {
                        ArticleId =
                            x.ArticleId,

                        ArticleTitle =
                            x.ArticleTitle,

                        Category =
                            x.Category,

                        Status =
                            x.Status,

                        Keywords =
                            x.Keywords,

                        Visibility =
                            x.Visibility,

                        Author =
                            x.Author,

                        Version =
                            x.Version,

                        LastUpdated =
                            x.LastUpdated,

                        Attachment =
                            x.Attachment,

                        UploadType =
                            x.UploadType,

                        Summary =
                            x.Summary,

                        ArticleContent =
                            x.ArticleContent,

                        IsActive =
                            x.IsActive
                    })
                    .ToList();

                return new ApiResponse<List<KnowledgeBaseDto>>
                {
                    Success = true,
                    Message =
                        "Knowledge base articles retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting knowledge base articles.");

                throw;
            }
        }

        // GET BY ID
        public async Task<ApiResponse<KnowledgeBaseDto>>
            GetKnowledgeBaseById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Article Id is required.");

                var knowledgeBase =
                    (await _unitOfWork
                        .Repository<KnowledgeBase>()
                        .FindAsync(x =>
                            x.ArticleId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (knowledgeBase == null)
                    throw new CustomException(
                        "Knowledge base article not found.");

                var result = new KnowledgeBaseDto
                {
                    ArticleId =
                        knowledgeBase.ArticleId,

                    ArticleTitle =
                        knowledgeBase.ArticleTitle,

                    Category =
                        knowledgeBase.Category,

                    Status =
                        knowledgeBase.Status,

                    Keywords =
                        knowledgeBase.Keywords,

                    Visibility =
                        knowledgeBase.Visibility,

                    Author =
                        knowledgeBase.Author,

                    Version =
                        knowledgeBase.Version,

                    LastUpdated =
                        knowledgeBase.LastUpdated,

                    Attachment =
                        knowledgeBase.Attachment,

                    UploadType =
                        knowledgeBase.UploadType,

                    Summary =
                        knowledgeBase.Summary,

                    ArticleContent =
                        knowledgeBase.ArticleContent,

                    IsActive =
                        knowledgeBase.IsActive
                };

                return new ApiResponse<KnowledgeBaseDto>
                {
                    Success = true,
                    Message =
                        "Knowledge base article retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting knowledge base article by id.");

                throw;
            }



        }
        // CREATE
        public async Task<ApiResponse<string>> CreateFaqManagement(
            FaqManagementDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Faqtitle))
                    throw new CustomException(
                        "FAQ Title is required.");

                if (string.IsNullOrWhiteSpace(dto.Faqcode))
                    throw new CustomException(
                        "FAQ Code is required.");

                if (string.IsNullOrWhiteSpace(dto.Question))
                    throw new CustomException(
                        "Question is required.");

                if (string.IsNullOrWhiteSpace(dto.Answer))
                    throw new CustomException(
                        "Answer is required.");

                if (string.IsNullOrWhiteSpace(dto.Visibility))
                    throw new CustomException(
                        "Visibility is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                if (dto.DisplayOrder.HasValue &&
                    dto.DisplayOrder.Value < 0)
                {
                    throw new CustomException(
                        "Display Order cannot be negative.");
                }

                // Duplicate FAQ Title
                var duplicateTitle =
                    (await _unitOfWork
                        .Repository<Faqmanagement>()
                        .FindAsync(x =>
                            x.Faqtitle.ToLower() ==
                            dto.Faqtitle.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateTitle != null)
                    throw new CustomException(
                        "FAQ Title already exists.");

                // Duplicate FAQ Code
                var duplicateCode =
                    (await _unitOfWork
                        .Repository<Faqmanagement>()
                        .FindAsync(x =>
                            x.Faqcode.ToLower() ==
                            dto.Faqcode.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateCode != null)
                    throw new CustomException(
                        "FAQ Code already exists.");

                var faq = new Faqmanagement
                {
                    Faqtitle = dto.Faqtitle.Trim(),

                    Faqcode = dto.Faqcode.Trim(),

                    Category = dto.Category?.Trim(),

                    Question = dto.Question.Trim(),

                    Answer = dto.Answer.Trim(),

                    Visibility = dto.Visibility.Trim(),

                    DisplayOrder = dto.DisplayOrder,

                    Status = dto.Status.Trim(),

                    CreatedBy = _currentUserService.UserId,

                    CreatedDate = DateTime.Now,

                    ModifiedBy = null,

                    ModifiedAt = null,

                    IsDeleted = false
                };

                await _unitOfWork
                    .Repository<Faqmanagement>()
                    .AddAsync(faq);

                await _unitOfWork.CompleteAsync();

                // Audit Log
                await _auditService.LogAsync(
                    "Faqmanagement",
                    "INSERT",
                    faq.Faqid,
                    "",
                    JsonConvert.SerializeObject(faq),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "FAQ created successfully.",
                    Data = faq.Faqtitle
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating FAQ.");

                throw;
            }
        }

        // UPDATE
        public async Task<ApiResponse<string>> UpdateFaqManagement(
            FaqManagementDto dto)
        {
            try
            {
                if (dto.Faqid <= 0)
                    throw new CustomException(
                        "Valid FAQ Id is required.");

                if (string.IsNullOrWhiteSpace(dto.Faqtitle))
                    throw new CustomException(
                        "FAQ Title is required.");

                if (string.IsNullOrWhiteSpace(dto.Faqcode))
                    throw new CustomException(
                        "FAQ Code is required.");

                if (string.IsNullOrWhiteSpace(dto.Question))
                    throw new CustomException(
                        "Question is required.");

                if (string.IsNullOrWhiteSpace(dto.Answer))
                    throw new CustomException(
                        "Answer is required.");

                if (string.IsNullOrWhiteSpace(dto.Visibility))
                    throw new CustomException(
                        "Visibility is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                if (dto.DisplayOrder.HasValue &&
                    dto.DisplayOrder.Value < 0)
                {
                    throw new CustomException(
                        "Display Order cannot be negative.");
                }

                var faq =
                    (await _unitOfWork
                        .Repository<Faqmanagement>()
                        .FindAsync(x =>
                            x.Faqid == dto.Faqid &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (faq == null)
                    throw new CustomException(
                        "FAQ not found.");

                // Duplicate FAQ Title
                var duplicateTitle =
                    (await _unitOfWork
                        .Repository<Faqmanagement>()
                        .FindAsync(x =>
                            x.Faqid != dto.Faqid &&
                            x.Faqtitle.ToLower() ==
                            dto.Faqtitle.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateTitle != null)
                    throw new CustomException(
                        "FAQ Title already exists.");

                // Duplicate FAQ Code
                var duplicateCode =
                    (await _unitOfWork
                        .Repository<Faqmanagement>()
                        .FindAsync(x =>
                            x.Faqid != dto.Faqid &&
                            x.Faqcode.ToLower() ==
                            dto.Faqcode.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateCode != null)
                    throw new CustomException(
                        "FAQ Code already exists.");

                var oldValues =
                    JsonConvert.SerializeObject(faq);

                faq.Faqtitle =
                    dto.Faqtitle.Trim();

                faq.Faqcode =
                    dto.Faqcode.Trim();

                faq.Category =
                    dto.Category?.Trim();

                faq.Question =
                    dto.Question.Trim();

                faq.Answer =
                    dto.Answer.Trim();

                faq.Visibility =
                    dto.Visibility.Trim();

                faq.DisplayOrder =
                    dto.DisplayOrder;

                faq.Status =
                    dto.Status.Trim();

                faq.ModifiedBy =
                    _currentUserService.UserId;

                faq.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<Faqmanagement>()
                    .Update(faq);

                await _unitOfWork.CompleteAsync();

                // Audit Log
                await _auditService.LogAsync(
                    "Faqmanagement",
                    "UPDATE",
                    faq.Faqid,
                    oldValues,
                    JsonConvert.SerializeObject(faq),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "FAQ updated successfully.",
                    Data = faq.Faqtitle
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating FAQ.");

                throw;
            }
        }

        // DELETE - SOFT DELETE
        public async Task<ApiResponse<string>> DeleteFaqManagement(
            int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid FAQ Id is required.");

                var faq =
                    (await _unitOfWork
                        .Repository<Faqmanagement>()
                        .FindAsync(x =>
                            x.Faqid == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (faq == null)
                    throw new CustomException(
                        "FAQ not found.");

                var oldValues =
                    JsonConvert.SerializeObject(faq);

                faq.IsDeleted = true;

                faq.ModifiedBy =
                    _currentUserService.UserId;

                faq.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<Faqmanagement>()
                    .Update(faq);

                await _unitOfWork.CompleteAsync();

                // Audit Log
                await _auditService.LogAsync(
                    "Faqmanagement",
                    "DELETE",
                    faq.Faqid,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "FAQ deleted successfully.",
                    Data = faq.Faqtitle
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting FAQ.");

                throw;
            }
        }

        // GET ALL
        public async Task<ApiResponse<List<FaqManagementDto>>>
            GetFaqManagements()
        {
            try
            {
                var faqs =
                    await _unitOfWork
                        .Repository<Faqmanagement>()
                        .FindAsync(x =>
                            !x.IsDeleted &&
                            x.CreatedBy == _currentUserService.UserId);

                var result = faqs
                    .Select(x => new FaqManagementDto
                    {
                        Faqid = x.Faqid,

                        Faqtitle = x.Faqtitle,

                        Faqcode = x.Faqcode,

                        Category = x.Category,

                        Question = x.Question,

                        Answer = x.Answer,

                        Visibility = x.Visibility,

                        DisplayOrder = x.DisplayOrder,

                        Status = x.Status
                    })
                    .ToList();

                return new ApiResponse<List<FaqManagementDto>>
                {
                    Success = true,
                    Message =
                        "FAQs retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting FAQs.");

                throw;
            }
        }

        // GET BY ID
        public async Task<ApiResponse<FaqManagementDto>>
            GetFaqManagementById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid FAQ Id is required.");

                var faq =
                    (await _unitOfWork
                        .Repository<Faqmanagement>()
                        .FindAsync(x =>
                            x.Faqid == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (faq == null)
                    throw new CustomException(
                        "FAQ not found.");

                var result = new FaqManagementDto
                {
                    Faqid = faq.Faqid,

                    Faqtitle = faq.Faqtitle,

                    Faqcode = faq.Faqcode,

                    Category = faq.Category,

                    Question = faq.Question,

                    Answer = faq.Answer,

                    Visibility = faq.Visibility,

                    DisplayOrder = faq.DisplayOrder,

                    Status = faq.Status
                };

                return new ApiResponse<FaqManagementDto>
                {
                    Success = true,
                    Message =
                        "FAQ retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting FAQ by id.");

                throw;
            }
        }
    }
}

