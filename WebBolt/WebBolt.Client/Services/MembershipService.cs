using System.Data.SqlClient;
using System.Data;
using WebBolt.Client.Models;
using Dapper;
using System.Net;
using System.Data.Common;

namespace WebBolt.Client.Services
{
    public class MembershipService
    {
        #region Connection

        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("DefaultConnection"));

        #endregion Connection

        public List<Membership> GetMemberships()
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();
                var members = connection.Query<Membership>("sp_GetMembers", commandType: CommandType.StoredProcedure);

                return members.AsList();
            }
        }

        public string AddMembership(Membership membership)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Gym_id", membership.Gym_id, DbType.Int32);
                parameters.Add("@Name", membership.Name, DbType.String);
                parameters.Add("@Price", membership.Price, DbType.Double);
                parameters.Add("@Days_available", membership.Days_available, DbType.Int32);
                parameters.Add("@Entries_available", membership.Entries_available, DbType.Int32);
                parameters.Add("@Start_time", membership.Start_time, DbType.String);
                parameters.Add("@End_time", membership.End_time, DbType.String);
                parameters.Add("@Usable_number", membership.Usable_number, DbType.Int32);
                parameters.Add("@Is_deleted", membership.Is_deleted, DbType.Boolean);

                // Call the stored procedure using Dapper
                connection.Query<int>("sp_AddMembership", parameters, commandType: CommandType.StoredProcedure);

                return membership.ToString();
            }
        }

        public Membership GetMembershipById(int id)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int32);

                var member = connection.QueryFirstOrDefault<Membership>("sp_GetByIdMembership", parameters, commandType: CommandType.StoredProcedure);
                return member;
            }
        }

        public string UpdateMembership(Membership updatedMembership)
        {
            using (IDbConnection connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", updatedMembership.Id, DbType.Int32);
                parameters.Add("@Gym_id", updatedMembership.Gym_id, DbType.Int32);
                parameters.Add("@Name", updatedMembership.Name, DbType.String);
                parameters.Add("@Price", updatedMembership.Price, DbType.Double);
                parameters.Add("@Days_available", updatedMembership.Days_available, DbType.Int32);
                parameters.Add("@Entries_available", updatedMembership.Entries_available, DbType.Int32);
                parameters.Add("@Start_time", updatedMembership.Start_time, DbType.String);
                parameters.Add("@End_time", updatedMembership.End_time, DbType.String);
                parameters.Add("@Usable_number", updatedMembership.Usable_number, DbType.Int32);
                parameters.Add("@Is_deleted", updatedMembership.Is_deleted, DbType.Boolean);

                // Add more parameters as needed based on your model properties

                // Using Dapper to execute the stored procedure with parameters
                string sQuery = "sp_UpdateMembership";
                connection.QueryFirstOrDefault<string>(sQuery, parameters, commandType: CommandType.StoredProcedure);

                return updatedMembership.ToString();
            }
        }

        public Membership DeleteMembership(Membership membership)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", membership, DbType.Int32);

                // Call the stored procedure using Dapper
                connection.QueryFirstOrDefault<string>("sp_DeleteMembership", parameters, commandType: CommandType.StoredProcedure);

                return membership;
            }
        }
    }
}