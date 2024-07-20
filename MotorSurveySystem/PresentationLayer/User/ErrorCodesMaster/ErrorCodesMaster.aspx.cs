using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.User.ErrorCodesMaster
{
    public partial class ErrorCodesMaster : System.Web.UI.Page
    {
        ErrorCodeMasterManager errorCodeMasterManager = new ErrorCodeMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ( Session["USER_ID"] != null && Session["USER_TYPE"].ToString() == "U" )
                {
                    if ( !IsPostBack )
                    {
                        BindErrorCodeMasterDetails();
                    }
                }
                else
                {
                    ErrorCodeMaster errorCodeMaster = new ErrorCodeMaster();
                    errorCodeMaster.ErrCode = "403";
                    string error = errorCodeMasterManager.FetchErrorCodeByErrCode(errorCodeMaster);
                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('ACCESS DENIED', '" + error + "','/Login.aspx');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnAddEcm_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/User/ErrorCodesMaster/AddErrorCodesMaster.aspx");
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        public void BindErrorCodeMasterDetails()
        {
            try
            {
                DataTable dt = errorCodeMasterManager.FetchAllErrorCodesMaster();

                if ( dt.Rows.Count > 0 )
                {
                    gvErrorCodeMaster.DataSource = dt;
                    gvErrorCodeMaster.DataBind();
                }
                else
                {
                    lblEmptyErrorCodesList.Text = "No Records Found!!!";
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void gvErrorCodeMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ErrorCodeMaster errorCodeMaster = new ErrorCodeMaster();
                errorCodeMaster.ErrCode = e.CommandArgument.ToString();

                if ( e.CommandName == "cmdEdit" )
                {
                    Response.Redirect("/User/ErrorCodesMaster/AddErrorCodesMaster?ERR_CODE=" + errorCodeMaster.ErrCode);

                }
                else if ( e.CommandName == "cmdDelete" )
                {
                    if ( errorCodeMasterManager.DeleteErrorCodesMaster(errorCodeMaster) )
                    {
                        errorCodeMaster.ErrCode = "103";
                        string message = errorCodeMasterManager.FetchErrorCodeByErrCode(errorCodeMaster);
                        ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS', '" + message + "','/User/ErrorCodesMaster/ErrorCodesMaster.aspx');", true);
                    }
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void gvErrorCodeMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvErrorCodeMaster.PageIndex = e.NewPageIndex;
                BindErrorCodeMasterDetails();
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
    }
}