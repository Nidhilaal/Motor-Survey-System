using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SURVEY_SYSTEM.EntityLayer;
using SURVEY_SYSTEM_APP.Areas.Master.Models;
using System.Data;
using static SURVEY_SYSTEM_APP.Filter.AuthorizeFilter;

namespace SURVEY_SYSTEM_APP.Areas.Master.Controllers
{
    [Authorize]
    public class UserMasterController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly IConfiguration _iConfiguration;

        public UserMasterController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = httpContextAccessor.HttpContext.Session;
            _iConfiguration = configuration;

        }
        public async Task<IActionResult> UserMasterList()
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;
            UserMasterModel userMasterModel = new UserMasterModel();
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "CodesMasterAPI/GetCodes/" + "USER_TYPE");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(apiResponse);

                   

                    userMasterModel.userTypeList.Add(new SelectListItem
                    {
                        Text = "--select--",
                        Value = ""
                    });
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        userMasterModel.userTypeList.Add(new SelectListItem
                        {
                            Text = dataRow["CM_DISPLAY"].ToString(),
                            Value = dataRow["CM_CODE"].ToString()
                        });
                    }
                }
            }
            return View(userMasterModel);
        }
        [HttpPost]
        public async Task<IActionResult> GetUserMasterList()
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseString + "UserMasterAPI/FetchUserMaster");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(apiResponse);
                    List<UserMaster> UserMasterList = new List<UserMaster>();

                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            UserMaster codes = new UserMaster
                            {
                                UserId = row["USER_ID"].ToString(),
                                UserName = row["USER_NAME"].ToString(),
                                UserPassword = row["USER_PASSWORD"].ToString(),
                                UserType = row["USER_TYPE"].ToString(),
                              
                            };

                            UserMasterList.Add(codes);
                        }

                    }
                    var codesList = new { data = UserMasterList };
                    return Ok(codesList);
                }
                else
                {
                    return BadRequest("Failed to fetch data");
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveUserMaster(UserMasterModel userMasterModel, string mode)
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;



            using (var client = new HttpClient())
            {


                if (mode == "edit")
                {
                    userMasterModel.userMaster.UserUpBy = HttpContext.Session.GetString("USER_ID");
                    userMasterModel.userMaster.UserUpDt = DateTime.Now.Date;

                    HttpResponseMessage response2 = await client.PostAsJsonAsync(baseString + "UserMasterAPI/UpdateUserMaster", userMasterModel.userMaster);
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
                        return Redirect("~/Master/UserMaster/UserMasterList");
                    }
                    else
                    {
                        return Redirect("error");
                    }
                }
                else
                {
                    HttpResponseMessage response1 = await client.PostAsJsonAsync(baseString + "UserMasterAPI/CheckDuplicateUserMaster", userMasterModel.userMaster);

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
                                TempData["Message3"] = "success";
                            }
                        }
                        return Redirect("~/Master/UserMaster/UserMasterList");
                    }
                    else
                    {
                        userMasterModel.userMaster.UserCrBy = HttpContext.Session.GetString("USER_ID");
                        userMasterModel.userMaster.UserCrDt = DateTime.Now.Date;

                        HttpResponseMessage response2 = await client.PostAsJsonAsync(baseString + "userMasterAPI/SaveUserMaster", userMasterModel.userMaster);
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
                            return Redirect("~/Master/UserMaster/UserMasterList");
                        }
                        else
                        {
                            return Redirect("error");
                        }
                    }

                }

            }
        }
        public async Task<IActionResult> DeleteUserMaster(string userId)
        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            using (var client = new HttpClient())
            {
                string apiUrl = $"{baseString}UserMasterAPI/DeleteUserMaster?userId={userId}";

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
                    return Redirect("~/Master/UserMaster/UserMasterList");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }
    }
}
