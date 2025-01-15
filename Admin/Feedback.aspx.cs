using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Food_Ordering_Website.Admin
{
    public partial class Feedback : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\virtualRoundtable\App_Data\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Users";
                if (Session["admin"] == null)
                {
                    Response.Redirect("~/Users/Login.aspx");
                }
                else
                {
                    getFAQ();
                }
            }
            lblMsg.Visible = false;
        }

        private void getFAQ()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM FAQ";
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rFAQ.DataSource = dt;
            rFAQ.DataBind();
            con.Close();
        }

        protected void rFAQ_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            if (e.CommandName == "edit")
            {
                cmd.CommandText="SELECT * FROM FAQ where FId = "+e.CommandArgument;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtQuestion.Text = dr.GetString(1);
                    txtAnswer.Text = dr.GetString(2);
                    txtQuestion.Enabled = false;
                    btnAddOrUpdate.Text = "Update";
                    hdnFId.Value = dr.GetInt32(0).ToString();
                }
                dr.Close();
            }
            else if (e.CommandName == "delete")
            {
                cmd.CommandText = "DELETE FROM FAQ WHERE FId = " + e.CommandArgument;
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            if (btnAddOrUpdate.Text == "Add")
            {
                cmd.CommandText = "INSERT INTO FAQ(Question,Answer,Email) VALUES(@question,@answer,'')";
                cmd.Parameters.AddWithValue("@question",txtQuestion.Text);
                cmd.Parameters.AddWithValue("@answer",txtAnswer.Text);
                cmd.ExecuteNonQuery();
            }
            else if(btnAddOrUpdate.Text == "Update")
            {
                cmd.CommandText = "UPDATE FAQ set Answer='" + txtAnswer.Text + "' where FID = " + hdnFId.Value;
                cmd.ExecuteNonQuery();
                clear();

            }
            con.Close();
        }
        protected void clear()
        {
            txtQuestion.Text = "";
            txtAnswer.Text = "";
            txtQuestion.Enabled = true;
            btnAddOrUpdate.Text = "Add";
        }
    }
}