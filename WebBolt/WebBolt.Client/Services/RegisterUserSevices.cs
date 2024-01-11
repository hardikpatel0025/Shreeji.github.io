using System.Data.SqlClient;
using System.Data;
using Dapper;
using WebBolt.Client.Models;

namespace WebBolt.Client.Services
{
    public class RegisterUserSevices
    {
        #region Connection

        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("DefaultConnection"));

        #endregion Connection

        public int AddNewUser(Register newClient)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("User_Id", Guid.NewGuid(), DbType.String);
                parameters.Add("LastName", newClient.LastName, DbType.String);
                parameters.Add("FirstName", newClient.FistName, DbType.String);
                parameters.Add("Email", newClient.Email, DbType.String);
                parameters.Add("Birthdate", newClient.BirthDate, DbType.Boolean);
                parameters.Add("Photo", newClient.Photo, DbType.Binary, ParameterDirection.Input);
                
                // Add other parameters as needed for your client properties

                // Call the stored procedure using Dapper
                int newClientId = connection.Query<int>("sp_InsertNewUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return newClientId;
            }
        }
    }
}
