using SURVEY_SYSTEM.EntityLayer.Transaction;

namespace SURVEY_SYSTEM_APP.Areas.Transaction.Models
{
    public class MotorClmSurDtlHistModel
    {
        public MotorClmSurDtlHist motorClmSurDtlHist { get; set; }  
        public MotorClaim motorClaim { get; set; }

        public MotorClmSurHdr motorClmSurHdr { get; set; }
    }
}
