using SURVEY_SYSTEM.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURVEY_SYSTEM.BusinessLayer.Transaction
{
    public class MotorClmSurDtlHistManager
    {
        public DataTable FetchAllSurveyHistory()
        {
            try
            {
                string query = $"SELECT SURDH_UID, SURDH_SRL, SURDH_ACTION, SURDH_SUR_UID, " +
                    $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = SURDH_ITEM_CODE AND CM_TYPE = 'SURD_ITEM_CODE') AS SURDH_ITEM_CODE, " +
                    $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = SURDH_TYPE AND CM_TYPE = 'SURD_TYPE') AS SURDH_TYPE, " +
                    $"SURDH_UNIT_PRICE, SURDH_QUANTITY, SURDH_FC_AMT, SURDH_LC_AMT, " +
                    $"MOTOR_CLM_SUR_HDR.SUR_NO, MOTOR_CLM_SUR_HDR.SUR_CURR, MOTOR_CLAIM.CLM_NO " +
                    $"FROM MOTOR_CLM_SUR_DTL_HIST " +
                    $"LEFT JOIN MOTOR_CLM_SUR_HDR ON MOTOR_CLM_SUR_DTL_HIST.SURDH_SUR_UID = MOTOR_CLM_SUR_HDR.SUR_UID " +
                    $"LEFT JOIN MOTOR_CLAIM ON MOTOR_CLAIM.CLM_SUR_NO = MOTOR_CLM_SUR_HDR.SUR_NO " +
                    $"ORDER BY SURDH_UID DESC, SURDH_SRL DESC";
                DataTable dt = DbConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
