using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.Surveyor.Claims
{
    public partial class ClaimsList : System.Web.UI.Page
    {
        readonly ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        readonly ClaimManager objClaimManager = new ClaimManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ( Session["USER_ID"] != null && Session["USER_TYPE"].ToString() == "S" )
                {
                    if (IsPostBack)
                    {
                        
                    }
                    else
                    {
                        BindClaimDetails();
                    }
                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "403";
                    string error = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);
                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('ACCESS DENIED', '" + error + "','/Login.aspx');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        public void BindClaimDetails()
        {
            try
            {
                DataTable dtClaim = objClaimManager.FetchAllClaimForSurveyor();

                if ( dtClaim.Rows.Count > 0 )
                {
                    gvClaim.DataSource = dtClaim;
                    gvClaim.DataBind();
                }
                else
                {
                    lblEmptyClaimsList.Text = "No Records Found!!!";
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void gvClaim_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string commandArgument = e.CommandArgument.ToString();
                string[] arguments = commandArgument.Split('\u002C');

                if ( e.CommandName == "cmdView" )
                {
                    string clmUid = arguments[0];
                    string clmNo = arguments[1];

                    Response.Redirect("/Surveyor/Header/AddSurveyHeader?CLM_UID=" + clmUid + "&CLM_NO=" + clmNo);

                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void gvClaim_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvClaim.PageIndex = e.NewPageIndex;
                BindClaimDetails();
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
    }
}