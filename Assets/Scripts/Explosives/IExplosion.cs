using Main;
using System.Collections.Generic;

namespace Explosives
{
    public interface IExplosion
    {
        public List<Cell> GetCellTarget(Cell cell, FaceController faceController);
    }
}
