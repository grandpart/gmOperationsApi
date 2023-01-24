using Grandmark;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zephry;

namespace Grandmark
{
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        #region Read specific - GET
        [Route("organization/{orgKey}")]
        [HttpGet]
        public string Load(int orgKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            try
            {
                var vOrganization = new Organization {
                    EntKey = vLogonToken.Entity,
                    OrgKey = orgKey
                };
                UserBridge.Invoke(OrganizationBusiness.Load, vOrganization, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, vOrganization.SerializeToJson());
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

        #region Read list - GET 
        [Route("organization")]
        [HttpGet]
        public string Load([FromServices] Connection aConnection)
        {
            try
            {
                var vLogonToken = Utils.GetLogonToken(HttpContext);
                var vOrganizationProxyList = new OrganizationProxyCollection();
                UserBridge.Invoke(OrganizationProxyBusiness.Load, vOrganizationProxyList, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                var vJson = Utils.StatusJson(null, vOrganizationProxyList.SerializeToJson());
                return Utils.StatusJson(null, vOrganizationProxyList.SerializeToJson());
            }
            catch (TransactionStatusException tx)
            {
                switch (tx.TransactionResult)
                {
                    case TransactionResult.Access:
                        Response.StatusCode = StatusCodes.Status401Unauthorized;
                        break;
                    default:
                        Response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }
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
        [Route("organization")]
        [HttpPost]
        public string Create([FromBody] Organization aOrganization, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aOrganization.EntKey = vLogonToken.Entity;
            try
            {
                UserBridge.Invoke(OrganizationBusiness.Insert, aOrganization, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, aOrganization.SerializeToJson());
            }
            catch (TransactionStatusException tx)
            {
                switch (tx.TransactionResult)
                {
                    case TransactionResult.Access:
                        Response.StatusCode = StatusCodes.Status401Unauthorized;
                        break;
                    default:
                        Response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }
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
        [Route("organization/{orgkey}")]
        [HttpPut]
        public string Update(int orgKey, [FromBody] Organization aOrganization, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aOrganization.EntKey = vLogonToken.Entity;
            aOrganization.OrgKey = orgKey;
            try
            {
                UserBridge.Invoke(OrganizationBusiness.Update, aOrganization, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, aOrganization.SerializeToJson());
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
        [Route("organization/{orgkey}")]
        [HttpDelete]
        public string Delete(int orgKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            OrganizationKey vOrganizationKey = new();
            vOrganizationKey.EntKey = vLogonToken.Entity;
            vOrganizationKey.OrgKey = orgKey;
            try
            {
                UserBridge.Invoke(OrganizationBusiness.Delete, vOrganizationKey, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return Utils.StatusJson(null, vOrganizationKey.SerializeToJson());
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
