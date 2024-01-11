using System.Data.SqlClient;
using System.Data;
using Dapper;
using WebBolt.Client.Models;

namespace WebBolt.Client.Services
{
    public class GymService
    {
        #region Connection

        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("DefaultConnection"));

        #endregion Connection

        public async Task<List<Gym>> GetGyms()
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                await connection.OpenAsync();
                var gym = await connection.QueryAsync<Gym>("sp_GetGyms", commandType: CommandType.StoredProcedure);

                return gym.ToList();
            }
        }

        public Gym? GetGymById(int id)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Gym_id", id, DbType.Int32);

                // Call the stored procedure using Dapper
                var gym = connection.QueryFirstOrDefault<Gym>("sp_GetGymById", parameters, commandType: CommandType.StoredProcedure);

                return gym;
            }
        }

        public string AddGym(Gym newGym)
        {
            using (IDbConnection connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Name", newGym.Name, DbType.String);
                parameters.Add("Is_deleted", newGym.Is_deleted, DbType.Boolean);

                // Using Dapper to execute the stored procedure with parameters
                var gym = connection.Execute("sp_InsertGym", parameters, commandType: CommandType.StoredProcedure);
                return gym.ToString();
            }
        }

        public string DeleteGym(Gym objGym)
        {
            using (var connection = new SqlConnection(ConnData.ConnectionString))
            {
                connection.Open();

                // Call the stored procedure using Dapper
                string sql = "DELETE FROM Gyms WHERE Gym_id = @Gym_id";
                connection.Execute(sql, new { objGym.Gym_id });

                return objGym.Gym_id.ToString();
            }
        }
    }
}