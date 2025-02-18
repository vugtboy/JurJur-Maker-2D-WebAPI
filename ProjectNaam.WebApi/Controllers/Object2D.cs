using JurJurMaker2D.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JurJurMaker2D.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class Object2DController : ControllerBase
{
    private readonly ILogger<Object2DController> _logger;
    private readonly IObject2DRepository _repository;
    public Object2DController(ILogger<Object2DController> logger, IObject2DRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet(Name = "GetObject2D")]
    public async Task<IEnumerable<Object2D?>> GetAll()
    {
        return await _repository.ReadAllAsync();
    }

    [HttpPost]
    public ActionResult Create(Object2D Object2D)
    {
        _repository.CreateAsync(Object2D);
        return Created();
    }
}