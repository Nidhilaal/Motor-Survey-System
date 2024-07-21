using System;
using System.Web.UI;

namespace PresentationLayer.Surveyor
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect("/Login.aspx");
                }
            }
            catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "ExceptionAlert", "showErrorMessage('ERROR','" + ex.Message + "');", true); }
        }
    }
}