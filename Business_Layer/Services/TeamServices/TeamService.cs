using Business_Layer.DTOs.Teams;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.Services;
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

namespace Business_Layer.Services.TeamServices
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IAuditService _auditService;

        private readonly ICurrentUserService _currentUserService;

        public TeamService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }

        #region CREATE

        public async Task<ApiResponse<string>>
            CreateTeam(WorkTeamDto dto)
        {
            try
            {
                ValidateTeam(dto);

                var existingTeam =
                    await _unitOfWork.Repository<WorkTeam>()
                    .FindAsync(x => x.TeamName.ToLower() == dto.TeamName.Trim().ToLower());

                if (existingTeam.Any())
                {
                    throw new CustomException("Team Name already exists.");
                }

                string teamCode =
                    await GenerateUniqueTeamCode(dto.TeamName.Trim());

                WorkTeam team = new WorkTeam
                {
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    DepartmentId = dto.DepartmentId,
                    TeamLeadId = dto.TeamLeadId,
                    TeamName = dto.TeamName.Trim(),
                    TeamCode = teamCode,
                    Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim(),
                    Status = dto.Status,
                    IsDefault = dto.IsDefault,
                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now
                };

                await _unitOfWork.Repository<WorkTeam>()
                    .AddAsync(team);

                await _unitOfWork.CompleteAsync();

                await SyncTeamMembers(team.TeamId, dto.MemberIds);

                await _auditService.LogAsync(
                    "Team",
                    "INSERT",
                    team.TeamId,
                    "",
                    JsonConvert.SerializeObject(team),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Team Created Successfully",
                    Data = team.TeamName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating team");
                throw;
            }
        }

        #endregion

        #region UPDATE

        public async Task<ApiResponse<string>>
            UpdateTeam(WorkTeamDto dto)
        {
            try
            {
                ValidateTeam(dto);

                var team =
                    (await _unitOfWork.Repository<WorkTeam>()
                    .FindAsync(x => x.TeamId == dto.TeamId))
                    .FirstOrDefault();

                if (team == null)
                {
                    throw new CustomException("Team not found.");
                }

                var duplicateName =
                    await _unitOfWork.Repository<WorkTeam>()
                    .FindAsync(x =>
                        x.TeamId != dto.TeamId &&
                        x.TeamName.ToLower() == dto.TeamName.Trim().ToLower());

                if (duplicateName.Any())
                {
                    throw new CustomException("Team Name already exists.");
                }

                string oldValues =
                    JsonConvert.SerializeObject(team);

                team.CompanyId = dto.CompanyId;
                team.RegionId = dto.RegionId;
                team.DepartmentId = dto.DepartmentId;
                team.TeamLeadId = dto.TeamLeadId;
                team.TeamName = dto.TeamName.Trim();
                team.Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim();
                team.Status = dto.Status;
                team.IsDefault = dto.IsDefault;
                team.ModifiedBy = _currentUserService.UserId;
                team.ModifiedDate = DateTime.Now;

                _unitOfWork.Repository<WorkTeam>()
                    .Update(team);

                await _unitOfWork.CompleteAsync();

                await SyncTeamMembers(team.TeamId, dto.MemberIds);

                string newValues =
                    JsonConvert.SerializeObject(team);

                await _auditService.LogAsync(
                    "Team",
                    "UPDATE",
                    team.TeamId,
                    oldValues,
                    newValues,
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Team Updated Successfully",
                    Data = team.TeamName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating team");
                throw;
            }
        }

        #endregion

        #region DELETE

        public async Task<ApiResponse<string>>
            DeleteTeam(int id)
        {
            try
            {
                var team =
                    (await _unitOfWork.Repository<WorkTeam>()
                    .FindAsync(x => x.TeamId == id))
                    .FirstOrDefault();

                if (team == null)
                {
                    throw new CustomException("Team not found.");
                }

                string oldValues =
                    JsonConvert.SerializeObject(team);

                team.Status = false;
                team.ModifiedBy = _currentUserService.UserId;
                team.ModifiedDate = DateTime.Now;

                _unitOfWork.Repository<WorkTeam>()
                    .Update(team);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Team",
                    "DELETE",
                    team.TeamId,
                    oldValues,
                    JsonConvert.SerializeObject(team),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Team Deleted Successfully",
                    Data = team.TeamName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting team");
                throw;
            }
        }

        #endregion

        #region GET ALL

        public async Task<ApiResponse<List<WorkTeamDto>>>
            GetTeams()
        {
            try
            {
                var teams =
                    (await _unitOfWork.Repository<WorkTeam>()
                    .FindAsync(x =>
                        x.Status &&
                        x.CreatedBy == _currentUserService.UserId))
                    .OrderByDescending(x => x.TeamId)
                    .ToList();

                var teamIds = teams.Select(x => x.TeamId).ToList();

                var members =
                    (await _unitOfWork.Repository<TeamMember>()
                    .FindAsync(x => teamIds.Contains(x.TeamId)))
                    .ToList();

                var companies = (await _unitOfWork.Repository<Company>().GetAllAsync()).ToList();

                var regions = (await _unitOfWork.Repository<Region>().GetAllAsync()).ToList();

                var departments = (await _unitOfWork.Repository<Department>().GetAllAsync()).ToList();

                var administrators = (await _unitOfWork.Repository<CompanyAdministrator>().GetAllAsync()).ToList();

                var result = teams.Select(x =>
                    MapToDto(x, members, companies, regions, departments, administrators)).ToList();

                return new ApiResponse<List<WorkTeamDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting teams");
                throw;
            }
        }

        #endregion

        #region GET BY ID

        public async Task<ApiResponse<WorkTeamDto>>
            GetTeamById(int id)
        {
            try
            {
                var team =
                    (await _unitOfWork.Repository<WorkTeam>()
                    .FindAsync(x => x.TeamId == id))
                    .FirstOrDefault();

                if (team == null)
                {
                    throw new CustomException("Team not found.");
                }

                var members =
                    (await _unitOfWork.Repository<TeamMember>()
                    .FindAsync(x => x.TeamId == id))
                    .ToList();

                var companies = (await _unitOfWork.Repository<Company>().GetAllAsync()).ToList();

                var regions = (await _unitOfWork.Repository<Region>().GetAllAsync()).ToList();

                var departments = (await _unitOfWork.Repository<Department>().GetAllAsync()).ToList();

                var administrators = (await _unitOfWork.Repository<CompanyAdministrator>().GetAllAsync()).ToList();

                return new ApiResponse<WorkTeamDto>
                {
                    Success = true,
                    Message = "Success",
                    Data = MapToDto(team, members, companies, regions, departments, administrators)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting team by id");
                throw;
            }
        }

        #endregion

        #region HELPERS

        private static void ValidateTeam(WorkTeamDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TeamName))
            {
                throw new CustomException("Team Name is required.");
            }

            if (dto.CompanyId <= 0)
            {
                throw new CustomException("Company is required.");
            }
        }

        private static WorkTeamDto MapToDto(
            WorkTeam team,
            List<TeamMember> members,
            List<Company> companies,
            List<Region> regions,
            List<Department> departments,
            List<CompanyAdministrator> administrators)
        {
            var teamMembers = members.Where(x => x.TeamId == team.TeamId).ToList();

            var memberAdmins = administrators
                .Where(a => teamMembers.Select(m => m.AdministratorId).Contains(a.AdministratorId))
                .ToList();

            var teamLead = team.TeamLeadId.HasValue
                ? administrators.FirstOrDefault(a => a.AdministratorId == team.TeamLeadId.Value)
                : null;

            return new WorkTeamDto
            {
                TeamId = team.TeamId,
                CompanyId = team.CompanyId,
                CompanyName = companies.FirstOrDefault(c => c.CompanyId == team.CompanyId)?.CompanyName,
                RegionId = team.RegionId,
                RegionName = team.RegionId.HasValue
                    ? regions.FirstOrDefault(r => r.RegionId == team.RegionId.Value)?.RegionName
                    : null,
                DepartmentId = team.DepartmentId,
                DepartmentName = team.DepartmentId.HasValue
                    ? departments.FirstOrDefault(d => d.DepartmentId == team.DepartmentId.Value)?.DepartmentName
                    : null,
                TeamLeadId = team.TeamLeadId,
                TeamLeadName = teamLead != null
                    ? $"{teamLead.FirstName} {teamLead.LastName}".Trim()
                    : null,
                TeamName = team.TeamName,
                TeamCode = team.TeamCode,
                Description = team.Description,
                Status = team.Status,
                IsDefault = team.IsDefault,
                MemberIds = memberAdmins.Select(a => a.AdministratorId).ToList(),
                MemberNames = memberAdmins.Select(a => $"{a.FirstName} {a.LastName}".Trim()).ToList()
            };
        }

        private async Task SyncTeamMembers(int teamId, List<int> memberIds)
        {
            var existing =
                (await _unitOfWork.Repository<TeamMember>()
                .FindAsync(x => x.TeamId == teamId))
                .ToList();

            foreach (var item in existing)
            {
                _unitOfWork.Repository<TeamMember>().Remove(item);
            }

            if (memberIds != null)
            {
                foreach (var administratorId in memberIds.Distinct())
                {
                    await _unitOfWork.Repository<TeamMember>()
                        .AddAsync(new TeamMember
                        {
                            TeamId = teamId,
                            AdministratorId = administratorId,
                            CreatedBy = _currentUserService.UserId,
                            CreatedDate = DateTime.Now
                        });
                }
            }

            await _unitOfWork.CompleteAsync();
        }

        private async Task<string> GenerateUniqueTeamCode(string teamName)
        {
            const int maxLength = 20;

            string baseCode =
                new string(teamName.ToUpper().Where(char.IsLetterOrDigit).ToArray());

            if (string.IsNullOrWhiteSpace(baseCode))
            {
                baseCode = "TEAM";
            }

            if (baseCode.Length > maxLength)
            {
                baseCode = baseCode.Substring(0, maxLength);
            }

            string code = baseCode;
            int suffix = 1;

            while ((await _unitOfWork.Repository<WorkTeam>().FindAsync(x => x.TeamCode == code)).Any())
            {
                suffix++;

                string suffixText = suffix.ToString();

                int maxBaseLength = maxLength - suffixText.Length;

                string trimmedBase =
                    baseCode.Length > maxBaseLength
                        ? baseCode.Substring(0, maxBaseLength)
                        : baseCode;

                code = trimmedBase + suffixText;
            }

            return code;
        }

        #endregion
    }
}
