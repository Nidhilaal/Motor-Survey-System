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
            try
            {
                CodesMasterManager objCodesMasterManager = new CodesMasterManager();
                DataTable dt = objCodesMasterManager.FetchAllCodesMaster();

                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("SaveCodesMaster")]
        public ActionResult SaveCodesMaster(CodesMaster codesMaster)
        {
            try
            {
                CodesMasterManager objCodesMasterManager = new CodesMasterManager();
                return Ok(objCodesMasterManager.SaveCodesMaster(codesMaster));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("UpdateCodesMaster")]
        public ActionResult UpdateCodesMaster(CodesMaster codesMaster)
        {
            try
            {
                CodesMasterManager objCodesMasterManager = new CodesMasterManager();
                return Ok(objCodesMasterManager.UpdateCodesMaster(codesMaster));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("CheckDuplicateCodesMaster")]
        public ActionResult CheckDuplicateCodesMaster(CodesMaster codesMaster)
        {
            try
            {
                CodesMasterManager objCodesMasterManager = new CodesMasterManager();
                return Ok(objCodesMasterManager.CheckDuplicateCodesMaster(codesMaster));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetCodes/{id}")]
        public ActionResult GetCodes(string id)
        {
            try
            {
                CodesMaster objCodeMaster = new CodesMaster();
                objCodeMaster.CmType = id;
                CodesMasterManager objCodesMasterManager = new CodesMasterManager();
                DataTable dt = objCodesMasterManager.FillDropDownList(objCodeMaster);

                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("DeleteCodesMaster")]
        public ActionResult DeleteCodesMaster(string cmCode, string cmType)
        {
            try
            {
                CodesMasterManager objCodesMasterManager = new CodesMasterManager();

                CodesMaster objCodesMaster = new CodesMaster();
                objCodesMaster.CmCode = cmCode;
                objCodesMaster.CmType = cmType;

                return Ok(objCodesMasterManager.DeleteCodesMaster(objCodesMaster));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("FetchCodesMasterDetails")]
        public ActionResult FetchCodesMasterDetails(string cmCode, string cmType)
        {
            try
            {
                CodesMasterManager objCodesMasterManager = new CodesMasterManager();

                CodesMaster objCodesMaster = new CodesMaster();
                objCodesMaster.CmCode = cmCode;
                objCodesMaster.CmType = cmType;

                DataTable dt = objCodesMasterManager.FetchByCmcode(objCodesMaster);

                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
