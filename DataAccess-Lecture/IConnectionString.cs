namespace Cmps253.Spring2021.DataAccess
{
    public interface IConnectionString
    {
        string server { get; }
        string database { get; }
        string userId { get; }
        string password { get; }
    }
}
