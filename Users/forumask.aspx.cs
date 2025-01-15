using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3.Users
{
    public partial class forumask : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
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
            }
            lblMsg.Visible = false;
        }

        protected void btnAsk_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty, action = string.Empty;
            bool isValidToExecute = false;
            cmd = con.CreateCommand();
            int isPrivate=0;
            if (chkIsPrivate.Checked == true)
            {
                isPrivate = 1;
            }
            //string imagepath = string.Empty;
            if (imagePost.HasFile)
            {
                if (Utils.IsValidExtension(imagePost.FileName))

                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(imagePost.FileName);
                    imagePath = "Images/Post/" + obj.ToString() + fileExtension;
                    imagePost.PostedFile.SaveAs(Server.MapPath("~/Images/Post/") + obj.ToString() + fileExtension);
                    //    cmd.Parameters.AddWithValue("@ImageUrl", imagePath);
                    isValidToExecute = true;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Please select .jpg, .jpeg or .png image";
                    lblMsg.CssClass = "alert alert-danger";
                    isValidToExecute = false;
                }
            }
            else
            {
                isValidToExecute = true;
            }

            if (isValidToExecute)
            {
                //cmd.CommandType = CommandType.StoredProcedure;
                if (txtTitle.ToString().Length > 60)
                {
                    Response.Write("<script>alert('Title too long. Max Length is 60 characters');");
                }
                else if (txtDesc.ToString().Length < 20)
                {
                    Response.Write("<script>alert('Description too short. Min Length is 20 characters');");
                }
                else
                {
                    try
                    {
                        con.Open();
                        string[] tags = txtTags.Text.Split(',');
                        int count = tags.Length;
                        while (count > 0)
                        {
                            count--;
                            cmd.CommandText = "SELECT * FROM Tags where TagName = '" + tags[count] + "' AND TopicId = '" + ddlTopic.SelectedValue + "'";
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                dr.Close();
                            }
                            else
                            {
                                dr.Close();
                                cmd.CommandText = "INSERT INTO Tags(TagName,TopicId) Values('"+tags[count]+"','"+ ddlTopic.SelectedValue + "')";
                                cmd.ExecuteNonQuery();
                            }
                        }
                        cmd.CommandText = "INSERT INTO Post(PostTitle, PostDescription, PostImage, Tags, TopicId, SubTId, CreatedDate, isPrivate, UId) " +
                        "Values(@title, @description, @imagePath, @tags, @topicID, @subTopicID, GETDATE(), '" + isPrivate + "','" + Convert.ToInt32(Session["userId"].ToString()) + "')";
                        cmd.Parameters.AddWithValue("@description",txtDesc.Text);
                        cmd.Parameters.AddWithValue("@title",txtTitle.Text);
                        cmd.Parameters.AddWithValue("@imagePath",imagePath);
                        cmd.Parameters.AddWithValue("@tags",txtTags.Text);
                        cmd.Parameters.AddWithValue("@topicID",ddlTopic.SelectedValue);
                        cmd.Parameters.AddWithValue("@subTopicID",ddlSubTopic.SelectedValue);
                        cmd.ExecuteNonQuery();
                        lblMsg.Visible = true;
                        lblMsg.Text = "Posted Successfully!";
                        con.Close();
                        clear();
                        Response.Redirect("forum.aspx?id=1");
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Error - " + ex.Message;
                        lblMsg.CssClass = "alert alert-danger";
                        con.Close();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        private void clear()
        {
            txtDesc.Text = "";
            txtTags.Text = "";
            txtTitle.Text = "";
            chkIsPrivate.Checked = false;
        }
    }
}