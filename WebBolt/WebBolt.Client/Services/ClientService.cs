using Dapper;
using WebBolt.Client.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace WebBolt.Client.Services
{
    public class ClientService
    {
        #region Connection

        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("DefaultConnection"));

        #endregion Connection

        public List<ClientModel> GetClients()
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();
                var clients = connection.Query<ClientModel>("sp_GetClients", commandType: CommandType.StoredProcedure);
                return clients.AsList();
            }
        }

        public int AddClient(ClientModel newClient)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Name", newClient.Name, DbType.String);
                parameters.Add("Phone_number", newClient.Phone_number, DbType.String);
                parameters.Add("Email", newClient.Email, DbType.String);
                parameters.Add("Password", newClient.Password, DbType.String);
                parameters.Add("Is_deleted", newClient.Is_deleted, DbType.Boolean);
                parameters.Add("Photo", newClient.Photo, DbType.Binary, ParameterDirection.Input);
                parameters.Add("Inserted_date", newClient.Inserted_date, DbType.DateTime);
                parameters.Add("PIN", newClient.PIN, DbType.String);
                parameters.Add("Address", newClient.Address, DbType.String);
                parameters.Add("Bar_code", newClient.Bar_code, DbType.String);
                parameters.Add("Comments", newClient.Comments, DbType.String);
                parameters.Add("User_type", newClient.User_type, DbType.Int32);
                // Add other parameters as needed for your client properties

                // Call the stored procedure using Dapper
                int newClientId = connection.Query<int>("sp_AddClient", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return newClientId;
            }
        }

        public ClientModel GetClientById(int clientId)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Client_id", clientId, DbType.Int32);

                // Call the stored procedure using Dapper
                var client = connection.QueryFirstOrDefault<ClientModel>("sp_GetClientById", parameters, commandType: CommandType.StoredProcedure);

                return client;
            }
        }

        public ClientModel GetClientByBarCode(String GetClientByBarCode)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))

            {
                connection.Open();

                // Assuming "usp_GetClientByBarcode" is the name of your stored procedure
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Bar_code", GetClientByBarCode, DbType.String, ParameterDirection.Input);

                // Dapper will map the result to the Client class
                var result = connection.QueryFirstOrDefault<ClientModel>("sp_GetClientByBarcode", parameters, commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public bool IsBarcodeValid(string barcode)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                // Replace "YourTableName" with the actual name of your database table
                string query = "SELECT COUNT(*) FROM Clients WHERE Bar_code = @Bar_code";

                // Dapper QueryFirstOrDefault method to get a single result (count) or 0
                int count = connection.QueryFirstOrDefault<int>(query, new { Bar_code = barcode });

                // If count is greater than 0, barcode is valid
                return count > 0;
            }
        }

        public ClientModel GetClientByEmail(String email)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))

            {
                connection.Open();

                // Assuming "usp_GetClientByBarcode" is the name of your stored procedure
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Email", email, DbType.String, ParameterDirection.Input);

                // Dapper will map the result to the Client class
                var result = connection.QueryFirstOrDefault<ClientModel>("sp_GetClientByEmail", parameters, commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public string UpdateClient(ClientModel objClient)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Client_id", objClient.Client_id, DbType.Int32);
                parameters.Add("Name", objClient.Name, DbType.String);
                parameters.Add("Phone_number", objClient.Phone_number, DbType.String);
                parameters.Add("Email", objClient.Email, DbType.String);
                parameters.Add("Password", objClient.Password, DbType.String);
                parameters.Add("Is_deleted", objClient.Is_deleted, DbType.Boolean);
                parameters.Add("Photo", objClient.Photo, DbType.Binary, ParameterDirection.Input);
                parameters.Add("Inserted_date", objClient.Inserted_date, DbType.DateTime);
                parameters.Add("PIN", objClient.PIN, DbType.String);
                parameters.Add("Address", objClient.Address, DbType.String);
                parameters.Add("Bar_code", objClient.Bar_code, DbType.String);
                parameters.Add("Comments", objClient.Comments, DbType.String);
                parameters.Add("User_type", objClient.User_type, DbType.Int32);
                // Add other parameters as needed for your client properties

                // Call the stored procedure using Dapper
                string sQuery = "sp_UpdateClient";
                connection.QueryFirstOrDefault<string>(sQuery, parameters, commandType: CommandType.StoredProcedure);

                return objClient.Client_id.ToString();
            }
        }

        public string DeleteClient(ClientModel objClient)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Client_id", objClient, DbType.Int32);

                string sql = "DELETE FROM Clients WHERE Client_id = @Client_id";
                // Call the stored procedure using Dapper
                connection.Execute(sql, new { Client_id = objClient.Client_id });

                return objClient.Client_id.ToString();
            }
        }
    }
}