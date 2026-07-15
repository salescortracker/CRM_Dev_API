using Business_Layer.Models.ForgotEmailClasses;
using DataAccess_Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces
{
    public interface IAuthService
    {
        Task<string> Signup(SignupRequest request);

        Task<AuthResponse> Login(LoginRequest request);

        // Forgot Password

        Task<string> ForgotPassword(ForgotPasswordRequest request);

        Task<string> VerifyOtp(VerifyOtpRequest request);

        Task<string> ResetPassword(ResetPasswordRequest request);

        // Change Password 

        Task<string> ChangePassword(ChangePasswordRequest request);
    }
}
