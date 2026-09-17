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
using static Business_Layer.Services.Adminservices.CompanyProfileService;

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


            #region User Group

            #region CREATE

            public async Task<ApiResponse<string>> CreateUserGroup(
                UserGroupDto dto)
            {
                try
                {
                    // Validate DTO
                    if (dto == null)
                        throw new CustomException(
                            "User Group details are required.");

                    // Validate Group Name
                    if (string.IsNullOrWhiteSpace(dto.GroupName))
                        throw new CustomException(
                            "Group Name is required.");

                    // Validate Group Code
                    if (string.IsNullOrWhiteSpace(dto.GroupCode))
                        throw new CustomException(
                            "Group Code is required.");

                    // Validate User Limit
                    if (dto.UserLimit.HasValue &&
                        dto.UserLimit.Value < 0)
                    {
                        throw new CustomException(
                            "User Limit cannot be negative.");
                    }

                    // Duplicate Group Name
                    var duplicateName =
                        await _unitOfWork.Repository<UserGroup>()
                            .FindAsync(x =>
                                x.GroupName.ToLower() ==
                                dto.GroupName.Trim().ToLower() &&
                                !x.IsDeleted);

                    if (duplicateName.Any())
                        throw new CustomException(
                            "Group Name already exists.");

                    // Duplicate Group Code
                    var duplicateCode =
                        await _unitOfWork.Repository<UserGroup>()
                            .FindAsync(x =>
                                x.GroupCode.ToLower() ==
                                dto.GroupCode.Trim().ToLower() &&
                                !x.IsDeleted);

                    if (duplicateCode.Any())
                        throw new CustomException(
                            "Group Code already exists.");

                    // Validate Department
                    if (dto.DepartmentId.HasValue)
                    {
                        var department =
                            (await _unitOfWork.Repository<Department>()
                                .FindAsync(x =>
                                    x.DepartmentId ==
                                    dto.DepartmentId.Value))
                            .FirstOrDefault();

                        if (department == null)
                            throw new CustomException(
                                "Department not found.");
                    }

                    // Validate Priority
                    if (dto.PriorityId.HasValue)
                    {
                        var priority =
                            (await _unitOfWork.Repository<Priority>()
                                .FindAsync(x =>
                                    x.PriorityId ==
                                    dto.PriorityId.Value))
                            .FirstOrDefault();

                        if (priority == null)
                            throw new CustomException(
                                "Priority not found.");
                    }

                    // Create Entity
                    UserGroup userGroup =
                        new UserGroup
                        {
                            GroupName =
                                dto.GroupName.Trim(),

                            GroupCode =
                                dto.GroupCode.Trim(),

                            GroupType =
                                dto.GroupType?.Trim(),

                            DepartmentId =
                                dto.DepartmentId,

                            Team =
                                dto.Team?.Trim(),

                            ReportingManager =
                                dto.ReportingManager?.Trim(),

                            DefaultRole =
                                dto.DefaultRole?.Trim(),

                            UserLimit =
                                dto.UserLimit,

                            PriorityId =
                                dto.PriorityId,

                            Status =
                                dto.Status,

                            Description =
                                dto.Description?.Trim(),

                            IsDeleted =
                                false,

                            CreatedBy =
                                _currentUserService.UserId,

                            CreatedAt =
                                DateTime.Now
                        };

                    await _unitOfWork.Repository<UserGroup>()
                        .AddAsync(userGroup);

                    await _unitOfWork.CompleteAsync();

                    // Audit
                    await _auditService.LogAsync(
                        "UserGroup",
                        "INSERT",
                        userGroup.UserGroupId,
                        "",
                        JsonConvert.SerializeObject(userGroup),
                        _currentUserService.UserId);

                    return new ApiResponse<string>
                    {
                        Success = true,

                        Message =
                            "User Group Created Successfully",

                        Data =
                            userGroup.GroupName
                    };
                }
                catch (Exception ex)
                {
                    Log.Error(
                        ex,
                        "Error while creating user group");

                    throw;
                }
            }

            #endregion


            #region UPDATE

            public async Task<ApiResponse<string>> UpdateUserGroup(
                UserGroupDto dto)
            {
                try
                {
                    // Validate DTO
                    if (dto == null)
                        throw new CustomException(
                            "User Group details are required.");

                    // Validate ID
                    if (dto.UserGroupId <= 0)
                        throw new CustomException(
                            "Valid User Group ID is required.");

                    // Validate Group Name
                    if (string.IsNullOrWhiteSpace(dto.GroupName))
                        throw new CustomException(
                            "Group Name is required.");

                    // Validate Group Code
                    if (string.IsNullOrWhiteSpace(dto.GroupCode))
                        throw new CustomException(
                            "Group Code is required.");

                    // Validate User Limit
                    if (dto.UserLimit.HasValue &&
                        dto.UserLimit.Value < 0)
                    {
                        throw new CustomException(
                            "User Limit cannot be negative.");
                    }

                    // Get Existing User Group
                    var userGroup =
                        (await _unitOfWork.Repository<UserGroup>()
                            .FindAsync(x =>
                                x.UserGroupId ==
                                dto.UserGroupId &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (userGroup == null)
                        throw new CustomException(
                            "User Group not found.");

                    // Duplicate Group Name
                    var duplicateName =
                        await _unitOfWork.Repository<UserGroup>()
                            .FindAsync(x =>
                                x.UserGroupId !=
                                dto.UserGroupId &&

                                x.GroupName.ToLower() ==
                                dto.GroupName.Trim().ToLower() &&

                                !x.IsDeleted);

                    if (duplicateName.Any())
                        throw new CustomException(
                            "Group Name already exists.");

                    // Duplicate Group Code
                    var duplicateCode =
                        await _unitOfWork.Repository<UserGroup>()
                            .FindAsync(x =>
                                x.UserGroupId !=
                                dto.UserGroupId &&

                                x.GroupCode.ToLower() ==
                                dto.GroupCode.Trim().ToLower() &&

                                !x.IsDeleted);

                    if (duplicateCode.Any())
                        throw new CustomException(
                            "Group Code already exists.");

                    // Validate Department
                    if (dto.DepartmentId.HasValue)
                    {
                        var department =
                            (await _unitOfWork.Repository<Department>()
                                .FindAsync(x =>
                                    x.DepartmentId ==
                                    dto.DepartmentId.Value))
                            .FirstOrDefault();

                        if (department == null)
                            throw new CustomException(
                                "Department not found.");
                    }

                    // Validate Priority
                    if (dto.PriorityId.HasValue)
                    {
                        var priority =
                            (await _unitOfWork.Repository<Priority>()
                                .FindAsync(x =>
                                    x.PriorityId ==
                                    dto.PriorityId.Value))
                            .FirstOrDefault();

                        if (priority == null)
                            throw new CustomException(
                                "Priority not found.");
                    }

                    // Old Values
                    string oldValues =
                        JsonConvert.SerializeObject(userGroup);

                    // Update Entity
                    userGroup.GroupName =
                        dto.GroupName.Trim();

                    userGroup.GroupCode =
                        dto.GroupCode.Trim();

                    userGroup.GroupType =
                        dto.GroupType?.Trim();

                    userGroup.DepartmentId =
                        dto.DepartmentId;

                    userGroup.Team =
                        dto.Team?.Trim();

                    userGroup.ReportingManager =
                        dto.ReportingManager?.Trim();

                    userGroup.DefaultRole =
                        dto.DefaultRole?.Trim();

                    userGroup.UserLimit =
                        dto.UserLimit;

                    userGroup.PriorityId =
                        dto.PriorityId;

                    userGroup.Status =
                        dto.Status;

                    userGroup.Description =
                        dto.Description?.Trim();

                    userGroup.ModifiedBy =
                        _currentUserService.UserId;

                    userGroup.ModifiedAt =
                        DateTime.Now;

                    _unitOfWork.Repository<UserGroup>()
                        .Update(userGroup);

                    await _unitOfWork.CompleteAsync();

                    // Audit
                    await _auditService.LogAsync(
                        "UserGroup",
                        "UPDATE",
                        userGroup.UserGroupId,
                        oldValues,
                        JsonConvert.SerializeObject(userGroup),
                        _currentUserService.UserId);

                    return new ApiResponse<string>
                    {
                        Success = true,

                        Message =
                            "User Group Updated Successfully",

                        Data =
                            userGroup.GroupName
                    };
                }
                catch (Exception ex)
                {
                    Log.Error(
                        ex,
                        "Error while updating user group");

                    throw;
                }
            }

            #endregion


            #region DELETE

            public async Task<ApiResponse<string>> DeleteUserGroup(
                int id)
            {
                try
                {
                    // Get Existing User Group
                    var userGroup =
                        (await _unitOfWork.Repository<UserGroup>()
                            .FindAsync(x =>
                                x.UserGroupId == id &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (userGroup == null)
                        throw new CustomException(
                            "User Group not found.");

                    // Old Values
                    string oldValues =
                        JsonConvert.SerializeObject(userGroup);

                    // Soft Delete
                    userGroup.IsDeleted = true;

                    userGroup.ModifiedBy =
                        _currentUserService.UserId;

                    userGroup.ModifiedAt =
                        DateTime.Now;

                    _unitOfWork.Repository<UserGroup>()
                        .Update(userGroup);

                    await _unitOfWork.CompleteAsync();

                    // Audit
                    await _auditService.LogAsync(
                        "UserGroup",
                        "DELETE",
                        userGroup.UserGroupId,
                        oldValues,
                        JsonConvert.SerializeObject(userGroup),
                        _currentUserService.UserId);

                    return new ApiResponse<string>
                    {
                        Success = true,

                        Message =
                            "User Group Deleted Successfully",

                        Data =
                            userGroup.GroupName
                    };
                }
                catch (Exception ex)
                {
                    Log.Error(
                        ex,
                        "Error while deleting user group");

                    throw;
                }
            }

            #endregion


            #region GET ALL

            public async Task<ApiResponse<List<UserGroupDto>>>
                GetUserGroups()
            {
                try
                {
                    var userGroups =
                        (await _unitOfWork.Repository<UserGroup>()
                            .FindAsync(x =>
                                !x.IsDeleted))
                        .OrderByDescending(x =>
                            x.UserGroupId)
                        .ToList();

                    var result =
                        userGroups.Select(x =>
                            new UserGroupDto
                            {
                                UserGroupId =
                                    x.UserGroupId,

                                GroupName =
                                    x.GroupName,

                                GroupCode =
                                    x.GroupCode,

                                GroupType =
                                    x.GroupType,

                                DepartmentId =
                                    x.DepartmentId,

                                Team =
                                    x.Team,

                                ReportingManager =
                                    x.ReportingManager,

                                DefaultRole =
                                    x.DefaultRole,

                                UserLimit =
                                    x.UserLimit,

                                PriorityId =
                                    x.PriorityId,

                                Status =
                                    x.Status,

                                Description =
                                    x.Description
                            })
                        .ToList();

                    return new ApiResponse<List<UserGroupDto>>
                    {
                        Success = true,

                        Message =
                            "Success",

                        Data =
                            result
                    };
                }
                catch (Exception ex)
                {
                    Log.Error(
                        ex,
                        "Error while getting user groups");

                    throw;
                }
            }

            #endregion


            #region GET BY ID

            public async Task<ApiResponse<UserGroupDto>>
                GetUserGroupById(int id)
            {
                try
                {
                    var userGroup =
                        (await _unitOfWork.Repository<UserGroup>()
                            .FindAsync(x =>
                                x.UserGroupId == id &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (userGroup == null)
                        throw new CustomException(
                            "User Group not found.");

                    var result =
                        new UserGroupDto
                        {
                            UserGroupId =
                                userGroup.UserGroupId,

                            GroupName =
                                userGroup.GroupName,

                            GroupCode =
                                userGroup.GroupCode,

                            GroupType =
                                userGroup.GroupType,

                            DepartmentId =
                                userGroup.DepartmentId,

                            Team =
                                userGroup.Team,

                            ReportingManager =
                                userGroup.ReportingManager,

                            DefaultRole =
                                userGroup.DefaultRole,

                            UserLimit =
                                userGroup.UserLimit,

                            PriorityId =
                                userGroup.PriorityId,

                            Status =
                                userGroup.Status,

                            Description =
                                userGroup.Description
                        };

                    return new ApiResponse<UserGroupDto>
                    {
                        Success = true,

                        Message =
                            "Success",

                        Data =
                            result
                    };
                }
                catch (Exception ex)
                {
                    Log.Error(
                        ex,
                        "Error while getting user group by id");

                    throw;
                }
            }

        #endregion

        #endregion
        #region License Management

        #region CREATE

        public async Task<ApiResponse<string>> CreateLicenseManagement(
            LicenseManagementDto dto)
        {
            try
            {
                // Validate DTO
                if (dto == null)
                    throw new CustomException(
                        "License Management details are required.");

                // Validate License Name
                if (string.IsNullOrWhiteSpace(dto.LicenseName))
                    throw new CustomException(
                        "License Name is required.");

                // Validate License Code
                if (string.IsNullOrWhiteSpace(dto.LicenseCode))
                    throw new CustomException(
                        "License Code is required.");

                // Validate Company
                var company =
                    (await _unitOfWork.Repository<Company>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (company == null)
                    throw new CustomException(
                        "Company not found.");

                // Validate License Type
                var licenseType =
                    (await _unitOfWork.Repository<License>()
                        .FindAsync(x =>
                            x.LicenseId == dto.LicenseTypeId))
                    .FirstOrDefault();

                if (licenseType == null)
                    throw new CustomException(
                        "License Type not found.");

                // Validate Maximum Users
                if (dto.MaximumUsers.HasValue &&
                    dto.MaximumUsers.Value < 0)
                {
                    throw new CustomException(
                        "Maximum Users cannot be negative.");
                }

                // Validate Storage Limit
                if (dto.StorageLimitGb.HasValue &&
                    dto.StorageLimitGb.Value < 0)
                {
                    throw new CustomException(
                        "Storage Limit cannot be negative.");
                }

                // Validate API Calls
                if (dto.ApicallsPerMonth.HasValue &&
                    dto.ApicallsPerMonth.Value < 0)
                {
                    throw new CustomException(
                        "API Calls Per Month cannot be negative.");
                }

                // Validate License Dates
                if (dto.LicenseStartDate.HasValue &&
                    dto.LicenseExpiryDate.HasValue)
                {
                    if (dto.LicenseExpiryDate.Value <
                        dto.LicenseStartDate.Value)
                    {
                        throw new CustomException(
                            "License Expiry Date cannot be earlier than License Start Date.");
                    }
                }

                // Duplicate License Name
                var duplicateName =
                    await _unitOfWork.Repository<LicenseManagement>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId &&
                            x.LicenseName.ToLower() ==
                            dto.LicenseName.Trim().ToLower() &&
                            !x.IsDeleted);

                if (duplicateName.Any())
                    throw new CustomException(
                        "License Name already exists for the selected company.");

                // Duplicate License Code
                var duplicateCode =
                    await _unitOfWork.Repository<LicenseManagement>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId &&
                            x.LicenseCode.ToLower() ==
                            dto.LicenseCode.Trim().ToLower() &&
                            !x.IsDeleted);

                if (duplicateCode.Any())
                    throw new CustomException(
                        "License Code already exists for the selected company.");

                // Create Entity
                LicenseManagement licenseManagement =
                    new LicenseManagement
                    {
                        LicenseName =
                            dto.LicenseName.Trim(),

                        LicenseCode =
                            dto.LicenseCode.Trim(),

                        CompanyId =
                            dto.CompanyId,

                        LicenseTypeId =
                            dto.LicenseTypeId,

                        MaximumUsers =
                            dto.MaximumUsers,

                        StorageLimitGb =
                            dto.StorageLimitGb,

                        LicenseStartDate =
                            dto.LicenseStartDate,

                        LicenseExpiryDate =
                            dto.LicenseExpiryDate,

                        ApicallsPerMonth =
                            dto.ApicallsPerMonth,

                        SupportLevel =
                            dto.SupportLevel?.Trim(),

                        Crmmodule =
                            dto.Crmmodule,

                        SalesModule =
                            dto.SalesModule,

                        MarketingModule =
                            dto.MarketingModule,

                        SupportModule =
                            dto.SupportModule,

                        Apiaccess =
                            dto.Apiaccess,

                        MobileApp =
                            dto.MobileApp,

                        ActiveLicense =
                            dto.ActiveLicense,

                        IsDeleted =
                            false,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedAt =
                            DateTime.Now
                    };

                await _unitOfWork.Repository<LicenseManagement>()
                    .AddAsync(licenseManagement);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "LicenseManagement",
                    "INSERT",
                    licenseManagement.LicenseManagementId,
                    "",
                    JsonConvert.SerializeObject(licenseManagement),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "License Management Created Successfully",

                    Data =
                        licenseManagement.LicenseName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating license management");

                throw;
            }
        }

        #endregion


        #region UPDATE

        public async Task<ApiResponse<string>> UpdateLicenseManagement(
            LicenseManagementDto dto)
        {
            try
            {
                // Validate DTO
                if (dto == null)
                    throw new CustomException(
                        "License Management details are required.");

                // Validate ID
                if (dto.LicenseManagementId <= 0)
                    throw new CustomException(
                        "Valid License Management ID is required.");

                // Validate License Name
                if (string.IsNullOrWhiteSpace(dto.LicenseName))
                    throw new CustomException(
                        "License Name is required.");

                // Validate License Code
                if (string.IsNullOrWhiteSpace(dto.LicenseCode))
                    throw new CustomException(
                        "License Code is required.");

                // Validate Maximum Users
                if (dto.MaximumUsers.HasValue &&
                    dto.MaximumUsers.Value < 0)
                {
                    throw new CustomException(
                        "Maximum Users cannot be negative.");
                }

                // Validate Storage Limit
                if (dto.StorageLimitGb.HasValue &&
                    dto.StorageLimitGb.Value < 0)
                {
                    throw new CustomException(
                        "Storage Limit cannot be negative.");
                }

                // Validate API Calls
                if (dto.ApicallsPerMonth.HasValue &&
                    dto.ApicallsPerMonth.Value < 0)
                {
                    throw new CustomException(
                        "API Calls Per Month cannot be negative.");
                }

                // Validate License Dates
                if (dto.LicenseStartDate.HasValue &&
                    dto.LicenseExpiryDate.HasValue)
                {
                    if (dto.LicenseExpiryDate.Value <
                        dto.LicenseStartDate.Value)
                    {
                        throw new CustomException(
                            "License Expiry Date cannot be earlier than License Start Date.");
                    }
                }

                // Get Existing License Management
                var licenseManagement =
                    (await _unitOfWork.Repository<LicenseManagement>()
                        .FindAsync(x =>
                            x.LicenseManagementId ==
                            dto.LicenseManagementId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (licenseManagement == null)
                    throw new CustomException(
                        "License Management not found.");

                // Validate Company
                var company =
                    (await _unitOfWork.Repository<Company>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (company == null)
                    throw new CustomException(
                        "Company not found.");

                // Validate License Type
                var licenseType =
                    (await _unitOfWork.Repository<License>()
                        .FindAsync(x =>
                            x.LicenseId == dto.LicenseTypeId))
                    .FirstOrDefault();

                if (licenseType == null)
                    throw new CustomException(
                        "License Type not found.");

                // Duplicate License Name
                var duplicateName =
                    await _unitOfWork.Repository<LicenseManagement>()
                        .FindAsync(x =>
                            x.LicenseManagementId !=
                            dto.LicenseManagementId &&

                            x.CompanyId ==
                            dto.CompanyId &&

                            x.LicenseName.ToLower() ==
                            dto.LicenseName.Trim().ToLower() &&

                            !x.IsDeleted);

                if (duplicateName.Any())
                    throw new CustomException(
                        "License Name already exists for the selected company.");

                // Duplicate License Code
                var duplicateCode =
                    await _unitOfWork.Repository<LicenseManagement>()
                        .FindAsync(x =>
                            x.LicenseManagementId !=
                            dto.LicenseManagementId &&

                            x.CompanyId ==
                            dto.CompanyId &&

                            x.LicenseCode.ToLower() ==
                            dto.LicenseCode.Trim().ToLower() &&

                            !x.IsDeleted);

                if (duplicateCode.Any())
                    throw new CustomException(
                        "License Code already exists for the selected company.");

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(licenseManagement);

                // Update Entity
                licenseManagement.LicenseName =
                    dto.LicenseName.Trim();

                licenseManagement.LicenseCode =
                    dto.LicenseCode.Trim();

                licenseManagement.CompanyId =
                    dto.CompanyId;

                licenseManagement.LicenseTypeId =
                    dto.LicenseTypeId;

                licenseManagement.MaximumUsers =
                    dto.MaximumUsers;

                licenseManagement.StorageLimitGb =
                    dto.StorageLimitGb;

                licenseManagement.LicenseStartDate =
                    dto.LicenseStartDate;

                licenseManagement.LicenseExpiryDate =
                    dto.LicenseExpiryDate;

                licenseManagement.ApicallsPerMonth =
                    dto.ApicallsPerMonth;

                licenseManagement.SupportLevel =
                    dto.SupportLevel?.Trim();

                licenseManagement.Crmmodule =
                    dto.Crmmodule;

                licenseManagement.SalesModule =
                    dto.SalesModule;

                licenseManagement.MarketingModule =
                    dto.MarketingModule;

                licenseManagement.SupportModule =
                    dto.SupportModule;

                licenseManagement.Apiaccess =
                    dto.Apiaccess;

                licenseManagement.MobileApp =
                    dto.MobileApp;

                licenseManagement.ActiveLicense =
                    dto.ActiveLicense;

                licenseManagement.ModifiedBy =
                    _currentUserService.UserId;

                licenseManagement.ModifiedAt =
                    DateTime.Now;

                _unitOfWork.Repository<LicenseManagement>()
                    .Update(licenseManagement);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "LicenseManagement",
                    "UPDATE",
                    licenseManagement.LicenseManagementId,
                    oldValues,
                    JsonConvert.SerializeObject(licenseManagement),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "License Management Updated Successfully",

                    Data =
                        licenseManagement.LicenseName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating license management");

                throw;
            }
        }

        #endregion


        #region DELETE

        public async Task<ApiResponse<string>> DeleteLicenseManagement(
            int id)
        {
            try
            {
                var licenseManagement =
                    (await _unitOfWork.Repository<LicenseManagement>()
                        .FindAsync(x =>
                            x.LicenseManagementId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (licenseManagement == null)
                    throw new CustomException(
                        "License Management not found.");

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(licenseManagement);

                // Soft Delete
                licenseManagement.IsDeleted =
                    true;

                licenseManagement.ModifiedBy =
                    _currentUserService.UserId;

                licenseManagement.ModifiedAt =
                    DateTime.Now;

                _unitOfWork.Repository<LicenseManagement>()
                    .Update(licenseManagement);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "LicenseManagement",
                    "DELETE",
                    licenseManagement.LicenseManagementId,
                    oldValues,
                    JsonConvert.SerializeObject(licenseManagement),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "License Management Deleted Successfully",

                    Data =
                        licenseManagement.LicenseName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting license management");

                throw;
            }
        }

        #endregion


        #region GET ALL

        public async Task<ApiResponse<List<LicenseManagementDto>>>
            GetLicenseManagements()
        {
            try
            {
                var licenseManagements =
                    (await _unitOfWork.Repository<LicenseManagement>()
                        .FindAsync(x =>
                            !x.IsDeleted))
                    .OrderByDescending(x =>
                        x.LicenseManagementId)
                    .ToList();

                var result =
                    licenseManagements.Select(x =>
                        new LicenseManagementDto
                        {
                            LicenseManagementId =
                                x.LicenseManagementId,

                            LicenseName =
                                x.LicenseName,

                            LicenseCode =
                                x.LicenseCode,

                            CompanyId =
                                x.CompanyId,

                            LicenseTypeId =
                                x.LicenseTypeId,

                            MaximumUsers =
                                x.MaximumUsers,

                            StorageLimitGb =
                                x.StorageLimitGb,

                            LicenseStartDate =
                                x.LicenseStartDate,

                            LicenseExpiryDate =
                                x.LicenseExpiryDate,

                            ApicallsPerMonth =
                                x.ApicallsPerMonth,

                            SupportLevel =
                                x.SupportLevel,

                            Crmmodule =
                                x.Crmmodule,

                            SalesModule =
                                x.SalesModule,

                            MarketingModule =
                                x.MarketingModule,

                            SupportModule =
                                x.SupportModule,

                            Apiaccess =
                                x.Apiaccess,

                            MobileApp =
                                x.MobileApp,

                            ActiveLicense =
                                x.ActiveLicense
                        })
                    .ToList();

                return new ApiResponse<List<LicenseManagementDto>>
                {
                    Success = true,

                    Message =
                        "Success",

                    Data =
                        result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting license managements");

                throw;
            }
        }

        #endregion


        #region GET BY ID

        public async Task<ApiResponse<LicenseManagementDto>>
            GetLicenseManagementById(int id)
        {
            try
            {
                var licenseManagement =
                    (await _unitOfWork.Repository<LicenseManagement>()
                        .FindAsync(x =>
                            x.LicenseManagementId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (licenseManagement == null)
                    throw new CustomException(
                        "License Management not found.");

                var result =
                    new LicenseManagementDto
                    {
                        LicenseManagementId =
                            licenseManagement.LicenseManagementId,

                        LicenseName =
                            licenseManagement.LicenseName,

                        LicenseCode =
                            licenseManagement.LicenseCode,

                        CompanyId =
                            licenseManagement.CompanyId,

                        LicenseTypeId =
                            licenseManagement.LicenseTypeId,

                        MaximumUsers =
                            licenseManagement.MaximumUsers,

                        StorageLimitGb =
                            licenseManagement.StorageLimitGb,

                        LicenseStartDate =
                            licenseManagement.LicenseStartDate,

                        LicenseExpiryDate =
                            licenseManagement.LicenseExpiryDate,

                        ApicallsPerMonth =
                            licenseManagement.ApicallsPerMonth,

                        SupportLevel =
                            licenseManagement.SupportLevel,

                        Crmmodule =
                            licenseManagement.Crmmodule,

                        SalesModule =
                            licenseManagement.SalesModule,

                        MarketingModule =
                            licenseManagement.MarketingModule,

                        SupportModule =
                            licenseManagement.SupportModule,

                        Apiaccess =
                            licenseManagement.Apiaccess,

                        MobileApp =
                            licenseManagement.MobileApp,

                        ActiveLicense =
                            licenseManagement.ActiveLicense
                    };

                return new ApiResponse<LicenseManagementDto>
                {
                    Success = true,

                    Message =
                        "Success",

                    Data =
                        result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting license management by id");

                throw;
            }
        }

        #endregion

        #endregion

        #region Password Policy

        #region CREATE

        public async Task<ApiResponse<string>> CreatePasswordPolicy(
            PasswordPolicyDto dto)
        {
            try
            {
                // Validate Policy Name
                if (string.IsNullOrWhiteSpace(dto.PolicyName))
                    throw new CustomException(
                        "Policy Name is required.");

                // Validate Minimum Password Length
                if (dto.MinimumPasswordLength <= 0)
                    throw new CustomException(
                        "Minimum Password Length must be greater than zero.");

                // Validate Maximum Password Length
                if (dto.MaximumPasswordLength <= 0)
                    throw new CustomException(
                        "Maximum Password Length must be greater than zero.");

                // Validate Min / Max
                if (dto.MinimumPasswordLength >
                    dto.MaximumPasswordLength)
                {
                    throw new CustomException(
                        "Minimum Password Length cannot be greater than Maximum Password Length.");
                }

                // Validate Password Expiry Days
                if (dto.PasswordExpiryDays.HasValue &&
                    dto.PasswordExpiryDays.Value < 0)
                {
                    throw new CustomException(
                        "Password Expiry Days cannot be negative.");
                }

                // Validate Password History
                if (dto.PasswordHistory.HasValue &&
                    dto.PasswordHistory.Value < 0)
                {
                    throw new CustomException(
                        "Password History cannot be negative.");
                }

                // Validate Maximum Failed Login Attempts
                if (dto.MaximumFailedLoginAttempts.HasValue &&
                    dto.MaximumFailedLoginAttempts.Value <= 0)
                {
                    throw new CustomException(
                        "Maximum Failed Login Attempts must be greater than zero.");
                }

                // Validate Account Lock Duration
                if (dto.AccountLockDurationMinutes.HasValue &&
                    dto.AccountLockDurationMinutes.Value <= 0)
                {
                    throw new CustomException(
                        "Account Lock Duration must be greater than zero.");
                }

                // Validate Session Timeout
                if (dto.SessionTimeoutMinutes.HasValue &&
                    dto.SessionTimeoutMinutes.Value <= 0)
                {
                    throw new CustomException(
                        "Session Timeout must be greater than zero.");
                }

                // Duplicate Policy Name
                var duplicate =
                    await _unitOfWork.Repository<PasswordPolicy>()
                        .FindAsync(x =>
                            !x.IsDeleted &&
                            x.PolicyName.ToLower() ==
                            dto.PolicyName.Trim().ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "Policy Name already exists.");

                // Create Entity
                PasswordPolicy passwordPolicy =
                    new PasswordPolicy
                    {
                        PolicyName =
                            dto.PolicyName.Trim(),

                        MinimumPasswordLength =
                            dto.MinimumPasswordLength,

                        MaximumPasswordLength =
                            dto.MaximumPasswordLength,

                        PasswordExpiryDays =
                            dto.PasswordExpiryDays,

                        PasswordHistory =
                            dto.PasswordHistory,

                        MaximumFailedLoginAttempts =
                            dto.MaximumFailedLoginAttempts,

                        AccountLockDurationMinutes =
                            dto.AccountLockDurationMinutes,

                        SessionTimeoutMinutes =
                            dto.SessionTimeoutMinutes,

                        Mfarequirement =
                            dto.Mfarequirement,

                        RequireUppercaseLetter =
                            dto.RequireUppercaseLetter,

                        RequireLowercaseLetter =
                            dto.RequireLowercaseLetter,

                        RequireNumber =
                            dto.RequireNumber,

                        RequireSpecialCharacter =
                            dto.RequireSpecialCharacter,

                        PreventUsernameInPassword =
                            dto.PreventUsernameInPassword,

                        ForcePasswordChangeOnFirstLogin =
                            dto.ForcePasswordChangeOnFirstLogin,

                        AllowPasswordReuse =
                            dto.AllowPasswordReuse,

                        PolicyActive =
                            dto.PolicyActive,

                        IsDeleted =
                            false,

                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedAt =
                            DateTime.Now
                    };

                await _unitOfWork.Repository<PasswordPolicy>()
                    .AddAsync(passwordPolicy);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "PasswordPolicy",
                    "INSERT",
                    passwordPolicy.PasswordPolicyId,
                    "",
                    JsonConvert.SerializeObject(passwordPolicy),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Password Policy Created Successfully",

                    Data =
                        passwordPolicy.PolicyName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating password policy");

                throw;
            }
        }

        #endregion


        #region UPDATE

        public async Task<ApiResponse<string>> UpdatePasswordPolicy(
            PasswordPolicyDto dto)
        {
            try
            {
                // Validate Policy Name
                if (string.IsNullOrWhiteSpace(dto.PolicyName))
                    throw new CustomException(
                        "Policy Name is required.");

                if (dto.PasswordPolicyId <= 0)
                    throw new CustomException(
                        "Valid Password Policy ID is required.");

                // Validate Minimum Password Length
                if (dto.MinimumPasswordLength <= 0)
                    throw new CustomException(
                        "Minimum Password Length must be greater than zero.");

                // Validate Maximum Password Length
                if (dto.MaximumPasswordLength <= 0)
                    throw new CustomException(
                        "Maximum Password Length must be greater than zero.");

                // Validate Min / Max
                if (dto.MinimumPasswordLength >
                    dto.MaximumPasswordLength)
                {
                    throw new CustomException(
                        "Minimum Password Length cannot be greater than Maximum Password Length.");
                }

                // Validate Password Expiry Days
                if (dto.PasswordExpiryDays.HasValue &&
                    dto.PasswordExpiryDays.Value < 0)
                {
                    throw new CustomException(
                        "Password Expiry Days cannot be negative.");
                }

                // Validate Password History
                if (dto.PasswordHistory.HasValue &&
                    dto.PasswordHistory.Value < 0)
                {
                    throw new CustomException(
                        "Password History cannot be negative.");
                }

                // Validate Maximum Failed Login Attempts
                if (dto.MaximumFailedLoginAttempts.HasValue &&
                    dto.MaximumFailedLoginAttempts.Value <= 0)
                {
                    throw new CustomException(
                        "Maximum Failed Login Attempts must be greater than zero.");
                }

                // Validate Account Lock Duration
                if (dto.AccountLockDurationMinutes.HasValue &&
                    dto.AccountLockDurationMinutes.Value <= 0)
                {
                    throw new CustomException(
                        "Account Lock Duration must be greater than zero.");
                }

                // Validate Session Timeout
                if (dto.SessionTimeoutMinutes.HasValue &&
                    dto.SessionTimeoutMinutes.Value <= 0)
                {
                    throw new CustomException(
                        "Session Timeout must be greater than zero.");
                }

                // Get Existing Policy
                var passwordPolicy =
                    (await _unitOfWork.Repository<PasswordPolicy>()
                        .FindAsync(x =>
                            x.PasswordPolicyId ==
                            dto.PasswordPolicyId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (passwordPolicy == null)
                    throw new CustomException(
                        "Password Policy not found.");

                // Duplicate Policy Name
                var duplicate =
                    await _unitOfWork.Repository<PasswordPolicy>()
                        .FindAsync(x =>
                            x.PasswordPolicyId !=
                            dto.PasswordPolicyId &&

                            !x.IsDeleted &&

                            x.PolicyName.ToLower() ==
                            dto.PolicyName.Trim().ToLower());

                if (duplicate.Any())
                    throw new CustomException(
                        "Policy Name already exists.");

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(passwordPolicy);

                // Update
                passwordPolicy.PolicyName =
                    dto.PolicyName.Trim();

                passwordPolicy.MinimumPasswordLength =
                    dto.MinimumPasswordLength;

                passwordPolicy.MaximumPasswordLength =
                    dto.MaximumPasswordLength;

                passwordPolicy.PasswordExpiryDays =
                    dto.PasswordExpiryDays;

                passwordPolicy.PasswordHistory =
                    dto.PasswordHistory;

                passwordPolicy.MaximumFailedLoginAttempts =
                    dto.MaximumFailedLoginAttempts;

                passwordPolicy.AccountLockDurationMinutes =
                    dto.AccountLockDurationMinutes;

                passwordPolicy.SessionTimeoutMinutes =
                    dto.SessionTimeoutMinutes;

                passwordPolicy.Mfarequirement =
                    dto.Mfarequirement;

                passwordPolicy.RequireUppercaseLetter =
                    dto.RequireUppercaseLetter;

                passwordPolicy.RequireLowercaseLetter =
                    dto.RequireLowercaseLetter;

                passwordPolicy.RequireNumber =
                    dto.RequireNumber;

                passwordPolicy.RequireSpecialCharacter =
                    dto.RequireSpecialCharacter;

                passwordPolicy.PreventUsernameInPassword =
                    dto.PreventUsernameInPassword;

                passwordPolicy.ForcePasswordChangeOnFirstLogin =
                    dto.ForcePasswordChangeOnFirstLogin;

                passwordPolicy.AllowPasswordReuse =
                    dto.AllowPasswordReuse;

                passwordPolicy.PolicyActive =
                    dto.PolicyActive;

                passwordPolicy.ModifiedBy =
                    _currentUserService.UserId;

                passwordPolicy.ModifiedAt =
                    DateTime.Now;

                _unitOfWork.Repository<PasswordPolicy>()
                    .Update(passwordPolicy);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "PasswordPolicy",
                    "UPDATE",
                    passwordPolicy.PasswordPolicyId,
                    oldValues,
                    JsonConvert.SerializeObject(passwordPolicy),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Password Policy Updated Successfully",

                    Data =
                        passwordPolicy.PolicyName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating password policy");

                throw;
            }
        }

        #endregion


        #region DELETE

        public async Task<ApiResponse<string>> DeletePasswordPolicy(
            int id)
        {
            try
            {
                // Get Existing Policy
                var passwordPolicy =
                    (await _unitOfWork.Repository<PasswordPolicy>()
                        .FindAsync(x =>
                            x.PasswordPolicyId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (passwordPolicy == null)
                    throw new CustomException(
                        "Password Policy not found.");

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(passwordPolicy);

                // Soft Delete
                passwordPolicy.IsDeleted =
                    true;

                passwordPolicy.ModifiedBy =
                    _currentUserService.UserId;

                passwordPolicy.ModifiedAt =
                    DateTime.Now;

                _unitOfWork.Repository<PasswordPolicy>()
                    .Update(passwordPolicy);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "PasswordPolicy",
                    "DELETE",
                    passwordPolicy.PasswordPolicyId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,

                    Message =
                        "Password Policy Deleted Successfully",

                    Data =
                        passwordPolicy.PolicyName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting password policy");

                throw;
            }
        }

        #endregion


        #region GET ALL

        public async Task<ApiResponse<List<PasswordPolicyDto>>>
            GetPasswordPolicies()
        {
            try
            {
                var passwordPolicies =
                    (await _unitOfWork.Repository<PasswordPolicy>()
                        .FindAsync(x =>
                            !x.IsDeleted))
                    .OrderByDescending(x =>
                        x.PasswordPolicyId)
                    .ToList();

                var result =
                    passwordPolicies.Select(x =>
                        new PasswordPolicyDto
                        {
                            PasswordPolicyId =
                                x.PasswordPolicyId,

                            PolicyName =
                                x.PolicyName,

                            MinimumPasswordLength =
                                x.MinimumPasswordLength,

                            MaximumPasswordLength =
                                x.MaximumPasswordLength,

                            PasswordExpiryDays =
                                x.PasswordExpiryDays,

                            PasswordHistory =
                                x.PasswordHistory,

                            MaximumFailedLoginAttempts =
                                x.MaximumFailedLoginAttempts,

                            AccountLockDurationMinutes =
                                x.AccountLockDurationMinutes,

                            SessionTimeoutMinutes =
                                x.SessionTimeoutMinutes,

                            Mfarequirement =
                                x.Mfarequirement,

                            RequireUppercaseLetter =
                                x.RequireUppercaseLetter,

                            RequireLowercaseLetter =
                                x.RequireLowercaseLetter,

                            RequireNumber =
                                x.RequireNumber,

                            RequireSpecialCharacter =
                                x.RequireSpecialCharacter,

                            PreventUsernameInPassword =
                                x.PreventUsernameInPassword,

                            ForcePasswordChangeOnFirstLogin =
                                x.ForcePasswordChangeOnFirstLogin,

                            AllowPasswordReuse =
                                x.AllowPasswordReuse,

                            PolicyActive =
                                x.PolicyActive
                        })
                    .ToList();

                return new ApiResponse<List<PasswordPolicyDto>>
                {
                    Success = true,

                    Message =
                        "Success",

                    Data =
                        result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting password policies");

                throw;
            }
        }

        #endregion


        #region GET BY ID

        public async Task<ApiResponse<PasswordPolicyDto>>
            GetPasswordPolicyById(int id)
        {
            try
            {
                var passwordPolicy =
                    (await _unitOfWork.Repository<PasswordPolicy>()
                        .FindAsync(x =>
                            x.PasswordPolicyId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (passwordPolicy == null)
                    throw new CustomException(
                        "Password Policy not found.");

                var result =
                    new PasswordPolicyDto
                    {
                        PasswordPolicyId =
                            passwordPolicy.PasswordPolicyId,

                        PolicyName =
                            passwordPolicy.PolicyName,

                        MinimumPasswordLength =
                            passwordPolicy.MinimumPasswordLength,

                        MaximumPasswordLength =
                            passwordPolicy.MaximumPasswordLength,

                        PasswordExpiryDays =
                            passwordPolicy.PasswordExpiryDays,

                        PasswordHistory =
                            passwordPolicy.PasswordHistory,

                        MaximumFailedLoginAttempts =
                            passwordPolicy.MaximumFailedLoginAttempts,

                        AccountLockDurationMinutes =
                            passwordPolicy.AccountLockDurationMinutes,

                        SessionTimeoutMinutes =
                            passwordPolicy.SessionTimeoutMinutes,

                        Mfarequirement =
                            passwordPolicy.Mfarequirement,

                        RequireUppercaseLetter =
                            passwordPolicy.RequireUppercaseLetter,

                        RequireLowercaseLetter =
                            passwordPolicy.RequireLowercaseLetter,

                        RequireNumber =
                            passwordPolicy.RequireNumber,

                        RequireSpecialCharacter =
                            passwordPolicy.RequireSpecialCharacter,

                        PreventUsernameInPassword =
                            passwordPolicy.PreventUsernameInPassword,

                        ForcePasswordChangeOnFirstLogin =
                            passwordPolicy.ForcePasswordChangeOnFirstLogin,

                        AllowPasswordReuse =
                            passwordPolicy.AllowPasswordReuse,

                        PolicyActive =
                            passwordPolicy.PolicyActive
                    };

                return new ApiResponse<PasswordPolicyDto>
                {
                    Success = true,

                    Message =
                        "Success",

                    Data =
                        result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting password policy by id");

                throw;
            }
        }

        #endregion

        #endregion
    }
}


