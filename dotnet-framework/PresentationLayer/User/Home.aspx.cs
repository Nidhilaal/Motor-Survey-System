using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationLayer.user
{
    public partial class Home : System.Web.UI.Page
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