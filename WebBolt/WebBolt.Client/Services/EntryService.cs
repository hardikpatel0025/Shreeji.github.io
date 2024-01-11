using System.Data.SqlClient;
using System.Data;
using WebBolt.Client.Models;
using Dapper;

namespace WebBolt.Client.Services
{
    public class EntryService
    {
        #region Connection

        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("DefaultConnection"));

        #endregion Connection

        public List<Entry> GetEntries()
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();
                var entries = connection.Query<Entry>("sp_GetEntries", commandType: CommandType.StoredProcedure);

                return entries.AsList();
            }
        }

        public string AddEntry(Entry newEntry)
        {
            using (IDbConnection connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Client_Id", newEntry.Client_Id, DbType.Int32);
                parameters.Add("@Membership_Id", newEntry.Membership_Id, DbType.Int32);
                parameters.Add("@Date", newEntry.Date, DbType.DateTime);
                parameters.Add("@Bar_Code", newEntry.Bar_code, DbType.String);
                parameters.Add("@Gym_Id", newEntry.Gym_Id, DbType.Int32);

                // Using Dapper to execute the stored procedure with parameters
                var entry = connection.Execute("sp_InsertEntry", parameters, commandType: CommandType.StoredProcedure);
                return entry.ToString();
            }
        }
    }
}