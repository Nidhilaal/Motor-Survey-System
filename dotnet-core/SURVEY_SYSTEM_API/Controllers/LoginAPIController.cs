using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SURVEY_SYSTEM.BusinessLayer.Master;
using SURVEY_SYSTEM.EntityLayer;

namespace SURVEY_SYSTEM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAPIController : ControllerBase
    {
        [HttpPost]
        [Route("Validatelogin")]
        public ActionResult Validatelogin (UserMaster objUserMaster)
        {
            UserMasterManager objUserMasterManager = new UserMasterManager();
            return Ok(objUserMasterManager.GetAuthentication(objUserMaster));

        }

        [HttpPost]
        [Route("GetUserType")]
        public ActionResult GetUserType(UserMaster objUserMaster)
        {
            UserMasterManager objUserMasterManager = new UserMasterManager();
            return Ok(objUserMasterManager.CheckUserType(objUserMaster));

        }
    }
}
