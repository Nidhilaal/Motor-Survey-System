using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.User.UsersMaster
{
    public partial class AddUsersMaster : System.Web.UI.Page
    {
        readonly CodeMasterManager objCodeMasterManager = new CodeMasterManager();
        readonly ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        readonly UserMasterManager objUserMasterManager = new UserMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CodeMaster objCodeMaster = new CodeMaster();
                    objCodeMaster.CmType = "USER_TYPE";

                    DataTable dtUserType = objCodeMasterManager.FillDropDownList(objCodeMaster);
                    ddlUserType.DataSource = dtUserType;
                    ddlUserType.DataTextField = "CM_DISPLAY";
                    ddlUserType.DataValueField = "CM_CODE";
                    ddlUserType.DataBind();
                    ddlUserType.Items.Insert(0, new ListItem("--select--", "NA"));

                    if (Request.QueryString["USER_ID"] != null)
                    {
                        UserMaster objUserMaster = new UserMaster();
                        objUserMaster.UserId = Request.QueryString["USER_ID"].ToString();

                        DataTable dtUserMaster = objUserMasterManager.FetchByUsedId(objUserMaster);

                        txtUserId.Text = dtUserMaster.Rows[0]["USER_ID"].ToString();
                        txtUserId.ReadOnly = true;
                        txtUsername.Text = dtUserMaster.Rows[0]["USER_NAME"].ToString();
                        txtPassword.Text = dtUserMaster.Rows[0]["USER_PASSWORD"].ToString();
                        ddlUserType.SelectedValue = dtUserMaster.Rows[0]["USER_TYPE"].ToString();

                        btnSaveUser.Visible = false;
                        btnAddNew.Visible = true;

                    }
                    else
                    {
                        btnUpdateUser.Visible = false;
                        btnAddNew.Visible = false;
                    }
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            try
            {
                UserMaster objUserMaster = new UserMaster();
                objUserMaster.UserId = txtUserId.Text.ToString().Trim();
                objUserMaster.UserName = txtUsername.Text.ToString().Trim();
                objUserMaster.UserPassword = txtPassword.Text.ToString().Trim();
                objUserMaster.UserType = ddlUserType.SelectedValue.ToString();
                objUserMaster.UserCrBy = Session["USER_ID"].ToString();
                objUserMaster.UserCrDt = DateTime.Now.Date;

                UserMasterManager userMasterManager = new UserMasterManager();

                if (!userMasterManager.CheckDuplicateUserMaster(objUserMaster) && userMasterManager.SaveUserMaster(objUserMaster))
                {
                    ErrorCodeMaster tempErrorCodeMaster = new ErrorCodeMaster();
                    tempErrorCodeMaster.ErrCode = "101";
                    string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(tempErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS','" + errMessage + "'," +
                        "'/User/UsersMaster/AddUsersMaster?USER_ID=" + objUserMaster.UserId + "');", true);
                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "201";
                    string error = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessage('ERROR','" + error + "');", true);

                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            try
            {
                UserMaster objUserMaster = new UserMaster();
                objUserMaster.UserId = txtUserId.Text.ToString().Trim();
                objUserMaster.UserName = txtUsername.Text.ToString().Trim();
                objUserMaster.UserPassword = txtPassword.Text.ToString().Trim();
                objUserMaster.UserType = ddlUserType.SelectedValue.ToString();
                objUserMaster.UserUpBy = Session["USER_ID"].ToString();
                objUserMaster.UserUpDt = DateTime.Now.Date;

                if (objUserMasterManager.UpdateUserMaster(objUserMaster))
                {
                    ErrorCodeMaster objErrorCodeMasterTemp = new ErrorCodeMaster();
                    objErrorCodeMasterTemp.ErrCode = "102";
                    string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMasterTemp);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS','" + errMessage + "'," +
                        "'/User/UsersMaster/AddUsersMaster?USER_ID=" + objUserMaster.UserId + "');", true);

                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "301";
                    string error = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessage('ERROR','" + error + "');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/User/UsersMaster/UsersMaster.aspx");
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/User/UsersMaster/AddUsersMaster.aspx");
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void btnResetUserMaster_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["USER_ID"] != null)
            {
                UserMaster objUserMaster = new UserMaster();
                objUserMaster.UserId = Request.QueryString["USER_ID"].ToString();

                DataTable dtUserMaster = objUserMasterManager.FetchByUsedId(objUserMaster);

                txtUserId.Text = dtUserMaster.Rows[0]["USER_ID"].ToString();
                txtUserId.ReadOnly = true;
                txtUsername.Text = dtUserMaster.Rows[0]["USER_NAME"].ToString();
                txtPassword.Text = dtUserMaster.Rows[0]["USER_PASSWORD"].ToString();
                ddlUserType.SelectedValue = dtUserMaster.Rows[0]["USER_TYPE"].ToString();

            }
            else
            {
                txtUserId.Text = string.Empty;
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                ddlUserType.SelectedValue = "NA";
            }
        }
    }
}