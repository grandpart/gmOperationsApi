using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zephry;

namespace Grandmark
{
   
    [ApiController]
    public class BranchController : ControllerBase
    {
        #region Get Specific - GET

        [Route("branch/{brhKey}")]
        [HttpGet]
        public string Load(int brhKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);

            try
            {
                var vBranch = new Branch
                {
                    EntKey = vLogonToken.Entity,
                    BrhKey = brhKey
                };
                UserBridge.Invoke(BranchBusiness.Load, vBranch, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, vBranch.SerializeToJson());
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

        [Route("branch")]
        [HttpGet]
        public string LoadList([FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            try
            {

                var vBranchList = new BranchCollection();
                UserBridge.Invoke(BranchBusiness.LoadList, vBranchList, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, vBranchList.SerializeToJson());
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
        [Route("branch")]
        [HttpPost]
        public string Create([FromBody] Branch aBranch, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aBranch.EntKey = vLogonToken.Entity;
            try
            {
                UserBridge.Invoke(BranchBusiness.Insert, aBranch, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, aBranch.SerializeToJson());
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
        [Route("branch/{brhKey}")]
        [HttpPut]
        public string Update(int brhKey, [FromBody] Branch aBranch, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aBranch.EntKey = vLogonToken.Entity;
            aBranch.BrhKey = brhKey;
            try
            {
                UserBridge.Invoke(BranchBusiness.Update, aBranch, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, aBranch.SerializeToJson());
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
        [Route("branch/{brhKey}")]
        [HttpDelete]
        public string Delete(int brhKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            Branch vBranchKey = new();
            vBranchKey.EntKey = vLogonToken.Entity;
            vBranchKey.BrhKey = brhKey;
            try
            {
                UserBridge.Invoke(BranchBusiness.Delete, vBranchKey, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, vBranchKey.SerializeToJson());
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
