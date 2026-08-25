using Business_Layer.DTOs.User;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.User;
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

namespace Business_Layer.Services.User
{
    public class CompanyInformationService : ICompanyInformationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public CompanyInformationService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }


        // =========================================================
        // CREATE COMPANY
        // =========================================================

        public async Task<ApiResponse<string>> CreateCompany(
            CompanyInformationDto dto)
        {
            try
            {
                // -------------------------------------------------
                // Required field validation
                // -------------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.CompanyName))
                    throw new CustomException("Company Name is required.");

                if (dto.IndustryId <= 0)
                    throw new CustomException("Industry is required.");

                if (dto.CompanyTypeId <= 0)
                    throw new CustomException("Company Type is required.");

                if (string.IsNullOrWhiteSpace(dto.CompanyOwner))
                    throw new CustomException("Company Owner is required.");

                if (string.IsNullOrWhiteSpace(dto.CompanyStatus))
                    throw new CustomException("Company Status is required.");

                if (string.IsNullOrWhiteSpace(dto.City))
                    throw new CustomException("City is required.");

                if (dto.StateId <= 0)
                    throw new CustomException("State is required.");

                if (dto.CountryId <= 0)
                    throw new CustomException("Country is required.");


                // -------------------------------------------------
                // Company Status validation
                // -------------------------------------------------

                var validStatuses = new[] { "Active", "Inactive" };

                if (!validStatuses.Contains(
                    dto.CompanyStatus,
                    StringComparer.OrdinalIgnoreCase))
                {
                    throw new CustomException(
                        "Company Status must be Active or Inactive.");
                }


                // -------------------------------------------------
                // Duplicate Company validation
                // -------------------------------------------------

                var duplicate = await _unitOfWork
                    .Repository<CompanyInformation>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CompanyName.ToLower() ==
                        dto.CompanyName.ToLower() &&
                        !x.IsDeleted);

                if (duplicate.Any())
                    throw new CustomException("Company already exists.");


                // -------------------------------------------------
                // Validate Industry
                // -------------------------------------------------

                var industry = (
                    await _unitOfWork
                        .Repository<Industry>()
                        .FindAsync(x =>
                            x.IndustryId == dto.IndustryId &&
                            x.IsActive &&
                            !x.IsDeleted)
                ).FirstOrDefault();

                if (industry == null)
                    throw new CustomException("Invalid Industry.");


                // -------------------------------------------------
                // Validate Company Type
                // -------------------------------------------------

                var companyType = (
                    await _unitOfWork
                        .Repository<CompanyType>()
                        .FindAsync(x =>
                            x.CompanyTypeId == dto.CompanyTypeId &&
                            x.IsActive &&
                            !x.IsDeleted)
                ).FirstOrDefault();

                if (companyType == null)
                    throw new CustomException("Invalid Company Type.");


                // -------------------------------------------------
                // Validate Country
                // -------------------------------------------------

                var country = (
                    await _unitOfWork
                        .Repository<Country>()
                        .FindAsync(x =>
                            x.CountryId == dto.CountryId &&
                            x.IsActive &&
                            !x.IsDeleted)
                ).FirstOrDefault();

                if (country == null)
                    throw new CustomException("Invalid Country.");


                // -------------------------------------------------
                // Validate State
                // -------------------------------------------------

                var state = (
                    await _unitOfWork
                        .Repository<StateMaster>()
                        .FindAsync(x =>
                            x.StateId == dto.StateId &&
                            x.CountryId == dto.CountryId &&
                            x.IsActive &&
                            !x.IsDeleted)
                ).FirstOrDefault();

                if (state == null)
                    throw new CustomException(
                        "Invalid State for the selected Country.");


                // -------------------------------------------------
                // Create Entity
                // -------------------------------------------------

                var company = new CompanyInformation
                {
                    CompanyName = dto.CompanyName.Trim(),

                    LegalCompanyName = dto.LegalCompanyName,

                    IndustryId = dto.IndustryId,

                    CompanyTypeId = dto.CompanyTypeId,

                    CompanyOwner = dto.CompanyOwner.Trim(),

                    CompanyStatus = dto.CompanyStatus.Trim(),

                    Website = dto.Website,

                    CompanyPhone = dto.CompanyPhone,

                    CompanyEmail = dto.CompanyEmail,

                    CompanyDescription = dto.CompanyDescription,


                    // Address
                    AddressLine1 = dto.AddressLine1,

                    AddressLine2 = dto.AddressLine2,

                    City = dto.City.Trim(),

                    StateId = dto.StateId,

                    CountryId = dto.CountryId,

                    PostalCode = dto.PostalCode,


                    // Business
                    NumberOfEmployees = dto.NumberOfEmployees,

                    AnnualRevenue = dto.AnnualRevenue,


                    // Registration
                    Gstnumber = dto.Gstnumber,

                    Pannumber = dto.Pannumber,

                    CinregistrationNumber = dto.CinregistrationNumber,

                    LinkedInCompanyUrl = dto.LinkedInCompanyUrl,


                    // Primary Contact
                    PrimaryContactName = dto.PrimaryContactName,

                    PrimaryContactDesignation =
                        dto.PrimaryContactDesignation,

                    PrimaryContactEmail =
                        dto.PrimaryContactEmail,

                    PrimaryContactPhone =
                        dto.PrimaryContactPhone,


                    // Tenant
                    CompanyId = dto.CompanyId,

                    RegionId = dto.RegionId,


                    // Audit
                    IsActive =
                        dto.CompanyStatus.Equals(
                            "Active",
                            StringComparison.OrdinalIgnoreCase),

                    IsDeleted = false,

                    CreatedBy = _currentUserService.UserId,

                    CreatedAt = DateTime.Now
                };


                await _unitOfWork
                    .Repository<CompanyInformation>()
                    .AddAsync(company);

                await _unitOfWork.CompleteAsync();


                // -------------------------------------------------
                // Audit
                // -------------------------------------------------

                await _auditService.LogAsync(
                    "CompanyInformation",
                    "INSERT",
                    company.CompanyInformationId,
                    "",
                    JsonConvert.SerializeObject(company),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Company Created Successfully",
                    Data = company.CompanyName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating company");

                throw;
            }
        }


        // =========================================================
        // UPDATE COMPANY
        // =========================================================

        public async Task<ApiResponse<string>> UpdateCompany(
            CompanyInformationDto dto)
        {
            try
            {
                var company = (
                    await _unitOfWork
                        .Repository<CompanyInformation>()
                        .FindAsync(x =>
                            x.CompanyInformationId ==
                            dto.CompanyInformationId &&
                            !x.IsDeleted)
                ).FirstOrDefault();


                if (company == null)
                    throw new CustomException("Company not found.");


                if (string.IsNullOrWhiteSpace(dto.CompanyName))
                    throw new CustomException("Company Name is required.");

                if (dto.IndustryId <= 0)
                    throw new CustomException("Industry is required.");

                if (dto.CompanyTypeId <= 0)
                    throw new CustomException("Company Type is required.");

                if (string.IsNullOrWhiteSpace(dto.CompanyOwner))
                    throw new CustomException("Company Owner is required.");

                if (string.IsNullOrWhiteSpace(dto.City))
                    throw new CustomException("City is required.");

                if (dto.StateId <= 0)
                    throw new CustomException("State is required.");

                if (dto.CountryId <= 0)
                    throw new CustomException("Country is required.");


                // -------------------------------------------------
                // Status validation
                // -------------------------------------------------

                var validStatuses = new[] { "Active", "Inactive" };

                if (!validStatuses.Contains(
                    dto.CompanyStatus,
                    StringComparer.OrdinalIgnoreCase))
                {
                    throw new CustomException(
                        "Company Status must be Active or Inactive.");
                }


                // -------------------------------------------------
                // Duplicate validation
                // -------------------------------------------------

                var duplicate = await _unitOfWork
                    .Repository<CompanyInformation>()
                    .FindAsync(x =>
                        x.CompanyInformationId !=
                        dto.CompanyInformationId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CompanyName.ToLower() ==
                        dto.CompanyName.ToLower() &&
                        !x.IsDeleted);

                if (duplicate.Any())
                    throw new CustomException(
                        "Company already exists.");


                // -------------------------------------------------
                // Validate State + Country
                // -------------------------------------------------

                var state = (
                    await _unitOfWork
                        .Repository<StateMaster>()
                        .FindAsync(x =>
                            x.StateId == dto.StateId &&
                            x.CountryId == dto.CountryId &&
                            x.IsActive &&
                            !x.IsDeleted)
                ).FirstOrDefault();

                if (state == null)
                    throw new CustomException(
                        "Invalid State for the selected Country.");


                // -------------------------------------------------
                // Old values for Audit
                // -------------------------------------------------

                string oldValues =
                    JsonConvert.SerializeObject(company);


                // -------------------------------------------------
                // Update
                // -------------------------------------------------

                company.CompanyName =
                    dto.CompanyName.Trim();

                company.LegalCompanyName =
                    dto.LegalCompanyName;

                company.IndustryId =
                    dto.IndustryId;

                company.CompanyTypeId =
                    dto.CompanyTypeId;

                company.CompanyOwner =
                    dto.CompanyOwner.Trim();

                company.CompanyStatus =
                    dto.CompanyStatus.Trim();

                company.Website =
                    dto.Website;

                company.CompanyPhone =
                    dto.CompanyPhone;

                company.CompanyEmail =
                    dto.CompanyEmail;

                company.CompanyDescription =
                    dto.CompanyDescription;


                // Address
                company.AddressLine1 =
                    dto.AddressLine1;

                company.AddressLine2 =
                    dto.AddressLine2;

                company.City =
                    dto.City.Trim();

                company.StateId =
                    dto.StateId;

                company.CountryId =
                    dto.CountryId;

                company.PostalCode =
                    dto.PostalCode;


                // Business
                company.NumberOfEmployees =
                    dto.NumberOfEmployees;

                company.AnnualRevenue =
                    dto.AnnualRevenue;


                // Registration
                company.Gstnumber =
                    dto.Gstnumber;

                company.Pannumber =
                    dto.Pannumber;

                company.CinregistrationNumber =
                    dto.CinregistrationNumber;

                company.LinkedInCompanyUrl =
                    dto.LinkedInCompanyUrl;


                // Primary Contact
                company.PrimaryContactName =
                    dto.PrimaryContactName;

                company.PrimaryContactDesignation =
                    dto.PrimaryContactDesignation;

                company.PrimaryContactEmail =
                    dto.PrimaryContactEmail;

                company.PrimaryContactPhone =
                    dto.PrimaryContactPhone;


                // Tenant
                company.CompanyId =
                    dto.CompanyId;

                company.RegionId =
                    dto.RegionId;


                // Status
                company.IsActive =
                    dto.CompanyStatus.Equals(
                        "Active",
                        StringComparison.OrdinalIgnoreCase);

                company.ModifiedBy =
                    _currentUserService.UserId;

                company.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<CompanyInformation>()
                    .Update(company);

                await _unitOfWork.CompleteAsync();


                // -------------------------------------------------
                // Audit
                // -------------------------------------------------

                await _auditService.LogAsync(
                    "CompanyInformation",
                    "UPDATE",
                    company.CompanyInformationId,
                    oldValues,
                    JsonConvert.SerializeObject(company),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Company Updated Successfully",
                    Data = company.CompanyName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating company");

                throw;
            }
        }


        // =========================================================
        // DELETE COMPANY
        // =========================================================

        public async Task<ApiResponse<string>> DeleteCompany(
            int id)
        {
            try
            {
                var company = (
                    await _unitOfWork
                        .Repository<CompanyInformation>()
                        .FindAsync(x =>
                            x.CompanyInformationId == id &&
                            !x.IsDeleted)
                ).FirstOrDefault();


                if (company == null)
                    throw new CustomException(
                        "Company not found.");


                string oldValues =
                    JsonConvert.SerializeObject(company);


                // -------------------------------------------------
                // Soft Delete
                // -------------------------------------------------

                company.IsDeleted = true;

                company.IsActive = false;

                company.ModifiedBy =
                    _currentUserService.UserId;

                company.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<CompanyInformation>()
                    .Update(company);

                await _unitOfWork.CompleteAsync();


                // -------------------------------------------------
                // Audit
                // -------------------------------------------------

                await _auditService.LogAsync(
                    "CompanyInformation",
                    "DELETE",
                    company.CompanyInformationId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Company Deleted Successfully",
                    Data = company.CompanyName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting company");

                throw;
            }
        }


        // =========================================================
        // GET ALL COMPANIES
        // =========================================================

        public async Task<ApiResponse<List<CompanyInformationDto>>>
            GetCompanies()
        {
            var companies =
                await _unitOfWork
                    .Repository<CompanyInformation>()
                    .GetAllAsync();

            var industries =
                await _unitOfWork
                    .Repository<Industry>()
                    .GetAllAsync();

            var companyTypes =
                await _unitOfWork
                    .Repository<CompanyType>()
                    .GetAllAsync();

            var countries =
                await _unitOfWork
                    .Repository<Country>()
                    .GetAllAsync();

            var states =
                await _unitOfWork
                    .Repository<StateMaster>()
                    .GetAllAsync();


            var result =
                (from company in companies

                 join industry in industries
                     on company.IndustryId equals
                     industry.IndustryId

                 join companyType in companyTypes
                     on company.CompanyTypeId equals
                     companyType.CompanyTypeId

                 join country in countries
                     on company.CountryId equals
                     country.CountryId

                 join state in states
                     on company.StateId equals
                     state.StateId

                 where !company.IsDeleted

                 select new CompanyInformationDto
                 {
                     CompanyInformationId =
                         company.CompanyInformationId,

                     CompanyName =
                         company.CompanyName,

                     LegalCompanyName =
                         company.LegalCompanyName,


                     IndustryId =
                         company.IndustryId,

                     IndustryName =
                         industry.IndustryName,


                     CompanyTypeId =
                         company.CompanyTypeId,

                     CompanyTypeName =
                         companyType.CompanyTypeName,


                     CompanyOwner =
                         company.CompanyOwner,

                     CompanyStatus =
                         company.CompanyStatus,


                     Website =
                         company.Website,

                     CompanyPhone =
                         company.CompanyPhone,

                     CompanyEmail =
                         company.CompanyEmail,

                     CompanyDescription =
                         company.CompanyDescription,


                     // Address
                     AddressLine1 =
                         company.AddressLine1,

                     AddressLine2 =
                         company.AddressLine2,

                     City =
                         company.City,


                     StateId =
                         company.StateId,

                     StateName =
                         state.StateName,


                     CountryId =
                         company.CountryId,

                     CountryName =
                         country.CountryName,


                     PostalCode =
                         company.PostalCode,


                     // Business
                     NumberOfEmployees =
                         company.NumberOfEmployees,

                     AnnualRevenue =
                         company.AnnualRevenue,


                     // Registration
                     Gstnumber =
                         company.Gstnumber,

                     Pannumber =
                         company.Pannumber,

                     CinregistrationNumber =
                         company.CinregistrationNumber,

                     LinkedInCompanyUrl =
                         company.LinkedInCompanyUrl,


                     // Primary Contact
                     PrimaryContactName =
                         company.PrimaryContactName,

                     PrimaryContactDesignation =
                         company.PrimaryContactDesignation,

                     PrimaryContactEmail =
                         company.PrimaryContactEmail,

                     PrimaryContactPhone =
                         company.PrimaryContactPhone,


                     // Tenant
                     CompanyId =
                         company.CompanyId,

                     RegionId =
                         company.RegionId,

                     IsActive =
                         company.IsActive
                 })
                .OrderByDescending(x =>
                    x.CompanyInformationId)
                .ToList();


            return new ApiResponse<List<CompanyInformationDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        // =========================================================
        // GET COMPANY BY ID
        // =========================================================

        public async Task<ApiResponse<CompanyInformationDto>>
            GetCompanyById(int id)
        {
            var company = (
                await _unitOfWork
                    .Repository<CompanyInformation>()
                    .FindAsync(x =>
                        x.CompanyInformationId == id &&
                        !x.IsDeleted)
            ).FirstOrDefault();


            if (company == null)
                throw new CustomException(
                    "Company not found.");


            var industry = (
                await _unitOfWork
                    .Repository<Industry>()
                    .FindAsync(x =>
                        x.IndustryId == company.IndustryId)
            ).FirstOrDefault();


            var companyType = (
                await _unitOfWork
                    .Repository<CompanyType>()
                    .FindAsync(x =>
                        x.CompanyTypeId ==
                        company.CompanyTypeId)
            ).FirstOrDefault();


            var country = (
                await _unitOfWork
                    .Repository<Country>()
                    .FindAsync(x =>
                        x.CountryId == company.CountryId)
            ).FirstOrDefault();


            var state = (
                await _unitOfWork
                    .Repository<StateMaster>()
                    .FindAsync(x =>
                        x.StateId == company.StateId)
            ).FirstOrDefault();


            var dto = new CompanyInformationDto
            {
                CompanyInformationId =
                    company.CompanyInformationId,

                CompanyName =
                    company.CompanyName,

                LegalCompanyName =
                    company.LegalCompanyName,


                IndustryId =
                    company.IndustryId,

                IndustryName =
                    industry?.IndustryName,


                CompanyTypeId =
                    company.CompanyTypeId,

                CompanyTypeName =
                    companyType?.CompanyTypeName,


                CompanyOwner =
                    company.CompanyOwner,

                CompanyStatus =
                    company.CompanyStatus,


                Website =
                    company.Website,

                CompanyPhone =
                    company.CompanyPhone,

                CompanyEmail =
                    company.CompanyEmail,

                CompanyDescription =
                    company.CompanyDescription,


                // Address
                AddressLine1 =
                    company.AddressLine1,

                AddressLine2 =
                    company.AddressLine2,

                City =
                    company.City,


                StateId =
                    company.StateId,

                StateName =
                    state?.StateName,


                CountryId =
                    company.CountryId,

                CountryName =
                    country?.CountryName,


                PostalCode =
                    company.PostalCode,


                // Business
                NumberOfEmployees =
                    company.NumberOfEmployees,

                AnnualRevenue =
                    company.AnnualRevenue,


                // Registration
                Gstnumber =
                    company.Gstnumber,

                Pannumber =
                    company.Pannumber,

                CinregistrationNumber =
                    company.CinregistrationNumber,

                LinkedInCompanyUrl =
                    company.LinkedInCompanyUrl,


                // Primary Contact
                PrimaryContactName =
                    company.PrimaryContactName,

                PrimaryContactDesignation =
                    company.PrimaryContactDesignation,

                PrimaryContactEmail =
                    company.PrimaryContactEmail,

                PrimaryContactPhone =
                    company.PrimaryContactPhone,


                CompanyId =
                    company.CompanyId,

                RegionId =
                    company.RegionId,

                IsActive =
                    company.IsActive
            };


            return new ApiResponse<CompanyInformationDto>
            {
                Success = true,
                Message = "Success",
                Data = dto
            };
        }
    }
}
