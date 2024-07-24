using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SURVEY_SYSTEM.BusinessLayer.Transaction;
using SURVEY_SYSTEM.EntityLayer.Transaction;
using SURVEY_SYSTEM_APP.Areas.Transaction.Models;
using System.Data;
using System.DirectoryServices.Protocols;
using System.Reflection;
using System.Web;
using static SURVEY_SYSTEM_APP.Filter.AuthorizeFilter;

namespace SURVEY_SYSTEM_APP.Areas.Transaction.Controllers
{
    [Authorize]
    public class MotorClaimController : Controller
    {
        MotorClaimManager objMotorClaimManager = new MotorClaimManager();
        MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly IConfiguration _iConfiguration;

        public MotorClaimController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = httpContextAccessor.HttpContext.Session;
            _iConfiguration = configuration;

        }
        public IActionResult MotorClaimList()
        {
            return View();
        }
        public IActionResult ViewSurvey(int? id)
        {

            MotorClmSurHdrModel motorClmSurHdrModel = new MotorClmSurHdrModel();

            if (id.HasValue)
            {
                MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                MotorClaim objMotorClaim = new MotorClaim();
                objMotorClmSurHdr.SurclmUid = Convert.ToInt32(id);

                DataTable dtSurveyHeader = objMotorClmSurHdrManager.FetchBySurClmUid(objMotorClmSurHdr);

                if (dtSurveyHeader.Rows.Count > 0)
                {
                    objMotorClmSurHdr.SurUid = Convert.ToInt32(dtSurveyHeader.Rows[0]["SUR_UID"]);
                    objMotorClmSurHdr.SurNo = dtSurveyHeader.Rows[0]["SUR_NO"].ToString();
                    objMotorClmSurHdr.SurclmNo = dtSurveyHeader.Rows[0]["SUR_CLM_NO"].ToString();
                    objMotorClmSurHdr.SurChassisNo = dtSurveyHeader.Rows[0]["SUR_CHASSIS_NO"].ToString();
                    objMotorClmSurHdr.SurEngineNo = dtSurveyHeader.Rows[0]["SUR_ENGINE_NO"].ToString();
                    objMotorClmSurHdr.SurRegnNo = dtSurveyHeader.Rows[0]["SUR_REGN_NO"].ToString();
                    objMotorClmSurHdr.SurCurr = dtSurveyHeader.Rows[0]["SUR_CURR"].ToString();
                    objMotorClmSurHdr.SurStatus = dtSurveyHeader.Rows[0]["SUR_STATUS"].ToString();
                    objMotorClmSurHdr.SurApprSts = dtSurveyHeader.Rows[0]["SUR_APPR_STS"].ToString();
                    objMotorClmSurHdr.SurFcAmt = Convert.ToDouble(dtSurveyHeader.Rows[0]["SUR_FC_AMT"]);
                    objMotorClmSurHdr.SurLcAmt = Convert.ToDouble(dtSurveyHeader.Rows[0]["SUR_LC_AMT"]);

                    objMotorClaim.ClmPolNo = dtSurveyHeader.Rows[0]["SUR_CLM_NO"].ToString();

                    motorClmSurHdrModel.motorClmSurHdr = objMotorClmSurHdr;
                    motorClmSurHdrModel.motorClaim = objMotorClaim;
                    return View(motorClmSurHdrModel);
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            else
            {
                return View();
            }
        }

        public IActionResult AddMotorClaim(int? id)
        {
            MotorClaimModel motorClaimModel = new MotorClaimModel();

            if (id.HasValue)
            {
                MotorClaim objMotorClaim = new MotorClaim();
                objMotorClaim.ClmUid = id.Value;

                DataTable dtMotorClaim = objMotorClaimManager.FetchByClmUid(objMotorClaim);

                if (dtMotorClaim.Rows.Count > 0)
                {
                    objMotorClaim.ClmUid = Convert.ToInt32(dtMotorClaim.Rows[0]["CLM_UID"]);
                    objMotorClaim.ClmNo = dtMotorClaim.Rows[0]["CLM_NO"].ToString();
                    objMotorClaim.ClmPolNo = dtMotorClaim.Rows[0]["CLM_POL_NO"].ToString();
                    objMotorClaim.ClmPolFmDt = (DateTime)dtMotorClaim.Rows[0]["CLM_POL_FM_DT"];
                    objMotorClaim.ClmPolToDt = (DateTime)dtMotorClaim.Rows[0]["CLM_POL_TO_DT"];
                    objMotorClaim.ClmLossDt = (DateTime)dtMotorClaim.Rows[0]["CLM_LOSS_DT"];
                    objMotorClaim.ClmIntmDt = (DateTime)dtMotorClaim.Rows[0]["CLM_INTM_DT"];
                    objMotorClaim.ClmRegDt = (DateTime)dtMotorClaim.Rows[0]["CLM_REG_DT"];
                    objMotorClaim.ClmPolRepNo = dtMotorClaim.Rows[0]["CLM_POL_REP_NO"].ToString();
                    objMotorClaim.ClmLossDesc = dtMotorClaim.Rows[0]["CLM_LOSS_DESC"].ToString();
                    objMotorClaim.ClmVehChassisNo = dtMotorClaim.Rows[0]["CLM_VEH_CHASSIS_NO"].ToString();
                    objMotorClaim.ClmVehEngineNo = dtMotorClaim.Rows[0]["CLM_VEH_ENGINE_NO"].ToString();
                    objMotorClaim.ClmVehRegnNo = dtMotorClaim.Rows[0]["CLM_VEH_REGN_NO"].ToString();
                    objMotorClaim.ClmVehValue = Convert.ToDouble(dtMotorClaim.Rows[0]["CLM_VEH_VALUE"]);
                    objMotorClaim.ClmSurCrYn = dtMotorClaim.Rows[0]["CLM_SUR_CR_YN"].ToString();
                    objMotorClaim.ClmSurNo = dtMotorClaim.Rows[0]["CLM_SUR_NO"].ToString();

                    motorClaimModel.motorClaim = objMotorClaim;
                    return View(motorClaimModel);
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            else
            {
                return View();
            }

        }
        public async Task<IActionResult> DeleteMotorClaim(int? id)
        {
            MotorClaimModel motorClaimModel = new MotorClaimModel();

            if (id.HasValue)
            {
                string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(baseString + "MotorClaimAPI/DeleteMotorClaim?clmUid=" + id);

                    if (response.IsSuccessStatusCode)
                    {
                        string errorcode = "103";
                        HttpResponseMessage errorResponse = await client.GetAsync(baseString + "ErrorCodesMasterAPI/GetErrorCodes/" + errorcode);
                        string errorMessage = await errorResponse.Content.ReadAsStringAsync();

                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(errorMessage);

                        if (dt != null)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                TempData["Message1"] = row["ERR_TYPE"].ToString();
                                TempData["Message2"] = row["ERR_DESC"].ToString();
                                TempData["Message3"] = "success";
                            }
                        }
                        return Redirect("~/Transaction/MotorClaim/MotorClaimList");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            else
            {
                return View("Error");
            }
        }
        public async Task<IActionResult> UpdateApprovalstatus(MotorClmSurHdrModel motorClmSurHdrModel, string approvalStatus)
        {
            MotorClaim objMotorClaim = new MotorClaim();
            MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();

            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            using (var client = new HttpClient())
            {
                if (approvalStatus == "A")
                {
                    objMotorClaim.ClmSurApprYn = "Y";
                    objMotorClaim.ClmNo = motorClmSurHdrModel.motorClaim.ClmNo;

                    objMotorClmSurHdr.SurApprSts = "A";
                    objMotorClmSurHdr.SurApprBy = HttpContext.Session.GetString("USER_ID");
                    objMotorClmSurHdr.SurApprDt = DateTime.Now.Date;
                    objMotorClmSurHdr.SurUid = motorClmSurHdrModel.motorClmSurHdr.SurUid;

                    HttpResponseMessage response1 = await client.PostAsJsonAsync(baseString + "MotorClaimAPI/UpdateSurveyStatus", objMotorClaim);
                    HttpResponseMessage response2 = await client.PostAsJsonAsync(baseString + "MotorClaimAPI/UpdateAppovalStatus", objMotorClmSurHdr);

                    string errorcode = "105";
                    HttpResponseMessage errorResponse = await client.GetAsync(baseString + "ErrorCodesMasterAPI/GetErrorCodes/" + errorcode);
                    string errorMessage = await errorResponse.Content.ReadAsStringAsync();

                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(errorMessage);

                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            TempData["Message1"] = row["ERR_TYPE"].ToString();
                            TempData["Message2"] = row["ERR_DESC"].ToString();
                            TempData["Message3"] = "success";
                        }
                    }

                }
                else if (approvalStatus == "R")
                {
                    objMotorClaim.ClmSurApprYn = "N";
                    objMotorClaim.ClmNo = motorClmSurHdrModel.motorClaim.ClmNo;

                    objMotorClmSurHdr.SurApprSts = "R";
                    objMotorClmSurHdr.SurApprBy = HttpContext.Session.GetString("USER_ID");
                    objMotorClmSurHdr.SurApprDt = DateTime.Now.Date;
                    objMotorClmSurHdr.SurUid = motorClmSurHdrModel.motorClmSurHdr.SurUid;

                    objMotorClmSurHdrManager.UpdateSurveyStatus(objMotorClmSurHdr);
                    objMotorClaimManager.UpdateAppovalStatus(objMotorClaim);

                    string errorcode = "302";
                    HttpResponseMessage errorResponse = await client.GetAsync(baseString + "ErrorCodesMasterAPI/GetErrorCodes/" + errorcode);
                    string errorMessage = await errorResponse.Content.ReadAsStringAsync();

                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(errorMessage);

                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            TempData["Message1"] = row["ERR_TYPE"].ToString();
                            TempData["Message2"] = row["ERR_DESC"].ToString();
                            TempData["Message3"] = "error";
                        }
                    }
                }
                return Redirect("~/Transaction/MotorClaim/ViewSurvey?surNo=" + motorClmSurHdrModel.motorClmSurHdr.SurNo);
            }
        }
        [HttpPost]

