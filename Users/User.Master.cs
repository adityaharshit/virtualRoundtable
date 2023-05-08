using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class User : System.Web.UI.MasterPage
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Golu\source\repos\WebApplication3\App_Data\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                lbLoginOrLogout.Text = "Login";
            }
            else
            {
                lbLoginOrLogout.Text = "Logout";
                getNotifications();
            }
        }



        protected void lbLoginOrLogout_Click(object sender, EventArgs e)
        {
            //Response.Redirect("login.aspx");
            if (Session["userId"] == null)
            {
                Session["redUrl"] = "Default.aspx";
                Response.Redirect("login.aspx");
            }
            else
            {
                Session.Abandon();
                Response.Redirect("login.aspx");
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        private void getNotifications()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Notification where DUId = " + Session["userId"] + " Order By ActionDoneOn DESC";
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            rNotification.DataSource = dt;
            if (dt.Rows.Count == 0)
            {
                rNotification.FooterTemplate = null;
                rNotification.FooterTemplate = new CustomTemplate(ListItemType.Footer);
            }
            rNotification.DataBind();
            con.Close();
        }

        protected void rNotification_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "linkToPost")
            {
                HiddenField hdnAction = e.Item.FindControl("hdnAction") as HiddenField;
                if(hdnAction.Value != "Followed")
                {
                    Response.Redirect("post.aspx?pid=" + e.CommandArgument);
                }
            }
        }

        protected void rNotification_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if(con.State==ConnectionState.Closed)
                    con.Open();
                cmd = con.CreateCommand();
                SqlDataReader dr;
                HiddenField hdnAction = e.Item.FindControl("hdnAction") as HiddenField;
                HiddenField hdnUsername = e.Item.FindControl("hdnUsername") as HiddenField;
                HiddenField hdnPostId = e.Item.FindControl("hdnPostId") as HiddenField;
                HiddenField hdnDUId = e.Item.FindControl("hdnDUId") as HiddenField;
                LinkButton lnbNotification = e.Item.FindControl("lnbNotification") as LinkButton;
                if(hdnAction.Value == "Liked")
                {
                    cmd.CommandText = "SELECT PostTitle from Post where PostId = " + hdnPostId.Value;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        lnbNotification.Text = "<strong>" + hdnUsername.Value+"</strong> liked your post titled <strong>"+dr.GetString(0)+"</strong>";
                    }
                    dr.Close();
                }
                else if (hdnAction.Value == "Followed")
                {
                    lnbNotification.Text = "<strong>" + hdnUsername.Value + "</strong> started following you";
                }
                else if (hdnAction.Value == "Comment")
                {
                    cmd.CommandText = "SELECT p.PostTitle,c.Comment from Post p INNER JOIN Comments c on p.PostId = c.PostId where p.PostId = " + hdnPostId.Value + " and c.UId = " + hdnDUId.Value;
                    dr = cmd.ExecuteReader();
                    int length = 0;
                    if (dr.Read())
                    {
                        if (dr.GetString(1).Length >= 20)
                        {
                            length = 20;
                        }
                        else
                        {
                            length = dr.GetString(1).Length;
                        }
                        lnbNotification.Text = "<strong>" + hdnUsername.Value + "</strong> commented on your post titled <strong>" + dr.GetString(0) +"</strong><br>Comment : "+dr.GetString(1).Substring(0,length) +"...";
                    }
                    dr.Close();
                }
                con.Close();
            }
        }
        private sealed class CustomTemplate : ITemplate
        {
            private ListItemType ListItemType { get; set; }
            public CustomTemplate(ListItemType type)
            {
                ListItemType = type;
            }
            public void InstantiateIn(Control container)
            {
                if (ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("No Notifications");
                    container.Controls.Add(footer);
                }
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
                //Response.Write("<script>alert('method');</script>");            
        }
    }
}