using Microsoft.AspNetCore.Mvc.Rendering;
using SURVEY_SYSTEM.EntityLayer.Transaction;

namespace SURVEY_SYSTEM_APP.Areas.Transaction.Models
{
    public class MotorClmSurHdrModel
    {
        public MotorClaim motorClaim { get; set; }
        public MotorClmSurHdr motorClmSurHdr { get; set; }
        public MotorClmSurDtl motorClmSurDtl { get; set; }

        public MotorClmSurHdrModel()
        {
            SurdItemCodeList = new List<SelectListItem>();
            SurCurrList = new List<SelectListItem>();
            SurdTypeList = new List<SelectListItem>();
        }


        public List<SelectListItem> SurdItemCodeList { get; set; }
        public List<SelectListItem> SurCurrList { get; set; }
        public List<SelectListItem> SurdTypeList { get; set; }
    }
}
