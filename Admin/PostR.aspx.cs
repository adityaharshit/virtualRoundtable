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
    public partial class PostR : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\virtualRoundtable\App_Data\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Topics";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../Users/Login.aspx");
                }
                getReportedPostDetails();
            }
        }

        private void getReportedPostDetails()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT p.PostTitle,p.PostImage,p.PostDescription,p.Likes,p.TotalComments,r.PostId,r.Count FROM ReportedPost r INNER JOIN Post p on r.PostId=p.PostId";
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rReportedPosts.DataSource = dt;
            rReportedPosts.DataBind();
            con.Close();
        }

        protected void rReportedPosts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName== "linkToPost")
            {
                Response.Redirect("../Users/post.aspx?pid=" + e.CommandArgument);
            }
            if(e.CommandName == "Block")
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO LoglkDlkSvd(Action,doneBy,PostId,doneOn) VALUES('Banned',0,'"+e.CommandArgument+"',GETDATE())";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM LoglkDlkSvd where PostId = " + e.CommandArgument +" and NOT Action = 'Banned'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM Comments where PostId = " + e.CommandArgument;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM PrivateTopics where PostId = " + e.CommandArgument;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "UPDATE Post SET Banned=1 where PostId = " + e.CommandArgument;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM ReportedPost where PostId = " + e.CommandArgument;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            if (e.CommandName == "Delete")
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM ReportedPost where PostId = " + e.CommandArgument;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}