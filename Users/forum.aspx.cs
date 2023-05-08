using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication3.Users
{
    public partial class forum : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        string gfollowing = "",gLiked="",gDisliked="", blocked="",savedPost="",hiddenPost="";
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Golu\source\repos\WebApplication3\App_Data\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox txtSearch = Master.FindControl("txtSearch") as TextBox;
            if (txtSearch != null)
            {
                txtSearch.AutoPostBack = true;
                txtSearch.Enabled = true;
                txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            }
            if (!IsPostBack)
            {
                //Session["breadCrum"] = "Topics";
                if (Session["userId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    if (Request.QueryString.Count > 0)
                    {
                        if(Request.QueryString["filter"] != null)
                        {

                        }
                    }
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT Following,LikedPosts,DislikedPosts,BlockedUser,SavePost,HidenPost from Users where UId = " + Session["userId"];
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        gfollowing = dr.GetString(0);
                        gLiked = dr.GetString(1);
                        gDisliked = dr.GetString(2);
                        blocked = dr.GetString(3);
                        savedPost = dr.GetString(4);
                        hiddenPost= dr.GetString(5);
                    }
                    con.Close();
                    getTopic();
                    getSubTopic();
                    getTags();
                }
            }
            //lblMsg.Visible = false;
           
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string query = "";
            TextBox txtSearch = sender as TextBox;
            if (Request.QueryString["id"] != null && Request.QueryString["sbid"] != null)
            {
                query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + " and p.SubTId = " + Request.QueryString["sbid"] + " and p.PostTitle LIKE '%" + txtSearch.Text + "%'Order by CreatedDate DESC";
            }
            else if (Request.QueryString["id"] != null)
            {
                query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + " and p.PostTitle LIKE '%"+txtSearch.Text+"%' Order by CreatedDate DESC";
            }
            getPosts(query);    
        }

        private void getTags()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * from Tags where TopicId = " + Request.QueryString["id"];
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rTags.DataSource = dt;
            rTags.DataBind();
            con.Close();
        }

        private void getSubTopic()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            string query = "";
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * from SubTopic ";
            if (Request.QueryString.Count > 0)
            {
                if(Request.QueryString["id"]!=null && Request.QueryString["sbid"]!=null)
                {
                    cmd.CommandText = "SELECT * from SubTopic where TopicId = " + Request.QueryString["id"];
                    query="SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] +" and p.SubTId = " + Request.QueryString["sbid"] + "Order by CreatedDate DESC";
                }
                else if(Request.QueryString["id"] != null)
                {
                    cmd.CommandText = "SELECT * from SubTopic where TopicId = " + Request.QueryString["id"];
                    query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + "Order by CreatedDate DESC";
                }
                
            }
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rSubTopic.DataSource = dt;
            rSubTopic.DataBind();
            con.Close();
            getPosts(query);
        }

        private void getTopic()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * from Topic";
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rTopics.DataSource = dt;
            rTopics.DataBind();
            con.Close();
        }

     

        protected void faclick()
        {

        }

        private void getPosts(String query)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = query;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rPosts.DataSource = dt;
            rPosts.DataBind();
            con.Close();
        }

        protected void lnbNew_Click(object sender, EventArgs e)
        {
            string query="";
            if (Request.QueryString["id"] != null && Request.QueryString["sbid"] != null)
            {
                query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + " and p.SubTId = " + Request.QueryString["sbid"] + "Order by CreatedDate DESC";
            }
            else if (Request.QueryString["id"] != null)
            {
                query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + "Order by CreatedDate DESC";
            }
            getPosts(query);
            //Response.Redirect(Request.RawUrl + "&filter=New");
        }

        protected void lnbHits_Click(object sender, EventArgs e)
        {
            string query = "";
            if (Request.QueryString["id"] != null && Request.QueryString["sbid"] != null)
            {
                query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + " and p.SubTId = " + Request.QueryString["sbid"] + "Order by Views DESC";
            }
            else if (Request.QueryString["id"] != null)
            {
                query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + "Order by Views DESC";
            }
            getPosts(query);
            //Response.Redirect(Request.RawUrl + "&filter=Hits");
        }

        protected void lnbRandom_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Request.RawUrl + "&filter=Random");
            string query = "";
            if (Request.QueryString["id"] != null && Request.QueryString["sbid"] != null)
            {
                query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + " and p.SubTId = " + Request.QueryString["sbid"] + "Order by CreatedDate DESC";
            }
            else if (Request.QueryString["id"] != null)
            {
                query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + "Order by CreatedDate DESC";
            }
            getPosts(query);
        }

        protected void lnbFollowing_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Request.RawUrl + "&filter=Following");
            string query = "";
            if (Request.QueryString["id"] != null && Request.QueryString["sbid"] != null)
            {
                query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + " and p.SubTId = " + Request.QueryString["sbid"] + " and u.Followers LIKE '%"+Session["userId"]+"%' Order by CreatedDate DESC";
            }
            else if (Request.QueryString["id"] != null)
            {
                query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + " and u.Followers LIKE '%" + Session["userId"] + "%' Order by CreatedDate DESC";
            }
            getPosts(query);
        }

        //protected void btnDates_Click(object sender, EventArgs e)
        //{
        //    string query = "";
        //    string contentAfter = (txtContentAfter.Text);
        //    if (Request.QueryString["id"] != null && Request.QueryString["sbid"] != null)
        //    {
        //        query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + " and p.SubTId = " + Request.QueryString["sbid"] + " and (DATEDIFF(DAY,p.CreatedDate, '"+ Convert.ToDateTime(contentAfter).ToString("yyyy-MM-dd") + "')>0) p.CreatedDate Order by CreatedDate DESC";
        //    }
        //    else if (Request.QueryString["id"] != null)
        //    {
        //        query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + " and (DATEDIFF(DAY,p.CreatedDate, '" + Convert.ToDateTime(contentAfter).ToString("yyyy-MM-dd") + "')>0) Order by CreatedDate DESC";
        //    }
        //    getPosts(query);
        //}

        protected void rPosts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd = con.CreateCommand();
            if (e.CommandName== "lnkLike")
            {
                Label likesCount = (Label)e.Item.FindControl("likeCount");
                int count = Convert.ToInt32( likesCount.Text.ToString()) + 1;
                
                cmd.CommandText = "Select LikedPosts, DislikedPosts from Users where UId = " + Session["userId"];
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string likedPosts = dr.GetString(0).ToString();
                    string dislikedPosts = dr.GetString(1).ToString();
                    dr.Close();
                    if (likedPosts.Contains(e.CommandArgument.ToString()))
                    {
                        likedPosts = likedPosts.Replace(e.CommandArgument.ToString()+",","");
                        cmd.CommandText = "UPDATE Users Set LikedPosts = '" + likedPosts + "' where UId = " + Session["userId"];
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "DELETE FROM LoglkDlkSvd where Action = 'Liked' and doneBy = '" + Session["userId"] + "' and PostId = '" + e.CommandArgument + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "UPDATE Post SET Likes = Likes-1,Views = Views+1  WHERE PostId = " + e.CommandArgument;
                    }
                    else
                    {
                        likedPosts = likedPosts + e.CommandArgument.ToString()+",";
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
            else if(e.CommandName == "lnkDislike")
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
            else if(e.CommandName == "Comment")
            {
                cmd.CommandText = "UPDATE Post SET Views = Views+1  WHERE PostId = " + e.CommandArgument;
                cmd.ExecuteNonQuery();
                Response.Redirect("post.aspx?pid=" + e.CommandArgument);
            }
            else if (e.CommandName == "Save")
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
                        cmd.CommandText="UPDATE Users SET SavePost = '"+savedPosts+ "' where UId = " + Session["userId"];
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
                        cmd.CommandText = "INSERT INTO LoglkDlkSvd(Action, doneBy, PostId, doneOn) VALUES('Saved','" + Session["userId"] + "','"+ e.CommandArgument+"',GETDATE())";
                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('Post Saved')</script>");
                    }
                }
            }
            else if(e.CommandName == "ReportUser")
            {
                cmd.CommandText = "SELECT Count from ReportedUser where UId = "+e.CommandArgument;
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
            else if(e.CommandName == "ReportPost")
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
            else if (e.CommandName == "BlockUser")
            {
                blocked = blocked + e.CommandArgument + ",";
                cmd.CommandText = "UPDATE Users SET BlockedUser = '" + blocked + "' where UId = '"+Session["userId"]+"'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Insert into Log(doneBy, doneTo, action, blockedOn) Values('"+Session["userId"]+"','"+e.CommandArgument+"','block',GETDATE())";
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('User Blocked');</script>");
            }
            else if(e.CommandName == "Follow")
            {
                string following,followers="";
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
                        cmd.CommandText = "UPDATE Users SET Following = '"+following+"' where UId = "+ Session["userId"];
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
            else if (e.CommandName == "hideDelete")
            {
                string url = Request.RawUrl;
                string[] arguments = e.CommandArgument.ToString().Split(',');
                int UId = Convert.ToInt32(arguments[0]);
                int postId = Convert.ToInt32(arguments[1]);
                if (Convert.ToInt32(Session["userId"]) == UId)
                {
                    cmd.CommandText = "DELETE FROM Post where PostId = " + postId;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM PrivateTopics where PostId = " + postId;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM Comments where PostId = " + postId;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM LoglkDlkSvd where PostId = " + postId;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM ReportedPost where PostId = " + postId;
                    cmd.ExecuteNonQuery();

                    Response.Write("<script>window.alert('Post Deleted!'); window.location='"+url+"';</script>");
                }
                else
                {
                    string hidenPost = "";
                    cmd.CommandText = "SELECT HidenPost from Users where UId = " + Session["userId"];
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        hidenPost = dr.GetString(0);
                    }
                    dr.Close();
                    hidenPost = postId + "," + hidenPost;
                    cmd.CommandText = "UPDATE Users set HidenPost = '" + hidenPost + "' where UId = " + Session["userId"];
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>window.alert('Post Hidden!'); window.location='"+url+"';</script>");
                }
            }
            Response.Redirect(Request.RawUrl);
            con.Close();
        }

        protected void rPosts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                
                LinkButton lnkFollow = e.Item.FindControl("lnkFollow") as LinkButton;
                LinkButton lnbUnfollow = e.Item.FindControl("lnbUnfollow") as LinkButton;
                LinkButton Liked = e.Item.FindControl("lnkLikes") as LinkButton;
                LinkButton Dislike = e.Item.FindControl("lnkDislike") as LinkButton;
                LinkButton lnbSave = e.Item.FindControl("lnbSave") as LinkButton;
                Label lblNegative = e.Item.FindControl("lblNegative") as Label;
                HiddenField postId = e.Item.FindControl("hdnPostId") as HiddenField;
                HiddenField UID = e.Item.FindControl("hdnUId") as HiddenField;
                HiddenField hdnDesc = e.Item.FindControl("hdnDesc") as HiddenField;
                HiddenField hdnBanned = e.Item.FindControl("hdnBanned") as HiddenField;
                HiddenField hdnUBanned = e.Item.FindControl("hdnUBanned") as HiddenField;
                string desc = hdnDesc.Value;
                string negContent = "Disappointing,Terrible,Awful,Horrible,Bad,Unpleasant,Unsatisfactory,Unacceptable,Frustrating,Displeasing,Disgusting,Repulsive,Abysmal,Atrocious,Inferior,Inadequate,Substandard,Deficient,Faulty,Flawed,Shoddy,Mediocre,Poorly made,Unreliable,Uncomfortable,Irritating,Annoying,Aggravating,Miserable,Depressing,Upsetting,Disheartening,Demoralizing,Discouraging,Dismal,Gloomy,Bleak,Drab,Dreary,Tedious,Boring,Monotonous,Tiring,Exhausting,Stressful,Chaotic,Confusing,Overwhelming,Complicated,Difficult";
                string[] negative = negContent.Split(',');
                int count = 0,i=0;
                if (UID.Value == Session["userId"].ToString())
                {
                    lnkFollow.Visible = false;
                }
                while (i < 50){
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
                if(hdnBanned.Value == "1")
                {
                    UID.Parent.Visible = false;
                }
                if(hdnUBanned.Value == "1")
                {
                    UID.Parent.Visible = false;
                }
                if (hiddenPost.Contains(postId.Value.ToString()))
                {
                    UID.Parent.Visible = false;
                }
            }
        }

        protected void rSubTopic_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandArgument.ToString()== "faclick")
            {
                string query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.SubTId = "+e.CommandArgument;
                getPosts(query);
            }
            
        }

        protected void rTags_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "clkTags")
            {
                string query="";
                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString["id"] != null && Request.QueryString["sbid"] != null)
                    {
                        query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + " and p.SubTId = " + Request.QueryString["sbid"] + " and Tags LIKE '%"+ e.CommandArgument +"%'";
                    }
                    else if (Request.QueryString["id"] != null)
                    {
                        query = "SELECT p.*,  u.Name as UserName,  u.ProfilePic, u.Banned as UBanned, u.UId from Post p INNER JOIN Users u on p.UId = u.UId where p.TopicId = " + Request.QueryString["id"] + " and Tags LIKE '%" + e.CommandArgument + "%'";
                    }   
                }
                getPosts(query);
            }
        }

    }
}