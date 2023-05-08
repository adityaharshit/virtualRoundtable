using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Food_Ordering_Website.Admin
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Orders";
                if (Session["admin"] == null)
                {
                    Response.Redirect("~/User/Login.aspx");
                }
                else
                {

                    getOrders();
                }
            }
            lblMsg.Visible = false;
        }

        private void getOrders()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Food Ordering Website\App_Data\Database1.mdf;Integrated Security=True");
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "select o.OrderNo, o.Quantity, o.Status, pay.Address, pay.PaymentMode as Mode, u.Name as UserName, s.Name as DelMan,p.Name as Product from((((Orders o INNER JOIN Payment pay on o.PaymentId = pay.PaymentId) INNER JOIN Products p on o.ProductId = p.ProductId) INNER JOIN Users u on o.UserId = u.UserId)INNER JOIN Staff s on o.DMan = s.DId) ORDER BY o.OrderDate DESC";
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rOrders.DataSource = dt;
            rOrders.DataBind();
        }
    }
}
//< td class= "table-plus" > <%# Eval("OrderNo") %> </td>//
//< td ><%# Eval("User") %></td>//
//< td ><%# Eval("Product") %></td>
//< td ><%# Eval("Quantity") %></td>//
//< td ><%# Eval("Address") %></td> //
//< td ><%# Eval("DelMan") %></td>//
//< td ><%# Eval("Status") %></td>//
//< td ><%# Eval("Mode") %></td>//