using Business_Layer.Interfaces;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.EmailService;
using Business_Layer.Interfaces.Services;
using Business_Layer.Models;
using Business_Layer.Models.ForgotEmailClasses;
using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Microsoft.Extensions.Configuration;
using Shared.Constants;
using Shared.Exceptions;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business_Layer.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _templateService;
        private readonly ICurrentUserService _currentUserService;

        public AuthService(IUnitOfWork uow, 
            IConfiguration config, 
            IEmailService emailService, 
            IEmailTemplateService templateService, 
            ICurrentUserService currentUserService)
        {
            _uow = uow;
            _config = config;
            _emailService = emailService;
            _templateService = templateService;
            _currentUserService = currentUserService;
        }

        #region Signup
        public async Task<string> Signup(SignupRequest request)
        {
            var repo = _uow.Repository<UserLogin>();

            var existingUser = await repo.FindAsync(x =>
                x.UserName == request.UserName || x.Email == request.Email);

            if (existingUser.Any())
                throw new CustomException(AppConstants.UserAlreadyExists, 401);

            var user = new UserLogin
            {
                FullName = request.FullName,
                UserName = request.UserName,
                Email = request.Email,
                MobileNumber = request.MobileNumber,
                PasswordHash = PasswordHelper.HashPassword(request.Password),
                Role = "User",
                IsActive = true,
                IsLocked = false,
                CreatedDate = DateTime.UtcNow
            };

            await repo.AddAsync(user);
            await _uow.CompleteAsync();

            return AppConstants.UserCreatedSuccessfully;
        }

        #endregion

        #region Login
        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var repo = _uow.Repository<UserLogin>();

            var user = (await repo.FindAsync(x =>
                       x.UserName == request.UserName))
                       .FirstOrDefault();

            if (user == null)
                throw new CustomException(AppConstants.InvalidUserName);

            // SAFE CHECK
            if (string.IsNullOrEmpty(user.PasswordHash))
                throw new CustomException(AppConstants.PasswordMissingInDB);

            if (!PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
            {
                user.FailedLoginAttempts += 1;

                if (user.FailedLoginAttempts >= 3)
                    user.IsLocked = true;

                repo.Update(user);
                await _uow.CompleteAsync();

                throw new CustomException(AppConstants.InvalidPassword);
            }

            if (user.IsLocked)
                throw new CustomException(AppConstants.AccountLocked, 403);

            user.LastLoginDate = DateTime.UtcNow;
            user.FailedLoginAttempts = 0;

            repo.Update(user);
            await _uow.CompleteAsync();

            var token = JwtHelper.GenerateToken(
               user.UserId,
               user.UserName,
               user.Role,
               _config["JwtSettings:SecretKey"],
               _config["JwtSettings:Issuer"],
               _config["JwtSettings:Audience"],
              int.Parse(_config["JwtSettings:ExpiryMinutes"])
            );

            return new AuthResponse
            {
                Token = token,
                UserName = user.UserName,
                Role = user.Role
            };
        }
        #endregion

        #region ForgotPassword
        public async Task<string> ForgotPassword(ForgotPasswordRequest request)
        {
            var repo = _uow.Repository<UserLogin>();

            var user = (await repo.FindAsync(x => x.Email == request.Email)).FirstOrDefault();

            if (user == null) throw new CustomException(AppConstants.EmailNotFound);

            string otp = OtpHelper.GenerateOtp();

            user.OtpCode = otp;

            user.OtpExpiry = DateTime.UtcNow.AddMinutes(5);

            user.IsOtpVerified = false;

            repo.Update(user);

            await _uow.CompleteAsync();

            string body = _templateService.ForgotPasswordOtpTemplate(user.FullName, otp);

            await _emailService.SendEmailAsync(
                new EmailRequest
                {
                    To = user.Email,

                    Subject = "CRM Password Reset OTP",

                    HtmlBody = body
                });

            return AppConstants.OtpSentSuccessfully;
        }
        #endregion

        #region VerifyOtp
        public async Task<string> VerifyOtp(VerifyOtpRequest request)
        {
            var repo = _uow.Repository<UserLogin>();

            var user = (await repo.FindAsync(x => x.Email == request.Email)).FirstOrDefault();

            if (user == null) throw new CustomException(AppConstants.EmailNotFound);

            if (user.OtpCode != request.Otp) throw new CustomException(AppConstants.InvalidOtp);

            if (user.OtpExpiry < DateTime.UtcNow)  throw new CustomException(AppConstants.OtpExpired);

            user.IsOtpVerified = true;

            repo.Update(user);

            await _uow.CompleteAsync();

            return AppConstants.OtpVerified;
        }
        #endregion

        #region ResetPassword
        public async Task<string> ResetPassword(ResetPasswordRequest request)
        {
            var repo = _uow.Repository<UserLogin>();

            var user = (await repo.FindAsync(x => x.Email == request.Email)).FirstOrDefault();

            if (user == null) throw new CustomException(AppConstants.EmailNotFound);

            if (!user.IsOtpVerified) throw new CustomException(AppConstants.VerifyOtpFirst);

            user.PasswordHash = PasswordHelper.HashPassword(request.NewPassword);

            user.OtpCode = null;

            user.OtpExpiry = null;

            user.IsOtpVerified = false;

            repo.Update(user);

            await _uow.CompleteAsync();

            return AppConstants.PasswordResetSuccessful;
        }
        #endregion

        #region ChangePassword

        public async Task<string> ChangePassword(ChangePasswordRequest request)
        {
            var repo = _uow.Repository<UserLogin>();

            // Logged-in User Id from JWT
            int userId = _currentUserService.UserId;

            var user = await repo.GetByIdAsync(userId);

            if (user == null)
                throw new CustomException(AppConstants.InvalidUserName);

            // Verify Current Password
            bool isCurrentPasswordValid = PasswordHelper.VerifyPassword(request.CurrentPassword, user.PasswordHash);

            if (!isCurrentPasswordValid) throw new CustomException( AppConstants.CurrentPasswordIncorrect);

            // Prevent same password
            if (PasswordHelper.VerifyPassword(
                    request.NewPassword,
                    user.PasswordHash))
            {
                throw new CustomException(
                    AppConstants.NewPasswordCannotBeSame);
            }

            // Hash New Password
            user.PasswordHash =
                PasswordHelper.HashPassword(
                    request.NewPassword);

            repo.Update(user);

            await _uow.CompleteAsync();

            return AppConstants.PasswordChangedSuccessfully;
        }

        #endregion
    }
}