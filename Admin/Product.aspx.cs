using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication3;

namespace Food_Ordering_Website.Admin
{
    public partial class Product : System.Web.UI.Page
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
                    Response.Redirect("~/User/Login.aspx");
                }
                else
                {
                    getProduct();

                }
            }
            lblMsg.Visible = false;

        }
        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Food Ordering Website\App_Data\Database1.mdf;Integrated Security=True");
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty, action = string.Empty;
            bool isValidToExecute = false;
            bool isactive = (cbIsActive.Checked == true ? true : false);
            int productId = Convert.ToInt32(hdnID.Value);
            cmd = con.CreateCommand();
            action = (productId == 0 ? "Insert" : "Update");
            //string imagepath = string.Empty;
            if (fuProductImage.HasFile)
            {
                if (Utils.IsValidExtension(fuProductImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(fuProductImage.FileName);
                    imagePath = "Images/Product/" + obj.ToString() + fileExtension;
                    fuProductImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + obj.ToString() + fileExtension);
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
                    if (productId == 0)
                    {
                        cmd.CommandText = "insert into Products(Name, Description, Price, Quantity,ImageUrl,CategoryId,IsActive,CreatedDate) Values('" + txtName.Text.Trim() + "','" + txtDescription.Text.Trim() + "','" + txtPrice.Text.Trim() + "','" + txtQuantity.Text.Trim() + "','" + imagePath + "','" + ddlCategory.SelectedValue + "','" + isactive + "', GETDATE())";
                        //                      cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        if (imagePath == string.Empty)
                        {
                            cmd.CommandText = "UPDATE Products SET Name = '" + txtName.Text.Trim() + "', Description = '" + txtDescription.Text.Trim() + "', Price = '" + txtPrice.Text.Trim() + "',Quantity = '" + txtQuantity.Text.Trim() + "', CategoryId = '" + ddlCategory.SelectedValue + "', IsActive = '" + isactive + "' WHERE ProductId = " + productId;
                        }
                        else
                        {
                            cmd.CommandText = "UPDATE Products SET Name = '" + txtName.Text.Trim() + "', Description = '" + txtDescription.Text.Trim() + "', Price = '" + txtPrice.Text.Trim() + "', ImageUrl = '" + imagePath + "',Quantity = '" + txtQuantity.Text.Trim() + "', CategoryId = '" + ddlCategory.SelectedValue + "', IsActive = '" + isactive + "' WHERE ProductId = " + productId;

                        }
                    }
                    cmd.ExecuteNonQuery();
                    actionName = productId == 0 ? "inserted" : "upadted";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product " + actionName + " Successfully!";
                    getProduct();
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

        private void getProduct()
        {
            //throw new NotImplementedException();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Food Ordering Website\App_Data\Database1.mdf;Integrated Security=True");
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT p.*,c.Name as CategoryName FROM Products p inner join Category c on c.CategoryId = p.CategoryId Order By p.CreatedDate Desc";
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
            txtQuantity.Text = string.Empty;
            txtPrice.Text = string.Empty;
            ddlCategory.ClearSelection();
            cbIsActive.Checked = false;
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
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Food Ordering Website\App_Data\Database1.mdf;Integrated Security=True");
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (e.CommandName == "edit")
            {
                cmd.CommandText = "SELECT p.*,c.Name as CategoryName FROM Products p inner join Category c on c.CategoryId = p.CategoryId where p.ProductId = '" + e.CommandArgument + "'";
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                txtName.Text = dt.Rows[0]["Name"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                txtPrice.Text = dt.Rows[0]["Price"].ToString();
                txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
                ddlCategory.SelectedValue = dt.Rows[0]["CategoryId"].ToString();
                imgProduct.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageUrl"].ToString()) ? "../Images/No_image.png" : "../" + dt.Rows[0]["ImageUrl"].ToString();
                imgProduct.Height = 200;
                imgProduct.Width = 200;
                hdnID.Value = dt.Rows[0]["ProductId"].ToString();
                btnAddOrUpdate.Text = "Update";
                LinkButton btn = e.Item.FindControl("lnkEdit") as LinkButton;
                btn.CssClass = "badge badge-warning";
            }
            else if (e.CommandName == "delete")
            {
                //    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Food Ordering Website\App_Data\Database1.mdf;Integrated Security=True");
                //  cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM Products WHERE ProductId = " + e.CommandArgument;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getProduct();
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
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblIsActive = e.Item.FindControl("lblIsActive") as Label;
                Label lblQuantity = e.Item.FindControl("lblQantity") as Label;
                if (lblIsActive.Text == "True")
                {
                    lblIsActive.Text = "Active";
                    lblIsActive.CssClass = "badge badge-success";
                }
                else
                {
                    lblIsActive.Text = "In-Active";
                    lblIsActive.CssClass = "badge badge-danger";
                }
                if (Convert.ToInt32(lblQuantity.Text) <= 5)
                {
                    lblQuantity.CssClass = "badge badge-danger";
                    lblQuantity.ToolTip = "Item about to be 'Out of stock'!";
                }
            }
        }
    }
}