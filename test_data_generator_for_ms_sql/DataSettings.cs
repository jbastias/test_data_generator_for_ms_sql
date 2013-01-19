
using System.Configuration;

namespace datatool
{
    public class DataSettings
    {

        public string ConnectionString { get; set; }
        public string Server { get; set; }
        public string Schema { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public DataSettings()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["datatool"].ConnectionString;
            Server = ConfigurationManager.AppSettings["server"];
            Schema = ConfigurationManager.AppSettings["schema"];
            Database = ConfigurationManager.AppSettings["database"];
            Username = ConfigurationManager.AppSettings["username"];
            Password = ConfigurationManager.AppSettings["password"];
        }

        public DataSettings(string connectionString, string server, string schema, string database, string username, string password)
        {
            ConnectionString = connectionString;
            Server = server;
            Schema = schema;
            Database = database;
            Username = username;
            Password = password;
        }

    }
}
