using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SURVEY_SYSTEM.EntityLayer;
using System.Data;
using System.Net.Http.Json;
using System.Reflection.Emit;

namespace SURVEY_SYSTEM_EXT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodesMasterAPIController : ControllerBase
    {
        private readonly IConfiguration _iConfiguration;
        private readonly HttpClient _client;
        public CodesMasterAPIController(IConfiguration configuration)
        {
            _iConfiguration = configuration;
            string baseAddress = _iConfiguration.GetValue<string>("ApiUrl");

            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };

        }
        
        [HttpGet]
        [Route("FetchCodesMaster")]
        public async Task<IActionResult> FetchCodesMaster()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("CodesMasterAPI/FetchCodesMaster");
                string apiResponse = await response.Content.ReadAsStringAsync();

                return Ok(apiResponse);
            }
            catch (Exception )
            {

                throw ;
            }

        }

        [HttpPost]
        [Route("SaveCodesMaster")]
        public async Task<IActionResult> SaveCodesMaster(CodesMaster codesMaster)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("CodesMasterAPI/SaveCodesMaster", codesMaster);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("UpdateCodesMaster")]
        public async Task<IActionResult> UpdateCodesMaster(CodesMaster codesMaster)
        {
            try
            {

                HttpResponseMessage response = await _client.PostAsJsonAsync("CodesMasterAPI/UpdateCodesMaster", codesMaster);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("CheckDuplicateCodesMaster")]
        public async Task<IActionResult> CheckDuplicateCodesMaster(CodesMaster codesMaster)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("CodesMasterAPI/CheckDuplicateCodesMaster", codesMaster);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetCodes/{id}")]
        public async Task<IActionResult> GetCodes(string id)
        {
            try
            {

                HttpResponseMessage response = await _client.GetAsync("CodesMasterAPI/GetCodes/" + id);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteCodesMaster")]
        public async Task<IActionResult> DeleteCodesMaster(string cmCode, string cmType)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("CodesMasterAPI/DeleteCodesMaster?cmCode=" + cmCode + "&cmType=" + cmType);
                string apiResponse = await response.Content.ReadAsStringAsync();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("FetchCodesMasterDetails")]
        public async Task<IActionResult> FetchCodesMasterDetails(string cmCode, string cmType)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("CodesMasterAPI/FetchCodesMasterDetails?cmCode=" + cmCode + "&cmType=" + cmType);
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
