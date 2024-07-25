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
        [Route("FetchByClmUid/{id}")]
        public ActionResult FetchByClmUid(int id)
        {
            try
            {
                MotorClaim objMotorClaim = new MotorClaim();
                objMotorClaim.ClmUid = id;

                MotorClaimManager objMotorClaimManager = new MotorClaimManager();
                DataTable dtMotorClaim = objMotorClaimManager.FetchByClmUid(objMotorClaim.ClmUid);

                return Ok(JsonConvert.SerializeObject(dtMotorClaim));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("FetchBySurUid/{surUid}")]
        public ActionResult FetchBySurUid(int surUid)
        {
            try
            {
                MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                objMotorClmSurHdr.SurUid = surUid;

                MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();
                DataTable dtSurveyHeader = objMotorClmSurHdrManager.FetchBySurUid(objMotorClmSurHdr);

                return Ok(JsonConvert.SerializeObject(dtSurveyHeader));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("FetchPendingMotorClaim")]
        public ActionResult FetchPendingMotorClaim()
        {
            try
            {
                MotorClaimManager objMotorClaimManager = new MotorClaimManager();

                DataTable dt = objMotorClaimManager.FetchAllClaimForSurveyor();

                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("FetchAllSurveyHistory")]
        public ActionResult FetchAllSurveyHistory()
        {
            try
            {
                MotorClmSurDtlHistManager objMotorClmSurDtlHistManager = new MotorClmSurDtlHistManager();

                DataTable dt = objMotorClmSurDtlHistManager.FetchAllSurveyHistory();

                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("FetchSurveyHeaderList/{userId}")]
        public ActionResult FetchSurveyHeaderList(string userId)
        {
            try
            {
                MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();

                MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                objMotorClmSurHdr.SurCrBy = userId;
                DataTable dt = objMotorClmSurHdrManager.FetchAllSurvey(objMotorClmSurHdr);

                return Ok(JsonConvert.SerializeObject(dt));

            }
            catch (Exception)
            {

                throw;
            }
           

            
        }
        [HttpGet]
        [Route("ConvertFCToLC")]
        public ActionResult ConvertFCToLC(string surCurr, double fcAmount)
        {
            try
            {
                CodesMasterManager objCodesMasterManager = new CodesMasterManager();
                CodesMaster objCodeMaster = new CodesMaster();
                objCodeMaster.CmCode = surCurr;
                objCodeMaster.CmType = "SUR_CURR";

                double conversionRate = objCodesMasterManager.GetCmValue(objCodeMaster);
                double lcAmount = fcAmount * conversionRate;
                return Ok(lcAmount);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("SaveSurveyDetails")]
        public ActionResult SaveSurveyDetails(MotorClmSurDtl objMotorClmSurDtl)
        {

            try
            {
                return Ok(objMotorClmSurDtlManager.SaveSurveyDetails(objMotorClmSurDtl));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("CheckDuplicateSurveyDetails")]
        public ActionResult CheckDuplicateSurveyDetails(MotorClmSurDtl objMotorClmSurDtl)
        {

            try
            {
                return Ok(objMotorClmSurDtlManager.CheckDuplicateSurdUid(objMotorClmSurDtl));

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpPost]
        [Route("UpdateSurveyDetails")]
        public ActionResult UpdateSurveyDetails(MotorClmSurDtl objMotorClmSurDtl)
        {

            try
            {
                return Ok(objMotorClmSurDtlManager.UpdateSurveyDetails(objMotorClmSurDtl));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("SaveSurveyHeader")]
        public ActionResult SaveSurveyHeader(MotorClmSurHdr objMotorClmSurHdr)
        {
            try
            {
                MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();

                return Ok(objMotorClmSurHdrManager.SaveSurveyHeader(objMotorClmSurHdr));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("SubmitSurveyHeader")]
        public ActionResult SubmitSurveyHeader(MotorClmSurHdr objMotorClmSurHdr)
        {
            try
            {
                MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();


                bool result;



                result = objMotorClmSurHdrManager.UpdateSurveyHeader(objMotorClmSurHdr);

                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("FetchSurveyDetailsList/{id}")]
        public ActionResult FetchSurveyDetailsList(int id)
        {
            try
            {
                MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
                objMotorClmSurDtl.SurdSurUid = id;

                DataTable dt = objMotorClmSurDtlManager.FetchBySurdSurUid(objMotorClmSurDtl);

                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("FetchSurveyDetails")]
        public ActionResult FetchSurveyDetails(int surdUid)
        {
            try
            {
                MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
                objMotorClmSurDtl.SurdUid = surdUid;

                DataTable dt = objMotorClmSurDtlManager.FetchBySurdUid(objMotorClmSurDtl);

                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }
        

        [HttpPost]
        [Route("UpdateSurNo")]
        public ActionResult UpdateSurNo(MotorClaim objMotorCLaim)
        {
            try
            {
                MotorClaimManager objMotorClaimManager = new MotorClaimManager();
                bool result;

                if (objMotorClaimManager.UpdateSurNo(objMotorCLaim))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("DeleteSurveyDetails/{id}")]
        public ActionResult DeleteSurveyDetails(int id)
        {
            try
            {

                MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
                objMotorClmSurDtl.SurdUid = id;

                return Ok(objMotorClmSurDtlManager.DeleteSurveyDetails(objMotorClmSurDtl));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetSurUidSequence")]
        public ActionResult GetSurUidSequence()
        {
            try
            {
                MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();

                return Ok(objMotorClmSurHdrManager.GetSurUidSequence());
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetSurdUidSequence")]
        public ActionResult GetSurdUidSequence()
        {
            try
            {
                MotorClmSurDtlManager objMotorClmSurDtlManager = new MotorClmSurDtlManager();

                return Ok(objMotorClmSurDtlManager.GetSurdUidSequence());
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("FetchBySurClmUid/{id}")]
        public ActionResult FetchBySurClmUid(int id)
        {

            try
            {
                MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();
                DataTable dt = objMotorClmSurHdrManager.FetchBySurClmUid(id);
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateSurveyStatus")]
        public ActionResult UpdateSurveyStatus(MotorClmSurHdr objMotorClmSurHdr)
        {
            try
            {
                MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();

                return Ok(objMotorClmSurHdrManager.UpdateSurveyStatus(objMotorClmSurHdr));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateSurveyCreated")]
        public ActionResult UpdateSurveyCreated(MotorClaim objMotorCLaim)
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }

    }
}
