using Loop;
using Main;
using System.Collections.Generic;

namespace Yandex.SaveLoad.SaveData
{
    public class FaceData
    {
        private readonly List<int> _blockValues = new List<int>();

        public bool IsFace { get; private set; }
        public ShapeType ShapeType { get; private set; }
        public IReadOnlyList<int> BlockValues => _blockValues;

        public void SetData(FaceController faceController)
        {
            _blockValues.Clear();

            if (faceController.Faces == null || faceController.Faces.Count == 0)
            {
                IsFace = false;
                return;
            }
            else
            {
                IsFace = true;
            }

            ShapeType = faceController.ShapeType;

            foreach (Face face in faceController.Faces)
            {
                AddBlockValues(face);
            }

            if (ShapeType == ShapeType.Cub)
            {
                AddBlockValues(faceController.UpFace);
                AddBlockValues(faceController.DownFace);
            }
        }

        private void AddBlockValues(Face face)
        {
            foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(face.CellEdge - 1)))
            {
                if (face.GetCell(valueIJ.I, valueIJ.J).Block != null)
                {
                    _blockValues.Add(face.GetCell(valueIJ.I, valueIJ.J).Block.Meaning);
                }
                else
                {
                    _blockValues.Add(0);
                }
            }
        }
    }
}
