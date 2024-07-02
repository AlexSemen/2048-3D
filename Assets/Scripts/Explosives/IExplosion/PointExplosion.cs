using System.Collections.Generic;

public class PointExplosion : IExplosion
{
    public List<Cell> GetCellTarget(Cell cell, FaceController faceController)
    {
        return new List<Cell>() {cell};
    }
}
