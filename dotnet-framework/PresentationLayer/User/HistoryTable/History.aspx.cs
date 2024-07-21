using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.User.HistoryTable
{
    public partial class History : System.Web.UI.Page
    { 
        readonly ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        readonly MotorClmSurDtlHistManager objMotorClmSurDtlHistManager = new MotorClmSurDtlHistManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ( Session["USER_ID"] != null && Session["USER_TYPE"].ToString() == "U" )
                {
                    BindSurveyHistoryDetails();
                }             
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "403";
                    string error = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);
                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('ACCESS DENIED'," +
                        "'" + error + "','/Login.aspx');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        public void BindSurveyHistoryDetails()
        {
            try
            {
                DataTable dt = objMotorClmSurDtlHistManager.FetchAllSurveyHistory();

                gvHistoryTable.DataSource = dt;
                gvHistoryTable.DataBind();
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }

        }

        protected void gvHistoryTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvHistoryTable.PageIndex = e.NewPageIndex;
                BindSurveyHistoryDetails();
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
    }
}