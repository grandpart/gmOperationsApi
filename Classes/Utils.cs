using System.Text;
using Microsoft.AspNetCore.Authentication;
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
            cookieOptions.Expires = DateTime.Now.AddDays(30);
            cookieOptions.Path = "/";
            Response.Cookies.Append(TokenDescriptor, protectedText);
        }

        public static LogonToken GetLogonToken(HttpRequest Request)
        {
            string? cookieValue = Request.Cookies[TokenDescriptor];
            if (cookieValue == null)
            {
                throw new Exception("No Grandmarkcookie in request");
            }
            var dataProtectionProvider = DataProtectionProvider.Create(ApplicationDescriptor);
            var protector = dataProtectionProvider.CreateProtector(ApplicationKey);
            cookieValue = protector.Unprotect(cookieValue);
            var vLogonToken = cookieValue.DeserializeFromJson<LogonToken>();
            return vLogonToken;
        }

        public static string StatusJson(TransactionStatus? aTransactionStatus, string aObjectJson)
        {
            var vTransactionStatus = new TransactionStatus();
            if (aTransactionStatus == null)
            {
                vTransactionStatus.TransactionResult = TransactionResult.Ok;
                vTransactionStatus.Message = "Success";
            }
            else
            {
                vTransactionStatus.AssignFromSource(aTransactionStatus);
            }
            var vStringBuilder = new StringBuilder();
            vStringBuilder.Append('{');
            vStringBuilder.AppendFormat("{0}:{1}", "\"status\"", vTransactionStatus.SerializeToJson());
            if (!string.IsNullOrWhiteSpace(aObjectJson))
            {
                vStringBuilder.AppendFormat(",{0}:{1}", "\"data\"", aObjectJson);
            }
            vStringBuilder.Append('}');
            return vStringBuilder.ToString();
        }
    }
}






