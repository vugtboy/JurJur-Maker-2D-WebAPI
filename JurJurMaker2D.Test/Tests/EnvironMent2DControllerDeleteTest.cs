using JurJurMaker2D.WebApi.Controllers;
using JurJurMaker2D.WebApi.Interfaces;
using JurJurMaker2D.WebApi;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace JurJurMaker.Test
{
    public sealed class Environment2DControllerDeleteTest
    {
        [Fact]
        public async Task Delete_ExistingEnvironment_DeletesEnvironmentAndReturnsNoContent()
        {
            // ARRANGE
            var environmentId = Guid.NewGuid();  // The ID of the environment to be deleted.

            // Mock repository setup: when DeleteAsync is called with the environment ID, do nothing (void method).
            var environmentRepository = new Mock<IEnvironment2DRepository>();
            environmentRepository.Setup(repo => repo.DeleteAsync(environmentId))
                                 .Verifiable();  // Verifiable ensures DeleteAsync was called

            // Create the controller, injecting the mocked repository.
            var environmentController = new Environment2DController(environmentRepository.Object, null);

            // ACT: Call the Delete method on the controller.
            var response = await environmentController.DeleteAsync(environmentId);

            // ASSERT: Verify that the response is of type NoContentResult.
            var result = Assert.IsType<NoContentResult>(response);

            // Verify that DeleteAsync was actually called.
            environmentRepository.Verify(repo => repo.DeleteAsync(environmentId), Times.Once);
        }
    }
}