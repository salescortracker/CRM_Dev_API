using Business_Layer.DTOs.MasterDTO_s;
using Business_Layer.DTOs.SuperAdmin;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.MasterIInterface;
using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Shared.CommonModels;
using Shared.Constants;
using Shared.Exceptions;

namespace BusinessLayer.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IAuditService _auditService;

        private readonly ILogger<DepartmentService> _logger;

        private readonly ICurrentUserService _currentUserService;

        public DepartmentService(IUnitOfWork unitOfWork, IAuditService auditService, ILogger<DepartmentService> logger, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        #region Department

        #region CREATE

        public async Task<ApiResponse<string>> CreateDepartment(
            DepartmentDto dto)
        {
            try
            {
                // Validate Department Name
                if (string.IsNullOrWhiteSpace(dto.DepartmentName))
                    throw new CustomException(
                        "Department Name is required.");

                // Validate Department Code
                if (string.IsNullOrWhiteSpace(dto.DepartmentCode))
                    throw new CustomException(
                        "Department Code is required.");

                // Clean values
                string departmentName = dto.DepartmentName.Trim();
                string departmentCode = dto.DepartmentCode.Trim().ToUpper();

                // Duplicate Department Name
                var duplicateName =
                    await _unitOfWork.Repository<Department>()
                        .FindAsync(x =>
                            x.DepartmentName.ToLower() ==
                            departmentName.ToLower());

                if (duplicateName.Any())
                    throw new CustomException(
                        "Department Name already exists.");

                // Duplicate Department Code
                var duplicateCode =
                    await _unitOfWork.Repository<Department>()
                        .FindAsync(x =>
                            x.DepartmentCode.ToLower() ==
                            departmentCode.ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Department Code already exists.");

                // Create Entity
                Department department = new Department
                {
                    DepartmentName = departmentName,
                    DepartmentCode = departmentCode,
                    Description = string.IsNullOrWhiteSpace(dto.Description)
                        ? null
                        : dto.Description.Trim(),

                    Status = dto.Status,

                    CreatedBy = _currentUserService.UserId,
                    CreatedDate = DateTime.Now,

                    UserId = _currentUserService.UserId

                    // CompanyId / RegionId can be assigned
                    // if you are getting them from current user/session.
                };

                await _unitOfWork.Repository<Department>()
                    .AddAsync(department);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "Department",
                    "INSERT",
                    department.DepartmentId,
                    "",
                    JsonConvert.SerializeObject(department),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Department Created Successfully",
                    Data = department.DepartmentName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while creating department");

                throw;
            }
        }

        #endregion
        #region UPDATE

        public async Task<ApiResponse<string>> UpdateDepartment(
            DepartmentDto dto)
        {
            try
            {
                // Validate Department Name
                if (string.IsNullOrWhiteSpace(dto.DepartmentName))
                    throw new CustomException(
                        "Department Name is required.");

                // Validate Department Code
                if (string.IsNullOrWhiteSpace(dto.DepartmentCode))
                    throw new CustomException(
                        "Department Code is required.");

                // Get existing record
                var department =
                    (await _unitOfWork.Repository<Department>()
                        .FindAsync(x =>
                            x.DepartmentId == dto.DepartmentId))
                    .FirstOrDefault();

                if (department == null)
                    throw new CustomException(
                        "Department not found.");

                string departmentName = dto.DepartmentName.Trim();
                string departmentCode = dto.DepartmentCode.Trim().ToUpper();

                // Duplicate Department Name
                var duplicateName =
                    await _unitOfWork.Repository<Department>()
                        .FindAsync(x =>
                            x.DepartmentId != dto.DepartmentId &&
                            x.DepartmentName.ToLower() ==
                            departmentName.ToLower());

                if (duplicateName.Any())
                    throw new CustomException(
                        "Department Name already exists.");

                // Duplicate Department Code
                var duplicateCode =
                    await _unitOfWork.Repository<Department>()
                        .FindAsync(x =>
                            x.DepartmentId != dto.DepartmentId &&
                            x.DepartmentCode.ToLower() ==
                            departmentCode.ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Department Code already exists.");

                // Old Values for Audit
                string oldValues =
                    JsonConvert.SerializeObject(department);

                // Update
                department.DepartmentName = departmentName;
                department.DepartmentCode = departmentCode;

                department.Description =
                    string.IsNullOrWhiteSpace(dto.Description)
                        ? null
                        : dto.Description.Trim();

                department.Status = dto.Status;

                department.UpdatedBy =
                    _currentUserService.UserId;

                department.UpdatedDate =
                    DateTime.Now;

                _unitOfWork.Repository<Department>()
                    .Update(department);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "Department",
                    "UPDATE",
                    department.DepartmentId,
                    oldValues,
                    JsonConvert.SerializeObject(department),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Department Updated Successfully",
                    Data = department.DepartmentName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while updating department");

                throw;
            }
        }

        #endregion
        #region DELETE

        public async Task<ApiResponse<string>> DeleteDepartment(int id)
        {
            try
            {
                // Get existing department
                var department =
                    (await _unitOfWork.Repository<Department>()
                        .FindAsync(x =>
                            x.DepartmentId == id))
                    .FirstOrDefault();

                if (department == null)
                    throw new CustomException(
                        "Department not found.");

                // Old Values for Audit
                string oldValues =
                    JsonConvert.SerializeObject(department);

                // Soft Delete / Deactivate
                department.Status = false;

                department.UpdatedBy =
                    _currentUserService.UserId;

                department.UpdatedDate =
                    DateTime.Now;

                _unitOfWork.Repository<Department>()
                    .Update(department);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "Department",
                    "DELETE",
                    department.DepartmentId,
                    oldValues,
                    JsonConvert.SerializeObject(department),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Department Deleted Successfully",
                    Data = department.DepartmentName
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while deleting department");

                throw;
            }
        }

        #endregion
        #region GET ALL

        public async Task<ApiResponse<List<DepartmentDto>>>
            GetDepartments()
        {
            try
            {
                var departments =
                    (await _unitOfWork.Repository<Department>()
                        .FindAsync(x =>
                            x.Status &&
                            x.CreatedBy == _currentUserService.UserId))
                    .OrderByDescending(x => x.DepartmentId)
                    .ToList();

                var result = departments.Select(x =>
                    new DepartmentDto
                    {
                        DepartmentId = x.DepartmentId,

                        DepartmentName =
                            x.DepartmentName,

                        DepartmentCode =
                            x.DepartmentCode,

                        Description =
                            x.Description ?? string.Empty,

                        Status =
                            x.Status

                    }).ToList();

                return new ApiResponse<List<DepartmentDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while getting departments");

                throw;
            }
        }

        #endregion
        #region GET BY ID

        public async Task<ApiResponse<DepartmentDto>>
            GetDepartmentById(int id)
        {
            try
            {
                var department =
                    (await _unitOfWork.Repository<Department>()
                        .FindAsync(x =>
                            x.DepartmentId == id))
                    .FirstOrDefault();

                if (department == null)
                    throw new CustomException(
                        "Department not found.");

                var result = new DepartmentDto
                {
                    DepartmentId =
                        department.DepartmentId,

                    DepartmentName =
                        department.DepartmentName,

                    DepartmentCode =
                        department.DepartmentCode,

                    Description =
                        department.Description ?? string.Empty,

                    Status =
                        department.Status
                };

                return new ApiResponse<DepartmentDto>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Error while getting department by id");

                throw;
            }
        }

        #endregion

        #endregion
        #region Designation

        #region CREATE

        public async Task<ApiResponse<string>> CreateDesignation(
    DesignationDto dto)
        {
            try
            {
                // Validate Designation Name
                if (string.IsNullOrWhiteSpace(dto.DesignationName))
                    throw new CustomException(
                        "Designation Name is required.");

                // Validate Designation Code
                if (string.IsNullOrWhiteSpace(dto.DesignationCode))
                    throw new CustomException(
                        "Designation Code is required.");

                // Validate Department
                if (!dto.DepartmentId.HasValue || dto.DepartmentId <= 0)
                    throw new CustomException(
                        "Department is required.");

                // Clean values
                string designationName =
                    dto.DesignationName.Trim();

                string designationCode =
                    dto.DesignationCode.Trim().ToUpper();

                // Check Department exists
                var department =
                    (await _unitOfWork.Repository<Department>()
                        .FindAsync(x =>
                            x.DepartmentId == dto.DepartmentId.Value))
                        .FirstOrDefault();

                if (department == null)
                    throw new CustomException(
                        "Selected Department not found.");

                // Duplicate Designation Name
                var duplicateName =
                    await _unitOfWork.Repository<Designation>()
                        .FindAsync(x =>
                            x.DesignationName.ToLower() ==
                            designationName.ToLower());

                if (duplicateName.Any())
                    throw new CustomException(
                        "Designation Name already exists.");

                // Duplicate Designation Code
                var duplicateCode =
                    await _unitOfWork.Repository<Designation>()
                        .FindAsync(x =>
                            x.DesignationCode.ToLower() ==
                            designationCode.ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Designation Code already exists.");

                // Create Entity
                Designation designation = new Designation
                {
                    DesignationName = designationName,

                    DesignationCode = designationCode,

                    Description =
                        string.IsNullOrWhiteSpace(dto.Description)
                            ? null
                            : dto.Description.Trim(),

                    DepartmentId = dto.DepartmentId,

                    Status = dto.Status,

                    CompanyId = dto.CompanyId,

                    RegionId = dto.RegionId,

                    CreatedBy =
                        _currentUserService.UserId,

                    CreatedDate = DateTime.Now,

                    UserId =
                        _currentUserService.UserId
                };

                await _unitOfWork.Repository<Designation>()
                    .AddAsync(designation);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "Designation",
                    "INSERT",
                    designation.DesignationId,
                    "",
                    JsonConvert.SerializeObject(designation, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Designation Created Successfully",
                    Data = designation.DesignationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating designation");

                throw;
            }
        }

        #endregion


        #region UPDATE

        public async Task<ApiResponse<string>> UpdateDesignation(
            DesignationDto dto)
        {
            try
            {
                // Validate Designation Name
                if (string.IsNullOrWhiteSpace(dto.DesignationName))
                    throw new CustomException(
                        "Designation Name is required.");

                // Validate Designation Code
                if (string.IsNullOrWhiteSpace(dto.DesignationCode))
                    throw new CustomException(
                        "Designation Code is required.");

                // Validate Department
                if (!dto.DepartmentId.HasValue || dto.DepartmentId <= 0)
                    throw new CustomException(
                        "Department is required.");

                // Get existing designation
                var designation =
                    (await _unitOfWork.Repository<Designation>()
                        .FindAsync(x =>
                            x.DesignationId == dto.DesignationId))
                        .FirstOrDefault();

                if (designation == null)
                    throw new CustomException(
                        "Designation not found.");

                // Clean values
                string designationName =
                    dto.DesignationName.Trim();

                string designationCode =
                    dto.DesignationCode.Trim().ToUpper();

                // Check Department exists
                var department =
                    (await _unitOfWork.Repository<Department>()
                        .FindAsync(x =>
                            x.DepartmentId == dto.DepartmentId.Value))
                        .FirstOrDefault();

                if (department == null)
                    throw new CustomException(
                        "Selected Department not found.");

                // Duplicate Designation Name
                var duplicateName =
                    await _unitOfWork.Repository<Designation>()
                        .FindAsync(x =>
                            x.DesignationId != dto.DesignationId &&
                            x.DesignationName.ToLower() ==
                            designationName.ToLower());

                if (duplicateName.Any())
                    throw new CustomException(
                        "Designation Name already exists.");

                // Duplicate Designation Code
                var duplicateCode =
                    await _unitOfWork.Repository<Designation>()
                        .FindAsync(x =>
                            x.DesignationId != dto.DesignationId &&
                            x.DesignationCode.ToLower() ==
                            designationCode.ToLower());

                if (duplicateCode.Any())
                    throw new CustomException(
                        "Designation Code already exists.");

                // Old values for audit
                string oldValues =
                    JsonConvert.SerializeObject(designation, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                // Update
                designation.DesignationName =
                    designationName;

                designation.DesignationCode =
                    designationCode;

                designation.Description =
                    string.IsNullOrWhiteSpace(dto.Description)
                        ? null
                        : dto.Description.Trim();

                designation.DepartmentId =
                    dto.DepartmentId;

                designation.Status =
                    dto.Status;

                designation.CompanyId =
                    dto.CompanyId;

                designation.RegionId =
                    dto.RegionId;

                designation.UpdatedBy =
                    _currentUserService.UserId;

                designation.UpdatedDate =
                    DateTime.Now;

                _unitOfWork.Repository<Designation>()
                    .Update(designation);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "Designation",
                    "UPDATE",
                    designation.DesignationId,
                    oldValues,
                    JsonConvert.SerializeObject(designation, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Designation Updated Successfully",
                    Data = designation.DesignationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating designation");

                throw;
            }
        }

        #endregion


        #region DELETE

        public async Task<ApiResponse<string>> DeleteDesignation(
            int id)
        {
            try
            {
                // Get existing designation
                var designation =
                    (await _unitOfWork.Repository<Designation>()
                        .FindAsync(x =>
                            x.DesignationId == id))
                        .FirstOrDefault();

                if (designation == null)
                    throw new CustomException(
                        "Designation not found.");

                // Old values for audit
                string oldValues =
                    JsonConvert.SerializeObject(designation, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                // Soft Delete / Deactivate
                designation.Status = false;

                designation.UpdatedBy =
                    _currentUserService.UserId;

                designation.UpdatedDate =
                    DateTime.Now;

                _unitOfWork.Repository<Designation>()
                    .Update(designation);

                await _unitOfWork.CompleteAsync();

                // Audit
                await _auditService.LogAsync(
                    "Designation",
                    "DELETE",
                    designation.DesignationId,
                    oldValues,
                    JsonConvert.SerializeObject(designation, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }),
                    _currentUserService.UserId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Designation Deleted Successfully",
                    Data = designation.DesignationName
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting designation");

                throw;
            }
        }

        #endregion


        #region GET ALL

        public async Task<ApiResponse<List<DesignationDto>>>
            GetDesignations()
        {
            try
            {
                // Get all designations
                var designations =
                    (await _unitOfWork.Repository<Designation>()
                        .FindAsync(x =>
                            x.CreatedBy == _currentUserService.UserId))
                    .OrderByDescending(x =>
                        x.DesignationId)
                    .ToList();

                // Get all departments
                var departments =
                    (await _unitOfWork.Repository<Department>()
                        .FindAsync(x => true))
                    .ToList();

                // Map result
                var result = designations.Select(x =>
                {
                    var department =
                        departments.FirstOrDefault(d =>
                            d.DepartmentId == x.DepartmentId);

                    return new DesignationDto
                    {
                        DesignationId =
                            x.DesignationId,

                        CompanyId =
                            x.CompanyId,

                        RegionId =
                            x.RegionId,

                        DepartmentId =
                            x.DepartmentId,

                        DepartmentName =
                            department?.DepartmentName,

                        DesignationName =
                            x.DesignationName,

                        DesignationCode =
                            x.DesignationCode,

                        Description =
                            x.Description ?? string.Empty,

                        Status =
                            x.Status
                    };
                }).ToList();

                return new ApiResponse<List<DesignationDto>>
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
                    "Error while getting designations");

                throw;
            }
        }

        #endregion


        #region GET BY ID

        public async Task<ApiResponse<DesignationDto>>
            GetDesignationById(int id)
        {
            try
            {
                // Get designation
                var designation =
                    (await _unitOfWork.Repository<Designation>()
                        .FindAsync(x =>
                            x.DesignationId == id))
                    .FirstOrDefault();

                if (designation == null)
                    throw new CustomException(
                        "Designation not found.");

                // Get department
                var department =
                    (await _unitOfWork.Repository<Department>()
                        .FindAsync(x =>
                            x.DepartmentId ==
                            designation.DepartmentId))
                    .FirstOrDefault();

                var result = new DesignationDto
                {
                    DesignationId =
                        designation.DesignationId,

                    CompanyId =
                        designation.CompanyId,

                    RegionId =
                        designation.RegionId,

                    DepartmentId =
                        designation.DepartmentId,

                    DepartmentName =
                        department?.DepartmentName,

                    DesignationName =
                        designation.DesignationName,

                    DesignationCode =
                        designation.DesignationCode,

                    Description =
                        designation.Description ?? string.Empty,

                    Status =
                        designation.Status
                };

                return new ApiResponse<DesignationDto>
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
                    "Error while getting designation by id");

                throw;
            }
        }

        #endregion
        #endregion
    }
}