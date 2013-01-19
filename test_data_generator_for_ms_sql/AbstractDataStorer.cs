using System;
using System.Data.SqlClient;

namespace datatool
{
    public class AbstractDataStorer
    {
        protected DataSettings _settings;

        public AbstractDataStorer(DataSettings settings)
        {
            _settings = settings;
        }

        public void RunStoredProcAndSaveResults(string tableName, string storedProcWithArgs)
        {
            var sql = DoesTableExist(tableName) ?
                        BuildStoredProcSqlForExistingTable(tableName, storedProcWithArgs) :
                        BuildStoredProcSqlForNewTable(tableName, storedProcWithArgs);

            using (var conn = new SqlConnection(_settings.ConnectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }

        }

        public string BuildStoredProcSqlForExistingTable(string tableName, string storedProcWithArgs)
        {
            return BuildStoredProcSqlForExistingTable(tableName, storedProcWithArgs, _settings.Server, _settings.Username,
                                            _settings.Password, _settings.Database, _settings.Schema);
        }

        public string BuildStoredProcSqlForExistingTable(string tableName, string storedProcWithArgs, string server, string username, string password, string database, string schema)
        {
            const string sql = @"insert into {0}
                                 SELECT  *
                                 FROM    OPENROWSET ('SQLOLEDB','{2}'; '{3}'; '{4}','set fmtonly off exec [{5}].[{6}].{1}')
                                 AS tbl";
            return string.Format(sql, tableName, storedProcWithArgs, server, username, password, database, schema);
        }

        public string BuildStoredProcSqlForNewTable(string tableName, string storedProcWithArgs)
        {
            return BuildStoredProcSqlForNewTable(tableName, storedProcWithArgs, _settings.Server, _settings.Username,
                                       _settings.Password, _settings.Database, _settings.Schema);
        }

        public string BuildStoredProcSqlForNewTable(string tableName, string storedProcWithArgs, string server, string username, string password, string database, string schema)
        {
            const string sql = @"SELECT  * 
                                 INTO {0}
                                 FROM    OPENROWSET ('SQLOLEDB','{2}'; '{3}'; '{4}','set fmtonly off exec [{5}].[{6}].{1}')
                                 AS tbl";
            return string.Format(sql, tableName, storedProcWithArgs, server, username, password, database, schema);
        }

        public void RunQueryAndSaveResults(string tableName, string query)
        {
            var sql = DoesTableExist(tableName) ?
              BuildQuerySqlForExistingTable(tableName, query) :
              BuildQuerySqlForNewTable(tableName, query);

            using (var conn = new SqlConnection(_settings.ConnectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }

        public string BuildQuerySqlForExistingTable(string tableName, string query)
        {
            return BuildQuerySqlForExistingTable(tableName, query, _settings.Server, _settings.Username, _settings.Password);
        }

        public string BuildQuerySqlForExistingTable(string tableName, string query, string server, string username, string password)
        {
            const string sql = @"insert into {0}
                                 SELECT  *                                  
                                 FROM    OPENROWSET ('SQLOLEDB','{2}'; '{3}'; '{4}','{1}')
                                 AS tbl";
            return string.Format(sql, tableName, query, server, username, password);
        }

        public string BuildQuerySqlForNewTable(string tableName, string query)
        {
            return BuildQuerySqlForNewTable(tableName, query, _settings.Server, _settings.Username, _settings.Password);
        }

        public string BuildQuerySqlForNewTable(string tableName, string query, string server, string username, string password)
        {
            const string sql = @"SELECT  * 
                                 INTO {0}
                                 FROM    OPENROWSET ('SQLOLEDB','{2}'; '{3}'; '{4}','{1}')
                                 AS tbl";
            return string.Format(sql, tableName, query, server, username, password);
        }

        public bool DoesTableExist(string tableName)
        {
            return DoesTableExist(_settings.Schema, tableName);
        }

        public bool DoesTableExist(string schemaName, string tableName)
        {
            using (var conn = new SqlConnection(_settings.ConnectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
 
                const string sql = @"declare @tableExists int
                                    if exists(SELECT * 
                                                FROM INFORMATION_SCHEMA.TABLES 
                                                WHERE TABLE_SCHEMA = '{0}' 
                                                AND  TABLE_NAME = '{1}')
	                                    set @tableExists = 1
                                    else
	                                    set @tableExists = 0
	
                                    select @tableExists as TableExists";

                cmd.CommandText = string.Format(sql, schemaName, tableName);
                var returnValue = cmd.ExecuteScalar();

                return returnValue != null && Convert.ToInt32(returnValue) != 0;
            }

        }

    }
}
