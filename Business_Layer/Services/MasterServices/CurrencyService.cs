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
  public class CurrencyService : ICurrencyService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public CurrencyService(
        IUnitOfWork unitOfWork,
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
      _unitOfWork = unitOfWork;
      _auditService = auditService;
      _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<string>> CreateCurrency(CurrencyDto dto)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(dto.CurrencyName))
          throw new CustomException("Currency Name is required.");

        var duplicate = await _unitOfWork.Repository<Currency>()
            .FindAsync(x =>
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.CurrencyName.ToLower() == dto.CurrencyName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Currency already exists.");

        Currency currency = new Currency
        {
          CompanyId = dto.CompanyId,
          RegionId = dto.RegionId,
          CurrencyName = dto.CurrencyName,
          CurrencyCode = dto.CurrencyCode,
          Description = dto.Description,
          IsActive = dto.IsActive,
          IsDeleted = false,
          CreatedBy = _currentUserService.UserId,
          CreatedAt = DateTime.Now
        };

        await _unitOfWork.Repository<Currency>().AddAsync(currency);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "Currency",
            "INSERT",
            currency.CurrencyId,
            "",
            JsonConvert.SerializeObject(currency),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Currency Created Successfully",
          Data = currency.CurrencyName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while creating currency");
        throw;
      }
    }

    // UPDATE CURRENCY
    public async Task<ApiResponse<string>> UpdateCurrency(CurrencyDto dto)
    {
      try
      {
        var currency = (await _unitOfWork.Repository<Currency>()
            .FindAsync(x => x.CurrencyId == dto.CurrencyId))
            .FirstOrDefault();

        if (currency == null)
          throw new CustomException("Currency not found.");

        var duplicate = await _unitOfWork.Repository<Currency>()
            .FindAsync(x =>
                x.CurrencyId != dto.CurrencyId &&
                x.CompanyId == dto.CompanyId &&
                x.RegionId == dto.RegionId &&
                x.CurrencyName.ToLower() == dto.CurrencyName.ToLower());

        if (duplicate.Any())
          throw new CustomException("Currency already exists.");

        string oldValues = JsonConvert.SerializeObject(currency);

        currency.CompanyId = dto.CompanyId;
        currency.RegionId = dto.RegionId;
        currency.CurrencyName = dto.CurrencyName;
        currency.CurrencyCode = dto.CurrencyCode;
        currency.Description = dto.Description;
        currency.IsActive = dto.IsActive;
        currency.ModifiedBy = _currentUserService.UserId;
        currency.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<Currency>().Update(currency);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "Currency",
            "UPDATE",
            currency.CurrencyId,
            oldValues,
            JsonConvert.SerializeObject(currency),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Currency Updated Successfully",
          Data = currency.CurrencyName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while updating currency");
        throw;
      }
    }

    public async Task<ApiResponse<string>> DeleteCurrency(int id)
    {
      try
      {
        var currency = (await _unitOfWork.Repository<Currency>()
            .FindAsync(x => x.CurrencyId == id))
            .FirstOrDefault();

        if (currency == null)
          throw new CustomException("Currency not found.");

        string oldValues = JsonConvert.SerializeObject(currency);

        currency.IsDeleted = true;
        currency.ModifiedBy = _currentUserService.UserId;
        currency.ModifiedAt = DateTime.Now;

        _unitOfWork.Repository<Currency>().Update(currency);

        await _unitOfWork.CompleteAsync();

        await _auditService.LogAsync(
            "Currency",
            "DELETE",
            currency.CurrencyId,
            oldValues,
            JsonConvert.SerializeObject(currency),
            _currentUserService.UserId);

        return new ApiResponse<string>
        {
          Success = true,
          Message = "Currency Deleted Successfully",
          Data = currency.CurrencyName
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while deleting currency");
        throw;
      }
    }

    public async Task<ApiResponse<List<CurrencyDto>>> GetCurrencies()
    {
      try
      {
        var currencies = await _unitOfWork.Repository<Currency>()
            .GetAllAsync();

        var result = currencies
            .Where(x => !x.IsDeleted)
            .Select(x => new CurrencyDto
            {
              CurrencyId = x.CurrencyId,
              CompanyId = x.CompanyId,
              RegionId = x.RegionId,
              CurrencyName = x.CurrencyName,
              CurrencyCode = x.CurrencyCode,
              Description = x.Description,
              IsActive = x.IsActive
            })
            .ToList();

        return new ApiResponse<List<CurrencyDto>>
        {
          Success = true,
          Message = "Currencies Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting currencies");
        throw;
      }
    }
    public async Task<ApiResponse<CurrencyDto>> GetCurrencyById(int id)
    {
      try
      {
        var currency = (await _unitOfWork.Repository<Currency>()
            .FindAsync(x => x.CurrencyId == id && !x.IsDeleted))
            .FirstOrDefault();

        if (currency == null)
          throw new CustomException("Currency not found.");

        var result = new CurrencyDto
        {
          CurrencyId = currency.CurrencyId,
          CompanyId = currency.CompanyId,
          RegionId = currency.RegionId,
          CurrencyName = currency.CurrencyName,
          CurrencyCode = currency.CurrencyCode,
          Description = currency.Description,
          IsActive = currency.IsActive
        };

        return new ApiResponse<CurrencyDto>
        {
          Success = true,
          Message = "Currency Retrieved Successfully",
          Data = result
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while getting currency by id");
        throw;
      }
    }

  }
}




