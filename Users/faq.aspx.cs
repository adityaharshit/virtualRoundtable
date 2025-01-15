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
    public partial class faq : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\virtualRoundtable\App_Data\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getFAQDetails();
            }
                TextBox txtSearch = Master.FindControl("txtSearch") as TextBox;
                if (txtSearch != null){
                txtSearch.Enabled = true;
                    txtSearch.AutoPostBack = true;
                    txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
                }
        }

        private void getFAQDetails()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM FAQ where NOT Answer=''";
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rFAQ.DataSource = dt;
            rFAQ.DataBind();
            con.Close();
        }

        protected void rFAQ_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblAnswer = (Label)e.Item.FindControl("lblAnswer") as Label;
                if (e.CommandName == "showHide")
                {
                    if (lblAnswer.Visible == true)
                        lblAnswer.Visible = true;
                    else
                        lblAnswer.Visible = false;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            string question = txtQuestion.Text.Replace("'", " ");
            string email= txtEmail.Text.Replace("'", " ");
            cmd.CommandText = "INSERT INTO FAQ(Question,Email) VALUES(@question,@email)";
            cmd.Parameters.AddWithValue("@question", txtQuestion.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script language='javascript'>window.alert('Query sent!');window.location='faq.aspx';</script>");
        }
        protected void clear()
        {
            txtEmail.Text = "";
            txtQuestion.Text = "";
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            TextBox txtSearch = sender as TextBox;
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM FAQ where Question LIKE '%"+txtSearch.Text+"%' and NOT Answer=''";
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rFAQ.DataSource = dt;
            rFAQ.DataBind();
            con.Close();
        }
    }
}