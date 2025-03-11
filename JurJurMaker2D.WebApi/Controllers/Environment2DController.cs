using JurJurMaker2D.WebApi.Interfaces;
using JurJurMaker2D.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace JurJurMaker2D.WebApi.Controllers;


[ApiController]
[Authorize]
[Route("[controller]")]
public class Environment2DController : ControllerBase
{
    private readonly IEnvironment2DRepository _repository;
    private readonly IAuthenticationService _authentication;
    public Environment2DController(IEnvironment2DRepository repository, IAuthenticationService authentication)
    {
        _repository = repository;
        _authentication = authentication;
    }

    [HttpGet("GetAllEnvironment2D")]
    public async Task<IEnumerable<Environment2D?>> GetAll(Guid Userid)
    {
        return await _repository.ReadAllAsync(Userid);
    }

    [HttpGet("GetEnvironment2D")]
    public async Task<Environment2D?> Get(Guid id)
    {
        return await _repository.ReadAsync(id);
    }

    [HttpPut(Name = "UpdateEnvironment2D")]
    public ActionResult Update(Environment2D environment2D)
    {
        _repository.Update(environment2D);
        return Ok();
    }

    [HttpPost(Name = "CreateEnvironment2D")]
    public async Task<ActionResult> CreateAsync(Environment2D environment2D)
    {
        await _repository.CreateAsync(environment2D);
        return CreatedAtAction(nameof(Get), new { id = environment2D.Id }, environment2D);
    }

    [HttpDelete(Name = "DeleteEnvironment2D")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        _repository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("GetUserId")]
    public string? GetId()
    {
        return _authentication.GetCurrentAuthenticatedUserId();
    }
}
