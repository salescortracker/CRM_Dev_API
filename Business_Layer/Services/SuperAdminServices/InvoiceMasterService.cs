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
using System.Threading.Tasks;

namespace Business_Layer.Services.SuperAdminServices
{
    public class InvoiceMasterService : IInvoiceMasterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public InvoiceMasterService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        #region CREATE

        public async Task<ApiResponse<string>> CreateInvoice(InvoiceMasterDto dto)
        {
            try
            {
                if (dto.OrganizationId <= 0)
                    throw new CustomException("Company is required.");

                if (dto.PlanId <= 0)
                    throw new CustomException("Plan is required.");

                string invoiceNumber = string.IsNullOrWhiteSpace(dto.InvoiceNumber)
                    ? "INV-" + DateTime.Now.Ticks
                    : dto.InvoiceNumber;

                var duplicate = await _unitOfWork.Repository<InvoiceMaster>()
                    .FindAsync(x =>
                        !x.IsDeleted &&
                        x.InvoiceNumber.ToLower() == invoiceNumber.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Invoice Number already exists.");

                InvoiceMaster invoice = new InvoiceMaster
                {
                    InvoiceNumber = invoiceNumber,
                    OrganizationId = dto.OrganizationId,
                    CompanyEmail = dto.CompanyEmail,
                    CompanyPhone = dto.CompanyPhone,
                    PlanId = dto.PlanId,
                    BillingCycle = dto.BillingCycle,
                    InvoiceDate = dto.InvoiceDate == default ? DateTime.Now : dto.InvoiceDate,
                    DueDate = dto.DueDate,
                    Description = dto.Description,
                    Quantity = dto.Quantity,
                    UnitPrice = dto.UnitPrice,
                    SubTotal = dto.SubTotal,
                    GstPercentage = dto.GstPercentage,
                    TaxAmount = dto.TaxAmount,
                    Discount = dto.Discount,
                    TotalAmount = dto.TotalAmount,
                    PaidAmount = dto.PaidAmount,
                    BalanceAmount = dto.TotalAmount - dto.PaidAmount,
                    PaymentStatus = dto.PaymentStatus,
                    PaymentMethod = dto.PaymentMethod,
                    TransactionId = dto.TransactionId,
                    Currency = dto.Currency,
                    Notes = dto.Notes,
                    IsActive = dto.IsActive,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<InvoiceMaster>().AddAsync(invoice);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Invoice",
                    "INSERT",
                    invoice.InvoiceId,
                    "",
                    JsonConvert.SerializeObject(invoice),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Invoice Created Successfully.",
                    Data = invoice.InvoiceNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating Invoice");
                throw;
            }
        }

        #endregion

        #region UPDATE

        public async Task<ApiResponse<string>> UpdateInvoice(InvoiceMasterDto dto)
        {
            try
            {
                var invoice = (await _unitOfWork.Repository<InvoiceMaster>()
                    .FindAsync(x => x.InvoiceId == dto.InvoiceId))
                    .FirstOrDefault();

                if (invoice == null)
                    throw new CustomException("Invoice not found.");

                var duplicate = await _unitOfWork.Repository<InvoiceMaster>()
                    .FindAsync(x =>
                        x.InvoiceId != dto.InvoiceId &&
                        !x.IsDeleted &&
                        x.InvoiceNumber.ToLower() == dto.InvoiceNumber.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Invoice Number already exists.");

                string oldValues = JsonConvert.SerializeObject(invoice);

                invoice.InvoiceNumber = dto.InvoiceNumber;
                invoice.OrganizationId = dto.OrganizationId;
                invoice.CompanyEmail = dto.CompanyEmail;
                invoice.CompanyPhone = dto.CompanyPhone;
                invoice.PlanId = dto.PlanId;
                invoice.BillingCycle = dto.BillingCycle;
                invoice.InvoiceDate = dto.InvoiceDate;
                invoice.DueDate = dto.DueDate;
                invoice.Description = dto.Description;
                invoice.Quantity = dto.Quantity;
                invoice.UnitPrice = dto.UnitPrice;
                invoice.SubTotal = dto.SubTotal;
                invoice.GstPercentage = dto.GstPercentage;
                invoice.TaxAmount = dto.TaxAmount;
                invoice.Discount = dto.Discount;
                invoice.TotalAmount = dto.TotalAmount;
                invoice.PaidAmount = dto.PaidAmount;
                invoice.BalanceAmount = dto.TotalAmount - dto.PaidAmount;
                invoice.PaymentStatus = dto.PaymentStatus;
                invoice.PaymentMethod = dto.PaymentMethod;
                invoice.TransactionId = dto.TransactionId;
                invoice.Currency = dto.Currency;
                invoice.Notes = dto.Notes;
                invoice.IsActive = dto.IsActive;

                invoice.ModifiedBy = _currentUserService.UserId;
                invoice.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<InvoiceMaster>().Update(invoice);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Invoice",
                    "UPDATE",
                    invoice.InvoiceId,
                    oldValues,
                    JsonConvert.SerializeObject(invoice),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Invoice Updated Successfully.",
                    Data = invoice.InvoiceNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating Invoice");
                throw;
            }
        }

        #endregion

        #region DELETE

        public async Task<ApiResponse<string>> DeleteInvoice(int id)
        {
            try
            {
                var invoice = (await _unitOfWork.Repository<InvoiceMaster>()
                    .FindAsync(x => x.InvoiceId == id))
                    .FirstOrDefault();

                if (invoice == null)
                    throw new CustomException("Invoice not found.");

                string oldValues = JsonConvert.SerializeObject(invoice);

                invoice.IsDeleted = true;
                invoice.ModifiedBy = _currentUserService.UserId;
                invoice.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<InvoiceMaster>().Update(invoice);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Invoice",
                    "DELETE",
                    invoice.InvoiceId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Invoice Deleted Successfully.",
                    Data = invoice.InvoiceNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting Invoice");
                throw;
            }
        }

        #endregion

        #region GET ALL

        public async Task<ApiResponse<List<InvoiceMasterDto>>> GetInvoices()
        {
            var organizations = await _unitOfWork.Repository<OrganizationDatum>().GetAllAsync();

            var plans = await _unitOfWork.Repository<SubscriptionPlanMaster>().GetAllAsync();

            var invoices = await _unitOfWork.Repository<InvoiceMaster>().GetAllAsync();

            var result = (
                from x in invoices

                join o in organizations
                    on x.OrganizationId equals o.OrganizationId

                join p in plans
                    on x.PlanId equals p.PlanId

                where !x.IsDeleted && x.CreatedBy == _currentUserService.UserId

                select new InvoiceMasterDto
                {
                    InvoiceId = x.InvoiceId,
                    InvoiceNumber = x.InvoiceNumber,

                    OrganizationId = x.OrganizationId,
                    OrganizationName = o.OrganizationName,

                    CompanyEmail = x.CompanyEmail,
                    CompanyPhone = x.CompanyPhone,

                    PlanId = x.PlanId,
                    PlanName = p.PlanName,

                    BillingCycle = x.BillingCycle,
                    InvoiceDate = x.InvoiceDate,
                    DueDate = x.DueDate,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    SubTotal = x.SubTotal,
                    GstPercentage = x.GstPercentage,
                    TaxAmount = x.TaxAmount,
                    Discount = x.Discount,
                    TotalAmount = x.TotalAmount,
                    PaidAmount = x.PaidAmount,
                    BalanceAmount = x.BalanceAmount,
                    PaymentStatus = x.PaymentStatus,
                    PaymentMethod = x.PaymentMethod,
                    TransactionId = x.TransactionId,
                    Currency = x.Currency,
                    Notes = x.Notes,
                    IsActive = x.IsActive
                }
            )
            .OrderByDescending(x => x.InvoiceId)
            .ToList();

            return new ApiResponse<List<InvoiceMasterDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        #endregion

        #region GET BY ID

        public async Task<ApiResponse<InvoiceMasterDto>> GetInvoiceById(int id)
        {
            var invoice = (await _unitOfWork.Repository<InvoiceMaster>()
                .FindAsync(x => x.InvoiceId == id && !x.IsDeleted))
                .FirstOrDefault();

            if (invoice == null)
                throw new CustomException("Invoice not found.");

            return new ApiResponse<InvoiceMasterDto>
            {
                Success = true,
                Message = "Success",
                Data = new InvoiceMasterDto
                {
                    InvoiceId = invoice.InvoiceId,
                    InvoiceNumber = invoice.InvoiceNumber,
                    OrganizationId = invoice.OrganizationId,
                    CompanyEmail = invoice.CompanyEmail,
                    CompanyPhone = invoice.CompanyPhone,
                    PlanId = invoice.PlanId,
                    BillingCycle = invoice.BillingCycle,
                    InvoiceDate = invoice.InvoiceDate,
                    DueDate = invoice.DueDate,
                    Description = invoice.Description,
                    Quantity = invoice.Quantity,
                    UnitPrice = invoice.UnitPrice,
                    SubTotal = invoice.SubTotal,
                    GstPercentage = invoice.GstPercentage,
                    TaxAmount = invoice.TaxAmount,
                    Discount = invoice.Discount,
                    TotalAmount = invoice.TotalAmount,
                    PaidAmount = invoice.PaidAmount,
                    BalanceAmount = invoice.BalanceAmount,
                    PaymentStatus = invoice.PaymentStatus,
                    PaymentMethod = invoice.PaymentMethod,
                    TransactionId = invoice.TransactionId,
                    Currency = invoice.Currency,
                    Notes = invoice.Notes,
                    IsActive = invoice.IsActive
                }
            };
        }

        #endregion
    }
}
