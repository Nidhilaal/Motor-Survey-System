using Microsoft.AspNetCore.Mvc.Rendering;
using SURVEY_SYSTEM.EntityLayer;

namespace SURVEY_SYSTEM_APP.Areas.Master.Models
{
    public class ErrorCodesMasterModel
    {
        public ErrorCodesMaster errorCodesMaster { get; set; }
        public ErrorCodesMasterModel()
        {
            errTypeList = new List<SelectListItem>();

        }
        public List<SelectListItem> errTypeList { get; set; }
    }
}
