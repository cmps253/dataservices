using System;
using System.Linq;

namespace Cmps253.Spring2021.DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            var customerRepo = new CustomerRepo(new ConnectionString());
            //var customers = customerRepo.GetCustomers();
            //customerRepo.Delete(30000);
            customerRepo.Insert(new Customer(343, "Jane", "Doe", "janedoe@aub.edu.lb"));
            return;

            var Mapper = new CustomerMapper("cmps.database.windows.net", "adventureworks", "mbdeir","!!Cmps253!!");

            Console.WriteLine("Hello World!");

            while (true)
            {
                Console.Write("enter and id: ");
                int CustomerId = int.Parse(Console.ReadLine());
                if (CustomerId == -1) return;

                Customer customer = Mapper.GetCustomers().SingleOrDefault(p => p.CustomerId == CustomerId);

                if (customer != null)
                {
                    Console.WriteLine(customer);
                    Console.WriteLine("Do you want to update?");
                    if(Console.ReadLine().ToLower()=="y")
                    {
                        customer.Email = "mbdeir@aub.edu.lb";
                        Mapper.UpdateCustomer(customer);
                    }
                }
                else
                {
                    Console.WriteLine("not found");
                }
            }
        }
    }
}
