namespace Cmps253.Spring2021.DataAccess
{
    public class Customer
    {
        public Customer(int customerId, string firstName, string lastName, string email)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Email;
        }
    }
}