        public async Task<IActionResult> GetMotorClaimList()
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "MotorClaimAPI/FetchMotorClaim");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(apiResponse);

                    List<MotorClaim> motorClaimList = new List<MotorClaim>();
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            MotorClaim codes = new()
                            {
                                ClmUid = Convert.ToInt32(row["CLM_UID"]),
                                ClmNo = row["CLM_NO"].ToString(),
                                ClmPolFmDt = Convert.ToDateTime(row["CLM_POL_FM_DT"]).Date,
                                ClmPolToDt = Convert.ToDateTime(row["CLM_POL_TO_DT"]).Date,
                                ClmPolRepNo = row["CLM_POL_REP_NO"].ToString(),
                                ClmVehChassisNo = row["CLM_VEH_CHASSIS_NO"].ToString(),
                                ClmVehValue = Convert.ToDouble(row["CLM_VEH_VALUE"]),
                                ClmSurCrYn = row["CLM_SUR_CR_YN"].ToString(),
                                ClmSurApprYn = row["CLM_SUR_APPR_YN"].ToString()
                            };
                            motorClaimList.Add(codes);
                        }
                    }
                    var ClaimList = new { data = motorClaimList };
                    return Ok(ClaimList);
                }
                else
                {
                    return BadRequest("Failed to fetch data");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckUniquePoliceRepNo(string clmPolRepNo, int clmUid)
        {
            try
            {
                string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;
                using (var client = new HttpClient())
                {
                    if (clmUid != 0)
                    {
                        MotorClaim motorClaim = new MotorClaim();
                        motorClaim.ClmPolRepNo = clmPolRepNo;


                        HttpResponseMessage response = await client.PostAsJsonAsync(baseString + "MotorClaimAPI/CheckDuplicatePolRepNo?clmUid=" + clmUid, motorClaim);

                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            return Ok(Convert.ToBoolean(apiResponse));
                        }
                        else
                        {
                            return Ok("error");
                        }
                    }
                    else
                    {
                        MotorClaim motorClaim = new MotorClaim();
                        motorClaim.ClmPolRepNo = clmPolRepNo;

                        HttpResponseMessage response = await client.PostAsJsonAsync(baseString + "MotorClaimAPI/CheckDuplicatePolRepNo", motorClaim);

                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            return Ok(Convert.ToBoolean(apiResponse));
                        }
                        else
                        {
                            return Ok("error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok("error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveMotorClaim(MotorClaimModel motorClaimModel)
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;
            using (var client = new HttpClient())
            {
                if (motorClaimModel.motorClaim.ClmUid == 0)
                {
                    motorClaimModel.motorClaim.ClmCrBy = HttpContext.Session.GetString("USER_ID");
                    motorClaimModel.motorClaim.ClmCrDt = DateTime.Now.Date;

                    motorClaimModel.motorClaim.ClmUid = objMotorClaimManager.GetClaimSequence();
                    motorClaimModel.motorClaim.ClmNo = "C/" + motorClaimModel.motorClaim.ClmPolFmDt.Year.ToString()
                       + "/" + motorClaimModel.motorClaim.ClmUid.ToString().PadLeft(6, '0');

                    HttpResponseMessage response1 = await client.PostAsJsonAsync(baseString + "MotorClaimAPI/SaveMotorClaim", motorClaimModel.motorClaim);
                    string apiresponse1 = await response1.Content.ReadAsStringAsync();

                    if (Convert.ToBoolean(apiresponse1))
                    {
                        string errorcode = "101";
                        HttpResponseMessage errorResponse = await client.GetAsync(baseString + "ErrorCodesMasterAPI/GetErrorCodes/" + errorcode);
                        string errorMessage = await errorResponse.Content.ReadAsStringAsync();

                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(errorMessage);

                        if (dt != null)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                TempData["Message1"] = row["ERR_TYPE"].ToString();
                                TempData["Message2"] = row["ERR_DESC"].ToString();
                                TempData["Message3"] = "success";
                            }
                        }
                        return Redirect("~/Transaction/MotorClaim/AddMotorClaim/" + motorClaimModel.motorClaim.ClmUid);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    motorClaimModel.motorClaim.ClmUpBy = HttpContext.Session.GetString("USER_ID");
                    motorClaimModel.motorClaim.ClmUpDt = DateTime.Now.Date;

                    HttpResponseMessage response1 = await client.PostAsJsonAsync(baseString + "MotorClaimAPI/UpdateMotorClaim", motorClaimModel.motorClaim);
                    string apiresponse1 = await response1.Content.ReadAsStringAsync();

                    if (Convert.ToBoolean(apiresponse1))
                    {
                        string errorcode = "102";
                        HttpResponseMessage errorResponse = await client.GetAsync(baseString + "ErrorCodesMasterAPI/GetErrorCodes/" + errorcode);
                        string errorMessage = await errorResponse.Content.ReadAsStringAsync();

                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(errorMessage);

                        if (dt != null)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                TempData["Message1"] = row["ERR_TYPE"].ToString();
                                TempData["Message2"] = row["ERR_DESC"].ToString();
                                TempData["Message3"] = "success";
                            }
                        }
                        return Redirect("~/Transaction/MotorClaim/AddMotorClaim/" + motorClaimModel.motorClaim.ClmUid);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
        }
    }
}
