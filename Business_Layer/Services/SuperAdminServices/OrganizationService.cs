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
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public OrganizationService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        #region CREATE ORGANIZATION

        public async Task<ApiResponse<string>> CreateOrganization(OrganizationDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.OrganizationName))
                    throw new CustomException("Organization Name is required.");

                if (string.IsNullOrWhiteSpace(dto.OrganizationCode))
                    throw new CustomException("Organization Code is required.");

                var existing = await _unitOfWork
                    .Repository<OrganizationDatum>()
                    .FindAsync(x =>
                        x.OrganizationCode.ToLower() == dto.OrganizationCode.ToLower());

                if (existing.Any())
                    throw new CustomException("Organization Code already exists.");

                var nameExists = await _unitOfWork
                    .Repository<OrganizationDatum>()
                    .FindAsync(x =>
                        x.OrganizationName.ToLower() == dto.OrganizationName.ToLower());

                if (nameExists.Any())
                    throw new CustomException("Organization Name already exists.");

                OrganizationDatum organization = new OrganizationDatum
                {
                    OrganizationCode = dto.OrganizationCode,
                    OrganizationName = dto.OrganizationName,
                    LegalName = dto.LegalName,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Website = dto.Website,

                    Gstnumber = dto.GSTNumber,
                    Pannumber = dto.PANNumber,

                    AddressLine1 = dto.AddressLine1,
                    AddressLine2 = dto.AddressLine2,
                    City = dto.City,
                    State = dto.State,
                    Country = dto.Country,
                    PostalCode = dto.PostalCode,

                    LogoUrl = dto.LogoUrl,

                  //  Status = dto.Status,

                    Domain = dto.Domain,

                    ContactPerson = dto.ContactPerson,
                    ContactEmail = dto.ContactEmail,
                    ContactMobile = dto.ContactMobile,

                    TimeZone = dto.TimeZone,
                    CurrencyCode = dto.CurrencyCode,

                    SubscriptionStartDate = dto.SubscriptionStartDate,
                    RenewalDate = dto.RenewalDate,

                    PlanId = dto.PlanId,

                    MaxUsers = dto.MaxUsers,
                    MaxStorageGb = dto.MaxStorageGB,
                    StorageUsedGb = dto.StorageUsedGB,

                    MonthlyRevenue = dto.MonthlyRevenue,

                    BrandColor = dto.BrandColor,
                    Industry = dto.Industry,

                    Features = dto.Features,

                    //CreatedOn = DateTime.Now,
                    CreatedAt = DateTime.Now,

                    CreatedBy = _currentUserService.UserId,
                    UserId = _currentUserService.UserId,
                    //CompanyId = _currentUserService.CompanyId,
                    //RegionId = _currentUserService.RegionId
                };

                await _unitOfWork
                    .Repository<OrganizationDatum>()
                    .AddAsync(organization);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Organization",
                    "INSERT",
                    organization.OrganizationId,
                    "",
                    JsonConvert.SerializeObject(organization),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Organization Created Successfully.",
                    Data = organization.OrganizationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating Organization");
                throw;
            }
        }

        #endregion
        #region UPDATE ORGANIZATION

        public async Task<ApiResponse<string>> UpdateOrganization(OrganizationDto dto)
        {
            try
            {
                var organization = (await _unitOfWork.Repository<OrganizationDatum>()
                    .FindAsync(x => x.OrganizationId == dto.OrganizationId))
                    .FirstOrDefault();

                if (organization == null)
                    throw new CustomException("Organization not found.");

                // Duplicate Organization Code
                var duplicateCode = await _unitOfWork.Repository<OrganizationDatum>()
                    .FindAsync(x =>
                        x.OrganizationId != dto.OrganizationId &&
                        x.OrganizationCode.ToLower() == dto.OrganizationCode.ToLower());

                if (duplicateCode.Any())
                    throw new CustomException("Organization Code already exists.");

                // Duplicate Organization Name
                var duplicateName = await _unitOfWork.Repository<OrganizationDatum>()
                    .FindAsync(x =>
                        x.OrganizationId != dto.OrganizationId &&
                        x.OrganizationName.ToLower() == dto.OrganizationName.ToLower());

                if (duplicateName.Any())
                    throw new CustomException("Organization Name already exists.");

                string oldValues = JsonConvert.SerializeObject(organization);

                organization.OrganizationCode = dto.OrganizationCode;
                organization.OrganizationName = dto.OrganizationName;
                organization.LegalName = dto.LegalName;

                organization.Email = dto.Email;
                organization.Phone = dto.Phone;
                organization.Website = dto.Website;

                organization.Gstnumber = dto.GSTNumber;
                organization.Pannumber = dto.PANNumber;

                organization.AddressLine1 = dto.AddressLine1;
                organization.AddressLine2 = dto.AddressLine2;
                organization.City = dto.City;
                organization.State = dto.State;
                organization.Country = dto.Country;
                organization.PostalCode = dto.PostalCode;

                organization.LogoUrl = dto.LogoUrl;

               // organization.Status = dto.Status;

                organization.Domain = dto.Domain;

                organization.ContactPerson = dto.ContactPerson;
                organization.ContactEmail = dto.ContactEmail;
                organization.ContactMobile = dto.ContactMobile;

                organization.TimeZone = dto.TimeZone;
                organization.CurrencyCode = dto.CurrencyCode;

                organization.SubscriptionStartDate = dto.SubscriptionStartDate;
                organization.RenewalDate = dto.RenewalDate;

                organization.PlanId = dto.PlanId;

                organization.MaxUsers = dto.MaxUsers;
                organization.MaxStorageGb = dto.MaxStorageGB;
                organization.StorageUsedGb = dto.StorageUsedGB;

                organization.MonthlyRevenue = dto.MonthlyRevenue;

                organization.BrandColor = dto.BrandColor;
                organization.Industry = dto.Industry;

                organization.Features = dto.Features;

                organization.ModifiedBy = _currentUserService.UserId;
                organization.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<OrganizationDatum>().Update(organization);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Organization",
                    "UPDATE",
                    organization.OrganizationId,
                    oldValues,
                    JsonConvert.SerializeObject(organization),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Organization Updated Successfully.",
                    Data = organization.OrganizationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating Organization");
                throw;
            }
        }

        #endregion
        #region DELETE ORGANIZATION

        public async Task<ApiResponse<string>> DeleteOrganization(int id)
        {
            try
            {
                var organization = (await _unitOfWork.Repository<OrganizationDatum>()
                    .FindAsync(x => x.OrganizationId == id))
                    .FirstOrDefault();

                if (organization == null)
                    throw new CustomException("Organization not found.");

                // Check whether Organization is used anywhere
                //var userExists = (await _unitOfWork.Repository<UserLogin>()
                //    .FindAsync(x => x.OrganizationId == id))
                //    .Any();

                //if (userExists)
                //    throw new CustomException("This Organization is assigned to Users and cannot be deleted.");

                string oldValues = JsonConvert.SerializeObject(organization);

                _unitOfWork.Repository<OrganizationDatum>().Remove(organization);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Organization",
                    "DELETE",
                    organization.OrganizationId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Organization Deleted Successfully.",
                    Data = organization.OrganizationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting Organization");
                throw;
            }
        }

        #endregion
        #region GET ALL ORGANIZATIONS

        public async Task<ApiResponse<List<OrganizationDto>>> GetOrganizations()
        {
            var organizations = (await _unitOfWork.Repository<OrganizationDatum>()
                .GetAllAsync())
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            var result = organizations.Select(x => new OrganizationDto
            {
                OrganizationId = x.OrganizationId,
                OrganizationCode = x.OrganizationCode,
                OrganizationName = x.OrganizationName,
                LegalName = x.LegalName,
                Email = x.Email,
                Phone = x.Phone,
                Website = x.Website,

                GSTNumber = x.Gstnumber,
                PANNumber = x.Pannumber,

                AddressLine1 = x.AddressLine1,
                AddressLine2 = x.AddressLine2,
                City = x.City,
                State = x.State,
                Country = x.Country,
                PostalCode = x.PostalCode,

                LogoUrl = x.LogoUrl,

                //Status = x.Status,

                Domain = x.Domain,

                ContactPerson = x.ContactPerson,
                ContactEmail = x.ContactEmail,
                ContactMobile = x.ContactMobile,

                TimeZone = x.TimeZone,
                CurrencyCode = x.CurrencyCode,

                SubscriptionStartDate = x.SubscriptionStartDate,
                RenewalDate = x.RenewalDate,

                PlanId = x.PlanId,

                // Navigation Property
                PlanName = x.Plan != null
                            ? x.Plan.PlanName
                            : "",

                MaxUsers = x.MaxUsers,
                MaxStorageGB = x.MaxStorageGb,
                StorageUsedGB = x.StorageUsedGb,

                MonthlyRevenue = x.MonthlyRevenue ?? 0,

                BrandColor = x.BrandColor,
                Industry = x.Industry,

                Features = x.Features,

                CreatedAt = x.CreatedAt,
                ModifiedAt = x.ModifiedAt
            }).ToList();

            return new ApiResponse<List<OrganizationDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        #endregion

        #region GET ORGANIZATION BY ID

        public async Task<ApiResponse<OrganizationDto>> GetOrganizationById(int id)
        {
            var organization = (await _unitOfWork.Repository<OrganizationDatum>()
                .FindAsync(x => x.OrganizationId == id))
                .FirstOrDefault();

            if (organization == null)
                throw new CustomException("Organization not found.");

            return new ApiResponse<OrganizationDto>
            {
                Success = true,
                Message = "Success",

                Data = new OrganizationDto
                {
                    OrganizationId = organization.OrganizationId,
                    OrganizationCode = organization.OrganizationCode,
                    OrganizationName = organization.OrganizationName,
                    LegalName = organization.LegalName,
                    Email = organization.Email,
                    Phone = organization.Phone,
                    Website = organization.Website,

                    GSTNumber = organization.Gstnumber,
                    PANNumber = organization.Pannumber,

                    AddressLine1 = organization.AddressLine1,
                    AddressLine2 = organization.AddressLine2,
                    City = organization.City,
                    State = organization.State,
                    Country = organization.Country,
                    PostalCode = organization.PostalCode,

                    LogoUrl = organization.LogoUrl,

                    //Status = organization.Status,

                    Domain = organization.Domain,

                    ContactPerson = organization.ContactPerson,
                    ContactEmail = organization.ContactEmail,
                    ContactMobile = organization.ContactMobile,

                    TimeZone = organization.TimeZone,
                    CurrencyCode = organization.CurrencyCode,

                    SubscriptionStartDate = organization.SubscriptionStartDate,
                    RenewalDate = organization.RenewalDate,

                    PlanId = organization.PlanId,
                    PlanName = organization.Plan?.PlanName,

                    MaxUsers = organization.MaxUsers,
                    MaxStorageGB = organization.MaxStorageGb,
                    StorageUsedGB = organization.StorageUsedGb,

                    MonthlyRevenue = organization.MonthlyRevenue ?? 0,

                    BrandColor = organization.BrandColor,
                    Industry = organization.Industry,

                    Features = organization.Features,

                    CreatedAt = organization.CreatedAt,
                    ModifiedAt = organization.ModifiedAt
                }
            };
        }

        #endregion


    }
}
