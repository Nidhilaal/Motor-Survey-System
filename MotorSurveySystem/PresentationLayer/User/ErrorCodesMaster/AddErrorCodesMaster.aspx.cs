using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.User.ErrorCodesMaster
{
    public partial class AddErrorCodesMaster : System.Web.UI.Page
    {
        readonly ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        readonly CodeMasterManager objCodeMasterManager = new CodeMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ( Session["USER_ID"] != null && Session["USER_TYPE"].ToString() == "U" )
                {
                    if ( !IsPostBack )
                    {
                        CodeMaster objCodeMaster = new CodeMaster();
                        objCodeMaster.CmType = "ECM_CODES";
                        DataTable dtErrType = objCodeMasterManager.FillDropDownList(objCodeMaster);
                        ddlErrType.DataSource = dtErrType;
                        ddlErrType.DataTextField = "CM_DISPLAY";
                        ddlErrType.DataValueField = "CM_DESC";
                        ddlErrType.DataBind();
                        ddlErrType.Items.Insert(0, new ListItem("--select--", "NA"));

                        if ( Request.QueryString["ERR_CODE"] != null ) 
                        {
                            ErrorCodeMaster errorCodeMaster = new ErrorCodeMaster();
                            errorCodeMaster.ErrCode = Request.QueryString["ERR_CODE"];
                          
                            DataTable dtErrorcodeMaster = objErrorCodeMasterManager.FetchByErrcode(errorCodeMaster);

                            txtErrCode.Text = dtErrorcodeMaster.Rows[0]["ERR_CODE"].ToString();
                            txtErrCode.ReadOnly = true;
                            ddlErrType.SelectedValue = dtErrorcodeMaster.Rows[0]["ERR_TYPE"].ToString();
                            txtErrDesc.Text = dtErrorcodeMaster.Rows[0]["ERR_DESC"].ToString();

                            btnSaveErrorCodesMaster.Visible = false;
                            btnAddNew.Visible = true;
                        }
                        else
                        {
                            btnUpdateErrorCodesMaster.Visible = false;
                            btnAddNew.Visible = false;
                        }
                    }
                }
               
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "403";
                    string error = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);
                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessageRedirect('ACCESS DENIED'," +
                        "'" + error + "','/Login.aspx');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnSaveErrorCodesMaster_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorCodeMaster errorCodeMaster = new ErrorCodeMaster();
                errorCodeMaster.ErrCode = txtErrCode.Text.Trim();
                errorCodeMaster.ErrType = ddlErrType.SelectedValue;
                errorCodeMaster.ErrDesc = txtErrDesc.Text.Trim();
                errorCodeMaster.ErrCrBy = Session["USER_ID"].ToString();
                errorCodeMaster.ErrCrDt = DateTime.Now.Date;

                if ( !objErrorCodeMasterManager.CheckDuplicateErrorCodesMaster(errorCodeMaster ) &&
                    objErrorCodeMasterManager.SaveErrorCodesMaster(errorCodeMaster) )
                {
                    ErrorCodeMaster objErrorCodeMasterTemp = new ErrorCodeMaster();
                    objErrorCodeMasterTemp.ErrCode = "101";
                    string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMasterTemp);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS','" + errMessage + "'," +
                     "'/User/ErrorCodesMaster/AddErrorCodesMaster?ERR_CODE=" + errorCodeMaster.ErrCode + "');", true);
                             
                }
                else
                {
                    ErrorCodeMaster objErrorCodeMasterTemp = new ErrorCodeMaster();
                    objErrorCodeMasterTemp.ErrCode = "201";
                    string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMasterTemp);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessage('ERROR','" + errMessage + "');", true);

                    txtErrCode.Text = string.Empty;
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnUpdateErrorCodesMaster_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                objErrorCodeMaster.ErrCode = txtErrCode.Text.Trim();
                objErrorCodeMaster.ErrType = ddlErrType.SelectedValue;
                objErrorCodeMaster.ErrDesc = txtErrDesc.Text.Trim();
                objErrorCodeMaster.ErrUpBy = Session["USER_ID"].ToString();
                objErrorCodeMaster.ErrUpDt = DateTime.Now.Date;

                if ( objErrorCodeMasterManager.UpdateErrorCodesMaster(objErrorCodeMaster) )
                {

                    ErrorCodeMaster objErrorCodeMasterTemp = new ErrorCodeMaster();
                    objErrorCodeMasterTemp.ErrCode = "102";
                    string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMasterTemp);                 

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS','" + errMessage + "'," +
                        "'/User/ErrorCodesMaster/AddErrorCodesMaster?ERR_CODE=" + objErrorCodeMaster.ErrCode + "');", true);

                }
                else
                {
                    ErrorCodeMaster objErrorCodeMasterTemp = new ErrorCodeMaster();
                    objErrorCodeMasterTemp.ErrCode = "301";
                    string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMasterTemp);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessage('ERROR','" + errMessage + "');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/User/ErrorCodesMaster/ErrorCodesMaster.aspx");

            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/User/ErrorCodesMaster/AddErrorCodesMaster.aspx");

            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void btnResetErrorCodesMaster_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ERR_CODE"] != null)
            {
                ErrorCodeMaster errorCodeMaster = new ErrorCodeMaster();
                errorCodeMaster.ErrCode = Request.QueryString["ERR_CODE"];

                DataTable dtErrorcodeMaster = objErrorCodeMasterManager.FetchByErrcode(errorCodeMaster);

                txtErrCode.Text = dtErrorcodeMaster.Rows[0]["ERR_CODE"].ToString();
                txtErrCode.ReadOnly = true;
                ddlErrType.SelectedValue = dtErrorcodeMaster.Rows[0]["ERR_TYPE"].ToString();
                txtErrDesc.Text = dtErrorcodeMaster.Rows[0]["ERR_DESC"].ToString();

            }
            else
            {
                txtErrCode.Text = string.Empty;
                ddlErrType.SelectedValue = "NA";
                txtErrDesc.Text = string.Empty;
            }
        }
    }
}