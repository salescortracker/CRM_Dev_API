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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.MasterServices
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public CountryService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }
        public async Task<ApiResponse<string>> CreateCountry(CountryDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.CountryName))
                    throw new CustomException("Country Name is required.");

                var duplicate = await _unitOfWork.Repository<Country>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CountryName.ToLower() == dto.CountryName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Country already exists.");

                Country country = new Country
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    CountryName = dto.CountryName,
                    CountryCode = dto.CountryCode,
                    IsActive = dto.IsActive,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<Country>().AddAsync(country);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Country",
                    "INSERT",
                    country.CountryId,
                    "",
                    JsonConvert.SerializeObject(country),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Country Created Successfully",
                    Data = country.CountryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating country");
                throw;
            }
        }


        public async Task<ApiResponse<string>> UpdateCountry(CountryDto dto)
        {
            try
            {
                var country = (await _unitOfWork.Repository<Country>()
                    .FindAsync(x => x.CountryId == dto.CountryId))
                    .FirstOrDefault();

                if (country == null)
                    throw new CustomException("Country not found.");

                var duplicate = await _unitOfWork.Repository<Country>()
                    .FindAsync(x =>
                        x.CountryId != dto.CountryId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CountryName.ToLower() == dto.CountryName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("Country already exists.");

                string oldValues = JsonConvert.SerializeObject(country);

                country.CompanyId = dto.CompanyId;
                country.RegionId = dto.RegionId;
                country.CountryName = dto.CountryName;
                country.CountryCode = dto.CountryCode;
                country.IsActive = dto.IsActive;
                country.ModifiedBy = _currentUserService.UserId;
                country.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<Country>().Update(country);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Country",
                    "UPDATE",
                    country.CountryId,
                    oldValues,
                    JsonConvert.SerializeObject(country),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Country Updated Successfully",
                    Data = country.CountryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating country");
                throw;
            }
        }

        public async Task<ApiResponse<string>> DeleteCountry(int id)
        {
            try
            {
                var country = (await _unitOfWork.Repository<Country>()
                    .FindAsync(x => x.CountryId == id))
                    .FirstOrDefault();

                if (country == null)
                    throw new CustomException("Country not found.");

                string oldValues = JsonConvert.SerializeObject(country);

                _unitOfWork.Repository<Country>().Remove(country);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Country",
                    "DELETE",
                    country.CountryId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Country Deleted Successfully",
                    Data = country.CountryName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting country");
                throw;
            }
        }

        public async Task<ApiResponse<List<CountryDto>>> GetCountries()
        {
            var companies = await _unitOfWork.Repository<Company>().GetAllAsync();
            var regions = await _unitOfWork.Repository<Region>().GetAllAsync();
            var countries = await _unitOfWork.Repository<Country>().GetAllAsync();

            var result = (from ct in countries
                          join c in companies on ct.CompanyId equals c.CompanyId
                          join r in regions on ct.RegionId equals r.RegionId
                          where !ct.IsDeleted
                          select new CountryDto
                          {
                              CountryId = ct.CountryId,
                              CompanyId = ct.CompanyId,
                              CompanyName = c.CompanyName,
                              RegionId = ct.RegionId,
                              RegionName = r.RegionName,
                              CountryName = ct.CountryName,
                              CountryCode = ct.CountryCode,
                              IsActive = ct.IsActive
                          })
                          .OrderByDescending(x => x.CountryId)
                          .ToList();

            return new ApiResponse<List<CountryDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }

        public async Task<ApiResponse<CountryDto>> GetCountryById(int id)
        {
            var country = (await _unitOfWork.Repository<Country>()
                .FindAsync(x => x.CountryId == id))
                .FirstOrDefault();

            if (country == null)
                throw new CustomException("Country not found.");

            return new ApiResponse<CountryDto>
            {
                Success = true,
                Message = "Success",
                Data = new CountryDto
                {
                    CountryId = country.CountryId,
                    CompanyId = country.CompanyId,
                    RegionId = country.RegionId,
                    CountryName = country.CountryName,
                    CountryCode = country.CountryCode,
                    IsActive = country.IsActive
                }
            };
        }
    }
}
