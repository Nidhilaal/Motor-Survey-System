using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SURVEY_SYSTEM.EntityLayer.Transaction;
using System.Data;

namespace SURVEY_SYSTEM_EXT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyAPIController : ControllerBase
    {
        private readonly IConfiguration _iConfiguration;
        private readonly HttpClient _client;
        public SurveyAPIController(IConfiguration configuration)
        {
            _iConfiguration = configuration;
            string baseAddress = _iConfiguration.GetValue<string>("ApiUrl");

            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };

        }
        [HttpGet]
        [Route("FetchAllSurveyHistory")]
        public async Task<IActionResult> FetchAllSurveyHistory()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("SurveyAPI/FetchAllSurveyHistory");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        [Route("FetchByClmUid/{id}")]
        public async Task<IActionResult> FetchByClmUid(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"SurveyAPI/FetchByClmUid/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }        
        }

        [HttpGet]
        [Route("FetchBySurUid/{id}")]
        public async Task<IActionResult> FetchBySurUid(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"SurveyAPI/FetchBySurUid/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("SaveSurveyHeader")]
        public async Task<IActionResult> SaveSurveyHeader(MotorClmSurHdr objMotorClmSurHdr)
        {
            try
            {

                HttpResponseMessage response = await _client.PostAsJsonAsync($"SurveyAPI/SaveSurveyHeader", objMotorClmSurHdr);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateSurveyHeader")]
        public async Task<IActionResult> UpdateSurveyHeader(MotorClmSurHdr objMotorClmSurHdr)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync($"SurveyAPI/SubmitSurveyHeader", objMotorClmSurHdr);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateSurveyStatus")]
        public async Task<IActionResult> UpdateSurveyStatus(MotorClmSurHdr objMotorClmSurHdr)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync($"SurveyAPI/UpdateSurveyStatus", objMotorClmSurHdr);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
      
        [HttpGet]
        [Route("FetchSurveyDetailsList/{id}")]
        public async Task<IActionResult>  FetchSurveyDetailsList(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"SurveyAPI/FetchSurveyDetailsList/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetSurUidSequence")]
        public async Task<IActionResult> GetSurUidSequence()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"SurveyAPI/GetSurUidSequence");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("FetchSurveyHeaderList/{id}")]
        public async Task<IActionResult> FetchSurveyHeaderList(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"SurveyAPI/FetchSurveyHeaderList/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("SaveSurveyDetails")]
        public async Task<IActionResult> SaveSurveyDetails(MotorClmSurDtl objMotorClmSurDtl)
        {

            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync($"SurveyAPI/SaveSurveyDetails", objMotorClmSurDtl);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateSurveyDetails")]
        public async Task<IActionResult> UpdateSurveyDetails(MotorClmSurDtl objMotorClmSurDtl)
        {

            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync($"SurveyAPI/UpdateSurveyDetails", objMotorClmSurDtl);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);

    }
            catch (Exception)
            {

                throw;
            };
        }
        [HttpGet]
        [Route("GetSurdUidSequence")]
        public async Task<IActionResult> GetSurdUidSequence()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"SurveyAPI/GetSurdUidSequence");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteSurveyDetails/{surdUid}")]
        public async Task<IActionResult> DeleteSurveyDetails(int surdUid)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"SurveyAPI/DeleteSurveyDetails/{surdUid}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("FetchBySurClmUid/{id}")]
        public async Task<IActionResult> FetchBySurClmUid(int id)
        {

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"SurveyAPI/FetchBySurClmUid/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("CheckDuplicateSurveyDetails")]
        public async Task<IActionResult> CheckDuplicateSurveyDetails(MotorClmSurDtl objMotorClmSurDtl)
        {
            try
            {

                HttpResponseMessage response = await _client.PostAsJsonAsync($"SurveyAPI/CheckDuplicateSurveyDetails", objMotorClmSurDtl);
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
        public async Task<IActionResult> UpdateSurveyCreated(MotorClaim objMotorCLaim)
        {

            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync($"SurveyAPI/UpdateSurveyCreated", objMotorCLaim);
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
