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
        public async Task Delete_ExistingEnvironment_DeletesEnvironment_ReturnsNoContent()
        {
            // ARRANGE alle testwaarden aanmaken en een repository mocken
            var environmentId = Guid.NewGuid(); 

            var environmentRepository = new Mock<IEnvironment2DRepository>();
            environmentRepository.Setup(repo => repo.DeleteAsync(environmentId))
                                 .Verifiable();  // Verifiable ensures DeleteAsync was called

            var environmentController = new Environment2DController(environmentRepository.Object, null);

            // ACT de neppe environment zogenaamd deleten
            var response = await environmentController.DeleteAsync(environmentId);

            // ASSERT als het reseltaat juist is, en maar één keer uitgevoerd is, is alles goed gegaan
            var result = Assert.IsType<NoContentResult>(response);

            
            environmentRepository.Verify(repo => repo.DeleteAsync(environmentId), Times.Once);
        }
    }
}