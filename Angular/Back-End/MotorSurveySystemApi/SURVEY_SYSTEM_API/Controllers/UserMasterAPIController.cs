using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using SURVEY_SYSTEM.BusinessLayer.Master;
using SURVEY_SYSTEM.EntityLayer;
using System.Data;

namespace SURVEY_SYSTEM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMasterAPIController : ControllerBase
    {
        UserMasterManager objUserMasterManager = new UserMasterManager();

        [HttpGet]
        [Route("FetchUserMaster")]
        public ActionResult FetchUserMaster()
        {
            try
            {
                DataTable dt = objUserMasterManager.FetchAllUserMaster();

                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("SaveUserMaster")]
        public ActionResult SaveUserMaster(UserMaster userMaster)
        {
            try
            {
                return Ok(objUserMasterManager.SaveUserMaster(userMaster));

            }
            catch (Exception)
            {

                throw;
            }        }

        [HttpPost]
        [Route("UpdateUserMaster")]
        public ActionResult UpdateUserMaster(UserMaster userMaster)
        {
            try
            {
                return Ok(objUserMasterManager.UpdateUserMaster(userMaster));

            }
            catch (Exception)
            {

                throw;
            }        
        }

        [HttpPost]
        [Route("CheckDuplicateUserMaster/{id}")]
        public ActionResult CheckDuplicateUserMaster(string id)
        {
            try
            {
                return Ok(objUserMasterManager.CheckDuplicateUserMaster(id));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("DeleteUserMaster/{id}")]
        public ActionResult DeleteUserMaster(string id)
        {

            try
            {
                UserMaster objUserMaster = new UserMaster();
                objUserMaster.UserId = id;

                return Ok(objUserMasterManager.DeleteUserMasterByUserId(objUserMaster)); ;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("FetchUserMasterDetails")]
        public ActionResult FetchUserMasterDetails(string userId)
        {
            try
            {

                UserMaster objUserMaster = new UserMaster();
                objUserMaster.UserId = userId;

                DataTable dt = objUserMasterManager.FetchByUserId(objUserMaster);

                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
