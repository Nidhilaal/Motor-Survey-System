using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SURVEY_SYSTEM.EntityLayer;
using SURVEY_SYSTEM_APP.Areas.Master.Models;
using SURVEY_SYSTEM_APP.Areas.Transaction.Models;
using System.Data;
using static SURVEY_SYSTEM_APP.Filter.AuthorizeFilter;

namespace SURVEY_SYSTEM_APP.Areas.Master.Controllers
{
    [Authorize]
    public class ErrorCodesMasterController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly IConfiguration _iConfiguration;

        public ErrorCodesMasterController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = httpContextAccessor.HttpContext.Session;
            _iConfiguration = configuration;

        }
        public async Task<IActionResult> ErrorCodesMasterList()
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;
            ErrorCodesMasterModel errorCodesMasterModel = new ErrorCodesMasterModel();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "CodesMasterAPI/GetCodes/" + "ECM_CODES");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(apiResponse);


                    errorCodesMasterModel.errTypeList.Add(new SelectListItem
                    {
                        Text = "--select--",
                        Value = ""
                    });
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        errorCodesMasterModel.errTypeList.Add(new SelectListItem
                        {
                            Text = dataRow["CM_DISPLAY"].ToString(),
                            Value = dataRow["CM_CODE"].ToString()
                        });
                    }
                }
            }
            return View(errorCodesMasterModel);
        }
        [HttpPost]
        public async Task<IActionResult> GetErrorCodesMasterList()
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "ErrorCodesMasterAPI/FetchErrorCodesMaster");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(apiResponse);
                    List<ErrorCodesMaster> ErrorCodesList = new List<ErrorCodesMaster>();

                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ErrorCodesMaster codes = new ErrorCodesMaster
                            {
                                ErrCode = row["ERR_CODE"].ToString(),
                                ErrType = row["ERR_TYPE"].ToString(),
                                ErrDesc = row["ERR_DESC"].ToString(),
                               
                            };

                            ErrorCodesList.Add(codes);
                        }

                    }
                    var codesList = new { data = ErrorCodesList };
                    return Ok(codesList);
                }
                else
                {
                    return BadRequest("Failed to fetch data");
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveErrorCodesMaster(ErrorCodesMasterModel errorCodesMasterModel, string mode)
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;



            using (var client = new HttpClient())
            {


                if (mode == "edit")
                {
                    errorCodesMasterModel.errorCodesMaster.ErrUpBy = HttpContext.Session.GetString("USER_ID");
                    errorCodesMasterModel.errorCodesMaster.ErrUpDt = DateTime.Now.Date;

                    HttpResponseMessage response2 = await client.PostAsJsonAsync(baseString + "ErrorCodesMasterAPI/UpdateErrorCodesMaster", errorCodesMasterModel.errorCodesMaster);
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
                        return Redirect("~/Master/ErrorCodesMaster/ErrorCodesMasterList");
                    }
                    else
                    {
                        return Redirect("error");
                    }
                }
                else
                {
                    HttpResponseMessage response1 = await client.PostAsJsonAsync(baseString + "ErrorCodesMasterAPI/CheckDuplicateErrorCodesMaster", errorCodesMasterModel.errorCodesMaster);
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
                        return Redirect("~/Master/ErrorCodesMaster/ErrorCodesMasterList");
                    }
                    else
                    {
                        errorCodesMasterModel.errorCodesMaster.ErrCrBy = HttpContext.Session.GetString("USER_ID");
                        errorCodesMasterModel.errorCodesMaster.ErrCrDt = DateTime.Now.Date;

                        HttpResponseMessage response2 = await client.PostAsJsonAsync(baseString + "ErrorCodesMasterAPI/SaveErrorCodesMaster", errorCodesMasterModel.errorCodesMaster);
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
                            return Redirect("~/Master/ErrorCodesMaster/ErrorCodesMasterList");
                        }
                        else
                        {
                            return Redirect("error");
                        }
                    }

                }

            }
        }
        public async Task<IActionResult> DeleteErrorCodesMaster(string errCode)
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            using (var client = new HttpClient())
            {
                string apiUrl = $"{baseString}ErrorCodesMasterAPI/DeleteErrorCodesMaster?errCode={errCode}";

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
                    return Redirect("~/Master/ErrorCodesMaster/ErrorCodesMasterList");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }
    }
}
