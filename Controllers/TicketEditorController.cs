using Microsoft.AspNetCore.Mvc;
using Zephry;

namespace Grandmark
{
    [ApiController]
    public class TicketEditorController : Controller
    {

        #region Get Specific - GET

        [Route("ticket/editor/{tckKey}")]
        [HttpGet]
        public string Load(int tckKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            try
            {
                var vTicketEditor = new TicketEditor();
                vTicketEditor.Ticket.TckKey = tckKey;
                UserBridge.Invoke(TicketEditorBusiness.Load, vTicketEditor, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return vTicketEditor.SerializeToJson();
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

    }
}
