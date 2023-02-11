using Microsoft.AspNetCore.Mvc;
using Zephry;

namespace Grandmark
{
    [ApiController]
    public class CustomerStreamController : Controller
    {

        #region Get Specific - GET

        [Route("customers/stream")]
        [HttpGet]
        public string Load([FromQuery] string filter, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            try
            {
                var aKeyValueCollection = new KeyValueCollection();
                aKeyValueCollection.Filter = filter;
                UserBridge.Invoke(CustomerStreamBusiness.Load, aKeyValueCollection, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                return aKeyValueCollection.SerializeToJson();
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
