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
    public partial class Staff : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Staff";
                if (Session["admin"] == null)
                {
                    Response.Redirect("~/User/Login.aspx");
                }
                else
                {

                    getStaff();
                }
            }
            lblMsg.Visible = false;
        }

        private void getStaff()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Food Ordering Website\App_Data\Database1.mdf;Integrated Security=True");
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "select * from Staff where DId != 0  Order BY JoinDate DESC";
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rStaff.DataSource = dt;
            rStaff.DataBind();
        }

        protected void rStaff_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {

                lblMsg.Visible = false;
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Food Ordering Website\App_Data\Database1.mdf;Integrated Security=True");
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Users WHERE UserId = " + e.CommandArgument;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "User deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getStaff();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error-" + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }
        }

        protected void btnAddNewStaff_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/registration.aspx?id=-1");
        }

        protected void rStaff_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbStatus = e.Item.FindControl("lbStatus") as Label;
                
                if (lbStatus.Text == "0")
                {
                    lbStatus.Text = "Idle";
                    lbStatus.CssClass = "badge badge-danger";
                }
                else
                {
                    lbStatus.Text = "onDelivery";
                    lbStatus.CssClass = "badge badge-success";
                }
                
            }
        }
    }
}