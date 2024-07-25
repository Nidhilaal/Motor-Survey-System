using SURVEY_SYSTEM.DataAccessLayer;
using SURVEY_SYSTEM.EntityLayer.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SURVEY_SYSTEM.BusinessLayer.Transaction
{
    public class MotorClaimManager
    {
        public bool SaveClaim(MotorClaim objMotorClaim)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pClmUid"] = objMotorClaim.ClmUid,
                    ["pClmNo"] = objMotorClaim.ClmNo,
                    ["pClmPolNo"] = objMotorClaim.ClmPolNo,
                    ["pClmPolFmDt"] = objMotorClaim.ClmPolFmDt,
                    ["pClmPolToDt"] = objMotorClaim.ClmPolToDt,
                    ["pClmLossDt"] = objMotorClaim.ClmLossDt,
                    ["pClmIntmDt"] = objMotorClaim.ClmIntmDt,
                    ["pClmRegDt"] = objMotorClaim.ClmRegDt,
                    ["pClmPolRepNo"] = objMotorClaim.ClmPolRepNo,
                    ["pClmLossDesc"] = objMotorClaim.ClmLossDesc,
                    ["pClmVehChassisNo"] = objMotorClaim.ClmVehChassisNo,
                    ["pClmVehEngineNo"] = objMotorClaim.ClmVehEngineNo,
                    ["pClmVehRegnNo"] = objMotorClaim.ClmVehRegnNo,
                    ["pClmVehValue"] = objMotorClaim.ClmVehValue,
                    ["pClmCrBy"] = objMotorClaim.ClmCrBy,
                    ["pClmCrDt"] = objMotorClaim.ClmCrDt
                };

                string query = $"INSERT INTO MOTOR_CLAIM (CLM_UID, CLM_NO, CLM_POL_NO, CLM_POL_FM_DT, CLM_POL_TO_DT, " +
                    $"CLM_LOSS_DT, CLM_INTM_DT, CLM_REG_DT, CLM_POL_REP_NO, CLM_LOSS_DESC, CLM_VEH_CHASSIS_NO, CLM_VEH_ENGINE_NO, " +
                    $"CLM_VEH_REGN_NO, CLM_VEH_VALUE, CLM_CR_BY, CLM_CR_DT)" +
                $"VALUES (:pClmUid, :pClmNo, :pClmPolNo, :pClmPolFmDt, :pClmPolToDt, :pClmLossDt, :pClmIntmDt, :pClmRegDt, :pClmPolRepNo," +
                $":pClmLossDesc, :pClmVehChassisNo, :pClmVehEngineNo, :pClmVehRegnNo, :pClmVehValue, :pClmCrBy, :pClmCrDt)";

                if (DbConnection.ExecuteQuery(paramValues, query) > 0)
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
        public bool UpdateClaim(MotorClaim objMotorClaim)
        {
            try
            {


                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                   
                    ["pClmPolNo"] = objMotorClaim.ClmPolNo,
                    ["pClmPolFmDt"] = objMotorClaim.ClmPolFmDt,
                    ["pClmPolToDt"] = objMotorClaim.ClmPolToDt,
                    ["pClmLossDt"] = objMotorClaim.ClmLossDt,
                    ["pClmIntmDt"] = objMotorClaim.ClmIntmDt,
                    ["pClmRegDt"] = objMotorClaim.ClmRegDt,
                    ["pClmPolRepNo"] = objMotorClaim.ClmPolRepNo,
                    ["pClmLossDesc"] = objMotorClaim.ClmLossDesc,
                    ["pClmVehChassisNo"] = objMotorClaim.ClmVehChassisNo,
                    ["pClmVehEngineNo"] = objMotorClaim.ClmVehEngineNo,
                    ["pClmVehRegnNo"] = objMotorClaim.ClmVehRegnNo,
                    ["pClmVehValue"] = objMotorClaim.ClmVehValue,
                    ["pClmUrBy"] = objMotorClaim.ClmUpBy,
                    ["pClmUrDt"] = objMotorClaim.ClmUpDt,
                    ["pClmUid"] = objMotorClaim.ClmUid
                };

                string query = $"UPDATE MOTOR_CLAIM " +
                    $"SET CLM_POL_NO = :pClmPolNo, CLM_POL_FM_DT = :pClmPolFmDt," +
                    $"CLM_POL_TO_DT = :pClmPolToDt, CLM_LOSS_DT = :pClmLossDt, CLM_INTM_DT = :pClmIntmDt, CLM_REG_DT = :pClmRegDt," +
                    $"CLM_POL_REP_NO = :pClmPolRepNo, CLM_LOSS_DESC =:pClmLossDesc, CLM_VEH_CHASSIS_NO = :pClmVehChassisNo, CLM_VEH_ENGINE_NO = :pClmVehEngineNo," +
                    $"CLM_VEH_REGN_NO = :pClmVehRegnNo, CLM_VEH_VALUE = :pClmVehValue, CLM_UP_BY = :pClmUrBy, CLM_UP_DT = :pClmUrDt " +
                    $"WHERE CLM_UID = :pClmUid";

                if (DbConnection.ExecuteQuery(paramValues, query) > 0)
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
            int sequence = int.Parse(DbConnection.ExecuteScalar(query).ToString());
            return sequence;
        }
        public DataTable FetchAllClaim()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = DbConnection.ExecuteDataset($"SELECT CLM_UID,CLM_NO, CLM_POL_FM_DT, CLM_POL_TO_DT," +
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

                dt = DbConnection.ExecuteDataset($"SELECT CLM_UID, CLM_NO, CLM_POL_FM_DT, CLM_POL_TO_DT," +
                    $"CLM_VEH_CHASSIS_NO, CLM_VEH_VALUE, " +
                    $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = 'N' AND CM_TYPE = 'CLM_SUR_CR_YN') AS CLM_SUR_CR_YN " +
                    $"FROM MOTOR_CLAIM " +
                    $"WHERE CLM_SUR_CR_YN = 'N' ORDER BY CLM_NO DESC ");
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable FetchByClmUid(int clmUid)
        {
            try
            {
                string query = $"SELECT * FROM MOTOR_CLAIM WHERE CLM_UID = '{clmUid}'";
                DataTable dt = DbConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateAppovalStatus(MotorClaim objMotorClaim)
        {
            try
            {
                string query = $"UPDATE MOTOR_CLAIM " +
                       $"SET CLM_SUR_APPR_YN = '{objMotorClaim.ClmSurApprYn}' " +
                       $"WHERE CLM_NO = '{objMotorClaim.ClmNo}'";

                if (DbConnection.ExecuteQuery(query) > 0)
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
        public bool UpdateSurveyCreated(MotorClaim objMotorClaim)
        {
            try
            {
                string query = $"UPDATE MOTOR_CLAIM " +
                        $"SET CLM_SUR_CR_YN = '{objMotorClaim.ClmSurCrYn}' " +
                        $"WHERE CLM_UID = {objMotorClaim.ClmUid}";

                if (DbConnection.ExecuteQuery(query) > 0)
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
        public bool CheckDuplicatePolRepNo(string clmPolRepNo )
        {
            try
            {
                string query;

                query = $"SELECT CLM_POL_REP_NO FROM MOTOR_CLAIM WHERE CLM_POL_REP_NO = '{clmPolRepNo}'";
              
                if (DbConnection.ExecuteScalar(query) != null)
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
        public bool UpdateSurNo(MotorClaim objMotorClaim)
        {
            try
            {
                string query = $"UPDATE MOTOR_CLAIM SET CLM_SUR_NO = '{objMotorClaim.ClmSurNo}' WHERE CLM_UID = {objMotorClaim.ClmUid}";

                if (DbConnection.ExecuteQuery(query) > 0)
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
        public bool DeleteClaim(MotorClaim objMotorClaim)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pClmUid"] = objMotorClaim.ClmUid
                };
                string query = $"DELETE FROM MOTOR_CLAIM WHERE CLM_UID = :pClmUid";

                if (DbConnection.ExecuteQuery(paramValues, query) > 0)
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
