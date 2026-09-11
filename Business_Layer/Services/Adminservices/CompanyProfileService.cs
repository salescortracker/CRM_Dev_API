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
    public class CompanyProfileService : ICompanyProfileService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public CompanyProfileService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }
        #region Company Profile

        #region CREATE

        public async Task<ApiResponse<string>> CreateCompanyProfile(
            CompanyProfileDto dto)
        {
            try
            {
                // Validate Company Name
                if (string.IsNullOrWhiteSpace(dto.CompanyName))
                    throw new CustomException(
                        "Company Name is required.");

                // Validate Company Code
                if (string.IsNullOrWhiteSpace(dto.CompanyCode))
                    throw new CustomException(
                        "Company Code is required.");

                // Validate Company
                var company =
                    (await _unitOfWork.Repository<Company>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (company == null)
                    throw new CustomException(
                        "Company not found.");

                // Validate Region
                var region =
                    (await _unitOfWork.Repository<Region>()
                        .FindAsync(x =>
                            x.RegionId == dto.RegionId &&
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (region == null)
                    throw new CustomException(
                        "Region not found for the selected company.");

                // Duplicate Company Profile
                var duplicate =
                    await _unitOfWork.Repository<CompanyProfile>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId);

                if (duplicate.Any())
                    throw new CustomException(
                        "Company Profile already exists for the selected company and region.");

                // Duplicate Company Code
                var duplicateCode =
                    await _unitOfWork.Repository<CompanyProfile>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId &&
                            x.CompanyCode.ToLower() ==
                            dto.CompanyCode.Trim().ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Company Code already exists.");

                // Validate Industry
                if (dto.IndustryId.HasValue)
                {
                    var industry =
                        (await _unitOfWork.Repository<Industry>()
                            .FindAsync(x =>
                                x.IndustryId ==
                                dto.IndustryId.Value))
                        .FirstOrDefault();

                    if (industry == null)
                        throw new CustomException(
                            "Industry not found.");
                }

                // Validate Company Type
                if (dto.CompanyTypeId.HasValue)
                {
                    var companyType =
                        (await _unitOfWork.Repository<CompanyType>()
                            .FindAsync(x =>
                                x.CompanyTypeId ==
                                dto.CompanyTypeId.Value))
                        .FirstOrDefault();

                    if (companyType == null)
                        throw new CustomException(
                            "Company Type not found.");
                }

                // Validate Country
                if (dto.CountryId.HasValue)
                {
                    var country =
                        (await _unitOfWork.Repository<Country>()
                            .FindAsync(x =>
                                x.CountryId ==
                                dto.CountryId.Value))
                        .FirstOrDefault();

                    if (country == null)
                        throw new CustomException(
                            "Country not found.");
                }

                // Validate State
                if (dto.StateId.HasValue)
                {
                    var state =
                        (await _unitOfWork.Repository<StateMaster>()
                            .FindAsync(x =>
                                x.StateId ==
                                dto.StateId.Value))
                        .FirstOrDefault();

                    if (state == null)
                        throw new CustomException(
                            "State not found.");
                }

                // Validate Currency
                if (dto.CurrencyId.HasValue)
                {
                    var currency =
                        (await _unitOfWork.Repository<Currency>()
                            .FindAsync(x =>
                                x.CurrencyId ==
                                dto.CurrencyId.Value))
                        .FirstOrDefault();

                    if (currency == null)
                        throw new CustomException(
                            "Currency not found.");
                }

                // Create Entity
                CompanyProfile companyProfile =
                    new CompanyProfile
                    {
                        CompanyId =
                            dto.CompanyId,

                        RegionId =
                            dto.RegionId,

                        CompanyName =
                            dto.CompanyName.Trim(),

                        CompanyCode =
                            dto.CompanyCode.Trim(),

                        LegalName =
                            dto.LegalName?.Trim(),

                        RegistrationNumber =
                            dto.RegistrationNumber?.Trim(),

                        Gstnumber =
                            dto.Gstnumber?.Trim(),

                        Pannumber =
                            dto.Pannumber?.Trim(),

                        IndustryId =
                            dto.IndustryId,

                        CompanyTypeId =
                            dto.CompanyTypeId,

                        EstablishedDate =
                            dto.EstablishedDate,

                        Email =
                            dto.Email?.Trim(),

                        Phone =
                            dto.Phone?.Trim(),

                        Mobile =
                            dto.Mobile?.Trim(),

                        Website =
                            dto.Website?.Trim(),

                        AddressLine1 =
                            dto.AddressLine1?.Trim(),

                        AddressLine2 =
                            dto.AddressLine2?.Trim(),

                        CountryId =
                            dto.CountryId,

                        StateId =
                            dto.StateId,

                        City =
                            dto.City?.Trim(),

                        Pincode =
                            dto.Pincode?.Trim(),

                        CurrencyId =
                            dto.CurrencyId,

                        FinancialYear =
                            dto.FinancialYear?.Trim(),

                        CompanyLogoPath =
                            dto.CompanyLogoPath?.Trim(),

                        Description =
                            dto.Description?.Trim(),

                        Active =
                            dto.Active,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedDate =
                            DateTime.Now
                    };

                await _unitOfWork.Repository<CompanyProfile>()
                    .AddAsync(companyProfile);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "CompanyProfile",
                    "INSERT",
                    companyProfile.CompanyProfileId,
                    "",
                    JsonConvert.SerializeObject(companyProfile),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Company Profile Created Successfully",

                    Data =
                        companyProfile.CompanyName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating company profile");

                throw;
            }
        }

        #endregion

        #region UPDATE

        public async Task<ApiResponse<string>> UpdateCompanyProfile(
            CompanyProfileDto dto)
        {
            try
            {
                // Required fields
                if (string.IsNullOrWhiteSpace(dto.CompanyName))
                    throw new CustomException(
                        "Company Name is required.");

                if (string.IsNullOrWhiteSpace(dto.CompanyCode))
                    throw new CustomException(
                        "Company Code is required.");

                // Get Existing Profile
                var companyProfile =
                    (await _unitOfWork.Repository<CompanyProfile>()
                        .FindAsync(x =>
                            x.CompanyProfileId ==
                            dto.CompanyProfileId))
                    .FirstOrDefault();

                if (companyProfile == null)
                    throw new CustomException(
                        "Company Profile not found.");

                // Validate Company
                var company =
                    (await _unitOfWork.Repository<Company>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (company == null)
                    throw new CustomException(
                        "Company not found.");

                // Validate Region
                var region =
                    (await _unitOfWork.Repository<Region>()
                        .FindAsync(x =>
                            x.RegionId == dto.RegionId &&
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (region == null)
                    throw new CustomException(
                        "Region not found for the selected company.");

                // Duplicate Company Profile
                var duplicate =
                    await _unitOfWork.Repository<CompanyProfile>()
                        .FindAsync(x =>
                            x.CompanyProfileId !=
                            dto.CompanyProfileId &&

                            x.CompanyId ==
                            dto.CompanyId &&

                            x.RegionId ==
                            dto.RegionId);

                if (duplicate.Any())
                    throw new CustomException(
                        "Company Profile already exists for the selected company and region.");

                // Duplicate Company Code
                var duplicateCode =
                    await _unitOfWork.Repository<CompanyProfile>()
                        .FindAsync(x =>
                            x.CompanyProfileId !=
                            dto.CompanyProfileId &&

                            x.CompanyId ==
                            dto.CompanyId &&

                            x.CompanyCode.ToLower() ==
                            dto.CompanyCode.Trim().ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Company Code already exists.");

                // Validate Industry
                if (dto.IndustryId.HasValue)
                {
                    var industry =
                        (await _unitOfWork.Repository<Industry>()
                            .FindAsync(x =>
                                x.IndustryId ==
                                dto.IndustryId.Value))
                        .FirstOrDefault();

                    if (industry == null)
                        throw new CustomException(
                            "Industry not found.");
                }

                // Validate Company Type
                if (dto.CompanyTypeId.HasValue)
                {
                    var companyType =
                        (await _unitOfWork.Repository<CompanyType>()
                            .FindAsync(x =>
                                x.CompanyTypeId ==
                                dto.CompanyTypeId.Value))
                        .FirstOrDefault();

                    if (companyType == null)
                        throw new CustomException(
                            "Company Type not found.");
                }

                // Validate Country
                if (dto.CountryId.HasValue)
                {
                    var country =
                        (await _unitOfWork.Repository<Country>()
                            .FindAsync(x =>
                                x.CountryId ==
                                dto.CountryId.Value))
                        .FirstOrDefault();

                    if (country == null)
                        throw new CustomException(
                            "Country not found.");
                }

                // Validate State
                if (dto.StateId.HasValue)
                {
                    var state =
                        (await _unitOfWork.Repository<StateMaster>()
                            .FindAsync(x =>
                                x.StateId ==
                                dto.StateId.Value))
                        .FirstOrDefault();

                    if (state == null)
                        throw new CustomException(
                            "State not found.");
                }

                // Validate Currency
                if (dto.CurrencyId.HasValue)
                {
                    var currency =
                        (await _unitOfWork.Repository<Currency>()
                            .FindAsync(x =>
                                x.CurrencyId ==
                                dto.CurrencyId.Value))
                        .FirstOrDefault();

                    if (currency == null)
                        throw new CustomException(
                            "Currency not found.");
                }

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(companyProfile);

                // Update
                companyProfile.CompanyId =
                    dto.CompanyId;

                companyProfile.RegionId =
                    dto.RegionId;

                companyProfile.CompanyName =
                    dto.CompanyName.Trim();

                companyProfile.CompanyCode =
                    dto.CompanyCode.Trim();

                companyProfile.LegalName =
                    dto.LegalName?.Trim();

                companyProfile.RegistrationNumber =
                    dto.RegistrationNumber?.Trim();

                companyProfile.Gstnumber =
                    dto.Gstnumber?.Trim();

                companyProfile.Pannumber =
                    dto.Pannumber?.Trim();

                companyProfile.IndustryId =
                    dto.IndustryId;

                companyProfile.CompanyTypeId =
                    dto.CompanyTypeId;

                companyProfile.EstablishedDate =
                    dto.EstablishedDate;

                companyProfile.Email =
                    dto.Email?.Trim();

                companyProfile.Phone =
                    dto.Phone?.Trim();

                companyProfile.Mobile =
                    dto.Mobile?.Trim();

                companyProfile.Website =
                    dto.Website?.Trim();

                companyProfile.AddressLine1 =
                    dto.AddressLine1?.Trim();

                companyProfile.AddressLine2 =
                    dto.AddressLine2?.Trim();

                companyProfile.CountryId =
                    dto.CountryId;

                companyProfile.StateId =
                    dto.StateId;

                companyProfile.City =
                    dto.City?.Trim();

                companyProfile.Pincode =
                    dto.Pincode?.Trim();

                companyProfile.CurrencyId =
                    dto.CurrencyId;

                companyProfile.FinancialYear =
                    dto.FinancialYear?.Trim();

                companyProfile.CompanyLogoPath =
                    dto.CompanyLogoPath?.Trim();

                companyProfile.Description =
                    dto.Description?.Trim();

                companyProfile.Active =
                    dto.Active;

                companyProfile.UpdatedBy =
                    _currentUserService.UserId;

                companyProfile.UpdatedDate =
                    DateTime.Now;

                _unitOfWork.Repository<CompanyProfile>()
                    .Update(companyProfile);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "CompanyProfile",
                    "UPDATE",
                    companyProfile.CompanyProfileId,
                    oldValues,
                    JsonConvert.SerializeObject(companyProfile),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Company Profile Updated Successfully",

                    Data =
                        companyProfile.CompanyName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating company profile");

                throw;
            }
        }

        #endregion

        #region DELETE

        public async Task<ApiResponse<string>> DeleteCompanyProfile(
            int id)
        {
            try
            {
                var companyProfile =
                    (await _unitOfWork.Repository<CompanyProfile>()
                        .FindAsync(x =>
                            x.CompanyProfileId == id))
                    .FirstOrDefault();

                if (companyProfile == null)
                    throw new CustomException(
                        "Company Profile not found.");

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(companyProfile);

                // Hard Delete
                _unitOfWork.Repository<CompanyProfile>()
                    .Remove(companyProfile);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "CompanyProfile",
                    "DELETE",
                    companyProfile.CompanyProfileId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Company Profile Deleted Successfully",

                    Data =
                        companyProfile.CompanyName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting company profile");

                throw;
            }
        }

        #endregion
        #region GET ALL

        public async Task<ApiResponse<List<CompanyProfileDto>>>
            GetCompanyProfiles()
        {
            try
            {
                var companyProfiles =
                    (await _unitOfWork.Repository<CompanyProfile>()
                        .GetAllAsync())
                    .OrderByDescending(x =>
                        x.CompanyProfileId)
                    .ToList();

                var result =
                    companyProfiles.Select(x =>
                        new CompanyProfileDto
                        {
                            CompanyProfileId =
                                x.CompanyProfileId,

                            CompanyId =
                                x.CompanyId,

                            RegionId =
                                x.RegionId,

                            CompanyName =
                                x.CompanyName,

                            CompanyCode =
                                x.CompanyCode,

                            LegalName =
                                x.LegalName,

                            RegistrationNumber =
                                x.RegistrationNumber,

                            Gstnumber =
                                x.Gstnumber,

                            Pannumber =
                                x.Pannumber,

                            IndustryId =
                                x.IndustryId,

                            CompanyTypeId =
                                x.CompanyTypeId,

                            EstablishedDate =
                                x.EstablishedDate,

                            Email =
                                x.Email,

                            Phone =
                                x.Phone,

                            Mobile =
                                x.Mobile,

                            Website =
                                x.Website,

                            AddressLine1 =
                                x.AddressLine1,

                            AddressLine2 =
                                x.AddressLine2,

                            CountryId =
                                x.CountryId,

                            StateId =
                                x.StateId,

                            City =
                                x.City,

                            Pincode =
                                x.Pincode,

                            CurrencyId =
                                x.CurrencyId,

                            FinancialYear =
                                x.FinancialYear,

                            CompanyLogoPath =
                                x.CompanyLogoPath,

                            Description =
                                x.Description,

                            Active =
                                x.Active
                        })
                    .ToList();

                return new ApiResponse<List<CompanyProfileDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting company profiles");

                throw;
            }
        }

        #endregion
        #region GET BY ID

        public async Task<ApiResponse<CompanyProfileDto>>
            GetCompanyProfileById(int id)
        {
            try
            {
                var companyProfile =
                    (await _unitOfWork.Repository<CompanyProfile>()
                        .FindAsync(x =>
                            x.CompanyProfileId == id))
                    .FirstOrDefault();

                if (companyProfile == null)
                    throw new CustomException(
                        "Company Profile not found.");

                var result =
                    new CompanyProfileDto
                    {
                        CompanyProfileId =
                            companyProfile.CompanyProfileId,

                        CompanyId =
                            companyProfile.CompanyId,

                        RegionId =
                            companyProfile.RegionId,

                        CompanyName =
                            companyProfile.CompanyName,

                        CompanyCode =
                            companyProfile.CompanyCode,

                        LegalName =
                            companyProfile.LegalName,

                        RegistrationNumber =
                            companyProfile.RegistrationNumber,

                        Gstnumber =
                            companyProfile.Gstnumber,

                        Pannumber =
                            companyProfile.Pannumber,

                        IndustryId =
                            companyProfile.IndustryId,

                        CompanyTypeId =
                            companyProfile.CompanyTypeId,

                        EstablishedDate =
                            companyProfile.EstablishedDate,

                        Email =
                            companyProfile.Email,

                        Phone =
                            companyProfile.Phone,

                        Mobile =
                            companyProfile.Mobile,

                        Website =
                            companyProfile.Website,

                        AddressLine1 =
                            companyProfile.AddressLine1,

                        AddressLine2 =
                            companyProfile.AddressLine2,

                        CountryId =
                            companyProfile.CountryId,

                        StateId =
                            companyProfile.StateId,

                        City =
                            companyProfile.City,

                        Pincode =
                            companyProfile.Pincode,

                        CurrencyId =
                            companyProfile.CurrencyId,

                        FinancialYear =
                            companyProfile.FinancialYear,

                        CompanyLogoPath =
                            companyProfile.CompanyLogoPath,

                        Description =
                            companyProfile.Description,

                        Active =
                            companyProfile.Active
                    };

                return new ApiResponse<CompanyProfileDto>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting company profile by id");

                throw;
            }
        }

        #endregion

        #endregion
    }
}
