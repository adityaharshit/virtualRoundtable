using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3.Users
{
    public partial class forgotPass : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\virtualRoundtable\App_Data\Database1.mdf;Integrated Security=True");
            try
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "Select * from Users where Username=@username and Email=@email and Phone=@phone";
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    cmd.CommandText = "Update Users SET Password=@password where Username = @username";
                    cmd.Parameters.AddWithValue("@password", txtPass.Text);
                    cmd.Parameters.AddWithValue("@sername", txtUsername.Text);
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Password Updated')</script>");
                }
                else
                {
                    dr.Close();
                    Response.Write("<script>alert('Incorrect details')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An Error Occured')</script>");
            }
        }

        protected void btnForgot_Click(object sender, EventArgs e)
        {

        }
    }
}