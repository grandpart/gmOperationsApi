using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Zephry;

namespace Grandmark
{
    public class AuthController : ControllerBase
    {
        #region Logon POST

        /// <summary>
        /// Logon credentials are posted to this method. Perform initial logon, save an auth cookie
        /// </summary>
        /// <param name="aLogonToken">A logon token.</param>
        /// <returns></returns>
        [Route("logon")]
        [HttpPost]
        [AllowAnonymous]
        public string Logon([FromServices] Connection aConnection, [FromBody] LogonToken aLogonToken)
        {
            try
            {
                // Invoke bridge with LogonToken from body - this authenticates the logon token from body
                AnonymousBridge.Invoke(LogonBusiness.Logon, aLogonToken, aConnection);
                // User is backend authentic, append a cookie
                Utils.AppendCookie(Response, aLogonToken);
                // Return success;
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return new TransactionStatus(200, "Authenticated", "Your logon credentials have been authenticated").SerializeToJson();
            }
            catch (TransactionStatusException tx)
            {
                Response.StatusCode = tx.HttpCode;
                return tx.getTransactionStatus().SerializeToJson();
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new TransactionStatus(StatusCodes.Status500InternalServerError, "Unexpected Server Error", ex.Message).SerializeToJson();
            }
        }

        #endregion

        #region Logoff

        //[AllowAnonymous]
        //public ActionResult Logoff()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Index", "Home", null);
        //}

        #endregion
    }
}
