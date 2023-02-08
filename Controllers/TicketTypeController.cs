using Microsoft.AspNetCore.Mvc;
using Zephry;

namespace Grandmark
{
    [ApiController]
    public class TicketTypeController : Controller
    {

        #region Get Specific - GET

        [Route("ticket/type/{ttpKey}")]
        [HttpGet]
        public string Load(int ttpKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);

            try
            {
                var vTicketType = new TicketType
                {
                    EntKey = vLogonToken.Entity,
                    TtpKey = ttpKey
                };
                UserBridge.Invoke(TicketTypeBusiness.Load, vTicketType, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return vTicketType.SerializeToJson();
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

        [Route("ticket/type")]
        [HttpGet]
        public string LoadList([FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            try
            {
                var vTicketTypeCollection = new TicketTypeCollection();
                UserBridge.Invoke(TicketTypeBusiness.Load, vTicketTypeCollection, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return vTicketTypeCollection.SerializeToJson();
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
        [Route("ticket/type")]
        [HttpPost]
        public string Create([FromBody] TicketType aTicketType, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aTicketType.EntKey = vLogonToken.Entity;
            try
            {
                UserBridge.Invoke(TicketTypeBusiness.Insert, aTicketType, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return aTicketType.SerializeToJson();
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
        [Route("ticket/type/{ttpKey}")]
        [HttpPut]
        public string Update(int ttpKey, [FromBody] TicketType aTicketType, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aTicketType.EntKey = vLogonToken.Entity;
            aTicketType.TtpKey = ttpKey;
            try
            {
                UserBridge.Invoke(TicketTypeBusiness.Update, aTicketType, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return aTicketType.SerializeToJson();
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
        [Route("ticket/type/{ttpKey}")]
        [HttpDelete]
        public string Delete(int ttpKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            TicketType vTicketType = new();
            vTicketType.EntKey = vLogonToken.Entity;
            vTicketType.TtpKey = ttpKey;
            try
            {
                UserBridge.Invoke(TicketTypeBusiness.Delete, vTicketType, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return vTicketType.SerializeToJson();
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
