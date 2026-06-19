using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Microsoft.Extensions.Configuration;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer.Interfaces;
using Shared.Exceptions;

namespace Business_Layer.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;

        public AuthService(IUnitOfWork uow, IConfiguration config)
        {
            _uow = uow;
            _config = config;
        }

        public async Task<string> Signup(SignupRequest request)
        {
            var repo = _uow.Repository<UserLogin>();

            var existingUser = await repo.FindAsync(x =>
                x.UserName == request.UserName || x.Email == request.Email);

            if (existingUser.Any())
                throw new CustomException("User already exists");

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

            return "User created successfully";
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var repo = _uow.Repository<UserLogin>();

            var user = (await repo.FindAsync(x =>
                       x.UserName == request.UserName))
                       .FirstOrDefault();

            if (user == null)
                throw new CustomException("Invalid username");

            // SAFE CHECK
            if (string.IsNullOrEmpty(user.PasswordHash))
                throw new CustomException("Password missing in DB");

            if (!PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
            {
                user.FailedLoginAttempts += 1;

                if (user.FailedLoginAttempts >= 3)
                    user.IsLocked = true;

                repo.Update(user);
                await _uow.CompleteAsync();

                throw new CustomException("Invalid password");
            }

            if (user.IsLocked)
                throw new CustomException("Account locked");

            user.LastLoginDate = DateTime.UtcNow;
            user.FailedLoginAttempts = 0;

            repo.Update(user);
            await _uow.CompleteAsync();

            var token = JwtHelper.GenerateToken(
                user.UserName,
                _config["JwtSettings:SecretKey"]);

            return new AuthResponse
            {
                Token = token,
                UserName = user.UserName,
                Role = user.Role
            };
        }
    }
}