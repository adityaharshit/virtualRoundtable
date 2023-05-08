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
    public partial class SubTopics : System.Web.UI.Page
    {
            SqlCommand cmd;
            SqlDataAdapter sda;
            DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Product";
                if (Session["admin"] == null)
                {
                    Response.Redirect("~/Users/Login.aspx");
                }
                else
                {
                    getSubTopic();

                }
            }
            lblMsg.Visible = false;
        }
        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Golu\source\repos\WebApplication3\App_Data\Database1.mdf;Integrated Security=True");
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty, action = string.Empty;
            bool isValidToExecute = false;
            
            int SubTId = Convert.ToInt32(hdnID.Value);
            cmd = con.CreateCommand();
            action = (SubTId == 0 ? "Insert" : "Update");
            //string imagepath = string.Empty;
            if (fuProductImage.HasFile)
            {
                if (Utils.IsValidExtension(fuProductImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(fuProductImage.FileName);
                    imagePath = "Images/SubTopic/" + obj.ToString() + fileExtension;
                    fuProductImage.PostedFile.SaveAs(Server.MapPath("~/Images/SubTopic/") + obj.ToString() + fileExtension);
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
                    if (SubTId == 0)
                    {
                        cmd.CommandText = "insert into SubTopic(Name, Description, Image,TopicId,CreatedDate) Values('" + txtName.Text.Trim() + "','" + txtDescription.Text.Trim() + "','" + imagePath + "','" + ddlTopic.SelectedValue + "', GETDATE())";
                        //                      cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        if (imagePath == string.Empty)
                        {
                            cmd.CommandText = "UPDATE SubTopic SET Name = '" + txtName.Text.Trim() + "', Description = '" + txtDescription.Text.Trim() + "', TopicId = '" + ddlTopic.SelectedValue + "' WHERE SubTId= " + SubTId;
                        }
                        else
                        {
                            cmd.CommandText = "UPDATE SubTopic SET Name = '" + txtName.Text.Trim() + "', Description = '" + txtDescription.Text.Trim() + "', Image= '" + imagePath + "', TopicId= '" + ddlTopic.SelectedValue + "' WHERE SubTId = " + SubTId;

                        }
                    }
                    cmd.ExecuteNonQuery();
                    actionName = SubTId == 0 ? "inserted" : "upadted";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product " + actionName + " Successfully!";
                    getSubTopic();
                    clear();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error - " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void getSubTopic()
        {
            //throw new NotImplementedException();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Golu\source\repos\WebApplication3\App_Data\Database1.mdf;Integrated Security=True");
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT s.*,t.TName FROM SubTopic s inner join Topic t on t.TopicId= s.TopicId Order By s.CreatedDate Desc";
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rProduct.DataSource = dt;
            rProduct.DataBind();
        }

        private void clear()
        {
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            ddlTopic.ClearSelection();
            hdnID.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imgProduct.ImageUrl = String.Empty;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void rProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Golu\source\repos\WebApplication3\App_Data\Database1.mdf;Integrated Security=True");
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (e.CommandName == "edit")
            {
                cmd.CommandText = "SELECT s.*,t.TName FROM SubTopic s inner join Topic t on t.TopicId= s.TopicId where s.SubTId = '" + e.CommandArgument + "'";
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                ddlTopic.SelectedValue = dt.Rows[0]["TopicId"].ToString();
                imgProduct.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["Image"].ToString()) ? "../Images/No_image.png" : "../" + dt.Rows[0]["Image"].ToString();
                imgProduct.Height = 200;
                imgProduct.Width = 200;
                hdnID.Value = dt.Rows[0]["SubTId"].ToString();
                btnAddOrUpdate.Text = "Update";
                LinkButton btn = e.Item.FindControl("lnkEdit") as LinkButton;
                btn.CssClass = "badge badge-warning";
            }
            else if (e.CommandName == "delete")
            {
                
                cmd.CommandText = "DELETE FROM SubTopic WHERE SubTId = " + e.CommandArgument;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Delete FROM Categories WHERE SubTId = " + e.CommandArgument;
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getSubTopic();
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

        protected void rProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }
    }
}