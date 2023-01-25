using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zephry;

namespace Grandmark
{
    
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        #region Get Specific - GET

        [Route("warehouse/{whsKey}")]
        [HttpGet]
        public string Load(int whsKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);

            try
            {
                var vWarehouse = new Warehouse
                {
                    EntKey = vLogonToken.Entity,
                    WhsKey = whsKey
                };
                UserBridge.Invoke(WarehouseBusiness.Load, vWarehouse, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, vWarehouse.SerializeToJson());
            }
            catch (TransactionStatusException tx)
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Utils.StatusJson(new TransactionStatus(tx.TransactionResult, tx.Message), string.Empty);
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Utils.StatusJson(new TransactionStatus(TransactionResult.General, ex.Message), string.Empty);
            }
        }

        #endregion

        #region Read List - GET

        [Route("warehouse")]
        [HttpGet]
        public string LoadList([FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            try
            {
                
                var vWarehouseList = new WarehouseCollection();
                UserBridge.Invoke(WarehouseBusiness.LoadList, vWarehouseList, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, vWarehouseList.SerializeToJson());
            }
            catch (TransactionStatusException tx)
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Utils.StatusJson(new TransactionStatus(tx.TransactionResult, tx.Message), string.Empty);
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Utils.StatusJson(new TransactionStatus(TransactionResult.General, ex.Message), string.Empty);
            }
        }

        #endregion

        #region Create - POST
        [Route("warehouse")]
        [HttpPost]
        public string Create([FromBody] Warehouse aWarehouse, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aWarehouse.EntKey = vLogonToken.Entity;
            try
            {
                UserBridge.Invoke(WarehouseBusiness.Insert, aWarehouse, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, aWarehouse.SerializeToJson());
            }
            catch (TransactionStatusException tx)
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Utils.StatusJson(new TransactionStatus(tx.TransactionResult, tx.Message), string.Empty);
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Utils.StatusJson(new TransactionStatus(TransactionResult.General, ex.Message), string.Empty);
            }
        }
        #endregion

        #region Update - PUT
        [Route("warehouse/{whsKey}")]
        [HttpPut]
        public string Update(int whsKey, [FromBody] Warehouse aWarehouse, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aWarehouse.EntKey = vLogonToken.Entity;
            aWarehouse.WhsKey = whsKey;
            try
            {
                UserBridge.Invoke(WarehouseBusiness.Update, aWarehouse, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, aWarehouse.SerializeToJson());
            }
            catch (TransactionStatusException tx)
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Utils.StatusJson(new TransactionStatus(tx.TransactionResult, tx.Message), string.Empty);
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Utils.StatusJson(new TransactionStatus(TransactionResult.General, ex.Message), string.Empty);
            }
        }
        #endregion

        #region Delete - DELETE
        [Route("warehouse/{whsKey}")]
        [HttpDelete]
        public string Delete(int whsKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            Warehouse vWarehouseKey = new();
            vWarehouseKey.EntKey = vLogonToken.Entity;
            vWarehouseKey.WhsKey = whsKey;
            try
            {
                UserBridge.Invoke(WarehouseBusiness.Delete, vWarehouseKey, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, vWarehouseKey.SerializeToJson());
            }
            catch (TransactionStatusException tx)
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Utils.StatusJson(new TransactionStatus(tx.TransactionResult, tx.Message), string.Empty);
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Utils.StatusJson(new TransactionStatus(TransactionResult.General, ex.Message), string.Empty);
            }
        }
        #endregion
    }
}
