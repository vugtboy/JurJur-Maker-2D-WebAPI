using JurJurMaker2D.WebApi.Interfaces;
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

    [HttpGet("GetAllObject2D")]
    public async Task<IEnumerable<Object2D?>> GetAll(Guid id)
    {
        return await _repository.ReadAllAsync(id);
    }

    [HttpGet("GetObject2D")]
    public async Task<Object2D?> Get(Guid id)
    {
        return await _repository.ReadAsync(id);
    }

    [HttpPost(Name = "GetObject2D")]
    public ActionResult Create(Object2D Object2D)
    {
        _repository.CreateAsync(Object2D);
        return Created();
    }

    [HttpPut(Name = "UpdateObject2D")]
    public ActionResult Update(Object2D Object2D)
    {
        _repository.UpdateAsync(Object2D);
        return Ok();
    }

    [HttpDelete(Name = "DeleteObject2D")]
    public ActionResult Delete(Guid id)
    {
        _repository.DeleteAsync(id);
        return Ok();
    }
}