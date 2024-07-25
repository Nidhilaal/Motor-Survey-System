using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SURVEY_SYSTEM.EntityLayer;

namespace SURVEY_SYSTEM_EXT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMasterAPIController : ControllerBase
    {
        private readonly IConfiguration _iConfiguration;
        private readonly HttpClient _client;
        public UserMasterAPIController(IConfiguration configuration)
        {
            _iConfiguration = configuration;
            string baseAddress = _iConfiguration.GetValue<string>("ApiUrl");

            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };

        }
        [HttpGet]
        [Route("FetchUserMaster")]
        public async Task<IActionResult> FetchUserMaster()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("UserMasterAPI/FetchUserMaster");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("SaveUserMaster")]
        public async Task<IActionResult> SaveUserMaster(UserMaster userMaster)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("UserMasterAPI/SaveUserMaster", userMaster);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("UpdateUserMaster")]
        public async Task<IActionResult> UpdateUserMaster(UserMaster userMaster)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("UserMasterAPI/UpdateUserMaster", userMaster);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
    

        [HttpPost]
        [Route("CheckDuplicateUserMaster/{id}")]
        public async Task<IActionResult> CheckDuplicateUserMaster(string id)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsync($"UserMasterAPI/CheckDuplicateUserMaster/{id}", null);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteUserMaster/{id}")]
        public async Task<IActionResult> DeleteUserMaster(string id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"UserMasterAPI/DeleteUserMaster/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
