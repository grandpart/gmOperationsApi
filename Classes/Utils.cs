using System.Net;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Zephry;

namespace Grandmark
{
    public static class Utils
    {
        public const string ApplicationDescriptor = "gmGrandmarkApi";
        public const string ApplicationKey = "this is some kind of secret key";
        public const string TokenDescriptor = "grandmarkcookie";

        public static void AppendCookie(HttpResponse Response, LogonToken aLogonToken)
        {
            var dataProtectionProvider = DataProtectionProvider.Create(ApplicationDescriptor);
            var protector = dataProtectionProvider.CreateProtector(ApplicationKey);
            var protectedText = protector.Protect(aLogonToken.SerializeToJson());

            var cookieOptions = new CookieOptions();
            cookieOptions.Secure = true;
            cookieOptions.HttpOnly = true;
            cookieOptions.SameSite = SameSiteMode.None;            
            cookieOptions.Expires = DateTime.Now.AddDays(30);
            cookieOptions.Path = "/";
            cookieOptions.IsEssential = true;
            Response.Cookies.Append(TokenDescriptor, protectedText, cookieOptions);
        }

        public static LogonToken GetLogonToken(HttpContext aHttpContext)
        {
            string? cookieValue = aHttpContext.Request.Cookies[TokenDescriptor];
            if (cookieValue == null)
            {
                throw new TransactionStatusException(TransactionResult.Access, "No cookie found, please logon");
            }
            var dataProtectionProvider = DataProtectionProvider.Create(ApplicationDescriptor);
            var protector = dataProtectionProvider.CreateProtector(ApplicationKey);
            cookieValue = protector.Unprotect(cookieValue);
            var vLogonToken = cookieValue.DeserializeFromJson<LogonToken>();
            return vLogonToken;
        }
    }
}






