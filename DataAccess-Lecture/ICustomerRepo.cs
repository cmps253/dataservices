using System.Collections.Generic;

namespace Cmps253.Spring2021.DataAccess
{
    public interface ICustomerRepo
    {
        List<Customer> GetCustomers();
        void Update(Customer c);
        void Delete(Customer c);
        void Delete(int customerId);
        void Insert(Customer c);
    }
}
