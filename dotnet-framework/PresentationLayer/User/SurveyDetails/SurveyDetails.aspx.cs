using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;

namespace PresentationLayer.User.SurveyDetails
{
    public partial class SurveyDetails : System.Web.UI.Page
    {
        
        readonly ClaimManager objClaimManager = new ClaimManager();
        readonly MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();
        readonly MotorClmSurDtlManager objMotorClmSurDtlManager = new MotorClmSurDtlManager();
        readonly ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ( Session["USER_ID"] != null && Session["USER_TYPE"].ToString() == "U" )
                {
                    if ( !IsPostBack )
                    {
                        if ( Request.QueryString["SUR_UID"] != null )
                        {
                            MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
                            objMotorClmSurDtl.SurdSurUid = int.Parse(Request.QueryString["SUR_UID"]);

                            MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                            objMotorClmSurHdr.SurUid = int.Parse(Request.QueryString["SUR_UID"]);

                            hfSurUid.Value = Request.QueryString["SUR_UID"];

                            BindSurveyDetails(objMotorClmSurDtl);

                            DataTable dtMotorClmSurHdr = objMotorClmSurHdrManager.FetchBySurUid(objMotorClmSurHdr);

                            lblSurClmNo.Text = dtMotorClmSurHdr.Rows[0]["SUR_CLM_NO"].ToString();
                            lblSurNo.Text = dtMotorClmSurHdr.Rows[0]["SUR_NO"].ToString();
                            lblSurChassisNo.Text = dtMotorClmSurHdr.Rows[0]["SUR_CHASSIS_NO"].ToString();
                            lblSurRegnNo.Text = dtMotorClmSurHdr.Rows[0]["SUR_REGN_NO"].ToString();
                            lblEngineNo.Text = dtMotorClmSurHdr.Rows[0]["SUR_ENGINE_NO"].ToString();
                            lblSurcurr.Text = dtMotorClmSurHdr.Rows[0]["SUR_CURR"].ToString();
                            lblSurFcAmt.Text = ((decimal)dtMotorClmSurHdr.Rows[0]["SUR_FC_AMT"]).ToString("N2");
                            lblSurLcAmt.Text = ((decimal)dtMotorClmSurHdr.Rows[0]["SUR_LC_AMT"]).ToString("N2"); 
                            lblClmPolNo.Text = dtMotorClmSurHdr.Rows[0]["CLM_POL_NO"].ToString();
                            lblSurApprSts.Text = dtMotorClmSurHdr.Rows[0]["SUR_APPR_STS"].ToString(); 

                            if ( dtMotorClmSurHdr.Rows[0]["SUR_APPR_STS"].ToString().Equals("Not Approved") )
                            {
                                btnGenerateReport.Visible = false;
                            }
                            else
                            {
                                btnApproveSurvey.Visible = false;
                                btnRejectSurvey.Visible = false;
                                btnGenerateReport.Visible = true;
                            }
                        }
                    }            
                }              
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "403";
                    string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);
                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('ACCESS DENIED', '" + errMessage + "','/Login.aspx');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        public void BindSurveyDetails(MotorClmSurDtl objMotorClmSurDtl)
        {
            try
            {
                DataTable dtMotorClmSurDtl = objMotorClmSurDtlManager.FetchBySurdSurUid(objMotorClmSurDtl);

                gvSurveyDetails.DataSource = dtMotorClmSurDtl;
                gvSurveyDetails.DataBind();
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }

        }
        protected void btnApproveSurvey_Click(object sender, EventArgs e)
        {
            try
            {
                MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                objMotorClmSurHdr.SurApprSts = "A";
                objMotorClmSurHdr.SurUid = int.Parse(hfSurUid.Value);
                objMotorClmSurHdr.SurApprBy = Session["USER_ID"].ToString();
                objMotorClmSurHdr.SurApprDt = DateTime.Now.Date;

                objMotorClmSurHdrManager.UpdateSurveyStatus(objMotorClmSurHdr);

                Claim claim = new Claim();
                claim.ClmSurApprYn = "Y";
                claim.ClmNo = lblSurClmNo.Text.ToString();

                if ( objClaimManager.UpdateAppovalStatus(claim) )
                {
                    ErrorCodeMaster errorCodeMaster = new ErrorCodeMaster();
                    errorCodeMaster.ErrCode = "105";
                    string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(errorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "approveAlert", "showSuccessMessageRedirect('SURVEY APPROVED','" + errMessage + "'," +
                       "'/User/SurveyDetails/SurveyDetails?SUR_UID=" + Request.QueryString["SUR_UID"] + "');", true);

                    btnApproveSurvey.Visible = false;
                    btnRejectSurvey.Visible = false;
                    btnGenerateReport.Visible = true;
                }
                
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnRejectSurvey_Click(object sender, EventArgs e)
        {
            try
            {
                MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                objMotorClmSurHdr.SurApprSts = "R";
                objMotorClmSurHdr.SurUid = int.Parse(hfSurUid.Value);
                objMotorClmSurHdr.SurApprBy = Session["USER_ID"].ToString();
                objMotorClmSurHdr.SurApprDt = DateTime.Now.Date;

                objMotorClmSurHdrManager.UpdateSurveyStatus(objMotorClmSurHdr);

                Claim claim = new Claim();
                claim.ClmSurApprYn = "N";
                claim.ClmNo = lblSurClmNo.Text.ToString();

                if(objClaimManager.UpdateAppovalStatus(claim))
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "302";
                    string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "rejectAlert", "showSuccessMessageRedirect('SURVEY REJECTED','" + errMessage + "'," +
                       "'/User/SurveyDetails/SurveyDetails?SUR_UID=" + Request.QueryString["SUR_UID"] + "');", true);

                    btnApproveSurvey.Visible = false;
                    btnRejectSurvey.Visible = false;
                    btnGenerateReport.Visible = true;
                }
               
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }

        }
        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {   
                Response.Redirect("/Report/ReportPage?SUR_UID=" + Request.QueryString["SUR_UID"].ToString());
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

    }
}