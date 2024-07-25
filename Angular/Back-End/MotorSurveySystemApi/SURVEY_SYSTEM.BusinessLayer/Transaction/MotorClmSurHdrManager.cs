using SURVEY_SYSTEM.DataAccessLayer;
using SURVEY_SYSTEM.EntityLayer.Transaction;
using System.Data;

namespace SURVEY_SYSTEM.BusinessLayer.Transaction
{
    public class MotorClmSurHdrManager
    {
        public bool SaveSurveyHeader(MotorClmSurHdr objMotorClmSurHdr)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pSurUid"] = objMotorClmSurHdr.SurUid,
                    ["pSurclmUid"] = objMotorClmSurHdr.SurclmUid,
                    ["pSurclmNo"] = objMotorClmSurHdr.SurclmNo,
                    ["pSurNo"] = objMotorClmSurHdr.SurNo,
                    ["pSurChassisNo"] = objMotorClmSurHdr.SurChassisNo,
                    ["pSurRegnNo"] = objMotorClmSurHdr.SurRegnNo,
                    ["pSurEngineNo"] = objMotorClmSurHdr.SurEngineNo,
                    ["pSurCurr"] = objMotorClmSurHdr.SurCurr,
                    ["pSurFcAmt"] = objMotorClmSurHdr.SurFcAmt,
                    ["pSurLcAmt"] = objMotorClmSurHdr.SurLcAmt,
                    ["pSurCrBy"] = objMotorClmSurHdr.SurCrBy,
                    ["pSurCrDt"] = objMotorClmSurHdr.SurCrDt
                };

