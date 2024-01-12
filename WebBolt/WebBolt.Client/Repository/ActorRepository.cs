using Dapper;
using WebBolt.Client.Models;
using System.Data;
using System.Data.SqlClient;

namespace WebBolt.Client.Repository
{
    public static class ActorRepository
    {
        #region Connection

        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("SqlDbContext"));

        #endregion Connection

        #region Create Actor

        public static async Task<ActorInfo> AddActorAsync(ActorInfo actor)
        {
            await using SqlConnection sqlConnection = new(ConnData.ConnectionString);
            DynamicParameters parameters = new();
            parameters.Add("FirstName", actor.FirstName);
            parameters.Add("FamilyName", actor.FamilyName);
            parameters.Add("DoB", actor.DoB);
            parameters.Add("DoD", actor.DoD);
            parameters.Add("GenderType", actor.GenderType);

            string sQuery = "spActor_Insert";
            await sqlConnection.ExecuteAsync(sQuery, parameters, commandType: CommandType.StoredProcedure);

            return null;
        }

        #endregion Create Actor

        #region Read Actor

        public static async Task<List<ActorInfo>> GetActorAsync()
        {
            await using SqlConnection sqlConnection = new(ConnData.ConnectionString);
            string sQuery = "select * from vActorData";
            var actors = sqlConnection.Query<ActorInfo>(sQuery);

            return actors.ToList();
        }

        #endregion Read Actor

        #region Read By Id Actor

        public static List<ActorInfo> GetActorById(int actorId)
        {
            using SqlConnection sqlConnection = new(ConnData.ConnectionString);
            DynamicParameters parameters = new();
            parameters.Add("Id", actorId, DbType.Int32);

            string sQuery = "spActor_GetOne";
            var actor = sqlConnection.Query<ActorInfo>(sQuery, parameters, commandType: CommandType.StoredProcedure);
            return actor.ToList();
        }

        #endregion Read By Id Actor

        #region Update Actor

        public static async Task<ActorInfo> UpdateActorAsync(ActorInfo updatedActor)
        {
            await using SqlConnection sqlConnection = new(ConnData.ConnectionString);
            DynamicParameters parameters = new();
            parameters.Add("Id", updatedActor.ActorID, DbType.Int32);
            parameters.Add("FirstName", updatedActor.FirstName);
            parameters.Add("FamilyName", updatedActor.FamilyName);
            parameters.Add("DoB", updatedActor.DoB);
            parameters.Add("DoD", updatedActor.DoD);
            parameters.Add("GenderType", updatedActor.GenderType);

            string sQuery = "spActor_Update";
            await sqlConnection.ExecuteAsync(sQuery, parameters, commandType: CommandType.StoredProcedure);

            return updatedActor;
        }

        #endregion Update Actor

        #region Delete Actor

        public static async Task<ActorInfo> DeleteActorAsync(int actorId)
        {
            await using SqlConnection sqlConnection = new(ConnData.ConnectionString);
            DynamicParameters parameters = new();
            parameters.Add("Id", actorId, DbType.Int32);

            string sQuery = "spActor_Delete";
            await sqlConnection.ExecuteAsync(sQuery, parameters, commandType: CommandType.StoredProcedure);

            return null;
        }

        #endregion Delete Actor

        #region Public Method

        public static IEnumerable<ActorInfo> GetItems()
        {
            using IDbConnection dbConnection = ConnData;

            dbConnection.Open();

            var result = dbConnection.Query<ActorInfo>("spActor_GetAll").ToList();
            dbConnection.Close();
            return result;
        }

        #endregion Public Method
    }
}