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
    public class Stateservice : Istateservices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public Stateservice(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<string>> CreateState(StateDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.StateName))
                    throw new CustomException("State Name is required.");

                var duplicate = await _unitOfWork.Repository<StateMaster>()
                    .FindAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CountryId == dto.CountryId &&
                        x.StateName.ToLower() == dto.StateName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("State already exists.");

                StateMaster state = new StateMaster
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    CountryId = dto.CountryId,
                    StateName = dto.StateName,
                    StateCode = dto.StateCode,
                    IsActive = dto.IsActive,
                    IsDeleted = false,
                    CreatedBy = _currentUserService.UserId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<StateMaster>().AddAsync(state);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "State",
                    "INSERT",
                    state.StateId,
                    "",
                    JsonConvert.SerializeObject(state),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "State Created Successfully",
                    Data = state.StateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating state");
                throw;
            }
        }

        public async Task<ApiResponse<string>> UpdateState(StateDto dto)
        {
            try
            {
                var state = (await _unitOfWork.Repository<StateMaster>()
                    .FindAsync(x => x.StateId == dto.StateId))
                    .FirstOrDefault();

                if (state == null)
                    throw new CustomException("State not found.");

                var duplicate = await _unitOfWork.Repository<StateMaster>()
                    .FindAsync(x =>
                        x.StateId != dto.StateId &&
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CountryId == dto.CountryId &&
                        x.StateName.ToLower() == dto.StateName.ToLower());

                if (duplicate.Any())
                    throw new CustomException("State already exists.");

                string oldValues = JsonConvert.SerializeObject(state);

                state.CompanyId = dto.CompanyId;
                state.RegionId = dto.RegionId;
                state.CountryId = dto.CountryId;
                state.StateName = dto.StateName;
                state.StateCode = dto.StateCode;
                state.IsActive = dto.IsActive;
                state.ModifiedBy = _currentUserService.UserId;
                state.ModifiedAt = DateTime.Now;

                _unitOfWork.Repository<StateMaster>().Update(state);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "State",
                    "UPDATE",
                    state.StateId,
                    oldValues,
                    JsonConvert.SerializeObject(state),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "State Updated Successfully",
                    Data = state.StateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating state");
                throw;
            }
        }

        public async Task<ApiResponse<string>> DeleteState(int id)
        {
            try
            {
                var state = (await _unitOfWork.Repository<StateMaster>()
                    .FindAsync(x => x.StateId == id))
                    .FirstOrDefault();

                if (state == null)
                    throw new CustomException("State not found.");

                string oldValues = JsonConvert.SerializeObject(state);

                _unitOfWork.Repository<StateMaster>().Remove(state);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "State",
                    "DELETE",
                    state.StateId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "State Deleted Successfully",
                    Data = state.StateName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting state");
                throw;
            }
        }

        public async Task<ApiResponse<List<StateDto>>> GetStates()
        {
            var companies = await _unitOfWork.Repository<Company>().GetAllAsync();
            var regions = await _unitOfWork.Repository<Region>().GetAllAsync();
            var countries = await _unitOfWork.Repository<Country>().GetAllAsync();
            var states = await _unitOfWork.Repository<StateMaster>().GetAllAsync();

            var result = (from s in states
                          join c in companies on s.CompanyId equals c.CompanyId
                          join r in regions on s.RegionId equals r.RegionId
                          join co in countries on s.CountryId equals co.CountryId
                          where !s.IsDeleted
                          select new StateDto
                          {
                              StateId = s.StateId,
                              CompanyId = s.CompanyId,
                              CompanyName = c.CompanyName,
                              RegionId = s.RegionId,
                              RegionName = r.RegionName,
                              CountryId = s.CountryId,
                              CountryName = co.CountryName,
                              StateName = s.StateName,
                              StateCode = s.StateCode,
                              IsActive = s.IsActive
                          })
                          .OrderByDescending(x => x.StateId)
                          .ToList();

            return new ApiResponse<List<StateDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }
        public async Task<ApiResponse<StateDto>> GetStateById(int id)
        {
            var state = (await _unitOfWork.Repository<StateMaster>()
                .FindAsync(x => x.StateId == id))
                .FirstOrDefault();

            if (state == null)
                throw new CustomException("State not found.");

            return new ApiResponse<StateDto>
            {
                Success = true,
                Message = "Success",
                Data = new StateDto
                {
                    StateId = state.StateId,
                    CompanyId = state.CompanyId,
                    RegionId = state.RegionId,
                    CountryId = state.CountryId,
                    StateName = state.StateName,
                    StateCode = state.StateCode,
                    IsActive = state.IsActive
                }
            };
        }
    }
}
