using System.Collections.Generic;

public class FaceData
{
    private List<int> _blockValues = new List<int>();

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
        for (int i = 0; i < face.CellEdge; i++)
        {
            for (int j = 0; j < face.CellEdge; j++)
            {
                if (face.GetCell(i, j).Block != null)
                {
                    _blockValues.Add(face.GetCell(i, j).Block.Meaning);
                }
                else
                {
                    _blockValues.Add(0);
                }
            }
        }
    }
}
