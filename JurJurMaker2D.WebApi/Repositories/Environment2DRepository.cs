using Dapper;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
namespace JurJurMaker2D.WebApi.Repositories
{
    public class Environment2DRepository : IEnvironment2DRepository
    {
        private string sqlConnectionString;
        public static List<Environment2D> environments = new List<Environment2D>();

        public Environment2DRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }
        public async void CreateAsync(Environment2D environment2D)
        {
            Guid id = environment2D.Id;
            string Name = environment2D.Name;
            int maxLength = environment2D.MaxLength;
            int maxHeigth = environment2D.MaxHeigth;
            environments.Add(environment2D);
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("INSERT INTO dbo.Environment2D (Id, name, maxheigth, maxlength) VALUES (@Id, @name, @maxheigth, @maxlength)", new { Id = id, name = Name, maxheigth = maxHeigth, maxlength = maxLength });
            }
            //en dan naar de database schrijven
        }
        
        //uit de database lezen en een environment2D object uitlezen met de id die je erin stopt
        public async Task<Environment2D?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Environment2D>("SELECT * FROM dbo.Environment2D WHERE Id = @Id", new {Id = id});
            }
        }

        //alle environments uitlezen
        public async Task<IEnumerable<Environment2D?>> ReadAllAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Environment2D>("SELECT * FROM dbo.Environment2D");
            }
        }

        //op id verwijderen uit tabel
        public async void DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE * FROM dbo.Environment2D WHERE Id = @Id", new { Id = id });
            }
        }

        //een tabel updaten
        public async void UpdateAsync(Environment2D environment)
        {
            //tabel uitlezen op id
            Guid id = environment.Id;
            string Name = environment.Name;
            int maxLength = environment.MaxLength;
            int maxHeigth = environment.MaxHeigth;
            environments.Add(environment);
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPATE dbo.Environment2D SET (Id, name, maxheigth, maxlength) VALUES (@Id, @name, @maxheigth, @maxlength)", new { Id = id, name = Name, maxheigth = maxHeigth, maxlength = maxLength });
            }
        }
    }
}
