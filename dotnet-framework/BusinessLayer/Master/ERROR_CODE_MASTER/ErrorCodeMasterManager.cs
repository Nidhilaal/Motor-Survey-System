using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ErrorCodeMasterManager
    {
        public bool SaveErrorCodesMaster(ErrorCodeMaster objErrorCodeMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pErrCode"] = objErrorCodeMaster.ErrCode,
                    ["pErrType"] = objErrorCodeMaster.ErrType,
                    ["pErrDesc"] = objErrorCodeMaster.ErrDesc,
                    ["pErrCrBy"] = objErrorCodeMaster.ErrCrBy,
                    ["pErrCrDt"] = objErrorCodeMaster.ErrCrDt
                    
                };

                string query = $"INSERT INTO ERROR_CODE_MASTER (ERR_CODE, ERR_TYPE, ERR_DESC, ERR_CR_BY, ERR_CR_DT)" +
                $"VALUES (:pErrCode, :pErrType, :pErrDesc, :pErrCrBy, :pErrCrDt)";

                if (Dbconnection.ExecuteQuery(paramValues, query) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


                //string formattedErrCrDt = objErrorCodeMaster.ErrCrDt.ToString("dd-MMM-yy").ToUpper();
             

                //string query = $"INSERT INTO ERROR_CODE_MASTER(ERR_CODE, ERR_TYPE, ERR_DESC, ERR_CR_BY, ERR_CR_DT) " +
                //$"VALUES('{objErrorCodeMaster.ErrCode}', '{objErrorCodeMaster.ErrType}', '{objErrorCodeMaster.ErrDesc}'," +
                //$"'{objErrorCodeMaster.ErrCrBy}', '{formattedErrCrDt}')";

                //if (Dbconnection.ExecuteQuery(query) > 0)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception)
            {

                throw;
            }

        }
        public DataTable FetchByErrcode(ErrorCodeMaster objErrorCodeMaster)
        {
            try
            {
                string query = $"SELECT ERR_CODE, ERR_TYPE, ERR_DESC FROM ERROR_CODE_MASTER WHERE ERR_CODE = '{objErrorCodeMaster.ErrCode}'";
                DataTable dt = Dbconnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable FetchAllErrorCodesMaster()
        {
            try
            {
                string query = $"SELECT ERR_CODE, ERR_TYPE, ERR_DESC FROM ERROR_CODE_MASTER ORDER BY ERR_CR_DT DESC";
                DataTable dt = Dbconnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateErrorCodesMaster(ErrorCodeMaster objErrorCodeMaster)
        {
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pErrCode"] = objErrorCodeMaster.ErrCode,
                    ["pErrType"] = objErrorCodeMaster.ErrType,
                    ["pErrDesc"] = objErrorCodeMaster.ErrDesc,
                    ["pErrUpBy"] = objErrorCodeMaster.ErrCrBy,
                    ["pErrUpDt"] = objErrorCodeMaster.ErrCrDt
                };

                sbQuery.Append("UPDATE ERROR_CODE_MASTER ");
                sbQuery.Append("SET ERR_TYPE = :pErrType, ERR_DESC = :pErrDesc, ERR_UP_BY = :pErrUpBy, ERR_UP_DT = :pErrUpDt ");          
                sbQuery.Append("WHERE ERR_CODE = :pErrCode");

                if (Dbconnection.ExecuteQuery(paramValues, sbQuery.ToString()) > 0)
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
        public bool DeleteErrorCodesMaster(ErrorCodeMaster objErrorCodeMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pErrCode"] = objErrorCodeMaster.ErrCode,

                };

                string query = $"DELETE FROM ERROR_CODE_MASTER WHERE ERR_CODE = :pErrCode";

                if (Dbconnection.ExecuteQuery(paramValues,query) > 0)
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
        public bool CheckDuplicateErrorCodesMaster(ErrorCodeMaster objErrorCodeMaster)
        {
            try
            {
                string query = $"SELECT ERR_CODE FROM ERROR_CODE_MASTER WHERE ERR_CODE = '{objErrorCodeMaster.ErrCode}'";

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
        public string FetchErrorCodeByErrCode(ErrorCodeMaster objErrorCodeMaster)
        {
            try
            {
                string query = $"SELECT ERR_DESC FROM ERROR_CODE_MASTER " +
                       $"WHERE ERR_CODE = '{objErrorCodeMaster.ErrCode}'";

                string errDesc = Dbconnection.ExecuteScalar(query).ToString();
                return errDesc;
            }
            catch (Exception)
            {

                throw;
            }
        }
      
    }
}
