using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ClaimManager
    {
        public bool SaveClaim(Claim objClaim)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pClmUid"] = objClaim.ClmUid,
                    ["pClmNo"] = objClaim.ClmNo,
                    ["pClmPolNo"] = objClaim.ClmPolNo,
                    ["pClmPolFmDt"] = objClaim.ClmPolFmDt,
                    ["pClmPolToDt"] = objClaim.ClmPolToDt,
                    ["pClmLossDt"] = objClaim.ClmLossDt,
                    ["pClmIntmDt"] = objClaim.ClmIntmDt,
                    ["pClmRegDt"] = objClaim.ClmRegDt,
                    ["pClmPolRepNo"] = objClaim.ClmPolRepNo,
                    ["pClmLossDesc"] = objClaim.ClmLossDesc,
                    ["pClmVehChassisNo"] = objClaim.ClmVehChassisNo,
                    ["pClmVehEngineNo"] = objClaim.ClmVehEngineNo,
                    ["pClmVehRegnNo"] = objClaim.ClmVehRegnNo,
                    ["pClmVehValue"] = objClaim.ClmVehValue,
                    ["pClmCrBy"] = objClaim.ClmCrBy,
                    ["pClmCrDt"] = objClaim.ClmCrDt
                };

                string query = $"INSERT INTO MOTOR_CLAIM (CLM_UID, CLM_NO, CLM_POL_NO, CLM_POL_FM_DT, CLM_POL_TO_DT, " +
                    $"CLM_LOSS_DT, CLM_INTM_DT, CLM_REG_DT, CLM_POL_REP_NO, CLM_LOSS_DESC, CLM_VEH_CHASSIS_NO, CLM_VEH_ENGINE_NO, " +
                    $"CLM_VEH_REGN_NO, CLM_VEH_VALUE, CLM_CR_BY, CLM_CR_DT)" +
                $"VALUES (:pClmUid, :pClmNo, :pClmPolNo, :pClmPolFmDt, :pClmPolToDt, :pClmLossDt, :pClmIntmDt, :pClmRegDt, :pClmPolRepNo," +
                $":pClmLossDesc, :pClmVehChassisNo, :pClmVehEngineNo, :pClmVehRegnNo, :pClmVehValue, :pClmCrBy, :pClmCrDt)";

                if (Dbconnection.ExecuteQuery(paramValues, query) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool UpdateClaim(Claim objClaim)
        {
            try
            {

                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pClmUid"] = objClaim.ClmUid,
                    ["pClmPolNo"] = objClaim.ClmPolNo,
                    ["pClmPolFmDt"] = objClaim.ClmPolFmDt,
                    ["pClmPolToDt"] = objClaim.ClmPolToDt,
                    ["pClmLossDt"] = objClaim.ClmLossDt,
                    ["pClmIntmDt"] = objClaim.ClmIntmDt,
                    ["pClmRegDt"] = objClaim.ClmRegDt,
                    ["pClmPolRepNo"] = objClaim.ClmPolRepNo,
                    ["pClmLossDesc"] = objClaim.ClmLossDesc,
                    ["pClmVehChassisNo"] = objClaim.ClmVehChassisNo,
                    ["pClmVehEngineNo"] = objClaim.ClmVehEngineNo,
                    ["pClmVehRegnNo"] = objClaim.ClmVehRegnNo,
                    ["pClmVehValue"] = objClaim.ClmVehValue,
                    ["pClmUrBy"] = objClaim.ClmUpBy,
                    ["pClmUrDt"] = objClaim.ClmUpDt
                };            

                string query = $"UPDATE MOTOR_CLAIM " +
                    $"SET CLM_POL_NO = :pClmPolNo, CLM_POL_FM_DT = :pClmPolFmDt," +
                    $"CLM_POL_TO_DT = :pClmPolToDt, CLM_LOSS_DT = :pClmLossDt, CLM_INTM_DT = :pClmIntmDt, CLM_REG_DT = :pClmRegDt," +
                    $"CLM_POL_REP_NO = :pClmPolRepNo, CLM_LOSS_DESC =:pClmLossDesc, CLM_VEH_CHASSIS_NO = :pClmVehChassisNo, CLM_VEH_ENGINE_NO = :pClmVehEngineNo," +
                    $"CLM_VEH_REGN_NO = :pClmVehRegnNo, CLM_VEH_VALUE = :pClmVehValue, CLM_UP_BY = :pClmUrBy, CLM_UP_DT = :pClmUrDt " +
                    $"WHERE CLM_UID = :pClmUid";

                if (Dbconnection.ExecuteQuery(paramValues, query) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int GetClaimSequence()
        {
            string query = $"SELECT CLM_UID_SEQ.NEXTVAL FROM DUAL";
            int sequence = int.Parse(Dbconnection.ExecuteScalar(query).ToString());
            return sequence;
        }
        public DataTable FetchAllClaim()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Dbconnection.ExecuteDataset($"SELECT CLM_UID,CLM_NO, CLM_POL_FM_DT, CLM_POL_TO_DT," +
                    $"CLM_POL_REP_NO, CLM_VEH_CHASSIS_NO, CLM_VEH_VALUE, " +
                    $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = CLM_SUR_CR_YN AND CM_TYPE = 'CLM_SUR_CR_YN') AS CLM_SUR_CR_YN, " +
                    $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = CLM_SUR_APPR_YN AND CM_TYPE = 'CLM_SUR_APPR') AS CLM_SUR_APPR_YN " +
                    $"FROM MOTOR_CLAIM ORDER BY CLM_NO DESC");

                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable FetchAllClaimForSurveyor()
        {
            try
            {
                DataTable dt = new DataTable();

                dt = Dbconnection.ExecuteDataset($"SELECT CLM_UID, CLM_NO, CLM_POL_FM_DT, CLM_POL_TO_DT," +
                    $"CLM_VEH_CHASSIS_NO, CLM_VEH_VALUE, " +
                    $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = 'N' AND CM_TYPE = 'CLM_SUR_CR_YN') AS CLM_SUR_CR_YN " +
                    $"FROM MOTOR_CLAIM " +
                    $"WHERE CLM_SUR_NO IS NULL OR CLM_SUR_NO = '' ORDER BY CLM_NO DESC ");
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable FetchByClmUid(Claim objCclaim)
        {
            try
            {
                string query = $"SELECT * FROM MOTOR_CLAIM WHERE CLM_UID = '{objCclaim.ClmUid}'";
                DataTable dt = Dbconnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateAppovalStatus(Claim objClaim)
        {
            try
            {
                string query = $"UPDATE MOTOR_CLAIM " +
                       $"SET CLM_SUR_APPR_YN = '{objClaim.ClmSurApprYn}' " +
                       $"WHERE CLM_NO = '{objClaim.ClmNo}'";

                if (Dbconnection.ExecuteQuery(query) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateSurveyCreated(Claim objClaim)
        {
            try
            {
                string query = $"UPDATE MOTOR_CLAIM " +
                        $"SET CLM_SUR_CR_YN = '{objClaim.ClmSurCrYn}' " +
                        $"WHERE CLM_UID = {objClaim.ClmUid}";

                if (Dbconnection.ExecuteQuery(query) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool CheckDuplicatePolRepNo(Claim objClaim, int? pClmUid = null)
        {
            try
            {
                string query;

                if (pClmUid.HasValue)
                {
                    query = $"SELECT CLM_POL_REP_NO FROM MOTOR_CLAIM WHERE CLM_POL_REP_NO = '{objClaim.ClmPolRepNo}' AND CLM_UID != {pClmUid}";
                }
                else
                {
                    query = $"SELECT CLM_POL_REP_NO FROM MOTOR_CLAIM WHERE CLM_POL_REP_NO = '{objClaim.ClmPolRepNo}'";
                }
                if (Dbconnection.ExecuteScalar(query) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateSurNo(Claim objClaim)
        {
            try
            {
                string query = $"UPDATE MOTOR_CLAIM SET CLM_SUR_NO = '{objClaim.ClmSurNo}' WHERE CLM_UID = {objClaim.ClmUid}";

                if (Dbconnection.ExecuteQuery(query) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteClaim(Claim objClaim)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pClmUid"] = objClaim.ClmUid
                };
                string query = $"DELETE FROM MOTOR_CLAIM WHERE CLM_UID = :pClmUid";

                if (Dbconnection.ExecuteQuery(paramValues, query) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
