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
            var vLogonToken = Utils.GetLogonToken(Request);
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

        #region Create - POST
        [Route("organization")]
        [HttpPost]
        public string Create([FromBody] Organization aOrganization, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(Request);
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
        [Route("organization/{orgkey}")]
        [HttpPut]
        public string Update(int orgKey, [FromBody] Organization aOrganization, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(Request);
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
            var vLogonToken = Utils.GetLogonToken(Request);
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
