using Business_Layer.DTOs.MasterDTO_s;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.MasterIInterface;
using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Newtonsoft.Json;
using Serilog;
using Shared.CommonModels;
using Shared.Exceptions;

namespace Business_Layer.Services.MasterServices
{
    public class CompanyAndRegionService : ICompanyAndRegionService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public CompanyAndRegionService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
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
                    PlanStartDate = dto.PlanStartDate,
                    ExpiryDate = dto.ExpiryDate,
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
                company.PlanStartDate = dto.PlanStartDate;
                company.ExpiryDate = dto.ExpiryDate;
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
                PlanStartDate = x.PlanStartDate,
                ExpiryDate = x.ExpiryDate,
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
                    PlanStartDate = company.PlanStartDate,
                    ExpiryDate = company.ExpiryDate,
                    CompanyEmail = company.CompanyEmail,
                    CompanyContact = company.CompanyContact,
                    CompanyAddress = company.CompanyAddress,
                    CompanyLogo = company.CompanyLogo
                }
            };
        }

        #endregion
        #endregion

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
    }
}
