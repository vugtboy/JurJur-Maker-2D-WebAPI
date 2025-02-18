using System.ComponentModel.DataAnnotations;

namespace JurJurMaker2D.WebApi;

public class Environment2D
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public int MaxLength { get; set; }

    public int MaxHeigth {  get; set; }
}
