using Business_Layer.DTOs.MasterDTO_s;
using Business_Layer.DTOs.SuperAdmin;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.EmailService;
using Microsoft.Extensions.Configuration;
using Business_Layer.Interfaces.MasterIInterface;
using Business_Layer.Models;
using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Newtonsoft.Json;
using Serilog;
using Shared.CommonModels;
using Shared.Exceptions;
using Shared.Helpers;
using System.Security.Cryptography;

namespace Business_Layer.Services.MasterServices
{
    public class CompanyAndRegionService : ICompanyAndRegionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _templateService;
        private readonly IConfiguration _config;

        public CompanyAndRegionService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService,
            IEmailService emailService,
            IEmailTemplateService templateService,
            IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
            _emailService = emailService;
            _templateService = templateService;
            _config = config;
        }
        #region Company CRUD Operations
        #region CREATE

        public async Task<ApiResponse<string>> CreateCompany(CompanyDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CompanyName))
                    throw new CustomException("Company Name is required.");

                var existing = await _unitOfWork.Repository<Company>()
 .FindAsync(x =>

     x.CompanyName.ToLower() ==
     dto.CompanyName.ToLower()

     ||

     (!string.IsNullOrWhiteSpace(dto.CompanyCode) &&
      x.CompanyCode == dto.CompanyCode)

 );

                if (existing.Any())
                    throw new CustomException("Company already exists.");

                Company company = new Company
                {
                    CompanyName = dto.CompanyName,
                    CompanyCode = dto.CompanyCode,
                    IndustryType = dto.IndustryType,
                    Headquarters = dto.Headquarters,
                    IsActive = dto.IsActive,
                    IsDefault = dto.IsDefault,
                    PlanId = dto.PlanId,
                    CompanyEmail = dto.CompanyEmail,
                    CompanyContact = dto.CompanyContact,
                    CompanyAddress = dto.CompanyAddress,
                    CompanyLogo = dto.CompanyLogo,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,
                    UserId = _currentUserService.UserId
                };

                await _unitOfWork.Repository<Company>().AddAsync(company);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Company",
                    "INSERT",
                    company.CompanyId,
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
                Log.Error(ex, "Error while creating company");
                throw;
            }
        }

        #endregion

        #region UPDATE

        public async Task<ApiResponse<string>> UpdateCompany(CompanyDto dto)
        {
            try
            {
                var company = (await _unitOfWork.Repository<Company>()
                    .FindAsync(x => x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();
                var duplicate = await _unitOfWork.Repository<Company>()
.FindAsync(x =>
    x.CompanyId != dto.CompanyId &&
    x.CompanyName.ToLower() ==
    dto.CompanyName.ToLower());

                if (duplicate.Any())
                {
                    throw new CustomException("Company Name already exists.");
                }

                if (company == null)
                    throw new CustomException("Company not found.");

                string oldValues = JsonConvert.SerializeObject(company);

                company.CompanyName = dto.CompanyName;
                company.CompanyCode = dto.CompanyCode;
                company.IndustryType = dto.IndustryType;
                company.Headquarters = dto.Headquarters;
                company.IsActive = dto.IsActive;
                company.IsDefault = dto.IsDefault;
                company.PlanId = dto.PlanId;
                company.CompanyEmail = dto.CompanyEmail;
                company.CompanyContact = dto.CompanyContact;
                company.CompanyAddress = dto.CompanyAddress;
                company.CompanyLogo = dto.CompanyLogo;
                company.ModifiedBy = _currentUserService.UserId;
                company.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<Company>().Update(company);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Company",
                    "UPDATE",
                    company.CompanyId,
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
                Log.Error(ex, "Error while updating company");
                throw;
            }
        }

        #endregion

        #region DELETE

        public async Task<ApiResponse<string>> DeleteCompany(int id)
        {
            try
            {
                var company = (await _unitOfWork.Repository<Company>()
                    .FindAsync(x => x.CompanyId == id))
                    .FirstOrDefault();

                if (company == null)
                    throw new CustomException("Company not found.");
                var regionExists = (await _unitOfWork.Repository<Region>()
           .FindAsync(x => x.CompanyId == id))
           .Any();

                if (regionExists)
                    throw new CustomException("This company is already assigned to a region and cannot be deleted.");

                string oldValues = JsonConvert.SerializeObject(company);

                _unitOfWork.Repository<Company>().Remove(company);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Company",
                    "DELETE",
                    company.CompanyId,
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
                Log.Error(ex, "Error while deleting company");
                throw;
            }
        }

        #endregion

        #region GET ALL

        public async Task<ApiResponse<List<CompanyDto>>> GetCompanies()
        {
            var companies = (await _unitOfWork.Repository<Company>()
        .GetAllAsync())
        .Where(x => x.CreatedBy == _currentUserService.UserId)
        .OrderByDescending(x => x.CompanyId)
        .ToList();

            var result = companies.Select(x => new CompanyDto
            {
                CompanyId = x.CompanyId,
                CompanyName = x.CompanyName,
                CompanyCode = x.CompanyCode,
                IndustryType = x.IndustryType,
                Headquarters = x.Headquarters,
                IsActive = x.IsActive,
                IsDefault = x.IsDefault,
                PlanId = x.PlanId,
                CompanyEmail = x.CompanyEmail,
                CompanyContact = x.CompanyContact,
                CompanyAddress = x.CompanyAddress,
                CompanyLogo = x.CompanyLogo
            }).ToList();

            return new ApiResponse<List<CompanyDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        #endregion

        #region GET BY ID

        public async Task<ApiResponse<CompanyDto>> GetCompanyById(int id)
        {
            var company = (await _unitOfWork.Repository<Company>()
                .FindAsync(x => x.CompanyId == id))
                .FirstOrDefault();

            if (company == null)
                throw new CustomException("Company not found.");

            return new ApiResponse<CompanyDto>
            {
                Success = true,
                Message = "Success",
                Data = new CompanyDto
                {
                    CompanyId = company.CompanyId,
                    CompanyName = company.CompanyName,
                    CompanyCode = company.CompanyCode,
                    IndustryType = company.IndustryType,
                    Headquarters = company.Headquarters,
                    IsActive = company.IsActive,
                    IsDefault = company.IsDefault,
                    PlanId = company.PlanId,
                    CompanyEmail = company.CompanyEmail,
                    CompanyContact = company.CompanyContact,
                    CompanyAddress = company.CompanyAddress,
                    CompanyLogo = company.CompanyLogo
                }
            };
        }

        #endregion
        #endregion
        #region Region CRUD Operations
        public async Task<ApiResponse<string>> CreateRegion(RegionDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.RegionName))
                    throw new CustomException("Region Name is required.");

                var duplicate = await _unitOfWork.Repository<Region>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionName.ToLower() == dto.RegionName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Region already exists for this company.");

                Region region = new Region
                {
                    CompanyId = dto.CompanyId,
                    RegionName = dto.RegionName,
                    Country = dto.Country,
                    RegionCode = dto.RegionCode,
                    ContactPerson = dto.ContactPerson,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Address = dto.Address,
                    IsActive = dto.IsActive,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,
                    UserId = _currentUserService.UserId
                };

                await _unitOfWork.Repository<Region>().AddAsync(region);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Region",
                    "INSERT",
                    region.RegionId,
                    "",
                    JsonConvert.SerializeObject(region),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Region Created Successfully",
                    Data = region.RegionName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating region");
                throw;
            }
        }
        public async Task<ApiResponse<string>> UpdateRegion(RegionDto dto)
        {
            try
            {
                var region = (await _unitOfWork.Repository<Region>()
                    .FindAsync(x => x.RegionId == dto.RegionId))
                    .FirstOrDefault();

                if (region == null)
                    throw new CustomException("Region not found.");

                var duplicate = await _unitOfWork.Repository<Region>()
                    .FindAsync(x =>
                        x.RegionId != dto.RegionId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionName.ToLower() == dto.RegionName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Region already exists.");

                string oldValues = JsonConvert.SerializeObject(region);

                region.CompanyId = dto.CompanyId;
                region.RegionName = dto.RegionName;
                region.Country = dto.Country;
                region.RegionCode = dto.RegionCode;
                region.ContactPerson = dto.ContactPerson;
                region.Email = dto.Email;
                region.PhoneNumber = dto.PhoneNumber;
                region.Address = dto.Address;
                region.IsActive = dto.IsActive;
                region.ModifiedBy = _currentUserService.UserId;
                region.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<Region>().Update(region);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Region",
                    "UPDATE",
                    region.RegionId,
                    oldValues,
                    JsonConvert.SerializeObject(region),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Region Updated Successfully",
                    Data = region.RegionName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating region");
                throw;
            }
        }
        public async Task<ApiResponse<string>> DeleteRegion(int id)
        {
            try
            {
                var region = (await _unitOfWork.Repository<Region>()
                    .FindAsync(x => x.RegionId == id))
                    .FirstOrDefault();

                if (region == null)
                    throw new CustomException("Region not found.");

                string oldValues = JsonConvert.SerializeObject(region);

                _unitOfWork.Repository<Region>().Remove(region);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Region",
                    "DELETE",
                    region.RegionId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Region Deleted Successfully",
                    Data = region.RegionName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting region");
                throw;
            }
        }
        public async Task<ApiResponse<List<RegionDto>>> GetRegions()
        {
            var companies = await _unitOfWork.Repository<Company>().GetAllAsync();
            var regions = await _unitOfWork.Repository<Region>().GetAllAsync();

            var result = (from r in regions
                          join c in companies
                          on r.CompanyId equals c.CompanyId
                          where r.CreatedBy == _currentUserService.UserId
                          select new RegionDto
                          {
                              RegionId = r.RegionId,
                              CompanyId = r.CompanyId,
                              CompanyName = c.CompanyName,
                              RegionName = r.RegionName,
                              Country = r.Country,
                              RegionCode = r.RegionCode,
                              ContactPerson = r.ContactPerson,
                              Email = r.Email,
                              PhoneNumber = r.PhoneNumber,
                              Address = r.Address,
                              IsActive = r.IsActive
                          }).OrderByDescending(x => x.RegionId).ToList();

            return new ApiResponse<List<RegionDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }
        public async Task<ApiResponse<RegionDto>> GetRegionById(int id)
        {
            var region = (await _unitOfWork.Repository<Region>()
                .FindAsync(x => x.RegionId == id))
                .FirstOrDefault();

            if (region == null)
                throw new CustomException("Region not found.");

            return new ApiResponse<RegionDto>
            {
                Success = true,
                Message = "Success",
                Data = new RegionDto
                {
                    RegionId = region.RegionId,
                    CompanyId = region.CompanyId,
                    RegionName = region.RegionName,
                    Country = region.Country,
                    RegionCode = region.RegionCode,
                    ContactPerson = region.ContactPerson,
                    Email = region.Email,
                    PhoneNumber = region.PhoneNumber,
                    Address = region.Address,
                    IsActive = region.IsActive
                }
            };
        }
        #endregion

        #region Branch

        #region CREATE

        public async Task<ApiResponse<string>> CreateBranch(BranchDto dto)
        {
            try
            {
                // Validate Branch Name
                if (string.IsNullOrWhiteSpace(dto.BranchName))
                    throw new CustomException("Branch Name is required.");

                // Validate Branch Code
                if (string.IsNullOrWhiteSpace(dto.BranchCode))
                    throw new CustomException("Branch Code is required.");

                // Validate Organization
                var organization =
                    (await _unitOfWork.Repository<OrganizationDatum>()
                        .FindAsync(x =>
                            x.OrganizationId == dto.OrganizationId))
                    .FirstOrDefault();

                if (organization == null)
                    throw new CustomException("Organization not found.");

                // Validate Company
                var company =
                    (await _unitOfWork.Repository<Company>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (company == null)
                    throw new CustomException(
                        "Company not found for the selected organization.");

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

                // Validate Opening and Closing Time
                if (dto.OpeningTime.HasValue &&
                    dto.ClosingTime.HasValue &&
                    dto.OpeningTime >= dto.ClosingTime)
                {
                    throw new CustomException(
                        "Opening Time must be earlier than Closing Time.");
                }

                // Duplicate Branch Name
                var duplicateName =
                    await _unitOfWork.Repository<BranchDatum>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId &&
                            x.BranchName.ToLower() ==
                            dto.BranchName.Trim().ToLower());

                if (duplicateName.Any())
                    throw new CustomException(
                        "Branch Name already exists.");

                // Duplicate Branch Code
                var duplicateCode =
                    await _unitOfWork.Repository<BranchDatum>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId &&
                            x.BranchCode.ToLower() ==
                            dto.BranchCode.Trim().ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Branch Code already exists.");

                // If Head Office is selected,
                // check whether another Head Office already exists
                if (dto.HeadOffice)
                {
                    var existingHeadOffice =
                        await _unitOfWork.Repository<BranchDatum>()
                            .FindAsync(x =>
                                x.CompanyId == dto.CompanyId &&
                                x.HeadOffice);

                    if (existingHeadOffice.Any())
                        throw new CustomException(
                            "Head Office already exists for this company.");
                }

                // Create Entity
                BranchDatum branch = new BranchDatum
                {
                    OrganizationId = dto.OrganizationId,
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,

                    BranchName = dto.BranchName.Trim(),
                    BranchCode = dto.BranchCode.Trim(),

                    BranchManager = dto.BranchManager?.Trim(),
                    Email = dto.Email?.Trim(),
                    PhoneNumber = dto.PhoneNumber?.Trim(),

                    Address = dto.Address?.Trim(),
                    City = dto.City?.Trim(),
                    State = dto.State?.Trim(),
                    Country = dto.Country?.Trim(),
                    ZipCode = dto.ZipCode?.Trim(),

                    OpeningTime = dto.OpeningTime,
                    ClosingTime = dto.ClosingTime,

                    Status = dto.Status,
                    HeadOffice = dto.HeadOffice,

                    Remarks = dto.Remarks?.Trim(),

                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now
                };

                await _unitOfWork.Repository<BranchDatum>()
                    .AddAsync(branch);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "BranchDatum",
                    "INSERT",
                    branch.BranchId,
                    "",
                    JsonConvert.SerializeObject(branch,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Branch Created Successfully",
                    Data = branch.BranchName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating branch");
                throw;
            }
        }

        #endregion


        #region UPDATE

        public async Task<ApiResponse<string>> UpdateBranch(BranchDto dto)
        {
            try
            {
                // Validate Branch Name
                if (string.IsNullOrWhiteSpace(dto.BranchName))
                    throw new CustomException("Branch Name is required.");

                // Validate Branch Code
                if (string.IsNullOrWhiteSpace(dto.BranchCode))
                    throw new CustomException("Branch Code is required.");

                // Get Existing Branch
                var branch =
                    (await _unitOfWork.Repository<BranchDatum>()
                        .FindAsync(x =>
                            x.BranchId == dto.BranchId))
                    .FirstOrDefault();

                if (branch == null)
                    throw new CustomException("Branch not found.");

                // Validate Organization
                var organization =
                    (await _unitOfWork.Repository<OrganizationDatum>()
                        .FindAsync(x =>
                            x.OrganizationId == dto.OrganizationId))
                    .FirstOrDefault();

                if (organization == null)
                    throw new CustomException("Organization not found.");

                // Validate Company
                var company =
                    (await _unitOfWork.Repository<Company>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (company == null)
                    throw new CustomException(
                        "Company not found for the selected organization.");

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

                // Validate Time
                if (dto.OpeningTime.HasValue &&
                    dto.ClosingTime.HasValue &&
                    dto.OpeningTime >= dto.ClosingTime)
                {
                    throw new CustomException(
                        "Opening Time must be earlier than Closing Time.");
                }

                // Duplicate Branch Name
                var duplicateName =
                    await _unitOfWork.Repository<BranchDatum>()
                        .FindAsync(x =>
                            x.BranchId != dto.BranchId &&
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId &&
                            x.BranchName.ToLower() ==
                            dto.BranchName.Trim().ToLower());

                if (duplicateName.Any())
                    throw new CustomException(
                        "Branch Name already exists.");

                // Duplicate Branch Code
                var duplicateCode =
                    await _unitOfWork.Repository<BranchDatum>()
                        .FindAsync(x =>
                            x.BranchId != dto.BranchId &&
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId &&
                            x.BranchCode.ToLower() ==
                            dto.BranchCode.Trim().ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Branch Code already exists.");

                // Head Office Validation
                if (dto.HeadOffice)
                {
                    var existingHeadOffice =
                        await _unitOfWork.Repository<BranchDatum>()
                            .FindAsync(x =>
                                x.BranchId != dto.BranchId &&
                                x.CompanyId == dto.CompanyId &&
                                x.HeadOffice);

                    if (existingHeadOffice.Any())
                        throw new CustomException(
                            "Head Office already exists for this company.");
                }

                // Old Values For Audit
                string oldValues =
                    JsonConvert.SerializeObject(branch,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                // Update Entity
                branch.OrganizationId = dto.OrganizationId;
                branch.CompanyId = dto.CompanyId;
                branch.RegionId = dto.RegionId;

                branch.BranchName = dto.BranchName.Trim();
                branch.BranchCode = dto.BranchCode.Trim();

                branch.BranchManager = dto.BranchManager?.Trim();
                branch.Email = dto.Email?.Trim();
                branch.PhoneNumber = dto.PhoneNumber?.Trim();

                branch.Address = dto.Address?.Trim();
                branch.City = dto.City?.Trim();
                branch.State = dto.State?.Trim();
                branch.Country = dto.Country?.Trim();
                branch.ZipCode = dto.ZipCode?.Trim();

                branch.OpeningTime = dto.OpeningTime;
                branch.ClosingTime = dto.ClosingTime;

                branch.Status = dto.Status;
                branch.HeadOffice = dto.HeadOffice;

                branch.Remarks = dto.Remarks?.Trim();

                branch.ModifiedBy =
                    _currentUserService.UserId;

                branch.ModifiedAt =
                    DateTime.Now;

                _unitOfWork.Repository<BranchDatum>()
                    .Update(branch);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "BranchDatum",
                    "UPDATE",
                    branch.BranchId,
                    oldValues,
                    JsonConvert.SerializeObject(branch,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Branch Updated Successfully",
                    Data = branch.BranchName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating branch");
                throw;
            }
        }

        #endregion


        #region DELETE

        public async Task<ApiResponse<string>> DeleteBranch(int id)
        {
            try
            {
                // Get Existing Branch
                var branch =
                    (await _unitOfWork.Repository<BranchDatum>()
                        .FindAsync(x =>
                            x.BranchId == id))
                    .FirstOrDefault();

                if (branch == null)
                    throw new CustomException("Branch not found.");

                // Old Values For Audit
                string oldValues =
                    JsonConvert.SerializeObject(branch,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                // Delete
                _unitOfWork.Repository<BranchDatum>()
                    .Remove(branch);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "BranchDatum",
                    "DELETE",
                    branch.BranchId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Branch Deleted Successfully",
                    Data = branch.BranchName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting branch");
                throw;
            }
        }

        #endregion


        #region GET ALL

        public async Task<ApiResponse<List<BranchDto>>> GetBranches()
        {
            try
            {
                var branches =
                    (await _unitOfWork.Repository<BranchDatum>()
                        .FindAsync(x =>
                            x.CreatedBy == _currentUserService.UserId))
                    .OrderByDescending(x => x.BranchId)
                    .ToList();

                var result = branches.Select(x =>
                    new BranchDto
                    {
                        BranchId = x.BranchId,

                        OrganizationId = x.OrganizationId,
                        CompanyId = x.CompanyId,
                        RegionId = x.RegionId,

                        BranchName = x.BranchName,
                        BranchCode = x.BranchCode,

                        BranchManager = x.BranchManager,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,

                        Address = x.Address,
                        City = x.City,
                        State = x.State,
                        Country = x.Country,
                        ZipCode = x.ZipCode,

                        OpeningTime = x.OpeningTime,
                        ClosingTime = x.ClosingTime,

                        Status = x.Status,
                        HeadOffice = x.HeadOffice,

                        Remarks = x.Remarks
                    })
                    .ToList();

                return new ApiResponse<List<BranchDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while getting branches");

                throw;
            }
        }

        #endregion


        #region GET BY ID

        public async Task<ApiResponse<BranchDto>> GetBranchById(int id)
        {
            try
            {
                var branch =
                    (await _unitOfWork.Repository<BranchDatum>()
                        .FindAsync(x =>
                            x.BranchId == id))
                    .FirstOrDefault();

                if (branch == null)
                    throw new CustomException(
                        "Branch not found.");

                var result = new BranchDto
                {
                    BranchId = branch.BranchId,

                    OrganizationId = branch.OrganizationId,
                    CompanyId = branch.CompanyId,
                    RegionId = branch.RegionId,

                    BranchName = branch.BranchName,
                    BranchCode = branch.BranchCode,

                    BranchManager = branch.BranchManager,
                    Email = branch.Email,
                    PhoneNumber = branch.PhoneNumber,

                    Address = branch.Address,
                    City = branch.City,
                    State = branch.State,
                    Country = branch.Country,
                    ZipCode = branch.ZipCode,

                    OpeningTime = branch.OpeningTime,
                    ClosingTime = branch.ClosingTime,

                    Status = branch.Status,
                    HeadOffice = branch.HeadOffice,

                    Remarks = branch.Remarks
                };

                return new ApiResponse<BranchDto>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while getting branch by id");

                throw;
            }
        }

        #endregion

        #endregion

        #region Company Administrator

        #region CREATE

        public async Task<ApiResponse<string>> CreateCompanyAdministrator(
            CompanyAdministratorDto dto)
        {
            try
            {
                // Validate Company
                var company =
                    (await _unitOfWork.Repository<Company>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (company == null)
                    throw new CustomException("Company not found.");

                // Validate Employee Code
                if (string.IsNullOrWhiteSpace(dto.EmployeeCode))
                    throw new CustomException(
                        "Employee Code is required.");

                // Username is generated automatically (see below), so it
                // is not taken from the request.

                // Validate First Name
                if (string.IsNullOrWhiteSpace(dto.FirstName))
                    throw new CustomException(
                        "First Name is required.");

                // Validate Email
                if (string.IsNullOrWhiteSpace(dto.Email))
                    throw new CustomException(
                        "Email is required.");

                // Validate Mobile Number
                if (string.IsNullOrWhiteSpace(dto.MobileNumber))
                    throw new CustomException(
                        "Mobile Number is required.");

                // Employee Code Duplicate
                var duplicateEmployeeCode =
                    await _unitOfWork.Repository<CompanyAdministrator>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId &&
                            x.EmployeeCode.ToLower() ==
                            dto.EmployeeCode.Trim().ToLower());

                if (duplicateEmployeeCode.Any())
                    throw new CustomException(
                        "Employee Code already exists.");

                // Email Duplicate
                var duplicateEmail =
                    await _unitOfWork.Repository<CompanyAdministrator>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId &&
                            x.Email.ToLower() ==
                            dto.Email.Trim().ToLower());

                if (duplicateEmail.Any())
                    throw new CustomException(
                        "Email already exists.");

                // Validate Department
                // Departments/Designations are created without a Company
                // (CompanyId = 0), so accept those as well as ones that
                // belong to the selected company.
                if (dto.DepartmentId.HasValue)
                {
                    var department =
                        (await _unitOfWork.Repository<Department>()
                            .FindAsync(x =>
                                x.DepartmentId == dto.DepartmentId.Value &&
                                (x.CompanyId == dto.CompanyId ||
                                 x.CompanyId == 0)))
                        .FirstOrDefault();

                    if (department == null)
                        throw new CustomException(
                            "Department not found for the selected company.");
                }

                // Validate Designation
                if (dto.DesignationId.HasValue)
                {
                    var designation =
                        (await _unitOfWork.Repository<Designation>()
                            .FindAsync(x =>
                                x.DesignationId == dto.DesignationId.Value &&
                                (x.CompanyId == dto.CompanyId ||
                                 x.CompanyId == 0)))
                        .FirstOrDefault();

                    if (designation == null)
                        throw new CustomException(
                            "Designation not found for the selected company.");
                }

                // Validate Region
                if (dto.RegionId.HasValue)
                {
                    var region =
                        (await _unitOfWork.Repository<Region>()
                            .FindAsync(x =>
                                x.RegionId == dto.RegionId.Value &&
                                x.CompanyId == dto.CompanyId))
                        .FirstOrDefault();

                    if (region == null)
                        throw new CustomException(
                            "Region not found for the selected company.");
                }

                // Validate Branch
                if (dto.BranchId.HasValue)
                {
                    var branch =
                        (await _unitOfWork.Repository<Branch1>()
                            .FindAsync(x =>
                                x.BranchId == dto.BranchId.Value &&
                                x.CompanyId == dto.CompanyId))
                        .FirstOrDefault();

                    if (branch == null)
                        throw new CustomException(
                            "Branch not found for the selected company.");

                    // If Region is selected, branch must belong to that Region
                    if (dto.RegionId.HasValue &&
                        branch.RegionId != dto.RegionId.Value)
                    {
                        throw new CustomException(
                            "Branch does not belong to the selected region.");
                    }
                }

                // Login account (dbo.UserLogin) - UserName and Email are
                // unique across all companies in that table.
                string loginEmail = dto.Email.Trim();
                string loginMobile = dto.MobileNumber.Trim();
                string loginFullName =
                    $"{dto.FirstName.Trim()} {dto.LastName?.Trim()}".Trim();

                if (loginEmail.Length > 150)
                    throw new CustomException(
                        "Email cannot exceed 150 characters.");

                if (loginMobile.Length > 15)
                    throw new CustomException(
                        "Mobile Number cannot exceed 15 characters.");

                if (loginFullName.Length > 150)
                    throw new CustomException(
                        "Full Name cannot exceed 150 characters.");

                var existingLogin =
                    await _unitOfWork.Repository<UserLogin>()
                        .FindAsync(x =>
                            x.Email.ToLower() ==
                            loginEmail.ToLower());

                if (existingLogin.Any())
                    throw new CustomException(
                        "A login with this Email already exists.");

                // Username and password are generated, never typed or
                // hard-coded. Only the BCrypt hash is stored; the plain
                // password is used once, in the welcome email.
                string loginUserName =
                    await GenerateUniqueUserName(dto.FirstName);

                string plainPassword = GeneratePassword();

                UserLogin userLogin = new UserLogin
                {
                    FullName = loginFullName,
                    UserName = loginUserName,
                    Email = loginEmail,
                    MobileNumber = loginMobile,
                    PasswordHash =
                        PasswordHelper.HashPassword(plainPassword),
                    Role =
                        string.IsNullOrWhiteSpace(dto.RoleName)
                            ? "User"
                            : dto.RoleName.Trim(),
                    IsActive = dto.Status,
                    IsLocked = false,
                    FailedLoginAttempts = 0,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now
                };

                // Create Entity
                CompanyAdministrator administrator =
                    new CompanyAdministrator
                    {
                        CompanyId = dto.CompanyId,

                        DepartmentId = dto.DepartmentId,
                        DesignationId = dto.DesignationId,
                        RegionId = dto.RegionId,
                        BranchId = dto.BranchId,

                        EmployeeCode = dto.EmployeeCode.Trim(),
                        Username = loginUserName,

                        FirstName = dto.FirstName.Trim(),
                        LastName = dto.LastName?.Trim(),

                        Email = dto.Email.Trim(),
                        MobileNumber = dto.MobileNumber.Trim(),

                        RoleName = dto.RoleName?.Trim(),
                        ReportingManager = dto.ReportingManager?.Trim(),

                        ProfileImagePath =
                            dto.ProfileImagePath?.Trim(),

                        Status = dto.Status,

                        EmailVerified = dto.EmailVerified,
                        MobileVerified = dto.MobileVerified,

                        TwoFactorAuthentication =
                            dto.TwoFactorAuthentication,

                        Remarks = dto.Remarks?.Trim(),

                        // Encrypted (not hashed) so the Edit form can show
                        // it. Login uses UserLogin.PasswordHash instead.
                        PasswordHash =
                            PasswordProtector.Protect(
                                plainPassword,
                                _config["PasswordEncryption:Key"]!),

                        CreatedBy = _currentUserService.UserId,
                        CreatedDate = DateTime.Now
                    };

                await _unitOfWork.Repository<UserLogin>()
                    .AddAsync(userLogin);

                await _unitOfWork.Repository<CompanyAdministrator>()
                    .AddAsync(administrator);

                // Single save: the administrator and its login row are
                // committed together or not at all.
                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "CompanyAdministrator",
                    "INSERT",
                    administrator.AdministratorId,
                    "",
                    JsonConvert.SerializeObject(
                        administrator,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling =
                                ReferenceLoopHandling.Ignore
                        }),
                    _currentUserService.UserId);

                // JWT for the new user, built with the existing JwtHelper
                // and the JwtSettings configuration. Best-effort - the
                // user is already created.
                try
                {
                    string token =
                        JwtHelper.GenerateToken(
                            userLogin.UserId,
                            userLogin.UserName,
                            userLogin.Role,
                            _config["JwtSettings:SecretKey"]!,
                            _config["JwtSettings:Issuer"]!,
                            _config["JwtSettings:Audience"]!,
                            int.Parse(
                                _config["JwtSettings:ExpiryMinutes"]!));

                    // The token is intentionally neither returned nor
                    // logged: it would let the caller act as the new user.
                    Log.Information(
                        "JWT generated for new user {UserId} ({UserName}), {Length} chars",
                        userLogin.UserId,
                        userLogin.UserName,
                        token.Length);
                }
                catch (Exception jwtEx)
                {
                    Log.Error(
                        jwtEx,
                        "User created but JWT generation failed for {UserName}",
                        userLogin.UserName);
                }

                // Welcome email with the login details (best-effort - user
                // is already created even if the email fails to send).
                try
                {
                    string? loginUrl = _config["loginURL:loginURL"];

                    if (string.IsNullOrWhiteSpace(loginUrl))
                        throw new InvalidOperationException(
                            "loginURL:loginURL is not configured in appsettings.");

                    await _emailService.SendEmailAsync(
                        new EmailRequest
                        {
                            To = administrator.Email,

                            Subject = "Welcome to CRM - Your Login Details",

                            HtmlBody =
                                _templateService.WelcomeUserTemplate(
                                    loginFullName,
                                    loginUrl,
                                    userLogin.UserName,
                                    plainPassword)
                        });
                }
                catch (Exception emailEx)
                {
                    Log.Error(
                        emailEx,
                        "Company Administrator created but welcome email failed to send to {Email}",
                        administrator.Email);
                }

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Company Administrator Created Successfully",
                    Data = administrator.EmployeeCode
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating company administrator");

                throw;
            }
        }

        // Username format: <first name>@<3 digits>, e.g. karishma@123.
        // Re-rolls the digits until the name is unused in UserLogin.
        private async Task<string> GenerateUniqueUserName(string firstName)
        {
            string baseName =
                new string(firstName
                    .Where(char.IsLetterOrDigit)
                    .ToArray())
                .ToLowerInvariant();

            if (baseName.Length == 0)
                baseName = "user";

            if (baseName.Length > 50)
                baseName = baseName.Substring(0, 50);

            string prefix = baseName + "@";

            var taken =
                (await _unitOfWork.Repository<UserLogin>()
                    .FindAsync(x => x.UserName.StartsWith(prefix)))
                .Select(x => x.UserName.ToLower())
                .ToHashSet();

            // 900 three-digit values; widen to four digits if exhausted.
            for (int attempt = 0; attempt < 50; attempt++)
            {
                string candidate =
                    prefix + RandomNumberGenerator.GetInt32(100, 1000);

                if (!taken.Contains(candidate))
                    return candidate;
            }

            for (int attempt = 0; attempt < 100; attempt++)
            {
                string candidate =
                    prefix + RandomNumberGenerator.GetInt32(1000, 10000);

                if (!taken.Contains(candidate))
                    return candidate;
            }

            throw new CustomException(
                "Unable to generate a unique username. Please try again.");
        }

        // 12 characters with at least one upper, lower, digit and symbol.
        private static string GeneratePassword()
        {
            const string upper = "ABCDEFGHJKLMNPQRSTUVWXYZ";
            const string lower = "abcdefghijkmnopqrstuvwxyz";
            const string digits = "23456789";
            const string symbols = "@#$%&*!?";
            const string all = upper + lower + digits + symbols;

            var chars = new List<char>
            {
                upper[RandomNumberGenerator.GetInt32(upper.Length)],
                lower[RandomNumberGenerator.GetInt32(lower.Length)],
                digits[RandomNumberGenerator.GetInt32(digits.Length)],
                symbols[RandomNumberGenerator.GetInt32(symbols.Length)]
            };

            while (chars.Count < 12)
                chars.Add(all[RandomNumberGenerator.GetInt32(all.Length)]);

            // Shuffle so the required characters are not always first.
            for (int i = chars.Count - 1; i > 0; i--)
            {
                int j = RandomNumberGenerator.GetInt32(i + 1);
                (chars[i], chars[j]) = (chars[j], chars[i]);
            }

            return new string(chars.ToArray());
        }

        #endregion


        #region UPDATE

        public async Task<ApiResponse<string>> UpdateCompanyAdministrator(
            CompanyAdministratorDto dto)
        {
            try
            {
                // Validate Company
                var company =
                    (await _unitOfWork.Repository<Company>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId))
                    .FirstOrDefault();

                if (company == null)
                    throw new CustomException("Company not found.");

                // Get Existing Administrator
                var administrator =
                    (await _unitOfWork.Repository<CompanyAdministrator>()
                        .FindAsync(x =>
                            x.AdministratorId ==
                            dto.AdministratorId))
                    .FirstOrDefault();

                if (administrator == null)
                    throw new CustomException(
                        "Company Administrator not found.");

                // Required fields
                if (string.IsNullOrWhiteSpace(dto.EmployeeCode))
                    throw new CustomException(
                        "Employee Code is required.");

                if (string.IsNullOrWhiteSpace(dto.Username))
                    throw new CustomException(
                        "Username is required.");

                if (string.IsNullOrWhiteSpace(dto.FirstName))
                    throw new CustomException(
                        "First Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Email))
                    throw new CustomException(
                        "Email is required.");

                if (string.IsNullOrWhiteSpace(dto.MobileNumber))
                    throw new CustomException(
                        "Mobile Number is required.");

                // Duplicate Employee Code
                var duplicateEmployeeCode =
                    await _unitOfWork.Repository<CompanyAdministrator>()
                        .FindAsync(x =>
                            x.AdministratorId != dto.AdministratorId &&
                            x.CompanyId == dto.CompanyId &&
                            x.EmployeeCode.ToLower() ==
                            dto.EmployeeCode.Trim().ToLower());

                if (duplicateEmployeeCode.Any())
                    throw new CustomException(
                        "Employee Code already exists.");

                // Duplicate Username
                var duplicateUsername =
                    await _unitOfWork.Repository<CompanyAdministrator>()
                        .FindAsync(x =>
                            x.AdministratorId != dto.AdministratorId &&
                            x.CompanyId == dto.CompanyId &&
                            x.Username.ToLower() ==
                            dto.Username.Trim().ToLower());

                if (duplicateUsername.Any())
                    throw new CustomException(
                        "Username already exists.");

                // Duplicate Email
                var duplicateEmail =
                    await _unitOfWork.Repository<CompanyAdministrator>()
                        .FindAsync(x =>
                            x.AdministratorId != dto.AdministratorId &&
                            x.CompanyId == dto.CompanyId &&
                            x.Email.ToLower() ==
                            dto.Email.Trim().ToLower());

                if (duplicateEmail.Any())
                    throw new CustomException(
                        "Email already exists.");

                // Validate Department
                if (dto.DepartmentId.HasValue)
                {
                    var department =
                        (await _unitOfWork.Repository<Department>()
                            .FindAsync(x =>
                                x.DepartmentId ==
                                dto.DepartmentId.Value &&
                                (x.CompanyId == dto.CompanyId ||
                                 x.CompanyId == 0)))
                        .FirstOrDefault();

                    if (department == null)
                        throw new CustomException(
                            "Department not found for the selected company.");
                }

                // Validate Designation
                if (dto.DesignationId.HasValue)
                {
                    var designation =
                        (await _unitOfWork.Repository<Designation>()
                            .FindAsync(x =>
                                x.DesignationId ==
                                dto.DesignationId.Value &&
                                (x.CompanyId == dto.CompanyId ||
                                 x.CompanyId == 0)))
                        .FirstOrDefault();

                    if (designation == null)
                        throw new CustomException(
                            "Designation not found for the selected company.");
                }

                // Validate Region
                if (dto.RegionId.HasValue)
                {
                    var region =
                        (await _unitOfWork.Repository<Region>()
                            .FindAsync(x =>
                                x.RegionId ==
                                dto.RegionId.Value &&
                                x.CompanyId == dto.CompanyId))
                        .FirstOrDefault();

                    if (region == null)
                        throw new CustomException(
                            "Region not found for the selected company.");
                }

                // Validate Branch
                if (dto.BranchId.HasValue)
                {
                    var branch =
                        (await _unitOfWork.Repository<Branch1>()
                            .FindAsync(x =>
                                x.BranchId ==
                                dto.BranchId.Value &&
                                x.CompanyId == dto.CompanyId))
                        .FirstOrDefault();

                    if (branch == null)
                        throw new CustomException(
                            "Branch not found for the selected company.");

                    if (dto.RegionId.HasValue &&
                        branch.RegionId != dto.RegionId.Value)
                    {
                        throw new CustomException(
                            "Branch does not belong to the selected region.");
                    }
                }

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(administrator);

                // Update
                administrator.CompanyId = dto.CompanyId;

                administrator.DepartmentId =
                    dto.DepartmentId;

                administrator.DesignationId =
                    dto.DesignationId;

                administrator.RegionId =
                    dto.RegionId;

                administrator.BranchId =
                    dto.BranchId;

                administrator.EmployeeCode =
                    dto.EmployeeCode.Trim();

                administrator.Username =
                    dto.Username.Trim();

                administrator.FirstName =
                    dto.FirstName.Trim();

                administrator.LastName =
                    dto.LastName?.Trim();

                administrator.Email =
                    dto.Email.Trim();

                administrator.MobileNumber =
                    dto.MobileNumber.Trim();

                administrator.RoleName =
                    dto.RoleName?.Trim();

                administrator.ReportingManager =
                    dto.ReportingManager?.Trim();

                administrator.ProfileImagePath =
                    dto.ProfileImagePath?.Trim();

                administrator.Status =
                    dto.Status;

                administrator.EmailVerified =
                    dto.EmailVerified;

                administrator.MobileVerified =
                    dto.MobileVerified;

                administrator.TwoFactorAuthentication =
                    dto.TwoFactorAuthentication;

                administrator.Remarks =
                    dto.Remarks?.Trim();

                administrator.UpdatedBy =
                    _currentUserService.UserId;

                administrator.UpdatedDate =
                    DateTime.Now;

                _unitOfWork.Repository<CompanyAdministrator>()
                    .Update(administrator);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "CompanyAdministrator",
                    "UPDATE",
                    administrator.AdministratorId,
                    oldValues,
                    JsonConvert.SerializeObject(administrator),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Company Administrator Updated Successfully",

                    Data = administrator.EmployeeCode
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating company administrator");

                throw;
            }
        }

        #endregion


        #region DELETE

        public async Task<ApiResponse<string>> DeleteCompanyAdministrator(
            int id)
        {
            try
            {
                var administrator =
                    (await _unitOfWork.Repository<CompanyAdministrator>()
                        .FindAsync(x =>
                            x.AdministratorId == id))
                    .FirstOrDefault();

                if (administrator == null)
                    throw new CustomException(
                        "Company Administrator not found.");

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(administrator);

                // Hard Delete
                _unitOfWork.Repository<CompanyAdministrator>()
                    .Remove(administrator);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "CompanyAdministrator",
                    "DELETE",
                    administrator.AdministratorId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Company Administrator Deleted Successfully",

                    Data = administrator.EmployeeCode
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting company administrator");

                throw;
            }
        }

        #endregion


        #region GET ALL

        public async Task<ApiResponse<List<CompanyAdministratorDto>>>
            GetCompanyAdministrators()
        {
            try
            {
                var administrators =
                    (await _unitOfWork.Repository<CompanyAdministrator>()
                        .GetAllAsync())
                    .Where(x =>
                        x.CreatedBy == _currentUserService.UserId)
                    .OrderByDescending(x =>
                        x.AdministratorId)
                    .ToList();

                var departments =
                    (await _unitOfWork.Repository<Department>()
                        .GetAllAsync())
                    .ToList();

                var designations =
                    (await _unitOfWork.Repository<Designation>()
                        .GetAllAsync())
                    .ToList();

                var result =
                    administrators.Select(x =>
                        new CompanyAdministratorDto
                        {
                            AdministratorId =
                                x.AdministratorId,

                            CompanyId =
                                x.CompanyId,

                            DepartmentId =
                                x.DepartmentId,

                            DepartmentName =
                                departments.FirstOrDefault(d =>
                                    d.DepartmentId == x.DepartmentId)
                                    ?.DepartmentName,

                            DesignationId =
                                x.DesignationId,

                            DesignationName =
                                designations.FirstOrDefault(d =>
                                    d.DesignationId == x.DesignationId)
                                    ?.DesignationName,

                            RegionId =
                                x.RegionId,

                            BranchId =
                                x.BranchId,

                            EmployeeCode =
                                x.EmployeeCode,

                            Username =
                                x.Username,

                            FirstName =
                                x.FirstName,

                            LastName =
                                x.LastName,

                            Email =
                                x.Email,

                            MobileNumber =
                                x.MobileNumber,

                            RoleName =
                                x.RoleName,

                            ReportingManager =
                                x.ReportingManager,

                            ProfileImagePath =
                                x.ProfileImagePath,

                            Status =
                                x.Status,

                            EmailVerified =
                                x.EmailVerified,

                            MobileVerified =
                                x.MobileVerified,

                            TwoFactorAuthentication =
                                x.TwoFactorAuthentication,

                            Remarks =
                                x.Remarks
                        })
                    .ToList();

                return new ApiResponse<List<CompanyAdministratorDto>>
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
                    "Error while getting company administrators");

                throw;
            }
        }

        #endregion


        #region GET BY ID

        public async Task<ApiResponse<CompanyAdministratorDto>>
            GetCompanyAdministratorById(int id)
        {
            try
            {
                var administrator =
                    (await _unitOfWork.Repository<CompanyAdministrator>()
                        .FindAsync(x =>
                            x.AdministratorId == id))
                    .FirstOrDefault();

                if (administrator == null)
                    throw new CustomException(
                        "Company Administrator not found.");

                var department = administrator.DepartmentId.HasValue
                    ? (await _unitOfWork.Repository<Department>()
                        .FindAsync(d =>
                            d.DepartmentId == administrator.DepartmentId.Value))
                        .FirstOrDefault()
                    : null;

                var designation = administrator.DesignationId.HasValue
                    ? (await _unitOfWork.Repository<Designation>()
                        .FindAsync(d =>
                            d.DesignationId == administrator.DesignationId.Value))
                        .FirstOrDefault()
                    : null;

                var result =
                    new CompanyAdministratorDto
                    {
                        AdministratorId =
                            administrator.AdministratorId,

                        CompanyId =
                            administrator.CompanyId,

                        DepartmentId =
                            administrator.DepartmentId,

                        DepartmentName =
                            department?.DepartmentName,

                        DesignationId =
                            administrator.DesignationId,

                        DesignationName =
                            designation?.DesignationName,

                        RegionId =
                            administrator.RegionId,

                        BranchId =
                            administrator.BranchId,

                        EmployeeCode =
                            administrator.EmployeeCode,

                        Username =
                            administrator.Username,

                        FirstName =
                            administrator.FirstName,

                        LastName =
                            administrator.LastName,

                        Email =
                            administrator.Email,

                        MobileNumber =
                            administrator.MobileNumber,

                        RoleName =
                            administrator.RoleName,

                        ReportingManager =
                            administrator.ReportingManager,

                        ProfileImagePath =
                            administrator.ProfileImagePath,

                        Status =
                            administrator.Status,

                        EmailVerified =
                            administrator.EmailVerified,

                        MobileVerified =
                            administrator.MobileVerified,

                        TwoFactorAuthentication =
                            administrator.TwoFactorAuthentication,

                        Remarks =
                            administrator.Remarks,

                        // Null for users created before passwords were
                        // stored, or if the value cannot be decrypted.
                        Password =
                            PasswordProtector.Unprotect(
                                administrator.PasswordHash,
                                _config["PasswordEncryption:Key"]!)
                    };

                return new ApiResponse<CompanyAdministratorDto>
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
                    "Error while getting company administrator by id");

                throw;
            }
        }

        #endregion

        #endregion
        #region Business Unit

        #region CREATE

        public async Task<ApiResponse<string>> CreateBusinessUnit(
            BusinessUnitDto dto)
        {
            try
            {
                // Validate Business Unit Name
                if (string.IsNullOrWhiteSpace(dto.BusinessUnitName))
                    throw new CustomException(
                        "Business Unit Name is required.");

                // Validate Business Unit Code
                if (string.IsNullOrWhiteSpace(dto.BusinessUnitCode))
                    throw new CustomException(
                        "Business Unit Code is required.");

                // Validate Organization
                var organization =
                    (await _unitOfWork.Repository<OrganizationDatum>()
                        .FindAsync(x =>
                            x.OrganizationId == dto.OrganizationId))
                    .FirstOrDefault();

                if (organization == null)
                    throw new CustomException(
                        "Organization not found.");

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
                if (dto.RegionId.HasValue)
                {
                    var region =
                        (await _unitOfWork.Repository<Region>()
                            .FindAsync(x =>
                                x.RegionId == dto.RegionId.Value &&
                                x.CompanyId == dto.CompanyId))
                        .FirstOrDefault();

                    if (region == null)
                        throw new CustomException(
                            "Region not found for the selected company.");
                }

                // Validate Branch
                if (dto.BranchId.HasValue)
                {
                    var branch =
                        (await _unitOfWork.Repository<BranchDatum>()
                            .FindAsync(x =>
                                x.BranchId == dto.BranchId.Value &&
                                x.CompanyId == dto.CompanyId))
                        .FirstOrDefault();

                    if (branch == null)
                        throw new CustomException(
                            "Branch not found for the selected company.");

                    // If Region is selected,
                    // Branch must belong to selected Region
                    if (dto.RegionId.HasValue &&
                        branch.RegionId != dto.RegionId.Value)
                    {
                        throw new CustomException(
                            "Branch does not belong to the selected region.");
                    }
                }

                // Employee Strength Validation
                if (dto.EmployeeStrength.HasValue &&
                    dto.EmployeeStrength < 0)
                {
                    throw new CustomException(
                        "Employee Strength cannot be negative.");
                }

                // Annual Budget Validation
                if (dto.AnnualBudget.HasValue &&
                    dto.AnnualBudget < 0)
                {
                    throw new CustomException(
                        "Annual Budget cannot be negative.");
                }

                // Duplicate Business Unit Name
                var duplicateName =
                    await _unitOfWork.Repository<BusinessUnit>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId &&
                            x.BusinessUnitName.ToLower() ==
                            dto.BusinessUnitName.Trim().ToLower());

                if (duplicateName.Any())
                    throw new CustomException(
                        "Business Unit Name already exists.");

                // Duplicate Business Unit Code
                var duplicateCode =
                    await _unitOfWork.Repository<BusinessUnit>()
                        .FindAsync(x =>
                            x.CompanyId == dto.CompanyId &&
                            x.BusinessUnitCode.ToLower() ==
                            dto.BusinessUnitCode.Trim().ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Business Unit Code already exists.");

                // Default Business Unit Validation
                if (dto.DefaultBusinessUnit)
                {
                    var existingDefault =
                        await _unitOfWork.Repository<BusinessUnit>()
                            .FindAsync(x =>
                                x.CompanyId == dto.CompanyId &&
                                x.DefaultBusinessUnit);

                    if (existingDefault.Any())
                        throw new CustomException(
                            "Default Business Unit already exists for this company.");
                }

                // Create Entity
                BusinessUnit businessUnit = new BusinessUnit
                {
                    OrganizationId = dto.OrganizationId,

                    CompanyId = dto.CompanyId,

                    RegionId = dto.RegionId,

                    BranchId = dto.BranchId,

                    BusinessUnitName =
                        dto.BusinessUnitName.Trim(),

                    BusinessUnitCode =
                        dto.BusinessUnitCode.Trim(),

                    ParentBusinessUnit =
                        dto.ParentBusinessUnit?.Trim(),

                    BusinessUnitHead =
                        dto.BusinessUnitHead?.Trim(),

                    UnitHead =
                        dto.UnitHead?.Trim(),

                    Email =
                        dto.Email?.Trim(),

                    MobileNumber =
                        dto.MobileNumber?.Trim(),

                    ContactNumber =
                        dto.ContactNumber?.Trim(),

                    ExtensionNumber =
                        dto.ExtensionNumber?.Trim(),

                    Description =
                        dto.Description?.Trim(),

                    Remarks =
                        dto.Remarks?.Trim(),

                    Status =
                        dto.Status,

                    DefaultBusinessUnit =
                        dto.DefaultBusinessUnit,

                    BillableUnit =
                        dto.BillableUnit,

                    EmployeeStrength =
                        dto.EmployeeStrength,

                    CostCenterCode =
                        dto.CostCenterCode?.Trim(),

                    AnnualBudget =
                        dto.AnnualBudget,

                    CreatedBy =
                        _currentUserService.UserId,

                    CreatedDate =
                        DateTime.Now
                };

                await _unitOfWork.Repository<BusinessUnit>()
                    .AddAsync(businessUnit);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "BusinessUnit",
                    "INSERT",
                    businessUnit.BusinessUnitId,
                    "",
                    JsonConvert.SerializeObject(businessUnit),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Business Unit Created Successfully",

                    Data =
                        businessUnit.BusinessUnitName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating business unit");

                throw;
            }
        }

        #endregion


        #region UPDATE

        public async Task<ApiResponse<string>> UpdateBusinessUnit(
            BusinessUnitDto dto)
        {
            try
            {
                // Validate Business Unit Name
                if (string.IsNullOrWhiteSpace(dto.BusinessUnitName))
                    throw new CustomException(
                        "Business Unit Name is required.");

                // Validate Business Unit Code
                if (string.IsNullOrWhiteSpace(dto.BusinessUnitCode))
                    throw new CustomException(
                        "Business Unit Code is required.");

                // Get Existing Business Unit
                var businessUnit =
                    (await _unitOfWork.Repository<BusinessUnit>()
                        .FindAsync(x =>
                            x.BusinessUnitId ==
                            dto.BusinessUnitId))
                    .FirstOrDefault();

                if (businessUnit == null)
                    throw new CustomException(
                        "Business Unit not found.");

                // Validate Organization
                var organization =
                    (await _unitOfWork.Repository<OrganizationDatum>()
                        .FindAsync(x =>
                            x.OrganizationId ==
                            dto.OrganizationId))
                    .FirstOrDefault();

                if (organization == null)
                    throw new CustomException(
                        "Organization not found.");

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
                if (dto.RegionId.HasValue)
                {
                    var region =
                        (await _unitOfWork.Repository<Region>()
                            .FindAsync(x =>
                                x.RegionId ==
                                dto.RegionId.Value &&
                                x.CompanyId ==
                                dto.CompanyId))
                        .FirstOrDefault();

                    if (region == null)
                        throw new CustomException(
                            "Region not found for the selected company.");
                }

                // Validate Branch
                if (dto.BranchId.HasValue)
                {
                    var branch =
                        (await _unitOfWork.Repository<BranchDatum>()
                            .FindAsync(x =>
                                x.BranchId ==
                                dto.BranchId.Value &&
                                x.CompanyId ==
                                dto.CompanyId))
                        .FirstOrDefault();

                    if (branch == null)
                        throw new CustomException(
                            "Branch not found for the selected company.");

                    if (dto.RegionId.HasValue &&
                        branch.RegionId != dto.RegionId.Value)
                    {
                        throw new CustomException(
                            "Branch does not belong to the selected region.");
                    }
                }

                // Employee Strength Validation
                if (dto.EmployeeStrength.HasValue &&
                    dto.EmployeeStrength < 0)
                {
                    throw new CustomException(
                        "Employee Strength cannot be negative.");
                }

                // Annual Budget Validation
                if (dto.AnnualBudget.HasValue &&
                    dto.AnnualBudget < 0)
                {
                    throw new CustomException(
                        "Annual Budget cannot be negative.");
                }

                // Duplicate Name
                var duplicateName =
                    await _unitOfWork.Repository<BusinessUnit>()
                        .FindAsync(x =>
                            x.BusinessUnitId !=
                            dto.BusinessUnitId &&

                            x.CompanyId ==
                            dto.CompanyId &&

                            x.BusinessUnitName.ToLower() ==
                            dto.BusinessUnitName.Trim().ToLower());

                if (duplicateName.Any())
                    throw new CustomException(
                        "Business Unit Name already exists.");

                // Duplicate Code
                var duplicateCode =
                    await _unitOfWork.Repository<BusinessUnit>()
                        .FindAsync(x =>
                            x.BusinessUnitId !=
                            dto.BusinessUnitId &&

                            x.CompanyId ==
                            dto.CompanyId &&

                            x.BusinessUnitCode.ToLower() ==
                            dto.BusinessUnitCode.Trim().ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Business Unit Code already exists.");

                // Default Business Unit
                if (dto.DefaultBusinessUnit)
                {
                    var existingDefault =
                        await _unitOfWork.Repository<BusinessUnit>()
                            .FindAsync(x =>
                                x.BusinessUnitId !=
                                dto.BusinessUnitId &&

                                x.CompanyId ==
                                dto.CompanyId &&

                                x.DefaultBusinessUnit);

                    if (existingDefault.Any())
                        throw new CustomException(
                            "Default Business Unit already exists for this company.");
                }

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(businessUnit);

                // Update
                businessUnit.OrganizationId =
                    dto.OrganizationId;

                businessUnit.CompanyId =
                    dto.CompanyId;

                businessUnit.RegionId =
                    dto.RegionId;

                businessUnit.BranchId =
                    dto.BranchId;

                businessUnit.BusinessUnitName =
                    dto.BusinessUnitName.Trim();

                businessUnit.BusinessUnitCode =
                    dto.BusinessUnitCode.Trim();

                businessUnit.ParentBusinessUnit =
                    dto.ParentBusinessUnit?.Trim();

                businessUnit.BusinessUnitHead =
                    dto.BusinessUnitHead?.Trim();

                businessUnit.UnitHead =
                    dto.UnitHead?.Trim();

                businessUnit.Email =
                    dto.Email?.Trim();

                businessUnit.MobileNumber =
                    dto.MobileNumber?.Trim();

                businessUnit.ContactNumber =
                    dto.ContactNumber?.Trim();

                businessUnit.ExtensionNumber =
                    dto.ExtensionNumber?.Trim();

                businessUnit.Description =
                    dto.Description?.Trim();

                businessUnit.Remarks =
                    dto.Remarks?.Trim();

                businessUnit.Status =
                    dto.Status;

                businessUnit.DefaultBusinessUnit =
                    dto.DefaultBusinessUnit;

                businessUnit.BillableUnit =
                    dto.BillableUnit;

                businessUnit.EmployeeStrength =
                    dto.EmployeeStrength;

                businessUnit.CostCenterCode =
                    dto.CostCenterCode?.Trim();

                businessUnit.AnnualBudget =
                    dto.AnnualBudget;

                businessUnit.UpdatedBy =
                    _currentUserService.UserId;

                businessUnit.UpdatedDate =
                    DateTime.Now;

                _unitOfWork.Repository<BusinessUnit>()
                    .Update(businessUnit);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "BusinessUnit",
                    "UPDATE",
                    businessUnit.BusinessUnitId,
                    oldValues,
                    JsonConvert.SerializeObject(businessUnit),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Business Unit Updated Successfully",

                    Data =
                        businessUnit.BusinessUnitName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating business unit");

                throw;
            }
        }

        #endregion


        #region DELETE

        public async Task<ApiResponse<string>> DeleteBusinessUnit(
            int id)
        {
            try
            {
                var businessUnit =
                    (await _unitOfWork.Repository<BusinessUnit>()
                        .FindAsync(x =>
                            x.BusinessUnitId == id))
                    .FirstOrDefault();

                if (businessUnit == null)
                    throw new CustomException(
                        "Business Unit not found.");

                // Old Values
                string oldValues =
                    JsonConvert.SerializeObject(businessUnit);

                // Hard Delete
                _unitOfWork.Repository<BusinessUnit>()
                    .Remove(businessUnit);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "BusinessUnit",
                    "DELETE",
                    businessUnit.BusinessUnitId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Business Unit Deleted Successfully",

                    Data =
                        businessUnit.BusinessUnitName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting business unit");

                throw;
            }
        }

        #endregion


        #region GET ALL

        public async Task<ApiResponse<List<BusinessUnitDto>>>
            GetBusinessUnits()
        {
            try
            {
                var businessUnits =
                    (await _unitOfWork.Repository<BusinessUnit>()
                        .GetAllAsync())
                    .Where(x =>
                        x.CreatedBy == _currentUserService.UserId)
                    .OrderByDescending(x =>
                        x.BusinessUnitId)
                    .ToList();

                var result =
                    businessUnits.Select(x =>
                        new BusinessUnitDto
                        {
                            BusinessUnitId =
                                x.BusinessUnitId,

                            OrganizationId =
                                x.OrganizationId,

                            CompanyId =
                                x.CompanyId,

                            RegionId =
                                x.RegionId,

                            BranchId =
                                x.BranchId,

                            BusinessUnitName =
                                x.BusinessUnitName,

                            BusinessUnitCode =
                                x.BusinessUnitCode,

                            ParentBusinessUnit =
                                x.ParentBusinessUnit,

                            BusinessUnitHead =
                                x.BusinessUnitHead,

                            UnitHead =
                                x.UnitHead,

                            Email =
                                x.Email,

                            MobileNumber =
                                x.MobileNumber,

                            ContactNumber =
                                x.ContactNumber,

                            ExtensionNumber =
                                x.ExtensionNumber,

                            Description =
                                x.Description,

                            Remarks =
                                x.Remarks,

                            Status =
                                x.Status,

                            DefaultBusinessUnit =
                                x.DefaultBusinessUnit,

                            BillableUnit =
                                x.BillableUnit,

                            EmployeeStrength =
                                x.EmployeeStrength,

                            CostCenterCode =
                                x.CostCenterCode,

                            AnnualBudget =
                                x.AnnualBudget
                        })
                    .ToList();

                return new ApiResponse<List<BusinessUnitDto>>
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
                    "Error while getting business units");

                throw;
            }
        }

        #endregion


        #region GET BY ID

        public async Task<ApiResponse<BusinessUnitDto>>
            GetBusinessUnitById(int id)
        {
            try
            {
                var businessUnit =
                    (await _unitOfWork.Repository<BusinessUnit>()
                        .FindAsync(x =>
                            x.BusinessUnitId == id))
                    .FirstOrDefault();

                if (businessUnit == null)
                    throw new CustomException(
                        "Business Unit not found.");

                var result =
                    new BusinessUnitDto
                    {
                        BusinessUnitId =
                            businessUnit.BusinessUnitId,

                        OrganizationId =
                            businessUnit.OrganizationId,

                        CompanyId =
                            businessUnit.CompanyId,

                        RegionId =
                            businessUnit.RegionId,

                        BranchId =
                            businessUnit.BranchId,

                        BusinessUnitName =
                            businessUnit.BusinessUnitName,

                        BusinessUnitCode =
                            businessUnit.BusinessUnitCode,

                        ParentBusinessUnit =
                            businessUnit.ParentBusinessUnit,

                        BusinessUnitHead =
                            businessUnit.BusinessUnitHead,

                        UnitHead =
                            businessUnit.UnitHead,

                        Email =
                            businessUnit.Email,

                        MobileNumber =
                            businessUnit.MobileNumber,

                        ContactNumber =
                            businessUnit.ContactNumber,

                        ExtensionNumber =
                            businessUnit.ExtensionNumber,

                        Description =
                            businessUnit.Description,

                        Remarks =
                            businessUnit.Remarks,

                        Status =
                            businessUnit.Status,

                        DefaultBusinessUnit =
                            businessUnit.DefaultBusinessUnit,

                        BillableUnit =
                            businessUnit.BillableUnit,

                        EmployeeStrength =
                            businessUnit.EmployeeStrength,

                        CostCenterCode =
                            businessUnit.CostCenterCode,

                        AnnualBudget =
                            businessUnit.AnnualBudget
                    };

                return new ApiResponse<BusinessUnitDto>
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
                    "Error while getting business unit by id");

                throw;
            }
        }

        #endregion

        #endregion


    }
}
