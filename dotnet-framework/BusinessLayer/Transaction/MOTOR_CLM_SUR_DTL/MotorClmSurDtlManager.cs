using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MotorClmSurDtlManager
    {
        public bool SaveSurveyDetails(MotorClmSurDtl objMotorClmSurDtl)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pSurdUid"] = objMotorClmSurDtl.SurdUid,
                    ["pSurdSurUid"] = objMotorClmSurDtl.SurdSurUid,
                    ["pSurdItemCode"] = objMotorClmSurDtl.SurdItemCode,
                    ["pSurdType"] = objMotorClmSurDtl.SurdType,
                    ["pSurdUnitPrice"] = objMotorClmSurDtl.SurdUnitPrice,
                    ["pSurdQuantity"] = objMotorClmSurDtl.SurdQuantity,
                    ["pSurdFcAmt"] = objMotorClmSurDtl.SurdFcAmt,
                    ["pSurdLcAmt"] = objMotorClmSurDtl.SurdLcAmt,
                    ["pSurdCrBy"] = objMotorClmSurDtl.SurdCrBy,
                    ["pSurdCrDt"] = objMotorClmSurDtl.SurdCrDt
                };

                string query = $"INSERT INTO MOTOR_CLM_SUR_DTL (SURD_UID, SURD_SUR_UID, SURD_ITEM_CODE, SURD_TYPE, SURD_UNIT_PRICE, SURD_QUANTITY," +
                    $"SURD_FC_AMT, SURD_LC_AMT, SURD_CR_BY, SURD_CR_DT) " +
                    $"VALUES (:pSurdUid, :pSurdSurUid, :pSurdItemCode, :pSurdType, :pSurdUnitPrice, :pSurdQuantity," +
                    $":pSurdFcAmt, :pSurdLcAmt, :pSurdCrBy, :pSurdCrDt)";

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
        public int GetSurdUidSequence()
        {
            try
            {
                string query = $"SELECT SURD_UID_SEQ.NEXTVAL FROM DUAL";
                int sequence = int.Parse(Dbconnection.ExecuteScalar(query).ToString());
                return sequence;
            }
            catch (Exception)
            {

                throw;
            }
        }      
        public DataTable FetchBySurdSurUid(MotorClmSurDtl objMotorClmSurDtl)            
        {
            try
            {
                string query = $"SELECT SURD_UID, SURD_SUR_UID, " +
                    $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = SURD_ITEM_CODE AND CM_TYPE = 'SURD_ITEM_CODE') AS SURD_ITEM_CODE," +
                    $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = SURD_TYPE AND CM_TYPE = 'SURD_TYPE') AS SURD_TYPE, SURD_UNIT_PRICE, SURD_QUANTITY," +
                    $"TO_NUMBER(SURD_FC_AMT, '9999999.99') AS SURD_FC_AMT, TO_NUMBER(SURD_LC_AMT, '9999999.99') AS SURD_LC_AMT FROM MOTOR_CLM_SUR_DTL " +
                    $"WHERE SURD_SUR_UID = {objMotorClmSurDtl.SurdSurUid} ORDER BY SURD_CR_DT DESC";

                DataTable dt = Dbconnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
    
        public bool UpdateSurveyDetails(MotorClmSurDtl objMotorClmSurDtl)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                   ["pSurdUid"] = objMotorClmSurDtl.SurdUid,
                   ["pSurdItemCode"] = objMotorClmSurDtl.SurdItemCode,
                   ["pSurdType"] = objMotorClmSurDtl.SurdType,
                   ["pSurdUnitPrice"] = objMotorClmSurDtl.SurdUnitPrice,
                   ["pSurdQuantity"] = objMotorClmSurDtl.SurdQuantity,
                   ["pSurdFcAmt"] = objMotorClmSurDtl.SurdFcAmt,
                   ["pSurdLcAmt"] = objMotorClmSurDtl.SurdLcAmt,
                   ["pSurdUpBy"] = objMotorClmSurDtl.SurdUpBy,
                   ["pSurdUpDt"] = objMotorClmSurDtl.SurdUpDt
                };              

                string query = $"UPDATE MOTOR_CLM_SUR_DTL " +
                    $"SET SURD_ITEM_CODE = :pSurdItemCode, SURD_TYPE = :pSurdType, SURD_UNIT_PRICE = :pSurdUnitPrice, SURD_QUANTITY = :pSurdQuantity, " +
                    $"SURD_FC_AMT = :pSurdFcAmt, SURD_LC_AMT = :pSurdLcAmt, SURD_UP_BY = :pSurdUpBy, SURD_UP_DT = :pSurdUpDt " +
                    $"WHERE  SURD_UID = :pSurdUid";

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
        public DataTable FetchBySurdUid(MotorClmSurDtl objMotorClmSurDtl)
        {
            try
            {
                string query = $"SELECT SURD_SUR_UID, SURD_ITEM_CODE, SURD_TYPE, SURD_UNIT_PRICE, SURD_QUANTITY," +
                      $"SURD_FC_AMT, SURD_LC_AMT FROM MOTOR_CLM_SUR_DTL " +
                      $"WHERE SURD_UID = {objMotorClmSurDtl.SurdUid}";

                DataTable dt = Dbconnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public double GetSumofLcAmt(MotorClmSurDtl objMotorClmSurDtl)
        {
            try
            {
                string query = $"SELECT TO_NUMBER(SUM(SURD_LC_AMT), '9999999.99') " +
                    $"FROM MOTOR_CLM_SUR_DTL WHERE SURD_SUR_UID = '{objMotorClmSurDtl.SurdSurUid}'";

                object result = Dbconnection.ExecuteScalar(query);

                if (result == null || result == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(result.ToString());
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public double GetSumofFcAmt(MotorClmSurDtl objMotorClmSurDtl)
        {
            try
            {
                string query = $"SELECT TO_NUMBER(SUM(SURD_FC_AMT), '9999999.99') AS SUM " +
                    $"FROM MOTOR_CLM_SUR_DTL WHERE SURD_SUR_UID = {objMotorClmSurDtl.SurdSurUid}";

                object result = Dbconnection.ExecuteScalar(query);

                if (result == null || result == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(result.ToString());
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool CheckDuplicatePolRepNo(Claim objClaim, int? pclmUid = null)
        {
            try
            {
                string query = string.Empty;

                if (pclmUid.HasValue)
                {
                    query = $"SELECT CLM_POL_REP_NO FROM MOTOR_CLAIM WHERE CLM_POL_REP_NO = '{objClaim.ClmPolRepNo}' AND CLM_UID != {pclmUid}";
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
        public bool CheckDuplicateSurdUid(MotorClmSurDtl objMotorClmSurDtl,int? pSurdUid = null)
        {
            try
            {
                string query;

                if (pSurdUid.HasValue)
                {
                    query = $"SELECT SURD_UID FROM MOTOR_CLM_SUR_DTL " +
                        $"WHERE SURD_SUR_UID = {objMotorClmSurDtl.SurdSurUid} AND SURD_ITEM_CODE = '{objMotorClmSurDtl.SurdItemCode}' " +
                        $"AND SURD_UID != {pSurdUid}";
                }
                else
                {
                    query = $"SELECT SURD_UID FROM MOTOR_CLM_SUR_DTL " +
                        $"WHERE SURD_SUR_UID = {objMotorClmSurDtl.SurdSurUid} AND SURD_ITEM_CODE = '{objMotorClmSurDtl.SurdItemCode}'";
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
        public bool DeleteSurveyDetails(MotorClmSurDtl objMotorClmSurDtl)
        {
            try
            {
                string query = $"DELETE FROM MOTOR_CLM_SUR_DTL " +
                    $"WHERE SURD_SUR_UID = {objMotorClmSurDtl.SurdSurUid} AND SURD_UID = {objMotorClmSurDtl.SurdUid}";
               
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
    }
}
