using Microsoft.AspNetCore.Mvc;
using SURVEY_SYSTEM.EntityLayer;
using SURVEY_SYSTEM_APP.Models;
using System.DirectoryServices.Protocols;

namespace SURVEY_SYSTEM_APP.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly IConfiguration _iConfiguration;

        
        public LoginController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = httpContextAccessor.HttpContext.Session;
            _iConfiguration = configuration;

        }
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }
        public async Task<IActionResult> Fnlogin(LoginModel loginModel)

        {
            string baseString = _iConfiguration.GetSection("Apiconfig").GetSection("BaseString").Value;

            _session.SetString("USER_ID", loginModel.userMaster.UserId);          

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(baseString + "LoginAPI/Validatelogin", loginModel.userMaster);
                string apiresponse = await response.Content.ReadAsStringAsync();

                HttpResponseMessage userTypeResponse = await client.PostAsJsonAsync(baseString + "LoginAPI/GetUserType", loginModel.userMaster);
                string userType = await userTypeResponse.Content.ReadAsStringAsync();

                _session.SetString("USER_TYPE", userType);

                if (Convert.ToBoolean(apiresponse))
                {
                    if (userType == "U")
                    {
                        return Redirect("~/Transaction/Home/Home");
                    }
                    else
                    {
                        return Redirect("~/Transaction/Dashboard/Dashboard");
                    }              
                }
                else
                {
                    TempData["Message1"] = "Login Failed";
                    TempData["Message2"] = "Invalid Credentials";
                    TempData["Message3"] = "error";
                    return RedirectToAction("Index");
                }
            }
        }
    }
}
