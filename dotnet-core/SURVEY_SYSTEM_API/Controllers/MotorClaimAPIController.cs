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
        [Route("FetchMotorClaim")]
        public ActionResult FetchMotorClaim()
        {
            MotorClaimManager objMotorClaimManager = new MotorClaimManager();

            DataTable dt = objMotorClaimManager.FetchAllClaim();

            return Ok(JsonConvert.SerializeObject(dt));
        }


        [HttpPost]
        [Route("CheckDuplicatePolRepNo")]
        public ActionResult CheckDuplicatePolRepNo(MotorClaim objMotorClaim,int? clmUid)
        {
            MotorClaimManager objMotorClaimManager = new MotorClaimManager();

            if (clmUid.HasValue)
            {
                return Ok(objMotorClaimManager.CheckDuplicatePolRepNo(objMotorClaim, clmUid));

            }
            else
            {
                return Ok(objMotorClaimManager.CheckDuplicatePolRepNo(objMotorClaim));

            }
        }

        [HttpPost]
        [Route("SaveMotorClaim")]
        public ActionResult SaveMotorClaim(MotorClaim objMotorClaim)
        {
            MotorClaimManager objMotorClaimManager = new MotorClaimManager();

            
     
            return Ok(objMotorClaimManager.SaveClaim(objMotorClaim));
        }
        [HttpPost]
        [Route("UpdateMotorClaim")]
        public ActionResult UpdateMotorClaim(MotorClaim objMotorClaim)
        {
            MotorClaimManager objMotorClaimManager = new MotorClaimManager();

            return Ok(objMotorClaimManager.UpdateClaim(objMotorClaim));
        }
        [HttpPost]
        [Route("UpdateSurveyStatus")]
        public ActionResult UpdateSurveyStatus(MotorClaim objMotorClaim)
        {
            MotorClaimManager objMotorClaimManager = new MotorClaimManager();

            return Ok(objMotorClaimManager.UpdateSurveyCreated(objMotorClaim));
        }
        [HttpPost]
        [Route("UpdateAppovalStatus")]
        public ActionResult UpdateAppovalStatus(MotorClmSurHdr objMotorClmSurHdr)
        {
            MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();

            return Ok(objMotorClmSurHdrManager.UpdateSurveyStatus(objMotorClmSurHdr));
        }
        [HttpGet]
        [Route("DeleteMotorClaim")]
        public ActionResult DeleteMotorClaim(int clmUid)
        {

            MotorClaim objMotorClaim = new MotorClaim();
            objMotorClaim.ClmUid = clmUid;
            MotorClaimManager objMotorClaimManager = new MotorClaimManager();
            
            return Ok(objMotorClaimManager.DeleteClaim(objMotorClaim));
        }
    }

}
