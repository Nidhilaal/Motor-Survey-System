using Microsoft.AspNetCore.Mvc.Rendering;
using SURVEY_SYSTEM.EntityLayer;

namespace SURVEY_SYSTEM_APP.Areas.Master.Models
{
    public class CodesMasterModel
    {
        public CodesMaster codesMaster { get; set; }
        public bool IsCmActiveYn
        {
            get => codesMaster.CmActiveYn == "Y";
            set => codesMaster.CmActiveYn = value ? "Y" : "N";
        }
        //public CodesMasterModel()
        //{
        //    cmTypeList = new List<SelectListItem>();
            
        //}
        //public List<SelectListItem> cmTypeList { get; set; }
    }
}
