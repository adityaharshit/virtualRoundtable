using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3.Users
{
    public partial class login : System.Web.UI.Page
    {
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Write("<script language = 'javascript'>window.alert('Login !');</script>");
            if (txtUsername.Text == "Admin" && txtPass.Text == "admin")
            {
                Session["admin"] = "admin";
                Session["role"] = "admin";
                Session["userId"] = 0;
                Response.Write("<script language = 'javascript'>window.alert('Login Successfull!');window.location='../Admin/Topics.aspx';</script>");
            }
            else
            {

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\virtualRoundtable\App_Data\Database1.mdf;Integrated Security=True");
                cmd = con.CreateCommand();
                cmd.CommandText = "Select * from Users where Username='" + txtUsername.Text + "' and Password='" + txtPass.Text + "'";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["Banned"].ToString() == "1")
                    {
                        dr.Close();
                        Response.Write("<script language = 'javascript'>window.alert('Your account has been banned!!');window.location='login.aspx';</script>");
                    }
                    else
                    {
                        Session["role"] = "user";
                        Session["userId"] = dr["UId"].ToString();
                        Session["UserName"] = dr["Username"].ToString();
                        Session["UName"] = dr["Name"].ToString();
                        Session["JoinedDate"] = dr["JoinedOn"].ToString();
                        Session["ProfilePic"] = dr["ProfilePic"].ToString();
                        dr.Close();
                        Response.Write("<script language = 'javascript'>window.alert('Login Successfull!');window.location='index.aspx';</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Incorrect credentials')</script>");
                }
            }
        }
    }
}