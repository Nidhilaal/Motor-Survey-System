using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.User.CodesMaster
{
    public partial class CodesMaster : System.Web.UI.Page
    {
        readonly CodeMasterManager objCodeMasterManager = new CodeMasterManager();
        readonly ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ( Session["USER_ID"] != null && Session["USER_TYPE"].ToString() == "U" )
                {
                    if ( !IsPostBack )
                    {
                        BindCodeMasterDetails();
                    }
                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "403";
                    string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);
                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessageRedirect('ACCESS DENIED'," +
                        "'" + errMessage + "','/Login.aspx');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }

        }
        protected void btnAddCm_click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/User/CodesMaster/AddCodesMaster.aspx");
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message + "');", true); }
        }
        public void BindCodeMasterDetails()
        {
            try
            {
                DataTable dtCodeMaster = objCodeMasterManager.FetchAllCodesMaster();

                if ( dtCodeMaster.Rows.Count > 0 )
                {
                    gvCodeMaster.DataSource = dtCodeMaster;
                    gvCodeMaster.DataBind();

                }
                else
                {
                    lblEmptyCodesMasterList.Text = "No Records Found!!!";
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void gvCode_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string commandArgument = e.CommandArgument.ToString();
                string[] arguments = commandArgument.Split('\u002C');

                if ( e.CommandName == "cmdEdit" )
                {
                    string cmCode = arguments[0];
                    string cmType = arguments[1];
                    Response.Redirect("/User/CodesMaster/AddCodesMaster?CM_CODE=" + cmCode + "&CM_TYPE=" + cmType);
                }
                else if ( e.CommandName == "cmdDelete" )
                {
                    CodeMaster objCodeMaster = new CodeMaster();
                    objCodeMaster.CmCode = arguments[0];
                    objCodeMaster.CmType = arguments[1];

                    if ( objCodeMasterManager.DeleteCodesMaster(objCodeMaster) )
                    {
                        ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                        objErrorCodeMaster.ErrCode = "103";
                        string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);
                        ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS', '" + errMessage + "'," +
                            "'/User/CodesMaster/CodesMaster.aspx');", true);
                    }
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void gvCodeMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCodeMaster.PageIndex = e.NewPageIndex;
                BindCodeMasterDetails();
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
    }
}