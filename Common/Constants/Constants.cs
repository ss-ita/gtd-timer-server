//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace GtdCommon.Constant
{
    /// <summary>
    /// Enum, which contains types of supported watches.
    /// </summary>
    public enum WatchType
    {
        Stopwatch,
        Timer
    }

    /// <summary>
    /// Enum, which contains types of supported tokens.
    /// </summary>
    public enum TokenType
    {
        EmailVerification,
        PasswordRecovery
    }

    /// <summary>
    /// Class for storing contstants
    /// </summary>
    public class Constants
    {
        public const string SecretKey = "This is my custom Secret key for authnetication";
        public const string RequiredUser = "Username is required";
        public const string CorectEmail = "example33@gmail.com";
        public const string CorectPassword = "1234567";
        public const string ClaimUserId = "UserId";
        public const string ConfirmEmail = "confirm-email";
        public const string PasswordRecovery = "password-recovery";
        public const string VerifyEmailButton = "Verify Email";
        public const string VerifyEmailTitle = "Verify your Email Address";
        public const string VerifyEmailMessage = "Thanks for signing up for Getting Things Done Timer! We're excited to have you as an early user.";
        public const string PasswordResetButton = "Reset Password";
        public const string PasswordResetTitle = "Password Recovery";
        public const string PasswordResetMessage = "You recently requested to reset your password for your GTD Timer account. Click the button below to reset it"; 
        public const int TokenExpirationInDays = 7;
        public const int EmailTokenExpiration = 3;
        public const int PasswordRecoveryTokenExpiration = 2;
        public const string PasswordInvalidMessage = "The Password field is not a valid password";
        public const string PasswordRegularExpression = @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=.,\-_!])([a-zA-Z0-9 @#$%^&+=*.,\-_!]){8,}$";
        public const string GoogleResponsePath = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=";
        public const string FacebookResponsePath = "https://graph.facebook.com/v3.2/me?fields=email,first_name,last_name&access_token=";
        public const string AdminRole = "Admin";
        public const string UserRole = "User";
    }
}
