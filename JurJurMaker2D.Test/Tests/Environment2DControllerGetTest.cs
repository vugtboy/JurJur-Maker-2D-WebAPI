using JurJurMaker2D.WebApi.Controllers;
using JurJurMaker2D.WebApi.Interfaces;
using JurJurMaker2D.WebApi;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace JurJurMaker.Test
{
    public sealed class Environment2DControllerControllerGetTests
    {
        [Fact]
        public async Task Get_EnvironmentReturnsCorrectEnvironment()
        {
            // ARRANGE niewe environment maken
            var environmentId = Guid.NewGuid();
            var rnd = new Random();
            var expectedEnvironment = new Environment2D
            {
                Id = environmentId,
                Name = "Test Environment",
                MaxLength = rnd.Next(14, 200),
                MaxHeigth = rnd.Next(25, 100),
                userId = Guid.NewGuid(),
                player = rnd.Next(0, 3),
                music = rnd.Next(0, 3),
                background = rnd.Next(0, 4)
            };

            var environmentRepository = new Mock<IEnvironment2DRepository>();
            environmentRepository.Setup(repo => repo.ReadAsync(environmentId)).ReturnsAsync(expectedEnvironment);

            var environmentController = new Environment2DController(environmentRepository.Object, null);

            // ACT de environment uit de controller halen zogenaamd
            var response = await environmentController.Get(environmentId);

            // ASSERT als alles goed is gegaan komen de juiste waarden eruit, en is de environment gelijk aan de eringestopte environment
            var returnedEnvironment = Assert.IsType<Environment2D>(response);
            Assert.Equal(expectedEnvironment.Id, returnedEnvironment.Id); 
            Assert.Equal(expectedEnvironment.Name, returnedEnvironment.Name);
            Assert.Equal(expectedEnvironment.MaxLength, returnedEnvironment.MaxLength);
            Assert.Equal(expectedEnvironment.MaxHeigth, returnedEnvironment.MaxHeigth);
        }
    }
}