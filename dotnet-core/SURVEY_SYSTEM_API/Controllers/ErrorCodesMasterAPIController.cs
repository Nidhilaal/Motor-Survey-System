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
            DataTable dt = objErrorCodesManager.FetchAllErrorCodesMaster();
            return Ok(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        [Route("SaveErrorCodesMaster")]
        public ActionResult SaveErrorCodesMaster(ErrorCodesMaster objErrorCodesMaster)
        {
            return Ok(objErrorCodesManager.SaveErrorCodesMaster(objErrorCodesMaster));
        }

        [HttpPost]
        [Route("UpdateErrorCodesMaster")]
        public ActionResult UpdateErrorCodesMaster(ErrorCodesMaster objErrorCodesMaster)
        {
            return Ok(objErrorCodesManager.UpdateErrorCodesMaster(objErrorCodesMaster));
        }

        [HttpPost]
        [Route("CheckDuplicateErrorCodesMaster")]
        public ActionResult CheckDuplicateErrorCodesMaster(ErrorCodesMaster objErrorCodesMaster)
        {
            return Ok(objErrorCodesManager.CheckDuplicateErrorCodesMaster(objErrorCodesMaster));
        }

        [HttpGet]
        [Route("GetErrorCodes/{id}")]
        public ActionResult GetErrorCodes(string id)
        {
            ErrorCodesMaster objErrorCodeMaster = new ErrorCodesMaster();
            objErrorCodeMaster.ErrCode = id;
            
            DataTable dt = objErrorCodesManager.FetchByErrcode(objErrorCodeMaster);
            return Ok(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        [Route("DeleteErrorCodesMaster")]
        public ActionResult DeleteErrorCodesMaster(string errCode)
        {
            ErrorCodesMaster objErrorCodeMaster = new ErrorCodesMaster();
            objErrorCodeMaster.ErrCode = errCode;

            return Ok(objErrorCodesManager.DeleteErrorCodesMaster(objErrorCodeMaster));
        }
      
    }
}
