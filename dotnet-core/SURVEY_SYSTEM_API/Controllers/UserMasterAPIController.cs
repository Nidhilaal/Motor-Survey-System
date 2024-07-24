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
            DataTable dt = objUserMasterManager.FetchAllUserMaster();

            return Ok(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        [Route("SaveUserMaster")]
        public ActionResult SaveUserMaster(UserMaster userMaster)
        {
            return Ok(objUserMasterManager.SaveUserMaster(userMaster));
        }

        [HttpPost]
        [Route("UpdateUserMaster")]
        public ActionResult UpdateUserMaster(UserMaster userMaster)
        {
            return Ok(objUserMasterManager.UpdateUserMaster(userMaster));
        }

        [HttpPost]
        [Route("CheckDuplicateUserMaster")]
        public ActionResult CheckDuplicateUserMaster(UserMaster userMaster)
        {
            return Ok(objUserMasterManager.CheckDuplicateUserMaster(userMaster));
        }

        

        [HttpGet]
        [Route("DeleteUserMaster")]
        public ActionResult DeleteUserMaster(string userId)
        {

            UserMaster objUserMaster = new UserMaster();
            objUserMaster.UserId = userId;

            return Ok(objUserMasterManager.DeleteUserMasterByUserId(objUserMaster));;
        }
        [HttpGet]
        [Route("FetchUserMasterDetails")]
        public ActionResult FetchUserMasterDetails(string userId)
        {

            UserMaster objUserMaster = new UserMaster();
            objUserMaster.UserId = userId;

            DataTable dt = objUserMasterManager.FetchByUserId(objUserMaster);

            return Ok(JsonConvert.SerializeObject(dt));
        }
    }
}
