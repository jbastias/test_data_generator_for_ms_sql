using System;
using System.Diagnostics;


namespace datatool
{
    public class DataStorer : AbstractDataStorer
    {

        public DataStorer(DataSettings settings)
            : base(settings)
        {

        }

        public void Run()
        {

            var querySql = string.Format(@"select top 200 * from [{0}].[{1}].customer_table", _settings.Database, _settings.Schema);

			RunQueryAndSaveResults("table_name", querySql);
            RunQueryAndSaveResults("table_name", querySql);

            Console.WriteLine(BuildQuerySqlForExistingTable("table_name", querySql));
            Console.WriteLine(BuildQuerySqlForNewTable("table_name", querySql));

            Debug.WriteLine(BuildQuerySqlForExistingTable("table_name", querySql));
            Debug.WriteLine(BuildQuerySqlForNewTable("table_name", querySql));


            RunStoredProcAndSaveResults("table2", "[spMyProc] ''test''");
            RunStoredProcAndSaveResults("table2", "[spMyProc] ''test''");

            Console.WriteLine(BuildStoredProcSqlForExistingTable("table2", "[spMyProc] ''test''"));
            Console.WriteLine(BuildStoredProcSqlForNewTable("table2", "[spMyProc] ''test''"));

            Debug.WriteLine(BuildStoredProcSqlForExistingTable("table2", "[spMyProc] ''test''"));
            Debug.WriteLine(BuildStoredProcSqlForNewTable("table2", "[spMyProc] ''test''"));

            Console.Read();

        }

    }
}
