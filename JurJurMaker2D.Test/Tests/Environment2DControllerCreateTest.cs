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
            var newEnvironment = GenerateRandomEnvironment("environment");

            var environmentRepository = new Mock<IEnvironment2DRepository>();
            var authenticationService = new Mock<IAuthenticationService>();
           
            var environmentController = new Environment2DController(environmentRepository.Object, authenticationService.Object);

            // ACT uitvoering van de noeperts, die dus doen wat er in het echt zou gebeuren als hij gebruikt word
            var response = await environmentController.CreateAsync(newEnvironment);


            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(response);

            
            var createdEnvironment = createdAtActionResult.Value as Environment2D;

            //ASSERT kijken of de test geslaagd als er een resultaat is, en deze overeenkomt met de testwaardes dan is de test geslaagd
            Assert.NotNull(createdEnvironment);
            
            Assert.Equal(newEnvironment.Id, createdEnvironment.Id);
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
