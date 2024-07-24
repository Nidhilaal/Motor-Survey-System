using Microsoft.AspNetCore.Mvc.Rendering;
using SURVEY_SYSTEM.EntityLayer;

namespace SURVEY_SYSTEM_APP.Areas.Master.Models
{
    public class UserMasterModel
    {
        public UserMaster userMaster { get; set; }
        public UserMasterModel()
        {
            userTypeList = new List<SelectListItem>();

        }
        public List<SelectListItem> userTypeList { get; set; }
    }
}
