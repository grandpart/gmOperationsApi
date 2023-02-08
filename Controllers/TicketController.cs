using Microsoft.AspNetCore.Mvc;
using Zephry;

namespace Grandmark
{
    [ApiController]
    public class TicketController : Controller
    {

        #region Get Specific - GET

        [Route("ticket/{tckKey}")]
        [HttpGet]
        public string Load(int tckKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);

            try
            {
                var vTicket = new Ticket
                {
                    EntKey = vLogonToken.Entity,
                    TtpKey = tckKey
                };
                UserBridge.Invoke(TicketBusiness.Load, vTicket, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return vTicket.SerializeToJson();
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

        #region Read List - GET

        [Route("ticket")]
        [HttpGet]
        public string LoadList([FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            try
            {
                var vTicketCollection = new TicketCollection();
                UserBridge.Invoke(TicketBusiness.Load, vTicketCollection, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return vTicketCollection.SerializeToJson();
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

        #region Create - POST
        [Route("ticket")]
        [HttpPost]
        public string Create([FromBody] Ticket aTicket, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aTicket.EntKey = vLogonToken.Entity;
            try
            {
                UserBridge.Invoke(TicketBusiness.Insert, aTicket, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return aTicket.SerializeToJson();
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

        #region Update - PUT
        [Route("ticket/{tckKey}")]
        [HttpPut]
        public string Update(int tckKey, [FromBody] Ticket aTicket, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aTicket.EntKey = vLogonToken.Entity;
            aTicket.TckKey = tckKey;
            try
            {
                UserBridge.Invoke(TicketBusiness.Update, aTicket, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return aTicket.SerializeToJson();
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

        #region Delete - DELETE
        [Route("ticket/{tckKey}")]
        [HttpDelete]
        public string Delete(int tckKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            Ticket vTicket = new();
            vTicket.EntKey = vLogonToken.Entity;
            vTicket.TtpKey = tckKey;
            try
            {
                UserBridge.Invoke(TicketBusiness.Delete, vTicket, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return vTicket.SerializeToJson();
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
