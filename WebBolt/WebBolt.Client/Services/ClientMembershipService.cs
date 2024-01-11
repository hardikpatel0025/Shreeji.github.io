using WebBolt.Client.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace WebBolt.Client.Services
{
    public class ClientMembershipService
    {
        #region Connection

        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("DefaultConnection"));

        #endregion Connection

        public List<ClientMembership> GetClientMemberships()
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();
                var clientMember = connection.Query<ClientMembership>("sp_GetClientMembes", commandType: CommandType.StoredProcedure);

                return clientMember.AsList();
            }
        }

        public ClientMembership GetClientMembershipById(int id)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Client_Membership_Id", id, DbType.Int32);

                var member = connection.QueryFirstOrDefault<ClientMembership>("sp_GetClientMembershipById", parameters, commandType: CommandType.StoredProcedure);
                return member;
            }
        }

        public List<ClientMembership> GetClientMembershipsByBarCode(String bar_code)
        {
            using (IDbConnection connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Bar_Code", bar_code, DbType.String, ParameterDirection.Input);

                // Using Dapper to execute the stored procedure with parameters
                var result = connection.Query<ClientMembership>("sp_GetClientMembershipByBarCode", parameters, commandType: CommandType.StoredProcedure
                ).ToList();

                return result;
            }
        }

        public string AddClientMembership(ClientMembership newClientMembership)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnData.ConnectionString))
            {
                dbConnection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Client_Id", newClientMembership.Client_Id, DbType.Int32);
                parameters.Add("@Membership_Id", newClientMembership.Membership_Id, DbType.Int32);
                parameters.Add("@Buying_Date", newClientMembership.Buying_Date, DbType.DateTime);
                parameters.Add("@Bar_Code", newClientMembership.Bar_Code, DbType.String);
                parameters.Add("@Entry_Count", newClientMembership.Entry_Count, DbType.Int32);
                parameters.Add("@Price", newClientMembership.Price, DbType.Double);
                parameters.Add("@Available_until", newClientMembership.Available_until, DbType.DateTime2);
                parameters.Add("@First_Usage_Date", newClientMembership.First_Usage_Date, DbType.DateTime2);
                parameters.Add("@Gym_Id", newClientMembership.Gym_Id, DbType.Int32);

                // Add more parameters as needed based on your model properties

                // Using Dapper to execute the stored procedure with parameters
                dbConnection.Execute("sp_InsertClientMembership", parameters, commandType: CommandType.StoredProcedure);

                return newClientMembership.Client_Id.ToString();
            }
        }

        public string UpdateClientMembership(ClientMembership updatedClientMembership)
        {
            using (IDbConnection connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Client_Membership_Id", updatedClientMembership.Client_Membership_Id, DbType.Int32);
                parameters.Add("@Client_Id", updatedClientMembership.Client_Id, DbType.Int32);
                parameters.Add("@Membership_Id", updatedClientMembership.Membership_Id, DbType.Int32);
                parameters.Add("@Buying_Date", updatedClientMembership.Buying_Date, DbType.DateTime2);
                parameters.Add("@Bar_Code", updatedClientMembership.Bar_Code, DbType.String);
                parameters.Add("@Entry_Count", updatedClientMembership.Entry_Count, DbType.Int32);
                parameters.Add("@Price", updatedClientMembership.Price, DbType.Double);
                parameters.Add("@Available_until", updatedClientMembership.Available_until, DbType.DateTime2);
                parameters.Add("@First_Usage_Date", updatedClientMembership.First_Usage_Date, DbType.DateTime2);
                parameters.Add("@Gym_Id", updatedClientMembership.Gym_Id, DbType.Int32);

                // Add more parameters as needed based on your model properties

                // Using Dapper to execute the stored procedure with parameters
                connection.Execute("sp_UpdateClientMembership", parameters, commandType: CommandType.StoredProcedure);
            }

            return null;
        }
    }
}