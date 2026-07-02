using Business_Layer.DTOs.MasterDTO_s;
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

        #region Department CRUD 

        #region CREATE

        public async Task<ApiResponse<string>>
            CreateDepartment(
            DepartmentCreateDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(
                    dto.DepartmentName))
                {
                    throw new CustomException(AppConstants.DepartmentNameRequired);
                }

                if (string.IsNullOrWhiteSpace(
                    dto.DepartmentCode))
                {
                    throw new CustomException(AppConstants.DepartmentCodeRequired);
                }

                var existingDepartment = await _unitOfWork.Repository<Department>()
                    .FindAsync(x =>
                        x.DepartmentCode ==
                        dto.DepartmentCode);

                if (existingDepartment.Any())
                {
                    throw new CustomException(AppConstants.DepartmentCodeExists);
                }

                Department department = new Department();

                department.DepartmentName = dto.DepartmentName;

                department.DepartmentCode = dto.DepartmentCode;

                department.Description = dto.Description;

                department.CompanyId = dto.CompanyId;

                department.RegionId = dto.RegionId;

                department.UserId = _currentUserService.UserId;


                department.CreatedDate = DateTime.Now;

                department.Status = true;


                await _unitOfWork.Repository<Department>().AddAsync(department);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync(
                    "Departments",
                    "INSERT",
                    department.DepartmentId,
                    "",
                    JsonConvert.SerializeObject(
                        department),
                    _currentUserService.UserId);

                Log.Information(
                    "Department Created : {DepartmentCode}",
                    dto.DepartmentCode);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = AppConstants.RecordSaved, Data = department.DepartmentCode
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,AppConstants.ExceptionWhileCreatingDepartment);

                throw;
            }
        }

        #endregion

        #region UPDATE

        public async Task<ApiResponse<string>>
            UpdateDepartment(
            DepartmentUpdateDto dto)
        {
            try
            {
                var department =
                (
                await _unitOfWork
                .Repository<Department>()
                .FindAsync(x =>
                    x.DepartmentId == dto.DepartmentId &&
                    x.UserId == _currentUserService.UserId)
                ).FirstOrDefault();

                if (department == null)
                {
                    throw new CustomException(AppConstants.DepartmentNotFound);
                }

                string oldValues = JsonConvert.SerializeObject(department);

                department.DepartmentName = dto.DepartmentName;

                department.DepartmentCode = dto.DepartmentCode;

                department.Description = dto.Description;

                department.Status = dto.Status;

                department.UserId = _currentUserService.UserId;

                department.UpdatedDate = DateTime.Now;

                _unitOfWork.Repository<Department>().Update(department);

                await _unitOfWork.CompleteAsync();

                string newValues = JsonConvert.SerializeObject(department);

                await _auditService.LogAsync("Departments","UPDATE",
                    department.DepartmentId,
                    oldValues,
                    newValues,
                    _currentUserService.UserId);

                Log.Information(
                    "Department Updated : {DepartmentId}",
                    department.DepartmentId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = AppConstants.RecordUpdated,
                    Data = department.DepartmentCode
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

        public async Task<ApiResponse<string>>
            DeleteDepartment(
            int departmentId)
        {
            try
            {
                var department =(await _unitOfWork.Repository<Department>().FindAsync(x =>
                                       x.DepartmentId == departmentId &&
                                       x.UserId == _currentUserService.UserId)).FirstOrDefault();

                if (department == null)
                {
                    throw new CustomException(AppConstants.DepartmentNotFound);
                }

                string oldValues = JsonConvert.SerializeObject(department);

                // Soft Delete

                department.UserId = _currentUserService.UserId;

                department.UpdatedDate = DateTime.Now;

                _unitOfWork.Repository<Department>().Update(department);

                await _unitOfWork.CompleteAsync();

                await _auditService.LogAsync("Departments","DELETE",department.DepartmentId, oldValues, "", _currentUserService.UserId);

                Log.Information("Department Deleted : {DepartmentId}", department.DepartmentId);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = AppConstants.RecordDeleted,
                    Data = department.DepartmentCode
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex,AppConstants.ErrorWhileDeleting);

                throw;
            }
        }

        #endregion

        #region GET ALL

        public async Task<ApiResponse<List<DepartmentResponseDto>>> GetDepartments()
        {
            try
            {
                var departments = await _unitOfWork.Repository<Department>().GetAllAsync();

                var result = departments.Where(x => x.UserId == _currentUserService.UserId && x.Status)
                    .Select(x =>
                        new DepartmentResponseDto
                        {
                            DepartmentId = x.DepartmentId,

                            DepartmentName = x.DepartmentName,

                            DepartmentCode = x.DepartmentCode,

                            Description = x.Description,

                            Status = x.Status
                        })
                    .ToList();

                return new ApiResponse<List<DepartmentResponseDto>>
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

        public async Task<ApiResponse<DepartmentResponseDto>> GetDepartmentById( int departmentId)
        {
            try
            {
                var department = (await _unitOfWork.Repository<Department>().FindAsync(x => 
                                  x.DepartmentId == departmentId &&
                                  x.UserId == _currentUserService.UserId)).FirstOrDefault();

                if (department == null)
                {
                    throw new CustomException(AppConstants.DepartmentNotFound);
                }

                DepartmentResponseDto dto = 
                    new DepartmentResponseDto
                    {
                        DepartmentId = department.DepartmentId,

                        DepartmentName = department.DepartmentName,

                        DepartmentCode = department.DepartmentCode,

                        Description = department.Description,

                        Status =  department.Status
                    };

             return new ApiResponse< DepartmentResponseDto>
                    {
                           Success = true,
                           Message = "Success",
                           Data = dto
                    };
            }
            catch (Exception ex)
            {
                Log.Error(ex,AppConstants.NoRecordsFound);

                throw;
            }
        }

        #endregion

        #endregion

    }
}