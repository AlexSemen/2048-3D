using System.Collections.Generic;

public interface IExplosion
{
    public List<Cell> GetCellTarget(Cell cell, FaceController faceController);
}
