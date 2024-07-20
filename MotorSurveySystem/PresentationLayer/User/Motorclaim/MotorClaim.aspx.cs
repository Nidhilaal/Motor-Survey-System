using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.User
{
    public partial class MotorClaim : System.Web.UI.Page
    {
        readonly ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        readonly ClaimManager objClaimManager = new ClaimManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] != null && Session["USER_TYPE"].ToString() == "U")
                {
                    if (!IsPostBack)
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
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('EXCEPTION','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnAddClaim_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/User/MotorClaim/AddMotorClaim.aspx");
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('EXCEPTION','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        public void BindClaimDetails()
        {
            try
            {
                DataTable dtClaim = objClaimManager.FetchAllClaim();

                if ( dtClaim.Rows.Count > 0 )
                {
                    gvClaim.DataSource = dtClaim;
                    gvClaim.DataBind();

                }
                else
                {
                    lblEmptyClaimTable.Text = "No records Found!!!";
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('EXCEPTION','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }


        protected void gvClaim_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if ( e.CommandName == "cmdEdit" )
                {
                    string clmUid = e.CommandArgument.ToString();
                    Response.Redirect("/User/MotorClaim/AddMotorClaim?CLM_UID=" + clmUid);
                }
                else if ( e.CommandName == "cmdDelete" )
                {
                    string clmUid = e.CommandArgument.ToString();

                    Claim objClaim = new Claim();
                    objClaim.ClmUid = int.Parse(clmUid);

                    if ( objClaimManager.DeleteClaim(objClaim) )
                    {
                        ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                        objErrorCodeMaster.ErrCode = "103";
                        string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);
                        ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS', '" + message + "'," +
                            "'/User/MotorClaim/MotorClaim.aspx');", true);
                    }

                }
                else if ( e.CommandName == "cmdView" )
                {
                    string clmUid = e.CommandArgument.ToString();
                    Response.Redirect("/User/MotorClaim/AddMotorClaim?CLM_UID=" + clmUid);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('EXCEPTION','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void gvClaim_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if ( e.Row.RowType == DataControlRowType.DataRow )
                {
                    DataRowView dataItem = (DataRowView)e.Row.DataItem;

                    Button btnUpdate = (Button)e.Row.FindControl("btnEdit");
                    Button btnRemove = (Button)e.Row.FindControl("btnDelete");
                    Button btnView = (Button)e.Row.FindControl("btnView");

                    if (dataItem["CLM_SUR_CR_YN"].ToString() == "Yes")
                    {
                        btnUpdate.Visible = false;
                        btnRemove.Visible = false;
                        btnView.Visible = true;
                    }
                    else
                    {
                        btnView.Visible = false;
                    }
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('EXCEPTION','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void gvClaim_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvClaim.PageIndex = e.NewPageIndex;
                BindClaimDetails();
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('EXCEPTION','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        //Do the sorting function later if needed
    }
}