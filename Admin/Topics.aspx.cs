using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3.Admin
{
    public partial class Topics : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Golu\source\repos\WebApplication3\App_Data\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Topics";
                if (Session["admin"] == null)
                {
                    Response.Redirect("~/Users/Login.aspx");
                }
                else
                {
                    getTopics();
                }
            }
            lblMsg.Visible = false;
        }

        private void getTopics()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "select t.TopicID,t.TName,t.BackImage,t.Description,t.CreatedDate, count(p.TopicId) as countPosts from Topic t INNER JOIN Post p on t.TopicID = p.TopicId group by t.TopicID,t.TName,t.BackImage,t.Description,t.CreatedDate ORDER BY CreatedDate DESC";
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rTopics.DataSource = dt;
            rTopics.DataBind();
            con.Close();
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty, action = string.Empty;
            bool isValidToExecute = false;
            int categoryId = Convert.ToInt32(hdnID.Value);
            cmd = con.CreateCommand();
            action = (categoryId == 0 ? "Insert" : "Update");
            //string imagepath = string.Empty;
            if (fuCategoryImage.HasFile)
            {
                if (Utils.IsValidExtension(fuCategoryImage.FileName))

                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(fuCategoryImage.FileName);
                    imagePath = "Images/Topics/" + obj.ToString() + fileExtension;
                    fuCategoryImage.PostedFile.SaveAs(Server.MapPath("~/Images/Topics/") + obj.ToString() + fileExtension);
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
                try
                {
                    con.Open();
                    if (categoryId == 0)
                    {
                        cmd.CommandText = "insert into Topic(TName, BackImage, Description, CreatedDate) Values('" + txtName.Text.Trim() + "','" + imagePath + "','" + txtDescription.Text.Trim() + "', GETDATE())";
                        //                      cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        if (imagePath == string.Empty)
                        {
                            cmd.CommandText = "UPDATE Topic SET TName = '" + txtName.Text.Trim() + "',Description = '" + txtDescription.Text.Trim() + "' WHERE TopicId = " + categoryId;
                        }
                        else
                        {
                            cmd.CommandText = "UPDATE Topic SET TName = '" + txtName.Text.Trim() + "',Description = '" + txtDescription.Text.Trim() + "', BackImage = '" + imagePath + "' WHERE ToicId = " + categoryId;
                        }
                    }
                    cmd.ExecuteNonQuery();
                    actionName = categoryId == 0 ? "inserted" : "upadted";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Topic " + actionName + " Successfully!";
                    con.Close();
                    getTopics();
                    clear();
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
        private void clear()
        {
            txtName.Text = string.Empty;
            hdnID.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imgCategory.ImageUrl = String.Empty;
            txtDescription.Text = "";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void rTopics_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (e.CommandName == "edit")
            {
                cmd.CommandText = "SELECT * FROM Topic WHERE TopicId = " + e.CommandArgument;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                txtName.Text = dt.Rows[0]["TName"].ToString();
                imgCategory.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["BackImage"].ToString()) ? "../Images/No_image.png" : "../" + dt.Rows[0]["BackImage"].ToString();
                imgCategory.Height = 200;
                imgCategory.Width = 200;
                hdnID.Value = dt.Rows[0]["TopicID"].ToString();
                btnAddOrUpdate.Text = "Update";
                LinkButton btn = e.Item.FindControl("lnkEdit") as LinkButton;
                btn.CssClass = "badge badge-warning";
            }
            else if (e.CommandName == "delete")
            {
                //    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Food Ordering Website\App_Data\Database1.mdf;Integrated Security=True");
                //  cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM Topic WHERE TopicId = " + e.CommandArgument;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Select * FROM SubTopic WHERE TopicId = " + e.CommandArgument;
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    int rowCount = (int)cmd.ExecuteScalar();
                    //int[] SubTId = new int[rowCount];
                    for(int i = 0; i < rowCount; i++)
                    {
                        //SubTId[i] = (int)dt.Rows[0]["SubTId"];
                        cmd.CommandText = "Delete from Categories where SubTId = " + (int)dt.Rows[0]["SubTId"];
                        cmd.ExecuteNonQuery();
                    }
                    cmd.CommandText = "Delete from SubTopic where TopicId = " + e.CommandArgument;
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Topic deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    con.Close();
                    getTopics();
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

        protected void rTopics_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}