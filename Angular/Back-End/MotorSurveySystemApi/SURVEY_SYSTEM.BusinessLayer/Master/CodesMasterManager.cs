using SURVEY_SYSTEM.DataAccessLayer;
using SURVEY_SYSTEM.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURVEY_SYSTEM.BusinessLayer
{
    public class CodesMasterManager
    {
        
        public bool SaveCodesMaster(CodesMaster objCodesMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pCmCode"] = objCodesMaster.CmCode,
                    ["pCmType"] = objCodesMaster.CmType,
                    ["pCmDesc"] = objCodesMaster.CmDesc,
                    ["pCmValue"] = objCodesMaster.CmValue,
                    ["pCmActiveYn"] = objCodesMaster.CmActiveYn,
                    ["pCmCrBy"] = objCodesMaster.CmCrBy,
                    ["pCmCrDt"] = objCodesMaster.CmCrDt
                };

               
                string query = $"INSERT INTO CODES_MASTER (CM_CODE, CM_TYPE, CM_DESC, CM_VALUE , CM_ACTIVE_YN, CM_CR_BY, CM_CR_DT)" +
                $"VALUES (:pCmCode, :pCmType, :pCmDesc, :pCmValue, :pCmActiveYn, " +
                $":pCmCrBy, :pCmCrDt)";

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
        public DataTable FetchByCmcode(CodesMaster objCodeMaster)
        {
            try
            {
                string query = $"SELECT CM_CODE, CM_TYPE, CM_DESC, CM_VALUE, CM_ACTIVE_YN FROM CODES_MASTER " +
                    $"WHERE CM_CODE = '{objCodeMaster.CmCode}' AND CM_TYPE = '{objCodeMaster.CmType}'";
                DataTable dt = DbConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable FetchAllCodesMaster()
        {
            try
            {
                string query = $"SELECT CM_CODE, CM_TYPE, CM_DESC, CM_VALUE, " +
                    $"CASE WHEN CM_ACTIVE_YN = 'Y' THEN 'Yes' WHEN CM_ACTIVE_YN = 'N' THEN 'No' END AS CM_ACTIVE_YN " +
                    $"FROM CODES_MASTER ORDER BY CM_CR_DT DESC";
                DataTable dt = DbConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateCodesMaster(CodesMaster objCodeMaster)
        {
            try
            {

                StringBuilder sbQuery = new StringBuilder();
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pCmCode"] = objCodeMaster.CmCode,
                    ["pCmType"] = objCodeMaster.CmType,
                    ["pCmDesc"] = objCodeMaster.CmDesc,
                    ["pCmValue"] = objCodeMaster.CmValue,
                    ["pCmActiveYn"] = objCodeMaster.CmActiveYn,
                    ["pCmUpBy"] = objCodeMaster.CmUpBy,
                    ["pCmUpDt"] = objCodeMaster.CmUpDt
                };

                sbQuery.Append("UPDATE CODES_MASTER ");
                sbQuery.Append("SET CM_CODE = :pCmCode, CM_TYPE = :pCmType, CM_DESC = :pCmDesc, CM_VALUE = :pCmValue, ");
                sbQuery.Append("CM_ACTIVE_YN = :pCmActiveYn, CM_UP_BY = :pCmUpBy, CM_UP_DT = :pCmUpDt ");
                sbQuery.Append("WHERE CM_CODE = :pCmCode AND CM_TYPE = :pCmType");

                if (DbConnection.ExecuteQuery(paramValues, sbQuery.ToString()) > 0)
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
        public bool DeleteCodesMaster(CodesMaster objCodeMaster)
        {
            try
            {
                StringBuilder sbQuery = new StringBuilder();

                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pCmCode"] = objCodeMaster.CmCode,
                    ["pCmType"] = objCodeMaster.CmType
                };

                string query = $"DELETE FROM CODES_MASTER WHERE CM_CODE = :pCmCode AND CM_TYPE = :pCmType";

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
        public bool CheckDuplicateCodesMaster(CodesMaster objCodeMaster)
        {
            try
            {
                string query = $"SELECT CM_CODE FROM CODES_MASTER WHERE CM_CODE = '{objCodeMaster.CmCode}' AND CM_TYPE = '{objCodeMaster.CmType}'";

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
        public DataTable FillDropDownList(CodesMaster objCodeMaster)
        {
            try
            {
                string query = $"SELECT CM_CODE, CM_CODE || '-' || CM_DESC AS CM_DISPLAY, CM_DESC, CM_VALUE FROM CODES_MASTER " +
                      $"WHERE CM_TYPE = '{objCodeMaster.CmType}' AND CM_ACTIVE_YN = 'Y'";

                DataTable dt = DbConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public double GetCmValue(CodesMaster objCodeMaster)
        {
            try
            {
                string query = $"SELECT CM_VALUE FROM CODES_MASTER WHERE CM_CODE = '{objCodeMaster.CmCode}' AND CM_TYPE = '{objCodeMaster.CmType}'";
                double cmValue = double.Parse(DbConnection.ExecuteScalar(query).ToString());
                return cmValue;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
