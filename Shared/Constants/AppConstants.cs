using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Constants
{
    public static class AppConstants
    {
        // Authentication Messages

        public const string UserAlreadyExists =
            "User already exists";

        public const string UserCreatedSuccessfully =
            "User created successfully";

        public const string InvalidUserName =
            "Invalid username";

        public const string PasswordMissingInDB =
            "Password missing in DB";

        public const string InvalidPassword =
            "Invalid password";

        public const string AccountLocked =
            "Account locked";

        // Generic Messages

        public const string RecordSaved =
            "Record saved successfully";

        public const string RecordUpdated =
            "Record updated successfully";

        public const string RecordDeleted =
            "Record deleted successfully";

        public const string NoRecordsFound =
            "No records found";

        public const string InternalServerError =
            "Internal server error";

        // Department Master Screen Messages 

        public const string DepartmentNameRequired =
            "Department Name is required";

        public const string DepartmentCodeRequired =
            "Department Code is required";

        public const string DepartmentCodeExists =
            "Department Code already exists";

        public const string DepartmentNotFound =
            "Department not found";

        public const string ExceptionWhileCreatingDepartment = 
            "Error while creating department";

        public const string ErrorWhileDeleting =
           "Error while Deleting department";

        // Forgot password messages 

        public const string EmailNotFound =
    "Email does not exist.";

        public const string OtpSentSuccessfully =
            "OTP sent successfully.";

        public const string InvalidOtp =
            "Invalid OTP.";

        public const string OtpExpired =
            "OTP has expired.";

        public const string OtpVerified =
            "OTP verified successfully.";

        public const string PasswordResetSuccessful =
            "Password changed successfully.";

        public const string VerifyOtpFirst =
            "Please verify OTP first.";

        // Change Password Messages 

        public const string CurrentPasswordIncorrect =
            "Current password is incorrect.";

        public const string PasswordChangedSuccessfully =
            "Password changed successfully.";

        public const string NewPasswordCannotBeSame =
            "New password cannot be the same as the current password.";
    }
}
