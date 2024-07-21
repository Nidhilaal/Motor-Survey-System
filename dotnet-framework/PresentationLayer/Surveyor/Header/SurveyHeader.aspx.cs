using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.Surveyor.Header
{
    public partial class SurveyHeader : System.Web.UI.Page
    {
        readonly MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();
        readonly ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ( Session["USER_ID"] != null && Session["USER_TYPE"].ToString() == "S" )
                {
                    if ( !IsPostBack )
                    {
                        MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                        objMotorClmSurHdr.SurCrBy = Session["USER_ID"].ToString();
                        BindSurveyHeaderDetails(objMotorClmSurHdr);
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
        public void BindSurveyHeaderDetails(MotorClmSurHdr objMotorClmSurHdr)
        {
            try
            {
                DataTable dtSurHdrDtl = objMotorClmSurHdrManager.FetchAllSurvey(objMotorClmSurHdr);

                if (dtSurHdrDtl.Rows.Count > 0 )
                {
                    gvSurveyHeader.DataSource = dtSurHdrDtl;
                    gvSurveyHeader.DataBind();
                }
                else
                {
                    lblEmptySurveyHeader.Text = "No Records Found!!!";
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void gvSurveyHeader_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if (e.CommandName == "cmdEdit")
                {
                    string surUid = e.CommandArgument.ToString();
                    Response.Redirect("/Surveyor/Header/AddSurveyHeader?SUR_UID=" + surUid);
                }
                else if (e.CommandName == "cmdView")
                {
                    string surUid = e.CommandArgument.ToString();
                    Response.Redirect("/Surveyor/Header/AddSurveyHeader?SUR_UID=" + surUid);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }

        }
        protected void gvSurveyHeader_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dataItem = (DataRowView)e.Row.DataItem;
                    Button btnUpdate = (Button)e.Row.FindControl("btnEditSurvey");
                    Button btnView = (Button)e.Row.FindControl("btnViewSurvey");

                    if (dataItem["SUR_STATUS"].ToString() == "Submitted")
                    {                       
                        btnUpdate.Visible = false;                       
                        btnView.Visible = true;
                    }
                    else
                    {                      
                        btnView.Visible = false;
                    }
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void gvSurveyHeader_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvSurveyHeader.PageIndex = e.NewPageIndex;
                MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                objMotorClmSurHdr.SurCrBy = Session["USER_ID"].ToString();
                BindSurveyHeaderDetails(objMotorClmSurHdr);
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
    }
}