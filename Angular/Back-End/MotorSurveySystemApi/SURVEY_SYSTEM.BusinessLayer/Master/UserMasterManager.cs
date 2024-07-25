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
    public class UserMasterManager
    {
        public Boolean GetAuthentication(UserMaster objUserMaster)
        {
            try
            {
                string sql = $"SELECT USER_ID,USER_NAME,USER_PASSWORD FROM USER_MASTER " +
                    $"WHERE USER_ID = '{objUserMaster.UserId}' AND USER_PASSWORD = '{objUserMaster.UserPassword}'";

                if (DbConnection.ExecuteDataset(sql).Rows.Count > 0)
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
        public string CheckUserType(UserMaster objUserMaster)
        {
            try
            {
                string query = $"SELECT USER_TYPE FROM USER_MASTER WHERE USER_ID = '{objUserMaster.UserId}'";
                DataTable dt = DbConnection.ExecuteDataset(query);

                var userType = string.Empty;

                foreach (DataRow row in dt.Rows)
                {
                    userType = row[0].ToString();
                }
                return userType;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool SaveUserMaster(UserMaster objUserMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pUserId"] = objUserMaster.UserId,
                    ["pUserName"] = objUserMaster.UserName,
                    ["pUserPassword"] = objUserMaster.UserPassword,
                    ["pUserType"] = objUserMaster.UserType,
                    ["pUserCrBy"] = objUserMaster.UserCrBy,
                    ["pUserCrDt"] = objUserMaster.UserCrDt

                };

                string query = $"INSERT INTO USER_MASTER (USER_ID, USER_NAME, USER_PASSWORD, USER_TYPE, USER_CR_BY, USER_CR_DT)" +
                $"VALUES (:pUserId, :pUserName, :pUserPassword, :pUserType, :pUserCrBy, :pUserCrDt)";

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
        public bool UpdateUserMaster(UserMaster objUserMaster)
        {
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                   
                    ["pUserName"] = objUserMaster.UserName,
                    ["pUserPassword"] = objUserMaster.UserPassword,
                    ["pUserType"] = objUserMaster.UserType,
                    ["pUserUpBy"] = objUserMaster.UserUpBy,
                    ["pUserUpDt"] = objUserMaster.UserUpDt,
                    ["pUserId"] = objUserMaster.UserId
                };

                sbQuery.Append("UPDATE USER_MASTER ");
                sbQuery.Append("SET USER_NAME = :pUserName, USER_PASSWORD = :pUserPassword, USER_TYPE = :pUserType, USER_UP_BY = :pUserUpBy, USER_UP_DT = :pUserUpDt ");
                sbQuery.Append("WHERE USER_ID = :pUserId");

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

        public bool DeleteUserMasterByUserId(UserMaster objUserMaster)
        {
            try
            {
                Dictionary<string, object> paramValues = new Dictionary<string, object>()
                {
                    ["pUserId"] = objUserMaster.UserId
                };

                string query = $"DELETE FROM USER_MASTER WHERE USER_ID = :pUserId";

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
        public bool CheckDuplicateUserMaster(string userId)
        {
            try
            {
                string query = $"SELECT USER_ID FROM USER_MASTER WHERE USER_ID = '{userId}'";

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
        public DataTable FetchByUserId(UserMaster objUserMaster)
        {
            try
            {
                string query = $"SELECT * FROM USER_MASTER WHERE USER_ID = '{objUserMaster.UserId}'";
                DataTable dt = DbConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable FetchAllUserMaster()
        {
            try
            {
                string query = $"SELECT USER_ID, USER_NAME, " +
                    $"(SELECT CM_DESC FROM CODES_MASTER WHERE CM_CODE = USER_TYPE AND CM_TYPE = 'USER_TYPE') AS USER_TYPE, USER_PASSWORD " +
                    $"FROM USER_MASTER ORDER BY USER_CR_DT DESC";
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
