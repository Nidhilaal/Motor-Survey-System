using BusinessLayer;
using System;

namespace PresentationLayer
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UserMaster objUserMaster = new UserMaster();
            objUserMaster.UserId = txtUserId.Text;
            objUserMaster.UserPassword = txtPassword.Text;
            objUserMaster.UserPassword = objUserMaster.UserPassword.Replace("'", "$");
            UserMasterManager objUserMasterManager = new UserMasterManager();

            Boolean loginSuccess = objUserMasterManager.GetAuthentication(objUserMaster);

            Session["USER_TYPE"] = objUserMasterManager.CheckUserType(objUserMaster);
            Session["USER_ID"] = txtUserId.Text;

            if (loginSuccess && Session["USER_TYPE"].ToString() == "U")
            {
                Response.Redirect("~/User/Home.aspx");
            }
            else if (loginSuccess && Session["USER_TYPE"].ToString() == "S")
            {
                Response.Redirect("~/Surveyor/Dashboard.aspx");              
            }
            else
            {
                lblLoginFailed.Text = "Invalid username or password!!!";              
            }
        }
    }
}