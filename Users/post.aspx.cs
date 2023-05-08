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
    public partial class post : System.Web.UI.Page
    {
        string gfollowing = "", gLiked = "", gDisliked = "", blocked = "", savedPost = "";
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Golu\source\repos\WebApplication3\App_Data\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Topics";
                if (Session["userId"] == null && Session["admin"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT Following,LikedPosts,DislikedPosts,BlockedUser,SavePost from Users where UId = " + Session["userId"];
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        gfollowing = dr.GetString(0);
                        gLiked = dr.GetString(1);
                        gDisliked = dr.GetString(2);
                        blocked = dr.GetString(3);
                        savedPost = dr.GetString(4);
                    }
                    dr.Close();
                    con.Close();
                    getPostData();
                    if(con.State==ConnectionState.Closed)
                        con.Open();
                    cmd.CommandText = "SELECT * FROM Post where PostId = " + Request.QueryString["pid"] +" and UId = " +Session["userId"];
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.GetInt32(9) == 1)
                        {
                        dr.Close();
                        getRequests();
                        }
                        else
                            lblMngAccess.Visible = false;
                        dr.Close();
                    }
                    else
                    {
                        lblMngAccess.Visible = false;
                    }
                    dr.Close();
                    con.Close();
                }
            }
        }

        private void getRequests()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = con.CreateCommand();

            cmd.CommandText = "Select p.*,u.Username,u.ProfilePic from PrivateTopics p INNER JOIN Users u ON p.RequestedBy=u.UId where p.PostId = " + Request.QueryString["pid"];
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rRequests.DataSource = dt;
            rRequests.DataBind();
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        protected void rPosts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                LinkButton lnkFollow = e.Item.FindControl("lnkFollow") as LinkButton;
                
                LinkButton Liked = e.Item.FindControl("lnkLikes") as LinkButton;
                LinkButton Dislike = e.Item.FindControl("lnkDislike") as LinkButton;
                LinkButton lnbSave = e.Item.FindControl("lnbSave") as LinkButton;
                Label lblNegative = e.Item.FindControl("lblNegative") as Label;
                HiddenField postId = e.Item.FindControl("hdnPostId") as HiddenField;
                HiddenField UID = e.Item.FindControl("hdnUId") as HiddenField;
                HiddenField hdnDesc = e.Item.FindControl("hdnDesc") as HiddenField;
                string desc = hdnDesc.Value;
                string negContent = "Disappointing,Terrible,Awful,Horrible,Bad,Unpleasant,Unsatisfactory,Unacceptable,Frustrating,Displeasing,Disgusting,Repulsive,Abysmal,Atrocious,Inferior,Inadequate,Substandard,Deficient,Faulty,Flawed,Shoddy,Mediocre,Poorly made,Unreliable,Uncomfortable,Irritating,Annoying,Aggravating,Miserable,Depressing,Upsetting,Disheartening,Demoralizing,Discouraging,Dismal,Gloomy,Bleak,Drab,Dreary,Tedious,Boring,Monotonous,Tiring,Exhausting,Stressful,Chaotic,Confusing,Overwhelming,Complicated,Difficult";
                string[] negative = negContent.Split(',');
                int count = 0, i = 0;
                if(UID.Value == Session["userId"].ToString())
                {
                    lnkFollow.Visible = false;
                }
                while (i < 50)
                {
                    if (desc.Contains(negative[i]))
                    {
                        count++;
                    }
                    i++;
                }
                if (count >= 1)
                {
                    lblNegative.Text = "Negative";
                    lblNegative.CssClass = "badge color-primary";
                }
                else
                {
                    lblNegative.Text = "Positive";
                    lblNegative.CssClass = "badge color-primary";
                }
                if (gfollowing.Contains(UID.Value))
                {
                    lnkFollow.Text = "Following";
                }
                if (gLiked.Contains(postId.Value))
                {
                    Liked.CssClass = "post-liked-disliked";
                }
                if (gDisliked.Contains(postId.Value))
                {
                    Dislike.CssClass = "post-liked-disliked";
                }
                if (blocked.Contains(UID.Value))
                {
                    UID.Parent.Visible = false;
                }
                if (savedPost.Contains(postId.Value))
                {
                    lnbSave.Text = "Unsave";
                }
            }
        }

      
        protected void rPosts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd = con.CreateCommand();
            if (e.CommandName == "lnkLike")
            {
                Label likesCount = (Label)e.Item.FindControl("likeCount");
                int count = Convert.ToInt32(likesCount.Text.ToString()) + 1;

                cmd.CommandText = "Select LikedPosts, DislikedPosts from Users where UId = " + Session["userId"];
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string likedPosts = dr.GetString(0).ToString();
                    string dislikedPosts = dr.GetString(1).ToString();
                    dr.Close();
                    if (likedPosts.Contains(e.CommandArgument.ToString()))
                    {
                        likedPosts = likedPosts.Replace(e.CommandArgument.ToString() + ",", "");
                        cmd.CommandText = "UPDATE Users Set LikedPosts = '" + likedPosts + "' where UId = " + Session["userId"];
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "DELETE FROM LoglkDlkSvd where Action = 'Liked' and doneBy = '" + Session["userId"] + "' and PostId = '" + e.CommandArgument + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "UPDATE Post SET Likes = Likes-1,Views = Views+1  WHERE PostId = " + e.CommandArgument;
                    }
                    else
                    {
                        likedPosts = likedPosts + e.CommandArgument.ToString() + ",";
                        cmd.CommandText = "UPDATE Users Set LikedPosts = '" + likedPosts + "' where UId = " + Session["userId"];
                        cmd.ExecuteNonQuery();
                        if (dislikedPosts.Contains(e.CommandArgument.ToString()))
                        {
                            dislikedPosts = dislikedPosts.Replace(e.CommandArgument.ToString() + ",", "");
                            cmd.CommandText = "UPDATE Users Set DislikedPosts = '" + dislikedPosts + "' where UId = " + Session["userId"];
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "DELETE FROM LoglkDlkSvd where Action = 'Disliked' and doneBy = '" + Session["userId"] + "' and PostId = '" + e.CommandArgument + "'";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "UPDATE Post SET Likes = Likes+1 WHERE PostId = " + e.CommandArgument;
                            cmd.ExecuteNonQuery();
                        }
                        cmd.CommandText = "INSERT INTO LoglkDlkSvd(Action, doneBy, PostId, doneOn) VALUES('Liked','" + Session["userId"] + "','" + e.CommandArgument + "',GETDATE())";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "UPDATE Post SET Likes = Likes+1,Views = Views+1 WHERE PostId = " + e.CommandArgument;
                    }
                }
                cmd.ExecuteNonQuery();
                Response.Redirect(Request.RawUrl);
            }
            if (e.CommandName == "lnkDislike")
            {
                Label likesCount = (Label)e.Item.FindControl("likeCount");
                int count = Convert.ToInt32(likesCount.Text.ToString()) + 1;

                cmd.CommandText = "Select LikedPosts, DislikedPosts from Users where UId = " + Session["userId"];
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string dislikedPosts = dr.GetString(1);
                    string likedPosts = dr.GetString(0);
                    dr.Close();
                    if (dislikedPosts.Contains(e.CommandArgument.ToString()))
                    {
                        dislikedPosts = dislikedPosts.Replace(e.CommandArgument.ToString() + ",", "");
                        cmd.CommandText = "UPDATE Users Set DislikedPosts = '" + dislikedPosts + "' where UId = " + Session["userId"];
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "DELETE FROM LoglkDlkSvd where Action = 'Disliked' and doneBy = '" + Session["userId"] + "' and PostId = '" + e.CommandArgument + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "UPDATE Post SET Likes = Likes+1,Views = Views+1 WHERE PostId = " + e.CommandArgument;
                    }
                    else
                    {
                        dislikedPosts = dislikedPosts + e.CommandArgument.ToString() + ",";
                        cmd.CommandText = "UPDATE Users Set DislikedPosts = '" + dislikedPosts + "' where UId = " + Session["userId"];
                        cmd.ExecuteNonQuery();
                        if (likedPosts.Contains(e.CommandArgument.ToString()))
                        {
                            likedPosts = likedPosts.Replace(e.CommandArgument.ToString() + ",", "");
                            cmd.CommandText = "UPDATE Users Set LikedPosts = '" + likedPosts + "' where UId = " + Session["userId"];
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "UPDATE Post SET Likes = Likes-1 WHERE PostId = " + e.CommandArgument;
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM LoglkDlkSvd where Action = 'Liked' and doneBy = '" + Session["userId"] + "' and PostId = '" + e.CommandArgument + "'";
                            cmd.ExecuteNonQuery();
                        }
                        cmd.CommandText = "INSERT INTO LoglkDlkSvd(Action, doneBy, PostId, doneOn) VALUES('Disliked','" + Session["userId"] + "','" + e.CommandArgument + "',GETDATE())";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "UPDATE Post SET Likes = Likes-1,Views = Views+1 WHERE PostId = " + e.CommandArgument;
                    }
                }
                cmd.ExecuteNonQuery();
                Response.Redirect(Request.RawUrl);
            }
            if (e.CommandName == "Comment")
            {
                cmd.CommandText = "UPDATE Post SET Views = Views+1  WHERE PostId = " + e.CommandArgument;
                cmd.ExecuteNonQuery();
                Response.Redirect("post.aspx?pid=" + e.CommandArgument);
            }
            if (e.CommandName == "Save")
            {
                cmd.CommandText = "SELECT SavePost FROM Users WHERE UId = " + Session["userId"];
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string savedPosts = dr.GetString(0);
                    dr.Close();
                    if (savedPosts.Contains(e.CommandArgument.ToString()))
                    {
                        savedPosts = savedPosts.Replace(e.CommandArgument.ToString() + ",", "");
                        cmd.CommandText = "UPDATE Users SET SavePost = '" + savedPosts + "' where UId = " + Session["userId"];
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "DELETE FROM LoglkDlkSvd where Action = 'Saved' and doneBy = '" + Session["userId"] + "' and PostId = '" + e.CommandArgument + "'";
                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('Post Removed from Saved')</script>");
                    }
                    else
                    {
                        savedPosts = savedPosts + e.CommandArgument.ToString() + ",";
                        cmd.CommandText = "UPDATE Users SET SavePost = '" + savedPosts + "' where UId = " + Session["userId"];
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO LoglkDlkSvd(Action, doneBy, PostId, doneOn) VALUES('Saved','" + Session["userId"] + "','" + e.CommandArgument + "',GETDATE())";
                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('Post Saved')</script>");
                    }
                }
            }
            if (e.CommandName == "ReportUser")
            {
                cmd.CommandText = "SELECT Count from ReportedUser where UId = " + e.CommandArgument;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    cmd.CommandText = "UPDATE ReportedUser SET Count = Count+1";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    dr.Close();
                    cmd.CommandText = "INSERT into ReportedUser(UId, ReportedDate, Count) VALUES('" + e.CommandArgument + "',GETDATE(),1)";
                    cmd.ExecuteNonQuery();
                }
                Response.Write("<script>alert('User Reported');</script>");
            }
            if (e.CommandName == "ReportPost")
            {
                cmd.CommandText = "SELECT Count from ReportedPost where PostId = " + e.CommandArgument;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    cmd.CommandText = "UPDATE ReportedPost SET Count = Count+1";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    dr.Close();
                    cmd.CommandText = "INSERT into ReportedPost(PostId, ReportedDate, Count) VALUES('" + e.CommandArgument + "',GETDATE(),1)";
                    cmd.ExecuteNonQuery();
                }
                Response.Write("<script>alert('Post Reported');</script>");
            }
            //if(e.CommandName == "Unfollow")
            //{
            //}
            if (e.CommandName == "BlockUser")
            {
                blocked = blocked + e.CommandArgument + ",";
                cmd.CommandText = "UPDATE Users SET BlockedUser = '" + blocked + "' where UId = '" + Session["userId"] + "'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Insert into Log(doneBy, doneTo, action, blockedOn) Values('" + Session["userId"] + "','" + e.CommandArgument + "','block',GETDATE())";
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('User Blocked');</script>");
            }
            if (e.CommandName == "Follow")
            {
                string following, followers = "";
                cmd.CommandText = "SELECT Followers FROM Users where UId =" + e.CommandArgument;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    followers = dr.GetString(0);
                }
                dr.Close();
                cmd.CommandText = "SELECT Following from Users where UId = " + Session["userId"];
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    following = dr.GetString(0);
                    dr.Close();
                    if (following.Contains(e.CommandArgument.ToString()))
                    {
                        following = following.Replace(e.CommandArgument.ToString() + ",", "");
                        cmd.CommandText = "UPDATE Users SET Following = '" + following + "' where UId = " + Session["userId"];
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "DELETE FROM Log where doneBy='" + Session["UserId"] + "' and doneTo = '" + e.CommandArgument + "' and action = 'Followed'";
                        cmd.ExecuteNonQuery();
                        followers = followers.Replace(Session["userId"] + ",", "");
                        cmd.CommandText = "UPDATE Users SET Followers = '" + followers + "' where UId = " + e.CommandArgument;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        following = following + e.CommandArgument.ToString() + ",";
                        cmd.CommandText = "UPDATE Users SET Following = '" + following + "'where UId = " + Session["userId"];
                        cmd.ExecuteNonQuery();
                        followers = followers + Session["userId"] + ",";
                        cmd.CommandText = "UPDATE Users SET Followers = '" + followers + "' where UId = " + e.CommandArgument;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Insert into Log(doneBy, doneTo, action, blockedOn) Values('" + Session["userId"] + "','" + e.CommandArgument + "','Followed',GETDATE())";
                        cmd.ExecuteNonQuery();
                    }
                }
                Response.Redirect(Request.RawUrl);
            }
            if(e.CommandName == "hideDelete")
            {
                int UId = Convert.ToInt32( Session["userId"]);
                if (Convert.ToInt32( e.CommandArgument) == UId)
                {
                    cmd.CommandText = "DELETE FROM Post where PostId = "+Request.QueryString["pid"];
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM PrivateTopics where PostId = " + Request.QueryString["pid"];
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM Comments where PostId = " + Request.QueryString["pid"];
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM LoglkDlkSvd where PostId = " + Request.QueryString["pid"];
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM ReportedPost where PostId = " + Request.QueryString["pid"];
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    string hidenPost = "";
                    cmd.CommandText = "SELECT HidenPost from Users where UId = " + e.CommandArgument;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        hidenPost = dr.GetString(0);
                    }
                    dr.Close();
                    hidenPost = Request.QueryString["pid"] + "," + hidenPost;
                    cmd.CommandText = "UPDATE Users set HidenPost = '" + hidenPost + "' where UId = " + e.CommandArgument;
                    cmd.ExecuteNonQuery();
                }
                Response.Redirect("forum.aspx?id=1");
            }
            Response.Redirect(Request.RawUrl);
            con.Close();
        }

        protected void rRequests_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkAccept = e.Item.FindControl("lnkAccept") as LinkButton;
                HiddenField hdnStatus = e.Item.FindControl("hdnStatus") as HiddenField;
                if (Convert.ToInt32( hdnStatus.Value) == 1)
                {
                    lnkAccept.Visible = false;
                }
            }
        }

        protected void rRequests_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            if(e.CommandName.ToString() == "accept")
            {
                cmd.CommandText = "UPDATE PrivateTopics SET Status = 1 where Id = "+e.CommandArgument;
                cmd.ExecuteNonQuery();
            }
            else if(e.CommandName.ToString() == "delete")
            {
                cmd.CommandText = "DELETE FROM PrivateTopics where Id = " + e.CommandArgument;
                cmd.ExecuteNonQuery();
            }
            Response.Redirect(Request.RawUrl);
        }

        
        protected void rComments_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "hideDelete")
                {
                    HiddenField hdnPostID = e.Item.FindControl("hdnPostID") as HiddenField;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Delete from Comments where ComId = " + e.CommandArgument;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "UPDATE Post set TotalComments=TotalComments-1 where PostId = " + hdnPostID.Value;
                    cmd.ExecuteNonQuery();
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('Something went wrong')</script>;");
            }
        }

        private void getComments()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT c.*,p.UId as PostUId,u.Name,u.Username,u.ProfilePic FROM ((Comments c INNER JOIN Users u on c.UId = u.UId) INNER JOIN Post p on c.PostId = p.PostId) where c.PostId = " + Request.QueryString["pid"];
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rComments.DataSource = dt;
            rComments.DataBind();
            if(con.State==ConnectionState.Open)
                con.Close();
        }

        private void getPostData()
        {
            if(con.State == ConnectionState.Closed)
                con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT p.*, u.Name,u.Username,u.ProfilePic FROM Post p INNER JOIN Users u on p.UId = u.UId where p.PostId = '" + Request.QueryString["pid"] +"'" ;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rPosts.DataSource = dt;
            rPosts.DataBind();
            if (Convert.ToInt32(dt.Rows[0]["UId"]) != Convert.ToInt32(Session["userId"]))
            {
                if (Convert.ToInt32(dt.Rows[0]["IsPrivate"]) == 0)
                {
                    //lblPrivate.Parent.Visible = false;
                    lblPrivate.Visible = false;
                    btnAccess.Visible = false;
                    getComments();
                }
                else
                {

                    cmd.CommandText = "SELECT * FROM PrivateTopics where RequestedBy = " + Session["userId"];
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.GetInt32(3) == 0)
                        {
                            dr.Close();
                            txtComment.Visible = false;
                            btnComment.Visible = false;
                            lblPrivate.Text = "You have alredy requested for access to this discussion. Kindly wait for the user to accept your request.";
                            btnAccess.Text = "Already Requested";
                            btnAccess.Enabled = false;
                            lblPrivate.Parent.Visible = true;
                        }
                        else
                        {
                            dr.Close();
                            lblPrivate.Visible = false;
                            btnAccess.Visible = false;
                            getComments();
                        }
                    }
                    else
                    {
                        txtComment.Visible = false;
                        btnComment.Visible = false;
                        lblPrivate.Parent.Visible = true;
                    }
                    dr.Close();
                }
            }
            else
            {
                lblPrivate.Visible = false;
                btnAccess.Visible = false;
                getComments();
            }
                if (con.State == ConnectionState.Open)
                    con.Close();
        }

        protected void btnComment_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO Comments(Comment,PostId,UId,CreatedDate) VALUES(@comment,'" + Request.QueryString["pid"]+"','"+ Session["userId"] +"',GETDATE())";
            cmd.Parameters.AddWithValue("@comment", txtComment.Text);
            cmd.ExecuteNonQuery();
            cmd.CommandText = "UPDATE Post SET TotalComments = TotalComments+1,Views=Views+1 where PostId = "+Request.QueryString["pid"];
            cmd.ExecuteNonQuery();
            Response.Redirect(Request.RawUrl);
            con.Close();
        }
        protected void btnAccess_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO PrivateTopics(PostId,RequestedBy) VALUES('" + Request.QueryString["pid"] + "','" + Session["userId"] + "')";
            cmd.ExecuteNonQuery();
            Response.Redirect(Request.RawUrl);
            con.Close();
        }
    }
}