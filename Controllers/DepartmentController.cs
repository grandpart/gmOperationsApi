using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zephry;

namespace Grandmark
{
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        #region Get Specific - GET

        [Route("department/{depKey}")]
        [HttpGet]
        public string Load(int depKey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);

            try
            {
                var vDepartment = new Department
                {
                    EntKey = vLogonToken.Entity,
                    DptKey = depKey
                };
                UserBridge.Invoke(DepartmentBusiness.Load, vDepartment, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return vDepartment.SerializeToJson();
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

        [Route("department")]
        [HttpGet]
        public string LoadList([FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            try
            {
                var vDepartmentList = new DepartmentCollection();
                UserBridge.Invoke(DepartmentBusiness.LoadList, vDepartmentList, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return vDepartmentList.SerializeToJson();
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
        [Route("department")]
        [HttpPost]
        public string Create([FromBody] Department aDepartment, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aDepartment.EntKey = vLogonToken.Entity;
            try
            {
                UserBridge.Invoke(DepartmentBusiness.Insert, aDepartment, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return aDepartment.SerializeToJson();
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
        [Route("department/{depKey}")]
        [HttpPut]
        public string Update(int depKey, [FromBody] Department aDepartment, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            aDepartment.EntKey = vLogonToken.Entity;
            aDepartment.DptKey = depKey;
            try
            {
                UserBridge.Invoke(DepartmentBusiness.Update, aDepartment, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return aDepartment.SerializeToJson();
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
        [Route("department/{depKey}")]
        [HttpDelete]
        public string Delete(int depkey, [FromServices] Connection aConnection)
        {
            var vLogonToken = Utils.GetLogonToken(HttpContext);
            Department vDepartment = new();
            vDepartment.EntKey = vLogonToken.Entity;
            vDepartment.DptKey = depkey;
            try
            {
                UserBridge.Invoke(DepartmentBusiness.Delete, vDepartment, vLogonToken, aConnection);
                Response.StatusCode = StatusCodes.Status200OK;
                // NB, change this to a pure success message, no return
                return vDepartment.SerializeToJson();
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
