using JurJurMaker2D.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JurJurMaker2D.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class Environment2DController : ControllerBase
{
    private readonly ILogger<Environment2DController> _logger;
    private readonly IEnvironment2DRepository _repository;
    public Environment2DController(ILogger<Environment2DController> logger, IEnvironment2DRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet(Name = "GetEnvironment2D")]
    public async Task<IEnumerable<Environment2D?>> GetAll()
    {
        return await _repository.ReadAllAsync();
    }


    [HttpPost]
    public ActionResult Create(Environment2D environment2D)
    {
        _repository.CreateAsync(environment2D);
        return Created();
    }
}
