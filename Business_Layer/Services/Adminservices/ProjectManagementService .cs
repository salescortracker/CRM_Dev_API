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
   
        public class ProjectManagementService : IProjectManagementService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IAuditService _auditService;
            private readonly ICurrentUserService _currentUserService;

            public ProjectManagementService(
                IUnitOfWork unitOfWork,
                IAuditService auditService,
                ICurrentUserService currentUserService)
            {
                _unitOfWork = unitOfWork;
                _auditService = auditService;
                _currentUserService = currentUserService;
            }

            // CREATE
            public async Task<ApiResponse<string>> CreateProjectManagement(
                ProjectManagementDto dto)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(dto.ProjectCode))
                        throw new CustomException(
                            "Project Code is required.");

                    if (string.IsNullOrWhiteSpace(dto.ProjectName))
                        throw new CustomException(
                            "Project Name is required.");

                    if (string.IsNullOrWhiteSpace(dto.Priority))
                        throw new CustomException(
                            "Priority is required.");

                    if (string.IsNullOrWhiteSpace(dto.Status))
                        throw new CustomException(
                            "Status is required.");

                    // Date validation
                    if (dto.StartDate.HasValue &&
                        dto.EndDate.HasValue &&
                        dto.EndDate.Value < dto.StartDate.Value)
                    {
                        throw new CustomException(
                            "End Date cannot be earlier than Start Date.");
                    }

                    // Budget validation
                    if (dto.Budget.HasValue &&
                        dto.Budget.Value < 0)
                    {
                        throw new CustomException(
                            "Budget cannot be negative.");
                    }

                    // Completion percentage validation
                    if (dto.CompletionPercentage < 0 ||
                        dto.CompletionPercentage > 100)
                    {
                        throw new CustomException(
                            "Completion Percentage must be between 0 and 100.");
                    }

                    // Duplicate Project Code
                    var duplicateCode =
                        (await _unitOfWork
                            .Repository<ProjectManagement>()
                            .FindAsync(x =>
                                x.ProjectCode.ToLower() ==
                                dto.ProjectCode.Trim().ToLower() &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (duplicateCode != null)
                        throw new CustomException(
                            "Project Code already exists.");

                    // Duplicate Project Name
                    var duplicateName =
                        (await _unitOfWork
                            .Repository<ProjectManagement>()
                            .FindAsync(x =>
                                x.ProjectName.ToLower() ==
                                dto.ProjectName.Trim().ToLower() &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (duplicateName != null)
                        throw new CustomException(
                            "Project Name already exists.");

                    var project = new ProjectManagement
                    {
                        ProjectCode = dto.ProjectCode.Trim(),
                        ProjectName = dto.ProjectName.Trim(),
                        Customer = dto.Customer?.Trim(),
                        ProjectManager = dto.ProjectManager,
                        ProjectType = dto.ProjectType?.Trim(),
                        Priority = dto.Priority.Trim(),
                        Status = dto.Status.Trim(),
                        StartDate = dto.StartDate,
                        EndDate = dto.EndDate,
                        Budget = dto.Budget,
                        CompletionPercentage =
                            dto.CompletionPercentage,
                        TeamMembers = dto.TeamMembers?.Trim(),
                        ProjectDescription =
                            dto.ProjectDescription?.Trim(),

                        CreatedBy = _currentUserService.UserId,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    };

                    await _unitOfWork
                        .Repository<ProjectManagement>()
                        .AddAsync(project);

                    await _unitOfWork.CompleteAsync();

                    // Audit
                    await _auditService.LogAsync(
                        "ProjectManagement",
                        "INSERT",
                        project.ProjectId,
                        "",
                        JsonConvert.SerializeObject(project),
                        _currentUserService.UserId);

                    return new ApiResponse<string>
                    {
                        Success = true,
                        Message = "Project created successfully.",
                        Data = project.ProjectName
                    };
                }
                catch (Exception ex)
                {
                    Log.Error(
                        ex,
                        "Error while creating project.");

                    throw;
                }
            }

            // UPDATE
            public async Task<ApiResponse<string>> UpdateProjectManagement(
                ProjectManagementDto dto)
            {
                try
                {
                    if (dto.ProjectId <= 0)
                        throw new CustomException(
                            "Valid Project Id is required.");

                    if (string.IsNullOrWhiteSpace(dto.ProjectCode))
                        throw new CustomException(
                            "Project Code is required.");

                    if (string.IsNullOrWhiteSpace(dto.ProjectName))
                        throw new CustomException(
                            "Project Name is required.");

                    if (string.IsNullOrWhiteSpace(dto.Priority))
                        throw new CustomException(
                            "Priority is required.");

                    if (string.IsNullOrWhiteSpace(dto.Status))
                        throw new CustomException(
                            "Status is required.");

                    // Date validation
                    if (dto.StartDate.HasValue &&
                        dto.EndDate.HasValue &&
                        dto.EndDate.Value < dto.StartDate.Value)
                    {
                        throw new CustomException(
                            "End Date cannot be earlier than Start Date.");
                    }

                    // Budget validation
                    if (dto.Budget.HasValue &&
                        dto.Budget.Value < 0)
                    {
                        throw new CustomException(
                            "Budget cannot be negative.");
                    }

                    // Completion percentage validation
                    if (dto.CompletionPercentage < 0 ||
                        dto.CompletionPercentage > 100)
                    {
                        throw new CustomException(
                            "Completion Percentage must be between 0 and 100.");
                    }

                    var project =
                        (await _unitOfWork
                            .Repository<ProjectManagement>()
                            .FindAsync(x =>
                                x.ProjectId == dto.ProjectId &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (project == null)
                        throw new CustomException(
                            "Project not found.");

                    // Duplicate Project Code
                    var duplicateCode =
                        (await _unitOfWork
                            .Repository<ProjectManagement>()
                            .FindAsync(x =>
                                x.ProjectId != dto.ProjectId &&
                                x.ProjectCode.ToLower() ==
                                dto.ProjectCode.Trim().ToLower() &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (duplicateCode != null)
                        throw new CustomException(
                            "Project Code already exists.");

                    // Duplicate Project Name
                    var duplicateName =
                        (await _unitOfWork
                            .Repository<ProjectManagement>()
                            .FindAsync(x =>
                                x.ProjectId != dto.ProjectId &&
                                x.ProjectName.ToLower() ==
                                dto.ProjectName.Trim().ToLower() &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (duplicateName != null)
                        throw new CustomException(
                            "Project Name already exists.");

                    var oldValues =
                        JsonConvert.SerializeObject(project);

                    project.ProjectCode =
                        dto.ProjectCode.Trim();

                    project.ProjectName =
                        dto.ProjectName.Trim();

                    project.Customer =
                        dto.Customer?.Trim();

                    project.ProjectManager =
                        dto.ProjectManager;

                    project.ProjectType =
                        dto.ProjectType?.Trim();

                    project.Priority =
                        dto.Priority.Trim();

                    project.Status =
                        dto.Status.Trim();

                    project.StartDate =
                        dto.StartDate;

                    project.EndDate =
                        dto.EndDate;

                    project.Budget =
                        dto.Budget;

                    project.CompletionPercentage =
                        dto.CompletionPercentage;

                    project.TeamMembers =
                        dto.TeamMembers?.Trim();

                    project.ProjectDescription =
                        dto.ProjectDescription?.Trim();

                    project.ModifiedBy =
                        _currentUserService.UserId;

                    project.ModifiedAt =
                        DateTime.Now;

                    _unitOfWork
                        .Repository<ProjectManagement>()
                        .Update(project);

                    await _unitOfWork.CompleteAsync();

                    // Audit
                    await _auditService.LogAsync(
                        "ProjectManagement",
                        "UPDATE",
                        project.ProjectId,
                        oldValues,
                        JsonConvert.SerializeObject(project),
                        _currentUserService.UserId);

                    return new ApiResponse<string>
                    {
                        Success = true,
                        Message = "Project updated successfully.",
                        Data = project.ProjectName
                    };
                }
                catch (Exception ex)
                {
                    Log.Error(
                        ex,
                        "Error while updating project.");

                    throw;
                }
            }

            // DELETE - Soft Delete
            public async Task<ApiResponse<string>> DeleteProjectManagement(
                int id)
            {
                try
                {
                    if (id <= 0)
                        throw new CustomException(
                            "Valid Project Id is required.");

                    var project =
                        (await _unitOfWork
                            .Repository<ProjectManagement>()
                            .FindAsync(x =>
                                x.ProjectId == id &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (project == null)
                        throw new CustomException(
                            "Project not found.");

                    var oldValues =
                        JsonConvert.SerializeObject(project);

                    project.IsDeleted = true;

                    project.ModifiedBy =
                        _currentUserService.UserId;

                    project.ModifiedAt =
                        DateTime.Now;

                    _unitOfWork
                        .Repository<ProjectManagement>()
                        .Update(project);

                    await _unitOfWork.CompleteAsync();

                    // Audit
                    await _auditService.LogAsync(
                        "ProjectManagement",
                        "DELETE",
                        project.ProjectId,
                        oldValues,
                        "",
                        _currentUserService.UserId);

                    return new ApiResponse<string>
                    {
                        Success = true,
                        Message = "Project deleted successfully.",
                        Data = project.ProjectName
                    };
                }
                catch (Exception ex)
                {
                    Log.Error(
                        ex,
                        "Error while deleting project.");

                    throw;
                }
            }

            // GET ALL
            public async Task<ApiResponse<List<ProjectManagementDto>>>
                GetProjectManagements()
            {
                try
                {
                    var projects =
                        await _unitOfWork
                            .Repository<ProjectManagement>()
                            .FindAsync(x =>
                                !x.IsDeleted &&
                                x.CreatedBy == _currentUserService.UserId);

                    var result = projects
                        .Select(x => new ProjectManagementDto
                        {
                            ProjectId = x.ProjectId,
                            ProjectCode = x.ProjectCode,
                            ProjectName = x.ProjectName,
                            Customer = x.Customer,
                            ProjectManager = x.ProjectManager,
                            ProjectType = x.ProjectType,
                            Priority = x.Priority,
                            Status = x.Status,
                            StartDate = x.StartDate,
                            EndDate = x.EndDate,
                            Budget = x.Budget,
                            CompletionPercentage =
                                x.CompletionPercentage,
                            TeamMembers = x.TeamMembers,
                            ProjectDescription =
                                x.ProjectDescription
                        })
                        .ToList();

                    return new ApiResponse<List<ProjectManagementDto>>
                    {
                        Success = true,
                        Message = "Projects retrieved successfully.",
                        Data = result
                    };
                }
                catch (Exception ex)
                {
                    Log.Error(
                        ex,
                        "Error while getting projects.");

                    throw;
                }
            }

            // GET BY ID
            public async Task<ApiResponse<ProjectManagementDto>>
                GetProjectManagementById(int id)
            {
                try
                {
                    if (id <= 0)
                        throw new CustomException(
                            "Valid Project Id is required.");

                    var project =
                        (await _unitOfWork
                            .Repository<ProjectManagement>()
                            .FindAsync(x =>
                                x.ProjectId == id &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (project == null)
                        throw new CustomException(
                            "Project not found.");

                    var result = new ProjectManagementDto
                    {
                        ProjectId = project.ProjectId,
                        ProjectCode = project.ProjectCode,
                        ProjectName = project.ProjectName,
                        Customer = project.Customer,
                        ProjectManager = project.ProjectManager,
                        ProjectType = project.ProjectType,
                        Priority = project.Priority,
                        Status = project.Status,
                        StartDate = project.StartDate,
                        EndDate = project.EndDate,
                        Budget = project.Budget,
                        CompletionPercentage =
                            project.CompletionPercentage,
                        TeamMembers = project.TeamMembers,
                        ProjectDescription =
                            project.ProjectDescription
                    };

                    return new ApiResponse<ProjectManagementDto>
                    {
                        Success = true,
                        Message = "Project retrieved successfully.",
                        Data = result
                    };
                }
                catch (Exception ex)
                {
                    Log.Error(
                        ex,
                        "Error while getting project by id.");

                    throw;
                }
            }
        // CREATE
        public async Task<ApiResponse<string>> CreateProjectMilestone(
            ProjectMilestoneDto dto)
        {
            try
            {
                // Required field validations
                if (string.IsNullOrWhiteSpace(dto.MilestoneName))
                    throw new CustomException(
                        "Milestone Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Project))
                    throw new CustomException(
                        "Project is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                if (string.IsNullOrWhiteSpace(dto.Priority))
                    throw new CustomException(
                        "Priority is required.");

                // Completion percentage validation
                if (dto.CompletionPercentage < 0 ||
                    dto.CompletionPercentage > 100)
                {
                    throw new CustomException(
                        "Completion Percentage must be between 0 and 100.");
                }

                // Estimated hours validation
                if (dto.EstimatedHours.HasValue &&
                    dto.EstimatedHours.Value < 0)
                {
                    throw new CustomException(
                        "Estimated Hours cannot be negative.");
                }

                // Actual hours validation
                if (dto.ActualHours.HasValue &&
                    dto.ActualHours.Value < 0)
                {
                    throw new CustomException(
                        "Actual Hours cannot be negative.");
                }

                // Actual hours should not exceed estimated hours
                if (dto.EstimatedHours.HasValue &&
                    dto.ActualHours.HasValue &&
                    dto.ActualHours.Value >
                    dto.EstimatedHours.Value)
                {
                    throw new CustomException(
                        "Actual Hours cannot exceed Estimated Hours.");
                }

                // Due date validation
                if (dto.DueDate.HasValue &&
                    dto.TargetDate.HasValue &&
                    dto.DueDate.Value > dto.TargetDate.Value)
                {
                    throw new CustomException(
                        "Due Date cannot be after Target Date.");
                }

                // Duplicate milestone validation
                var duplicateMilestone =
                    (await _unitOfWork
                        .Repository<ProjectMilestone>()
                        .FindAsync(x =>
                            x.MilestoneName.ToLower() ==
                            dto.MilestoneName.Trim().ToLower() &&
                            x.Project.ToLower() ==
                            dto.Project.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateMilestone != null)
                    throw new CustomException(
                        "Milestone Name already exists for this Project.");

                var milestone = new ProjectMilestone
                {
                    MilestoneName = dto.MilestoneName.Trim(),
                    Project = dto.Project.Trim(),
                    Owner = dto.Owner,
                    DueDate = dto.DueDate,
                    Status = dto.Status.Trim(),
                    CompletionPercentage =
                        dto.CompletionPercentage,
                    EstimatedHours = dto.EstimatedHours,
                    ActualHours = dto.ActualHours,
                    Priority = dto.Priority.Trim(),
                    TargetDate = dto.TargetDate,
                    Description = dto.Description?.Trim(),

                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,

                    IsDeleted = false
                };

                await _unitOfWork
                    .Repository<ProjectMilestone>()
                    .AddAsync(milestone);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "ProjectMilestone",
                    "INSERT",
                    milestone.MilestoneId,
                    "",
                    JsonConvert.SerializeObject(milestone),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Project milestone created successfully.",
                    Data = milestone.MilestoneName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating project milestone.");

                throw;
            }
        }

        // UPDATE
        public async Task<ApiResponse<string>> UpdateProjectMilestone(
            ProjectMilestoneDto dto)
        {
            try
            {
                if (dto.MilestoneId <= 0)
                    throw new CustomException(
                        "Valid Milestone Id is required.");

                if (string.IsNullOrWhiteSpace(dto.MilestoneName))
                    throw new CustomException(
                        "Milestone Name is required.");

                if (string.IsNullOrWhiteSpace(dto.Project))
                    throw new CustomException(
                        "Project is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                if (string.IsNullOrWhiteSpace(dto.Priority))
                    throw new CustomException(
                        "Priority is required.");

                // Completion percentage validation
                if (dto.CompletionPercentage < 0 ||
                    dto.CompletionPercentage > 100)
                {
                    throw new CustomException(
                        "Completion Percentage must be between 0 and 100.");
                }

                // Estimated hours validation
                if (dto.EstimatedHours.HasValue &&
                    dto.EstimatedHours.Value < 0)
                {
                    throw new CustomException(
                        "Estimated Hours cannot be negative.");
                }

                // Actual hours validation
                if (dto.ActualHours.HasValue &&
                    dto.ActualHours.Value < 0)
                {
                    throw new CustomException(
                        "Actual Hours cannot be negative.");
                }

                // Actual hours should not exceed estimated hours
                if (dto.EstimatedHours.HasValue &&
                    dto.ActualHours.HasValue &&
                    dto.ActualHours.Value >
                    dto.EstimatedHours.Value)
                {
                    throw new CustomException(
                        "Actual Hours cannot exceed Estimated Hours.");
                }

                // Due date validation
                if (dto.DueDate.HasValue &&
                    dto.TargetDate.HasValue &&
                    dto.DueDate.Value > dto.TargetDate.Value)
                {
                    throw new CustomException(
                        "Due Date cannot be after Target Date.");
                }

                var milestone =
                    (await _unitOfWork
                        .Repository<ProjectMilestone>()
                        .FindAsync(x =>
                            x.MilestoneId == dto.MilestoneId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (milestone == null)
                    throw new CustomException(
                        "Project milestone not found.");

                // Duplicate validation
                var duplicateMilestone =
                    (await _unitOfWork
                        .Repository<ProjectMilestone>()
                        .FindAsync(x =>
                            x.MilestoneId != dto.MilestoneId &&
                            x.MilestoneName.ToLower() ==
                            dto.MilestoneName.Trim().ToLower() &&
                            x.Project.ToLower() ==
                            dto.Project.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateMilestone != null)
                    throw new CustomException(
                        "Milestone Name already exists for this Project.");

                // Old values for audit
                var oldValues =
                    JsonConvert.SerializeObject(milestone);

                milestone.MilestoneName =
                    dto.MilestoneName.Trim();

                milestone.Project =
                    dto.Project.Trim();

                milestone.Owner =
                    dto.Owner;

                milestone.DueDate =
                    dto.DueDate;

                milestone.Status =
                    dto.Status.Trim();

                milestone.CompletionPercentage =
                    dto.CompletionPercentage;

                milestone.EstimatedHours =
                    dto.EstimatedHours;

                milestone.ActualHours =
                    dto.ActualHours;

                milestone.Priority =
                    dto.Priority.Trim();

                milestone.TargetDate =
                    dto.TargetDate;

                milestone.Description =
                    dto.Description?.Trim();

                milestone.ModifiedBy =
                    _currentUserService.UserId;

                milestone.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<ProjectMilestone>()
                    .Update(milestone);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "ProjectMilestone",
                    "UPDATE",
                    milestone.MilestoneId,
                    oldValues,
                    JsonConvert.SerializeObject(milestone),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Project milestone updated successfully.",
                    Data = milestone.MilestoneName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating project milestone.");

                throw;
            }
        }

        // DELETE - Soft Delete
        public async Task<ApiResponse<string>> DeleteProjectMilestone(
            int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Milestone Id is required.");

                var milestone =
                    (await _unitOfWork
                        .Repository<ProjectMilestone>()
                        .FindAsync(x =>
                            x.MilestoneId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (milestone == null)
                    throw new CustomException(
                        "Project milestone not found.");

                var oldValues =
                    JsonConvert.SerializeObject(milestone);

                milestone.IsDeleted = true;

                milestone.ModifiedBy =
                    _currentUserService.UserId;

                milestone.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<ProjectMilestone>()
                    .Update(milestone);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "ProjectMilestone",
                    "DELETE",
                    milestone.MilestoneId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Project milestone deleted successfully.",
                    Data = milestone.MilestoneName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting project milestone.");

                throw;
            }
        }

        // GET ALL
        public async Task<ApiResponse<List<ProjectMilestoneDto>>>
            GetProjectMilestones()
        {
            try
            {
                var milestones =
                    await _unitOfWork
                        .Repository<ProjectMilestone>()
                        .FindAsync(x =>
                            !x.IsDeleted &&
                            x.CreatedBy == _currentUserService.UserId);

                var result = milestones
                    .Select(x => new ProjectMilestoneDto
                    {
                        MilestoneId = x.MilestoneId,
                        MilestoneName = x.MilestoneName,
                        Project = x.Project,
                        Owner = x.Owner,
                        DueDate = x.DueDate,
                        Status = x.Status,
                        CompletionPercentage =
                            x.CompletionPercentage,
                        EstimatedHours =
                            x.EstimatedHours,
                        ActualHours =
                            x.ActualHours,
                        Priority = x.Priority,
                        TargetDate = x.TargetDate,
                        Description = x.Description
                    })
                    .ToList();

                return new ApiResponse<List<ProjectMilestoneDto>>
                {
                    Success = true,
                    Message =
                        "Project milestones retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting project milestones.");

                throw;
            }
        }

        // GET BY ID
        public async Task<ApiResponse<ProjectMilestoneDto>>
            GetProjectMilestoneById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Milestone Id is required.");

                var milestone =
                    (await _unitOfWork
                        .Repository<ProjectMilestone>()
                        .FindAsync(x =>
                            x.MilestoneId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (milestone == null)
                    throw new CustomException(
                        "Project milestone not found.");

                var result = new ProjectMilestoneDto
                {
                    MilestoneId = milestone.MilestoneId,
                    MilestoneName = milestone.MilestoneName,
                    Project = milestone.Project,
                    Owner = milestone.Owner,
                    DueDate = milestone.DueDate,
                    Status = milestone.Status,
                    CompletionPercentage =
                        milestone.CompletionPercentage,
                    EstimatedHours =
                        milestone.EstimatedHours,
                    ActualHours =
                        milestone.ActualHours,
                    Priority = milestone.Priority,
                    TargetDate = milestone.TargetDate,
                    Description = milestone.Description
                };

                return new ApiResponse<ProjectMilestoneDto>
                {
                    Success = true,
                    Message =
                        "Project milestone retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting project milestone by id.");

                throw;
            }
        }
        // CREATE
        public async Task<ApiResponse<string>> CreateProjectTask(
            ProjectTaskDto dto)
        {
            try
            {
                // Required validations
                if (string.IsNullOrWhiteSpace(dto.TaskName))
                    throw new CustomException(
                        "Task Name is required.");

                if (dto.ProjectId <= 0)
                    throw new CustomException(
                        "Valid Project Id is required.");

                if (string.IsNullOrWhiteSpace(dto.AssignedTo))
                    throw new CustomException(
                        "Assigned To is required.");

                if (dto.PriorityId <= 0)
                    throw new CustomException(
                        "Valid Priority Id is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                // Completion percentage
                if (dto.CompletionPercentage < 0 ||
                    dto.CompletionPercentage > 100)
                {
                    throw new CustomException(
                        "Completion Percentage must be between 0 and 100.");
                }

                // Hours validation
                if (dto.EstimatedHours.HasValue &&
                    dto.EstimatedHours.Value < 0)
                {
                    throw new CustomException(
                        "Estimated Hours cannot be negative.");
                }

                if (dto.ActualHours.HasValue &&
                    dto.ActualHours.Value < 0)
                {
                    throw new CustomException(
                        "Actual Hours cannot be negative.");
                }

                if (dto.EstimatedHours.HasValue &&
                    dto.ActualHours.HasValue &&
                    dto.ActualHours.Value >
                    dto.EstimatedHours.Value)
                {
                    throw new CustomException(
                        "Actual Hours cannot exceed Estimated Hours.");
                }

                // Date validation
                if (dto.StartDate.HasValue &&
                    dto.DueDate.HasValue &&
                    dto.StartDate.Value > dto.DueDate.Value)
                {
                    throw new CustomException(
                        "Start Date cannot be after Due Date.");
                }

                // Validate Project
                var project =
                    (await _unitOfWork
                        .Repository<ProjectManagement>()
                        .FindAsync(x =>
                            x.ProjectId == dto.ProjectId))
                    .FirstOrDefault();

                if (project == null)
                    throw new CustomException(
                        "Project not found.");

                // Validate Priority
                var priority =
                    (await _unitOfWork
                        .Repository<Priority>()
                        .FindAsync(x =>
                            x.PriorityId == dto.PriorityId))
                    .FirstOrDefault();

                if (priority == null)
                    throw new CustomException(
                        "Priority not found.");

                // Validate Milestone if provided
                if (dto.MilestoneId.HasValue)
                {
                    var milestone =
                        (await _unitOfWork
                            .Repository<ProjectMilestone>()
                            .FindAsync(x =>
                                x.MilestoneId ==
                                dto.MilestoneId.Value &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (milestone == null)
                        throw new CustomException(
                            "Milestone not found.");
                }

                // Duplicate task validation
                var duplicateTask =
                    (await _unitOfWork
                        .Repository<ProjectTask>()
                        .FindAsync(x =>
                            x.TaskName.ToLower() ==
                            dto.TaskName.Trim().ToLower() &&
                            x.ProjectId == dto.ProjectId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateTask != null)
                    throw new CustomException(
                        "Task Name already exists for this Project.");

                var task = new ProjectTask
                {
                    TaskName = dto.TaskName.Trim(),
                    ProjectId = dto.ProjectId,
                    MilestoneId = dto.MilestoneId,
                    AssignedTo = dto.AssignedTo.Trim(),
                    PriorityId = dto.PriorityId,
                    Status = dto.Status.Trim(),
                    StartDate = dto.StartDate,
                    DueDate = dto.DueDate,
                    EstimatedHours = dto.EstimatedHours,
                    ActualHours = dto.ActualHours,
                    CompletionPercentage =
                        dto.CompletionPercentage,
                    Tags = dto.Tags?.Trim(),
                    Description = dto.Description?.Trim(),

                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                await _unitOfWork
                    .Repository<ProjectTask>()
                    .AddAsync(task);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "ProjectTask",
                    "INSERT",
                    task.TaskId,
                    "",
                    JsonConvert.SerializeObject(task),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Project task created successfully.",
                    Data = task.TaskName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating project task.");

                throw;
            }
        }

        // UPDATE
        public async Task<ApiResponse<string>> UpdateProjectTask(
            ProjectTaskDto dto)
        {
            try
            {
                if (dto.TaskId <= 0)
                    throw new CustomException(
                        "Valid Task Id is required.");

                if (string.IsNullOrWhiteSpace(dto.TaskName))
                    throw new CustomException(
                        "Task Name is required.");

                if (dto.ProjectId <= 0)
                    throw new CustomException(
                        "Valid Project Id is required.");

                if (string.IsNullOrWhiteSpace(dto.AssignedTo))
                    throw new CustomException(
                        "Assigned To is required.");

                if (dto.PriorityId <= 0)
                    throw new CustomException(
                        "Valid Priority Id is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                if (dto.CompletionPercentage < 0 ||
                    dto.CompletionPercentage > 100)
                {
                    throw new CustomException(
                        "Completion Percentage must be between 0 and 100.");
                }

                if (dto.EstimatedHours.HasValue &&
                    dto.EstimatedHours.Value < 0)
                {
                    throw new CustomException(
                        "Estimated Hours cannot be negative.");
                }

                if (dto.ActualHours.HasValue &&
                    dto.ActualHours.Value < 0)
                {
                    throw new CustomException(
                        "Actual Hours cannot be negative.");
                }

                if (dto.EstimatedHours.HasValue &&
                    dto.ActualHours.HasValue &&
                    dto.ActualHours.Value >
                    dto.EstimatedHours.Value)
                {
                    throw new CustomException(
                        "Actual Hours cannot exceed Estimated Hours.");
                }

                if (dto.StartDate.HasValue &&
                    dto.DueDate.HasValue &&
                    dto.StartDate.Value > dto.DueDate.Value)
                {
                    throw new CustomException(
                        "Start Date cannot be after Due Date.");
                }

                var task =
                    (await _unitOfWork
                        .Repository<ProjectTask>()
                        .FindAsync(x =>
                            x.TaskId == dto.TaskId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (task == null)
                    throw new CustomException(
                        "Project task not found.");

                // Validate Project
                var project =
                    (await _unitOfWork
                        .Repository<ProjectManagement>()
                        .FindAsync(x =>
                            x.ProjectId == dto.ProjectId))
                    .FirstOrDefault();

                if (project == null)
                    throw new CustomException(
                        "Project not found.");

                // Validate Priority
                var priority =
                    (await _unitOfWork
                        .Repository<Priority>()
                        .FindAsync(x =>
                            x.PriorityId == dto.PriorityId))
                    .FirstOrDefault();

                if (priority == null)
                    throw new CustomException(
                        "Priority not found.");

                // Validate Milestone
                if (dto.MilestoneId.HasValue)
                {
                    var milestone =
                        (await _unitOfWork
                            .Repository<ProjectMilestone>()
                            .FindAsync(x =>
                                x.MilestoneId ==
                                dto.MilestoneId.Value &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (milestone == null)
                        throw new CustomException(
                            "Milestone not found.");
                }

                // Duplicate task validation
                var duplicateTask =
                    (await _unitOfWork
                        .Repository<ProjectTask>()
                        .FindAsync(x =>
                            x.TaskId != dto.TaskId &&
                            x.TaskName.ToLower() ==
                            dto.TaskName.Trim().ToLower() &&
                            x.ProjectId == dto.ProjectId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateTask != null)
                    throw new CustomException(
                        "Task Name already exists for this Project.");

                var oldValues =
                    JsonConvert.SerializeObject(task);

                task.TaskName =
                    dto.TaskName.Trim();

                task.ProjectId =
                    dto.ProjectId;

                task.MilestoneId =
                    dto.MilestoneId;

                task.AssignedTo =
                    dto.AssignedTo.Trim();

                task.PriorityId =
                    dto.PriorityId;

                task.Status =
                    dto.Status.Trim();

                task.StartDate =
                    dto.StartDate;

                task.DueDate =
                    dto.DueDate;

                task.EstimatedHours =
                    dto.EstimatedHours;

                task.ActualHours =
                    dto.ActualHours;

                task.CompletionPercentage =
                    dto.CompletionPercentage;

                task.Tags =
                    dto.Tags?.Trim();

                task.Description =
                    dto.Description?.Trim();

                task.ModifiedBy =
                    _currentUserService.UserId;

                task.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<ProjectTask>()
                    .Update(task);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "ProjectTask",
                    "UPDATE",
                    task.TaskId,
                    oldValues,
                    JsonConvert.SerializeObject(task),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Project task updated successfully.",
                    Data = task.TaskName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating project task.");

                throw;
            }
        }

        // DELETE - Soft Delete
        public async Task<ApiResponse<string>> DeleteProjectTask(
            int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Task Id is required.");

                var task =
                    (await _unitOfWork
                        .Repository<ProjectTask>()
                        .FindAsync(x =>
                            x.TaskId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (task == null)
                    throw new CustomException(
                        "Project task not found.");

                var oldValues =
                    JsonConvert.SerializeObject(task);

                task.IsDeleted = true;

                task.ModifiedBy =
                    _currentUserService.UserId;

                task.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<ProjectTask>()
                    .Update(task);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "ProjectTask",
                    "DELETE",
                    task.TaskId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Project task deleted successfully.",
                    Data = task.TaskName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting project task.");

                throw;
            }
        }

        // GET ALL
        public async Task<ApiResponse<List<ProjectTaskDto>>>
            GetProjectTasks()
        {
            try
            {
                var tasks =
                    await _unitOfWork
                        .Repository<ProjectTask>()
                        .FindAsync(x =>
                            !x.IsDeleted &&
                            x.CreatedBy == _currentUserService.UserId);

                var result = tasks
                    .Select(x => new ProjectTaskDto
                    {
                        TaskId = x.TaskId,
                        TaskName = x.TaskName,
                        ProjectId = x.ProjectId,
                        MilestoneId = x.MilestoneId,
                        AssignedTo = x.AssignedTo,
                        PriorityId = x.PriorityId,
                        Status = x.Status,
                        StartDate = x.StartDate,
                        DueDate = x.DueDate,
                        EstimatedHours =
                            x.EstimatedHours,
                        ActualHours =
                            x.ActualHours,
                        CompletionPercentage =
                            x.CompletionPercentage,
                        Tags = x.Tags,
                        Description =
                            x.Description
                    })
                    .ToList();

                return new ApiResponse<List<ProjectTaskDto>>
                {
                    Success = true,
                    Message =
                        "Project tasks retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting project tasks.");

                throw;
            }
        }

        // GET BY ID
        public async Task<ApiResponse<ProjectTaskDto>>
            GetProjectTaskById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Task Id is required.");

                var task =
                    (await _unitOfWork
                        .Repository<ProjectTask>()
                        .FindAsync(x =>
                            x.TaskId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (task == null)
                    throw new CustomException(
                        "Project task not found.");

                var result = new ProjectTaskDto
                {
                    TaskId = task.TaskId,
                    TaskName = task.TaskName,
                    ProjectId = task.ProjectId,
                    MilestoneId = task.MilestoneId,
                    AssignedTo = task.AssignedTo,
                    PriorityId = task.PriorityId,
                    Status = task.Status,
                    StartDate = task.StartDate,
                    DueDate = task.DueDate,
                    EstimatedHours =
                        task.EstimatedHours,
                    ActualHours =
                        task.ActualHours,
                    CompletionPercentage =
                        task.CompletionPercentage,
                    Tags = task.Tags,
                    Description =
                        task.Description
                };

                return new ApiResponse<ProjectTaskDto>
                {
                    Success = true,
                    Message =
                        "Project task retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting project task by id.");

                throw;
            }
        }
        // CREATE
        public async Task<ApiResponse<string>> CreateProjectDocument(
            ProjectDocumentDto dto)
        {
            try
            {
                // Required validations
                if (string.IsNullOrWhiteSpace(dto.DocumentName))
                    throw new CustomException(
                        "Document Name is required.");

                if (dto.ProjectId <= 0)
                    throw new CustomException(
                        "Valid Project Id is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                // File size validation
                if (dto.FileSizeKb.HasValue &&
                    dto.FileSizeKb.Value < 0)
                {
                    throw new CustomException(
                        "File Size cannot be negative.");
                }

                // Upload date validation
                if (dto.UploadDate > DateTime.Now)
                {
                    throw new CustomException(
                        "Upload Date cannot be in the future.");
                }

                // Validate Project
                var project =
                    (await _unitOfWork
                        .Repository<ProjectManagement>()
                        .FindAsync(x =>
                            x.ProjectId == dto.ProjectId))
                    .FirstOrDefault();

                if (project == null)
                    throw new CustomException(
                        "Project not found.");

                // Duplicate document validation
                var duplicateDocument =
                    (await _unitOfWork
                        .Repository<ProjectDocument>()
                        .FindAsync(x =>
                            x.ProjectId == dto.ProjectId &&
                            x.DocumentName.ToLower() ==
                            dto.DocumentName.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateDocument != null)
                    throw new CustomException(
                        "Document Name already exists for this Project.");

                var document = new ProjectDocument
                {
                    DocumentName =
                        dto.DocumentName.Trim(),

                    ProjectId =
                        dto.ProjectId,

                    Category =
                        dto.Category?.Trim(),

                    Version =
                        dto.Version?.Trim(),

                    UploadedBy =
                        dto.UploadedBy,

                    UploadDate =
                        dto.UploadDate == default
                            ? DateTime.Now
                            : dto.UploadDate,

                    UploadFile =
                        dto.UploadFile?.Trim(),

                    FileSizeKb =
                        dto.FileSizeKb,

                    FileType =
                        dto.FileType?.Trim(),

                    Description =
                        dto.Description?.Trim(),

                    Status =
                        dto.Status.Trim(),

                    CreatedBy =
                        _currentUserService.UserId,

                    CreatedDate =
                        DateTime.Now,

                    IsDeleted =
                        false
                };

                await _unitOfWork
                    .Repository<ProjectDocument>()
                    .AddAsync(document);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "ProjectDocument",
                    "INSERT",
                    document.DocumentId,
                    "",
                    JsonConvert.SerializeObject(document),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Project document created successfully.",
                    Data =
                        document.DocumentName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating project document.");

                throw;
            }
        }

        // UPDATE
        public async Task<ApiResponse<string>> UpdateProjectDocument(
            ProjectDocumentDto dto)
        {
            try
            {
                if (dto.DocumentId <= 0)
                    throw new CustomException(
                        "Valid Document Id is required.");

                if (string.IsNullOrWhiteSpace(dto.DocumentName))
                    throw new CustomException(
                        "Document Name is required.");

                if (dto.ProjectId <= 0)
                    throw new CustomException(
                        "Valid Project Id is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                if (dto.FileSizeKb.HasValue &&
                    dto.FileSizeKb.Value < 0)
                {
                    throw new CustomException(
                        "File Size cannot be negative.");
                }

                if (dto.UploadDate > DateTime.Now)
                {
                    throw new CustomException(
                        "Upload Date cannot be in the future.");
                }

                var document =
                    (await _unitOfWork
                        .Repository<ProjectDocument>()
                        .FindAsync(x =>
                            x.DocumentId == dto.DocumentId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (document == null)
                    throw new CustomException(
                        "Project document not found.");

                // Validate Project
                var project =
                    (await _unitOfWork
                        .Repository<ProjectManagement>()
                        .FindAsync(x =>
                            x.ProjectId == dto.ProjectId))
                    .FirstOrDefault();

                if (project == null)
                    throw new CustomException(
                        "Project not found.");

                // Duplicate document validation
                var duplicateDocument =
                    (await _unitOfWork
                        .Repository<ProjectDocument>()
                        .FindAsync(x =>
                            x.DocumentId != dto.DocumentId &&
                            x.ProjectId == dto.ProjectId &&
                            x.DocumentName.ToLower() ==
                            dto.DocumentName.Trim().ToLower() &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateDocument != null)
                    throw new CustomException(
                        "Document Name already exists for this Project.");

                // Old values for audit
                var oldValues =
                    JsonConvert.SerializeObject(document);

                document.DocumentName =
                    dto.DocumentName.Trim();

                document.ProjectId =
                    dto.ProjectId;

                document.Category =
                    dto.Category?.Trim();

                document.Version =
                    dto.Version?.Trim();

                document.UploadedBy =
                    dto.UploadedBy;

                document.UploadDate =
                    dto.UploadDate == default
                        ? document.UploadDate
                        : dto.UploadDate;

                document.UploadFile =
                    dto.UploadFile?.Trim();

                document.FileSizeKb =
                    dto.FileSizeKb;

                document.FileType =
                    dto.FileType?.Trim();

                document.Description =
                    dto.Description?.Trim();

                document.Status =
                    dto.Status.Trim();

                document.ModifiedBy =
                    _currentUserService.UserId;

                document.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<ProjectDocument>()
                    .Update(document);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "ProjectDocument",
                    "UPDATE",
                    document.DocumentId,
                    oldValues,
                    JsonConvert.SerializeObject(document),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Project document updated successfully.",
                    Data =
                        document.DocumentName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating project document.");

                throw;
            }
        }

        // DELETE - Soft Delete
        public async Task<ApiResponse<string>> DeleteProjectDocument(
            int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Document Id is required.");

                var document =
                    (await _unitOfWork
                        .Repository<ProjectDocument>()
                        .FindAsync(x =>
                            x.DocumentId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (document == null)
                    throw new CustomException(
                        "Project document not found.");

                var oldValues =
                    JsonConvert.SerializeObject(document);

                document.IsDeleted =
                    true;

                document.ModifiedBy =
                    _currentUserService.UserId;

                document.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<ProjectDocument>()
                    .Update(document);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "ProjectDocument",
                    "DELETE",
                    document.DocumentId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Project document deleted successfully.",
                    Data =
                        document.DocumentName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting project document.");

                throw;
            }
        }

        // GET ALL
        public async Task<ApiResponse<List<ProjectDocumentDto>>>
            GetProjectDocuments()
        {
            try
            {
                var documents =
                    await _unitOfWork
                        .Repository<ProjectDocument>()
                        .FindAsync(x =>
                            !x.IsDeleted &&
                            x.CreatedBy == _currentUserService.UserId);

                var result =
                    documents
                        .Select(x => new ProjectDocumentDto
                        {
                            DocumentId =
                                x.DocumentId,

                            DocumentName =
                                x.DocumentName,

                            ProjectId =
                                x.ProjectId,

                            Category =
                                x.Category,

                            Version =
                                x.Version,

                            UploadedBy =
                                x.UploadedBy,

                            UploadDate =
                                x.UploadDate,

                            UploadFile =
                                x.UploadFile,

                            FileSizeKb =
                                x.FileSizeKb,

                            FileType =
                                x.FileType,

                            Description =
                                x.Description,

                            Status =
                                x.Status
                        })
                        .ToList();

                return new ApiResponse<List<ProjectDocumentDto>>
                {
                    Success = true,
                    Message =
                        "Project documents retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting project documents.");

                throw;
            }
        }

        // GET BY ID
        public async Task<ApiResponse<ProjectDocumentDto>>
            GetProjectDocumentById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Document Id is required.");

                var document =
                    (await _unitOfWork
                        .Repository<ProjectDocument>()
                        .FindAsync(x =>
                            x.DocumentId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (document == null)
                    throw new CustomException(
                        "Project document not found.");

                var result =
                    new ProjectDocumentDto
                    {
                        DocumentId =
                            document.DocumentId,

                        DocumentName =
                            document.DocumentName,

                        ProjectId =
                            document.ProjectId,

                        Category =
                            document.Category,

                        Version =
                            document.Version,

                        UploadedBy =
                            document.UploadedBy,

                        UploadDate =
                            document.UploadDate,

                        UploadFile =
                            document.UploadFile,

                        FileSizeKb =
                            document.FileSizeKb,

                        FileType =
                            document.FileType,

                        Description =
                            document.Description,

                        Status =
                            document.Status
                    };

                return new ApiResponse<ProjectDocumentDto>
                {
                    Success = true,
                    Message =
                        "Project document retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting project document by id.");

                throw;
            }
        }
        // CREATE
        public async Task<ApiResponse<string>> CreateTimesheet(
            TimesheetManagementDto dto)
        {
            try
            {
                // Required validations
                if (dto.EmployeeId <= 0)
                    throw new CustomException(
                        "Valid Employee Id is required.");

                if (dto.ProjectId <= 0)
                    throw new CustomException(
                        "Valid Project Id is required.");

                if (string.IsNullOrWhiteSpace(dto.BillingType))
                    throw new CustomException(
                        "Billing Type is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                // Time validation
                if (dto.StartTime.HasValue &&
                    dto.EndTime.HasValue &&
                    dto.StartTime.Value >= dto.EndTime.Value)
                {
                    throw new CustomException(
                        "Start Time must be earlier than End Time.");
                }

                // Total hours validation
                if (dto.TotalHours < 0)
                    throw new CustomException(
                        "Total Hours cannot be negative.");

                // If start and end times are supplied,
                // validate TotalHours against the time difference.
                if (dto.StartTime.HasValue &&
                    dto.EndTime.HasValue)
                {
                    var timeDifference =
                        dto.EndTime.Value.ToTimeSpan() -
                        dto.StartTime.Value.ToTimeSpan();

                    var calculatedHours =
                        (decimal)timeDifference.TotalHours;

                    if (Math.Abs(
                        calculatedHours - dto.TotalHours) > 0.01m)
                    {
                        throw new CustomException(
                            "Total Hours does not match Start Time and End Time.");
                    }
                }

                // Validate Project
                var project =
                    (await _unitOfWork
                        .Repository<ProjectManagement>()
                        .FindAsync(x =>
                            x.ProjectId == dto.ProjectId))
                    .FirstOrDefault();

                if (project == null)
                    throw new CustomException(
                        "Project not found.");

                // Validate Task if supplied
                if (dto.TaskId.HasValue)
                {
                    var task =
                        (await _unitOfWork
                            .Repository<ProjectTask>()
                            .FindAsync(x =>
                                x.TaskId == dto.TaskId.Value &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (task == null)
                        throw new CustomException(
                            "Task not found.");

                    // Task must belong to selected project
                    if (task.ProjectId != dto.ProjectId)
                        throw new CustomException(
                            "Selected Task does not belong to the selected Project.");
                }

                // Duplicate timesheet validation
                var duplicateTimesheet =
                    (await _unitOfWork
                        .Repository<TimesheetManagement>()
                        .FindAsync(x =>
                            x.EmployeeId == dto.EmployeeId &&
                            x.ProjectId == dto.ProjectId &&
                            x.TaskId == dto.TaskId &&
                            x.TimesheetDate == dto.TimesheetDate &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateTimesheet != null)
                    throw new CustomException(
                        "Timesheet already exists for this Employee, Project, Task and Date.");

                var timesheet = new TimesheetManagement
                {
                    EmployeeId = dto.EmployeeId,
                    ProjectId = dto.ProjectId,
                    TaskId = dto.TaskId,
                    TimesheetDate = dto.TimesheetDate,
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime,
                    TotalHours = dto.TotalHours,
                    BillingType = dto.BillingType.Trim(),
                    Status = dto.Status.Trim(),
                    ApprovedBy = dto.ApprovedBy,
                    WorkDescription =
                        dto.WorkDescription?.Trim(),

                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,

                    IsDeleted = false
                };

                await _unitOfWork
                    .Repository<TimesheetManagement>()
                    .AddAsync(timesheet);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "TimesheetManagement",
                    "INSERT",
                    timesheet.TimesheetId,
                    "",
                    JsonConvert.SerializeObject(timesheet),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Timesheet created successfully.",
                    Data = timesheet.TimesheetId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating timesheet.");

                throw;
            }
        }

        // UPDATE
        public async Task<ApiResponse<string>> UpdateTimesheet(
            TimesheetManagementDto dto)
        {
            try
            {
                if (dto.TimesheetId <= 0)
                    throw new CustomException(
                        "Valid Timesheet Id is required.");

                if (dto.EmployeeId <= 0)
                    throw new CustomException(
                        "Valid Employee Id is required.");

                if (dto.ProjectId <= 0)
                    throw new CustomException(
                        "Valid Project Id is required.");

                if (string.IsNullOrWhiteSpace(dto.BillingType))
                    throw new CustomException(
                        "Billing Type is required.");

                if (string.IsNullOrWhiteSpace(dto.Status))
                    throw new CustomException(
                        "Status is required.");

                // Time validation
                if (dto.StartTime.HasValue &&
                    dto.EndTime.HasValue &&
                    dto.StartTime.Value >= dto.EndTime.Value)
                {
                    throw new CustomException(
                        "Start Time must be earlier than End Time.");
                }

                // Total hours validation
                if (dto.TotalHours < 0)
                    throw new CustomException(
                        "Total Hours cannot be negative.");

                if (dto.StartTime.HasValue &&
                    dto.EndTime.HasValue)
                {
                    var timeDifference =
                        dto.EndTime.Value.ToTimeSpan() -
                        dto.StartTime.Value.ToTimeSpan();

                    var calculatedHours =
                        (decimal)timeDifference.TotalHours;

                    if (Math.Abs(
                        calculatedHours - dto.TotalHours) > 0.01m)
                    {
                        throw new CustomException(
                            "Total Hours does not match Start Time and End Time.");
                    }
                }

                var timesheet =
                    (await _unitOfWork
                        .Repository<TimesheetManagement>()
                        .FindAsync(x =>
                            x.TimesheetId == dto.TimesheetId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (timesheet == null)
                    throw new CustomException(
                        "Timesheet not found.");

                // Validate Project
                var project =
                    (await _unitOfWork
                        .Repository<ProjectManagement>()
                        .FindAsync(x =>
                            x.ProjectId == dto.ProjectId))
                    .FirstOrDefault();

                if (project == null)
                    throw new CustomException(
                        "Project not found.");

                // Validate Task
                if (dto.TaskId.HasValue)
                {
                    var task =
                        (await _unitOfWork
                            .Repository<ProjectTask>()
                            .FindAsync(x =>
                                x.TaskId == dto.TaskId.Value &&
                                !x.IsDeleted))
                        .FirstOrDefault();

                    if (task == null)
                        throw new CustomException(
                            "Task not found.");

                    if (task.ProjectId != dto.ProjectId)
                        throw new CustomException(
                            "Selected Task does not belong to the selected Project.");
                }

                // Duplicate validation
                var duplicateTimesheet =
                    (await _unitOfWork
                        .Repository<TimesheetManagement>()
                        .FindAsync(x =>
                            x.TimesheetId != dto.TimesheetId &&
                            x.EmployeeId == dto.EmployeeId &&
                            x.ProjectId == dto.ProjectId &&
                            x.TaskId == dto.TaskId &&
                            x.TimesheetDate == dto.TimesheetDate &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (duplicateTimesheet != null)
                    throw new CustomException(
                        "Timesheet already exists for this Employee, Project, Task and Date.");

                var oldValues =
                    JsonConvert.SerializeObject(timesheet);

                timesheet.EmployeeId =
                    dto.EmployeeId;

                timesheet.ProjectId =
                    dto.ProjectId;

                timesheet.TaskId =
                    dto.TaskId;

                timesheet.TimesheetDate =
                    dto.TimesheetDate;

                timesheet.StartTime =
                    dto.StartTime;

                timesheet.EndTime =
                    dto.EndTime;

                timesheet.TotalHours =
                    dto.TotalHours;

                timesheet.BillingType =
                    dto.BillingType.Trim();

                timesheet.Status =
                    dto.Status.Trim();

                timesheet.ApprovedBy =
                    dto.ApprovedBy;

                timesheet.WorkDescription =
                    dto.WorkDescription?.Trim();

                timesheet.ModifiedBy =
                    _currentUserService.UserId;

                timesheet.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<TimesheetManagement>()
                    .Update(timesheet);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "TimesheetManagement",
                    "UPDATE",
                    timesheet.TimesheetId,
                    oldValues,
                    JsonConvert.SerializeObject(timesheet),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Timesheet updated successfully.",
                    Data = timesheet.TimesheetId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating timesheet.");

                throw;
            }
        }

        // DELETE - Soft Delete
        public async Task<ApiResponse<string>> DeleteTimesheet(
            int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Timesheet Id is required.");

                var timesheet =
                    (await _unitOfWork
                        .Repository<TimesheetManagement>()
                        .FindAsync(x =>
                            x.TimesheetId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (timesheet == null)
                    throw new CustomException(
                        "Timesheet not found.");

                var oldValues =
                    JsonConvert.SerializeObject(timesheet);

                timesheet.IsDeleted = true;

                timesheet.ModifiedBy =
                    _currentUserService.UserId;

                timesheet.ModifiedAt =
                    DateTime.Now;

                _unitOfWork
                    .Repository<TimesheetManagement>()
                    .Update(timesheet);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "TimesheetManagement",
                    "DELETE",
                    timesheet.TimesheetId,
                    oldValues,
                    "",
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Timesheet deleted successfully.",
                    Data = timesheet.TimesheetId.ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting timesheet.");

                throw;
            }
        }

        // GET ALL
        public async Task<ApiResponse<List<TimesheetManagementDto>>>
            GetTimesheets()
        {
            try
            {
                var timesheets =
                    await _unitOfWork
                        .Repository<TimesheetManagement>()
                        .FindAsync(x =>
                            !x.IsDeleted &&
                            x.CreatedBy == _currentUserService.UserId);

                var result = timesheets
                    .Select(x => new TimesheetManagementDto
                    {
                        TimesheetId =
                            x.TimesheetId,

                        EmployeeId =
                            x.EmployeeId,

                        ProjectId =
                            x.ProjectId,

                        TaskId =
                            x.TaskId,

                        TimesheetDate =
                            x.TimesheetDate,

                        StartTime =
                            x.StartTime,

                        EndTime =
                            x.EndTime,

                        TotalHours =
                            x.TotalHours,

                        BillingType =
                            x.BillingType,

                        Status =
                            x.Status,

                        ApprovedBy =
                            x.ApprovedBy,

                        WorkDescription =
                            x.WorkDescription
                    })
                    .ToList();

                return new ApiResponse<List<TimesheetManagementDto>>
                {
                    Success = true,
                    Message =
                        "Timesheets retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting timesheets.");

                throw;
            }
        }

        // GET BY ID
        public async Task<ApiResponse<TimesheetManagementDto>>
            GetTimesheetById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(
                        "Valid Timesheet Id is required.");

                var timesheet =
                    (await _unitOfWork
                        .Repository<TimesheetManagement>()
                        .FindAsync(x =>
                            x.TimesheetId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (timesheet == null)
                    throw new CustomException(
                        "Timesheet not found.");

                var result =
                    new TimesheetManagementDto
                    {
                        TimesheetId =
                            timesheet.TimesheetId,

                        EmployeeId =
                            timesheet.EmployeeId,

                        ProjectId =
                            timesheet.ProjectId,

                        TaskId =
                            timesheet.TaskId,

                        TimesheetDate =
                            timesheet.TimesheetDate,

                        StartTime =
                            timesheet.StartTime,

                        EndTime =
                            timesheet.EndTime,

                        TotalHours =
                            timesheet.TotalHours,

                        BillingType =
                            timesheet.BillingType,

                        Status =
                            timesheet.Status,

                        ApprovedBy =
                            timesheet.ApprovedBy,

                        WorkDescription =
                            timesheet.WorkDescription
                    };

                return new ApiResponse<TimesheetManagementDto>
                {
                    Success = true,
                    Message =
                        "Timesheet retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while getting timesheet by id.");

                throw;
            }
        }
    }
}


    