                string query = $"INSERT INTO MOTOR_CLM_SUR_HDR (SUR_UID, SUR_CLM_UID, SUR_CLM_NO, SUR_NO, SUR_CHASSIS_NO," +
                    $"SUR_REGN_NO, SUR_ENGINE_NO, SUR_CURR, SUR_FC_AMT, SUR_LC_AMT, SUR_CR_BY, SUR_CR_DT)" +
                    $"VALUES (:pSurUid, :pSurclmUid, :pSurclmNo, :pSurNo, :pSurChassisNo, :pSurRegnNo, :pSurEngineNo, :pSurCurr, " +
                    $":pSurFcAmt, :pSurLcAmt, :pSurCrBy, :pSurCrDt)";

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
        public DataTable FetchBySurUid(MotorClmSurHdr objMotorClmSurHdr)
        {
            try
            {
                string query = $"SELECT SUR_UID, SUR_NO, SUR_CLM_UID ,SUR_CLM_NO, SUR_CHASSIS_NO, SUR_REGN_NO, SUR_ENGINE_NO, SUR_CURR, " +
                       $"TO_NUMBER(SUR_FC_AMT, '9999999.99') AS SUR_FC_AMT, TO_NUMBER(SUR_LC_AMT, '9999999.99') AS SUR_LC_AMT, SUR_STATUS, " +
                       $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = SUR_APPR_STS AND CM_TYPE = 'SUR_APPR_STS') AS SUR_APPR_STS, " +
                       $"TRUNC(SUR_APPR_DT) AS SUR_APPR_DT, SUR_APPR_BY, MOTOR_CLAIM.CLM_POL_NO " +
                       $"FROM MOTOR_CLM_SUR_HDR " +
                       $"LEFT JOIN MOTOR_CLAIM ON MOTOR_CLAIM.CLM_SUR_NO = MOTOR_CLM_SUR_HDR.SUR_NO " +
                       $"WHERE SUR_UID = '{objMotorClmSurHdr.SurUid}'";
                DataTable dt = DbConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateSurveyHeader(MotorClmSurHdr objMotorClmSurHdr)
        {
            try
            {

                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                   
                    ["pSurCurr"] = objMotorClmSurHdr.SurCurr,
                    ["pSurFcAmt"] = objMotorClmSurHdr.SurFcAmt,
                    ["pSurLcAmt"] = objMotorClmSurHdr.SurLcAmt,
                    ["pSurStatus"] = objMotorClmSurHdr.SurStatus,
                    ["pSurUpBy"] = objMotorClmSurHdr.SurUpBy,
                    ["pSurUpDt"] = objMotorClmSurHdr.SurUpDt,
                    ["pSurUid"] = objMotorClmSurHdr.SurUid
                };

                string query = $"UPDATE MOTOR_CLM_SUR_HDR " +
                    $"SET SUR_CURR = :pSurCurr, SUR_FC_AMT = :pSurFcAmt, SUR_LC_AMT = :pSurLcAmt, SUR_STATUS = :pSurStatus, SUR_UP_BY = :pSurUpBy, " +
                    $"SUR_UP_DT = :pSurUpDt WHERE SUR_UID = :pSurUid";

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
        public int GetSurUidSequence()
        {
            try
            {
                string query = $"SELECT SUR_UID_SEQ.NEXTVAL FROM DUAL";
                string sequence = DbConnection.ExecuteScalar(query).ToString();
                return Convert.ToInt32(sequence);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable FetchAllSurvey(MotorClmSurHdr objMotorClmSurHdr)
        {
            try
            {
                string query = $"SELECT SUR_UID, SUR_NO, SUR_CLM_NO, SUR_CHASSIS_NO, SUR_REGN_NO, SUR_ENGINE_NO, SUR_FC_AMT, SUR_LC_AMT, " +
                       $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = SUR_STATUS AND CM_TYPE = 'SUR_STATUS') AS SUR_STATUS, " +
                       $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = SUR_APPR_STS AND CM_TYPE = 'SUR_APPR_STS') AS SUR_APPR_STS " +
                       $"FROM MOTOR_CLM_SUR_HDR WHERE SUR_CR_BY = '{objMotorClmSurHdr.SurCrBy}' ORDER BY SUR_NO DESC ";

                DataTable dt = DbConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateSurveyStatus(MotorClmSurHdr objMotorClmSurHdr)
        {
            try
            {
                string formattedSurApprDt = objMotorClmSurHdr.SurApprDt.ToString("dd-MMM-yy").ToUpper();

                string query = $"UPDATE  MOTOR_CLM_SUR_HDR " +
                    $"SET SUR_APPR_STS = '{objMotorClmSurHdr.SurApprSts}', SUR_APPR_DT = '{formattedSurApprDt}', SUR_APPR_BY = '{objMotorClmSurHdr.SurApprBy}' " +
                    $"WHERE SUR_UID = {objMotorClmSurHdr.SurUid}";

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
        public DataTable FetchBySurClmUid(int surclmUid)
        {
            try
            {
                string query = $"SELECT SUR_UID, SUR_NO, SUR_CLM_UID ,SUR_CLM_NO, SUR_CHASSIS_NO, SUR_REGN_NO, SUR_ENGINE_NO, SUR_CURR, " +
                       $"TO_NUMBER(SUR_FC_AMT, '9999999.99') AS SUR_FC_AMT, TO_NUMBER(SUR_LC_AMT, '9999999.99') AS SUR_LC_AMT, SUR_STATUS, " +
                       $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = SUR_APPR_STS AND CM_TYPE = 'SUR_APPR_STS') AS SUR_APPR_STS, " +
                       $"TRUNC(SUR_APPR_DT) AS SUR_APPR_DT, SUR_APPR_BY, MOTOR_CLAIM.CLM_POL_NO " +
                       $"FROM MOTOR_CLM_SUR_HDR " +
                       $"LEFT JOIN MOTOR_CLAIM ON MOTOR_CLAIM.CLM_SUR_NO = MOTOR_CLM_SUR_HDR.SUR_NO " +
                       $"WHERE SUR_CLM_UID = '{surclmUid}'";
                DataTable dt = DbConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public int GetSurUidBySurNo(MotorClmSurHdr objMotorClmSurHdr)
        //{
        //    try
        //    {
        //        string query = $"SELECT SUR_UID FROM MOTOR_CLM_SUR_HDR WHERE SUR_NO = '{objMotorClmSurHdr.SurNo}'";
        //        string surUid = DbConnection.ExecuteScalar(query).ToString();
        //        return Convert.ToInt32(surUid);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public void UpdateFcAmt(MotorClmSurHdr objMotorClmSurHdr)
        {
            string query = $"UPDATE  MOTOR_CLM_SUR_HDR " +
                    $"SET SUR_FC_AMT = {objMotorClmSurHdr.SurFcAmt} " +
                    $"WHERE SUR_UID = {objMotorClmSurHdr.SurUid}";
            DbConnection.ExecuteQuery(query);
        }
        public void UpdateLcAmt(MotorClmSurHdr objMotorClmSurHdr)
        {
            string query = $"UPDATE  MOTOR_CLM_SUR_HDR " +
                    $"SET SUR_LC_AMT = {objMotorClmSurHdr.SurLcAmt} " +
                    $"WHERE SUR_UID = {objMotorClmSurHdr.SurUid}";
            DbConnection.ExecuteQuery(query);
        }
        public bool UpdateSurveyCurrency(MotorClmSurHdr objMotorClmSurHdr)
        {
            try
            {

                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pSurUid"] = objMotorClmSurHdr.SurUid,
                    ["pSurCurr"] = objMotorClmSurHdr.SurCurr,
                    ["pSurStatus"] = objMotorClmSurHdr.SurStatus,
                    ["pSurUpBy"] = objMotorClmSurHdr.SurUpBy,
                    ["pSurUpDt"] = objMotorClmSurHdr.SurUpDt
                };

                string query = $"UPDATE MOTOR_CLM_SUR_HDR " +
                    $"SET SUR_CURR = :pSurCurr, SUR_STATUS = :pSurStatus, SUR_UP_BY = :pSurUpBy, " +
                    $"SUR_UP_DT = :pSurUpDt WHERE SUR_UID = :pSurUid";

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
