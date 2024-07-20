using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.User.UsersMaster
{
    public partial class UsersMaster : System.Web.UI.Page
    {
        readonly UserMasterManager objUserMasterManager = new UserMasterManager();
        readonly ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ( !IsPostBack )
                {
                    BindUserMasterDetails();
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        public void BindUserMasterDetails()
        {
            try
            {
                DataTable dtUserMaster = objUserMasterManager.FetchAllUserMaster();

                if (dtUserMaster.Rows.Count > 0 )
                {
                    gvUserMaster.DataSource = dtUserMaster;
                    gvUserMaster.DataBind();
                }
                else
                {
                    lblEmptyUsersList.Text = "No Records Found!!!";
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }

        }
        protected void btnAddUserMaster_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/User/UsersMaster/AddUsersMaster.aspx");
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void gvUserMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string userId = e.CommandArgument.ToString();

                if  ( e.CommandName == "cmdEdit" )
                {
                    Response.Redirect("/User/UsersMaster/AddUsersMaster?USER_ID=" + userId);
                }
                else if ( e.CommandName == "cmdDelete" )
                {
                    UserMaster objUserMaster = new UserMaster();
                    objUserMaster.UserId = userId;

                    if (objUserMasterManager.DeleteUserMasterByUserId(objUserMaster) )
                    {
                        ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                        objErrorCodeMaster.ErrCode = "103";
                        string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);
                        ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS', '" + message + "'," +
                            "'/User/UsersMaster/UsersMaster.aspx');", true);
                    }
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void gvUserMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvUserMaster.PageIndex = e.NewPageIndex;
                BindUserMasterDetails();
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
    }
}