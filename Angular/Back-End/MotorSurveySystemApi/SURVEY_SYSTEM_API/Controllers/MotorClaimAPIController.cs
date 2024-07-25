using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SURVEY_SYSTEM.BusinessLayer;
using SURVEY_SYSTEM.BusinessLayer.Transaction;
using SURVEY_SYSTEM.EntityLayer;
using SURVEY_SYSTEM.EntityLayer.Transaction;
using System.Data;

namespace SURVEY_SYSTEM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorClaimAPIController : ControllerBase
    {
        [HttpGet]
        [Route("FetchMotorClaimList")]
        public ActionResult FetchMotorClaimList()
        {
            try
            {
                MotorClaimManager objMotorClaimManager = new MotorClaimManager();

                DataTable dt = objMotorClaimManager.FetchAllClaim();

                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("CheckDuplicatePolRepNo/{id}")]
        public ActionResult CheckDuplicatePolRepNo(string id)
        {
            try
            {
                MotorClaimManager objMotorClaimManager = new MotorClaimManager();
                return Ok(objMotorClaimManager.CheckDuplicatePolRepNo(id));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("SaveMotorClaim")]
        public ActionResult SaveMotorClaim(MotorClaim objMotorClaim)
        {
            try
            {
                MotorClaimManager objMotorClaimManager = new MotorClaimManager();



                return Ok(objMotorClaimManager.SaveClaim(objMotorClaim));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateMotorClaim")]
        public ActionResult UpdateMotorClaim(MotorClaim objMotorClaim)
        {
            try
            {

                MotorClaimManager objMotorClaimManager = new MotorClaimManager();

                return Ok(objMotorClaimManager.UpdateClaim(objMotorClaim));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateAppovalStatus")]
        public ActionResult UpdateAppovalStatus(MotorClaim objMotorClaim)
        {
            try
            {
                MotorClaimManager objMotorClaimManager = new MotorClaimManager();

                return Ok(objMotorClaimManager.UpdateAppovalStatus(objMotorClaim));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("DeleteMotorClaim")]
        public ActionResult DeleteMotorClaim(int clmUid)
        {

            try
            {
                MotorClaim objMotorClaim = new MotorClaim();
                objMotorClaim.ClmUid = clmUid;
                MotorClaimManager objMotorClaimManager = new MotorClaimManager();

                return Ok(objMotorClaimManager.DeleteClaim(objMotorClaim));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetClaimSequence")]
        public ActionResult GetClaimSequence()
        {
            try
            {
                MotorClaimManager objMotorClaimManager = new MotorClaimManager();

                return Ok(objMotorClaimManager.GetClaimSequence());
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("FetchMotorClaim/{id}")]
        public ActionResult FetchMotorClaim(int id)
        {
            try
            {
                MotorClaimManager objMotorClaimManager = new MotorClaimManager();

                DataTable dt = objMotorClaimManager.FetchByClmUid(id);

                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }
       
    }

}
