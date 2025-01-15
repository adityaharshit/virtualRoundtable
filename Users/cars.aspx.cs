using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3.Users
{
    public partial class cars : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\virtualRoundtable\App_Data\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText= "SELECT * from Topic where TopicId = " + id;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            Session["Topic"] = dt.Rows[0]["TName"];
            string imageUrl = "linear-gradient(rgba(0,0,0,0.8),rgba(0,0,0,0.2)), url(../"+dt.Rows[0]["BackImage"].ToString()+")";
            string divId = "ContentPlaceHolder1_programHeader";
            string script = string.Format("document.getElementById('{0}').style.backgroundImage = '{1}';", divId, imageUrl);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "changeBackground", script, true);
            con.Close();
            getSubT();
        }

        private void getSubT()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * from SubTopic where TopicId = " + id;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rSubTopics.DataSource = dt;
            rSubTopics.DataBind();
            con.Close();
        }
    }
}