using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3.Users
{
    public partial class registration : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Golu\source\repos\WebApplication3\App_Data\Database1.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userId"] != null)
                {
                    if (Request.RawUrl.Contains("id"))
                    {
                        if(Session["userId"].ToString()== Request.QueryString["id"])
                        {
                            getUserDetails();
                            btnSignUp.Text = "Update";
                        }
                        else
                        {
                            Session.Abandon();
                            Response.Redirect("login.aspx");
                        }
                    }
                }
            }
        }

        private void getUserDetails()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from Users where UID = " + Request.QueryString["id"];
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtName.Text = dr.GetString(1);
                txtEmail.Text = dr.GetString(2);
                txtNumber.Text = dr.GetDecimal(3).ToString();
                txtUsername.Text = dr.GetString(4);
                txtdob.Text = dr.GetDateTime(6).ToString("dd/MM/yyyy");
                txtgender.Text = dr.GetString(7);
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            string currDate = date.ToString("dd-MM-yyyy");
            string Name = txtName.Text;
            string Email = txtEmail.Text;
            string Username = txtUsername.Text;
            string Phone = txtNumber.Text;
            string DOB = txtdob.Text;
            string Gender = txtgender.Text;
            string Pass = pass.Text;
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int userId = Convert.ToInt32(Request.QueryString["id"]);
            
            if (fuUserImage.HasFile)
            {
                if (Utils.IsValidExtension(fuUserImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(fuUserImage.FileName);
                    imagePath = "Images/" + obj.ToString() + fileExtension;
                    fuUserImage.PostedFile.SaveAs(Server.MapPath("~/Images/") + obj.ToString() + fileExtension);

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
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    if (btnSignUp.Text == "Sign Up")
                    {
                        cmd.CommandText = "Select * from Users where Email = '" + Email + "'";
                    }
                    else
                    {
                        cmd.CommandText = "Select * from Users where Email = '" + Email + "' and NOT UId="+Request.QueryString["id"];
                    }
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        Response.Write("Email already registered");
                        dr.Close();
                    }
                    else
                    {
                        dr.Close();
                        if (btnSignUp.Text == "Sign Up")
                        {
                            cmd.CommandText = "Select * from Users where Username = '" + Username + "'";
                        }
                        else
                        {
                            cmd.CommandText = "Select * from Users where Username = '" + Username + "'and NOT UId=" + Request.QueryString["id"];
                        }
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Username already exists";
                            dr.Close();
                        }
                        else
                        {
                            dr.Close();
                            if(btnSignUp.Text == "Sign Up")
                            {
                                cmd.CommandText = "Insert into Users(Name,Email,Phone,Username,Password,DOB,Gender,JoinedOn,ProfilePic) " +
                                    "Values(@name,@email,@phone,@username,@pass,'" + Convert.ToDateTime( DOB).ToString("yyyy-MM-dd")+"'," +
                                    "@gender,GETDATE(),@imagePath)";
                                cmd.Parameters.AddWithValue("@name",Name);
                                cmd.Parameters.AddWithValue("@email",Email);
                                cmd.Parameters.AddWithValue("@phone",Phone);
                                cmd.Parameters.AddWithValue("@username",Username);
                                cmd.Parameters.AddWithValue("@pass",Pass);
                                cmd.Parameters.AddWithValue("@gender",Gender);
                                cmd.Parameters.AddWithValue("@imagePath",imagePath);
                                cmd.ExecuteNonQuery();
                                Response.Write("<script language = 'javascript'>window.alert('User Registered!');window.location='login.aspx';</script>");
                            }
                            else if(btnSignUp.Text == "Update")
                            {
                                if (fuUserImage.HasFile)
                                {
                                    cmd.CommandText = "UPDATE Users SET Name='" + Name + "',Email='" + Email + "',Phone = '" + Phone + "',Username='" + Username + "',Password='" + Pass + "',DOB='" + Convert.ToDateTime(DOB).ToString("yyyy-MM-dd") + "',Gender='" + Gender + "',ProfilePic='" + imagePath + "' where UId = " + Request.QueryString["id"];
                                }   
                                else
                                {
                                    cmd.CommandText = "UPDATE Users SET Name='" + Name + "',Email='" + Email + "',Phone = '" + Phone + "',Username='" + Username + "',Password='" + Pass + "',DOB='" + Convert.ToDateTime(DOB).ToString("yyyy-MM-dd") + "',Gender='" + Gender + "' where UId = "+Request.QueryString["id"];
                                }
                                cmd.ExecuteNonQuery();
                                Response.Write("<script language = 'javascript'>window.alert('User Details Updated!');window.location='profile.aspx';</script>");
                            }
                            
                        }
                    }

                }
                catch(Exception ex){
                    lblMsg.Visible = true;
                    lblMsg.Text = ex.ToString();
                }

            }
            else
            {
                Console.WriteLine("Else Block");
            }
        }

        

    }
}