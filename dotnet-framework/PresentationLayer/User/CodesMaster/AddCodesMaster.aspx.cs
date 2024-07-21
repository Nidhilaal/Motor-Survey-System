


using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;

namespace PresentationLayer.User.CodesMaster
{
    public partial class AddCodesMaster : System.Web.UI.Page
    {
        readonly CodeMasterManager objCodeMasterManager = new CodeMasterManager();
        readonly ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] != null && Session["USER_TYPE"].ToString() == "U")
                {
                    if (!IsPostBack)
                    {
                        if (Request.QueryString["CM_CODE"] != null && Request.QueryString["CM_TYPE"] != null)
                        {
                            CodeMaster objCodeMaster = new CodeMaster();
                            objCodeMaster.CmCode = Request.QueryString["CM_CODE"];
                            objCodeMaster.CmType = Request.QueryString["CM_TYPE"];

                            DataTable dtCodesMaster = objCodeMasterManager.FetchByCmcode(objCodeMaster);

                            txtCmCode.Text = dtCodesMaster.Rows[0]["CM_CODE"].ToString();
                            txtCmCode.ReadOnly = true;
                            txtCmType.Text = dtCodesMaster.Rows[0]["CM_TYPE"].ToString();
                            txtCmType.ReadOnly = true;
                            txtCmDesc.Text = dtCodesMaster.Rows[0]["CM_DESC"].ToString();
                            txtCmValue.Text = dtCodesMaster.Rows[0]["CM_VALUE"].ToString();

                            btnSaveCodesMaster.Visible = false;
                            btnAddNew.Visible = true;                                                 
                            chkActiveCodeMaster.Visible = true;

                            if (dtCodesMaster.Rows[0]["CM_ACTIVE_YN"].ToString() == "Y")
                            {
                                chkActiveCodeMaster.Checked = true;
                            }
                            else
                            {
                                chkActiveCodeMaster.Checked = false;
                            }
                        }
                        else
                        {
                            btnUpdateCodesMaster.Visible = false;
                            btnAddNew.Visible = false;
                            chkActiveCodeMaster.Visible= false;
                            lblActiveCodeMaster.Visible = false;
                        }
                    }
                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "403";
                    string error = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);
                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessageRedirect('ACCESS DENIED', '" + error + "'," +
                        "'/Login.aspx');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnSaveCodesMaster_Click(object sender, EventArgs e)
        {
            try
            {
                CodeMaster objCodeMaster = new CodeMaster();
                objCodeMaster.CmCode = txtCmCode.Text.Trim();
                objCodeMaster.CmType = txtCmType.Text.ToUpper().Trim();

                if ( !string.IsNullOrEmpty(txtCmValue.Text) )
                {
                    objCodeMaster.CmValue = double.Parse(txtCmValue.Text);
                }

                objCodeMaster.CmDesc = txtCmDesc.Text;

             
                objCodeMaster.CmCrBy = Session["USER_ID"].ToString();
                objCodeMaster.CmCrDt = DateTime.Now.Date;

                if ( !objCodeMasterManager.CheckDuplicateCodesMaster(objCodeMaster) && objCodeMasterManager.SaveCodesMaster(objCodeMaster) )
                {
                
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "101";
                    string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS','" + message + "'," +
                       "'/User/CodesMaster/AddCodesMaster?CM_CODE=" + objCodeMaster.CmCode + "&CM_TYPE=" + objCodeMaster.CmType + " ');", true);

                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "201";
                    string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessage('ERROR','" + errMessage + "');", true);

                    txtCmCode.Text = string.Empty;
                    txtCmType.Text = string.Empty;
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
;
        }
        protected void btnUpdateCodesMaster_Click(object sender, EventArgs e)
        {
            try
            {
                CodeMaster objCodeMaster = new CodeMaster();
                objCodeMaster.CmCode = txtCmCode.Text.Trim();
                objCodeMaster.CmType = txtCmType.Text.ToUpper().Trim();
                objCodeMaster.CmDesc = txtCmDesc.Text;

                if (!string.IsNullOrEmpty(txtCmValue.Text))
                {
                    objCodeMaster.CmValue = double.Parse(txtCmValue.Text);
                }

                if (chkActiveCodeMaster.Checked)
                {
                    objCodeMaster.CmActiveYn = "Y";
                }
                else
                {
                    objCodeMaster.CmActiveYn = "N";
                }
                objCodeMaster.CmUpBy = Session["USER_ID"].ToString();
                objCodeMaster.CmUpDt = DateTime.Now.Date;

                if ( objCodeMasterManager.UpdateCodesMaster(objCodeMaster) )
                {
                    ErrorCodeMaster errorCodeMaster = new ErrorCodeMaster();
                    errorCodeMaster.ErrCode = "102";
                    string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(errorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS','" + message + "'," +
                       "'/User/CodesMaster/AddCodesMaster?CM_CODE=" + objCodeMaster.CmCode + "&CM_TYPE=" + objCodeMaster.CmType + " ');", true);
                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "301";
                    string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessage('ERROR','" + errMessage + "');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {

            try
            {
                Response.Redirect("/User/CodesMaster/CodesMaster.aspx");
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message + "');", true); }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/User/CodesMaster/AddCodesMaster.aspx");

            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void btnResetCodesMaster_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["CM_CODE"] != null && Request.QueryString["CM_TYPE"] != null)
            {
                CodeMaster objCodeMaster = new CodeMaster();
                objCodeMaster.CmCode = Request.QueryString["CM_CODE"];
                objCodeMaster.CmType = Request.QueryString["CM_TYPE"];

                DataTable dtCodesMaster = objCodeMasterManager.FetchByCmcode(objCodeMaster);

                txtCmCode.Text = dtCodesMaster.Rows[0]["CM_CODE"].ToString();
                txtCmCode.ReadOnly = true;
                txtCmType.Text = dtCodesMaster.Rows[0]["CM_TYPE"].ToString();
                txtCmType.ReadOnly = true;
                txtCmDesc.Text = dtCodesMaster.Rows[0]["CM_DESC"].ToString();
                txtCmValue.Text = dtCodesMaster.Rows[0]["CM_VALUE"].ToString();            

                if (dtCodesMaster.Rows[0]["CM_ACTIVE_YN"].ToString() == "Y")
                {
                    chkActiveCodeMaster.Checked = true;
                }
                else
                {
                    chkActiveCodeMaster.Checked = false;
                }
            }
            else
            {
                txtCmCode.Text = string.Empty;            
                txtCmType.Text = string.Empty;
                txtCmDesc.Text = string.Empty;
                txtCmValue.Text = string.Empty;
            }
        }
    }
}