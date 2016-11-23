using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tentamen.Models;

namespace Tentamen
{
    class Program
    {
        static string constring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NORTHWND;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static void Main(string[] args)
        {
            //ProductsByCategoryName("Confections"); Klar
            //SalesByTerritory(); Klar
            //EmployeesperRegion(); Nästan Klar
            //OrdersPerEmployee();
            //CustomersWithNamesLongerThan25Characters();   klar

            Console.ReadLine();

        }

        private static void OrdersPerEmployee()
        {
            
        }

        private static void CustomersWithNamesLongerThan25Characters()
        {
            using (NorthWNDContext db = new NorthWNDContext())
            {
                foreach (var cust in db.Customers)
                {
                    if (cust.ContactName.Length > 15)//Finns ingen kund med 25 tecken längsta är 22
                    {
                        Console.WriteLine(cust.CompanyName);
                    }
                }
            }
        }

        private static void EmployeesperRegion()
        {
            SqlConnection cn = new SqlConnection(constring);
            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT Region, COUNT(*) AS EmployeesPerRegion FROM Employees GROUP BY Region"; //Kan inte söka på att displaya null värde i console, det är rätt i ssms i alla fall.
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine(rd.GetString(0) + rd["EmployeesPerRegion"]);
            }
            rd.Close();
            cn.Close();
        }

        private static void SalesByTerritory()
        {
            SqlConnection cn = new SqlConnection(constring);
            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT top (5) Territories.TerritoryDescription, SUM(([Order Details].UnitPrice * [Order Details].Quantity) * (1 - [Order Details].Discount)) as FörsäljningsTotalen FROM Employees INNER JOIN EmployeeTerritories ON Employees.EmployeeID = EmployeeTerritories.EmployeeID INNER JOIN Orders ON Employees.EmployeeID = Orders.EmployeeID INNER JOIN [Order Details] ON Orders.OrderID = [Order Details].OrderID INNER JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID GROUP BY Territories.TerritoryDescription, [Order Details].UnitPrice order by FörsäljningsTotalen desc";
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine(rd.GetString(0) + rd["FörsäljningsTotalen"]);
            }
            rd.Close();
            cn.Close();
        }

        private static void ProductsByCategoryName(string categoryName)
        {
            SqlConnection cn = new SqlConnection(constring);
            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT Products.ProductName, Products.UnitPrice, Products.UnitsInStock, Categories.CategoryName FROM Products INNER JOIN Categories ON Products.CategoryID = Categories.CategoryID where CategoryName = @CategoryName";
            cmd.Parameters.AddWithValue("@CategoryName", categoryName);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine(rd.GetString(0) + "    " + rd["UnitPrice"] + "    " + rd["UnitsInStock"]);
            }

            rd.Close();
            cn.Close();
        }
    }
}
