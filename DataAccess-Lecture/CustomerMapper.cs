using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Cmps253.Spring2021.DataAccess
{
    public class CustomerMapper
    {
        SqlConnection cn;
        public CustomerMapper(string server, string database, string userId, string password)
        {
            cn = new SqlConnection($"server={server};database={database};user id={userId};password={password}");
            cn.Open();
        }
        public List<Customer> GetCustomers()
        {
            var cmd = new SqlCommand("select CustomerID,FirstName,LastName,EmailAddress from [SalesLT].[Customer] ", cn);
            var reader = cmd.ExecuteReader();
            var Customers = new List<Customer>();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string firstname = reader.GetString(1);
                string lastname = reader.GetString(2);
                string email = reader.GetString(3);

                Customers.Add(new Customer(id, firstname, lastname, email));
            }
            reader.Close();
            return Customers;
        }

        public void UpdateCustomer(Customer customer)
        {
            var cmd = new SqlCommand($"UPDATE [SalesLT].[Customer] set EmailAddress='{customer.Email}' where  CustomerID={customer.CustomerId}", cn);
            cmd.ExecuteNonQuery();
        }
    }
}
