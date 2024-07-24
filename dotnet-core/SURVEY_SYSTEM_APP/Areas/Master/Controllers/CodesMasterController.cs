using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol;
using SURVEY_SYSTEM.EntityLayer;
using SURVEY_SYSTEM.EntityLayer.Transaction;
using SURVEY_SYSTEM_APP.Areas.Master.Models;
using SURVEY_SYSTEM_APP.Areas.Transaction.Models;
using SURVEY_SYSTEM_APP.Models;
using System.Data;
using static SURVEY_SYSTEM_APP.Filter.AuthorizeFilter;

namespace SURVEY_SYSTEM_APP.Areas.Master.Controllers
{
    [Authorize]
    public class CodesMasterController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly IConfiguration _iConfiguration;

        public CodesMasterController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = httpContextAccessor.HttpContext.Session;
            _iConfiguration = configuration;

        }
        public  IActionResult CodesMasterList()
        {          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetCodesMasterList()
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "CodesMasterAPI/FetchCodesMaster");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(apiResponse);
                    List<CodesMaster> codesMasterList = new List<CodesMaster>();

                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            CodesMaster codes = new CodesMaster
                            {
                                CmCode = row["CM_CODE"].ToString(),
                                CmType = row["CM_TYPE"].ToString(),
                                CmDesc = row["CM_DESC"].ToString(),
                                CmValue = Convert.ToDouble(row["CM_VALUE"]),
                                CmActiveYn = row["CM_ACTIVE_YN"].ToString()
                            };

                            codesMasterList.Add(codes);
                        }

                    }
                    var codesList = new { data = codesMasterList };
                    return Ok(codesList);
                }
                else
                {
                    return BadRequest("Failed to fetch data");
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveCodesMaster(CodesMasterModel codesMasterModel, string mode)
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            using (var client = new HttpClient())
            {
                if (mode == "edit")
                {
                    codesMasterModel.codesMaster.CmUpBy = HttpContext.Session.GetString("USER_ID");
                    codesMasterModel.codesMaster.CmUpDt = DateTime.Now.Date;

                    HttpResponseMessage response2 = await client.PostAsJsonAsync(baseString + "CodesMasterAPI/UpdateCodesMaster", codesMasterModel.codesMaster);
                    string apiresponse2 = await response2.Content.ReadAsStringAsync();

                    if (Convert.ToBoolean(apiresponse2))
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
                        return Redirect("~/Master/CodesMaster/CodesMasterList");
                    }
                    else
                    {
                        return Redirect("error");
                    }
                }
                else
                {
                    HttpResponseMessage response1 = await client.PostAsJsonAsync(baseString + "CodesMasterAPI/CheckDuplicateCodesMaster", codesMasterModel.codesMaster);
                    string apiresponse1 = await response1.Content.ReadAsStringAsync();

                    if (Convert.ToBoolean(apiresponse1))
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
                        return Redirect("~/Master/CodesMaster/CodesMasterList");
                    }
                    else
                    {
                        codesMasterModel.codesMaster.CmCrBy = HttpContext.Session.GetString("USER_ID");
                        codesMasterModel.codesMaster.CmCrDt = DateTime.Now.Date;

                        HttpResponseMessage response2 = await client.PostAsJsonAsync(baseString + "CodesMasterAPI/SaveCodesMaster", codesMasterModel.codesMaster);
                        string apiresponse2 = await response2.Content.ReadAsStringAsync();
                        if (Convert.ToBoolean(apiresponse2))
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
                            return Redirect("~/Master/CodesMaster/CodesMasterList");
                        }
                        else
                        {
                            return Redirect("error");
                        }
                    }

                }

            }
        }
        public async Task<IActionResult> DeleteCodesMaster(string cmCode, string cmType)
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            using (var client = new HttpClient())
            {
                string apiUrl = $"{baseString}CodesMasterAPI/DeleteCodesMaster?cmCode={cmCode}&cmType={cmType}";

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                string apiresponse = await response.Content.ReadAsStringAsync();

                if (Convert.ToBoolean(apiresponse))
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
                    return Redirect("~/Master/CodesMaster/CodesMasterList");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }
        public async Task<IActionResult> GetCodesMasterDetails(string cmCode, string cmType)
        {
            try
            {
                string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(baseString + "CodesMasterAPI/FetchCodesMasterDetails?cmCode=" + cmCode + "&cmType=" + cmType);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        DataTable dtCodesMaster = JsonConvert.DeserializeObject<DataTable>(apiResponse);

                        CodesMaster objCodesMaster = new CodesMaster();

                        if (dtCodesMaster.Rows.Count > 0)
                        {
                            objCodesMaster.CmType = dtCodesMaster.Rows[0]["CM_TYPE"].ToString();
                            objCodesMaster.CmCode = dtCodesMaster.Rows[0]["CM_CODE"].ToString();
                            objCodesMaster.CmDesc = dtCodesMaster.Rows[0]["CM_DESC"].ToString();
                            objCodesMaster.CmActiveYn = dtCodesMaster.Rows[0]["CM_ACTIVE_YN"].ToString();


                        }
                        return Ok(objCodesMaster);
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
