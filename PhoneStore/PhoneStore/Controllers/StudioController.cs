using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneStore.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace PhoneStore.Controllers
{
    public class StudioController : Controller
    {
        // GET: Studio
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Home()
        {
            return View();
        }
        Order orderPerson = new Order();
        public ActionResult Detail(string path, string name)
        {
            ViewData["path"] = path;
            ViewData["productName"] = name;
            
            //string connStr = ConfigurationManager.ConnectionStrings["Studio"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(connStr))
            //{
            //    string query = "INSERT INTO Details(Name, Address, Phone, Email, Product) VALUES(@FullName, @Cus_Address, @MobilePhone, @Email, @ProductName)"; 

            //    query += " SELECT SCOPE_IDENTITY()";
            //    string new_query = "INSERT INTO Details(Name) VALUES (@Name)";
            //    using (SqlCommand cmd = new SqlCommand(new_query))
            //    {
            //        cmd.Connection = con;
            //        con.Open();
            //        //cmd.Parameters.AddWithValue("@FullName", orderPerson.FullName);

            //        //cmd.Parameters.AddWithValue("@Cus_Address", orderPerson.Address);
            //        //cmd.Parameters.AddWithValue("@MobilePhone", orderPerson.Phone);
            //        //cmd.Parameters.AddWithValue("@Email", orderPerson.Email);
            //        //cmd.Parameters.AddWithValue("@ProductName", orderPerson.Product);

            //        //name
            //        //if (String.IsNullOrEmpty(orderPerson.FullName))
            //        //{

            //        //    cmd.Parameters.AddWithValue("@FullName", "");
            //        //}
            //        //else
            //        //{

            //        //    cmd.Parameters.AddWithValue("@FullName", orderPerson.FullName);
            //        //}
            //        ////address
            //        //if (String.IsNullOrEmpty(orderPerson.Address))
            //        //{


            //        //    cmd.Parameters.AddWithValue("@Cus_Address", "");
            //        //}
            //        //else
            //        //{
            //        //    cmd.Parameters.AddWithValue("@Cus_Address", orderPerson.Address);
            //        //}
            //        //// phone
            //        //if (String.IsNullOrEmpty(orderPerson.Phone))
            //        //{

            //        //    cmd.Parameters.AddWithValue("@MobilePhone", "");
            //        //}
            //        //else
            //        //{

            //        //    cmd.Parameters.AddWithValue("@MobilePhone", orderPerson.Phone);
            //        //}
            //        //// Email
            //        //if (String.IsNullOrEmpty(orderPerson.Email))
            //        //{


            //        //    cmd.Parameters.AddWithValue("@Email", "");
            //        //}
            //        //else
            //        //{
            //        //    cmd.Parameters.AddWithValue("@Email", orderPerson.Email);
            //        //}
            //        //// Product
            //        //if (String.IsNullOrEmpty(orderPerson.Email))
            //        //{

            //        //    cmd.Parameters.AddWithValue("@ProductName", "");
            //        //}
            //        //else
            //        //{

            //        //    cmd.Parameters.AddWithValue("@ProductName", orderPerson.Product);
            //        //}
            //        //cmd.Parameters.Add("@Name", SqlDbType.NVarChar,100);
            //        //cmd.Parameters["@Name"].Value = orderPerson.FullName;
            //        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
            //        cmd.Parameters["@Name"].Value = orderPerson.Name;
            //        //cmd.Parameters.AddWithValue("@Name", orderPerson.Name);
            //        //cmd.Parameters.Add("@Cus_Address", SqlDbType.NVarChar,100).Value = orderPerson.Address;
            //        //cmd.Parameters.Add("@MobilePhone", SqlDbType.NVarChar,100).Value = orderPerson.Phone;
            //        //cmd.Parameters.Add("@Email", SqlDbType.NVarChar,100).Value = orderPerson.Email;
            //        //cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar,100).Value = orderPerson.Product;
            //        //orderPerson.OrderId = Convert.ToInt32(cmd.ExecuteScalar());
            //        //DataTable sourceTable = new DataTable();
            //        //foreach(  DataRow r in sourceTable.Rows)
            //        //{
            //        //    for(int i = 0; i < 5; i++)
            //        //    {
            //        //        cmd.Parameters[i].Value = r[i];
            //        //    }

            //        //}



            //        cmd.ExecuteNonQuery();
            //        con.Close();
            //    }
            //}
            return View("Detail");
        }

        public string Encrypt(string orderObj, int k)
        {
            char[] dschar = orderObj.ToCharArray();

            for (int i = 0; i < dschar.Length; i++)
            {
                //Console.WriteLine(dschar[i]);
                int ds = (int)dschar[i] + k;
                dschar[i] = (char)ds

                    ;
                Console.WriteLine(dschar[i]);
            }
            //orderObj = dschar.ToString();
            orderObj = "";
            foreach (char item in dschar)
            {
                orderObj += item;
            }
            return orderObj;
        }

        public void Buy(FormCollection fc)
        {
            orderPerson.FullName    = Request.Form["FullName"];
            orderPerson.Address     = Request.Form["Address"];
            orderPerson.Phone       = Request.Form["Phone"];
            orderPerson.Email       = Request.Form["Email"];
            orderPerson.Product     = Request.Form["Product"];
            int k = 5;

            string fullname  = Encrypt(orderPerson.FullName, k);
            string address   =  Encrypt(orderPerson.Address, k);
            string phone     =   Encrypt(orderPerson.Phone, k);
            string email     =  Encrypt(orderPerson.Email, k);
            string product   = Encrypt(orderPerson.Product, k);

            string constr = ConfigurationManager.ConnectionStrings["Studio"].ConnectionString;
            Response.Write("Success");
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "insert into Details(Name,Address,Phone,Email,Product)values(@name, @address,@phone,@email,@product)";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    
                    con.Open();
                    //cmd.Parameters.AddWithValue("@Name", customer.Name);
                    //cmd.Parameters.AddWithValue("@Country", customer.Country);
                    //customer.CustomerId = Convert.ToInt32(cmd.ExecuteScalar());
                    //SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    //cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", fullname);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@product", product);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            
        }

    }
}