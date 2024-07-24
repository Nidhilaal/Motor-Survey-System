using SURVEY_SYSTEM.DataAccessLayer;
using SURVEY_SYSTEM.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURVEY_SYSTEM.BusinessLayer.Master
{
    public class ErrorCodesMasterManager
    {
        public bool SaveErrorCodesMaster(ErrorCodesMaster objErrorCodesMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pErrCode"] = objErrorCodesMaster.ErrCode,
                    ["pErrType"] = objErrorCodesMaster.ErrType,
                    ["pErrDesc"] = objErrorCodesMaster.ErrDesc,
                    ["pErrCrBy"] = objErrorCodesMaster.ErrCrBy,
                    ["pErrCrDt"] = objErrorCodesMaster.ErrCrDt

                };

                string query = $"INSERT INTO ERROR_CODE_MASTER (ERR_CODE, ERR_TYPE, ERR_DESC, ERR_CR_BY, ERR_CR_DT)" +
                $"VALUES (:pErrCode, :pErrType, :pErrDesc, :pErrCrBy, :pErrCrDt)";

                if (DbConnection.ExecuteQuery(paramValues, query) > 0)
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
        public DataTable FetchByErrcode(ErrorCodesMaster objErrorCodesMaster)
        {
            try
            {
                string query = $"SELECT ERR_CODE, ERR_TYPE, ERR_DESC FROM ERROR_CODE_MASTER WHERE ERR_CODE = '{objErrorCodesMaster.ErrCode}'";
                DataTable dt = DbConnection.ExecuteDataset(query);
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
                DataTable dt = DbConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateErrorCodesMaster(ErrorCodesMaster objErrorCodesMaster)
        {
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {

                    ["pErrType"] = objErrorCodesMaster.ErrType,
                    ["pErrDesc"] = objErrorCodesMaster.ErrDesc,
                    ["pErrUpBy"] = objErrorCodesMaster.ErrCrBy,
                    ["pErrUpDt"] = objErrorCodesMaster.ErrCrDt,
                    ["pErrCode"] = objErrorCodesMaster.ErrCode
                };

                sbQuery.Append("UPDATE ERROR_CODE_MASTER ");
                sbQuery.Append("SET ERR_TYPE = :pErrType, ERR_DESC = :pErrDesc, ERR_UP_BY = :pErrUpBy, ERR_UP_DT = :pErrUpDt ");
                sbQuery.Append("WHERE ERR_CODE = :pErrCode");

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
        public bool DeleteErrorCodesMaster(ErrorCodesMaster objErrorCodesMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pErrCode"] = objErrorCodesMaster.ErrCode,

                };

                string query = $"DELETE FROM ERROR_CODE_MASTER WHERE ERR_CODE = :pErrCode";

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
        public bool CheckDuplicateErrorCodesMaster(ErrorCodesMaster objErrorCodesMaster)
        {
            try
            {
                string query = $"SELECT ERR_CODE FROM ERROR_CODE_MASTER WHERE ERR_CODE = '{objErrorCodesMaster.ErrCode}'";

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
        public string FetchErrorCodeByErrCode(ErrorCodesMaster objErrorCodesMaster)
        {
            try
            {
                string query = $"SELECT ERR_DESC FROM ERROR_CODE_MASTER " +
                       $"WHERE ERR_CODE = '{objErrorCodesMaster.ErrCode}'";

                string errDesc = DbConnection.ExecuteScalar(query).ToString();
                return errDesc;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
