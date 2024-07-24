using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using SURVEY_SYSTEM.EntityLayer.Transaction;
using SURVEY_SYSTEM_APP.Areas.Transaction.Models;
using System.Data;
using SURVEY_SYSTEM.BusinessLayer.Transaction;
using System.Net.Http.Json;
using System.DirectoryServices.Protocols;
using SURVEY_SYSTEM.EntityLayer;
using static SURVEY_SYSTEM_APP.Filter.AuthorizeFilter;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SURVEY_SYSTEM_APP.Areas.Transaction.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        MotorClmSurDtlManager objMotorClmSurDtlManager = new MotorClmSurDtlManager();
        MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();


        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly IConfiguration _iConfiguration;

        public SurveyController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = httpContextAccessor.HttpContext.Session;
            _iConfiguration = configuration;

        }
        public async Task<IActionResult> AddSurvey(string id, int? id1)
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;
            MotorClmSurHdrModel motorClmSurHdrModel = new MotorClmSurHdrModel();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "CodesMasterAPI/GetCodes/" + "SURD_ITEM_CODE");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(apiResponse);
                    motorClmSurHdrModel.SurdItemCodeList.Add(new SelectListItem
                    {
                        Text = "--select--",
                        Value = ""
                    });
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        motorClmSurHdrModel.SurdItemCodeList.Add(new SelectListItem
                        {
                            Text = dataRow["CM_DISPLAY"].ToString(),
                            Value = dataRow["CM_CODE"].ToString()
                        });
                    }
                }
            }
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "CodesMasterAPI/GetCodes/" + "SURD_TYPE");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(apiResponse);
                    motorClmSurHdrModel.SurdTypeList.Add(new SelectListItem
                    {
                        Text = "--select--",
                        Value = ""
                    });
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        motorClmSurHdrModel.SurdTypeList.Add(new SelectListItem
                        {
                            Text = dataRow["CM_DISPLAY"].ToString(),
                            Value = dataRow["CM_CODE"].ToString()
                        });
                    }
                }
            }

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "CodesMasterAPI/GetCodes/" + "SUR_CURR");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(apiResponse);
                    motorClmSurHdrModel.SurCurrList.Add(new SelectListItem
                    {
                        Text = "--select--",
                        Value = ""
                    });
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        motorClmSurHdrModel.SurCurrList.Add(new SelectListItem
                        {
                            Text = dataRow["CM_DISPLAY"].ToString(),
                            Value = dataRow["CM_CODE"].ToString()
                        });
                    }
                }
            }
            if (id =="C")
            {
                MotorClaim objMotorClaim = new MotorClaim();
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(baseString + "SurveyAPI/FetchByClmUid?clmUid=" + id1);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        DataTable dtMotorClaim = JsonConvert.DeserializeObject<DataTable>(apiResponse);

                        if (dtMotorClaim.Rows.Count > 0)
                        {
                            objMotorClaim.ClmUid = Convert.ToInt32(dtMotorClaim.Rows[0]["CLM_UID"]);
                            objMotorClaim.ClmNo = dtMotorClaim.Rows[0]["CLM_NO"].ToString();
                            objMotorClaim.ClmVehChassisNo = dtMotorClaim.Rows[0]["CLM_VEH_CHASSIS_NO"].ToString();
                            objMotorClaim.ClmVehEngineNo = dtMotorClaim.Rows[0]["CLM_VEH_ENGINE_NO"].ToString();
                            objMotorClaim.ClmVehRegnNo = dtMotorClaim.Rows[0]["CLM_VEH_REGN_NO"].ToString();
                        }
                    }
                }

                motorClmSurHdrModel.motorClaim = objMotorClaim;

                return View(motorClmSurHdrModel);

            }else if (id=="S")
            {
                MotorClaim objMotorClaim = new MotorClaim();
                MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(baseString + "SurveyAPI/FetchBySurUid?surUid=" + id1);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        DataTable dtSurveyHeader = JsonConvert.DeserializeObject<DataTable>(apiResponse);

                        if (dtSurveyHeader.Rows.Count > 0)
                        {
                            objMotorClmSurHdr.SurUid = Convert.ToInt32(dtSurveyHeader.Rows[0]["SUR_UID"]);
                            objMotorClmSurHdr.SurNo = dtSurveyHeader.Rows[0]["SUR_NO"].ToString();
                            objMotorClmSurHdr.SurclmNo = dtSurveyHeader.Rows[0]["SUR_CLM_NO"].ToString();
                            objMotorClmSurHdr.SurclmUid = Convert.ToInt32(dtSurveyHeader.Rows[0]["SUR_CLM_UID"]);
                            objMotorClaim.ClmUid = Convert.ToInt32(dtSurveyHeader.Rows[0]["SUR_CLM_UID"]);
                            objMotorClmSurHdr.SurChassisNo = dtSurveyHeader.Rows[0]["SUR_CHASSIS_NO"].ToString();
                            objMotorClmSurHdr.SurEngineNo = dtSurveyHeader.Rows[0]["SUR_ENGINE_NO"].ToString();
                            objMotorClmSurHdr.SurRegnNo = dtSurveyHeader.Rows[0]["SUR_REGN_NO"].ToString();
                            objMotorClmSurHdr.SurCurr = dtSurveyHeader.Rows[0]["SUR_CURR"].ToString();
                            objMotorClmSurHdr.SurStatus = dtSurveyHeader.Rows[0]["SUR_STATUS"].ToString();

                            if (dtSurveyHeader.Rows[0]["SUR_FC_AMT"] == DBNull.Value && dtSurveyHeader.Rows[0]["SUR_LC_AMT"] == DBNull.Value)
                            {
                                objMotorClmSurHdr.SurFcAmt = 0;
                                objMotorClmSurHdr.SurLcAmt = 0;
                            }
                            else
                            {
                                objMotorClmSurHdr.SurFcAmt = Convert.ToDouble(dtSurveyHeader.Rows[0]["SUR_FC_AMT"]);
                                objMotorClmSurHdr.SurLcAmt = Convert.ToDouble(dtSurveyHeader.Rows[0]["SUR_LC_AMT"]);
                            }

                        }
                    }
                }

                motorClmSurHdrModel.motorClmSurHdr = objMotorClmSurHdr;
                motorClmSurHdrModel.motorClaim = objMotorClaim;

                return View(motorClmSurHdrModel);
            }
          
            else
            {
                return View(motorClmSurHdrModel);
            }
        }
      
        public IActionResult PendingMotorClaimList()
        {
            return View();
        }
        public IActionResult SurveyList()
        {
            return View();
        }
        public IActionResult SurveyHistory()

        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetSurveyHistory()
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "SurveyAPI/FetchAllSurveyHistory");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(apiResponse);

                    var motorClmSurDtlHistList = new List<MotorClmSurDtlHistModel>();
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            MotorClmSurDtlHistModel motorClmSurDtlHistModel = new MotorClmSurDtlHistModel();

                            MotorClmSurDtlHist motorClmSurDtlHist = new()
                            {
                                SurdhSrl = Convert.ToInt32(row["SURDH_SRL"]),
                                SurdhAction = row["SURDH_ACTION"].ToString(),                                                       
                                SurdhItemCode = row["SURDH_ITEM_CODE"].ToString(),
                                SurdhQuantity = Convert.ToInt32(row["SURDH_QUANTITY"]),
                                SurdhType = row["SURDH_TYPE"].ToString(),
                                SurdhUnitPrice = Convert.ToDouble(row["SURDH_UNIT_PRICE"]),
                                SurdhFcAmt = Convert.ToDouble(row["SURDH_FC_AMT"]),
                                SurdhLcAmt = Convert.ToDouble(row["SURDH_LC_AMT"])
                            };

                            MotorClaim motorClaim = new()
                            {
                                ClmNo = row["CLM_NO"].ToString()
                            };

                            MotorClmSurHdr motorClmSurHdr = new()
                            {
                                SurCurr = row["SUR_CURR"].ToString(),
                                SurNo = row["SUR_NO"].ToString()
                            };
                            motorClmSurDtlHistModel.motorClaim = motorClaim;
                            motorClmSurDtlHistModel.motorClmSurHdr = motorClmSurHdr;
                            motorClmSurDtlHistModel.motorClmSurDtlHist = motorClmSurDtlHist;


                            motorClmSurDtlHistList.Add(motorClmSurDtlHistModel);
                        }
                    }
                    var ClaimList = new { data = motorClmSurDtlHistList };
                    return Ok(ClaimList);
                }
                else
                {
                    return BadRequest("Failed to fetch data");
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetPendingMotorClaimList()
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "SurveyAPI/FetchPendingMotorClaim");

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

                                ClmVehChassisNo = row["CLM_VEH_CHASSIS_NO"].ToString(),
                                ClmVehValue = Convert.ToDouble(row["CLM_VEH_VALUE"]),
                                ClmSurCrYn = row["CLM_SUR_CR_YN"].ToString()

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
        public async Task<IActionResult> GetSurveyHeaderList()
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;
            string userId = HttpContext.Session.GetString("USER_ID");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "SurveyAPI/FetchSurveyHeaderList?userId=" + userId);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(apiResponse);

                    List<MotorClmSurHdr> SurveyHeaderList = new List<MotorClmSurHdr>();
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            MotorClmSurHdr motorClmSurHdr = new()
                            {
                                SurUid = Convert.ToInt32(row["SUR_UID"]),
                                SurclmNo = row["SUR_CLM_NO"].ToString(),
                                SurNo = row["SUR_NO"].ToString(),
                                SurChassisNo = row["SUR_CHASSIS_NO"].ToString(),
                                SurRegnNo = row["SUR_REGN_NO"].ToString(),
                                SurEngineNo = row["SUR_ENGINE_NO"].ToString(),
                                SurStatus = row["SUR_STATUS"].ToString(),
                                SurApprSts = row["SUR_APPR_STS"].ToString()

                            };
                            SurveyHeaderList.Add(motorClmSurHdr);
                        }
                    }
                    var SurveyList = new { data = SurveyHeaderList };
                    return Ok(SurveyList);
                }
                else
                {
                    return BadRequest("Failed to fetch data");
                }
            }
        }
        public async Task<IActionResult> GetConversionRate(string surCurr, double fcAmount)
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "SurveyAPI/ConvertFCToLC?surCurr=" + surCurr + "&fcAmount=" + fcAmount);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return Ok(Convert.ToDouble(apiResponse));
                }
                else
                {
                    return BadRequest("Failed to fetch data");
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveSurveyHeader(MotorClmSurHdrModel motorClmSurHdrModel)
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;
            using (var client = new HttpClient())
            {
                if (motorClmSurHdrModel.motorClmSurHdr.SurUid == 0)
                {

                    motorClmSurHdrModel.motorClmSurHdr.SurCrBy = HttpContext.Session.GetString("USER_ID");
                    motorClmSurHdrModel.motorClmSurHdr.SurCrDt = DateTime.Now.Date;

                    motorClmSurHdrModel.motorClmSurHdr.SurUid = objMotorClmSurHdrManager.GetSurUidSequence();
                    motorClmSurHdrModel.motorClmSurHdr.SurNo = "S/" + DateTime.Now.Year
                      + "/" + motorClmSurHdrModel.motorClmSurHdr.SurUid.ToString().PadLeft(6, '0');

                    motorClmSurHdrModel.motorClmSurHdr.SurclmNo = motorClmSurHdrModel.motorClaim.ClmNo;
                    motorClmSurHdrModel.motorClmSurHdr.SurclmUid = motorClmSurHdrModel.motorClaim.ClmUid;

                    motorClmSurHdrModel.motorClmSurHdr.SurChassisNo = motorClmSurHdrModel.motorClaim.ClmVehChassisNo;
                    motorClmSurHdrModel.motorClmSurHdr.SurRegnNo = motorClmSurHdrModel.motorClaim.ClmVehRegnNo;
                    motorClmSurHdrModel.motorClmSurHdr.SurEngineNo = motorClmSurHdrModel.motorClaim.ClmVehEngineNo;

                    motorClmSurHdrModel.motorClaim.ClmSurCrYn = "Y";

                    await client.PostAsJsonAsync(baseString + "SurveyAPI/UpdateSurveyCreated", motorClmSurHdrModel.motorClaim);

                    HttpResponseMessage response2 = await client.PostAsJsonAsync(baseString + "SurveyAPI/SaveSurveyHeader", motorClmSurHdrModel.motorClmSurHdr);

                    string apiresponse = await response2.Content.ReadAsStringAsync();

                    if (Convert.ToBoolean(apiresponse))
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
                        return Redirect("~/Transaction/Survey/AddSurvey/S/" + motorClmSurHdrModel.motorClmSurHdr.SurUid);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {

                    motorClmSurHdrModel.motorClmSurHdr.SurUpBy = HttpContext.Session.GetString("USER_ID");
                    motorClmSurHdrModel.motorClmSurHdr.SurUpDt = DateTime.Now.Date;
                    motorClmSurHdrModel.motorClmSurHdr.SurStatus = "S";    
                    
                    motorClmSurHdrModel.motorClaim.ClmSurNo = motorClmSurHdrModel.motorClmSurHdr.SurNo;

                    await client.PostAsJsonAsync(baseString + "SurveyAPI/UpdateSurNo", motorClmSurHdrModel.motorClaim);

                    HttpResponseMessage response = await client.PostAsJsonAsync(baseString + "SurveyAPI/SubmitSurveyHeader", motorClmSurHdrModel.motorClmSurHdr);

                    string apiresponse = await response.Content.ReadAsStringAsync();

                    if (Convert.ToBoolean(apiresponse))
                    {
                        string errorcode = "104";
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
                        return Redirect("~/Transaction/Survey/AddSurvey/S/" + motorClmSurHdrModel.motorClmSurHdr.SurUid);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveSurveyDetails(MotorClmSurHdrModel motorClmSurHdrModel)
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;
            using (var client = new HttpClient())
            {
                if (Convert.ToInt32(motorClmSurHdrModel.motorClmSurDtl.SurdUid) == 0)
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(baseString + "SurveyAPI/CheckDuplicateSurveyDetails", motorClmSurHdrModel.motorClmSurDtl);
                    string apiresponse = await response.Content.ReadAsStringAsync();

                    if (Convert.ToBoolean(apiresponse))
                    {
                        string errorcode = "201";
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
                        return Redirect("~/Transaction/Survey/AddSurvey/S/" + motorClmSurHdrModel.motorClmSurDtl.SurdSurUid);
                    }
                    else
                    {
                        motorClmSurHdrModel.motorClmSurDtl.SurdCrBy = HttpContext.Session.GetString("USER_ID");
                        motorClmSurHdrModel.motorClmSurDtl.SurdCrDt = DateTime.Now.Date;

                        motorClmSurHdrModel.motorClmSurDtl.SurdUid = objMotorClmSurDtlManager.GetSurdUidSequence();


                        HttpResponseMessage response1 = await client.PostAsJsonAsync(baseString + "SurveyAPI/SaveSurveyDetails", motorClmSurHdrModel.motorClmSurDtl);

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
                            return Redirect("~/Transaction/Survey/AddSurvey/S/" + motorClmSurHdrModel.motorClmSurDtl.SurdSurUid);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }

                }
                else
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(baseString + "SurveyAPI/CheckDuplicateSurveyDetails?surdUid=" + motorClmSurHdrModel.motorClmSurDtl.SurdUid, motorClmSurHdrModel.motorClmSurDtl);
                    string apiresponse = await response.Content.ReadAsStringAsync();

                    if (Convert.ToBoolean(apiresponse))
                    {
                        string errorcode = "201";
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

                        return Redirect("~/Transaction/Survey/AddSurvey/S/" + motorClmSurHdrModel.motorClmSurDtl.SurdSurUid);
                    }
                    else
                    {
                        motorClmSurHdrModel.motorClmSurDtl.SurdUpBy = HttpContext.Session.GetString("USER_ID");
                        motorClmSurHdrModel.motorClmSurDtl.SurdUpDt = DateTime.Now.Date;

                        HttpResponseMessage response1 = await client.PostAsJsonAsync(baseString + "SurveyAPI/UpdateSurveyDetails?surdUid=" + motorClmSurHdrModel.motorClmSurDtl.SurdUid, motorClmSurHdrModel.motorClmSurDtl);

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
                            return Redirect("~/Transaction/Survey/AddSurvey/S/" + motorClmSurHdrModel.motorClmSurDtl.SurdSurUid);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
        }
        public async Task<IActionResult> GetSurveyDetailsList(int? surdSurUid)
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "SurveyAPI/FetchSurveyDetailsList?surdSurUid=" + surdSurUid);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(apiResponse);

                    List<MotorClmSurDtl> SurveyDetailsList = new List<MotorClmSurDtl>();

                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            MotorClmSurDtl details = new()
                            {
                                SurdUid = Convert.ToInt32(row["SURD_UID"]),
                                SurdItemCode = row["SURD_ITEM_CODE"].ToString(),
                                SurdType = row["SURD_TYPE"].ToString(),
                                SurdUnitPrice = Convert.ToDouble(row["SURD_UNIT_PRICE"]),
                                SurdQuantity = Convert.ToInt32(row["SURD_QUANTITY"]),
                                SurdFcAmt = Convert.ToDouble(row["SURD_FC_AMT"]),
                                SurdLcAmt = Convert.ToDouble(row["SURD_LC_AMT"])

                            };
                            SurveyDetailsList.Add(details);
                        }
                    }
                    var ClaimList = new { data = SurveyDetailsList };
                    return Ok(ClaimList);
                }
                else
                {
                    return BadRequest("Failed to fetch data");
                }
            }
        }
        public async Task<IActionResult> DeleteSurveyDetails(int? surdUid, int? surdSurUid)
        {

            if (surdUid.HasValue)
            {
                string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(baseString + "SurveyAPI/DeleteSurveyDetails?surdUid=" + surdUid);

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
                        return Redirect("~/Transaction/Survey/AddSurvey/S/" + surdSurUid);
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
        public async Task<IActionResult> GetSurveyDetails(int? surdUid)
        {
            try
            {
                string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(baseString + "SurveyAPI/FetchSurveyDetails?surdUid=" + surdUid);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        DataTable dtSurveyDetails = JsonConvert.DeserializeObject<DataTable>(apiResponse);


                        MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();

                        if (dtSurveyDetails.Rows.Count > 0)
                        {
                            objMotorClmSurDtl.SurdUid = Convert.ToInt32(dtSurveyDetails.Rows[0]["SURD_UID"]);
                            objMotorClmSurDtl.SurdSurUid = Convert.ToInt32(dtSurveyDetails.Rows[0]["SURD_SUR_UID"]);
                            objMotorClmSurDtl.SurdItemCode = dtSurveyDetails.Rows[0]["SURD_ITEM_CODE"].ToString();
                            objMotorClmSurDtl.SurdType = dtSurveyDetails.Rows[0]["SURD_TYPE"].ToString();
                            objMotorClmSurDtl.SurdQuantity = Convert.ToInt32(dtSurveyDetails.Rows[0]["SURD_UNIT_PRICE"]);
                            objMotorClmSurDtl.SurdUnitPrice = Convert.ToDouble(dtSurveyDetails.Rows[0]["SURD_QUANTITY"]);
                            objMotorClmSurDtl.SurdFcAmt = Convert.ToDouble(dtSurveyDetails.Rows[0]["SURD_FC_AMT"]);
                            objMotorClmSurDtl.SurdLcAmt = Convert.ToDouble(dtSurveyDetails.Rows[0]["SURD_LC_AMT"]);

                        }
                        return Ok(objMotorClmSurDtl);
                    }
                    else
                    {
                        return BadRequest("Failed to fetch survey details");
                    }
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error");
            }
        }

    }
}
