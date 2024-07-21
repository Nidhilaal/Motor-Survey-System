using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.Surveyor.Header
{
    public partial class AddSurveyHeader : System.Web.UI.Page
    {

        readonly ClaimManager objclaimManager = new ClaimManager();
        readonly CodeMasterManager objCodeMasterManager = new CodeMasterManager();
        readonly ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        readonly  MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();
        readonly MotorClmSurDtlManager objMotorClmSurDtlManager = new MotorClmSurDtlManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] != null && Session["USER_TYPE"].ToString() == "S")
                {
                    if (!IsPostBack)
                    {
                         if (Request.QueryString["CLM_UID"] != null && Request.QueryString["CLM_NO"] != null)
                        {
                            CodeMaster objCodeMaster = new CodeMaster();
                            objCodeMaster.CmType = "SUR_CURR";
                            DataTable dtSurCurr = objCodeMasterManager.FillDropDownList(objCodeMaster);
                            ddlSurrCurr.DataSource = dtSurCurr;
                            ddlSurrCurr.DataTextField = "CM_DISPLAY";
                            ddlSurrCurr.DataValueField = "CM_CODE";
                            ddlSurrCurr.DataBind();
                            ddlSurrCurr.Items.Insert(0, new ListItem("--select--", "NA"));
                            hfSurUid.Value = objMotorClmSurHdrManager.GetSurUidSequence();

                            Claim objClaim = new Claim();
                            objClaim.ClmUid = int.Parse(Request.QueryString["CLM_UID"]);
                            objClaim.ClmNo = Request.QueryString["CLM_NO"].ToString();

                            DataTable dtClaim = objclaimManager.FetchByClmUid(objClaim);

                            txtSurClmNo.Enabled = false;
                            txtSurChassisNo.Enabled = false;
                            txtSurEngineNo.Enabled = false;
                            txtSurRegnNo.Enabled = false;
                            txtSurFcAmt.Enabled = false;
                            txtSurLcAmt.Enabled = false;
                            hfSurClmUid.Value = Request.QueryString["CLM_UID"];
                            txtSurClmNo.Text = dtClaim.Rows[0]["CLM_NO"].ToString();
                            txtSurChassisNo.Text = dtClaim.Rows[0]["CLM_VEH_CHASSIS_NO"].ToString();
                            txtSurEngineNo.Text = dtClaim.Rows[0]["CLM_VEH_ENGINE_NO"].ToString();
                            txtSurRegnNo.Text = dtClaim.Rows[0]["CLM_VEH_REGN_NO"].ToString();

                            btnUpdateSurveyHeader.Visible = false;
                            btnAddDetails.Visible = false;
                            lblAccessories.Visible = false;
                            btnSubmitSurveyHeader.Visible = false;
                            lblTxtSurNo.Visible = false;
                            lblSurNo.Visible = false;
                        }
                        if (Request.QueryString["SUR_UID"] != null)
                        {
                            CodeMaster objCodeMaster = new CodeMaster();
                            objCodeMaster.CmType = "SUR_CURR";
                            DataTable dtSurCurr = objCodeMasterManager.FillDropDownList(objCodeMaster);
                            ddlSurrCurr.DataSource = dtSurCurr;
                            ddlSurrCurr.DataTextField = "CM_DISPLAY";
                            ddlSurrCurr.DataValueField = "CM_CODE";
                            ddlSurrCurr.DataBind();

                            MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                            objMotorClmSurHdr.SurUid = int.Parse(Request.QueryString["SUR_UID"]);
                            

                            MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
                            objMotorClmSurDtl.SurdSurUid = objMotorClmSurHdr.SurUid;

                            objMotorClmSurHdr.SurFcAmt = objMotorClmSurDtlManager.GetSumofFcAmt(objMotorClmSurDtl);
                            objMotorClmSurHdr.SurLcAmt = objMotorClmSurDtlManager.GetSumofLcAmt(objMotorClmSurDtl);
                           
                            objMotorClmSurHdrManager.UpdateFcAmt(objMotorClmSurHdr);
                            objMotorClmSurHdrManager.UpdateLcAmt(objMotorClmSurHdr);

                            DataTable dtMotorClmHdrManager = objMotorClmSurHdrManager.FetchBySurUid(objMotorClmSurHdr);
                            txtSurClmNo.Enabled = false;
                            txtSurChassisNo.Enabled = false;
                            txtSurEngineNo.Enabled = false;
                            txtSurRegnNo.Enabled = false;
                            hfSurClmUid.Value = dtMotorClmHdrManager.Rows[0]["SUR_CLM_UID"].ToString();
                            txtSurClmNo.Text = dtMotorClmHdrManager.Rows[0]["SUR_CLM_NO"].ToString();
                            txtSurRegnNo.Text = dtMotorClmHdrManager.Rows[0]["SUR_REGN_NO"].ToString();
                            txtSurChassisNo.Text = dtMotorClmHdrManager.Rows[0]["SUR_CHASSIS_NO"].ToString();
                            txtSurEngineNo.Text = dtMotorClmHdrManager.Rows[0]["SUR_ENGINE_NO"].ToString();
                            ddlSurrCurr.SelectedValue = dtMotorClmHdrManager.Rows[0]["SUR_CURR"].ToString();
                            lblSurNo.Text = dtMotorClmHdrManager.Rows[0]["SUR_NO"].ToString();

                            txtSurFcAmt.Text = dtMotorClmHdrManager.Rows[0]["SUR_FC_AMT"].ToString();
                            txtSurLcAmt.Text = dtMotorClmHdrManager.Rows[0]["SUR_LC_AMT"].ToString();


                            txtSurFcAmt.Enabled = false; 
                            txtSurLcAmt.Enabled = false;

                            btnSaveSurveyHeader.Visible = false;
                            btnUpdateSurveyHeader.Visible = true;
                            lblAccessories.Visible = true;
                            btnAddDetails.Visible = true;
                            lblAddSurveyDetails.Visible = false;
                            lblSurNo.Visible = true;
                            lblTxtSurNo.Visible = true;
                            

                            if (BindSurveyDetails(objMotorClmSurDtl))
                            {
                                ddlSurrCurr.Enabled = false;
                                btnSubmitSurveyHeader.Visible = true;
                                btnUpdateSurveyHeader.Visible = false;
                            }
                            else
                            {
                                btnSubmitSurveyHeader.Visible = false;
                            }
                            if (dtMotorClmHdrManager.Rows[0]["SUR_STATUS"].ToString() == "S")
                            {
                                btnSubmitSurveyHeader.Visible = false;
                                btnUpdateSurveyHeader.Visible = false;
                                btnAddDetails.Visible = false;

                                BindSurveyDetails(objMotorClmSurDtl);
                                gvSurveyDetails.Columns[8].Visible = false;
                            }
                             
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
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnUpdateSurveyHeader_Click(object sender, EventArgs e)
        {
            try
            {
                MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                objMotorClmSurHdr.SurUid = int.Parse(Request.QueryString["SUR_UID"]);
                objMotorClmSurHdr.SurCurr = ddlSurrCurr.SelectedValue;             

                objMotorClmSurHdr.SurUpBy = Session["USER_ID"].ToString();
                objMotorClmSurHdr.SurUpDt = DateTime.Now.Date;
                objMotorClmSurHdr.SurStatus = "P";

                if (objMotorClmSurHdrManager.UpdateSurveyCurrency(objMotorClmSurHdr))
                {

                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "102";
                    string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessage('SUCCESS','" + message + "');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnSaveSurveyHeader_Click(object sender, EventArgs e)
        {
            try
            {
                MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                objMotorClmSurHdr.SurUid = int.Parse(hfSurUid.Value);
                objMotorClmSurHdr.SurclmUid = int.Parse(hfSurClmUid.Value);
                objMotorClmSurHdr.SurclmNo = txtSurClmNo.Text;
                objMotorClmSurHdr.SurNo = "S/" + DateTime.Now.Year.ToString() + "/" + hfSurUid.Value.ToString().PadLeft(6, '0');
                objMotorClmSurHdr.SurChassisNo = txtSurChassisNo.Text;
                objMotorClmSurHdr.SurEngineNo = txtSurEngineNo.Text;
                objMotorClmSurHdr.SurRegnNo = txtSurRegnNo.Text;
                objMotorClmSurHdr.SurCurr = ddlSurrCurr.SelectedValue;

                if (!string.IsNullOrEmpty(txtSurFcAmt.Text))
                {
                    objMotorClmSurHdr.SurFcAmt = double.Parse(txtSurFcAmt.Text);
                }
                if (!string.IsNullOrEmpty(txtSurLcAmt.Text))
                {
                    objMotorClmSurHdr.SurLcAmt = double.Parse(txtSurLcAmt.Text);
                }
                objMotorClmSurHdr.SurCrBy = Session["USER_ID"].ToString();
                objMotorClmSurHdr.SurCrDt = DateTime.Now.Date;

                if (objMotorClmSurHdrManager.SaveSurveyHeader(objMotorClmSurHdr))
                {
                    MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
                    objMotorClmSurDtl.SurdSurUid = objMotorClmSurHdr.SurUid;

                    BindSurveyDetails(objMotorClmSurDtl);

                    Claim objClaim = new Claim();
                    objClaim.ClmUid = objMotorClmSurHdr.SurclmUid;
                    objClaim.ClmSurNo = objMotorClmSurHdr.SurNo;

                    objclaimManager.UpdateSurNo(objClaim);

                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "101";
                    string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS','" + message + "<br>Survey No:" + objMotorClmSurHdr.SurNo + "'," +
                       "'/Surveyor/Header/AddSurveyHeader?SUR_UID=" + hfSurUid.Value.ToString() + "');", true);

                    btnAddDetails.Visible = true;
                    lblAccessories.Visible = true;
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }

        }
        public bool BindSurveyDetails(MotorClmSurDtl objMotorClmSurDtl)
        {
            try
            {
                DataTable dtSurveyDetails = objMotorClmSurDtlManager.FetchBySurdSurUid(objMotorClmSurDtl);

                if ( dtSurveyDetails.Rows.Count > 0 )
                {
                    gvSurveyDetails.DataSource = dtSurveyDetails;
                    gvSurveyDetails.DataBind();

                    return true;
                }
                else
                {
                    lblEmptySurveyDetailsList.Text = "No Records Found!!!";
                    return false;
                }
            }
            catch (Exception ex) 
            { 
                ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true);
                throw;
            }
        }
        protected void gvSurveyDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if (e.CommandName == "cmdEdit")
                {
                    string surdUid = e.CommandArgument.ToString();

                    CodeMaster codeMaster = new CodeMaster();
                    codeMaster.CmCode = ddlSurrCurr.SelectedValue;
                    codeMaster.CmType = "SUR_CURR";
                    string surCurr = objCodeMasterManager.GetCmValue(codeMaster).ToString();

                    Response.Redirect("/Surveyor/Details/AddSurveyDetails?SURD_UID=" + surdUid + "&SUR_CURR=" + surCurr);
                }
                else if (e.CommandName == "cmdDelete")
                {
                    string commandArgument = e.CommandArgument.ToString();
                    string[] arguments = commandArgument.Split('\u002C');

                    MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();

                    objMotorClmSurDtl.SurdUid = int.Parse(arguments[0]);
                    objMotorClmSurDtl.SurdSurUid = int.Parse(arguments[1]);

                    string surUid = arguments[1];

                    if (objMotorClmSurDtlManager.DeleteSurveyDetails(objMotorClmSurDtl))
                    {
                        ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                        objErrorCodeMaster.ErrCode = "103";
                        string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);
                        ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS', '" + message + "'," +
                            "'/Surveyor/Header/AddSurveyHeader?SUR_UID=" + surUid + "');", true);
                    }
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["CLM_UID"] != null && Request.QueryString["CLM_NO"] != null)
                {
                    string surUid = hfSurClmUid.Value.ToString();

                    CodeMaster objCodeMaster = new CodeMaster();
                    objCodeMaster.CmCode = ddlSurrCurr.SelectedValue;
                    objCodeMaster.CmType = "SUR_CURR";
                    string surCurr = objCodeMasterManager.GetCmValue(objCodeMaster).ToString();

                    Response.Redirect("/Surveyor/Details/AddSurveyDetails?SUR_UID=" + surUid + "&SUR_CURR=" + surCurr);
                }
                if (Request.QueryString["SUR_UID"] != null)
                {
                    string surUid = Request.QueryString["SUR_UID"].ToString();

                    CodeMaster objCodeMaster = new CodeMaster();
                    objCodeMaster.CmCode = ddlSurrCurr.SelectedValue;
                    objCodeMaster.CmType = "SUR_CURR";
                    string surCurr = objCodeMasterManager.GetCmValue(objCodeMaster).ToString();

                    Response.Redirect("/Surveyor/Details/AddSurveyDetails?SUR_UID=" + surUid + "&SUR_CURR=" + surCurr);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnSubmitSurveyHeader_Click(object sender, EventArgs e)
        {
            try
            {
                MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();

                objMotorClmSurHdr.SurUid = int.Parse(Request.QueryString["SUR_UID"]);
                objMotorClmSurHdr.SurclmUid = int.Parse(hfSurClmUid.Value);

                objMotorClmSurHdr.SurclmNo = txtSurClmNo.Text;
                objMotorClmSurHdr.SurChassisNo = txtSurChassisNo.Text;
                objMotorClmSurHdr.SurEngineNo = txtSurEngineNo.Text;
                objMotorClmSurHdr.SurRegnNo = txtSurRegnNo.Text;
                objMotorClmSurHdr.SurCurr = ddlSurrCurr.SelectedValue;

                objMotorClmSurHdr.SurFcAmt = double.Parse(txtSurFcAmt.Text);
                objMotorClmSurHdr.SurLcAmt = double.Parse(txtSurLcAmt.Text);

                objMotorClmSurHdr.SurUpBy = Session["USER_ID"].ToString();
                objMotorClmSurHdr.SurUpDt = DateTime.Now.Date;
                objMotorClmSurHdr.SurStatus = "S";

                objMotorClmSurHdrManager.UpdateSurveyHeader(objMotorClmSurHdr);

                btnSubmitSurveyHeader.Visible = false;
                btnUpdateSurveyHeader.Visible = false;
                btnAddDetails.Visible = false;              

                MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
                objMotorClmSurDtl.SurdSurUid = objMotorClmSurHdr.SurUid;
                BindSurveyDetails(objMotorClmSurDtl);
                gvSurveyDetails.Columns[8].Visible = false;

                Claim objClaim = new Claim();

                objClaim.ClmSurCrYn = "Y";
                objClaim.ClmUid = objMotorClmSurHdr.SurclmUid;
                objclaimManager.UpdateSurveyCreated(objClaim);

                ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                objErrorCodeMaster.ErrCode = "104";
                string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);
                ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessage('SUCCESS','" + message + "');", true);

            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/Surveyor/Header/SurveyHeader.aspx");

            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
    }
}