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
    public partial class Profile : System.Web.UI.Page
    {
        protected int postCount=0;
        protected int followerCount=0;
        protected int followingCount=0;

        SqlCommand cmd;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\virtualRoundtable\App_Data\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Topics";
                if (Session["userId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    if (Session["role"].Equals("user"))
                    {
                        getBlockedUserDetails();
                        getSavedPosts();
                        getLikedPosts();
                        getDisLikedPosts();
                        getComments();
                        lnbRegistration.Enabled = true;
                    }
                    else
                    {
                        ImageButton imageButton1 = Master.FindControl("ImageButton1") as ImageButton;
                        imageButton1.Enabled = false;
                        LinkButton lnbFaq= Master.FindControl("lnbFaq") as LinkButton;
                        lnbFaq.Enabled = false;
                        LinkButton lnbHome = Master.FindControl("lnbHome") as LinkButton;
                        lnbHome.Enabled = false;
                        LinkButton lnbProfile = Master.FindControl("lnbProfile") as LinkButton;
                        lnbProfile.Enabled = false;
                        LinkButton lnbNotification = Master.FindControl("lnbNotification") as LinkButton;
                        lnbNotification.Enabled = false;
                    }
                    getUserDetails();
                    getUserPosts();
                }
            }
        }

        private void getComments()
        {

            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "Select c.*,p.PostTitle,p.PostImage from Comments c INNER JOIN Post p on c.PostId=p.PostId INNER JOIN Users u ON c.UId=u.UId where c.UId = " + Session["userId"] + "";
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rPrivate.DataSource = dt;
            rPrivate.DataBind();
            con.Close();
        }

        private void getDisLikedPosts()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "Select d.* from Disliked d inner join Users u on d.UId = u.UId where d.UId = " + Session["userId"] + " and u.HidenPost NOT LIKE '%'+convert(varchar,d.PostId)+',%' and u.BlockedUser NOT LIKE '%'+convert(varchar,d.PostId)+',%'";
            cmd.Parameters.AddWithValue("@postId", "d.PostId");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rDisliked.DataSource = dt;
            rDisliked.DataBind();
            con.Close();
        }

        private void getLikedPosts()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "Select d.* from Liked d inner join Users u on d.UId = u.UId where d.UId = " + Session["userId"] + " and u.HidenPost NOT LIKE '%'+convert(varchar,d.PostId)+',%' and u.BlockedUser NOT LIKE '%'+convert(varchar,d.PostId)+',%'";
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rLiked.DataSource = dt;
            rLiked.DataBind();
            con.Close();
        }

        private void getUserPosts()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "Select d.* from Post d inner join Users u on d.UId = u.UId where d.UId = " + Session["userId"] + " and u.HidenPost NOT LIKE '%'+convert(varchar,d.PostId)+'%'";
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            postCount = dt.Rows.Count;
            rMyPosts.DataSource = dt;
            rMyPosts.DataBind();
            con.Close();
        }

        private void getSavedPosts()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "Select d.* from Saved d inner join Users u on d.UId = u.UId where d.UId = " + Session["userId"] + " and u.HidenPost NOT LIKE '%'+convert(varchar,d.PostId)+',%' and u.BlockedUser NOT LIKE '%'+convert(varchar,d.PostId)+',%'";
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rSaved.DataSource = dt;
            rSaved.DataBind();
            con.Close();
        }

        private void getBlockedUserDetails()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from Blocked where BlockedBy = " + Session["userId"];
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rBlocked.DataSource = dt;
            rBlocked.DataBind();
            con.Close();
        }

        private void getUserDetails()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from Users where UId = " + Session["userId"];
            string[] followers= { };
            string[] following= { };
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Session["UserName"] = dr["Username"].ToString();
                Session["UName"] = dr["Name"].ToString();
                Session["JoinedDate"] = dr["JoinedOn"].ToString();
                Session["ProfilePic"] = dr["ProfilePic"].ToString();
                followers = dr["Followers"].ToString().Split(',');
                following = dr["Following"].ToString().Split(',');
            }
            dr.Close();
            followerCount = followers.Length-1;
            followingCount = following.Length-1;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rfUserProfile.DataSource = dt;
            rfUserProfile.DataBind();
            con.Close();
        }

        protected void rBlocked_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Unblock")
            {
                con.Open();
                string blocked = "";
                cmd = con.CreateCommand();
                cmd.CommandText = "Select BlockedUser from Users where UId = " + Session["UserId"];
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    blocked = dr.GetString(0);
                }
                dr.Close();
                blocked = blocked.Replace(e.CommandArgument + ",", "");
                cmd.CommandText = "UPDATE Users SET BlockedUser = '" + blocked + "' where UId = " + Session["UserId"];
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM Log where doneBy='" + Session["UserId"] + "' and doneTo = '" + e.CommandArgument + "' and action = 'block'";
                cmd.ExecuteNonQuery();
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void rSaved_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "linkToPost")
            {
                Response.Redirect("Post.aspx?pid=" + e.CommandArgument);
            }
        }

        protected void rSaved_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void rMyPosts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "linkToPost")
            {
                Response.Redirect("Post.aspx?pid=" + e.CommandArgument);
            }
        }

        protected void rDisliked_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "linkToPost")
            {
                Response.Redirect("Post.aspx?pid=" + e.CommandArgument);
            }
        }

        protected void rPrivate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        //private void getPrivateRequests(int postId, Repeater rRequests)
        //{
        //    if(con.State==ConnectionState.Closed)
        //        con.Open();
        //    cmd = con.CreateCommand();

        //    cmd.CommandText = "Select * from PrivateTopics where PostId = " + postId;
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);
        //    rRequests.DataSource = dt;
        //    rRequests.DataBind();
        //    if(con.State==ConnectionState.Open)
        //        con.Close();
        //}

        protected void rPrivate_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "linkToPost")
            {
                Response.Redirect("post.aspx?pid=" + e.CommandArgument);
            }
        }

        protected void lnbRegistration_Click(object sender, EventArgs e)
        {
            Response.Redirect("registration.aspx?id=" + Session["userId"]);
        }
    }
}