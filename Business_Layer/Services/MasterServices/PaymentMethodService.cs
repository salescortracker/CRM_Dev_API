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
  public class PaymentMethodService : IPaymentMethodService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public PaymentMethodService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }


    public async Task<ApiResponse<string>> CreatePaymentMethod(PaymentMethodDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.PaymentMethodName))
          throw new CustomException("Payment Method Name is required.");

        var duplicate = await _unitOfWork.Repository<PaymentMethod>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.PaymentMethodName.ToLower() == dto.PaymentMethodName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Payment Method already exists.");

        PaymentMethod paymentMethod = new PaymentMethod
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          PaymentMethodName = dto.PaymentMethodName,
          PaymentMethodCode = dto.PaymentMethodCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<PaymentMethod>()
            .AddAsync(paymentMethod);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "PaymentMethod",
            "INSERT",
            paymentMethod.PaymentMethodId,
            "",
            JsonConvert.SerializeObject(paymentMethod),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Payment Method Created Successfully",
          Data = paymentMethod.PaymentMethodName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating payment method");
        throw;
      }
    }

    public async Task<ApiResponse<string>> UpdatePaymentMethod(PaymentMethodDto dto)
    {
      try
      {
        var paymentMethod = (await _unitOfWork.Repository<PaymentMethod>()
            .FindAsync(x => x.PaymentMethodId == dto.PaymentMethodId))
            .FirstOrDefault();

        if (paymentMethod == null)
          throw new CustomException("Payment Method not found.");

        var duplicate = await _unitOfWork.Repository<PaymentMethod>()
            .FindAsync(x =>
                x.PaymentMethodId != dto.PaymentMethodId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.PaymentMethodName.ToLower() == dto.PaymentMethodName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Payment Method already exists.");

        string oldValues = JsonConvert.SerializeObject(paymentMethod);

        paymentMethod.CompanyId = dto.CompanyId;
        paymentMethod.RegionId = dto.RegionId;
        paymentMethod.PaymentMethodName = dto.PaymentMethodName;
        paymentMethod.PaymentMethodCode = dto.PaymentMethodCode;
        paymentMethod.Description = dto.Description;
        paymentMethod.IsActive = dto.IsActive;
        paymentMethod.ModifiedBy = _currentUserService.UserId;
        paymentMethod.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<PaymentMethod>().Update(paymentMethod);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "PaymentMethod",
            "UPDATE",
            paymentMethod.PaymentMethodId,
            oldValues,
            JsonConvert.SerializeObject(paymentMethod),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Payment Method Updated Successfully",
          Data = paymentMethod.PaymentMethodName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating payment method");
        throw;
      }
    }
    public async Task<ApiResponse<string>> DeletePaymentMethod(int id)
    {
      try
      {
        var paymentMethod = (await _unitOfWork.Repository<PaymentMethod>()
            .FindAsync(x => x.PaymentMethodId == id))
            .FirstOrDefault();

        if (paymentMethod == null)
          throw new CustomException("Payment Method not found.");

        string oldValues = JsonConvert.SerializeObject(paymentMethod);

        paymentMethod.IsDeleted = true;
        paymentMethod.ModifiedBy = _currentUserService.UserId;
        paymentMethod.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<PaymentMethod>().Update(paymentMethod);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "PaymentMethod",
            "DELETE",
            paymentMethod.PaymentMethodId,
            oldValues,
            JsonConvert.SerializeObject(paymentMethod),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Payment Method Deleted Successfully",
          Data = paymentMethod.PaymentMethodName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting payment method");
        throw;
      }
    }

    public async Task<ApiResponse<List<PaymentMethodDto>>> GetPaymentMethods()
    {
      try
      {
        var paymentMethods = await _unitOfWork.Repository<PaymentMethod>()
            .GetAllAsync();

        var result = paymentMethods
            .Where(x => !x.IsDeleted)
            .Select(x => new PaymentMethodDto
            {
              PaymentMethodId = x.PaymentMethodId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              PaymentMethodName = x.PaymentMethodName,
              PaymentMethodCode = x.PaymentMethodCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<PaymentMethodDto>>
        {
          Success = true,
          Message = "Payment Methods Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting payment methods");
        throw;
      }
    }

    public async Task<ApiResponse<PaymentMethodDto>> GetPaymentMethodById(int id)
    {
      try
      {
        var paymentMethod = (await _unitOfWork.Repository<PaymentMethod>()
            .FindAsync(x => x.PaymentMethodId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (paymentMethod == null)
          throw new CustomException("Payment Method not found.");

        var result = new PaymentMethodDto
        {
          PaymentMethodId = paymentMethod.PaymentMethodId,
          CompanyId = paymentMethod.CompanyId,
          RegionId = paymentMethod.RegionId,
          PaymentMethodName = paymentMethod.PaymentMethodName,
          PaymentMethodCode = paymentMethod.PaymentMethodCode,
          Description = paymentMethod.Description,
          IsActive = paymentMethod.IsActive
        };

        return new ApiResponse<PaymentMethodDto>
        {
          Success = true,
          Message = "Payment Method Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting payment method by id");
        throw;
      }
    }
  }
}
