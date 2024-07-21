using BusinessLayer;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Data;
using System.Web.UI;
namespace PresentationLayer.Report
{
    public partial class ReportPage : System.Web.UI.Page
    {
        readonly MotorClmSurDtlManager objMotorClmSurDtlManager = new MotorClmSurDtlManager();
        readonly MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();
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
                            MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
                            objMotorClmSurHdr.SurUid = int.Parse(Request.QueryString["SUR_UID"]);

                            DataTable dtSurHdr = objMotorClmSurHdrManager.FetchBySurUid(objMotorClmSurHdr);

                            MotorClmSurDtl motorClmSurDtl = new MotorClmSurDtl();
                            motorClmSurDtl.SurdSurUid = int.Parse(Request.QueryString["SUR_UID"]);

                            DataTable dtSurDtl = objMotorClmSurDtlManager.FetchBySurdSurUid(motorClmSurDtl);

                            if ( dtSurHdr.Rows.Count > 0 && dtSurDtl.Rows.Count > 0 )
                            {
                                SurveyDataSet ds = new SurveyDataSet();

                                ds.Tables["SurveyHeader"].Merge(dtSurHdr, true, MissingSchemaAction.Ignore);
                                ds.Tables["SurveyDetails"].Merge(dtSurDtl, true, MissingSchemaAction.Ignore);
                                string reportPath = Server.MapPath("~") + "Report\\SurveyReport.rpt";
                                ReportDocument report = new ReportDocument();

                                report.Load(reportPath);
                                report.SetDataSource(ds);

                                report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "SurveyDocument");
                            }
                        }
                    }
                }
                else
                {
                    ErrorCodeMaster errorCodeMaster = new ErrorCodeMaster();
                    errorCodeMaster.ErrCode = "403";
                    string error = objErrorCodeMasterManager.FetchErrorCodeByErrCode(errorCodeMaster);
                    ScriptManager.RegisterStartupScript(this, GetType(), "successAlert", "showErrorMessageRedirect('ACCESS DENIED', '" + error + "','/Login.aspx');", true);
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty) + "');", true); }
        }
    }
}