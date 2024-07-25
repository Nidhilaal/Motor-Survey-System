using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SURVEY_SYSTEM.EntityLayer.Transaction;
using System.Data;

namespace SURVEY_SYSTEM_EXT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorClaimAPIController : ControllerBase
    {
        private readonly IConfiguration _iConfiguration;
        private readonly HttpClient _client;
        public MotorClaimAPIController(IConfiguration configuration)
        {
            _iConfiguration = configuration;
            string baseAddress = _iConfiguration.GetValue<string>("ApiUrl");

            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };

        }
        [HttpGet]
        [Route("FetchMotorClaimList")]
        public async Task<IActionResult> FetchMotorClaim()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("MotorClaimAPI/FetchMotorClaimList");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteMotorClaim")]
        public async Task<IActionResult> DeleteMotorClaim(int id)
        {

            try
            {
                HttpResponseMessage response = await _client.GetAsync("MotorClaimAPI/DeleteMotorClaim?clmUid=" + id);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("GetClaimSequence")]
        public async Task<IActionResult> GetClaimSequence()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("MotorClaimAPI/GetClaimSequence");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        [Route("FetchMotorClaim/{id}")]
        public async Task<IActionResult> FetchMotorClaim(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"MotorClaimAPI/FetchMotorClaim/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("CheckDuplicatePolRepNo/{id}")]
        public async Task<IActionResult> CheckDuplicatePolRepNo(string id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"MotorClaimAPI/CheckDuplicatePolRepNo/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        [Route("SaveMotorClaim")]
        public async Task<IActionResult> SaveMotorClaim(MotorClaim objMotorClaim)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("MotorClaimAPI/SaveMotorClaim", objMotorClaim);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateMotorClaim")]
        public async Task<IActionResult> UpdateMotorClaim(MotorClaim objMotorClaim)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("MotorClaimAPI/UpdateMotorClaim", objMotorClaim);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
  
        [HttpPost]
        [Route("UpdateAppovalStatus")]
        public async Task<IActionResult> UpdateAppovalStatus(MotorClaim objMotorClaim)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("MotorClaimAPI/UpdateAppovalStatus", objMotorClaim);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateSurveyCreated")]
        public async Task<IActionResult> UpdateSurveyCreated(MotorClaim objMotorClaim)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("MotorClaimAPI/UpdateSurveyCreated", objMotorClaim);
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
