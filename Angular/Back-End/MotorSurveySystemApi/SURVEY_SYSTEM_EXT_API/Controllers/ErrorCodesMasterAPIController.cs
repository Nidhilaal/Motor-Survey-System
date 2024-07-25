using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SURVEY_SYSTEM.EntityLayer;
using System.Data;
using System.Net.Http.Json;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace SURVEY_SYSTEM_EXT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorCodesMasterAPIController : ControllerBase
    {
        private readonly IConfiguration _iConfiguration;
        private readonly HttpClient _client;
        public ErrorCodesMasterAPIController(IConfiguration configuration)
        {
            _iConfiguration = configuration;
            string baseAddress = _iConfiguration.GetValue<string>("ApiUrl");

            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };

        }
        [HttpGet]
        [Route("GetErrorCodes/{id}")]
        public async Task<IActionResult> GetErrorCodes(string id)
        {

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"ErrorCodesMasterAPI/GetErrorCodes/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("FetchErrorCodesMaster")]
        public async Task<IActionResult> FetchErrorCodesMaster()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("ErrorCodesMasterAPI/FetchErrorCodesMaster");
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        [Route("SaveErrorCodesMaster")]
        public async Task<IActionResult> SaveErrorCodesMaster(ErrorCodesMaster objErrorCodesMaster)
        {

            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("ErrorCodesMasterAPI/SaveErrorCodesMaster", objErrorCodesMaster);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("UpdateErrorCodesMaster")]
        public async Task<IActionResult> UpdateErrorCodesMaster(ErrorCodesMaster objErrorCodesMaster)
        {

            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("ErrorCodesMasterAPI/UpdateErrorCodesMaster", objErrorCodesMaster);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("CheckDuplicateErrorCodesMaster/{id}")]
        public async Task<IActionResult> CheckDuplicateErrorCodesMaster(string id)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsync($"ErrorCodesMasterAPI/CheckDuplicateErrorCodesMaster/{id}", null);
                string apiResponse = await response.Content.ReadAsStringAsync();

                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteErrorCodesMaster/{id}")]
        public async Task<IActionResult> DeleteErrorCodesMaster(string id)
        {
            try
            {

                HttpResponseMessage response = await _client.GetAsync($"ErrorCodesMasterAPI/DeleteErrorCodesMaster/{id}");
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
