using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SURVEY_SYSTEM.EntityLayer;

namespace SURVEY_SYSTEM_EXT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAPIController : ControllerBase
    {
        private readonly IConfiguration _iConfiguration;
        private readonly HttpClient _client;
        public LoginAPIController(IConfiguration configuration)
        {
            _iConfiguration = configuration;
            string baseAddress = _iConfiguration.GetValue<string>("ApiUrl");

            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };

        }

        [HttpPost]
        [Route("Validatelogin")]
        public async Task<IActionResult> Validatelogin(UserMaster objUserMaster)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("LoginAPI/Validatelogin", objUserMaster);
                string apiresponse = await response.Content.ReadAsStringAsync();
                return Ok(apiresponse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("GetUserType")]
        public async Task<IActionResult> GetUserType(UserMaster objUserMaster)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("LoginAPI/GetUserType", objUserMaster);
                string apiresponse = await response.Content.ReadAsStringAsync();
                return Ok(apiresponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
