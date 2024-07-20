using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.Surveyor.Details
{
    public partial class AddSurveyDetails : System.Web.UI.Page
    {
        readonly ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        readonly MotorClmSurDtlManager objMotorClmSurDtlManager = new MotorClmSurDtlManager();
        readonly CodeMasterManager objCodeMasterManager = new CodeMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_ID"] != null && Session["USER_TYPE"].ToString() == "S")
                {
                    if (!IsPostBack)
                    {
                        CodeMaster objCodeMasterSurdType = new CodeMaster();
                        objCodeMasterSurdType.CmType = "SURD_TYPE";
                        DataTable dtSurdType = objCodeMasterManager.FillDropDownList(objCodeMasterSurdType);
                        ddlSurdType.DataSource = dtSurdType;
                        ddlSurdType.DataTextField = "CM_DISPLAY";
                        ddlSurdType.DataValueField = "CM_CODE";
                        ddlSurdType.DataBind();
                        ddlSurdType.Items.Insert(0, new ListItem("--select--", "NA"));

                        CodeMaster objCodeMasterItemCode = new CodeMaster();
                        objCodeMasterItemCode.CmType = "SURD_ITEM_CODE";
                        DataTable dtItemCode = objCodeMasterManager.FillDropDownList(objCodeMasterItemCode);
                        ddlSurdItemCode.DataSource = dtItemCode;
                        ddlSurdItemCode.DataTextField = "CM_DISPLAY";
                        ddlSurdItemCode.DataValueField = "CM_CODE";
                        ddlSurdItemCode.DataBind();
                        ddlSurdItemCode.Items.Insert(0, new ListItem("--select--", "NA"));

                        if (Request.QueryString["SUR_UID"] != null && Request.QueryString["SUR_CURR"] != null)
                        {
                            hfSurdSurUid.Value = Request.QueryString["SUR_UID"].ToString();
                            txtSurrCurr.Text = Request.QueryString["SUR_CURR"].ToString();

                            txtSurdFcAmt.Attributes.Add("readonly", "readonly");
                            txtSurdLcAmt.Attributes.Add("readonly", "readonly");

                            btnUpdateSurveyDetails.Visible = false;
                            btnAddNew.Visible = false;

                        }
                        if (Request.QueryString["SURD_UID"] != null && Request.QueryString["SUR_CURR"] != null)
                        {
                            txtSurrCurr.Text = Request.QueryString["SUR_CURR"].ToString();

                            txtSurdFcAmt.Attributes.Add("readonly", "readonly");
                            txtSurdLcAmt.Attributes.Add("readonly", "readonly");

                            btnAddNew.Visible = true;

                            MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
                            objMotorClmSurDtl.SurdUid = int.Parse(Request.QueryString["SURD_UID"]);

                            if (objMotorClmSurDtlManager.FetchBySurdUid(objMotorClmSurDtl).Rows.Count > 0)
                            {
                                DataTable dtMotorClmSurDtl = objMotorClmSurDtlManager.FetchBySurdUid(objMotorClmSurDtl);

                                hfSurdSurUid.Value = dtMotorClmSurDtl.Rows[0]["SURD_SUR_UID"].ToString();
                                ddlSurdItemCode.SelectedValue = dtMotorClmSurDtl.Rows[0]["SURD_ITEM_CODE"].ToString();
                                ddlSurdType.SelectedValue = dtMotorClmSurDtl.Rows[0]["SURD_TYPE"].ToString();
                                txtSurdUnitPrice.Text = dtMotorClmSurDtl.Rows[0]["SURD_UNIT_PRICE"].ToString();
                                txtSurdQty.Text = dtMotorClmSurDtl.Rows[0]["SURD_QUANTITY"].ToString();
                                txtSurdFcAmt.Text = dtMotorClmSurDtl.Rows[0]["SURD_FC_AMT"].ToString();
                                txtSurdLcAmt.Text = dtMotorClmSurDtl.Rows[0]["SURD_LC_AMT"].ToString();

                                btnSaveSurveyDetails.Visible = false;
                            }
                            else
                            {
                                btnUpdateSurveyDetails.Visible = false;
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
        protected void btnSaveSurveyDetails_Click(object sender, EventArgs e)
        {
            try
            {
                MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
                objMotorClmSurDtl.SurdSurUid = int.Parse(hfSurdSurUid.Value);
                objMotorClmSurDtl.SurdItemCode = ddlSurdItemCode.SelectedValue;
                objMotorClmSurDtl.SurdType = ddlSurdType.SelectedValue;
                objMotorClmSurDtl.SurdUnitPrice = double.Parse(txtSurdUnitPrice.Text.Replace(",", string.Empty));
                objMotorClmSurDtl.SurdQuantity = int.Parse(txtSurdQty.Text);
                objMotorClmSurDtl.SurdFcAmt = double.Parse(txtSurdFcAmt.Text);
                objMotorClmSurDtl.SurdLcAmt = double.Parse(txtSurdLcAmt.Text);
                objMotorClmSurDtl.SurdCrBy = Session["USER_ID"].ToString();
                objMotorClmSurDtl.SurdCrDt = DateTime.Now.Date;

                if (objMotorClmSurDtlManager.CheckDuplicateSurdUid(objMotorClmSurDtl))
                {
                    ErrorCodeMaster objerrorCodeMaster = new ErrorCodeMaster();
                    objerrorCodeMaster.ErrCode = "201";
                    string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objerrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessage('ERROR','" + message + "');", true);

                    ddlSurdItemCode.SelectedValue = "NA";
                }
                else
                {
                    objMotorClmSurDtl.SurdUid = objMotorClmSurDtlManager.GetSurdUidSequence();

                    if (objMotorClmSurDtlManager.SaveSurveyDetails(objMotorClmSurDtl))
                    {
                        ErrorCodeMaster objerrorCodeMaster = new ErrorCodeMaster();
                        objerrorCodeMaster.ErrCode = "101";
                        string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objerrorCodeMaster);

                        ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('SUCCESS', '" + message + "'," +
                            "'/Surveyor/Details/AddSurveyDetails?SURD_UID=" + objMotorClmSurDtl.SurdUid + "&SUR_CURR=" + txtSurrCurr.Text.ToString() + "');", true);
                    }
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnUpdateSurveyDetails_Click(object sender, EventArgs e)
        {
            try
            {
                MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();

                objMotorClmSurDtl.SurdUid = int.Parse(Request.QueryString["SURD_UID"]);
                objMotorClmSurDtl.SurdSurUid = int.Parse(hfSurdSurUid.Value);
                objMotorClmSurDtl.SurdItemCode = ddlSurdItemCode.SelectedValue;
                objMotorClmSurDtl.SurdType = ddlSurdType.SelectedValue;
                objMotorClmSurDtl.SurdUnitPrice = double.Parse(txtSurdUnitPrice.Text.Replace(",", string.Empty));
                objMotorClmSurDtl.SurdQuantity = int.Parse(txtSurdQty.Text);
                objMotorClmSurDtl.SurdFcAmt = double.Parse(txtSurdFcAmt.Text);
                objMotorClmSurDtl.SurdLcAmt = double.Parse(txtSurdLcAmt.Text);
                objMotorClmSurDtl.SurdUpBy = Session["USER_ID"].ToString();
                objMotorClmSurDtl.SurdUpDt = DateTime.Now.Date;

                if (!objMotorClmSurDtlManager.CheckDuplicateSurdUid(objMotorClmSurDtl, objMotorClmSurDtl.SurdUid))
                {
                    objMotorClmSurDtlManager.UpdateSurveyDetails(objMotorClmSurDtl);
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "102";
                    string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showSuccessMessageRedirect('Success','" + message + "'," +
                        "'/Surveyor/Details/AddSurveyDetails?SURD_UID=" + objMotorClmSurDtl.SurdUid + "&SUR_CURR=" + txtSurrCurr.Text.ToString() + "');", true);
                }
                else
                {
                    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
                    objErrorCodeMaster.ErrCode = "201";
                    string message = objErrorCodeMasterManager.FetchErrorCodeByErrCode(objErrorCodeMaster);

                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessage('ERROR','" + message + "');", true);

                    txtSurdUnitPrice.Text = string.Empty;
                    txtSurdQty.Text = string.Empty;
                    txtSurdFcAmt.Text = string.Empty;
                    txtSurdLcAmt.Text = string.Empty;
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["SUR_UID"] != null && Request.QueryString["SUR_CURR"] != null)
                {
                    Response.Redirect("/Surveyor/Header/AddSurveyHeader?SUR_UID=" + Request.QueryString["SUR_UID"].ToString());
                }
                else
                {
                    Response.Redirect("/Surveyor/Header/AddSurveyHeader?SUR_UID=" + hfSurdSurUid.Value.ToString());
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/Surveyor/Details/AddSurveyDetails?SUR_UID=" + hfSurdSurUid.Value.ToString() +
                       "&SUR_CURR=" + txtSurrCurr.Text.ToString());
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }

        protected void btnResetSurveyDetails_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["SUR_UID"] != null && Request.QueryString["SUR_CURR"] != null)
            {

                ddlSurdItemCode.SelectedValue = "NA";
                ddlSurdType.SelectedValue = "NA";
                txtSurdQty.Text = string.Empty;
                txtSurdUnitPrice.Text = string.Empty;
                txtSurdFcAmt.Text = string.Empty;
                txtSurdLcAmt.Text = string.Empty;


            }
            if (Request.QueryString["SURD_UID"] != null && Request.QueryString["SUR_CURR"] != null)
            {


                MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
                objMotorClmSurDtl.SurdUid = int.Parse(Request.QueryString["SURD_UID"]);


                DataTable dtMotorClmSurDtl = objMotorClmSurDtlManager.FetchBySurdUid(objMotorClmSurDtl);

                hfSurdSurUid.Value = dtMotorClmSurDtl.Rows[0]["SURD_SUR_UID"].ToString();
                ddlSurdItemCode.SelectedValue = dtMotorClmSurDtl.Rows[0]["SURD_ITEM_CODE"].ToString();
                ddlSurdType.SelectedValue = dtMotorClmSurDtl.Rows[0]["SURD_TYPE"].ToString();
                txtSurdUnitPrice.Text = dtMotorClmSurDtl.Rows[0]["SURD_UNIT_PRICE"].ToString();
                txtSurdQty.Text = dtMotorClmSurDtl.Rows[0]["SURD_QUANTITY"].ToString();
                txtSurdFcAmt.Text = dtMotorClmSurDtl.Rows[0]["SURD_FC_AMT"].ToString();
                txtSurdLcAmt.Text = dtMotorClmSurDtl.Rows[0]["SURD_LC_AMT"].ToString();




            }
        }
    }
}