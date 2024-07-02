using System.Collections.Generic;

public class LimitingData
{
    private List<int> _limitingMovementsValues = new List<int>();

    public bool IsLimitMove { get; private set; }
    public IReadOnlyList<int> LimitingMovementsValues => _limitingMovementsValues;

    public void SetData(bool isLimitMove, Dictionary<Face, int> faceMoves, FaceController faceController)
    {
        _limitingMovementsValues.Clear();
        
        IsLimitMove = isLimitMove;

        if (IsLimitMove == false)
        {
            return;
        }

        foreach (Face face in faceController.Faces)
        {
            _limitingMovementsValues.Add(faceMoves[face]);
        }

        if (faceController.ShapeType == ShapeType.Cub)
        {
            _limitingMovementsValues.Add(faceMoves[faceController.UpFace]);
            _limitingMovementsValues.Add(faceMoves[faceController.DownFace]);
        }
    }
}
