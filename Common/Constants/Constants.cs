//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace GtdCommon.Constant
{
    public class Constants
    {
        public const string SecretKey = "This is my custom Secret key for authnetication";
        public const string RequiredUser = "Username is required";
        public const string CorectEmail = "example33@gmail.com";
        public const string CorectPassword = "1234567";
        public const string ClaimUserId = "UserId";
        public const int TokenExpirationInHours = 1;
        public const string PasswordInvalidMessage = "The Password field is not a valid password";
        public const string PasswordRegularExpression = @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=.,\-_!])([a-zA-Z0-9 @#$%^&+=*.,\-_!]){8,}$";
        public const string GoogleResponsePath = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=";
        public const string FacebookResponsePath = "https://graph.facebook.com/v3.2/me?fields=email,first_name,last_name&access_token=";
        public const string AdminRole = "Admin";
        public const string SuperAdminRole = "SuperAdmin";
        public const string UserRole = "User";
    }
}
