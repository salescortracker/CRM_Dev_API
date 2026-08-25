using Business_Layer.DTOs.MasterDTO_s;
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
    public class LeadService : ILeadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public LeadService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }


        // =====================================================
        // CREATE LEAD
        // =====================================================

        public async Task<ApiResponse<string>> CreateLead(LeadDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.FirstName))
                    throw new CustomException("First Name is required.");

                if (string.IsNullOrWhiteSpace(dto.LastName))
                    throw new CustomException("Last Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Email))
                    throw new CustomException("Email is required.");

                if (string.IsNullOrWhiteSpace(dto.CompanyName))
                    throw new CustomException("Company Name is required.");

                if (dto.LeadTypeId <= 0)
                    throw new CustomException("Lead Type is required.");

                if (dto.LeadSourceId <= 0)
                    throw new CustomException("Lead Source is required.");


                // ---------------------------------------------
                // DUPLICATE EMAIL
                // ---------------------------------------------

                var duplicateEmail =
                    await _unitOfWork.Repository<LeadInformation>()
                    .FindAsync(x =>
                        x.Email.ToLower() == dto.Email.ToLower() &&
                        x.CrmcompanyId == dto.CrmcompanyId &&
                        !x.IsDeleted);

                if (duplicateEmail.Any())
                    throw new CustomException(
                        "A lead with this email already exists.");


                // ---------------------------------------------
                // GENERATE LEAD NUMBER
                // ---------------------------------------------

                var existingLeads =
                    await _unitOfWork.Repository<LeadInformation>()
                    .GetAllAsync();

                int nextNumber = existingLeads.Count() + 1;

                string leadNumber =
                    $"LD-{DateTime.Now:yyyy}-{nextNumber:D5}";


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                LeadInformation lead = new LeadInformation
                {
                    LeadNumber = leadNumber,

                    Salutation = dto.Salutation,
                    FirstName = dto.FirstName.Trim(),
                    LastName = dto.LastName.Trim(),
                    JobTitle = dto.JobTitle,

                    Email = dto.Email.Trim(),
                    Phone = dto.Phone,
                    Mobile = dto.Mobile,

                    LeadTypeId = dto.LeadTypeId,
                    LeadOwnerId = dto.LeadOwnerId,
                    LeadSourceId = dto.LeadSourceId,

                    LeadStatus =
                        string.IsNullOrWhiteSpace(dto.LeadStatus)
                            ? "New"
                            : dto.LeadStatus,

                    LeadRating = dto.LeadRating,
                    LeadScore = dto.LeadScore,

                    PreferredContactMethod =
                        dto.PreferredContactMethod,

                    CompanyName = dto.CompanyName.Trim(),
                    Website = dto.Website,

                    IndustryId = dto.IndustryId,

                    CompanySize = dto.CompanySize,
                    AnnualRevenue = dto.AnnualRevenue,

                    StreetAddress = dto.StreetAddress,
                    City = dto.City,

                    StateId = dto.StateId,
                    PostalCode = dto.PostalCode,
                    CountryId = dto.CountryId,

                    EstimatedDealValue = dto.EstimatedDealValue,
                    ExpectedCloseDate = dto.ExpectedCloseDate,
                    Description = dto.Description,

                    CompanyId = dto.CompanyId,
                    PrimaryContactId = dto.PrimaryContactId,

                    CrmcompanyId = dto.CrmcompanyId,
                    RegionId = dto.RegionId,

                    IsActive = true,
                    IsDeleted = false,

                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };


                await _unitOfWork
                    .Repository<LeadInformation>()
                    .AddAsync(lead);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT LOG
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "LeadInformation",
                    "INSERT",
                    lead.LeadId,
                    "",
                    JsonConvert.SerializeObject(lead),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Lead Created Successfully",
                    Data = lead.LeadNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating lead");
                throw;
            }
        }


        // =====================================================
        // UPDATE LEAD
        // =====================================================

        public async Task<ApiResponse<string>> UpdateLead(LeadDto dto)
        {
            try
            {
                var lead =
                    (await _unitOfWork.Repository<LeadInformation>()
                        .FindAsync(x =>
                            x.LeadId == dto.LeadId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (lead == null)
                    throw new CustomException("Lead not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.FirstName))
                    throw new CustomException("First Name is required.");

                if (string.IsNullOrWhiteSpace(dto.LastName))
                    throw new CustomException("Last Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Email))
                    throw new CustomException("Email is required.");

                if (string.IsNullOrWhiteSpace(dto.CompanyName))
                    throw new CustomException("Company Name is required.");


                // ---------------------------------------------
                // DUPLICATE EMAIL
                // ---------------------------------------------

                var duplicateEmail =
                    await _unitOfWork.Repository<LeadInformation>()
                    .FindAsync(x =>
                        x.LeadId != dto.LeadId &&
                        x.Email.ToLower() == dto.Email.ToLower() &&
                        x.CrmcompanyId == dto.CrmcompanyId &&
                        !x.IsDeleted);

                if (duplicateEmail.Any())
                    throw new CustomException(
                        "A lead with this email already exists.");


                string oldValues =
                    JsonConvert.SerializeObject(lead);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                lead.Salutation = dto.Salutation;

                lead.FirstName = dto.FirstName.Trim();
                lead.LastName = dto.LastName.Trim();

                lead.JobTitle = dto.JobTitle;

                lead.Email = dto.Email.Trim();
                lead.Phone = dto.Phone;
                lead.Mobile = dto.Mobile;

                lead.LeadTypeId = dto.LeadTypeId;
                lead.LeadOwnerId = dto.LeadOwnerId;
                lead.LeadSourceId = dto.LeadSourceId;

                lead.LeadStatus = dto.LeadStatus;
                lead.LeadRating = dto.LeadRating;
                lead.LeadScore = dto.LeadScore;

                lead.PreferredContactMethod =
                    dto.PreferredContactMethod;


                lead.CompanyName = dto.CompanyName.Trim();
                lead.Website = dto.Website;

                lead.IndustryId = dto.IndustryId;

                lead.CompanySize = dto.CompanySize;
                lead.AnnualRevenue = dto.AnnualRevenue;

                lead.StreetAddress = dto.StreetAddress;
                lead.City = dto.City;

                lead.StateId = dto.StateId;
                lead.PostalCode = dto.PostalCode;
                lead.CountryId = dto.CountryId;


                lead.EstimatedDealValue =
                    dto.EstimatedDealValue;

                lead.ExpectedCloseDate =
                    dto.ExpectedCloseDate;

                lead.Description =
                    dto.Description;


                lead.CompanyId =
                    dto.CompanyId;

                lead.PrimaryContactId =
                    dto.PrimaryContactId;


                lead.CrmcompanyId =
                    dto.CrmcompanyId;

                lead.RegionId =
                    dto.RegionId;

                lead.IsActive =
                    dto.IsActive;

                lead.ModifiedBy =
                    _currentUserService.UserId;

                lead.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<LeadInformation>()
                    .Update(lead);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "LeadInformation",
                    "UPDATE",
                    lead.LeadId,
                    oldValues,
                    JsonConvert.SerializeObject(lead),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Lead Updated Successfully",
                    Data = lead.LeadNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating lead");
                throw;
            }
        }


        // =====================================================
        // DELETE LEAD
        // =====================================================

        public async Task<ApiResponse<string>> DeleteLead(int id)
        {
            try
            {
                var lead =
                    (await _unitOfWork.Repository<LeadInformation>()
                        .FindAsync(x =>
                            x.LeadId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (lead == null)
                    throw new CustomException("Lead not found.");


                string oldValues =
                    JsonConvert.SerializeObject(lead);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                lead.IsDeleted = true;
                lead.IsActive = false;

                lead.ModifiedBy =
                    _currentUserService.UserId;

                lead.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<LeadInformation>()
                    .Update(lead);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "LeadInformation",
                    "DELETE",
                    lead.LeadId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Lead Deleted Successfully",
                    Data = lead.LeadNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting lead");
                throw;
            }
        }


        // =====================================================
        // GET ALL LEADS
        // =====================================================

        public async Task<ApiResponse<List<LeadDto>>> GetLeads()
        {
            var leads =
                await _unitOfWork.Repository<LeadInformation>()
                    .GetAllAsync();

            var leadTypes =
                await _unitOfWork.Repository<LeadType>()
                    .GetAllAsync();

            var leadSources =
                await _unitOfWork.Repository<LeadSourceDatum>()
                    .GetAllAsync();

            var industries =
                await _unitOfWork.Repository<Industry>()
                    .GetAllAsync();

            var countries =
                await _unitOfWork.Repository<Country>()
                    .GetAllAsync();

            var states =
                await _unitOfWork.Repository<StateMaster>()
                    .GetAllAsync();


            var result =
                (from l in leads

                 join lt in leadTypes
                     on l.LeadTypeId equals lt.LeadTypeId
                     into ltGroup
                 from lt in ltGroup.DefaultIfEmpty()

                 join ls in leadSources
                     on l.LeadSourceId equals ls.LeadSourceId
                     into lsGroup
                 from ls in lsGroup.DefaultIfEmpty()

                 join ind in industries
                     on l.IndustryId equals ind.IndustryId
                     into indGroup
                 from ind in indGroup.DefaultIfEmpty()

                 join c in countries
                     on l.CountryId equals c.CountryId
                     into countryGroup
                 from c in countryGroup.DefaultIfEmpty()

                 join s in states
                     on l.StateId equals s.StateId
                     into stateGroup
                 from s in stateGroup.DefaultIfEmpty()

                 where !l.IsDeleted

                 select new LeadDto
                 {
                     LeadId = l.LeadId,

                     LeadNumber = l.LeadNumber,

                     Salutation = l.Salutation,

                     FirstName = l.FirstName,
                     LastName = l.LastName,

                     JobTitle = l.JobTitle,

                     Email = l.Email,
                     Phone = l.Phone,
                     Mobile = l.Mobile,

                     LeadTypeId = l.LeadTypeId,
                     LeadTypeName =
                         lt != null
                             ? lt.LeadTypeName
                             : null,

                     LeadOwnerId = l.LeadOwnerId,

                     LeadSourceId = l.LeadSourceId,
                     LeadSourceName =
                         ls != null
                             ? ls.LeadSourceName
                             : null,

                     LeadStatus = l.LeadStatus,

                     LeadRating = l.LeadRating,

                     LeadScore = l.LeadScore,

                     PreferredContactMethod =
                         l.PreferredContactMethod,


                     CompanyName = l.CompanyName,

                     Website = l.Website,

                     IndustryId = l.IndustryId,
                     IndustryName =
                         ind != null
                             ? ind.IndustryName
                             : null,

                     CompanySize = l.CompanySize,

                     AnnualRevenue = l.AnnualRevenue,

                     StreetAddress = l.StreetAddress,

                     City = l.City,

                     StateId = l.StateId,
                     StateName =
                         s != null
                             ? s.StateName
                             : null,

                     PostalCode = l.PostalCode,

                     CountryId = l.CountryId,
                     CountryName =
                         c != null
                             ? c.CountryName
                             : null,


                     EstimatedDealValue =
                         l.EstimatedDealValue,

                     ExpectedCloseDate =
                         l.ExpectedCloseDate,

                     Description =
                         l.Description,


                     CompanyId =
                         l.CompanyId,

                     PrimaryContactId =
                         l.PrimaryContactId,


                     CrmcompanyId =
                         l.CrmcompanyId,

                     RegionId =
                         l.RegionId,

                     IsActive =
                         l.IsActive,

                     CreatedAt =
                         l.CreatedAt,

                     ModifiedAt =
                         l.ModifiedAt
                 })
                .OrderByDescending(x => x.LeadId)
                .ToList();


            return new ApiResponse<List<LeadDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        // =====================================================
        // GET LEAD BY ID
        // =====================================================

        public async Task<ApiResponse<LeadDto>> GetLeadById(int id)
        {
            var lead =
                (await _unitOfWork.Repository<LeadInformation>()
                    .FindAsync(x =>
                        x.LeadId == id &&
                        !x.IsDeleted))
                .FirstOrDefault();

            if (lead == null)
                throw new CustomException("Lead not found.");


            return new ApiResponse<LeadDto>
            {
                Success = true,
                Message = "Success",

                Data = new LeadDto
                {
                    LeadId = lead.LeadId,

                    LeadNumber = lead.LeadNumber,

                    Salutation = lead.Salutation,

                    FirstName = lead.FirstName,
                    LastName = lead.LastName,

                    JobTitle = lead.JobTitle,

                    Email = lead.Email,
                    Phone = lead.Phone,
                    Mobile = lead.Mobile,

                    LeadTypeId = lead.LeadTypeId,

                    LeadOwnerId = lead.LeadOwnerId,

                    LeadSourceId = lead.LeadSourceId,

                    LeadStatus = lead.LeadStatus,

                    LeadRating = lead.LeadRating,

                    LeadScore = lead.LeadScore,

                    PreferredContactMethod =
                        lead.PreferredContactMethod,

                    CompanyName =
                        lead.CompanyName,

                    Website =
                        lead.Website,

                    IndustryId =
                        lead.IndustryId,

                    CompanySize =
                        lead.CompanySize,

                    AnnualRevenue =
                        lead.AnnualRevenue,

                    StreetAddress =
                        lead.StreetAddress,

                    City =
                        lead.City,

                    StateId =
                        lead.StateId,

                    PostalCode =
                        lead.PostalCode,

                    CountryId =
                        lead.CountryId,

                    EstimatedDealValue =
                        lead.EstimatedDealValue,

                    ExpectedCloseDate =
                        lead.ExpectedCloseDate,

                    Description =
                        lead.Description,

                    CompanyId =
                        lead.CompanyId,

                    PrimaryContactId =
                        lead.PrimaryContactId,

                    CrmcompanyId =
                        lead.CrmcompanyId,

                    RegionId =
                        lead.RegionId,

                    IsActive =
                        lead.IsActive,

                    CreatedAt =
                        lead.CreatedAt,

                    ModifiedAt =
                        lead.ModifiedAt
                }
            };
        }


        // =====================================================
        // GET LEAD TYPES
        // =====================================================

        public async Task<ApiResponse<List<LeadTypeDto>>> GetLeadTypes()
        {
            var data =
                await _unitOfWork.Repository<LeadType>()
                    .GetAllAsync();

            var result = data
                .Where(x =>
                    x.IsActive &&
                    !x.IsDeleted)
                .OrderBy(x => x.LeadTypeName)
                .Select(x => new LeadTypeDto
                {
                    LeadTypeId = x.LeadTypeId,
                    LeadTypeName = x.LeadTypeName
                })
                .ToList();

            return new ApiResponse<List<LeadTypeDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        // =====================================================
        // GET LEAD SOURCES
        // =====================================================

        public async Task<ApiResponse<List<LeadSourceDto>>> GetLeadSources()
        {
            var data =
                await _unitOfWork.Repository<LeadSourceDatum>()
                    .GetAllAsync();

            var result = data
                .Where(x =>
                    x.IsActive &&
                    !x.IsDeleted)
                .OrderBy(x => x.LeadSourceName)
                .Select(x => new LeadSourceDto
                {
                    LeadSourceId = x.LeadSourceId,
                    LeadSourceName = x.LeadSourceName
                })
                .ToList();

            return new ApiResponse<List<LeadSourceDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        // =====================================================
        // GET INDUSTRIES
        // =====================================================

        public async Task<ApiResponse<List<IndustryDto>>> GetIndustries()
        {
            var data =
                await _unitOfWork.Repository<Industry>()
                    .GetAllAsync();

            var result = data
                .Where(x =>
                    x.IsActive &&
                    !x.IsDeleted)
                .OrderBy(x => x.IndustryName)
                .Select(x => new IndustryDto
                {
                    IndustryId = x.IndustryId,
                    IndustryName = x.IndustryName
                })
                .ToList();

            return new ApiResponse<List<IndustryDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        // =====================================================
        // GET COUNTRIES
        // =====================================================

        public async Task<ApiResponse<List<CountryDto>>> GetCountries()
        {
            var data =
                await _unitOfWork.Repository<Country>()
                    .GetAllAsync();

            var result = data
                .Where(x =>
                    x.IsActive &&
                    !x.IsDeleted)
                .OrderBy(x => x.CountryName)
                .Select(x => new CountryDto
                {
                    CountryId = x.CountryId,

                    CountryName =
                        x.CountryName,

                    CountryCode =
                        x.CountryCode
                })
                .ToList();

            return new ApiResponse<List<CountryDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }
        public async Task<ApiResponse<List<CompanyTypeDto>>>
          GetCompanyTypes()
        {
            var data =
                await _unitOfWork
                    .Repository<CompanyType>()
                    .GetAllAsync();

            var result = data
                .Where(x =>
                    x.IsActive &&
                    !x.IsDeleted)
                .OrderBy(x =>
                    x.CompanyTypeName)
                .Select(x => new CompanyTypeDto
                {
                    CompanyTypeId =
                        x.CompanyTypeId,

                    CompanyId =
                        x.CompanyId,

                    RegionId =
                        x.RegionId,

                    CompanyTypeName =
                        x.CompanyTypeName,

                    CompanyTypeCode =
                        x.CompanyTypeCode,

                    Description =
                        x.Description,

                    IsActive =
                        x.IsActive
                })
                .ToList();


            return new ApiResponse<List<CompanyTypeDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }



        // =====================================================
        // GET STATES
        // =====================================================

        public async Task<ApiResponse<List<StateDto>>> GetStates(
            int? countryId)
        {
            var data =
                await _unitOfWork.Repository<StateMaster>()
                    .GetAllAsync();

            var query = data
                .Where(x =>
                    x.IsActive &&
                    !x.IsDeleted);


            // Country dependent dropdown
            if (countryId.HasValue && countryId.Value > 0)
            {
                query = query.Where(x =>
                    x.CountryId == countryId.Value);
            }


            var result = query
                .OrderBy(x => x.StateName)
                .Select(x => new StateDto
                {
                    StateId = x.StateId,

                    CountryId =
                        x.CountryId,

                    StateName =
                        x.StateName,

                    StateCode =
                        x.StateCode
                })
                .ToList();


            return new ApiResponse<List<StateDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }
    }
}
