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
    public partial class Category : System.Web.UI.Page
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
                    getCategories();

                }
            }
            lblMsg.Visible = false;
        }
        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Golu\source\repos\WebApplication3\App_Data\Database1.mdf;Integrated Security=True");
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty, action = string.Empty;
            bool isValidToExecute = false;

            int CatId = Convert.ToInt32(hdnID.Value);
            cmd = con.CreateCommand();
            action = (CatId == 0 ? "Insert" : "Update");
            //string imagepath = string.Empty;
            if (fuProductImage.HasFile)
            {
                if (Utils.IsValidExtension(fuProductImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(fuProductImage.FileName);
                    imagePath = "Images/Categories/" + obj.ToString() + fileExtension;
                    fuProductImage.PostedFile.SaveAs(Server.MapPath("~/Images/Categories/") + obj.ToString() + fileExtension);
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
                    if (CatId == 0)
                    {
                        cmd.CommandText = "insert into Categories(Name, Description, Image,SubTId,CreatedDate) Values('" + txtName.Text.Trim() + "','" + txtDescription.Text.Trim() + "','" + imagePath + "','" + ddlSubT.SelectedValue + "', GETDATE())";
                        //                      cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        if (imagePath == string.Empty)
                        {
                            cmd.CommandText = "UPDATE Categories SET Name = '" + txtName.Text.Trim() + "', Description = '" + txtDescription.Text.Trim() + "', SubTId = '" + ddlSubT.SelectedValue + "' WHERE CatId= " + CatId;
                        }
                        else
                        {
                            cmd.CommandText = "UPDATE Categories SET Name = '" + txtName.Text.Trim() + "', Description = '" + txtDescription.Text.Trim() + "',Image= '" + imagePath + "',SubTId= '" + ddlSubT.SelectedValue + "' WHERE CatId = " + CatId;

                        }
                    }
                    cmd.ExecuteNonQuery();
                    actionName = CatId == 0 ? "inserted" : "upadted";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Category " + actionName + " Successfully!";
                    getCategories();
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

        private void getCategories()
        {
            //throw new NotImplementedException();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Golu\source\repos\WebApplication3\App_Data\Database1.mdf;Integrated Security=True");
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT c.*,s.Name as SubName FROM Categories c inner join SubTopic s on s.SubTId= c.SubTId Order By c.CreatedDate Desc";
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rCategories.DataSource = dt;
            rCategories.DataBind();
        }

        private void clear()
        {
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            ddlSubT.ClearSelection();
            hdnID.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imgProduct.ImageUrl = String.Empty;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void rCategories_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Golu\source\repos\WebApplication3\App_Data\Database1.mdf;Integrated Security=True");
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (e.CommandName == "edit")
            {
                cmd.CommandText = "SELECT c.*,s.Name as SName FROM Categories c inner join SubTopic s on s.SubTId= c.SubTId where c.CatId = '" + e.CommandArgument + "'";
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                ddlSubT.SelectedValue = dt.Rows[0]["SubTId"].ToString();
                imgProduct.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["Image"].ToString()) ? "../Images/No_image.png" : "../" + dt.Rows[0]["Image"].ToString();
                imgProduct.Height = 200;
                imgProduct.Width = 200;
                hdnID.Value = dt.Rows[0]["CatId"].ToString();
                btnAddOrUpdate.Text = "Update";
                LinkButton btn = e.Item.FindControl("lnkEdit") as LinkButton;
                btn.CssClass = "badge badge-warning";
            }
            else if (e.CommandName == "delete")
            {

                cmd.CommandText = "DELETE FROM Categories WHERE CatId = " + e.CommandArgument;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getCategories();
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

        protected void rCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}