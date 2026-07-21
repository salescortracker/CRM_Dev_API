using Business_Layer.Interfaces;
using Business_Layer.Services.Auth;
using DataAccess_Layers.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business_Layer.Models.ForgotEmailClasses;
using Business_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Business_Layer.Interfaces.SuperAdminInterface;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAuditLogService _service;

        public AuthController(IAuthService authService, IAuditLogService service)
        {
            _authService = authService;
            _service = service;
        }

        /// <summary>
        /// signup
        /// </summary>
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignupRequest request)
        {
            var result = await _authService.Signup(request);
            return Ok(result);
        }

        /// <summary>
        /// login
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.Login(request);
            return Ok(result);
        }

        /// <summary>
        /// forgot-password
        /// </summary>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var result = await _authService.ForgotPassword(request);

            return Ok(new
            {
                Success = true,
                Message = result
            });
        }

        /// <summary>
        /// verify-otp
        /// </summary>
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpRequest request)
        {
            var result = await _authService.VerifyOtp(request);

            return Ok(new
            {
                Success = true,
                Message = result
            });
        }

        /// <summary>
        /// reset-password
        /// </summary>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _authService.ResetPassword(request);

            return Ok(new
            {
                Success = true,
                Message = result
            });
        }

        /// <summary>
        /// Change Password
        /// </summary>
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var result = await _authService.ChangePassword(request);

            return Ok(result);
        }

        [HttpGet("getallauditlogs")]
        public async Task<IActionResult> GetAuditLogs()
        {
            return Ok(await _service.GetAuditLogs());
        }
    }
}
