using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zephry;

namespace Grandmark
{
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {

        #region Get Specific - GET

        [Route("exchangerate/{exrKey}")]
        [HttpGet]
        public string Load(int exrKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);

            try
            {
                var vExchangeRate = new ExchangeRate
                {
                    EntKey = vLogonToken.Entity,
                    ExrKey = exrKey
                };
                UserBridge.Invoke(ExchangeRateBusiness.Load, vExchangeRate, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return vExchangeRate.SerializeToJson();
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

        [Route("exchangerate")]
        [HttpGet]
        public string LoadList([FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            try
            {
                var vExchangeRateList = new ExchangeRateCollection();
                UserBridge.Invoke(ExchangeRateBusiness.LoadList, vExchangeRateList, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return vExchangeRateList.SerializeToJson();
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
        [Route("exchangerate")]
        [HttpPost]
        public string Create([FromBody] ExchangeRate aExchangeRate, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aExchangeRate.EntKey = vLogonToken.Entity;
            try
            {
                UserBridge.Invoke(ExchangeRateBusiness.Insert, aExchangeRate, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return aExchangeRate.SerializeToJson();
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
        [Route("exchangerate/{exrKey}")]
        [HttpPut]
        public string Update(int exrKey, [FromBody] ExchangeRate aExchangeRate, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aExchangeRate.EntKey = vLogonToken.Entity;
            aExchangeRate.ExrKey = exrKey;
            try
            {
                UserBridge.Invoke(ExchangeRateBusiness.Update, aExchangeRate, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return aExchangeRate.SerializeToJson();
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
        [Route("exchangerate/{exrKey}")]
        [HttpDelete]
        public string Delete(int exrKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            ExchangeRate vExchangeRate = new();
            vExchangeRate.EntKey = vLogonToken.Entity;
            vExchangeRate.ExrKey = exrKey;
            try
            {
                UserBridge.Invoke(ExchangeRateBusiness.Delete, vExchangeRate, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return vExchangeRate.SerializeToJson();
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
