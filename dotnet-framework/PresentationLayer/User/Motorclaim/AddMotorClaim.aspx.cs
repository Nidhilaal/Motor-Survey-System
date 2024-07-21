using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;

namespace PresentationLayer.User.Motorclaim
{
    public partial class AddMotorClaim : System.Web.UI.Page
    {
        readonly ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        readonly ClaimManager objClaimManager = new ClaimManager();
        readonly MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] != null && Session["USER_TYPE"].ToString() == "U")
                {
                    if (!IsPostBack)
                    {
                        if (Request.QueryString["CLM_UID"] != null)
                        {
                            Claim objClaim = new Claim();
                            objClaim.ClmUid = int.Parse(Request.QueryString["CLM_UID"]);

                            DataTable dtClaim = objClaimManager.FetchByClmUid(objClaim);

                            hfClmUid.Value = Request.QueryString["CLM_UID"];
                            txtClaimPolNo.Text = dtClaim.Rows[0]["CLM_POL_NO"].ToString();

                            DateTime clmFmDt = (DateTime)dtClaim.Rows[0]["CLM_POL_FM_DT"];
                            txtClaimFromDate.Text = clmFmDt.ToString("dd-MM-yyyy");

                            DateTime clmToDt = (DateTime)dtClaim.Rows[0]["CLM_POL_TO_DT"];
                            txtClaimToDate.Text = clmToDt.ToString("dd-MM-yyyy");

                            DateTime clmLossDt = (DateTime)dtClaim.Rows[0]["CLM_LOSS_DT"];
                            txtClaimPolicyLossDate.Text = clmLossDt.ToString("dd-MM-yyyy");

                            DateTime clmIntmDt = (DateTime)dtClaim.Rows[0]["CLM_INTM_DT"];
                            txtClaimIntmDate.Text = clmIntmDt.ToString("dd-MM-yyyy");

                            DateTime clmRegnDate = (DateTime)dtClaim.Rows[0]["CLM_REG_DT"];
                            txtClaimRegnDate.Text = clmIntmDt.ToString("dd-MM-yyyy");

                            txtClaimPolRepNo.Text = dtClaim.Rows[0]["CLM_POL_REP_NO"].ToString();
                            txtClaimLossDesc.Text = dtClaim.Rows[0]["CLM_LOSS_DESC"].ToString();
                            txtVehChassisNo.Text = dtClaim.Rows[0]["CLM_VEH_CHASSIS_NO"].ToString();
                            txtVehEngineNo.Text = dtClaim.Rows[0]["CLM_VEH_ENGINE_NO"].ToString();
                            txtVehRegnNo.Text = dtClaim.Rows[0]["CLM_VEH_REGN_NO"].ToString();
                            txtVehValue.Text = dtClaim.Rows[0]["CLM_VEH_VALUE"].ToString();

                            btnSaveClaim.Visible = false;
                            btnViewSurvey.Visible = false;
                            btnAddNew.Visible = true;

                            lblAddClaimDetails.Visible = false;
                            lbltxtClaimNo.Visible = true;
                            lblClaimNo.Visible = true;
                            lblClaimNo.Text = dtClaim.Rows[0]["CLM_NO"].ToString();
                            hfClmSurNo.Value = dtClaim.Rows[0]["CLM_SUR_NO"].ToString();

                            if (dtClaim.Rows[0]["CLM_SUR_CR_YN"].ToString() == "Y")
                            {
                                txtClaimPolNo.Enabled = false;
                                txtClaimFromDate.Enabled = false;
                                txtClaimToDate.Enabled = false;
                                txtClaimIntmDate.Enabled = false;
                                txtClaimPolicyLossDate.Enabled = false;
                                txtClaimRegnDate.Enabled = false;
                                txtClaimLossDesc.Enabled = false;
                                txtClaimPolRepNo.Enabled = false;
                                txtVehChassisNo.Enabled = false;
                                txtVehEngineNo.Enabled = false;
                                txtVehRegnNo.Enabled = false;
                                txtVehValue.Enabled = false;

                                btnViewSurvey.Visible = true;
                                btnUpdateClaim.Visible = false;
                                btnResetClaim.Visible = false;
                            }
                        }
                        else
                        {
                            btnUpdateClaim.Visible = false;
                            lbltxtClaimNo.Visible = false;
                            lblClaimNo.Visible = false;
                            btnViewSurvey.Visible = false;
                            btnAddNew.Visible = false;
                        }
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
        protected void btnSaveClaim_Click(object sender, EventArgs e)
        {
            try
            {
                Claim objClaim = new Claim();
                objClaim.ClmPolNo = txtClaimPolNo.Text.Trim();
                objClaim.ClmPolFmDt = Convert.ToDateTime(txtClaimFromDate.Text).Date;
                objClaim.ClmPolToDt = Convert.ToDateTime(txtClaimToDate.Text).Date;
                objClaim.ClmLossDt = Convert.ToDateTime(txtClaimPolicyLossDate.Text).Date;
                objClaim.ClmIntmDt = Convert.ToDateTime(txtClaimIntmDate.Text).Date;
                objClaim.ClmRegDt = Convert.ToDateTime(txtClaimRegnDate.Text).Date;
                objClaim.ClmPolRepNo = txtClaimPolRepNo.Text.Trim();
                objClaim.ClmLossDesc = txtClaimLossDesc.Text.Trim();
                objClaim.ClmVehChassisNo = txtVehChassisNo.Text.Trim();
                objClaim.ClmVehEngineNo = txtVehEngineNo.Text.Trim();
                objClaim.ClmVehRegnNo = txtVehRegnNo.Text.Trim();
                objClaim.ClmVehValue = double.Parse(txtVehValue.Text);
                objClaim.ClmCrBy = Session["USER_ID"].ToString();
                objClaim.ClmCrDt = DateTime.Now.Date;

                if (!objClaimManager.CheckDuplicatePolRepNo(objClaim))
                {
                    objClaim.ClmUid = objClaimManager.GetClaimSequence();
                    objClaim.ClmNo = "C/" + objClaim.ClmPolFmDt.Year.ToString() + "/" + objClaim.ClmUid.ToString().PadLeft(6, '0');

                    if (objClaimManager.SaveClaim(objClaim))
                    {
                        ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                        objErrorCodeMaster.ErrCode = "101";
                        string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                        ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS','" + message + "<br>Claim No:" + objClaim.ClmNo + "'," +
                            "'/User/MotorClaim/AddMotorClaim?CLM_UID=" + objClaim.ClmUid + "');", true);
                    }

                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "201";
                    string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessage('FAILED','" + message + "');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('EXCEPTION','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnUpdateClaim_Click(object sender, EventArgs e)
        {
            try
            {
                Claim objClaim = new Claim();
                objClaim.ClmPolNo = txtClaimPolNo.Text.Trim();
                objClaim.ClmUid = int.Parse(hfClmUid.Value);
                objClaim.ClmPolFmDt = Convert.ToDateTime(txtClaimFromDate.Text).Date;
                objClaim.ClmPolToDt = Convert.ToDateTime(txtClaimToDate.Text).Date;
                objClaim.ClmLossDt = Convert.ToDateTime(txtClaimPolicyLossDate.Text).Date;
                objClaim.ClmIntmDt = Convert.ToDateTime(txtClaimIntmDate.Text).Date;
                objClaim.ClmRegDt = Convert.ToDateTime(txtClaimRegnDate.Text).Date;
                objClaim.ClmPolRepNo = txtClaimPolRepNo.Text.Trim();
                objClaim.ClmLossDesc = txtClaimLossDesc.Text.Trim();
                objClaim.ClmVehChassisNo = txtVehChassisNo.Text.Trim();
                objClaim.ClmVehEngineNo = txtVehEngineNo.Text.Trim();
                objClaim.ClmVehRegnNo = txtVehRegnNo.Text.Trim();
                objClaim.ClmVehValue = double.Parse(txtVehValue.Text);
                objClaim.ClmUpBy = Session["USER_ID"].ToString();
                objClaim.ClmUpDt = DateTime.Now.Date;

                if (!objClaimManager.CheckDuplicatePolRepNo(objClaim, objClaim.ClmUid))
                {
                    if (objClaimManager.UpdateClaim(objClaim))
                    {
                        ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                        objErrorCodeMaster.ErrCode = "102";
                        string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                        ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS','" + message + "'," +
                            "'/User/MotorClaim/AddMotorClaim?CLM_UID=" + objClaim.ClmUid + "');", true);
                    }

                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "201";
                    string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessage('FAILED','" + message + "');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('EXCEPTION','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void txtClaimFromDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.TryParse(txtClaimFromDate.Text, out DateTime fromDate))
                {
                    txtClaimToDate.Text = fromDate.AddDays(365).ToString("dd-MM-yyyy");
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('EXCEPTION','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/User/MotorClaim/MotorClaim.aspx");
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('EXCEPTION','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnViewSurvey_Click(object sender, EventArgs e)
        {
            try
            {
                MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                objMotorClmSurHdr.SurNo = hfClmSurNo.Value;
                string surUid = objMotorClmSurHdrManager.GetSurUidBySurNo(objMotorClmSurHdr);

                Response.Redirect("/User/SurveyDetails/SurveyDetails?SUR_UID=" + surUid);
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('EXCEPTION','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/User/Motorclaim/AddMotorClaim.aspx");
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('EXCEPTION','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void txtClaimPolRepNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Claim objClaim = new Claim();
                objClaim.ClmPolRepNo = txtClaimPolRepNo.Text.Trim();

                if (hfClmUid.Value != null)
                {
                    if (objClaimManager.CheckDuplicatePolRepNo(objClaim))
                    {
                        ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                        objErrorCodeMaster.ErrCode = "201";
                        string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                        ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessage('FAILED','" + errMessage + "');", true);

                        txtClaimPolRepNo.Text = string.Empty;
                    }
                }
                else
                {
                    objClaim.ClmUid = int.Parse(hfClmUid.Value.ToString());

                    if (objClaimManager.CheckDuplicatePolRepNo(objClaim, objClaim.ClmUid))
                    {
                        ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                        objErrorCodeMaster.ErrCode = "201";
                        string errMessage = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                        ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessage('FAILED','" + errMessage + "');", true);

                        txtClaimPolRepNo.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('EXCEPTION','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void btnResetClaim_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["CLM_UID"] != null)
            {
                Claim objClaim = new Claim();
                objClaim.ClmUid = int.Parse(Request.QueryString["CLM_UID"]);

                DataTable dtClaim = objClaimManager.FetchByClmUid(objClaim);

                hfClmUid.Value = Request.QueryString["CLM_UID"];
                txtClaimPolNo.Text = dtClaim.Rows[0]["CLM_POL_NO"].ToString();

            

                DateTime clmFmDt = (DateTime)dtClaim.Rows[0]["CLM_POL_FM_DT"];
                txtClaimFromDate.Text = clmFmDt.ToString("dd-MM-yyyy");

                DateTime clmToDt = (DateTime)dtClaim.Rows[0]["CLM_POL_TO_DT"];
                txtClaimToDate.Text = clmToDt.ToString("dd-MM-yyyy");

                DateTime clmLossDt = (DateTime)dtClaim.Rows[0]["CLM_LOSS_DT"];
                txtClaimPolicyLossDate.Text = clmLossDt.ToString("dd-MM-yyyy");

                DateTime clmIntmDt = (DateTime)dtClaim.Rows[0]["CLM_INTM_DT"];
                txtClaimIntmDate.Text = clmIntmDt.ToString("dd-MM-yyyy");

                DateTime clmRegnDate = (DateTime)dtClaim.Rows[0]["CLM_REG_DT"];
                txtClaimRegnDate.Text = clmIntmDt.ToString("dd-MM-yyyy");

                txtClaimPolRepNo.Text = dtClaim.Rows[0]["CLM_POL_REP_NO"].ToString();
                txtClaimLossDesc.Text = dtClaim.Rows[0]["CLM_LOSS_DESC"].ToString();
                txtVehChassisNo.Text = dtClaim.Rows[0]["CLM_VEH_CHASSIS_NO"].ToString();
                txtVehEngineNo.Text = dtClaim.Rows[0]["CLM_VEH_ENGINE_NO"].ToString();
                txtVehRegnNo.Text = dtClaim.Rows[0]["CLM_VEH_REGN_NO"].ToString();
                txtVehValue.Text = dtClaim.Rows[0]["CLM_VEH_VALUE"].ToString();


            }
            else
            {
                txtClaimPolNo.Text = string.Empty;
                txtClaimFromDate.Text = string.Empty;
                txtClaimToDate.Text = string.Empty;
                txtClaimPolicyLossDate.Text = string.Empty;
                txtClaimIntmDate.Text = string.Empty;
                txtClaimRegnDate.Text = string.Empty;
                txtClaimPolRepNo.Text = string.Empty;
                txtClaimLossDesc.Text = string.Empty;
                txtVehChassisNo.Text = string.Empty;
                txtVehEngineNo.Text = string.Empty;
                txtVehRegnNo.Text = string.Empty;
                txtVehValue.Text = string.Empty;

            }
        }
    }

}