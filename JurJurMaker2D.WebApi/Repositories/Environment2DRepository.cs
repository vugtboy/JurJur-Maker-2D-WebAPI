using Dapper;
using JurJurMaker2D.WebApi.Interfaces;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Common;
using System.Numerics;
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
            Guid PlayerId = environment2D.userId;
            int Music = environment2D.music;
            int Background = environment2D.background;
            int Player = environment2D.player;
            environments.Add(environment2D);
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("INSERT INTO dbo.Environment2D (Id, name, maxheigth, maxlength, playerId, music, background, player) VALUES (@Id, @name, @maxheigth, @maxlength, @playerId, @music, @background, @player)", new { Id = id, name = Name, maxheigth = maxHeigth, maxlength = maxLength, playerId = PlayerId, music = Music, background = Background, player = Player });
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
        public async Task<IEnumerable<Environment2D?>> ReadAllAsync(Guid Userid)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Environment2D>("SELECT * FROM dbo.Environment2D Where playerId = @playerId", new {playerId = Userid});
            }
        }

        //op id verwijderen uit tabel
        public async void DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM dbo.Environment2D WHERE Id = @Id", new { Id = id });
            }
        }

        //een tabel updaten
        public async void Update(Environment2D environment2D)
        {
            //tabel uitlezen op id
            Guid id = environment2D.Id;
            int maxLength = environment2D.MaxLength;
            int maxHeigth = environment2D.MaxHeigth;
            int Music = environment2D.music;
            int Background = environment2D.background;
            int Player = environment2D.player;
            environments.Add(environment2D);
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE dbo.Environment2D SET maxheigth = @maxheigth, maxlength = @maxlength, music = @music, background = @background, player = @player WHERE Id = @Id", new { Id = id, maxheigth = maxHeigth, maxlength = maxLength, music = Music, background = Background, player = Player });
            }
        }
    }
}
