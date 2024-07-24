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
    public class SurveyAPIController : ControllerBase
    {
        MotorClmSurDtlManager objMotorClmSurDtlManager = new MotorClmSurDtlManager();

        [HttpGet]
        [Route("FetchByClmUid")]
        public ActionResult FetchByClmUid(int clmUid)
        {
            MotorClaim objMotorClaim = new MotorClaim();
            objMotorClaim.ClmUid = clmUid;

            MotorClaimManager objMotorClaimManager = new MotorClaimManager();
            DataTable dtMotorClaim = objMotorClaimManager.FetchByClmUid(objMotorClaim);

            return Ok(JsonConvert.SerializeObject(dtMotorClaim));
        }
        [HttpGet]
        [Route("FetchBySurUid")]
        public ActionResult FetchBySurUid(int surUid)
        {
            MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
            objMotorClmSurHdr.SurUid = surUid;

            MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();
            DataTable dtSurveyHeader = objMotorClmSurHdrManager.FetchBySurUid(objMotorClmSurHdr);

            return Ok(JsonConvert.SerializeObject(dtSurveyHeader));
        }
        [HttpGet]
        [Route("FetchPendingMotorClaim")]
        public ActionResult FetchPendingMotorClaim()
        {
            MotorClaimManager objMotorClaimManager = new MotorClaimManager();

            DataTable dt = objMotorClaimManager.FetchAllClaimForSurveyor();

            return Ok(JsonConvert.SerializeObject(dt));
        }
        [HttpGet]
        [Route("FetchAllSurveyHistory")]
        public ActionResult FetchAllSurveyHistory()
        {
            MotorClmSurDtlHistManager objMotorClmSurDtlHistManager = new MotorClmSurDtlHistManager();

            DataTable dt = objMotorClmSurDtlHistManager.FetchAllSurveyHistory();

            return Ok(JsonConvert.SerializeObject(dt));
        }
        [HttpGet]
        [Route("FetchSurveyHeaderList")]
        public ActionResult FetchSurveyHeaderList(string userId)
        {
            MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();

            MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
            objMotorClmSurHdr.SurCrBy = userId;
            DataTable dt = objMotorClmSurHdrManager.FetchAllSurvey(objMotorClmSurHdr);

            return Ok(JsonConvert.SerializeObject(dt));

           

            
        }
        [HttpGet]
        [Route("ConvertFCToLC")]
        public ActionResult ConvertFCToLC(string surCurr, double fcAmount)
        {
            CodesMasterManager objCodesMasterManager = new CodesMasterManager();
            CodesMaster objCodeMaster = new CodesMaster();
            objCodeMaster.CmCode = surCurr;
            objCodeMaster.CmType = "SUR_CURR";

            double conversionRate = objCodesMasterManager.GetCmValue(objCodeMaster);
            double lcAmount = fcAmount * conversionRate;
            return Ok(lcAmount);
        }
        [HttpPost]
        [Route("SaveSurveyDetails")]
        public ActionResult SaveSurveyDetails(MotorClmSurDtl objMotorClmSurDtl)
        {
  
            return Ok(objMotorClmSurDtlManager.SaveSurveyDetails(objMotorClmSurDtl));
        }

        [HttpPost]
        [Route("CheckDuplicateSurveyDetails")]
        public ActionResult CheckDuplicateSurveyDetails(MotorClmSurDtl objMotorClmSurDtl, int? surdUid)
        {
            if (surdUid.HasValue)
            {
                return Ok(objMotorClmSurDtlManager.CheckDuplicateSurdUid(objMotorClmSurDtl, surdUid));

            }
            else
            {
                return Ok(objMotorClmSurDtlManager.CheckDuplicateSurdUid(objMotorClmSurDtl));

            }
        }

        [HttpPost]
        [Route("UpdateSurveyDetails")]
        public ActionResult UpdateSurveyDetails(MotorClmSurDtl objMotorClmSurDtl,int? surdUid)
        {
            
            return Ok(objMotorClmSurDtlManager.UpdateSurveyDetails(objMotorClmSurDtl));
        }
        [HttpPost]
        [Route("SaveSurveyHeader")]
        public ActionResult SaveSurveyHeader(MotorClmSurHdr objMotorClmSurHdr)
        {
            MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();

            return Ok(objMotorClmSurHdrManager.SaveSurveyHeader(objMotorClmSurHdr));
        }
        [HttpPost]
        [Route("SubmitSurveyHeader")]
        public ActionResult SubmitSurveyHeader(MotorClmSurHdr objMotorClmSurHdr)
        {
            MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();


            bool result;



            result = objMotorClmSurHdrManager.UpdateSurveyHeader(objMotorClmSurHdr);

            return Ok(result);
        }
        [HttpGet]
        [Route("FetchSurveyDetailsList")]
        public ActionResult FetchSurveyDetailsList(int surdSurUid)
        {
            MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
            objMotorClmSurDtl.SurdSurUid = surdSurUid;

            DataTable dt = objMotorClmSurDtlManager.FetchBySurdSurUid(objMotorClmSurDtl);

            return Ok(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        [Route("FetchSurveyDetails")]
        public ActionResult FetchSurveyDetails(int surdUid)
        {
            MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
            objMotorClmSurDtl.SurdUid = surdUid;

            DataTable dt = objMotorClmSurDtlManager.FetchBySurdUid(objMotorClmSurDtl);

            return Ok(JsonConvert.SerializeObject(dt));
        }
        [HttpPost]
        [Route("UpdateSurveyCreated")]
        public ActionResult UpdateSurveyCreated(MotorClaim objMotorCLaim)
        {
            MotorClaimManager objMotorClaimManager = new MotorClaimManager();
            bool result;

            if (objMotorClaimManager.UpdateSurveyCreated(objMotorCLaim) && objMotorClaimManager.UpdateSurNo(objMotorCLaim))
            {
                result = true;
            }
            else
            {
                result = false;
            }       
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateSurNo")]
        public ActionResult UpdateSurNo(MotorClaim objMotorCLaim)
        {
            MotorClaimManager objMotorClaimManager = new MotorClaimManager();
            bool result;

            if ( objMotorClaimManager.UpdateSurNo(objMotorCLaim))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("DeleteSurveyDetails")]
        public ActionResult DeleteSurveyDetails(int surdUid)
        {

            MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
            objMotorClmSurDtl.SurdUid = surdUid;

            return Ok(objMotorClmSurDtlManager.DeleteSurveyDetails(objMotorClmSurDtl));
        }

    }
}
