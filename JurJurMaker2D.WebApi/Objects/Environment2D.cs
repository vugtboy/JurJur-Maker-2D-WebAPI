using System.ComponentModel.DataAnnotations;

namespace JurJurMaker2D.WebApi;

public class Environment2D
{
    public Guid userId { get; set; }
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int MaxLength { get; set; }
    public int MaxHeigth {  get; set; }
    public int player {  get; set; }
    public int music {  get; set; }
    public int background {  get; set; }

}
