using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmps253.Spring2021.DataAccess
{
    public class CustomerRepo : ICustomerRepo
    {
        SqlConnection cn;
        public CustomerRepo(IConnectionString cs)
        {
            cn = new SqlConnection($"server={cs.server};database={cs.database};user id={cs.userId};password={cs.password}");
            cn.Open();
        }
        public void Delete(int customerId)
        {
            var cmd = new SqlCommand($"spDeleteCustomer3", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.ExecuteNonQuery();
        }
        public void Delete(Customer c)
        {
            if (c == null) return;
            Delete(c.CustomerId);
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
                string email = string.Empty;
                if(!reader.IsDBNull(3))
                {
                    email = reader.GetString(3);
                }

                Customers.Add(new Customer(id, firstname, lastname, email));
            }
            reader.Close();
            return Customers;
        }
        public Customer GetCustomerById(int customerId)
        {
            var cmd = new SqlCommand($"select CustomerID,FirstName,LastName,EmailAddress from [SalesLT].[Customer] where customerId={customerId}", cn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string firstname = reader.GetString(1);
                string lastname = reader.GetString(2);
                string email = string.Empty;
                if (!reader.IsDBNull(3))
                {
                    email = reader.GetString(3);
                }
                return new Customer(id, firstname, lastname, email);
            }
            reader.Close();
            return null;
        }
        public Customer GetCustomerByLastName(string lastName)
        {
            var cmd = new SqlCommand($"select CustomerID,FirstName,LastName,EmailAddress from [SalesLT].[Customer] where lastname='{lastName}'", cn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string firstname = reader.GetString(1);
                string lastname = reader.GetString(2);
                string email = string.Empty;
                if (!reader.IsDBNull(3))
                {
                    email = reader.GetString(3);
                }
                return new Customer(id, firstname, lastname, email);
            }
            reader.Close();
            return null;
        }
        public void Insert(Customer c)
        {
            var cmd = new SqlCommand($"spInsertCustomer", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
            cmd.Parameters.AddWithValue("@LastName", c.LastName);
            cmd.Parameters.AddWithValue("@PasswordHash", c.PasswordHash);
            cmd.Parameters.AddWithValue("@PasswordSalt", c.PasswordSalt);
            cmd.ExecuteNonQuery();
        }

        public void Update(Customer c)
        {
            var cmd = new SqlCommand($"UPDATE [SalesLT].[Customer] set EmailAddress='{c.Email}' where  CustomerID={c.CustomerId}", cn);
            cmd.ExecuteNonQuery();
        }
    }
}
