using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SURVEY_SYSTEM.BusinessLayer;
using SURVEY_SYSTEM.BusinessLayer.Master;
using SURVEY_SYSTEM.EntityLayer;
using System.Data;

namespace SURVEY_SYSTEM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorCodesMasterAPIController : ControllerBase
    {
        ErrorCodesMasterManager objErrorCodesManager = new ErrorCodesMasterManager();

        [HttpGet]
        [Route("FetchErrorCodesMaster")]
        public ActionResult FetchErrorCodesMaster()
        {
            try
            {
                DataTable dt = objErrorCodesManager.FetchAllErrorCodesMaster();
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("SaveErrorCodesMaster")]
        public ActionResult SaveErrorCodesMaster(ErrorCodesMaster objErrorCodesMaster)
        {
            try
            {
                return Ok(objErrorCodesManager.SaveErrorCodesMaster(objErrorCodesMaster));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("UpdateErrorCodesMaster")]
        public ActionResult UpdateErrorCodesMaster(ErrorCodesMaster objErrorCodesMaster)
        {
            try
            {
                return Ok(objErrorCodesManager.UpdateErrorCodesMaster(objErrorCodesMaster));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("CheckDuplicateErrorCodesMaster/{id}")]
        public ActionResult CheckDuplicateErrorCodesMaster(string id )
        {
            try
            {
                return Ok(objErrorCodesManager.CheckDuplicateErrorCodesMaster(id));

            }
            catch (Exception)
            {

                throw;
            }        }

        [HttpGet]
        [Route("GetErrorCodes/{id}")]
        public ActionResult GetErrorCodes(string id)
        {
            try
            {
                ErrorCodesMaster objErrorCodeMaster = new ErrorCodesMaster();
                objErrorCodeMaster.ErrCode = id;

                DataTable dt = objErrorCodesManager.FetchByErrcode(objErrorCodeMaster);
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("DeleteErrorCodesMaster/{id}")]
        public ActionResult DeleteErrorCodesMaster(string id)
        {
            try
            {
                ErrorCodesMaster objErrorCodeMaster = new ErrorCodesMaster();
                objErrorCodeMaster.ErrCode = id;

                return Ok(objErrorCodesManager.DeleteErrorCodesMaster(objErrorCodeMaster));
            }
            catch (Exception)
            {

                throw;
            }
        }
      
    }
}
