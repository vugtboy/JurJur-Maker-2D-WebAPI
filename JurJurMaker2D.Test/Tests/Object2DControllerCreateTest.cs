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
    public sealed class Object2DControllerCreateTests
    {
        [Fact]
        public async Task Add_AddObject2DToEnvironment2D_ReturnsCreatedObject2D()
        {
            //ARANGE alles naamaken in de testklasse zodat we noeperts kunnen gebruiken om te testen in plaats van echte
            var environmentId = Guid.NewGuid();
            var newObject2D = GenerateRandomObject2D(environmentId);

            var objectRepository = new Mock<IObject2DRepository>();
           
            var environmentController = new Object2DController(objectRepository.Object);

            // ACT uitvoering van de noeperts, die dus doen wat er in het echt zou gebeuren als hij gebruikt word
            var response = await environmentController.CreateAsync(newObject2D);


            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(response);

            
            var createdObject = createdAtActionResult.Value as Object2D;

            //ASSERT kijken of de test geslaagd is door te controlleren of het resultaat niet null is en input met output te vergelijken
            Assert.NotNull(createdObject);
            
            Assert.Equal(newObject2D.Id, createdObject.Id);
        }

        //Voor een of meerdere nieuwe werelden te maken zoals in het voorbeeld

        private Object2D GenerateRandomObject2D(Guid environmentId)
        {
            var random = new Random();
            return new Object2D
            {
                Id = environmentId,
                PrefabID = random.Next(0, 16),
                PositionX = random.Next(0, 200),
                PositionY = random.Next(0, 100),
                RotationZ = random.Next(0, 360),
                objectId = Guid.NewGuid(),
                aditionalIndex = random.Next(0, 8)
            };
        }
    }
}
