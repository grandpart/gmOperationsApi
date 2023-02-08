using Microsoft.AspNetCore.Mvc;
using Zephry;

namespace Grandmark
{
    [ApiController]
    public class TicketPriorityController : Controller
    {

        #region Get Specific - GET

        [Route("ticket/priority/{tprKey}")]
        [HttpGet]
        public string Load(int tprKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);

            try
            {
                var vTicketPriority = new TicketPriority
                {
                    EntKey = vLogonToken.Entity,
                    TprKey = tprKey
                };
                UserBridge.Invoke(TicketPriorityBusiness.Load, vTicketPriority, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return vTicketPriority.SerializeToJson();
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

        [Route("ticket/priority")]
        [HttpGet]
        public string LoadList([FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            try
            {
                var vTicketPriorityCollection = new TicketPriorityCollection();
                UserBridge.Invoke(TicketPriorityBusiness.Load, vTicketPriorityCollection, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return vTicketPriorityCollection.SerializeToJson();
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
        [Route("ticket/priority")]
        [HttpPost]
        public string Create([FromBody] TicketPriority aTicketPriority, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aTicketPriority.EntKey = vLogonToken.Entity;
            try
            {
                UserBridge.Invoke(TicketPriorityBusiness.Insert, aTicketPriority, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return aTicketPriority.SerializeToJson();
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
        [Route("ticket/priority/{tprKey}")]
        [HttpPut]
        public string Update(int tprKey, [FromBody] TicketPriority aTicketPriority, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aTicketPriority.EntKey = vLogonToken.Entity;
            aTicketPriority.TprKey = tprKey;
            try
            {
                UserBridge.Invoke(TicketPriorityBusiness.Update, aTicketPriority, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return aTicketPriority.SerializeToJson();
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
        [Route("ticket/priority/{tprKey}")]
        [HttpDelete]
        public string Delete(int tprKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            TicketPriority vTicketPriority = new();
            vTicketPriority.EntKey = vLogonToken.Entity;
            vTicketPriority.TprKey = tprKey;
            try
            {
                UserBridge.Invoke(TicketPriorityBusiness.Delete, vTicketPriority, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return vTicketPriority.SerializeToJson();
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
