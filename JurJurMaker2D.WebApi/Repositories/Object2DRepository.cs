using Dapper;
using JurJurMaker2D.WebApi.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
namespace JurJurMaker2D.WebApi.Repositories
{
    public class Object2DRepository : IObject2DRepository
    {
        private string sqlConnectionString;
        public static List<Object2D> objects = new List<Object2D>();

        public Object2DRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }
        public async Task CreateAsync(Object2D object2D)
        {
            Guid id = object2D.Id;
            Guid ObjectId = object2D.objectId;
            int PrefabID = object2D.PrefabID;
            int PositionX = object2D.PositionX;
            int PositionY = object2D.PositionY;
            int RotationZ = object2D.RotationZ;
            int AditionalIndex = object2D.aditionalIndex;
            objects.Add(object2D);
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("INSERT INTO dbo.Object2D (Id, prefabID, positionX, positionY, rotationZ, aditionalIndex, objectId) VALUES (@Id, @prefabID, @positionX, @positionY, @rotationZ, @aditionalIndex, @objectId)", new { Id = id, prefabID = PrefabID, positionX = PositionX, positionY = PositionY, rotationZ = RotationZ, aditionalIndex = AditionalIndex, objectId = ObjectId });
            }
            //en dan naar de database schrijven
        }
        
        //uit de database lezen en een environment2D object uitlezen met de id die je erin stopt
        public async Task<Object2D?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Object2D>("SELECT * FROM dbo.Object2D WHERE Id = @Id", new {Id = id});
            }
        }

        //alle environments uitlezen
        public async Task<IEnumerable<Object2D?>> ReadAllAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Object2D>("SELECT * FROM dbo.Object2D WHERE Id = @Id", new {Id = id});
            }
        }

        //op id verwijderen uit tabel
        public async void DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM dbo.Object2D WHERE objectId = @objectId", new { objectId = id });
            }
        }

        //een tabel updaten
        public async void UpdateAsync(Object2D object2D)
        {
            //tabel uitlezen op id
            Guid ObjectId = object2D.objectId;
            int PrefabID = object2D.PrefabID;
            int PositionX = object2D.PositionX;
            int PositionY = object2D.PositionY;
            int RotationZ = object2D.RotationZ;
            int AditionalIndex = object2D.aditionalIndex;
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE dbo.Object2D SET prefabID = @prefabID, positionX = @positionX, positionY = @positionY, rotationZ = @rotationZ, aditionalIndex = @aditionalIndex WHERE objectId = @objectId", new {prefabID = PrefabID, positionX = PositionX, positionY = PositionY, rotationZ = RotationZ, aditionalIndex = AditionalIndex, objectId = ObjectId });
            }
        }
    }
}
