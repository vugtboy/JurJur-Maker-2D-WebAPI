using System.ComponentModel.DataAnnotations;

namespace JurJurMaker2D.WebApi;

public class Object2D
{
    public Guid objectId { get; set; }
    public Guid Id { get; set; }
    public int PrefabID { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int RotationZ { get; set; }
    public int aditionalIndex {  get; set; }
}
