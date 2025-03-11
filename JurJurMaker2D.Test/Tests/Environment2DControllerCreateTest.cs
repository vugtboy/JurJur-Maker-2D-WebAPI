using Azure;
using JurJurMaker2D.WebApi.Controllers;
using JurJurMaker2D.WebApi.Interfaces;
using JurJurMaker2D.WebApi;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using JurJurMaker2D.WebApi.Repositories;
using System.Collections.Generic;
using JurJurMaker2D.WebApi.Services;
namespace JurJurMaker.Test
{
    public sealed class Environment2DControllerCreateTests
    {
        [Fact]
        public async Task Add_AddEnvinroment2DToUserWithNoEnviroments_ReturnsCreatedEnvironment2D()
        {
            //ARANGE alles naamaken in de testklasse zodat we noeperts kunnen gebruiken om te testen in plaats van echte
            var userId = Guid.NewGuid();
            var newEnvironment = GenerateRandomEnvironment("environment");
            var existingUserEnvironments = GenerateRandomEnvironments(0);

            var environmentRepository = new Mock<IEnvironment2DRepository>();
            var objectRepository = new Mock<IObject2DRepository>();
            var authenticationService = new Mock<IAuthenticationService>();

            environmentRepository.Setup(x => x.ReadAllAsync(userId)).ReturnsAsync(existingUserEnvironments);
           
            var environmentController = new Environment2DController(environmentRepository.Object, authenticationService.Object);

            // ACT uitvoering van de noeperts, die dus doen wat er in het echt zou gebeuren als hij gebruikt word
            var response = await environmentController.CreateAsync(newEnvironment);


            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(response);

            
            var createdEnvironment = createdAtActionResult.Value as Environment2D;

            //ASSERT kijken of de test geslaagd is door te controlleren of het resultaat niet null is en input met output te vergelijken
            Assert.NotNull(createdEnvironment);
            
            Assert.Equal(newEnvironment.Id, createdEnvironment.Id);
        }

        //Voor een of meerdere nieuwe werelden te maken zoals in het voorbeeld
        private List<Environment2D> GenerateRandomEnvironments(int numberOfEnvironments)
        {
            List<Environment2D> list = [];

            for (int i = 0; i < numberOfEnvironments; i++)
            {
                list.Add(GenerateRandomEnvironment($"Random Environment {i}"));
            }

            return list;
        }

        private Environment2D GenerateRandomEnvironment(string name)
        {
            var random = new Random();
            return new Environment2D
            {
                Id = Guid.NewGuid(),
                MaxHeigth = random.Next(14, 100),
                MaxLength = random.Next(25, 200),
                Name = name ?? "Random environment",
                userId = Guid.NewGuid(),
                player = random.Next(0, 2),
                music = random.Next(0, 2),
                background = random.Next(0, 3)
            };
        }
    }
}
