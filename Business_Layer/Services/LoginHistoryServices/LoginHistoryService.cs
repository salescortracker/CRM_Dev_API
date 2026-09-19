using Business_Layer.DTOs.LoginHistories;
using Business_Layer.Interfaces.Services;
using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Serilog;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business_Layer.Services.LoginHistoryServices
{
    public class LoginHistoryService : ILoginHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginHistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GET ALL

        public async Task<ApiResponse<List<LoginHistoryDto>>>
            GetLoginHistories()
        {
            try
            {
                var history =
                    (await _unitOfWork.Repository<LoginHistory>()
                    .GetAllAsync())
                    .OrderByDescending(x => x.LoginTime)
                    .ToList();

                var userIds = history
                    .Where(x => x.UserId.HasValue)
                    .Select(x => x.UserId!.Value)
                    .Distinct()
                    .ToList();

                var users =
                    (await _unitOfWork.Repository<UserLogin>()
                    .FindAsync(x => userIds.Contains(x.UserId)))
                    .ToList();

                var result = history.Select(x => new LoginHistoryDto
                {
                    LoginHistoryId = x.LoginHistoryId,
                    UserId = x.UserId,
                    UserName = x.UserName,
                    FullName = x.UserId.HasValue
                        ? users.FirstOrDefault(u => u.UserId == x.UserId.Value)?.FullName
                        : null,
                    Email = x.Email,
                    LoginType = x.LoginType,
                    Device = x.Device,
                    IpAddress = x.IpAddress,
                    Location = x.Location,
                    Status = x.Status,
                    LoginTime = x.LoginTime
                }).ToList();

                return new ApiResponse<List<LoginHistoryDto>>
                {
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting login history");
                throw;
            }
        }

        #endregion
    }
}
