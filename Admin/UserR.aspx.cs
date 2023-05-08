using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3.Admin
{
    public partial class UserR : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Golu\source\repos\WebApplication3\App_Data\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Topics";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../Users/Login.aspx");
                }
                getReportedUserDetails();
            }
        }

        private void getReportedUserDetails()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT u.Name,u.ProfilePic,u.Email,u.Username,u.JoinedOn,r.Count,r.UId FROM ReportedUser r INNER JOIN Users u on r.UId=u.UId";
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rReportedUser.DataSource = dt;
            rReportedUser.DataBind();
            con.Close();
        }

        protected void rReportedUser_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Block")
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Log(doneBy,doneTo,action,blockedOn) VALUES(0,'" + e.CommandArgument + "','Banned',GETDATE())";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "UPDATE Users SET Name='Banned User',Banned=1,ProfilePic='Assets/Images/profile.png' where UId = " + e.CommandArgument;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM ReportedUser where UId = " + e.CommandArgument;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            if(e.CommandName == "Delete")
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM ReportedUser where UId = " + e.CommandArgument;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            if(e.CommandName== "linkToProfile")
            {
                Session["userId"] = e.CommandArgument;
                Response.Redirect("../Users/Profile.aspx");

            }
            Response.Redirect(Request.RawUrl);
        }

    }
}