namespace Cmps253.Spring2021.DataAccess
{
    public class ConnectionString : IConnectionString
    {
        public string server => "cmps.database.windows.net";

        public string database => "adventureworks";

        public string userId => "mbdeir";

        public string password => "!!Cmps253!!";
    }
}
