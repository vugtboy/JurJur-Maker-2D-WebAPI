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
            // ARRANGE
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

            // ACT
            var response = await environmentController.Get(environmentId);

            // ASSERT
            var returnedEnvironment = Assert.IsType<Environment2D>(response);  // Directly assert the returned object is of type Environment2D
            Assert.Equal(expectedEnvironment.Id, returnedEnvironment.Id);  // Ensure the returned Environment2D's ID matches the expected ID
            Assert.Equal(expectedEnvironment.Name, returnedEnvironment.Name);  // Ensure the returned Name matches
            Assert.Equal(expectedEnvironment.MaxLength, returnedEnvironment.MaxLength);  // Ensure MaxLength matches
            Assert.Equal(expectedEnvironment.MaxHeigth, returnedEnvironment.MaxHeigth);
        }
    }
}