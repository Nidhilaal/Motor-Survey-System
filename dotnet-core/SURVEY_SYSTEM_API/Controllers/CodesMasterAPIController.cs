using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SURVEY_SYSTEM.BusinessLayer;
using SURVEY_SYSTEM.BusinessLayer.Master;
using SURVEY_SYSTEM.BusinessLayer.Transaction;
using SURVEY_SYSTEM.EntityLayer;
using SURVEY_SYSTEM.EntityLayer.Transaction;
using System.Data;

namespace SURVEY_SYSTEM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodesMasterAPIController : ControllerBase
    {
        [HttpGet]
        [Route("FetchCodesMaster")]
        public ActionResult FetchCodesMaster()
        {
            CodesMasterManager objCodesMasterManager = new CodesMasterManager();
            DataTable dt = objCodesMasterManager.FetchAllCodesMaster();

            return Ok(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        [Route("SaveCodesMaster")]
        public ActionResult SaveCodesMaster(CodesMaster codesMaster)
        {
            CodesMasterManager objCodesMasterManager = new CodesMasterManager();       
            return Ok(objCodesMasterManager.SaveCodesMaster(codesMaster));
        }

        [HttpPost]
        [Route("UpdateCodesMaster")]
        public ActionResult UpdateCodesMaster(CodesMaster codesMaster)
        {
            CodesMasterManager objCodesMasterManager = new CodesMasterManager();
            return Ok(objCodesMasterManager.UpdateCodesMaster(codesMaster));
        }

        [HttpPost]
        [Route("CheckDuplicateCodesMaster")]
        public ActionResult CheckDuplicateCodesMaster(CodesMaster codesMaster)
        {
            CodesMasterManager objCodesMasterManager = new CodesMasterManager();
            return Ok(objCodesMasterManager.CheckDuplicateCodesMaster(codesMaster));
        }

        [HttpGet]
        [Route("GetCodes/{id}")]
        public ActionResult GetCodes(string id)
        {
            CodesMaster objCodeMaster = new CodesMaster();
            objCodeMaster.CmType = id;
            CodesMasterManager objCodesMasterManager = new CodesMasterManager();
            DataTable dt = objCodesMasterManager.FillDropDownList(objCodeMaster);

            return Ok(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        [Route("DeleteCodesMaster")]
        public ActionResult DeleteCodesMaster(string cmCode, string cmType)
        {
            CodesMasterManager objCodesMasterManager = new CodesMasterManager();
  
            CodesMaster objCodesMaster = new CodesMaster();
            objCodesMaster.CmCode = cmCode;
            objCodesMaster.CmType = cmType;

            return Ok(objCodesMasterManager.DeleteCodesMaster(objCodesMaster));
        }
        [HttpGet]
        [Route("FetchCodesMasterDetails")]
        public ActionResult FetchCodesMasterDetails(string cmCode, string cmType)
        {
            CodesMasterManager objCodesMasterManager = new CodesMasterManager();

            CodesMaster objCodesMaster = new CodesMaster();
            objCodesMaster.CmCode = cmCode;
            objCodesMaster.CmType = cmType;

            DataTable dt = objCodesMasterManager.FetchByCmcode(objCodesMaster);

            return Ok(JsonConvert.SerializeObject(dt));
        }
    }
}
